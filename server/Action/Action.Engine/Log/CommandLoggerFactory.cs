using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Core;
using System.ComponentModel.Composition;

namespace Action.Engine
{
    public class CommandLoggerFactory
    {
        private static CommandLoggerFactory _current = null;
        public static CommandLoggerFactory Current
        {
            get
            {
                if (_current == null)
                    _current = new CommandLoggerFactory();
                return _current;
            }
        }

        private CommandLoggerFactory()
        {
            Composition.ComposeParts(this);
            foreach (var logger in _loggers)
                _hash.Add(logger.Type, logger);
        }

        [ImportMany]
        private IEnumerable<ICommandLogger> _loggers = null;
        private Dictionary<int, ICommandLogger> _hash = new Dictionary<int, ICommandLogger>();

        public ICommandLogger GetLogger(int type)
        {
            ICommandLogger logger = null;
            if (_hash.TryGetValue(type, out logger))
                return logger;
            return null;
        }
    }
}
