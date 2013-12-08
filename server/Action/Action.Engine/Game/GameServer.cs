using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Concurrent;
using System.ComponentModel.Composition;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.Common;
using System.Timers;
using Action.Core;
using System.Threading.Tasks;

namespace Action.Engine
{
    public class GameServer : ActionServer<GameSession>
    {
        private int _passedSec = 0;
        private Timer _timer;

        public GameServer()
        {
            _gate = new GameGate(this);
            _world = new GameWorld(this);
            _mainQueue = new CallbackQueue("Main");
            _chatQueue = new CallbackQueue("Chat");

            _timer = new Timer(1000);
            _timer.Elapsed += (o, e) =>
            {
                var now = DateTime.Now;
                //RaiseModuleTick(1);
                _passedSec++;
                if (_passedSec == 1800)
                    _passedSec = 0;
                if (_passedSec % 300 == 0)
                    RaiseModuleTick(300);
                if ((now.Minute == 0 || now.Minute == 30) && now.Second == 0)
                {
                    Logger.LogInfo("执行整点半小时计划");
                    RaiseModuleTick(1800);
                }
            };
        }

        private void RaiseModuleTick(int sec)
        {
            foreach (var module in _moduleFactory.Modules)
            {
                switch (sec)
                {
                    //case 1:
                    //    module.RaisePerSecond(_world);
                    //    break;
                    case 300:
                        module.RaisePerFiveMinutes(_world);
                        break;
                    case 1800:
                        module.RaisePerHalfHour(_world);
                        break;
                }
            }
        }

        [ImportMany]
        private IEnumerable<GameCommandBase> _commands = null;
        public override IEnumerable<ActionCommandBase<GameSession>> Commands
        {
            get { return _commands; }
        }

        public T FindCommand<T>() where T : GameCommandBase
        {
            return _typeCommands[typeof(T)] as T;
        }

        private CallbackQueue _mainQueue;
        public CallbackQueue MainQueue
        {
            get { return _mainQueue; }
        }

        private CallbackQueue _chatQueue;
        public CallbackQueue ChatQueue
        {
            get { return _chatQueue; }
        }

        private GameGate _gate;
        public GameGate Gate
        {
            get { return _gate; }
        }

        private GameWorld _world;
        public GameWorld World
        {
            get { return _world; }
        }

        private GameModuleFactory _moduleFactory = new GameModuleFactory();
        public GameModuleFactory ModuleFactory
        {
            get { return _moduleFactory; }
        }

        private GameProfile _profile = new GameProfile();
        public GameProfile Profile
        {
            get { return _profile; }
        }

        protected override void OnStartup()
        {
            base.OnStartup();

            //开启同步队列
            _mainQueue.Start(false);
            _chatQueue.Start(true);

            foreach (var module in _moduleFactory.Modules)
                module.RaiseLoad(_world);
            _opened = true;
            _timer.Start();
            Logger.LogInfo("游戏服务已启动");
        }

        protected override void OnStopped()
        {
            base.OnStopped();

            //关闭同步队列
            _mainQueue.Stop();
            _chatQueue.Stop();

            foreach (var module in _moduleFactory.Modules)
                module.RaiseUnload(_world);
            _opened = false;
            Logger.LogInfo("游戏服务已停止");
        }

        protected override void OnPerformanceDataCollected(GlobalPerformanceData globalPerfData, PerformanceData performanceData)
        {
            // TODO 周期性记录在线玩家数量等信息
            _profile.CollectTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _profile.CpuUsage = globalPerfData.CpuUsage.ToString("0.00") + "%";
            _profile.WorkingSet = globalPerfData.WorkingSet.ToString("###,###,###,##0");
            _profile.TotalThreadCount = globalPerfData.TotalThreadCount;
            _profile.AvailableWorkingThreads = globalPerfData.AvailableWorkingThreads;
            _profile.AvailableCompletionPortThreads = globalPerfData.AvailableCompletionPortThreads;
            _profile.MaxWorkingThreads = globalPerfData.MaxWorkingThreads;
            _profile.MaxCompletionPortThreads = globalPerfData.MaxCompletionPortThreads;
            _profile.Connections = SessionCount;
            _profile.LoginPlayers = _world.AllPlayers.Count();
            _profile.HandledCommands = performanceData.CurrentRecord.TotalHandledCommands.ToString("###,###,###,##0");
            _profile.HandlingRate = (int)((performanceData.CurrentRecord.TotalHandledCommands
                - performanceData.PreviousRecord.TotalHandledCommands)
                / performanceData.CurrentRecord.RecordSpan);
            _profile.MainQueueTasks = _mainQueue.Count;
            _profile.MainQueueBusy = _mainQueue.Busy;
            _profile.ChatQueueTasks = _chatQueue.Count;
            _profile.ChatQueueBusy = _chatQueue.Busy;
        }

        protected override void OnAppSessionClosed(object sender, AppSessionClosedEventArgs<GameSession> e)
        {
            if (e.Session.CommandLogger != null && e.Session.LoggedCommands.Count > 0)
                e.Session.CommandLogger.Save(e.Session);
            if (e.Session.Player.Status >= LoginStatus.EnterGate)
            {
                if (e.Session.Player.Status == LoginStatus.EnterGame)
                {
                    //各模块处理玩家离开游戏
                    foreach (var module in _moduleFactory.Modules)
                        module.RaiseLeaveGame(e.Session.Player);

                    //从世界删除玩家
                    this.World.RemovePlayer(e.Session.Player);
                }
                this.Gate.RemovePlayer(e.Session.Player);
            }

            //PerfCommands汇总到GameServer
            _profile.AddCommands(e.Session.CommandHistory.PerfCommands);
        }
    }
}
