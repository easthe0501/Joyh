using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase;
using Action.Core;

namespace Action.Engine
{
    public interface IActionCommand
    {
        void ExecuteCommand2(IActionSession session, BinaryCommandInfo commandInfo);
    }

    public abstract class ActionCommandBase<TAppSession> : CommandBase<TAppSession, BinaryCommandInfo>, IActionCommand
        where TAppSession : IAppSession, IAppSession<TAppSession, BinaryCommandInfo>, new()
    {
        public ActionCommandBase()
        {
            var baseType = GetType().BaseType;
            if (baseType != null && baseType.IsGenericType)
                _genericType = baseType.GetGenericArguments().FirstOrDefault();
        }

        private Type _genericType;
        public Type GenericType
        {
            get { return _genericType; }
        }

        protected int _id;
        public int ID
        {
            get { return _id; }
        }

        public abstract void ExecuteCommand2(IActionSession session, BinaryCommandInfo commandInfo);
    }
}
