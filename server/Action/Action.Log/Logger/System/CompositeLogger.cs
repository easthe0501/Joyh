using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;
using Action.Core;

namespace Action.Log
{
    public class CompositeLogger : ILogger
    {
        private ILogger[] _subLoggers;

        public CompositeLogger(string name, ILogger[] subLoggers)
        {
            this.Name = name;
            _subLoggers = subLoggers;
        }

        public void LogDebug(string message)
        {
            foreach (var logger in _subLoggers)
                _(()=> logger.LogDebug(message));
        }

        public void LogError(string message)
        {
            foreach (var logger in _subLoggers)
                _(()=> logger.LogError(message));
        }

        public void LogError(string title, Exception e)
        {
            foreach (var logger in _subLoggers)
                _(()=> logger.LogError(title, e));
        }

        public void LogError(Exception e)
        {
            foreach (var logger in _subLoggers)
                _(()=> logger.LogError(e));
        }

        public void LogInfo(string message)
        {
            foreach (var logger in _subLoggers)
                _(()=> logger.LogInfo(message));
        }

        public void LogPerf(string message)
        {
            foreach (var logger in _subLoggers)
                _(()=> logger.LogPerf(message));
        }

        public string Name { get; private set; }

        private void _(Callback callback)
        {
            try
            {
                callback();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
