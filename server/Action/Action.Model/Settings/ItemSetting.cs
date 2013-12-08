using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public enum ItemType
    {
        Other,
        Equip,
        Material,
        UseItem,
        Task,
        GiftBag,
        Part,
        CopyKey,
        CopyTool,
        HeroToken
    }

    /// <summary>
    /// 道具基本配置
    /// </summary>
    public class ItemSetting : JsonSetting
    {
        /// <summary>
        /// 道具类型（装备1，材料2，使用道具3，人物道具4，礼包5）
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// 是否叠加
        /// </summary>
        public bool IsStack { get; set; }

        /// <summary>
        /// 道具价格
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// 需求等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 品质
        /// </summary>
        public int Quality { get; set; }

    }
}
