using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;

namespace Action.Log
{
    public class LogHelper
    {
        private const string _splitLine = "---------------------------------------------------";

        private string _name;
        public LogHelper(string name)
        {
            _name = name;
        }

        private string GetNowTimeString()
        {
            return DateTime.Now.ToLocalString();
        }

        public string BuildLogText(string message)
        {
            var sb = new StringBuilder().AppendLine(GetNowTimeString())
                .AppendFormat("[{0}] : {1}", _name, message)
                .AppendLine().AppendLine(_splitLine);
            return sb.ToString();
        }

        public string BuildLogText(Exception ex)
        {
            return BuildLogText("", ex);
        }

        public string BuildLogText(string title, Exception ex)
        {
            var sb = new StringBuilder().AppendLine(GetNowTimeString())
                .AppendFormat("[{0}]{1} : {2}\r\n{3}", _name, title, ex.Message, ex.StackTrace).AppendLine();
            if (ex.InnerException != null)
                sb.AppendFormat("{0}\n{1}\n", ex.InnerException.Message, ex.InnerException.StackTrace);
            sb.AppendLine(_splitLine);
            return sb.ToString();
        }
    }
}
