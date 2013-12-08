using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Action.Model;
using Action.Engine;
using Action.Core;

namespace Action.Chat.Command
{
    [GameCommand((int)CommandEnum.TalkToPrivate)]
    public class TalkToPrivateCommand : GameCommand<ChatArgs>
    {
        protected override CallbackQueue Queue
        {
            get { return ServerContext.GameServer.ChatQueue; }
        }

        protected override bool Ready(GameSession session, ChatArgs args)
        {
            if (base.Ready(session, args) && args.Content.Length < APF.Settings.Chat.TextMaxLength)
            {
                var speakTime = session.Player.GetSummary().SpeakTime;
                if (speakTime < DateTime.Now)
                    return true;
                else
                    session.SendResponse((int)CommandEnum.TalkDisabled, speakTime.ToLocalString());
            }
            return false;
        }

        protected override void Run(GameSession session, ChatArgs args)
        {
            var result = new PrivateChatArgs();
            result.SendPlayer = session.Player.Name;
            result.Content = ChatHelper.Filter(args.Content);
            result.ReceivePlayer = args.Player;

            var player = session.Server.World.GetPlayer(args.Player);
            if (player == null)
            {
                session.SendError(ErrorCode.PlayerOffline);
                return;
            }
            if (session.Player.Data.AsDbPlayer().Blacklist.Contains(args.Player))
            {
                session.SendError(ErrorCode.CannotTalkToBacklist);
                return;
            }
            if (player != null)
            {
                player.Session.SendResponse(ID, result);
                session.SendResponse(ID, result);
            }
            else
            {
                session.Server.World.Data.AsDbWorld().GetSummary(args.Player)
                    .OfflineCache.ChatMessages.Add(result);
            }
        }
    }
}
