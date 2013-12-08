using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public interface IBattleCalculator
    {
        void CalcSecProps(Hero hero);
        float CalcHurtData(BattleFighter attacker, BattleFighter defensor, SkillSetting skill, Buff buff);
        float CalcCureData(BattleFighter attacker, Buff buff);
    }
}
