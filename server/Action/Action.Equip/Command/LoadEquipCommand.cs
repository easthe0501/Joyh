using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Equip.Command
{
    [GameCommand((int)CommandEnum.LoadEquip)]
    public class LoadEquipCommand : GameCommand<ObjArgs>
    {
        protected override void Run(GameSession session, ObjArgs args)
        {
            var player = APF.LoadPlayer(session.Player, args.Player);
            if (player == null)
                return;
            var equip = player.Snapshot.Find<Model.Equip>(args.Id);
            if (equip == null)
                return;
            equip.Refresh();
            session.SendResponse(ID, equip.ToEquipArgs(args.Player));
        }
    }
}
