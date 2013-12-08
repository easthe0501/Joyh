using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    /// <summary>
    /// Buff类型
    /// </summary>
    public enum BuffEffect
    {
        Null = 0,
        HeroHpPlus = 1,
        HeroXpPlus,
        HeroFirstStrikePlus = 20,
        HeroCommonAttackPlus,
        HeroCommonDefencePlus,
        HeroSkillAttackPlus,
        HeroSkillDefencePlus,
        HeroMindAttackPlus,
        HeroMindDefencePlus,
        HeroHitPlus = 40,
        HeroDodgePlus,
        HeroCrackPlus,
        HeroBlockPlus,
        HeroCritPlus,
        HeroTenacityPlus,
        BattleHurt = 100,
        BattleCure,
        BattleXpPlus,
        BattleXpMinus,
        BattleXpFull,
        BattleXpClear,
        xxx = 120,        //暂不加入Battle攻防升降
        BattleHitPlus = 140,
        BattleHitMinus,
        BattleDodgePlus,
        BattleDodgeMinus,
        BattleCrackPlus,
        BattleCrackMinus,
        BattleBlockPlus,
        BattleBlockMinus,
        BattleCritPlus,
        BattleCritMinus,
        BattleTenacityPlus,
        BattleTenacityMinus,
        BattleDoubleHit = 160,
        BattleVertigo = 180,
        BattleSleep,
        BattlePoisoning,
        BattleLock
    }

    /// <summary>
    /// Buff设置
    /// </summary>
    public class Buff
    {
        /// <summary>
        /// Buff效果
        /// </summary>
        public BuffEffect Effect { get; set; }

        /// <summary>
        /// Buff附加数据
        /// </summary>
        public int Data { get; set; }

        /// <summary>
        /// Buff持续回合数
        /// </summary>
        public int Round { get; set; }

        /// <summary>
        /// 创建运行时Buff
        /// </summary>
        /// <returns></returns>
        public BuffState ToBuffState()
        {
            var state = new BuffState() { Buff = this, Round = this.Round };
            //非状态类Buff，回合数要先减去1
            if (Effect < BuffEffect.BattleVertigo)
                state.Round--;
            return state;
        }
    }

    public class BuffState
    {
        /// <summary>
        /// Buff引用
        /// </summary>
        public Buff Buff { get; set; }

        /// <summary>
        /// Buff持续回合数
        /// </summary>
        public int Round { get; set; }
    }
}
