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
    public class BackCommandAttribute : ExportAttribute
    {
        public int CommandId { get; private set; }
        public BackCommandAttribute(int commandId)
            : base(typeof(BackCommandBase))
        {
            CommandId = commandId;
        }
    }
}
