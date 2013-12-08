//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Action.Model;
//using Action.Engine;

//namespace Action.Test.Command
//{
//    [GameCommand(105)]
//    public class TestMessageCommand : GameCommand<EnterGameArgs>
//    {
//        protected override bool Ready(GameSession session, EnterGameArgs args)
//        {
//            return true;
//        }

//        protected override void Run(GameSession session, EnterGameArgs args)
//        {
//            session.SendResponse<EnterGameArgs>((int)CommandEnum.EnterGame, args);
//        }
//    }
//}
