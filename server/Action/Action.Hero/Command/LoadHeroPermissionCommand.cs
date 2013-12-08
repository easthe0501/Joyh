using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hero.Command
{
    [GameCommand((int)CommandEnum.LoadHeroPermission)]
    public class LoadHeroPermissionCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            LoadHeroShopArgs loadHeroShopArgs = new LoadHeroShopArgs();
            foreach (int i in player.Permission.Heros)
                loadHeroShopArgs.LoadHeroShop.Add(new HeroShopArgs()
                {
                    HeroSettingId = i,
                    IfPossess = player.Heros.Exists(p => p.SettingId == i)
                });
            session.SendResponse(ID, loadHeroShopArgs);
        }
    }
}
