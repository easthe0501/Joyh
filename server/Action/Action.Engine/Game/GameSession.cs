using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using System.IO;
using ProtoBuf;
using System.Configuration;
using SuperSocket.Common;
using Action.Core;

namespace Action.Engine
{
    public class GameSession : ActionSession<GameSession>
    {
        public GameSession()
        {
            _player = new GamePlayer(this);
        }

        private int _commandLoggerType;
        public int CommandLoggerType
        {
            get { return _commandLoggerType; }
            set { _commandLogger = CommandLoggerFactory.Current.GetLogger(_commandLoggerType = value); }
        }

        private ICommandLogger _commandLogger;
        public ICommandLogger CommandLogger
        {
            get { return _commandLogger; }
        }

        private List<LoggedCommandInfo> _loggedCmds = new List<LoggedCommandInfo>();
        public List<LoggedCommandInfo> LoggedCommands
        {
            get { return _loggedCmds; }
        }

        public GameServer Server
        {
            get { return (GameServer)base.AppServer; }
        }

        public override void StartSession()
        {
            base.StartSession();
            CommandLoggerType = Global.Config.CommandLoggerType;
        }

        private GamePlayer _player;
        public GamePlayer Player
        {
            get { return _player; }
        }

        public void EnterGate(string account)
        {
            _player.Account = account;
            _player.Status = LoginStatus.EnterGate;

            var onlinePlayer = Server.Gate.GetPlayer(_player.Account);
            if (onlinePlayer != null)
            {
                //如果是同一个客户端实例，不做处理，否则踢掉在线用户
                if (onlinePlayer.Session == this)
                    return;
                _player.Data = onlinePlayer.Data;
                onlinePlayer.Session.Close();
            }

            Server.Gate.AddPlayer(_player);
        }

        public void EnterGame(string key, string name)
        {
            _player.Key = key;
            _player.Name = name;
            _player.Status = LoginStatus.EnterGame;

            //玩家游戏世界
            Server.World.AddPlayer(_player);

            //各模块处理玩家进入游戏
            foreach (var module in Server.ModuleFactory.Modules)
                module.RaiseEnterGame(_player);
        }

        private GameCommandHistory _cmdHis = new GameCommandHistory();
        public GameCommandHistory CommandHistory
        {
            get { return _cmdHis; }
        }

        private GameContext _context = new GameContext();
        public GameContext Context
        {
            get { return _context; }
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}/({2})", base.ToString(), Player.Name, Player.Account);
        }
    }
}
