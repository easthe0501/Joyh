//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Action.Engine;
//using Action.Model;

//namespace Action.Test.Command
//{
//    [GameCommand(106)]
//    public class TestResponseCommand : GameCommand<int>
//    {
//        protected override bool Ready(GameSession session, int args)
//        {
//            return true;
//        }

//        protected override void Run(GameSession session, int args)
//        {
//            var report = session.Server.ModuleFactory
//                .Module<IBattleModule>().CreateTestReport();
//            for (var i = 0; i < args; i++)
//                session.SendResponse(106, report);
//        }
//    }
//}
