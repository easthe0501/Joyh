using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Role.Command
{
    [GameCommand((int)CommandEnum.GoldBuyEnergy)]
    public class GoldBuyEnergyCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var setting = APF.Settings.Role;
            var player = session.Player.Data.AsDbPlayer();
            if (player.DailyCountHistory.BuyEnergy >= APF.Settings.Vips.Find(player.Vip).DailyCount.BuyEnergy)
            {
                session.SendError(ErrorCode.CountOverflow);
                return;
            }
            var cost = setting.GoldBuyEnergyCosts.GetCost(player.DailyCountHistory.BuyEnergy);
            if (player.Gold < cost)
            {
                session.SendError(ErrorCode.GoldNotEnough);
                return;
            }
            player.DailyCountHistory.BuyEnergy++;
            player.Gold -= cost;
            player.Energy += setting.GoldBuyEnergyIncome;
            session.SendResponse((int)CommandEnum.RefreshGold, player.Gold);
            session.SendResponse((int)CommandEnum.RefreshEnergy, player.Energy);;
            session.SendResponse(ID, new LoadBuyEnergyArgs()
            {
                Gold = setting.GoldBuyEnergyCosts.GetCost(player.DailyCountHistory.BuyEnergy),
                Energy = setting.GoldBuyEnergyIncome,
                BuyEnergyCount = APF.Settings.Vips.Find(player.Vip).DailyCount.BuyEnergy - player.DailyCountHistory.BuyEnergy
            });
        }
    }
}
