using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.HeroShop.Command
{
    [GameCommand((int)CommandEnum.LoadHeroShop)]
    public class LoadHeroShopCommand:GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            player.HeroShop
        }
    }
}
