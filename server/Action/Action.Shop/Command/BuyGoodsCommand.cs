using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Shop.Command
{
    [GameCommand((int)CommandEnum.BuyGoods)]
    public class BuyGoodsCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            //判断物品背包是否满
            if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session, 0))
                return;

            var player = session.Player.Data.AsDbPlayer();
            var goods = APF.Settings.Goods.Find(args);
            if (goods == null)
                return;
            
            switch (goods.Class)
            {
                case GoodsClass.Money:
                    if (player.Money < goods.NewPrice)
                    {
                        session.SendError(ErrorCode.MoneyNotEnough);
                        return;
                    }
                    if (goods.NewPrice != 0)
                    {
                        player.Money -= goods.NewPrice;
                        session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
                    }
                    break;
                case GoodsClass.Gold:
                    if(player.Gold < goods.NewPrice)
                    {
                        session.SendError(ErrorCode.GoldNotEnough);
                        return;
                    }
                    if (goods.NewPrice != 0)
                    {
                        player.Gold -= goods.NewPrice;
                        session.SendResponse((int)CommandEnum.RefreshGold, player.Gold);
                    }
                    break;
            }

            if (!session.Server.ModuleFactory.Module<IBagModule>().AddItem(session, goods.ItemId, goods.ItemCount))
                return;
            session.SendResponse(ID, goods.Id);

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.BuyGoods, goods.Id);
        }
    }
}
