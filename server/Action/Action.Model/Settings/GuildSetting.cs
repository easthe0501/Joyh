using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class GuildSetting
    {
        public int NameMaxLength { get; set; }
        public int InitMemberMaxCount { get; set; }
        public int PlusMemberMaxCount { get; set; }
        public int ManagerMaxCount { get; set; }
        public int LogMaxCount { get; set; }
        public int CreateCostMoney { get; set; }
        public int CreateNeedLevel { get; set; }
        public int ImpeachNeedGold { get; set; }
        public int ImpeachNeedDay { get; set; }
        public int GuildsAPage { get; set; }
        public int GuildMaxLevel { get; set; }
        public int DailyContributeCount { get; set; }
    }
}
