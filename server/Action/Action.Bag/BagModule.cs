using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;
using Action.Core;
using System.Diagnostics;

namespace Action.Bag
{
    [Export(typeof(IGameModule))]
    public class BagModule : GameModule, IBagModule
    {
        /// <summary>
        /// 加物品
        /// </summary>
        /// <param name="session"></param>
        /// <param name="itemId"></param>
        /// <param name="itemCount"></param>
        /// <returns>true:添加成功,false:添加失败</returns>
        public bool AddItem(GameSession session, int itemId, int itemCount)
        {
            var result = false;
            if (APF.Settings.Items.Find(itemId) == null)
                return result;
            //不可叠加
            if (!APF.Settings.Items.Find(itemId).IsStack)
            {
                //继续添加
                while (itemCount > 0 && AllocateItems(session, itemId, 1))
                    itemCount -= 1;
                //添加成功
                if (itemCount <= 0)
                    result = true;
            }
            //可叠加
            else
                result = AllocateItems(session, itemId, itemCount);

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                session.Player.Data.AsDbPlayer(), TaskType.CollectItem, IdCountPair.Create(itemId, itemCount));

            return result;
        }

        /// <summary>
        /// 分批添加物品
        /// </summary>
        /// <param name="session"></param>
        /// <param name="itemId"></param>
        /// <param name="itemCount"></param>
        /// <returns>true:添加成功,false:添加失败</returns>
        public bool AllocateItems(GameSession session, int itemId, int itemCount)
        {
            var player = session.Player.Data.AsDbPlayer();
            List<Item> goodsBag = player.Bag.GoodsBag;
            List<Item> materialsBag = player.Bag.MaterialsBag;
            List<Item> tempBag = player.Bag.TempBag;
            BagItemCollectionArgs addItemList = new BagItemCollectionArgs();
            bool addItem = false;
            BagType whichBag = 0;

            List<Item> items = null;
            var itemSetting = APF.Settings.Items.Find(itemId);
            switch (APF.Settings.Items.Find(itemId).Type)
            {
                //材料背包
                case ItemType.Material:
                    //若物品可叠加,不用考虑
                    if (player.Bag.MaterialsBag.Count < player.Bag.MaterialsBagSize)
                    {
                        items = player.Bag.MaterialsBag;
                        whichBag = BagType.MaterialBag;
                    }
                    break;
                //物品背包
                default:
                    if (player.Bag.GoodsBag.Count < player.Bag.GoodsBagSize)
                    {
                        items = player.Bag.GoodsBag;
                        whichBag = BagType.GoodBag;
                    }
                    break;
            }
            //两个背包已满，但临时背包未满
            if (items == null && tempBag.Count < APF.Settings.Bag.CapacityExpandSize)
            {
                items = player.Bag.TempBag;
                whichBag = BagType.TempBag;
            }
            if (items != null)
            {
                List<Item> tempItems = items.FindAll(p => p.SettingId == itemId);
                var quantity = 0;
                if (tempItems.Count != 0)
                    quantity = tempItems[tempItems.Count - 1].Count;
                if (APF.Settings.Items.Find(itemId).IsStack)
                {
                    tempItems.Sort((x, y) => y.Count - x.Count);
                    //若堆叠后有溢出
                    if (quantity + itemCount > APF.Settings.Bag.ItemsStackLimit)
                    {
                        if (tempItems.Count != 0)
                        {
                            itemCount -= APF.Settings.Bag.ItemsStackLimit - quantity;
                            tempItems[tempItems.Count - 1].Count = APF.Settings.Bag.ItemsStackLimit;
                            addItemList.Items.Add(new BagItemArgs()
                            {
                                Id = tempItems[tempItems.Count - 1].Id,
                                SortId = tempItems[tempItems.Count - 1].SortId,
                                Quantity = tempItems[tempItems.Count - 1].Count,
                                SettingId = tempItems[tempItems.Count - 1].SettingId,
                                WhichBag = whichBag
                            });
                        }

                        int q = (int)itemCount / APF.Settings.Bag.ItemsStackLimit;    //一共可以放几堆
                        int rest = (int)itemCount % APF.Settings.Bag.ItemsStackLimit;  //剩余几个
                        for (int i = 0; i < q; i++)
                            AddToBag(session, items, itemId, APF.Settings.Bag.ItemsStackLimit, player, whichBag, addItemList);
                        if (rest != 0)
                            AddToBag(session, items, itemId, rest, player, whichBag, addItemList);
                    }
                    //直接堆叠，无溢出
                    else
                    {
                        if (tempItems.Count != 0)
                        {
                            tempItems[tempItems.Count - 1].Count += itemCount;
                            addItemList.Items.Add(new BagItemArgs()
                            {
                                Id = tempItems[tempItems.Count - 1].Id,
                                SortId = tempItems[tempItems.Count - 1].SortId,
                                Quantity = tempItems[tempItems.Count - 1].Count,
                                SettingId = tempItems[tempItems.Count - 1].SettingId,
                                WhichBag = whichBag
                            });
                        }
                        else
                        {
                            AddToBag(session, items, itemId, itemCount, player, whichBag, addItemList);
                        }
                        itemCount = 0;
                    }
                }
                else
                {
                    while (itemCount > 0)
                    {
                        AddToBag(session, items, itemId, 1, player, whichBag, addItemList);
                        itemCount--;
                    }
                }
                addItem = true;
            }

            if (addItem)
            {
                session.SendResponse((int)CommandEnum.ChangeItem, addItemList);
                return true;
            }

            return false;
        }

        private BagItemCollectionArgs AddToBag(GameSession session, List<Item> bag, int itemId, int itemCount, Player player, BagType whichBag, BagItemCollectionArgs addItemList)
        {
            Item item = APF.Factory.CreateItem(player, APF.Settings.Items.Find(itemId).Type, itemId);
            for (int i = 0; i <= bag.Count; i++)
            {
                if (bag.SingleOrDefault(p => p.SortId == i) == null)
                {
                    item.SortId = i;
                    break;
                }
            }
            item.Count = itemCount;
            bag.Add(item);
            addItemList.Items.Add(new BagItemArgs()
            {
                Id = item.Id,
                SortId = item.SortId,
                Quantity = item.Count,
                SettingId = item.SettingId,
                WhichBag = whichBag
            });
            return addItemList;
        }

        /// <summary>
        /// 消耗物品
        /// </summary>
        /// <param name="session"></param>
        /// <param name="idcps"></param>
        /// <returns></returns>
        public bool ConsumeItem(GameSession session, IdCountPair[] idcps)
        {
            if (idcps.Length == 0)
                return true;
            var player = session.Player.Data.AsDbPlayer();
            List<Item> items = null;
            Dictionary<BagType, HashSet<Item>> delItems = new Dictionary<BagType, HashSet<Item>>();
            HashSet<Item> hashItem = new HashSet<Item>();
            BagType bagType = 0;
            foreach (IdCountPair idcp in idcps)
            {
                int sumQ = 0;
                switch (APF.Settings.Items.Find(idcp.Id).Type)
                {
                    case ItemType.Material:
                        //材料背包
                        sumQ = player.Bag.MaterialsBag.Where(p => p.SettingId == idcp.Id).Sum(p => p.Count);
                        break;
                    default:
                        //物品背包
                        sumQ = player.Bag.GoodsBag.Where(p => p.SettingId == idcp.Id).Sum(p => p.Count);
                        break;
                }
                if (sumQ < idcp.Count)
                {
                    session.SendError(ErrorCode.MaterialsNotEnough);
                    return false;
                }
            }
            bool flag = false;
            foreach (IdCountPair idcp in idcps)
            {
                switch (APF.Settings.Items.Find(idcp.Id).Type)
                {
                    case ItemType.Material:
                        //材料背包
                        items = player.Bag.MaterialsBag.FindAll(p => p.SettingId == idcp.Id);
                        bagType = BagType.MaterialBag;
                        break;
                    default:
                        //物品背包
                        items = player.Bag.GoodsBag.FindAll(p => p.SettingId == idcp.Id);
                        bagType = BagType.GoodBag;
                        break;
                }
                if (items != null)
                {
                    items.Sort((x, y) => y.Count - x.Count);
                    int sumQ = items.Sum(i => i.Count);
                    if (sumQ >= idcp.Count)
                    {
                        int i = items.Count - 1;
                        int sumCount = idcp.Count;
                        while (sumCount > 0)
                        {
                            if (items[i].Count < sumCount)
                            {
                                sumCount -= items[i].Count;
                                items[i].Count = 0;
                                items[i].SortId = -1;
                                hashItem.Add(items[i]);
                                delItems[bagType] = hashItem;
                            }
                            else
                            {
                                items[i].Count -= sumCount;
                                sumCount = 0;
                                hashItem.Add(items[i]);
                                delItems[bagType] = hashItem;
                            }
                            i--;
                            if (i < 0)
                                break;
                        }
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                player.Bag.MaterialsBag.DestoryAll(p => p.Count == 0);
                player.Bag.GoodsBag.DestoryAll(p => p.Count == 0);
                DelFromBag(session, delItems);
            }
            return flag;
        }

        private void DelFromBag(GameSession session, Dictionary<BagType, HashSet<Item>> items)
        {
            BagItemCollectionArgs bagItemCollectionArgs = new BagItemCollectionArgs();
            foreach (var ikv in items)
            {
                foreach (var i in ikv.Value)
                {
                    bagItemCollectionArgs.Items.Add(new BagItemArgs()
                    {
                        Id = i.Id,
                        SettingId = i.SettingId,
                        SortId = i.Count == 0 ? -1 : i.SortId,
                        Quantity = i.Count,
                        WhichBag = ikv.Key
                    });
                }
            }
            session.SendResponse((int)CommandEnum.ChangeItem, bagItemCollectionArgs);
        }

        /// <summary>
        /// 检验背包是否已满
        /// </summary>
        /// <param name="session"></param>
        /// <param name="whichBag">物品所属背包，0:物品背包，1:材料背包,缺省-1:所有背包</param>
        /// <returns>true:背包已满,false:背包未满</returns>
        public bool IfBagFull(GameSession session, BagType whichBag = BagType.AllBag)
        {
            var player = session.Player.Data.AsDbPlayer();
            bool GoodsBagSizeFull = false;
            bool MaterialsBagSizeFull = false;
            if (player.Bag.GoodsBag.Count >= player.Bag.GoodsBagSize)
                GoodsBagSizeFull = true;
            if (player.Bag.MaterialsBag.Count >= player.Bag.MaterialsBagSize)
                MaterialsBagSizeFull = true;
            if (whichBag == BagType.AllBag)
            {
                if (GoodsBagSizeFull)
                    session.SendError(ErrorCode.GoodsBagIsFull);
                if (MaterialsBagSizeFull)
                    session.SendError(ErrorCode.MaterialsBagIsFull);
                return GoodsBagSizeFull || MaterialsBagSizeFull;
            }
            if (whichBag == BagType.GoodBag)
            {
                if (GoodsBagSizeFull)
                    session.SendError(ErrorCode.GoodsBagIsFull);
                return GoodsBagSizeFull;
            }
            if (whichBag == BagType.MaterialBag)
            {
                if (MaterialsBagSizeFull)
                    session.SendError(ErrorCode.MaterialsBagIsFull);
                return MaterialsBagSizeFull;
            }

            session.SendError(ErrorCode.BagIsFull);
            return true;
        }

        public override void PerHalfHour(GameWorld world)
        {
            var now = DateTime.Now;
            if (now.Hour == 0 && now.Minute == 0)
            {
                foreach (var player in world.AllPlayers)
                {
                    player.Data.AsDbPlayer().Bag.TempBag = new List<Item>();
                }
            }
        }

        public override void EnterGame(GamePlayer player)
        {
            if (DateTime.Compare(Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(player.Data.AsDbPlayer().LeaveTime.ToShortDateString())) > 0)
            {
                player.Data.AsDbPlayer().Bag.TempBag = new List<Item>();
            }
        }

        public override void Load(GameWorld world)
        {
            foreach (var item in APF.Settings.Items.All)
            {
                var strItemId = item.Id.ToString();

                //合法性验证
                //Trace.Assert(item.Level > 0, strItemId);
                //Trace.Assert(item.Price > 0, strItemId);
                //Trace.Assert(item.Quality > 0, strItemId);
                //升级家园花费材料验证
                foreach (var uh in APF.Settings.UpdateHomes.All)
                {
                    var strUpdateHomesId = uh.Id.ToString();
                    foreach (var items in uh.Materials)
                    {
                        Trace.Assert(APF.Settings.Items.Find(items.Id) != null, strUpdateHomesId);
                    }
                }

                foreach (var i in APF.Settings.EquipCompounds.All)
                {
                    var strEquipCompounds = i.Id.ToString();
                    foreach (var items in i.Consumable.Materials)
                        Trace.Assert(APF.Settings.Items.Find(items.Id) != null, strEquipCompounds);
                    Trace.Assert(APF.Settings.Items.Find(i.TargetId) != null, strEquipCompounds);
                }
            }
        }
    }
}
