using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using System.ComponentModel.Composition;
using Action.Core;
using Action.Utility;

namespace Action.Bag.Command
{
    [GameCommand((int)CommandEnum.MoveItem)]
    public class MoveItemCommand : GameCommand<MoveItemArgs>
    {
        protected override bool Ready(GameSession session, MoveItemArgs args)
        {
            return base.Ready(session, args) && NumberHelper.Between(args.TargetSortId, 1, 
                APF.Settings.Bag.CapacityExpandSize);
        }

        protected override void Run(GameSession session, MoveItemArgs moveItemArgs)
        {
            var player = session.Player.Data.AsDbPlayer();

            List<Item> tempItem = null;
            Item item = null;
            
            switch (moveItemArgs.whichBag)
            {
                case BagType.GoodBag:
                    tempItem = player.Bag.GoodsBag;
                    break;
                case BagType.MaterialBag:
                    tempItem = player.Bag.MaterialsBag;
                    break;
                case BagType.TempBag:
                    tempItem = player.Bag.TempBag;
                    break;
            }

            item = tempItem.SingleOrDefault(p => p.Id == moveItemArgs.Id);
            if (item == null)
                return;
            Item targetItem = tempItem.SingleOrDefault(p => p.SortId == moveItemArgs.TargetSortId);
            
            BagItemCollectionArgs bagItemCollectionArgs = new BagItemCollectionArgs();
            //移到空白位置
            if (targetItem == null)
            {
                item.SortId = moveItemArgs.TargetSortId;
                bagItemCollectionArgs.Items.Add(new BagItemArgs() { Id = item.Id, SettingId = item.SettingId, SortId = item.SortId, Quantity = item.Count, WhichBag = moveItemArgs.whichBag });
            }
            else
            {
                //物品是否能叠加
                if (!item.Setting.IsStack)
                    return;
                //比较两个物品是否是同种物品
                if (item.SettingId != targetItem.SettingId)
                    return;
                //目标物品可否继续叠加
                if (targetItem.Count >= APF.Settings.Bag.ItemsStackLimit)
                    return;

                int sumCount = item.Count + targetItem.Count;
                int stackCount = sumCount / APF.Settings.Bag.ItemsStackLimit;
                int restNum = sumCount % APF.Settings.Bag.ItemsStackLimit;
                //一堆
                if (stackCount == 0)
                {
                    tempItem.Remove(item);
                    targetItem.Count = item.Count + targetItem.Count;
                    item.Count = 0;
                    item.SortId = -1;
                }
                //两堆
                if (stackCount > 0)
                {
                    item.Count = restNum;
                    targetItem.Count = APF.Settings.Bag.ItemsStackLimit;
                }
                
                bagItemCollectionArgs.Items.Add(new BagItemArgs() { Id = item.Id, SettingId = item.SettingId, SortId = item.SortId, Quantity = item.Count, WhichBag = moveItemArgs.whichBag });
                bagItemCollectionArgs.Items.Add(new BagItemArgs() { Id = targetItem.Id, SettingId = targetItem.SettingId, SortId = targetItem.SortId, Quantity = targetItem.Count, WhichBag = moveItemArgs.whichBag });
            }

            session.SendResponse((int)CommandEnum.ChangeItem, bagItemCollectionArgs);
        }

    }
}
