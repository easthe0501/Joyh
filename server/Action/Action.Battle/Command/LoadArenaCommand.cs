using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Battle.Command
{
    [GameCommand((int)CommandEnum.LoadArena)]
    public class LoadArenaCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var playerSummary = session.Server.World.GetSummary(player.Name);
            if (playerSummary == null)
                return;

            LoadArenaArgs loadArena = new LoadArenaArgs();
            loadArena.ArenaBestRank = playerSummary.ArenaBestRank;
            loadArena.ArenaRank = playerSummary.ArenaRank;
            loadArena.ArenaScore = playerSummary.ArenaScore;
            loadArena.PvpCount = APF.Settings.Role.DailyPvpCount + player.DailyCountHistory.BuyPvpCount - player.DailyCountHistory.Pvp;
            loadArena.BuyPvpCount = player.DailyCountHistory.BuyPvpCount;

            //补丁代码，之后一定要找出ArenaTargets=null的问题
            if (playerSummary.ArenaTargets == null || playerSummary.ArenaRank == 0)
            {
                playerSummary.World.BattleArena.Join(playerSummary);
                playerSummary.RefreshArenaTargets();
            }

            if (playerSummary.ArenaTargets.Length == 0)
                playerSummary.RefreshArenaTargets();
            foreach (var p in playerSummary.ArenaTargets)
            {
                if (p == null)
                    continue;
                var targetSummary = session.Server.World.GetSummary(p);
                loadArena.ArenaTargets.Add(new ArenaTargetArgs
                {
                    ArenaRank = targetSummary.ArenaRank,
                    TargetLevel = targetSummary.Level,
                    TargetName = targetSummary.Name,
                    Sex = targetSummary.Sex
                });
            }

            if (playerSummary.ArenaLogs != null)
            {
                foreach (var log in playerSummary.ArenaLogs)
                {
                    loadArena.ArenaLog.Add(new ArenaLogArgs
                    {
                        CreateTime = log.CreateTime,
                        ReportId = log.ReportGuid,
                        ArenaPlayer = log.ArenaPlayer,
                        TargetPlayer = log.TargetPlayer,
                        WinOrLose = log.WinOrLose
                    });
                }
            }

            Prize prize = null;
            foreach (var p in APF.Settings.Arena.WeekRankPrizes)
            {
                if (playerSummary.ArenaRank <= p.LowestRank)
                {
                    prize = p.Prize;
                    break;
                }
            }
            if (prize != null)
            {
                loadArena.Prize = new ArenaPrize()
                {
                    Gold = prize.Gold,
                    Money = prize.Money,
                    Repute = prize.Repute
                };
            }

            session.SendResponse(ID, loadArena);
        }
    }
}
