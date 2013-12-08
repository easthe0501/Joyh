//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Action.Engine;
//using Action.Model;

//namespace Action.Battle.Command
//{
//    [GameCommand((int)CommandEnum.RefreshArena)]
//    public class RefreshArenaCommand : GameCommand
//    {
//        protected override void Run(GameSession session)
//        {
//            var player = session.Player.Data.AsDbPlayer();
//            var playerSummary = session.Server.World.GetSummary(player.Name);
//            if (playerSummary == null)
//                return;
//            RefreshArenaArgs raa = new RefreshArenaArgs();
//            raa.ArenaRank = playerSummary.ArenaRank;
//            raa.ArenaBestRank = playerSummary.ArenaBestRank;
//            raa.PvpCount = APF.Settings.Vips.Find(player.Vip).DailyCount.BuyPvpCount - player.DailyCountHistory.Pvp;
//            if (playerSummary.ArenaLogs != null)
//            {
//                foreach (var log in playerSummary.ArenaLogs)
//                {
//                    raa.ArenaLog.Add(new ArenaLogArgs
//                    {
//                        CreateTime = log.CreateTime,
//                        ReportId = log.ReportGuid,
//                        ArenaPlayer = log.ArenaPlayer,
//                        TargetPlayer = log.TargetPlayer,
//                        WinOrLose = log.WinOrLose
//                    });
//                }
//            }
//            session.SendResponse(ID, raa);
//        }
//    }
//}
