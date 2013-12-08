using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Command;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using Action.Core;

namespace Action.Engine
{
    public abstract class BackCommandBase : ActionCommandBase<BackSession>
    {
        protected BackCommandBase()
        {
            var type = this.GetType();
            _name = type.Name;

            var attr = Attribute.GetCustomAttribute(type, typeof(BackCommandAttribute));
            if (attr == null)
                _id = 0;
            else
                _id = ((BackCommandAttribute)attr).CommandId;
        }

        private string _name;
        public override string Name { get { return _name; } }

        public override void ExecuteCommand2(IActionSession session, BinaryCommandInfo commandInfo)
        {
            if (session is BackSession)
                ExecuteCommand2((BackSession)session, commandInfo);
        }

        public override void ExecuteCommand(BackSession session, BinaryCommandInfo commandInfo)
        {
            //TODO:执行命令
            try
            {
                Execute(session, commandInfo);
            }
            catch (Exception ex)
            {
                session.Logger.LogError(session, ToString(), ex);
            }
        }

        protected abstract void Execute(BackSession session, BinaryCommandInfo commandInfo);

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, ID);
        }
    }

    public abstract class BackCommand : BackCommandBase
    {
        protected override void Execute(BackSession session, BinaryCommandInfo commandInfo)
        {
            if (Ready(session))
                Run(session);
        }

        protected virtual bool Ready(BackSession session)
        {
            return true;
        }

        protected abstract void Run(BackSession session);
    }

    public abstract class BackCommand<T> : BackCommandBase
    {
        protected override void Execute(BackSession session, BinaryCommandInfo commandInfo)
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
                session.Logger.LogError(session, string.Format("命令[{0}]不能反序列化参数[{1}]", this.ToString(), typeof(T).FullName), ex);
                session.Close();
                return;
            }
            if (Ready(session, args))
                Run(session, args);
        }

        protected virtual bool Ready(BackSession session, T args)
        {
            return true;
        }

        protected abstract void Run(BackSession session, T args);
    }
}
