using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;
using Action.Engine;

namespace Action.Log
{
    public class MongoLoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new MongoLogger();
        }

        public ILogger CreateLogger(string name)
        {
            return new MongoLogger(name);
        }
    }
}
