using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Core;
using Action.Engine;
using Action.Model;

namespace Action.Chat
{
    [Export(typeof(IGameModule))]
    public class ChatModule : GameModule, IChatModule
    {
        public override void EnterGame(GamePlayer player)
        {
            var chatMsgs = player.GetSummary().OfflineCache.ChatMessages;
            foreach (var msg in chatMsgs)
                player.Session.SendResponse((int)CommandEnum.TalkToPrivate, msg);
            chatMsgs.Clear();
        }

        //public void SystemNote(GameSession session, ChatChannel channel, string content)
        //{
        //    var targets = GetNoteTargets(session, channel);
        //    foreach (var target in targets)
        //        target.SendResponse((int)channel, new ChatArgs() { Content = content });
        //}

        //private IEnumerable<GameSession> GetNoteTargets(GameSession session, ChatChannel channel)
        //{
        //    switch (channel)
        //    {
        //        case ChatChannel.World:
        //            return session.Player.World.AllPlayers.Select(p => p.Session);
        //        case ChatChannel.Guild:
        //            return session.Player.GetGuildMembers().Select(p => p.Session);
        //        case ChatChannel.Self:
        //            return new GameSession[] { session };
        //        default:
        //            goto case ChatChannel.Self;
        //    }
        //}

        public void WorldNoteGoodEquip(GameSession session, Equip equip)
        {
            var playerName = session.Player.Name;
            var msg = new GoodEquipArgs()
            {
                Player = playerName,
                Equip = equip.ToEquipArgs(playerName)
            };
            foreach (var player in session.Server.World.AllPlayers)
                player.Session.SendResponse((int)CommandEnum.SysNoteGoodEquip, msg);
        }

        public void WorldNoteGoodSoul(GameSession session, int soulId)
        {
            var playerName = session.Player.Name;
            var msg = new GoodSoulArgs()
            {
                Player = playerName,
                SoulId = soulId
            };
            foreach (var player in session.Server.World.AllPlayers)
                player.Session.SendResponse((int)CommandEnum.SysNoteGoodSoul, msg);
        }
    }
}
