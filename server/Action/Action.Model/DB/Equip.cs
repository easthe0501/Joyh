using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Action.Model
{
    public class Equip : Item
    {
        protected override void OnInit(IEntityRoot root)
        {
            Plugins = new List<EquipPlugin>();
            Level = 1;
        }

        [BsonIgnore]
        /// <summary>
        /// 道具的静态配置信息
        /// </summary>
        public ItemSetting ItemSetting
        {
            get { return base.Setting; }
        }

        [BsonIgnore]
        /// <summary>
        /// 装备拥有者
        /// </summary>
        public Hero Owner { get; set; }

        [BsonIgnore]
        /// <summary>
        /// 装备的静态配置信息
        /// </summary>
        public new EquipSetting Setting
        {
            get { return APF.Settings.Equips.Find(SettingId); }
        }

        /// <summary>
        /// 强化到现在等级的总花费
        /// </summary>
        /// <returns></returns>
        public int StrengthenCostSum()
        {
            int costSum = 0;
            for (int i = 1; i < Level; i++)
            {
                costSum += APF.Settings.EquipStrenthens.Find(i)
                    .QualityCosts[ItemSetting.Quality - 1];
            }
            return costSum;
        }

        /// <summary>
        /// 装备等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 合成祝福值
        /// </summary>
        public int CompoundLucky { get; set; }

        /// <summary>
        /// 装备插件（洗练）
        /// </summary>
        public List<EquipPlugin> Plugins { get; private set; }

        private Buff _buff = new Buff();
        [BsonIgnore]
        /// <summary>
        /// 装备Buff（受等级影响）
        /// </summary>
        public Buff Buff
        {
            get { return _buff; }
        }

        /// <summary>
        /// 刷新装备属性
        /// </summary>
        public void Refresh()
        {
            _buff.Effect = Setting.Buff.Effect;
            _buff.Data = Setting.Buff.Data * (10 + Level);
        }

        public EquipArgs ToEquipArgs(string player)
        {
            var args = new EquipArgs();
            args.Obj = new ObjArgs() { Player = player, Id = this.Id };
            args.SettingId = SettingId;
            args.Level = Level;

            //基础Buff
            args.BasicBuff = new Buff1Args()
            {
                Effect = (int)Buff.Effect,
                Data = Buff.Data
            };

            //洗练Buff
            foreach (var plugin in Plugins)
            {
                args.PluginBuffs.Add(new Buff1Args()
                {
                    Effect = (int)plugin.Buff.Effect,
                    Data = plugin.Buff.Data
                });
            }

            return args;
        }

        public Equip2Args ToEquip2Args(string player)
        {
            var args = new Equip2Args();
            args.Obj = new ObjArgs() { Player = player, Id = this.Id };
            args.SettingId = SettingId;
            args.Level = Level;
            args.Pos = (int)Setting.Pos;
            return args;
        }

        protected override void OnLoad(IEntityRoot root)
        {
            Refresh();
        }

        public int GetEquipStrenthenCost()
        {
            return APF.Settings.EquipStrenthens.Find(Level)
                .QualityCosts[ItemSetting.Quality - 1];
        }

        [BsonIgnore]
        /// <summary>
        /// 装备合成的配置信息
        /// </summary>
        public EquipCompoundSetting EquipCompoundSetting
        {
            get { return APF.Settings.EquipCompounds.Find(SettingId); }
        }
    }

    /// <summary>
    /// 装备插件（洗练）
    /// </summary>
    public class EquipPlugin : Entity
    {
        protected override void OnInit(IEntityRoot root)
        {
            Buff = new Buff();
        }

        public bool Locked { get; set; }
        public Buff Buff { get; private set; }
    }
}
