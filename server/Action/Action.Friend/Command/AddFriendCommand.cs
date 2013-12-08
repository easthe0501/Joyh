using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Friend.Command
{
    [GameCommand((int)CommandEnum.AddFriend)]
    public class AddFriendCommand : GameCommand<string>
    {
        protected override void Run(GameSession session, string friName)
        {
            var player = session.Player.Data.AsDbPlayer();
            if (player.Name == friName)
            {
                session.SendError(ErrorCode.CannotAddSelf);
                return;
            }
            if (player.Friends.Contains(friName))
            {
                session.SendError(ErrorCode.RepeatAdd);
                return;
            }
            if (player.Friends.Count >= APF.Settings.Role.FriendsMax)
            {
                session.SendError(ErrorCode.OverFirsMax);
                return;
            }
            var fPlayer = APF.LoadPlayer(session.Player, friName);
            if (fPlayer == null)
            {
                session.SendError(ErrorCode.PlayerIsNotExist);
                return;
            }
            player.Friends.Add(friName);
            player.Blacklist.Remove(friName);

            var friendPlayer = session.Player.World.GetPlayer(friName);
            if (friendPlayer != null)
                friendPlayer.Session.SendResponse((int)CommandEnum.AddedFriend, player.Name);
            //else
            //{
            //    session.Server.World.Data.AsDbWorld().GetSummary(friName)
            //        .OfflineCache.NewFriends.Add(player.Name);
            //}
            var isOnline = session.Player.World.IsOnline(friName);
            session.SendResponse(ID, new FriendArgs() { FriName = friName, IfOnline = isOnline, Level = fPlayer.Level, Sex = fPlayer.Sex });

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.AddFriend, 1);
        }
    }
}
