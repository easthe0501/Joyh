using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    /// <summary>
    /// Buff作用范围
    /// </summary>
    public enum BuffRange
    {
        Self = 0,
        EnemyFront = 100,
        EnemyBack,
        EnemyHorizontal,
        EnemyVertical,
        EnemyT,
        EnemyAll,
        EnemyRandom1,
        EnemyRandom2,
        EnemyRandom3,
        EnemyMinHp1,
        EnemyMinHp2,
        EnemyMaxHp1,
        EnemyMaxHp2,
        EnemyMinXp1,
        EnemyMinXp2,
        EnemyMaxXp1,
        EnemyMaxXp2,
        EnemyVertigo,
        EnemySleep,
        EnemyPoisoning,
        EnemyLock,
        FriendFront = 200,
        FriendBack,
        FriendHorizontal,
        FriendVertical,
        FriendT,
        FriendAll,
        FriendRandom1,
        FriendRandom2,
        FriendRandom3,
        FriendMinHp1,
        FriendMinHp2,
        FriendMaxHp1,
        FriendMaxHp2,
        FriendMinXp1,
        FriendMinXp2,
        FriendMaxXp1,
        FriendMaxXp2,
        FriendVertigo,
        FriendSleep,
        FriendPoisoning,
        FriendLock
    }

    public class BuffGroup
    {
        static BuffGroup()
        {
            Defaults = new BuffGroup[]
            {
                new BuffGroup()
                {
                    Range = BuffRange.EnemyFront,
                    Buffs = new Buff[]
                    {
                        new Buff(){ Effect = BuffEffect.BattleHurt}
                    }
                }
            };
        }

        public static BuffGroup[] Defaults { get; private set; }
        public BuffRange Range { get; set; }
        public Buff[] Buffs { get; set; }
    }
}
