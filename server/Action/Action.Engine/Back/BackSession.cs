using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using ProtoBuf;
using Action.Core;

namespace Action.Engine
{
    public class BackSession : ActionSession<BackSession>
    {
        public BackServer Server
        {
            get { return (BackServer)base.AppServer; }
        }

        private bool _authorized = false;
        public bool Authorized
        {
            get { return _authorized; }
        }

        public bool Validate(string token)
        {
            if (token == Server.Token)
                return _authorized = true;
            else
                return false;
        }

        public override void StartSession()
        {
            base.StartSession();
#if DEBUG
            _authorized = true;
#else
            TimerHelper.Delay(CloseIfNotAuthorized, 10000);
#endif
        }

        private void CloseIfNotAuthorized()
        {
            if (!_authorized)
                Close();
        }
    }
}
