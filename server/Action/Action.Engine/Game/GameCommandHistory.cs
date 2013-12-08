using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Command;
using Action.Core;

namespace Action.Engine
{
    public class GameCommandHistory
    {
        private Dictionary<int, DateTime> _cdCmds = new Dictionary<int, DateTime>();
        public Dictionary<int, DateTime> CdCommands
        {
            get { return _cdCmds; }
        }

        private Dictionary<int, long> _perfCmds = new Dictionary<int, long>();
        public Dictionary<int, long> PerfCommands
        {
            get { return _perfCmds; }
        }

        public void AddCdCommand(int commandId)
        {
            _cdCmds[commandId] = DateTime.Now;
        }

        public void AddPerfCommand(int commandId, long msel)
        {
            _perfCmds[commandId] = Math.Max(msel, _perfCmds.GetValue(commandId));
        }

        public DateTime GetCommandLastExecTime(int commandId)
        {
            var lastTime = DateTime.MinValue;
            if (_cdCmds.TryGetValue(commandId, out lastTime))
                return lastTime;
            else
                return DateTime.MinValue;
        }

        public DateTime GetTimeOutOfCommandCD(int commandId, int commandCD)
        {
            return GetCommandLastExecTime(commandId).AddMilliseconds(commandCD);
        }

        public int GetLeftSecondsWhenOutOfCommandCD(int commandId, int commandCD)
        {
            return (int)(GetCommandLastExecTime(commandId).AddMilliseconds(commandCD) - DateTime.Now).TotalSeconds;
        }

        public bool IsCommandInCD(int commandId, int commandCD)
        {
            return GetLeftSecondsWhenOutOfCommandCD(commandId, commandCD) > 0;
        }
    }
}
