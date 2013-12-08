using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Role.Command
{
    [GameCommand((int)CommandEnum.GetExpByItem)]
    public class GetExpByItemCommand : GameCommand<UseItemsArgs>
    {
        protected override bool Ready(GameSession session, UseItemsArgs args)
        {
            var flag = true;
            foreach (var i in args.ItemIds)
                if (i < 63001 || i > 63005)
                {
                    flag = false;
                    break;
                }
            return base.Ready(session, args) && flag;
        }
        protected override void Run(GameSession session, UseItemsArgs args)
        {
            var player = session.Player.Data.AsDbPlayer();
            var hero = player.Snapshot.Find<Model.Hero>(args.HeroId);
            if (hero == null)
                return;
            if (hero.Setting.IsMain)
            {
                session.SendError(ErrorCode.MainHeroCannotEatItem);
                return;
            }
            if (hero.Level >= APF.Settings.Role.LevelMax)
            {
                session.SendError(ErrorCode.HeroLevelOverMax);
                return;
            }
            int sumCost = APF.Settings.Role.GetExpCostMoney * args.ItemIds.Count;
            if (player.Money < sumCost)
            {
                session.SendError(ErrorCode.MoneyNotEnough);
                return;
            }
            var mainHero = player.GetMainHero();
            if (hero.Level >= mainHero.Level)
            {
                session.SendError(ErrorCode.HeroLevelLimited);
                return;
            }

            int sumExp = 0;
            //Dictionary<int, int> costItem = new Dictionary<int, int>();
            //foreach(var item in args.ItemIds)
            //{
            //    sumExp += int.Parse(APF.Settings.Items.GetItem(item).Data);
            //    costItem[item] += 1;
            //}
            //IdCountPair[] costItems = new IdCountPair[costItem.Count];

            IdCountPair[] costItems = new IdCountPair[args.ItemIds.Distinct().Count()];
            int j = 0;
            for (int i = 0; i < args.ItemIds.Count; i++)
            {
                sumExp += int.Parse(APF.Settings.Items.Find(args.ItemIds[i]).Data);
                bool flag = false;
                foreach (var ci in costItems)
                {
                    if (ci == null)
                        continue;
                    if (ci.Id == args.ItemIds[i])
                    {
                        ci.Count += 1;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    costItems[j] = new IdCountPair() { Id = args.ItemIds[i], Count = 1 };
                    j++;
                }
            }
            if (!session.Server.ModuleFactory.Module<IBagModule>().ConsumeItem(session, costItems))
                return;

            player.Money -= sumCost;
            var levelUp = hero.GetExp(sumExp, mainHero);
            session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            session.SendResponse((int)CommandEnum.RefreshMateHeroLevel, new RefreshLevelArgs() 
            {
                HeroLevel = hero.Level,
                HeroExp = hero.Exp, 
                HeroId = hero.Id 
            });
            session.SendResponse(ID, args);

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.GetExpByItem, 0);

            //if (levelUp && hero == player.GetMainHero())
            //    player.OnLevelUp(session);
        }
    }
}
