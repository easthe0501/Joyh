using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;

namespace Action.Engine
{
    public interface ILoggerFactory
    {
        ILogger CreateLogger();
        ILogger CreateLogger(string name);
    }
}
