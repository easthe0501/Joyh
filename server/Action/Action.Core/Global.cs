using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Action.Core
{
    public static class Global
    {
        public const int Version = 0;

        static Global()
        {
            _hostDir = AppDomain.CurrentDomain.BaseDirectory;
            _logDir = Path.Combine(_hostDir, "Logs");
            _resDir = Path.Combine(_hostDir, "../Res");
        }

        private static string _hostDir;
        public static string HostDir
        {
            get { return _hostDir; }
        }

        private static string _logDir;
        public static string LogDir
        {
            get { return _logDir; }
        }

        private static string _resDir;
        public static string ResDir
        {
            get { return _resDir; }
        }

        private static AppConfig _config = new AppConfig();
        public static AppConfig Config
        {
            get { return _config; }
        }

        private static IOutput _output = new ConsoleOutput();
        public static IOutput Output
        {
            get { return _output; }
            set { _output = value; }
        }

        public static readonly DateTime DefaultDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
    }
}
