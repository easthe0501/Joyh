using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Copy.Command
{
    [GameCommand((int)CommandEnum.SelectCard)]
    public class SelectCardCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var cardPro = player.Temp.CardProcess;
            var cardCosts = APF.Settings.Role.SelectCardCosts;
            if (cardPro.TryTimes >= cardCosts.Length)
            {
                session.SendError(ErrorCode.CardTimesOverflow);
                return;
            }
            var gold = cardCosts[cardPro.TryTimes];
            if (player.Gold < gold)
            {
                session.SendError(ErrorCode.GoldNotEnough);
                return;
            }
            if (gold > 0)
            {
                player.Gold -= gold;
                session.SendResponse((int)CommandEnum.RefreshGold, player.Gold);
            }
            var card = cardPro.Cards[cardPro.TryTimes];
            cardPro.TryTimes++;
            card.Prize.Open(session, PrizeSource.CopyGrid);
        }
    }
}
