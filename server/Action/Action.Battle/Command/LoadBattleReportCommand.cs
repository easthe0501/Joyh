using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Battle.Command
{
    [GameCommand((int)CommandEnum.LoadBattleReport)]
    public class LoadBattleReportCommand : GameCommand<string>
    {
        protected override void Run(GameSession session, string args)
        {
            var report = session.Server.ModuleFactory.Module<IBattleModule>()
                .LoadReport(session.Server.World, args);
            if (report == null)
            {
                session.SendError(ErrorCode.BattleReportMissing);
                return;
            }
            session.SendResponse(ID, report);
        }
    }
}
