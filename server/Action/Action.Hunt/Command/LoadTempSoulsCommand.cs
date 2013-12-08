using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.LoadHunt)]
    public class LoadTempSoulsCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();

            LoadHunthouseArgs loadHunthouseArgs = new LoadHunthouseArgs();
            foreach (Soul s in player.SoulWarehouse.TempSouls)
                loadHunthouseArgs.TempSouls.Add(new TempSoulArgs() { Id = s.Id, SettingId = s.SettingId });
            foreach (int i in player.LightSoulQualities)
                loadHunthouseArgs.LightSoulQualities.Add(i);

            session.SendResponse(ID, loadHunthouseArgs);
        }
    }
}
