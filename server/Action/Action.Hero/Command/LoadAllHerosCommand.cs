using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hero.Command
{
    [GameCommand((int)CommandEnum.LoadAllHeros)]
    public class LoadAllHerosCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var msg = new LoadHerosArgs();
            msg.Player = player.Name;
            foreach (var hero in player.Heros)
                msg.Heros.Add(hero.ToHero1Args(player.Name));
            session.SendResponse(ID, msg);
        }
    }
}
