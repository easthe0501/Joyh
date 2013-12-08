using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Action.Model
{
    [BsonKnownTypes(typeof(Equip))]
    public class Item : EntityEx, IComparable<Item>
    {
        /// <summary>
        /// 排序Id
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// 道具数量
        /// </summary>
        public int Count { get; set; }

        [BsonIgnore]
        /// <summary>
        /// 道具的静态配置信息
        /// </summary>
        public ItemSetting Setting
        {
            get { return APF.Settings.Items.Find(SettingId); }
        }

        public int CompareTo(Item other)
        {
            if (this.Setting.Type < other.Setting.Type)
                return -1;
            if (this.Setting.Type == other.Setting.Type)
            {
                if (this.SettingId < other.SettingId)
                    return -1;
                if (this.SettingId == other.SettingId)
                {
                    if (this.Count > other.Count)
                        return -1;
                    if (this.Count == other.Count)
                        return 0;
                }
            }

            return 1;            
        }
    }
}
