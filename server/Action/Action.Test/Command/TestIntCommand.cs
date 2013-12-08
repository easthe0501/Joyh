//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Action.Engine;

//namespace Action.Test.Command
//{
//    [GameCommand(102)]
//    public class TestIntCommand : GameCommand<int>
//    {
//        protected override bool Ready(GameSession session, int args)
//        {
//            return true;
//        }

//        protected override void Run(GameSession session, int args)
//        {
//            session.SendResponse(ID, args);
//        }
//    }

//    [BackCommand(102)]
//    public class TestIntCommandBack : BackCommand<int>
//    {
//        protected override bool Ready(BackSession session, int args)
//        {
//            return true;
//        }

//        protected override void Run(BackSession session, int args)
//        {
//            session.SendResponse(ID, args);
//        }
//    }
//}
