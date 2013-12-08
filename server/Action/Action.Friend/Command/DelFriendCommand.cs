using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Friend.Command
{
    [GameCommand((int)CommandEnum.DelFriend)]
    public class DelFriendCommand : GameCommand<DelFriendArgs>
    {
        protected override void Run(GameSession session, DelFriendArgs args)
        {
            var player = session.Player.Data.AsDbPlayer();
            if (APF.ContainsPlayer(session.Player, args.friName))
            {
                switch (args.FriOrBlack)
                {
                    case 0:
                        if (!player.Blacklist.Contains(args.friName))
                            return;
                        player.Blacklist.Remove(args.friName);
                        break;
                    case 1:
                        if (!player.Friends.Contains(args.friName))
                            return;
                        player.Friends.Remove(args.friName);
                        break;
                    default:
                        return;
                }
            }
            session.SendResponse(ID, args);
        }
    }

}