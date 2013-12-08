//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using MongoDB.Driver;
//using MongoDB.Driver.Linq;
//using Action.Model;
//using Action.Engine;
//using Action.Core;

//namespace Action.Chat.Command
//{
//    [GameCommand((int)CommandEnum.TalkToLocal)]
//    public class TalkToLocalCommand : GameCommand<string>
//    {
//        protected override bool Ready(GameSession session, string args)
//        {
//            if (base.Ready(session, args) && args.Length < APF.Settings.Chat.TextMaxLength)
//            {
//                var speakTime = session.Player.GetSummary().SpeakTime;
//                if (speakTime < DateTime.Now)
//                    return true;
//                else
//                    session.SendResponse((int)CommandEnum.TalkDisabled, speakTime.ToLocalString());
//            }
//            return false;
//        }

//        protected override void Run(GameSession session, string args)
//        {
//            var result = new ChatArgs();
//            result.Player = session.Player.Name;
//            result.Content = ChatHelper.Filter(args);
//            var localPlayers = session.Server.World.AllPlayers
//                .Where(p => p.Display.Job == session.Player.Display.Job);
//            foreach (var player in localPlayers)
//                player.Session.SendResponse(ID, result);
//        }
//    }
//}
