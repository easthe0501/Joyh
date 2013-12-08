using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;

namespace Action.Role.Command
{
    [GameCommand((int)CommandEnum.LoadBuyMoney)]
    public class LoadBuyMoneyCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var setting = APF.Settings.Role;
            var player = session.Player.Data.AsDbPlayer();
            var cost = setting.GoldBuyMoneyCosts.GetCost(player.DailyCountHistory.BuyMoney);
            var money = setting.GoldBuyMoneyIncome;
            session.SendResponse(ID, new LoadBuyMoneyArgs() 
            { 
                BuyMoneyCount = APF.Settings.Vips.Find(player.Vip).DailyCount.BuyMoney - player.DailyCountHistory.BuyMoney,
                Gold = cost,
                Money = money
            });
        }
    }
}
