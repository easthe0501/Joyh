using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.PickupSoul)]
    public class PickupSoulCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            var player = session.Player.Data.AsDbPlayer();

            Soul soul = player.SoulWarehouse.TempSouls.SingleOrDefault(s => s.Id == args);
            if (soul == null)
                return;
            if (soul.Setting.Quality == APF.Settings.Role.RubbishSoul)
            {
                session.SendError(ErrorCode.BadQualitySoul);
                return;
            }
            if (player.SoulWarehouse.BackSouls.Count >= APF.Settings.Role.SoulWarehouseBackSpace)
            {
                session.SendError(ErrorCode.SoulWarehouseBackSpaceNotEnough);
                return;
            }
            for (int i = 0; i < APF.Settings.Role.SoulWarehouseBackSpace; i++)
                if (!player.SoulWarehouse.BackSouls.Exists(p => p.Pos == i))
                {
                    soul.Pos = i;
                    break;
                }
            //soul.Pos = player.SoulWarehouse.BackSouls.Count;
            player.SoulWarehouse.BackSouls.Add(soul);
            player.SoulWarehouse.TempSouls.Remove(soul);

            //返回
            session.SendResponse(ID, new SoulArgs() { Id = soul.Id, Level = soul.Level, Pos = soul.Pos, SettingId = soul.SettingId, Exp = soul.Exp });
        }
    }
}
