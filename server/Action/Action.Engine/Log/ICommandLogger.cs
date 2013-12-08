using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Command;

namespace Action.Engine
{
    public interface ICommandLogger
    {
        //void LogCommand(GameSession session, int commandId, byte[] commandArgs);
        int Type { get; }
        void Save(GameSession session);
    }
}
