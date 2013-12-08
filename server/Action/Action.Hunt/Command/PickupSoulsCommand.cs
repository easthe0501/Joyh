using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.PickupSouls)]
    public class PickupSoulsCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var temp = new List<Soul>(player.SoulWarehouse.TempSouls.ToArray());
            foreach (Soul s in temp)
            {
                if (player.SoulWarehouse.BackSouls.Count >= APF.Settings.Role.SoulWarehouseBackSpace)
                    break;
                if (s.Setting.Quality == APF.Settings.Role.RubbishSoul)
                {
                    //卖掉,destory
                    player.Money += s.Setting.Price;
                    continue;
                }

                for (int i = 0; i < APF.Settings.Role.SoulWarehouseBackSpace; i++)
                    if (!player.SoulWarehouse.BackSouls.Exists(p => p.Pos == i))
                    {
                        s.Pos = i;
                        break;
                    }
                //s.Pos = player.SoulWarehouse.BackSouls.Count;
                player.SoulWarehouse.BackSouls.Add(s);
                player.SoulWarehouse.TempSouls.Remove(s);
            }
            player.SoulWarehouse.TempSouls.DestoryAll(p => p.Setting.Quality == APF.Settings.Role.RubbishSoul);

            bool IfFull = false;
            if (player.SoulWarehouse.TempSouls.Count > 0)
                IfFull = true;

            //返回
            PickupSoulsArgs pickupSoulsArgs = new PickupSoulsArgs();
            pickupSoulsArgs.IfFull = IfFull;
            foreach (Soul s in player.SoulWarehouse.TempSouls)
                pickupSoulsArgs.TempSouls.Add(new TempSoulArgs() { Id = s.Id, SettingId = s.SettingId });

            session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            session.SendResponse(ID, pickupSoulsArgs);
        }
    }
}
