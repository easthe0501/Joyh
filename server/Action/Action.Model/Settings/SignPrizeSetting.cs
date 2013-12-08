using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    /// <summary>
    /// 签到奖励配置
    /// </summary>
    public class SignPrizeSetting : JsonSetting
    {
        public class SignPrizes
        {
            public int Index { get; set; }
            public Prize Prize { get; set; }
        }
        public SignPrizes[] ContinuePrize { get; set; }

        public SignPrizes[] SumPrize { get; set; }
    }
}
