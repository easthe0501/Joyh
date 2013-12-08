using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;

namespace Action.Battle.Command
{
    [GameCommand((int)CommandEnum.BuyPvpCount)]
    public class BuyPvpCountCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            if (player.Vip <= 0)
            {
                session.SendError(ErrorCode.VipNotEnough);
                return;
            }
            //vip每日可购买的次数count
            var count = APF.Settings.Vips.Find(player.Vip).DailyCount.BuyPvpCount;
            if (player.DailyCountHistory.BuyPvpCount >= count)
            {
                session.SendError(ErrorCode.CountOverflow);
                return;
            }
            if (player.Gold < APF.Settings.Role.GoldBuyPvpCount)
            {
                session.SendError(ErrorCode.GoldNotEnough);
                return;
            }

            player.Gold -= APF.Settings.Role.GoldBuyPvpCount;
            session.SendResponse((int)CommandEnum.RefreshGold, player.Gold);

            player.DailyCountHistory.BuyPvpCount += 1;
            //剩余购买次数
            BuyPvpCountArgs buyPvpCount = new BuyPvpCountArgs()
            {
                PvpCount = APF.Settings.Role.DailyPvpCount + player.DailyCountHistory.BuyPvpCount - player.DailyCountHistory.Pvp,
                BuyPvpCount = count - player.DailyCountHistory.BuyPvpCount
            };
            session.SendResponse(ID, buyPvpCount);
        }
    }
}
