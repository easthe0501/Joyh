using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;
using Action.Engine;

namespace Action.Log
{
    public class FileLoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new FileLogger();
        }

        public ILogger CreateLogger(string name)
        {
            return new FileLogger(name);
        }
    }
}
