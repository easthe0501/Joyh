using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase;

namespace Action.Engine
{
    public class ActionProtocol : ICustomProtocol<BinaryCommandInfo>
    {
        public ICommandReader<BinaryCommandInfo> CreateCommandReader(IAppServer appServer)
        {
            return new ActionCommandHeaderReader(appServer);
        }
    }
}
