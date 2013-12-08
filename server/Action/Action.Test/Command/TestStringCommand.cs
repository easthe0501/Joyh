//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Action.Engine;

//namespace Action.Test.Command
//{
//    [GameCommand(104)]
//    public class TestStringCommand : GameCommand<string>
//    {
//        protected override bool Ready(GameSession session, string args)
//        {
//            return true;
//        }

//        protected override void Run(GameSession session, string args)
//        {
//            session.SendResponse(ID, args);
//        }
//    }

//    [BackCommand(503)]
//    public class TestStringCommandBack : BackCommand<string>
//    {
//        protected override bool Ready(BackSession session, string args)
//        {
//            return true;
//        }

//        protected override void Run(BackSession session, string args)
//        {
//            session.SendResponse(ID, args);
//        }
//    }
//}
