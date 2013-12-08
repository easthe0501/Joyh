using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Role.Command
{
    [GameCommand((int)CommandEnum.ViewRolePanel)]
    public class ViewRolePanelCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var playerSummaries = session.Server.World.Data.AsDbWorld().GetSummary(player.Name);
            var args = new ViewRolePanelArgs()
            {
                Name = player.Name,
                Sex = player.Sex,
                Face = player.Face,
                Level = player.Level,
                Money = player.Money,
                Gold = player.Gold,
                Energy = player.Energy,
                Repute = player.Repute,
                Title = player.Title,
                Exp = player.Exp,
                GuildName = playerSummaries.GuildName,
                Vip = player.Vip
            };
            var setting = APF.Settings.Role;
            var cost = setting.GoldBuyEnergyCosts.GetCost(player.DailyCountHistory.BuyEnergy);
            session.SendResponse((int)CommandEnum.GoldBuyEnergy, new LoadBuyEnergyArgs() 
            {
                BuyEnergyCount = APF.Settings.Vips.Find(player.Vip).DailyCount.BuyEnergy - player.DailyCountHistory.BuyEnergy,
                Energy = setting.GoldBuyEnergyIncome,
                Gold = cost
            });
            session.SendResponse(ID, args);
        }
    }
}
