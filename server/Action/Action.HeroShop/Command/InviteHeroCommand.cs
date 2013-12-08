using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.HeroShop.Command
{
    [GameCommand((int)CommandEnum.InviteHero)]
    public class InviteHeroCommand:GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            
        }
    }
}
