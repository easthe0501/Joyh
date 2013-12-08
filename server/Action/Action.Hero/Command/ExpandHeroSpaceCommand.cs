//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Action.Engine;
//using Action.Model;

//namespace Action.Hero.Command
//{
//    [GameCommand((int)CommandEnum.ExpandHeroSpace)]
//    public class ExpandHeroSpaceCommand : GameCommand
//    {
//        protected override void Run(GameSession session)
//        {
//            var player = session.Player.Data.AsDbPlayer();

//            if (player.HeroSpace >= APF.Settings.Role.HeroMaxSpace)
//            {
//                session.SendError(ErrorCode.HeroSpaceLimit);
//                return;
//            }

//            //扩展伙伴空间花费元宝
//            int gold = APF.Settings.Role.ExpandHeroSpaceCosts.GetCost(player.HeroSpace - APF.Settings.Role.HeroInitSpace);
//            if (player.Gold <= gold)
//            {
//                session.SendError(ErrorCode.GoldNotEnough);
//                return;
//            }

//            player.HeroSpace ++;
//            player.Gold -= gold;
//            session.SendResponse((int)CommandEnum.RefreshGold, player.Gold);
//            session.SendResponse(ID);
//        }
//    }
//}
