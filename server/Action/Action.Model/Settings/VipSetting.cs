using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class VipSetting : JsonSetting
    {
        public VipSetting()
        {
            DailyCount = new DailyCountHistory();
        }

        public int Gold { get; set; }
        public int[] CommandIds { get; set; }
        public Prize OncePrize { get; set; }
        public Prize DailyPrize { get; set; }
        public DailyCountHistory DailyCount { get; set; }
        public int BagSize { get; set; }
    }
}
