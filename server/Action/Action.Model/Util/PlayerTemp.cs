using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class PlayerTemp
    {
        public Prize CopyGridPrize { get; set; }
        public Prize CopyPassPrize { get; set; }
        public Prize BattleArenaPrize { get; set; }
        public BattleReport BattleReport { get; set; }
        public MeetingOption[] MeetingOptions { get; set; }
        public CardProcess CardProcess { get; set; }
    }
}
