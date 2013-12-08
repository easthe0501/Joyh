using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Core
{
    public class LoggedCommandInfo
    {
        public DateTime Time { get; set; }
        public int Key { get; set; }
        public Type Type { get; set; }
        public byte[] Data { get; set; }

        public LoggedCommandInfo(int key, Type type, byte[] data)
        {
            Time = DateTime.Now;
            Key = key;
            Type = type;
            Data = data;
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", Time.ToString("yyyy-MM-dd HH:mm:ss"), Key);
        }
    }
}
