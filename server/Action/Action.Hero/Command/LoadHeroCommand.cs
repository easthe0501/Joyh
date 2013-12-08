using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hero.Command
{
    [GameCommand((int)CommandEnum.LoadHero)]
    public class LoadHeroCommand : GameCommand<ObjArgs>
    {
        protected override void Run(GameSession session, ObjArgs args)
        {
            var player = APF.LoadPlayer(session.Player, args.Player);
            if (player == null)
                return;
            var hero = player.Snapshot.Find<Action.Model.Hero>(args.Id);
            if (hero == null)
                return;
            session.SendResponse(ID, hero.ToHero3Args(player.Name));
        }
    }
}
