using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    /// <summary>
    /// 英雄设置
    /// </summary>
    public class HeroSetting : JsonSetting
    {
        /// <summary>
        /// 性别（0女，1男）
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 是否主角
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// 品质
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// 体质
        /// </summary>
        public int Life { get; set; }

        /// <summary>
        /// 力量
        /// </summary>
        public int Force { get; set; }

        /// <summary>
        /// 绝技
        /// </summary>
        public int Skill { get; set; }

        /// <summary>
        /// 魅力
        /// </summary>
        public int Charm { get; set; }

        /// <summary>
        /// 职业Id
        /// </summary>
        public int JobId { get; set; }

        /// <summary>
        /// 绝技Id
        /// </summary>
        public int SkillId { get; set; }

        /// <summary>
        /// 招募所需声望
        /// </summary>
        public int Repute { get; set; }

        /// <summary>
        /// 招募花费铜钱
        /// </summary>
        public int Money { get; set; }

        public int TS
        {
            get { return Life; }
        }

        public int WY
        {
            get { return Force; }
        }

        public int ZM
        {
            get { return Skill; }
        }

        public int ZW
        {
            get { return Charm; }
        }
    }
}
