using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;

namespace Action.Role.Command
{
    [GameCommand((int)CommandEnum.DailyCount)]
    public class DailyCountCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            DailyCountArgs dailyCount = new DailyCountArgs()
            {
                BuyEnergy = APF.Settings.Vips.Find(player.Vip).DailyCount.BuyEnergy - player.DailyCountHistory.BuyEnergy,
                BuyMoney = APF.Settings.Vips.Find(player.Vip).DailyCount.BuyMoney - player.DailyCountHistory.BuyMoney,
                ContributeForGuild = APF.Settings.Guild.DailyContributeCount - player.DailyCountHistory.ContributeForGuild,
                Pvp = APF.Settings.Role.DailyPvpCount + player.DailyCountHistory.BuyPvpCount - player.DailyCountHistory.Pvp,
                PvpCount = APF.Settings.Vips.Find(player.Vip).DailyCount.BuyPvpCount - player.DailyCountHistory.BuyPvpCount
            };

            session.SendResponse(ID, dailyCount);
        }
    }
}
