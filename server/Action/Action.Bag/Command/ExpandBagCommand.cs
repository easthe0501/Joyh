//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel.Composition;
//using Action.Engine;
//using Action.Model;
//using Action.Core;

//namespace Action.Bag.Command
//{
//    [GameCommand((int)CommandEnum.ExpandBag)]
//    public class ExpandBagCommand : GameCommand<int>
//    {
//        protected override void Run(GameSession session, int args)
//        {
//            var player = session.Player.Data.AsDbPlayer();
//            //vip4级以上的玩家才可开启背包
//            if (player.Vip < APF.Settings.Bag.ExpandBagVip)
//            {
//                session.SendError(ErrorCode.VipNotEnough);
//                return;
//            }
            
//            int bagSize = args == (int)BagType.GoodBag ? player.Bag.GoodsBagSize : player.Bag.MaterialsBagSize;
//            if (bagSize / APF.Settings.Bag.CapacityExpandSize >= APF.Settings.Bag.BagMaxPage)
//            {
//                session.SendError(ErrorCode.BagSizeLimit);
//                return;
//            }
//            int money = APF.Settings.Bag.ExpandBagPageCosts.GetCost((bagSize - APF.Settings.Bag.CapacityExpandSize) / APF.Settings.Bag.CapacityExpandSize);
//            if (player.Money < money)
//            {
//                session.SendError(ErrorCode.MoneyNotEnough);
//                return;
//            }
//            player.Money -= money;
//            session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);

//            if (args == (int)BagType.GoodBag)
//            {
//                player.Bag.GoodsBagSize += APF.Settings.Bag.CapacityExpandSize;
//                bagSize = player.Bag.GoodsBagSize;
//            }
//            if (args == (int)BagType.MaterialBag)
//            {
//                player.Bag.MaterialsBagSize += APF.Settings.Bag.CapacityExpandSize;
//                bagSize = player.Bag.MaterialsBagSize;
//            }
//            session.SendResponse(ID, new ExpandBagArgs() { Capacity = bagSize, whichBag = (BagType)args });

//        }
//    }
//}
