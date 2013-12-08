//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Action.Engine;

//namespace Action.Test.Command
//{
//    [GameCommand(103)]
//    public class TestFloatCommand : GameCommand<float>
//    {
//        protected override bool Ready(GameSession session, float args)
//        {
//            return true;
//        }

//        protected override void Run(GameSession session, float args)
//        {
//            session.SendResponse(ID, args);
//        }
//    }
//}
