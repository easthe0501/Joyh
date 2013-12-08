using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Copy.Command
{
    [GameCommand((int)CommandEnum.ViewCards)]
    public class ViewCardsCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var cardPro = session.Player.Data.AsDbPlayer().Temp.CardProcess;
            var msg = new CardArrayArgs();
            foreach (var card in cardPro.Cards)
                msg.Cards.Add(card.ToArgs());
            session.SendResponse(ID, msg);
        }
    }
}
