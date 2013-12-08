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
using Action.Core;

namespace Action.Engine
{
    public class BackServer : ActionServer<BackSession>
    {
        [ImportMany]
        private IEnumerable<BackCommandBase> _commands = null;
        public override IEnumerable<ActionCommandBase<BackSession>> Commands
        {
            get { return _commands; }
        }

        public T FindCommand<T>() where T : BackCommandBase
        {
            return _typeCommands[typeof(T)] as T;
        }

        private string _token;
        public string Token
        {
            get { return _token; }
        }

        protected override void OnStartup()
        {
            base.OnStartup();
            _token = Global.Config.BackServerToken;
            _opened = true;
            Logger.LogInfo("后台服务已启动");
        }

        protected override void OnStopped()
        {
            base.OnStopped();
            _opened = false;
            Logger.LogInfo("后台服务已停止");
        }
    }
}
