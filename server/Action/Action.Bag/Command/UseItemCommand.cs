using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Bag.Command
{
    [GameCommand((int)CommandEnum.UseItem)]
    public class UseItemCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            //判断背包是否满
            if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session, BagType.GoodBag))
                return;

            var self = session.Player.Data.AsDbPlayer();
            var item = self.Snapshot.Find<Item>(args);
            if (item == null)
                return;
            //判断等级
            if (self.Level < item.Setting.Level)
            {
                session.SendError(ErrorCode.LevelNotEnough);
                return;
            }

            var strategy = Strategy.ItemStrategyFactory.Current.GetStrategy((int)item.Setting.Type);
            if (strategy != null)
            {
                if (strategy.Run(session, item))
                {
                    //背包中消耗
                    session.Server.ModuleFactory.Module<IBagModule>().ConsumeItem(session, new IdCountPair[] 
                        {
                            IdCountPair.Create(item.SettingId, 1)
                        });
                    session.SendResponse(ID, item.SettingId);

                    //任务事件
                    session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                        self, TaskType.UseItem, item.SettingId);
                }
            }
        }
    }
}
