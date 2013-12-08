using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;

namespace Action.Battle
{
    public static class BattleHelper
    {
        private static BattleFighter[] _emptyTargets = new BattleFighter[0];

        public static void RefreshArenaTargets(this PlayerSummary self)
        {
            var battlePlayers = self.World.BattleArena.PlayerList.Where(b => self.Name != b).ToArray();
            var curRank = self.ArenaRank;
            var minRank = Math.Max(1, self.ArenaRank - 20);
            var maxRank = Math.Min(battlePlayers.Length, curRank + 50);

            var targetList = new List<string>();
            targetList.AddRange(battlePlayers.Skip(minRank - 1).Take(curRank - minRank).Randoms(4));
            targetList.AddRange(battlePlayers.Skip(curRank - 1).Take(maxRank - curRank).Randoms(6 - targetList.Count));
            self.ArenaTargets = targetList.ToArray();
        }

        public static BattleFighter[] GetTargets(this BattleFighter self, BuffRange range)
        {
            if (range == BuffRange.Self)
                return new BattleFighter[] { self };
            else
                return GetTargets(self, range < BuffRange.FriendFront
                    ? self.EnemyGroup : self.FriendGroup, range);
        }

        private static BattleFighter[] GetTargets(BattleFighter self, BattleGroup group, BuffRange range)
        {
            switch (range)
            {
                //固定单体定位
                case BuffRange.EnemyFront:
                case BuffRange.FriendFront:
                    var target = GetSingleTarget(self, group, false);
                    return target != null ? new BattleFighter[] { target } : _emptyTargets;
                case BuffRange.EnemyBack:
                case BuffRange.FriendBack:
                    target = GetSingleTarget(self, group, true);
                    return target != null ? new BattleFighter[] { target } : _emptyTargets;

                //固定范围定位
                case BuffRange.EnemyHorizontal:
                case BuffRange.FriendHorizontal:
                case BuffRange.EnemyVertical:
                case BuffRange.FriendVertical:
                case BuffRange.EnemyT:
                case BuffRange.FriendT:
                    return GetRangeTargets(self, group, range).ToArray();
                case BuffRange.EnemyAll:
                case BuffRange.FriendAll:
                    return group.GetAliveFighters().ToArray();

                //随机定位
                case BuffRange.EnemyRandom1:
                case BuffRange.FriendRandom1:
                    return group.GetAliveFighters().Randoms(1).ToArray();
                case BuffRange.EnemyRandom2:
                case BuffRange.FriendRandom2:
                    return group.GetAliveFighters().Randoms(2).ToArray();
                case BuffRange.EnemyRandom3:
                case BuffRange.FriendRandom3:
                    return group.GetAliveFighters().Randoms(3).ToArray();

                //根据血量定位
                case BuffRange.EnemyMinHp1:
                case BuffRange.FriendMinHp1:
                    return group.GetAliveFighters().OrderBy(f => f.HP).Take(1).ToArray();
                case BuffRange.EnemyMinHp2:
                case BuffRange.FriendMinHp2:
                    return group.GetAliveFighters().OrderBy(f => f.HP).Take(2).ToArray();
                case BuffRange.EnemyMaxHp1:
                case BuffRange.FriendMaxHp1:
                    return group.GetAliveFighters().OrderByDescending(f => f.HP).Take(1).ToArray();
                case BuffRange.EnemyMaxHp2:
                case BuffRange.FriendMaxHp2:
                    return group.GetAliveFighters().OrderByDescending(f => f.HP).Take(2).ToArray();

                //根据气势定位
                case BuffRange.EnemyMinXp1:
                case BuffRange.FriendMinXp1:
                    return group.GetAliveFighters().OrderBy(f => f.XP).Take(1).ToArray();
                case BuffRange.EnemyMinXp2:
                case BuffRange.FriendMinXp2:
                    return group.GetAliveFighters().OrderBy(f => f.XP).Take(2).ToArray();
                case BuffRange.EnemyMaxXp1:
                case BuffRange.FriendMaxXp1:
                    return group.GetAliveFighters().OrderByDescending(f => f.XP).Take(1).ToArray();
                case BuffRange.EnemyMaxXp2:
                case BuffRange.FriendMaxXp2:
                    return group.GetAliveFighters().OrderByDescending(f => f.XP).Take(2).ToArray();

                //根据状态定位
                case BuffRange.EnemyVertigo:
                case BuffRange.FriendVertigo:
                    return GetStatusTargets(self, group, FighterStatus.Vertigo);
                case BuffRange.EnemySleep:
                case BuffRange.FriendSleep:
                    return GetStatusTargets(self, group, FighterStatus.Sleep);
                case BuffRange.EnemyPoisoning:
                case BuffRange.FriendPoisoning:
                    return GetStatusTargets(self, group, FighterStatus.Poisoning);
                case BuffRange.EnemyLock:
                case BuffRange.FriendLock:
                    return GetStatusTargets(self, group, FighterStatus.Lock);

                default:
                    goto case BuffRange.EnemyFront;
            }
        }

        private static BattleFighter[] GetStatusTargets(BattleFighter self, BattleGroup group, FighterStatus status)
        {
            var targets = group.GetAliveFighters().Where(f => f.Status == status).ToArray();
            if (targets.Length == 0)
                targets = new BattleFighter[] { GetSingleTarget(self, group, false) };
            return targets;
        }

        private static IEnumerable<BattleFighter> GetRangeTargets(BattleFighter self, BattleGroup group, BuffRange range)
        {
            var target = GetSingleTarget(self, group, false);
            if (target == null)
                yield break;
            if (range == BuffRange.EnemyHorizontal || range == BuffRange.FriendHorizontal
                || range == BuffRange.EnemyT || range == BuffRange.FriendT)
            {
                var poses = new int[] { target.Pos - 2, target.Pos - 1, target.Pos + 1, target.Pos + 2 };
                foreach (var other in target.FriendGroup.GetAliveFighters())
                {
                    if ((target.Pos - 1) / 3 == (other.Pos - 1) / 3)
                        yield return other;
                }
            }
            if (range == BuffRange.EnemyVertical || range == BuffRange.FriendVertical
                || range == BuffRange.EnemyT || range == BuffRange.FriendT)
            {
                foreach (var other in target.FriendGroup.GetAliveFighters())
                {
                    if (target.Pos % 3 == other.Pos % 3)
                        yield return other;
                }
            }
        }

        private static BattleFighter GetSingleTarget(BattleFighter self, BattleGroup group, bool backFirst)
        {
            foreach (var pos in GetOrderedPoses(self.Pos, backFirst))
            {
                var target = group.GetAliveFighter(pos);
                if (target != null)
                    return target;
            }
            return null;
        }

        public static int[] GetOrderedPoses(int selfPos, bool backFirst)
        {
            switch (selfPos % 3)
            {
                case 1:
                    return backFirst ? new int[] { 7, 4, 1, 8, 5, 2, 9, 6, 3 }
                        : new int[] { 1, 4, 7, 2, 5, 8, 3, 6, 9 };
                case 2:
                    return backFirst ? new int[] { 8, 5, 2, 7, 4, 1, 9, 6, 3 }
                        : new int[] { 2, 5, 8, 1, 4, 7, 3, 6, 9 };
                case 0:
                    return backFirst ? new int[] { 9, 6, 3, 8, 5, 2, 7, 4, 1 }
                        : new int[] { 3, 6, 9, 2, 5, 8, 1, 4, 7 };
                default:
                    goto case 0;
            }
        }
    }
}
