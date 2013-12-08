using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.SortSouls)]
    public class SortSoulsCommand:GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var souls = player.SoulWarehouse.BackSouls;
            souls.Sort();
            //if (this.Setting.Quality < other.Setting.Quality)
            //    return 1;
            //if (this.Setting.Quality == other.Setting.Quality)
            //{
            //    if (this.Level < other.Level)
            //        return 1;
            //    if (this.Level == other.Level)
            //    {
            //        if (this.Exp < other.Exp)
            //            return 1;
            //        if (this.Exp == other.Exp)
            //        {
            //            if (this.Setting.Type < other.Setting.Type)
            //                return 1;
            //            if (this.Setting.Type == other.Setting.Type)
            //                return 0;
            //        }
            //    }
            //}
            int pos = 0;
            foreach (var s in souls)
            {
                s.Pos = pos;
                pos++;
            }
            SoulsArgs backArgs = new SoulsArgs();
            foreach (Soul s in player.SoulWarehouse.BackSouls)
                backArgs.Souls.Add(new SoulArgs() { Id = s.Id, SettingId = s.SettingId, Pos = s.Pos, Level = s.Level, Exp = s.Exp });
            session.SendResponse(ID, backArgs);
        }
    }
}
