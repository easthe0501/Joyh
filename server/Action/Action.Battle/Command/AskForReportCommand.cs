using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;

namespace Action.Battle.Command
{
    [GameCommand((int)CommandEnum.AskForReport)]
    public class AskForReportCommand:GameCommand<string>
    {
        protected override void Run(GameSession session, string args)
        {
            var report = session.Server.ModuleFactory.Module<IBattleModule>()
                    .LoadReport(session.Server.World, args);
            session.SendResponse(ID, report);
        }
    }
}
