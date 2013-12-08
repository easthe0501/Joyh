//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Action.Engine;

//namespace Action.Test.Command
//{
//    [GameCommand(101)]
//    public class TestBoolCommand : GameCommand<bool>
//    {
//        protected override bool Ready(GameSession session, bool args)
//        {
//            return true;
//        }

//        protected override void Run(GameSession session, bool args)
//        {
//            session.SendResponse(ID, args);
//        }
//    }
//}
