using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class SignIn : Entity
    {
        protected override void OnInit(IEntityRoot root)
        {
            SignInDay = new List<int>();
            ContinuePrize = new Dictionary<int, bool>();
            SumPrize = new Dictionary<int, bool>();
        }

        /// <summary>
        /// 签到日期
        /// </summary>
        public List<int> SignInDay { get; set; }

        /// <summary>
        /// 连续签到次数
        /// </summary>
        public int ConDays { get; set; }

        /// <summary>
        /// 累计签到次数
        /// </summary>
        public int SumDays { get; set; }

        /// <summary>
        /// 漏签次数
        /// </summary>
        public int MissTimes { get; set; }

        /// <summary>
        /// 领取连续签到奖励
        /// </summary>
        public Dictionary<int, bool> ContinuePrize { get; set; }

        /// <summary>
        /// 领取累计签到奖励
        /// </summary>
        public Dictionary<int, bool> SumPrize { get; set; }
    }
}
