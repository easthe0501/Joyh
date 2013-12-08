using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;
using Action.Model;
using Action.Engine;

namespace Action.Hero.Command
{
    [GameCommand((int)CommandEnum.LoadEmbattle)]
    public class LoadEmbattleCommand:GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            LoadEmbattleArgs lea = new LoadEmbattleArgs();
            foreach (var i in player.Permission.EmbattlePoses)
                lea.Pos.Add(i);
            session.SendResponse(ID, lea);
        }
    }
}
