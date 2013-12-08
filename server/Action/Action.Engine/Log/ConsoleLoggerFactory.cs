using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;

namespace Action.Engine
{
    public class ConsoleLoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new ConsoleLogger();
        }

        public ILogger CreateLogger(string name)
        {
            return new ConsoleLogger(name);
        }
    }
}
