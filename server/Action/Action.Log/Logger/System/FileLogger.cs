using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;
using System.IO;
using Action.Core;
using Action.Model;

namespace Action.Log
{
    public class FileLogger : ILogger
    {
        private LogHelper _logHelper;
        private FileStream _fsDebug;
        private FileStream _fsError;
        private FileStream _fsInfo;
        private FileStream _fsPref;

        public FileLogger()
            : this("Action")
        { }

        public FileLogger(string name)
        {
            _logHelper = new LogHelper(this.Name = name);
            _fsDebug = CreateStream(Path.Combine(Global.LogDir, LOG.Debug.Cls), name + ".log");
            _fsError = CreateStream(Path.Combine(Global.LogDir, LOG.Error.Cls), name + ".log");
            _fsInfo = CreateStream(Path.Combine(Global.LogDir, LOG.Info.Cls), name + ".log");
            _fsPref = CreateStream(Path.Combine(Global.LogDir, LOG.Pref.Cls), name + ".log");
        }

        private FileStream CreateStream(string dir, string file)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, file);
            if (File.Exists(path))
                File.Move(path, string.Format("{0}.{1}.bak", path, DateTime.Now.Ticks));
            return new FileStream(path, FileMode.OpenOrCreate);
        }

        public void LogDebug(string message)
        {
            _fsDebug.WriteFlush(_logHelper.BuildLogText(message));
        }

        public void LogError(Exception e)
        {
            _fsError.WriteFlush(_logHelper.BuildLogText(e));
        }

        public void LogError(string message)
        {
            _fsError.WriteFlush(_logHelper.BuildLogText(message));
        }

        public void LogError(string title, Exception e)
        {
            _fsError.WriteFlush(_logHelper.BuildLogText(title, e));
        }

        public void LogInfo(string message)
        {
            _fsInfo.WriteFlush(_logHelper.BuildLogText(message));
        }

        public void LogPerf(string message)
        {
            _fsPref.WriteFlush(_logHelper.BuildLogText(message));
        }

        public string Name { get; private set; }
    }
}
