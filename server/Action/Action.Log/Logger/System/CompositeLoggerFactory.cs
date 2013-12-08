using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;
using Action.Engine;

namespace Action.Log
{
    public class CompositeLoggerFactory : ILoggerFactory
    {
        private ILoggerFactory[] _subLoggerFactories;

        public CompositeLoggerFactory(params ILoggerFactory[] subLoggerFactories)
        {
            _subLoggerFactories = subLoggerFactories;
        }

        public ILogger CreateLogger()
        {
            return CreateLogger("Action");
        }

        public ILogger CreateLogger(string name)
        {
            return new CompositeLogger(name, CreateSubLoggers(name).ToArray());
        }

        private IEnumerable<ILogger> CreateSubLoggers(string name)
        {
            foreach (var subLoggerFactory in _subLoggerFactories)
                yield return subLoggerFactory.CreateLogger(name);
        }
    }
}
