using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Battle.Command
{
    [GameCommand((int)CommandEnum.LoadArenaTop10)]
    public class LoadArenaTop10Command : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var players = session.Server.World.Data.AsDbWorld().Summaries.Values.Where(p => p.ArenaRank <= 10 && p.ArenaRank != 0);
            LoadTop10 loadTop = new LoadTop10();
            foreach (var p in players)
            {
                loadTop.Top10.Add(new Top10Args() { Name = p.Name, Level = p.Level, Rank = p.ArenaRank, Sex = p.Sex });
            }
            session.SendResponse(ID, loadTop);
        }
    }
}
