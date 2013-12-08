using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Battle.Command
{
    [GameCommand((int)CommandEnum.RefreshTargets)]
    public class RefreshTargetsCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var playerSum = session.Server.World.GetSummary(player.Name);
            if (playerSum == null)
                return;
            if (player.Gold < APF.Settings.Arena.RefreshCost)
            {
                session.SendError(ErrorCode.GoldNotEnough);
                return;
            }

            playerSum.RefreshArenaTargets();
            player.Gold -= APF.Settings.Arena.RefreshCost;
            session.SendResponse((int)CommandEnum.RefreshGold, player.Gold);
            LoadTargetsArgs loadTarget = new LoadTargetsArgs();
            foreach (var p in playerSum.ArenaTargets)
            {
                if (p == null)
                    continue;
                var targetSummary = session.Server.World.GetSummary(p);
                loadTarget.ArenaTargets.Add(new ArenaTargetArgs
                {
                    ArenaRank = targetSummary.ArenaRank,
                    TargetLevel = targetSummary.Level,
                    TargetName = targetSummary.Name,
                    Sex = targetSummary.Sex
                });
            }
            session.SendResponse(ID, loadTarget);
        }
    }
}
