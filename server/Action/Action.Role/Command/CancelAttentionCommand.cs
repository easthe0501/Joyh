//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel.Composition;
//using Action.Engine;
//using Action.Model;

//namespace Action.Role.Command
//{
//    [GameCommand((int)CommandEnum.CancelAttention)]
//    public class CancelAttentionCommand : GameCommand<int>
//    {
//        protected override void Run(GameSession session, int args)
//        {
//            session.Player.Attentions.Remove(args);
//        }
//    }
//}
