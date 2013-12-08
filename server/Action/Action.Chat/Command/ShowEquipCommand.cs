using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Chat.Command
{
    [GameCommand((int)CommandEnum.ShowEquip)]
    public class ShowEquipCommand : GameCommand<int>
    {
        protected override CallbackQueue Queue
        {
            get { return ServerContext.GameServer.ChatQueue; }
        }

        protected override void Run(GameSession session, int args)
        {
            var equip = session.Player.Data.AsDbPlayer().Snapshot.Find<Equip>(args);
            if (equip == null)
                return;
            equip.Refresh();

            //基础属性
            var msg = new ShowEquipArgs();
            msg.Sender = session.Player.Name;
            msg.Data = equip.ToEquipArgs(msg.Sender);

            //向世界广播
            foreach(var chatPlayer in session.Server.World.AllPlayers)
                chatPlayer.Session.SendResponse(ID, msg);
        }
    }
}
