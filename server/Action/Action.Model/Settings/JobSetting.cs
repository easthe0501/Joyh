using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    /// <summary>
    /// 职业设置
    /// </summary>
    public class JobSetting : JsonSetting
    {
        /// <summary>
        /// 命中
        /// </summary>
        public int Hit { get; set; }

        /// <summary>
        /// 闪避
        /// </summary>
        public int Dodge { get; set; }

        /// <summary>
        /// 破击
        /// </summary>
        public int Crack { get; set; }

        /// <summary>
        /// 格挡
        /// </summary>
        public int Block { get; set; }

        /// <summary>
        /// 暴击
        /// </summary>
        public int Crit { get; set; }

        /// <summary>
        /// 韧性
        /// </summary>
        public int Tenacity { get; set; }
    }
}
