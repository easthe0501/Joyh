using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using System.ComponentModel.Composition;

namespace Action.Home.Command
{
    [GameCommand((int)CommandEnum.TempToBag)]
    public class TempToBagCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            var player = session.Player.Data.AsDbPlayer();
            List<Item> goodsBag = player.Bag.GoodsBag;
            List<Item> materialsBag = player.Bag.MaterialsBag;

            Item item = player.Bag.TempBag.SingleOrDefault(p => p.Id == args);
            if (item == null)
                return;

            bool success = false;
            switch (item.Setting.Type)
            {
                case ItemType.Material:
                    //材料背包
                    //判断背包是否满
                    if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session, BagType.MaterialBag))
                        return;
                    if (materialsBag.Count < player.Bag.MaterialsBagSize)
                    {
                        for (int i = 0; i <= materialsBag.Count; i++)
                            if (materialsBag.SingleOrDefault(p => p.SortId == i) == null)
                            {
                                item.SortId = i;
                                break;
                            }
                        materialsBag.Add(item);
                        player.Bag.TempBag.Remove(item);
                        success = true;
                    }
                    break;
                case ItemType.Equip:
                case ItemType.UseItem:
                case ItemType.Task:
                case ItemType.GiftBag:
                    //物品背包
                    if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session, BagType.GoodBag))
                        return;
                    if (goodsBag.Count < player.Bag.GoodsBagSize)
                    {
                        for (int i = 0; i <= goodsBag.Count; i++)
                            if (goodsBag.SingleOrDefault(p => p.SortId == i) == null)
                            {
                                item.SortId = i;
                                break;
                            }
                        goodsBag.Add(item);
                        player.Bag.TempBag.Remove(item);
                        success = true;
                    }
                    break;
            }
            if (!success)
            {
                session.SendError(ErrorCode.BagIsFull);
                return;
            }

            //返回
            var sortBagArg = new BagsArgs();
            foreach (Item ig in player.Bag.GoodsBag)
            {
                sortBagArg.GoodsBag.Add(new BagItemArgs() { Id = ig.Id, SortId = ig.SortId, Quantity = ig.Count, SettingId = ig.SettingId });
            }
            foreach (Item im in player.Bag.MaterialsBag)
            {
                sortBagArg.MaterialsBag.Add(new BagItemArgs() { Id = im.Id, SortId = im.SortId, Quantity = im.Count, SettingId = im.SettingId });
            }
            foreach (Item it in player.Bag.TempBag)
            {
                sortBagArg.TempBag.Add(new BagItemArgs() { Id = it.Id, SortId = it.SortId, Quantity = it.Count, SettingId = it.SettingId });
            }
            session.SendResponse(ID, sortBagArg);
        } 
    }
}
