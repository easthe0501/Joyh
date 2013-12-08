using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hero.Command
{
    [GameCommand((int)CommandEnum.InviteHero)]
    public class InviteHeroCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            var player = session.Player.Data.AsDbPlayer();
            HeroSetting heroSetting = APF.Settings.Heros.Find(args);
            if (heroSetting == null)
                return;
            if (heroSetting.IsMain)
                return;
            if (!player.Permission.Heros.Contains(args))
            {
                session.SendError(ErrorCode.HeroLocked);
                return;
            }
            var oldhero = player.Heros.SingleOrDefault(p => p.SettingId == args);
            if (oldhero != null && oldhero.Pos != -1)
            {
                session.SendError(ErrorCode.HeroRepeated);
                return;
            }

            if (player.Repute < heroSetting.Repute)
            {
                session.SendError(ErrorCode.ReputeNotEnough);
                return;
            }

            if (player.Money < heroSetting.Money)
            {
                session.SendError(ErrorCode.MoneyNotEnough);
                return;
            }

            if (player.Heros.Count(h => h.Pos >= 0) >= player.HeroSpace)
            {
                session.SendError(ErrorCode.HeroSpaceNotEnough);
                return;
            }
            //if (player.Heros.Count >= APF.Settings.Role.MaxHeroQueueCount)
            //{
            //    session.SendError(ErrorCode.HeroQueueIsFull);
            //    return;
            //}

            //新招聘
            if (oldhero == null)
            {
                var hero = APF.Factory.Create<Action.Model.Hero>(player, args);
                if (hero.Setting.Money != 0)
                {
                    player.Money -= hero.Setting.Money;
                    session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
                }
                hero.Pos = 0;
                player.Heros.Add(hero);
            }
            //if (player.Heros.Count(h => h.Pos >= 0) >= APF.Settings.Role.MaxHeroQueueCount)
            //    hero.Pos = -1;
            //else
            if (oldhero != null && oldhero.Pos == -1)
            {
                oldhero.Pos = 0;
            }
            
            session.SendResponse(ID, args);

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.InviteHero, args);
        }
    }
}
