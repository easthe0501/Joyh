using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class SlgBattleCalculator : IBattleCalculator
    {
        public void CalcSecProps(Hero hero)
        {
            var heroSetting = hero.Setting;
            var rate = APF.Common.GetLevelRate(hero.Level);
            hero.HP = (int)((200 + heroSetting.TS + heroSetting.WY) * rate);
            hero.XP = 50;
            hero.CommonAttack = (int)(heroSetting.TS * rate);
            hero.CommonDefence = (int)(heroSetting.TS * rate * 0.7);
            hero.SkillAttack = (int)(heroSetting.WY * rate * 1.5);
            hero.SkillDefence = (int)(heroSetting.WY * rate);
            hero.MindAttack = (int)(heroSetting.ZM * rate * 1.2);
            hero.MindDefence = (int)(heroSetting.ZM * rate * 0.8);
            hero.FirstStrike = (int)(heroSetting.ZW * rate);

            //职业属性
            var jobSetting = APF.Settings.Jobs.Find(hero.Setting.JobId);
            hero.Hit = jobSetting.Hit;
            hero.Dodge = jobSetting.Dodge;
            hero.Crack = jobSetting.Crack;
            hero.Block = jobSetting.Block;
            hero.Crit = jobSetting.Crit;
            hero.Tenacity = jobSetting.Tenacity;
        }

        public float CalcHurtData(BattleFighter attacker, BattleFighter defensor, SkillSetting skill, Buff buff)
        {
            var distance = skill.IsMind ? attacker.MindAttack - defensor.MindDefence
                : attacker.SkillAttack - defensor.SkillDefence;
            return distance * (buff.Data / 100f) * attacker.XpHurtRatio;
        }

        public float CalcCureData(BattleFighter attacker, Buff buff)
        {
            return attacker.MindAttack * (buff.Data / 100f) * attacker.XpHurtRatio;
        }
    }
}
