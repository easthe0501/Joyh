using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Friend.Command
{
    [Export(typeof(IGameModule))]
    public class FriendModule:GameModule
    {
        public override void EnterGame(GamePlayer player)
        {
            var friMsgs = player.GetSummary().OfflineCache.NewFriends;
            foreach (var msg in friMsgs)
                player.Session.SendResponse((int)CommandEnum.AddedFriend, msg);
            friMsgs.Clear();
        }
    }
}
