using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Battle.Command
{
    [GameCommand((int)CommandEnum.ChallengeInArena)]
    public class ChallengeInArenaCommand : GameCommand<string>
    {
        protected override CallbackQueue Queue
        {
            get { return ServerContext.GameServer.MainQueue; }
        }

        protected override void Run(GameSession session, string args)
        {
            var world = session.Server.World;
            var dbWorld = world.Data.AsDbWorld();
            var player = session.Player.Data.AsDbPlayer();
            var playerSum = world.GetSummary(player.Name);
            if (playerSum == null)
                return;
            if (playerSum.ArenaTargets == null)
                return;
            if (!playerSum.ArenaTargets.Contains(args))
                return;
            if(player.DailyCountHistory.Pvp >= APF.Settings.Role.DailyPvpCount + player.DailyCountHistory.BuyPvpCount)
            {
                session.SendError(ErrorCode.DailyPvpCountLimit);
                return;
            }
            
            var playerTarget = APF.LoadPlayer(session.Player, args);
            var targetSum = world.GetSummary(playerTarget.Name);
            var report = session.Server.ModuleFactory.Module<IBattleModule>().PVP(player, playerTarget);
            player.DailyCountHistory.Pvp += 1;
            //双方加战斗记录
            var newLog = new ArenaLog()
                {
                    CreateTime = MyConvert.ToSeconds(DateTime.Now),
                    ReportGuid = report.Guid,
                    ArenaPlayer = player.Name,
                    TargetPlayer = targetSum.Name,
                    WinOrLose = report.Win
                };
            if (playerSum.ArenaLogs.Count >= 5)
                playerSum.ArenaLogs.RemoveAt(playerSum.ArenaLogs.Count - 1);
            playerSum.ArenaLogs.Add(newLog);
            if (targetSum.ArenaLogs.Count >= 5)
                targetSum.ArenaLogs.RemoveAt(targetSum.ArenaLogs.Count - 1);
            targetSum.ArenaLogs.Add(newLog);
            var targetWorldPlayer = world.GetPlayer(targetSum.Name);
            //if (targetWorldPlayer != null)  //一定要用ProtoBuf生成的对象
            //{
            //    targetWorldPlayer.Session.SendResponse((int)CommandEnum.ArenaLog, newLog);
            //}
            //排名比自己高，并且自己赢了的交换排名
            if (targetSum.ArenaRank < playerSum.ArenaRank && report.Win)
            {
                world.Data.AsDbWorld().BattleArena.Exhange(playerSum, targetSum);
            }
            playerSum.RefreshArenaTargets();
            player.Temp.BattleArenaPrize = report.Win
                ? APF.Settings.Arena.WinnerPrize
                : APF.Settings.Arena.LoserPrize;
            session.SendResponse(ID, report);

            //战报加入到历史
            dbWorld.BattleReports[report.Guid] = report;
            //if (dbWorld.BattleReports.Count > 5)
            //{
            //    var keyGuid = dbWorld.BattleReports.Keys.ElementAt(0);
            //    dbWorld.BattleReports.TryRemove(keyGuid, out report);
            //}
        }
    }
}
