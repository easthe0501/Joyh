using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;
using Action.Utility;

namespace Action.Bag.Command
{
    [GameCommand((int)CommandEnum.CompoundItem)]
    public class CompoundItemCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            //判断背包是否满
            if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session, BagType.GoodBag))
                return;
            var player = session.Player.Data.AsDbPlayer();
            var item = player.Snapshot.Find<Item>(args);
            if (item == null)
                return;
            IdCountPair[] icp = new IdCountPair[1];
            IdCountPair target = null;
            try
            {
                target = JsonHelper.FromJson<IdCountPair>(item.Setting.Data);
                icp[0] = new IdCountPair() { Id = item.SettingId, Count = target.Count };
            }
            catch { return; }
            if (!session.Server.ModuleFactory.Module<IBagModule>().ConsumeItem(session, icp))
                return;
            //加入背包
            int pos = -1;
            BagItemCollectionArgs addItemList = new BagItemCollectionArgs();
            List<Item> tempItems = player.Bag.GoodsBag.FindAll(p => p.SettingId == target.Id);
            if (tempItems.Count != 0)
            {
                tempItems.Sort((x, y) => y.Count - x.Count);
                if (tempItems[tempItems.Count - 1].Count + 1 <= APF.Settings.Bag.ItemsStackLimit)
                {
                    tempItems[tempItems.Count - 1].Count += 1;
                    addItemList.Items.Add(new BagItemArgs()
                    {
                        Id = tempItems[tempItems.Count - 1].Id,
                        SortId = tempItems[tempItems.Count - 1].SortId,
                        Quantity = tempItems[tempItems.Count - 1].Count,
                        SettingId = tempItems[tempItems.Count - 1].SettingId,
                        WhichBag = BagType.GoodBag
                    });
                    pos = tempItems[tempItems.Count - 1].SortId;
                    session.SendResponse((int)CommandEnum.ChangeItem, addItemList);
                    session.SendResponse(ID, tempItems[tempItems.Count - 1].Id);
                    return;
                }
                
            }

            Item targetItem = APF.Factory.CreateItem(player, ItemType.Part, target.Id);
            for (int i = 0; i <= player.Bag.GoodsBag.Count; i++)
                if (player.Bag.GoodsBag.SingleOrDefault(p => p.SortId == i) == null)
                {
                    targetItem.SortId = i;
                    break;
                }
            targetItem.Count = 1;
            player.Bag.GoodsBag.Add(targetItem);
            addItemList.Items.Add(new BagItemArgs()
            {
                Id = targetItem.Id,
                SortId = targetItem.SortId,
                Quantity = targetItem.Count,
                SettingId = targetItem.SettingId,
                WhichBag = BagType.GoodBag
            });
            pos = targetItem.SortId;
            session.SendResponse((int)CommandEnum.ChangeItem, addItemList);
            session.SendResponse(ID, targetItem.Id);
        }
    }
}
