using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using Action.Core;

namespace Action.Model
{
    public class Hero : EntityEx, IFighter
    {
        protected override void OnInit(IEntityRoot root)
        {
            Equips = new List<Equip>();
            Souls = new List<Soul>();
            Level = APF.Settings.Role.LevelMin;
        }

        protected override void OnLoad(IEntityRoot root)
        {
            Refresh();
        }

        [BsonIgnore]
        public HeroSetting Setting
        {
            get { return APF.Settings.Heros.Find(SettingId); }
        }

        public int GetNextLevelExp()
        {
            return APF.Settings.Levels.Find(Level).Exp;
        }

        /// <summary>
        /// 阵法中位置（1-9）
        /// </summary>
        public int Pos { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 经验
        /// </summary>
        public int Exp { get; set; }

        /// <summary>
        /// 获取经验
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="mainHero"></param>
        public bool GetExp(int exp, Hero mainHero=null)
        {
            int befLevel = Level;
            exp += Exp;
            if (mainHero == null)
                mainHero = this;
            var maxLevel = mainHero == this ? APF.Settings.Role.LevelMax : mainHero.Level;
            while (Level < maxLevel)
            {
                var upExp = GetNextLevelExp();
                if (exp < upExp)
                {
                    Exp = exp;
                    break;
                }
                Level++;
                exp -= upExp;
            }
            if (Level == APF.Settings.Role.LevelMax)
                Exp = 0;
            if (Level == mainHero.Level)
                Exp = Math.Min(exp, mainHero.Exp);
            if (Level > befLevel)
            {
                Refresh();
                return true;
            }
            else
                return false;
        }

        [BsonIgnore]
        public int HP { get; set; }
        [BsonIgnore]
        public int XP { get; set; }
        [BsonIgnore]
        public int CommonAttack { get; set; }
        [BsonIgnore]
        public int CommonDefence { get; set; }
        [BsonIgnore]
        public int SkillAttack { get; set; }
        [BsonIgnore]
        public int SkillDefence { get; set; }
        [BsonIgnore]
        public int MindAttack { get; set; }
        [BsonIgnore]
        public int MindDefence { get; set; }
        [BsonIgnore]
        public int FirstStrike { get; set; }
        [BsonIgnore]
        public int Hit { get; set; }
        [BsonIgnore]
        public int Dodge { get; set; }
        [BsonIgnore]
        public int Crack { get; set; }
        [BsonIgnore]
        public int Block { get; set; }
        [BsonIgnore]
        public int Crit { get; set; }
        [BsonIgnore]
        public int Tenacity { get; set; }

        /// <summary>
        /// 穿戴的装备列表
        /// </summary>
        public List<Equip> Equips { get; private set; }

        /// <summary>
        /// 佩戴的战魂列表
        /// </summary>
        public List<Soul> Souls { get; private set; }

        /// <summary>
        /// 可佩带的战魂数量
        /// </summary>
        public int SoulSpace { get; set; }

        /// <summary>
        /// 根据等级、装备、战魂等重算属性
        /// </summary>
        public void Refresh()
        {
            //个人属性
            APF.BattleCalculator.CalcSecProps(this);

            //装备
            foreach (var equip in Equips)
            {
                equip.Owner = this;
                equip.Refresh();
                CalcByBuff(equip.Buff);
                foreach (var plugin in equip.Plugins)
                    CalcByBuff(plugin.Buff);
            }

            //战魂
            foreach (var soul in Souls)
            {
                soul.Refresh();
                CalcByBuff(soul.Buff);
            }
        }

        private void CalcByBuff(Buff buff)
        {
            switch (buff.Effect)
            {
                case BuffEffect.HeroHpPlus:
                    HP += buff.Data;
                    break;
                case BuffEffect.HeroXpPlus:
                    XP += buff.Data;
                    break;
                case BuffEffect.HeroCommonAttackPlus:
                    CommonAttack += buff.Data;
                    break;
                case BuffEffect.HeroCommonDefencePlus:
                    CommonDefence += buff.Data;
                    break;
                case BuffEffect.HeroSkillAttackPlus:
                    SkillAttack += buff.Data;
                    break;
                case BuffEffect.HeroSkillDefencePlus:
                    SkillDefence += buff.Data;
                    break;
                case BuffEffect.HeroFirstStrikePlus:
                    FirstStrike += buff.Data;
                    break;

                case BuffEffect.HeroHitPlus:
                    Hit += buff.Data;
                    break;
                case BuffEffect.HeroDodgePlus:
                    Dodge += buff.Data;
                    break;
                case BuffEffect.HeroCrackPlus:
                    Crack += buff.Data;
                    break;
                case BuffEffect.HeroBlockPlus:
                    Block += buff.Data;
                    break;
                case BuffEffect.HeroCritPlus:
                    Crit += buff.Data;
                    break;
                case BuffEffect.HeroTenacityPlus:
                    Tenacity += buff.Data;
                    break;
            }
        }

        public Hero1Args ToHero1Args(string player)
        {
            var msg = new Hero1Args();
            msg.Obj = new ObjArgs() { Player = player, Id = Id };
            msg.SettingId = SettingId;
            msg.Level = Level;
            msg.Pos = Pos;
            return msg;
        }

        public Hero3Args ToHero3Args(string player)
        {
            var msg = new Hero3Args();
            msg.Obj = new ObjArgs() { Player = player, Id = Id };
            msg.Exp = Exp;
            msg.SettingId = SettingId;
            msg.Level = Level;
            msg.HP = HP;
            msg.CommonAttack = CommonAttack;
            msg.CommonDefence = CommonDefence;
            msg.SkillAttack = SkillAttack;
            msg.SkillDefence = SkillDefence;
            msg.FirstStrike = FirstStrike;
            msg.Hit = Hit;
            msg.Dodge = Dodge;
            msg.Crack = Crack;
            msg.Block = Block;
            msg.Crit = Crit;
            msg.Tenacity = Tenacity;
            msg.Equips.AddRange(Equips.Select(e => e.ToEquip2Args(player)));
            return msg;
        }

        public int SkillId
        {
            get { return Setting.SkillId; }
        }

        public int[] ImmuneBuffs
        {
            get { return null; }
        }
    }
}
