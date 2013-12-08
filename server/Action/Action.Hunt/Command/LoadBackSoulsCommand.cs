using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.LoadBackSouls)]
    public class LoadBackSoulsCommand:GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            SoulsArgs backArgs = new SoulsArgs();
            foreach (Soul s in player.SoulWarehouse.BackSouls)
                backArgs.Souls.Add(new SoulArgs() { Id = s.Id, SettingId = s.SettingId, Pos = s.Pos, Level = s.Level, Exp = s.Exp });

            session.SendResponse(ID, backArgs);
        }
    }
}
