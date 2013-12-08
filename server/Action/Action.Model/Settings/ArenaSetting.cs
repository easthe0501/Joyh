using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class ArenaSetting
    {
        public int UnlockLevel { get; set; }
        public Prize WinnerPrize { get; set; }
        public Prize LoserPrize { get; set; }

        public class RankPrize
        {
            public int LowestRank { get; set; }
            public Prize Prize { get; set; }
        }
        public RankPrize[] WeekRankPrizes { get; set; }
        public int RefreshCost { get; set; }
    }
}
