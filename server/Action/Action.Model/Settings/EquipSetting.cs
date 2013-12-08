using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public enum EquipPos
    {
        /// <summary>
        /// 护符
        /// </summary>
        Amulet = 1,

        /// <summary>
        /// 法冠
        /// </summary>
        Hat,

        /// <summary>
        /// 武器
        /// </summary>
        Weapon,

        /// <summary>
        /// 法袍
        /// </summary>
        Clothes,

        /// <summary>
        /// 战魂
        /// </summary>
        Gem,
        
        /// <summary>
        /// 战靴
        /// </summary>
        Shoe
    }

    public class EquipSetting : JsonSetting
    {
        public EquipSetting()
        {
            Buff = new Buff();
        }

        /// <summary>
        /// 适合的职业
        /// </summary>
        //public int[] SuitedJobs { get; set; }

        /// <summary>
        /// 装备部位
        /// </summary>
        public EquipPos Pos { get; set; }

        /// <summary>
        /// 装备所带Buff
        /// </summary>
        public Buff Buff { get; set; }
    }
}
