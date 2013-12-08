using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class LevelSetting : JsonSetting
    {
        public int Exp { get; set; }
        public int EmbattlePos { get; set; }
        public int SoulPos { get; set; }
        public int[] CommandIds { get; set; }
        public Prize Prize { get; set; }
    }
}
