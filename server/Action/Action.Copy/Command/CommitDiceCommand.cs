//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel.Composition;
//using Action.Engine;
//using Action.Model;

//namespace Action.Copy.Command
//{
//    [GameCommand((int)CommandEnum.CommitDice)]
//    public class CommitDiceCommand : GameCommand
//    {
//        protected override void Run(GameSession session)
//        {
//            var player = session.Player.Data.AsDbPlayer();
//            var copyInstance = player.CurrentCopy;
//            if (copyInstance == null)
//            {
//                session.SendError(ErrorCode.CopyInstanceMissing);
//                return;
//            }
//            var copyMember = copyInstance.GetMemberOnTurn();
//            if (copyMember.Name != player.Name)
//            {
//                session.SendError(ErrorCode.NotYourCopyTurn);
//                return;
//            }
//            if (copyMember.Dice < 1)
//            {
//                session.SendError(ErrorCode.DiceMissing);
//                return;
//            }
//            if (player.Energy < 1)
//            {
//                session.SendError(ErrorCode.EnergyNotEnough);
//                return;
//            }
//            session.SendResponse((int)CommandEnum.RefreshEnergy, --player.Energy);

//            //开始执行移动
//            copyMember.LastPos = copyMember.CurrentPos;
//            var egrp = new CopyEventGroup() { Player = copyMember.Name };
//            egrp.Events.AddRange(Strategy.CopyGridStrategyFactory.GetStrategy(GridType.Move)
//                .Run(session, copyMember, copyMember.Dice.ToString()));

//            //广播行为
//            foreach (var member in copyInstance.GetAllMembers())
//            {
//                var gPlayer = session.Server.World.GetPlayer(member.Name);
//                if (gPlayer != null)
//                    gPlayer.Session.SendResponse(ID, egrp);
//            }

//            //重置骰子、更换行动者
//            copyMember.Dice = 0;
//            if (copyInstance.TurnUp())
//            {
//                var playerOnTurn = copyInstance.GetMemberOnTurn().Name;
//                foreach (var member in copyInstance.GetAllMembers())
//                    session.SendResponse((int)CommandEnum.RefreshTurn, playerOnTurn);
//            }
//        }
//    }
//}
