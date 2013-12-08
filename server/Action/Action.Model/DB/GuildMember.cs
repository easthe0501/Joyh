using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public enum GuildPost
    {
        Common = 0,
        Deputy,
        Leader
    }

    public class GuildMember
    {
        public static GuildMember Create(Player member)
        {
            return new GuildMember()
            {
                Name = member.Name,
            };
        }

        /// <summary>
        /// 成员名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 总贡献
        /// </summary>
        public int TotalContribution { get; set; }

        /// <summary>
        /// 今日贡献
        /// </summary>
        public int TodayContribution { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public GuildPost Post { get; set; }
    }
}
