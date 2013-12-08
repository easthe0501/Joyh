using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Command;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using Action.Core;
using System.Diagnostics;

namespace Action.Engine
{
    public abstract class GameCommandBase : ActionCommandBase<GameSession>
    {
        protected GameCommandBase()
        {
            var type = this.GetType();
            _name = type.Name;

            var attr = Attribute.GetCustomAttribute(type, typeof(GameCommandAttribute));
            if (attr == null)
                _id = 0;
            else
                _id = ((GameCommandAttribute)attr).CommandId;
        }

        private string _name;
        public override string Name
        {
            get { return _name; }
        }

        protected virtual CallbackQueue Queue
        {
            get { return ServerContext.GameServer.MainQueue; }
        }

        public int CD { get; set; }
        public bool CheckRight { get; set; }

        public override void ExecuteCommand2(IActionSession session, BinaryCommandInfo commandInfo)
        {
            if (session is GameSession)
                ExecuteCommand((GameSession)session, commandInfo);
        }

        public override void ExecuteCommand(GameSession session, BinaryCommandInfo commandInfo)
        {
            try
            {
                //TODO:是否记录Command
                if (session.CommandLoggerType > 0)
                    session.LoggedCommands.Add(new LoggedCommandInfo(int.Parse(commandInfo.Key),
                        GenericType, commandInfo.Data));

                //TODO:权限认证
                if (CheckRight && !session.Player.Permission.Contains(ID))
                    return;

                //TODO:CD验证
                if (CD > 0 && session.CommandHistory.IsCommandInCD(ID, CD))
                {
                    session.SendResponse(100, ID);
                    return;
                }

                //TODO:执行命令
                try
                {
                    Execute(session, commandInfo);
                }
                finally
                {
                    session.CommandHistory.AddCdCommand(ID);
                }
            }
            catch (Exception ex)
            {
                session.Logger.LogError(session, string.Format("{0}/{1}", session.Player, ToString()), ex);
            }
        }

        protected abstract void Execute(GameSession session, BinaryCommandInfo commandInfo);

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, ID);
        }
    }

    public abstract class GameCommand : GameCommandBase
    {
        protected override void Execute(GameSession session, BinaryCommandInfo commandInfo)
        {
            if (Ready(session))
            {
                var queue = Queue;
                if (queue != null)
                    queue.Add(() => SafeRun(session));
                else
                    SafeRun(session);
            }
        }

        protected virtual bool Ready(GameSession session)
        {
            return session.Player.Status == LoginStatus.EnterGame;
        }

        private void SafeRun(GameSession session)
        {
            var sw = new Stopwatch();
            try
            {
                sw.Start();
                Run(session);
            }
            catch (Exception ex)
            {
                session.Logger.LogError(session, string.Format("{0}/{1}",
                    session.Player, ToString()), ex);
            }
            finally
            {
                sw.Stop();
                session.CommandHistory.AddPerfCommand(ID, sw.ElapsedMilliseconds);
            }
        }

        protected abstract void Run(GameSession session);
    }

    public abstract class GameCommand<T> : GameCommandBase
    {
        protected override void Execute(GameSession session, BinaryCommandInfo commandInfo)
        {
            T args;
            try
            {
                // UNDONE 对象池取args实例
                args = ActionCommandDataDeserializer.Deserialize<T>(commandInfo.Data);
            }
            catch (Exception ex)
            {
                //args = default(T);
                session.Logger.LogError(string.Format("命令[{0}]不能反序列化参数[{1}]\n{2}",
                    string.Format("{0}/{1}", session.Player, ToString()), typeof(T).FullName, ex.ToString()));
                session.Close();
                return;
            }
            if (Ready(session, args))
            {
                var queue = Queue;
                if (queue != null)
                    queue.Add(() => SafeRun(session, args));
                else
                    SafeRun(session, args);
            }
        }

        protected virtual bool Ready(GameSession session, T args)
        {
            return session.Player.Status == LoginStatus.EnterGame;
        }

        private void SafeRun(GameSession session, T args)
        {
            var sw = new Stopwatch();
            try
            {
                sw.Start();
                Run(session, args);
            }
            catch (Exception ex)
            {
                session.Logger.LogError(session, string.Format("{0}/{1}",
                    session.Player, ToString()), ex);
            }
            finally
            {
                sw.Stop();
                session.CommandHistory.AddPerfCommand(ID, sw.ElapsedMilliseconds);
            }
        }

        protected abstract void Run(GameSession session, T args);
    }
}
