using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using System.ComponentModel.Composition;

namespace Action.Friend.Command
{
    [GameCommand((int)CommandEnum.LoadFriend)]
    public class LoadFriendListCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var friArgs = new LoadFriendsArgs();
            foreach (string f in player.Friends)
            {
                var fPlayer = APF.LoadPlayer(session.Player, f);
                var ifOnline = session.Server.World.IsOnline(f);
                friArgs.FriendList.Add(new FriendArgs() { FriName = f, IfOnline = ifOnline, Level = fPlayer.Level, Sex = fPlayer.Sex });
            }
            session.SendResponse(ID, friArgs);
        }
    }

}
