using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using SuperSocket.Common;
using Action.Engine;
using Action.Core;

namespace Action.Log
{
    public class ColorLogger : ILogger
    {
        private LogHelper _logHelper;

        public ColorLogger()
            : this("Action")
        { }

        public ColorLogger(string name)
        {
            _logHelper = new LogHelper(this.Name = name);
        }

        public void LogDebug(string message)
        {
            Global.Output.WriteLine(ConsoleColor.Yellow, _logHelper.BuildLogText(message));
        }

        public void LogError(Exception e)
        {
            Global.Output.WriteLine(ConsoleColor.Red, _logHelper.BuildLogText(e));
        }

        public void LogError(string message)
        {
            Global.Output.WriteLine(ConsoleColor.Red, _logHelper.BuildLogText(message));
        }

        public void LogError(string title, Exception e)
        {
            Global.Output.WriteLine(ConsoleColor.Red, _logHelper.BuildLogText(title, e));
        }

        public void LogInfo(string message)
        {
            Global.Output.WriteLine(ConsoleColor.Green, _logHelper.BuildLogText(message));
        }

        public void LogPerf(string message)
        {
            Global.Output.WriteLine(ConsoleColor.Blue, _logHelper.BuildLogText(message));
        }

        public string Name { get; private set; }
    }
}
