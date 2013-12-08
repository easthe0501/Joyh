using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class GuildContributionSetting : JsonSetting
    {
        public int Vip { get; set; }
        public int CostMoney { get; set; }
        public int CostGold { get; set; }
        public int IncomeContribution { get; set; }
        public int IncomeRepute { get; set; }
    }
}
