using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.SellSouls)]
    public class SellSoulsCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();

            var souls = player.SoulWarehouse.TempSouls;
            var moneys = 0;
            TempSoulsArgs tempSouls = new TempSoulsArgs();
            foreach (var s in souls)
            {
                if (s.Setting.Quality != APF.Settings.Role.RubbishSoul)
                    continue;
                moneys += s.Setting.Price;
                tempSouls.TempSoul.Add(new TempSoulArgs() { Id = s.Id, SettingId = s.SettingId });
            }
            player.SoulWarehouse.TempSouls.DestoryAll(s => s.Setting.Quality == APF.Settings.Role.RubbishSoul);
            if (moneys != 0)
            {
                player.Money += moneys;
                session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            }

            session.SendResponse(ID, tempSouls);
        }
    }
}
