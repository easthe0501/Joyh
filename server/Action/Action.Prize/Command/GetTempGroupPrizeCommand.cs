//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel.Composition;
//using Action.Core;
//using Action.Engine;
//using Action.Model;

//namespace Action.Prize.Command
//{
//    [GameCommand((int)CommandEnum.GetTempGroupPrize)]
//    public class GetTempGroupPrizeCommand : GameCommand<int>
//    {
//        protected override bool Ready(GameSession session, int args)
//        {
//            return base.Ready(session, args) && args > 0;
//        }

//        protected override void Run(GameSession session, int args)
//        {
//            if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session))
//                return;
//            var pKey = args.ToString();

//            var player = session.Player.Data.AsDbPlayer();
//            var prizeGroup = player.TempPrizeGroup;
//            var prize = prizeGroup.Prizes.GetValue(pKey);
//            if (prize == null)
//            {
//                session.SendError(ErrorCode.PrizeMissing);
//                return;
//            }
//            if (args >= APF.Settings.Role.GetGroupPrizeCosts.Length)
//            {
//                session.SendError(ErrorCode.GetPrizeTimesOverflow);
//                return;
//            }
//            var cost = APF.Settings.Role.GetGroupPrizeCosts[args];
//            if (player.Money < cost)
//            {
//                session.SendError(ErrorCode.MoneyNotEnough);
//                return;
//            }
//            session.SendResponse((int)CommandEnum.RefreshMoney, player.Money -= cost);

//            prizeGroup.Prizes.Remove(pKey);
//            prizeGroup.TryTimes++;
//            prize.Open(session);

//            var tip = prize.ToTip(PrizeTag.Copy);
//            tip.Id = args;
//            session.SendResponse(ID, tip);
//        }
//    }
//}
