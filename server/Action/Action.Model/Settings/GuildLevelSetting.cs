using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    /// <summary>
    /// 帮派成长配置
    /// </summary>
    public class GuildLevelSetting : JsonSetting
    {
        public int Exp { get; set; }
        public int[] CommandIds { get; set; }
        public int[] CopyIds { get; set; }
    }
}
