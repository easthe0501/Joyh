using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class CommandSetting : JsonSetting
    {
        public int CD { get; set; }
        public bool CheckRight { get; set; }
    }
}
