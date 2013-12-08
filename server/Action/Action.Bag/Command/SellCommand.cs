using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using System.ComponentModel.Composition;

namespace Action.Bag.Command
{
    [GameCommand((int)CommandEnum.Sell)]
    public class SellCommand : GameCommand<SellAbandonArgs>
    {
        protected override void Run(GameSession session, SellAbandonArgs sellArgs)
        {
            var player = session.Player.Data.AsDbPlayer();

            List<Item> tempItem = null;
            Item item = null;
            switch (sellArgs.whichBag)
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
            item = tempItem.SingleOrDefault(p => p.Id == sellArgs.Id);
            
            //判断出售的物品是否在玩家背包中
            if (item == null)
                return;
            if (item.Setting.Type == ItemType.UseItem)
            {
                session.SendError(ErrorCode.ItemCannotSell);
                return;
            }

            //获得银币
            if (item.Setting.Price != 0)
            {
                player.Money += (int)(item.Setting.Price * item.Count * APF.Settings.Bag.ItemSellRate);
                session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            }
            tempItem.Remove(item);

            BagItemCollectionArgs bagItemCollectionArgs = new BagItemCollectionArgs();
            bagItemCollectionArgs.Items.Add(new BagItemArgs() { Id = item.Id, SortId = -1, SettingId = item.SettingId, Quantity = 0, WhichBag = sellArgs.whichBag });
            session.SendResponse((int)CommandEnum.ChangeItem, bagItemCollectionArgs);
        }
    }
}
