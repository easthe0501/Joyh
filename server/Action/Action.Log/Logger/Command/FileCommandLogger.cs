using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.DataAccess;
using Action.Model;
using Action.Core;
using Action.Utility;

namespace Action.Log
{
    [Export(typeof(ICommandLogger))]
    public class FileCommandLogger : ICommandLogger
    {
        private string _logDir;

        public FileCommandLogger()
        {
            _logDir = Path.Combine(Global.LogDir, LOG.Command.Cls);
            if (!Directory.Exists(_logDir))
                Directory.CreateDirectory(_logDir);
        }

        public int Type
        {
            get { return 1; }
        }

        public void Save(GameSession session)
        {
            var cmdLog = new LOG.Command();
            cmdLog.Player = session.Player.Name;
            cmdLog.Import(session.LoggedCommands);
            var file = Path.Combine(_logDir,
                string.Format("{0}_{1}.acr", cmdLog.Player,
                DateTime.Now.ToString("yyyyMMddHHmmss")));
            UtcToLocal(cmdLog);
            File.WriteAllText(file, JsonHelper.ToJson(cmdLog));
        }

        private void UtcToLocal(LOG.Command cmdLog)
        {
            foreach (var item in cmdLog.Items)
                item.Time = item.Time.ToLocalTime();
        }
    }
}
