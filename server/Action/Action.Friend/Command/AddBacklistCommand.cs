using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Friend.Command
{
    [GameCommand((int)CommandEnum.AddBacklist)]
    public class AddBacklistCommand : GameCommand<string>
    {
        protected override void Run(GameSession session, string args)
        {
            var player = session.Player.Data.AsDbPlayer();
            if (player.Name == args)
            {
                session.SendError(ErrorCode.CannotAddSelf);
                return;
            }
            if (player.Blacklist.Contains(args))
            {
                session.SendError(ErrorCode.RepeatBlackAdd);
                return;
            }
            if (player.Blacklist.Count >= APF.Settings.Role.FriendsMax)
            {
                session.SendError(ErrorCode.OverBlacksMax);
                return;
            }
            var fPlayer = APF.LoadPlayer(session.Player, args);
            if (fPlayer == null)
            {
                session.SendError(ErrorCode.PlayerIsNotExist);
                return;
            }
            player.Blacklist.Add(args);
            player.Friends.Remove(args);

            var isOnline = session.Player.World.IsOnline(args);
            session.SendResponse(ID, new FriendArgs() { FriName = args, IfOnline = isOnline, Level = fPlayer.Level, Sex = fPlayer.Sex });
        }
    }
}
