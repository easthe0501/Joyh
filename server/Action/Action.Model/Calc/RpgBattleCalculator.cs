using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class RpgBattleCalculator : IBattleCalculator
    {
        public void CalcSecProps(Hero hero)
        {
            var heroSetting = hero.Setting;
            var rate = APF.Common.GetLevelRate(hero.Level);
            hero.HP = (hero.Setting.Quality + 5) * (hero.Level + 10) * 5;
            hero.XP = 50;
            hero.CommonAttack = (int)(heroSetting.Force * rate);
            hero.CommonDefence = (int)(heroSetting.Life * rate * 0.6);
            hero.SkillAttack = (int)(heroSetting.Skill * rate);
            hero.SkillDefence = (int)(heroSetting.Life * rate * 0.4);
            hero.FirstStrike = (int)(heroSetting.Charm * rate);

            //职业属性
            var jobSetting = APF.Settings.Jobs.Find(heroSetting.JobId);
            hero.Hit = jobSetting.Hit * heroSetting.Quality;
            hero.Dodge = jobSetting.Dodge * heroSetting.Quality;
            hero.Crack = jobSetting.Crack * heroSetting.Quality;
            hero.Block = jobSetting.Block * heroSetting.Quality;
            hero.Crit = jobSetting.Crit * heroSetting.Quality;
            hero.Tenacity = jobSetting.Tenacity * heroSetting.Quality;
        }

        public float CalcHurtData(BattleFighter attacker, BattleFighter defensor, SkillSetting skill, Buff buff)
        {
            return (attacker.SkillAttack - defensor.SkillDefence) * (buff.Data / 100f) * attacker.XpHurtRatio;
        }

        public float CalcCureData(BattleFighter attacker, Buff buff)
        {
            return attacker.SkillAttack * (buff.Data / 100f) * attacker.XpHurtRatio;
        }
    }
}
