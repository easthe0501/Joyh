using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Equip.Command
{
    [GameCommand((int)CommandEnum.StrengthenEquip)]
    public class StrengthenEquipCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            var player = session.Player.Data.AsDbPlayer();
            var equip = player.Snapshot.Find<Model.Equip>(args);
            if (equip == null)
                return;
            if (player.Money < equip.GetEquipStrenthenCost())
            {
                session.SendError(ErrorCode.MoneyNotEnough);
                return;
            }
            if (equip.Level >= player.Level)
            {
                session.SendError(ErrorCode.EquipOverPlayerLevel);
                return;
            }

            if (equip.GetEquipStrenthenCost() != 0)
            {
                player.Money -= equip.GetEquipStrenthenCost();
                session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            }
            equip.Level += 1;
            equip.Refresh();
            //若装备穿在人物身上，伙伴属性变化
            if (equip.Owner != null)
            {
                equip.Owner.Refresh();
            }
            session.SendResponse(ID, new StrengthenEquipArgs() { EquipId = args, EquipLevel = equip.Level });

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.StrengthenEquip, equip.SettingId);
        }
    }
}
