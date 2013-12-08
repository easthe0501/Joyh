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
    [GameCommand((int)CommandEnum.GoldBuyMoney)]
    public class GoldBuyMoneyCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var setting = APF.Settings.Role;
            var player = session.Player.Data.AsDbPlayer();
            if (player.DailyCountHistory.BuyMoney >= APF.Settings.Vips.Find(player.Vip).DailyCount.BuyMoney)
            {
                session.SendError(ErrorCode.CountOverflow);
                return;
            }
            var cost = setting.GoldBuyMoneyCosts.GetCost(player.DailyCountHistory.BuyMoney);
            if (player.Gold < cost)
            {
                session.SendError(ErrorCode.GoldNotEnough);
                return;
            }
            player.DailyCountHistory.BuyMoney++;
            player.Gold -= cost;
            player.Money += setting.GoldBuyMoneyIncome;
            session.SendResponse((int)CommandEnum.RefreshGold, player.Gold);
            session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            session.SendResponse(ID, new GoldMoneyArgs()
            { 
                Gold = setting.GoldBuyMoneyCosts.GetCost(player.DailyCountHistory.BuyMoney), 
                Money = setting.GoldBuyMoneyIncome 
            });

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.GoldBuyMoney, 1);
        }
    }
}
