using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using SuperSocket.SocketBase.Command;
using Action.Core;
using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;
using System.IO;
using Action.Utility;

namespace Action.Model
{
    public abstract class SysLog
    {
        public BsonDateTime CreateTime { get; set; }
        public string Target { get; set; }
        public string Content { get; set; }
    }

    public class LOG
    {
        public class Debug : SysLog
        {
            public const string Cls = "Debug";
        }

        public class Error : SysLog
        {
            public const string Cls = "Error";
        }

        public class Info : SysLog
        {
            public const string Cls = "Info";
        }

        public class Pref : SysLog
        {
            public const string Cls = "Pref";
        }

        public class Command
        {
            public const string Cls = "Command";
            public string Player { get; set; }
            public List<CommandItem> Items { get; set; }

            public void Import(List<LoggedCommandInfo> infoList)
            {
                Items = new List<CommandItem>();
                foreach (var info in infoList)
                {
                    var item = new CommandItem()
                    {
                        Time = info.Time,
                        Key = info.Key,
                        Type = info.Type != null ? info.Type.ToString() : "",
                        Data = info.Data
                    };
                    Items.Add(item);
                }
            }
        }

        [DisplayName("Command")]
        public class CommandItem
        {
            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            public DateTime Time { get; set; }

            public int Key { get; set; }
            public string Type { get; set; }
            public byte[] Data { get; set; }

            public override string ToString()
            {
                return string.Format("[{0}] {1}", Time.ToString("yyyy-MM-dd HH:mm:ss.fff"), ((CommandEnum)Key).ToString());
            }
        }
    }
}
