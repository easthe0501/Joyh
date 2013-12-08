using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Shop.Command
{
    [GameCommand((int)CommandEnum.LoadShop)]
    public class LoadShopCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var loadShopArgs = new LoadShopArgs();
            foreach (var gs in APF.Settings.Goods.All)
                loadShopArgs.Goods.Add(new ShopGoodsArgs() { 
                    GoodsClass = (int)gs.Class,
                    ItemId = gs.ItemId,
                    ItemCount = gs.ItemCount,
                    OldPrice = gs.OldPrice,
                    NewPrice = gs.NewPrice,
                    IsNew = gs.IsNew,
                    IsDiscount = gs.IsDiscount,
                    GoodsId = gs.Id
                });

            session.SendResponse(ID, loadShopArgs);
        }
    }
}
