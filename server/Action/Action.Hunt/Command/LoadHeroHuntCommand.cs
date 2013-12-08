using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.LoadHeroHunt)]
    public class LoadHeroHuntCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            var player = session.Player.Data.AsDbPlayer();
            var hero = player.Snapshot.Find<Action.Model.Hero>(args);
            if (hero == null)
                return;
            HeroHuntArgs heroHunt = new HeroHuntArgs();
            foreach (var s in hero.Souls)
            {
                heroHunt.Souls.Add(new SoulArgs
                {
                    Id = s.Id,
                    SettingId = s.SettingId,
                    Exp = s.Exp,
                    Pos = s.Pos,
                    Level = s.Level
                });
            }
            foreach(var p in player.Permission.SoulPoses)
                heroHunt.OpenPos.Add(p);

            session.SendResponse(ID, heroHunt);
        }
    }

}