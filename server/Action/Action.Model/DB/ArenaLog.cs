using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class ArenaLog : Entity
    {
        public string ReportGuid { get; set; }
        public int CreateTime { get; set; }
        public string ArenaPlayer { get; set; }
        public string TargetPlayer { get; set; }
        public bool WinOrLose { get; set; }
    }
}
