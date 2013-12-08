using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Core;
using Action.Engine;

namespace Action.Hero.Command
{
    [GameCommand((int)CommandEnum.UpDownQueue)]
    public class UpDownQueueCommand : GameCommand<UpDownQueueArgs>
    {
        protected override void Run(GameSession session, UpDownQueueArgs args)
        {
            var player = session.Player.Data.AsDbPlayer();
            
            if(args.Flag == 1 && player.Heros.Count(h => h.Pos >= 0) >= APF.Settings.Role.MaxHeroQueueCount)
            {
                session.SendError(ErrorCode.HeroQueueIsFull);
                return;
            }

            var hero = player.Heros.SingleOrDefault(p => p.Id == args.HeroId);
            if (hero == null)
                return;
            if (hero.Setting.IsMain)
            {
                session.SendError(ErrorCode.MainHeroCannotOffQueue);
                return;
            }
            if (hero.Equips.Count != 0)
            {
                session.SendError(ErrorCode.HeroHasEquipCannotOffQueue);
                return;
            }
            if (hero.Souls.Count != 0)
            {
                session.SendError(ErrorCode.HeroHasSoulCannotOffQueue);
                return;
            }

            if (args.Flag == 1)
            {
                if (player.Money < hero.Setting.Money)
                {
                    session.SendError(ErrorCode.MoneyNotEnough);
                    return;
                }
                hero.Pos = 0;
                player.Money -= hero.Setting.Money;
                session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            }
            if (args.Flag == -1)
            {
                if (hero.Pos > 0)
                {
                    session.SendError(ErrorCode.HeroInEmbattle);
                    return;
                }
                hero.Pos = -1;
            }
            session.SendResponse(ID, args);
        }
    }
}