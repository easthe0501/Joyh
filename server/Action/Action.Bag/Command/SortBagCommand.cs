using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;
using System.ComponentModel.Composition;
using Action.Core;

namespace Action.Bag.Command
{
    [GameCommand((int)CommandEnum.SortBag)]
    public class SortBagCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            List<List<Item>> bags = new List<List<Item>>();
            HashSet<int> tempBagIds = new HashSet<int>();
            BagItemCollectionArgs delItems = new BagItemCollectionArgs();
            foreach (Item i in player.Bag.TempBag)
            {
                tempBagIds.Add(i.Id);
                if (i.Setting.Type == ItemType.Material)
                    player.Bag.MaterialsBag.Add(i);
                else
                    player.Bag.GoodsBag.Add(i);
            }
            player.Bag.TempBag.Clear();
            bags.Add(player.Bag.GoodsBag);
            bags.Add(player.Bag.MaterialsBag);

            //同种物品叠加
            Dictionary<int, List<Item>> tempSortDir = null;
            foreach (List<Item> li in bags)
            {
                tempSortDir = new Dictionary<int, List<Item>>();
                foreach (Item i in li)
                {
                    //判断可堆叠，并且数量少于每堆限制的物品
                    if (i.Setting.IsStack && i.Count < APF.Settings.Bag.ItemsStackLimit)
                    {
                        if (tempSortDir.ContainsKey(i.SettingId))
                            tempSortDir[i.SettingId].Add(i);
                        else
                        {
                            List<Item> items = new List<Item>();
                            items.Add(i);
                            tempSortDir.Add(i.SettingId, items);
                        }
                    }
                }

                foreach (KeyValuePair<int, List<Item>> kv in tempSortDir)
                {
                    List<Item> items = kv.Value;
                    if (items.Count == 1)
                        continue;
                    for (int i = items.Count - 1; i > 0; i--)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (items[j].Count >= APF.Settings.Bag.ItemsStackLimit)
                                continue;
                            if (items[j].Count + items[i].Count <= APF.Settings.Bag.ItemsStackLimit)
                            {
                                items[j].Count += items[i].Count;
                                items[i].Count = 0;
                                break;
                            }
                            else
                            {
                                items[i].Count = items[j].Count + items[i].Count - APF.Settings.Bag.ItemsStackLimit;
                                items[j].Count = APF.Settings.Bag.ItemsStackLimit;
                            }
                        }
                    }

                    var items0 = items.FindAll(i => i.Count == 0);
                    foreach (var i in items0)
                    {
                        var bag = BagType.TempBag;
                        if (!tempBagIds.Contains(i.Id))
                        {
                            switch (i.Setting.Type)
                            {
                                case ItemType.Material:
                                    bag = BagType.MaterialBag;
                                    break;
                                case ItemType.Equip:
                                case ItemType.UseItem:
                                case ItemType.Task:
                                case ItemType.GiftBag:
                                    bag = BagType.GoodBag;
                                    break;
                            }
                        }
                        delItems.Items.Add(new BagItemArgs()
                        {
                            Id = i.Id,
                            SortId = -1,
                            SettingId = i.SettingId,
                            Quantity = 0,
                            WhichBag = bag
                        });
                    }
                }
            }
            player.Bag.GoodsBag.DestoryAll(i => i.Count == 0);
            player.Bag.MaterialsBag.DestoryAll(i => i.Count == 0);
            if (player.Bag.GoodsBag.Count > player.Bag.GoodsBagSize)
            {
                int tempCount = player.Bag.GoodsBag.Count - player.Bag.GoodsBagSize;
                int size = player.Bag.GoodsBagSize;
                List<Item> tempItems = new List<Item>(player.Bag.GoodsBag.ToArray());
                for (int i = 0; i < tempCount; i++)
                {
                    Item tempI = tempItems[size + i];
                    player.Bag.TempBag.Add(tempI);
                    player.Bag.GoodsBag.Remove(tempI);
                }
            }
            if (player.Bag.MaterialsBag.Count > player.Bag.MaterialsBagSize)
            {
                int tempCount2 = player.Bag.MaterialsBag.Count - player.Bag.MaterialsBagSize;
                int size = player.Bag.MaterialsBagSize;
                List<Item> tempItems = new List<Item>(player.Bag.MaterialsBag.ToArray());
                for (int i = 0; i < tempCount2; i++)
                {
                    Item tempI = tempItems[size + i];
                    player.Bag.TempBag.Add(tempI);
                    player.Bag.MaterialsBag.Remove(tempI);
                }
            }
            bags.Add(player.Bag.TempBag);

            //根据装备>材料>使用道具>任务道具,次级按SettingId排序
            foreach (List<Item> li in bags)
            {
                if (li.Count == 0)
                    continue;
                li.Sort();
                int sortId = 0;
                foreach (Item i in li)
                {
                    i.SortId = sortId;
                    sortId++;
                }
            }
            session.SendResponse((int)CommandEnum.ChangeItem, delItems);
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
