using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using SuperSocket.SocketBase.Command;

namespace Action.Engine
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GameCommandAttribute : ExportAttribute
    {
        public int CommandId { get; private set; }
        public GameCommandAttribute(int commandId)
            : base(typeof(GameCommandBase))
        {
            CommandId = commandId;
        }
    }
}
