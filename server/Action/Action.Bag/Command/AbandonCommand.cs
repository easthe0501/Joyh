using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Bag.Command
{
    [GameCommand((int)CommandEnum.Abandon)]
    public class AbandonCommand : GameCommand<SellAbandonArgs>
    {
        protected override void Run(GameSession session, SellAbandonArgs abandonArgs)
        {
            var player = session.Player.Data.AsDbPlayer();
            List<Item> tempItem = null;
            Item item = null;
            switch (abandonArgs.whichBag)
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
            item = tempItem.SingleOrDefault(p => p.Id == abandonArgs.Id);

            //判断出售的物品是否在玩家背包中
            if (item == null)
                return;
            //删除该物品
            tempItem.Remove(item);

            BagItemCollectionArgs bagItemCollectionArgs = new BagItemCollectionArgs();
            bagItemCollectionArgs.Items.Add(new BagItemArgs() { Id = item.Id, SortId = -1, SettingId = item.SettingId, Quantity = 0, WhichBag = abandonArgs.whichBag });
            session.SendResponse((int)CommandEnum.ChangeItem, bagItemCollectionArgs);
        }   
    }
}
