using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hero.Command
{
    [GameCommand((int)CommandEnum.LoadQueueHeros)]
    public class LoadQueueHerosCommand : GameCommand<string>
    {
        protected override void Run(GameSession session, string args)
        {
            var player = APF.LoadPlayer(session.Player, args);
            if (player == null)
                return;
            var msg = new LoadHerosArgs();
            msg.Player = player.Name;
            msg.Sex = player.Sex;
            foreach (var hero in player.Heros.Where(h => h.Pos >= 0))
                msg.Heros.Add(hero.ToHero1Args(player.Name));
            session.SendResponse(ID, msg);
        }
    }
}
