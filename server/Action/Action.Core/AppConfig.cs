using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;

namespace Action.Core
{
    public class AppConfig
    {
        public AppConfig()
        {
            CommandLoggerType = MyConvert.ToInt32(ConfigurationManager.AppSettings["CommandLoggerType"], 1);
        }

        public int CommandLoggerType { get; set; }

        private string _battleCalculator = ConfigurationManager.AppSettings["BattleCalculator"];
        public string BattleCalculator
        {
            get { return _battleCalculator; }
        }

        private int _consoleLines = MyConvert.ToInt32(ConfigurationManager.AppSettings["ConsoleLines"], 5000);
        public int ConsoleLines
        {
            get { return _consoleLines; }
        }

        private int _zoneId = MyConvert.ToInt32(ConfigurationManager.AppSettings["ZoneId"]);
        public int ZoneId
        {
            get { return _zoneId; }
        }

        private string _serverIp = ConfigurationManager.AppSettings["ServerIp"];
        public string ServerIp
        {
            get { return _serverIp; }
        }

        private DateTime _publishDate = MyConvert.ToDateTime(ConfigurationManager.AppSettings["PublishDate"], Global.DefaultDateTime);
        public DateTime PublishDate
        {
            get { return _publishDate; }
        }

        private string _backServerToken = ConfigurationManager.AppSettings["BackServerToken"];
        public string BackServerToken
        {
            get { return _backServerToken; }
        }
    }
}
