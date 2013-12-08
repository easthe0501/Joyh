using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Core;
using Action.Engine;
using Action.Model;
using System.Diagnostics;
using Action.Utility;

namespace Action.Battle
{
    [Export(typeof(IGameModule))]
    public class BattleModule : GameModule, IBattleModule
    {
        public override void Load(GameWorld world)
        {
            var skills = APF.Settings.Skills;
            foreach (var battle in APF.Settings.Battles.All)
            {
                var strBattleId = battle.Id.ToString();
                Trace.Assert(battle.Layouts != null && battle.Layouts.Length > 0, strBattleId);
                foreach (var layout in battle.Layouts)
                {
                    Trace.Assert(NumberHelper.Between(layout.Pos, 1, 9), strBattleId);
                    Trace.Assert(skills.Find(layout.Monster.SkillId) != null, strBattleId + "-" + layout.Monster.SkillId.ToString());
                }
                Trace.Assert(battle.WinnerPrize != null && battle.LoserPrize != null, strBattleId);
                battle.WinnerPrize.Assert(strBattleId);
                battle.LoserPrize.Assert(strBattleId);
            }
        }

        public override void LevelUp(GamePlayer player, int oldLevel)
        {
            var newLevel = player.Data.AsDbPlayer().Level;
            var ulcLevel = APF.Settings.Arena.UnlockLevel;
            if (newLevel >= ulcLevel && oldLevel < ulcLevel)
            {
                player.World.Data.AsDbWorld().BattleArena.Join(player.GetSummary());
            }
        }

        public BattleReport LoadReport(GameWorld world, string guid)
        {
            return world.Data.AsDbWorld().BattleReports.GetValue(guid);
        }

        public BattleReport PVP(Player player1, Player player2)
        {
            return new BattleProcess(player1, player2).Run();
        }

        public BattleReport PVP(GameSession session, Player player)
        {
            return PVP(session.Player.Data.AsDbPlayer(), player);
        }

        public BattleReport PVE(Player player, BattleSetting battle)
        {
            return new BattleProcess(player, battle).Run();
        }

        public BattleReport PVE(GameSession session, int battleId)
        {
            var battle = APF.Settings.Battles.Find(battleId);
            if (battle == null)
                return null;
            var player = session.Player.Data.AsDbPlayer();
            return PVE(player, battle);            
        }

        public override void PerHalfHour(GameWorld world)
        {
            var now = DateTime.Now;
            if (now.Hour == 0 && now.Minute == 0 && now.DayOfWeek == DayOfWeek.Monday)
            {
                var playerSum = world.Data.AsDbWorld().Summaries.Values;
                var playerList = world.Data.AsDbWorld().BattleArena.PlayerList;
                var players = new List<PlayerSummary>();
                foreach (var p in playerList)
                {
                    players.Add(world.GetSummary(p));
                }
                var prize = APF.Settings.Arena.WeekRankPrizes;
                foreach (var pl in players)
                {
                    Prize arenaPrize = null;
                    foreach (var p in APF.Settings.Arena.WeekRankPrizes)
                    {
                        if (pl.ArenaRank <= p.LowestRank)
                        {
                            arenaPrize = p.Prize;
                            break;
                        }
                    }
                    world.AppServer.ModuleFactory.Module<IPrizeModule>()
                        .AddPrize(world, pl, arenaPrize);
                }
            }
        }

        private int[] _heroIds1 = new int[] { 90005, 90011, 90012, 90013, 90014, 90015, 90019, 90020, 90021 };
        private int[] _heroIds2 = new int[] { 90022, 90023, 90024, 90025, 90026, 90027, 90028, 90029, 90030 };

        public BattleReport CreateTestReport()
        {
            var report = new BattleReport();
            report.Side1 = "萧峰";
            report.Side2 = "慕容复";
            report.Sex1 = 1;
            report.Sex2 = 2;
            report.Win = true;

            for (int i = 0; i < 9; i++)
            {
                report.Units.Add(new BattleUnit()
                {
                    Id = i + 1,
                    Pos = i + 1,
                    SettingId = _heroIds1[i],
                    HP = 500,
                    XP = 100,
                    Level = 20,
                    SkillId = 91025
                });
            }
            for (int i = 0; i < 9; i++)
            {
                report.Units.Add(new BattleUnit()
                {
                    Id = i + 11,
                    Pos = i + 1,
                    SettingId = _heroIds2[i],
                    HP = 500,
                    XP = 100
                });
            }

            var round = new BattleRound() { Id = 1 };
            report.Rounds.Add(round);

            var action = new BattleAction() { UnitId = 1, SkillId = 91005 };
            action.Effects.Add(new BattleEffect() { UnitId = 11, Type = BattleEffectType.Crit, Data = 387 });
            action.Effects.Add(new BattleEffect() { UnitId = 11, Type = BattleEffectType.XpDown, Data = 50 });
            action.Effects.Add(new BattleEffect() { UnitId = 11, Type = BattleEffectType.StatusVertigo });
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.XpUpdate, Data = 50 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 2, SkillId = 91011 };
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.BlockUp, Data = 75 });
            action.Effects.Add(new BattleEffect() { UnitId = 12, Type = BattleEffectType.Common, Data = 114 });
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 12, SkillId = 91023 };
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.Common, Data = 39 });
            action.Effects.Add(new BattleEffect() { UnitId = 5, Type = BattleEffectType.Crit, Data = 132 });
            action.Effects.Add(new BattleEffect() { UnitId = 8, Type = BattleEffectType.Common, Data = 52 });
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.StatusSleep });
            action.Effects.Add(new BattleEffect() { UnitId = 12, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 3, SkillId = 91012 };
            action.Effects.Add(new BattleEffect() { UnitId = 13, Type = BattleEffectType.Common, Data = 144 });
            action.Effects.Add(new BattleEffect() { UnitId = 3, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 13, SkillId = 91024 };
            action.Effects.Add(new BattleEffect() { UnitId = 9, Type = BattleEffectType.Common, Data = 122 });
            action.Effects.Add(new BattleEffect() { UnitId = 9, Type = BattleEffectType.StatusPoisoning });
            action.Effects.Add(new BattleEffect() { UnitId = 13, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 4, SkillId = 91013 };
            action.Effects.Add(new BattleEffect() { UnitId = 9, Type = BattleEffectType.Cure, Data = 90 });
            action.Effects.Add(new BattleEffect() { UnitId = 4, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 14, SkillId = 91025 };
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.Common, Data = 78 });
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.Common, Data = 82 });
            action.Effects.Add(new BattleEffect() { UnitId = 3, Type = BattleEffectType.Crit, Data = 237 });
            action.Effects.Add(new BattleEffect() { UnitId = 4, Type = BattleEffectType.Common, Data = 144 });
            action.Effects.Add(new BattleEffect() { UnitId = 5, Type = BattleEffectType.Crit, Data = 278 });
            action.Effects.Add(new BattleEffect() { UnitId = 6, Type = BattleEffectType.Dodge, Data = 0 });
            action.Effects.Add(new BattleEffect() { UnitId = 7, Type = BattleEffectType.Dodge, Data = 0 });
            action.Effects.Add(new BattleEffect() { UnitId = 8, Type = BattleEffectType.Common, Data = 108 });
            action.Effects.Add(new BattleEffect() { UnitId = 9, Type = BattleEffectType.Crit, Data = 226 });
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.XpDown, Data = 20 });
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.XpDown, Data = 20 });
            action.Effects.Add(new BattleEffect() { UnitId = 3, Type = BattleEffectType.XpDown, Data = 20 });
            action.Effects.Add(new BattleEffect() { UnitId = 4, Type = BattleEffectType.XpDown, Data = 20 });
            action.Effects.Add(new BattleEffect() { UnitId = 5, Type = BattleEffectType.XpDown, Data = 20 });
            action.Effects.Add(new BattleEffect() { UnitId = 8, Type = BattleEffectType.XpDown, Data = 20 });
            action.Effects.Add(new BattleEffect() { UnitId = 9, Type = BattleEffectType.XpDown, Data = 20 });
            action.Effects.Add(new BattleEffect() { UnitId = 14, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 5, SkillId = 91014 };
            action.Effects.Add(new BattleEffect() { UnitId = 6, Type = BattleEffectType.XpUp, Data = 150 });
            action.Effects.Add(new BattleEffect() { UnitId = 5, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 15, SkillId = 91026 };
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.Common, Data = 103 });
            action.Effects.Add(new BattleEffect() { UnitId = 5, Type = BattleEffectType.Crit, Data = 262 });
            action.Effects.Add(new BattleEffect() { UnitId = 8, Type = BattleEffectType.Common, Data = 117 });
            action.Effects.Add(new BattleEffect() { UnitId = 15, Type = BattleEffectType.XpUpdate, Data = 50 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 6, SkillId = 91015 };
            action.Effects.Add(new BattleEffect() { UnitId = 19, Type = BattleEffectType.Crit, Data = 497 });
            action.Effects.Add(new BattleEffect() { UnitId = 19, Type = BattleEffectType.StatusLock });
            action.Effects.Add(new BattleEffect() { UnitId = 6, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 16, SkillId = 91027 };
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.Common, Data = 56 });
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.Common, Data = 61 });
            action.Effects.Add(new BattleEffect() { UnitId = 3, Type = BattleEffectType.Crit, Data = 198 });
            action.Effects.Add(new BattleEffect() { UnitId = 4, Type = BattleEffectType.Common, Data = 112 });
            action.Effects.Add(new BattleEffect() { UnitId = 5, Type = BattleEffectType.Crit, Data = 224 });
            action.Effects.Add(new BattleEffect() { UnitId = 6, Type = BattleEffectType.Common, Data = 120 });
            action.Effects.Add(new BattleEffect() { UnitId = 7, Type = BattleEffectType.Common, Data = 77 });
            action.Effects.Add(new BattleEffect() { UnitId = 8, Type = BattleEffectType.Common, Data = 108 });
            action.Effects.Add(new BattleEffect() { UnitId = 9, Type = BattleEffectType.Common, Data = 97 });
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.StatusVertigo });
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.StatusVertigo });
            action.Effects.Add(new BattleEffect() { UnitId = 4, Type = BattleEffectType.StatusVertigo });
            action.Effects.Add(new BattleEffect() { UnitId = 7, Type = BattleEffectType.StatusVertigo });
            action.Effects.Add(new BattleEffect() { UnitId = 16, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 7, SkillId = 91019 };
            action.Effects.Add(new BattleEffect() { UnitId = 7, Type = BattleEffectType.CrackUp, Data = 50 });
            action.Effects.Add(new BattleEffect() { UnitId = 11, Type = BattleEffectType.Common, Data = 137 });
            action.Effects.Add(new BattleEffect() { UnitId = 12, Type = BattleEffectType.Crit, Data = 312 });
            action.Effects.Add(new BattleEffect() { UnitId = 13, Type = BattleEffectType.Dodge, Data = 0 });
            action.Effects.Add(new BattleEffect() { UnitId = 7, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 17, SkillId = 91028 };
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.Common, Data = 32 });
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.StatusSleep });
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.XpClear });
            action.Effects.Add(new BattleEffect() { UnitId = 17, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 8, SkillId = 91020 };
            action.Effects.Add(new BattleEffect() { UnitId = 8, Type = BattleEffectType.CritUp, Data = 50 });
            action.Effects.Add(new BattleEffect() { UnitId = 12, Type = BattleEffectType.Crit, Data = 334 });
            action.Effects.Add(new BattleEffect() { UnitId = 8, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 18, SkillId = 91029 };
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.Dodge });
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.Dodge });
            action.Effects.Add(new BattleEffect() { UnitId = 3, Type = BattleEffectType.Dodge });
            action.Effects.Add(new BattleEffect() { UnitId = 18, Type = BattleEffectType.XpUpdate, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 19, SkillId = 0 };
            action.Effects.Add(new BattleEffect() { UnitId = 3, Type = BattleEffectType.Crit, Data = 127 });
            action.Effects.Add(new BattleEffect() { UnitId = 19, Type = BattleEffectType.XpUpdate, Data = 125 });
            round.Actions.Add(action);

            round = new BattleRound() { Id = 2 };
            report.Rounds.Add(round);

            action = new BattleAction() { UnitId = 2, SkillId = -1 };
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.BlockDown, Data = 75 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 13, SkillId = 0 };
            action.Effects.Add(new BattleEffect() { UnitId = 6, Type = BattleEffectType.Dodge, Data = 0 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 14, SkillId = 0 };
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.Crit, Data = 122 });
            action.Effects.Add(new BattleEffect() { UnitId = 14, Type = BattleEffectType.XpUpdate, Data = 25 });
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.XpUpdate, Data = 25 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 15, SkillId = 0 };
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.Common, Data = 56 });
            action.Effects.Add(new BattleEffect() { UnitId = 15, Type = BattleEffectType.XpUpdate, Data = 75 });
            action.Effects.Add(new BattleEffect() { UnitId = 2, Type = BattleEffectType.XpUpdate, Data = 25 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 7, SkillId = -1 };
            action.Effects.Add(new BattleEffect() { UnitId = 7, Type = BattleEffectType.CrackDown, Data = 50 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 8, SkillId = -1 };
            action.Effects.Add(new BattleEffect() { UnitId = 8, Type = BattleEffectType.CritDown, Data = 50 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 9, SkillId = -1 };
            action.Effects.Add(new BattleEffect() { UnitId = 9, Type = BattleEffectType.Poisoning, Data = 75 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 9, SkillId = 0 };
            action.Effects.Add(new BattleEffect() { UnitId = 13, Type = BattleEffectType.Dodge, Data = 0 });
            round.Actions.Add(action);

            round = new BattleRound() { Id = 3 };
            report.Rounds.Add(round);

            action = new BattleAction() { UnitId = 1, SkillId = -1 };
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.StatusNormal });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 1, SkillId = 0 };
            action.Effects.Add(new BattleEffect() { UnitId = 14, Type = BattleEffectType.Common, Data = 124 });
            action.Effects.Add(new BattleEffect() { UnitId = 1, Type = BattleEffectType.XpUpdate, Data = 50 });
            action.Effects.Add(new BattleEffect() { UnitId = 14, Type = BattleEffectType.XpUpdate, Data = 50 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 9, SkillId = -1 };
            action.Effects.Add(new BattleEffect() { UnitId = 9, Type = BattleEffectType.Poisoning, Data = 75 });
            round.Actions.Add(action);

            action = new BattleAction() { UnitId = 14, SkillId = 91004 };
            action.Effects.Add(new BattleEffect() { UnitId = 13, Type = BattleEffectType.Common, Data = 787 });
            action.Effects.Add(new BattleEffect() { UnitId = 15, Type = BattleEffectType.Common, Data = 612 });
            action.Effects.Add(new BattleEffect() { UnitId = 16, Type = BattleEffectType.Common, Data = 977 });
            action.Effects.Add(new BattleEffect() { UnitId = 17, Type = BattleEffectType.Crit, Data = 2344 });
            action.Effects.Add(new BattleEffect() { UnitId = 18, Type = BattleEffectType.Common, Data = 1280 });
            action.Effects.Add(new BattleEffect() { UnitId = 19, Type = BattleEffectType.Common, Data = 1978 });
            round.Actions.Add(action);

            return report;
        }
    }
}
