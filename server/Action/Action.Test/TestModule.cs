//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel.Composition;
//using Action.Engine;
//using System.Diagnostics;
//using Action.Model;

//namespace Action.Test
//{
//#if DEBUG
//    //[Export(typeof(IGameModule))]
//#endif
//    public class TestModule : GameModule
//    {
//        public override void Load(GameWorld world)
//        {
//            Stopwatch sw = new Stopwatch();
//            sw.Start();
//            for (int i = 0; i < 10; i++)
//            {
//                //var account = APF.Database.LoadAccount("ky" + i);
//                //if (account != null)
//                //    APF.Database.SaveAccount(account);
//                var player = APF.Database.LoadPlayer("ky" + i);
//                if (player != null)
//                    APF.Database.SavePlayer(player);
//            }
//            sw.Stop();
//            world.AppServer.Logger.LogDebug(string.Format("{0} - {1}",
//                sw.ElapsedMilliseconds, sw.ElapsedTicks));
//        }
//    }
//}
