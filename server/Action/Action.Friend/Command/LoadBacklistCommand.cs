using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Friend.Command
{
    [GameCommand((int)CommandEnum.LoadBacklist)]
    public class LoadBacklistCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var backlistArgs = new LoadFriendsArgs();
            foreach (string b in player.Blacklist)
            {
                var fPlayer = APF.LoadPlayer(session.Player, b);
                var ifOnline = session.Server.World.IsOnline(b);
                backlistArgs.FriendList.Add(new FriendArgs() { FriName = b, IfOnline = ifOnline, Level = fPlayer.Level, Sex = fPlayer.Sex });
            }
            session.SendResponse(ID, backlistArgs);
        }
    }
}
