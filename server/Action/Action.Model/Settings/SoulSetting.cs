using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public enum SoulType
    {
        HP = 1,
        XP,
        CommonAttack,
        CommonDefence,
        SkillAttack,
        SkillDefence,
        Hit,
        Dodge,
        Crack,
        Block,
        Crit,
        Tenacity
    }

    public class SoulSetting : JsonSetting
    {
        public SoulSetting()
        {
            Buff = new Buff();
        }

        /// <summary>
        /// 战魂品质
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// 战魂类型
        /// </summary>
        public SoulType Type { get; set; }

        /// <summary>
        /// 战魂所带Buff
        /// </summary>
        public Buff Buff { get; set; }

        /// <summary>
        /// 战魂初始经验（魂力）
        /// </summary>
        public int InitExp { get; set; }

        /// <summary>
        /// 战魂价格
        /// </summary>
        public int Price { get; set; }

    }
}
