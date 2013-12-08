using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Battle.Command
{
    [GameCommand((int)CommandEnum.LoadTempBattleReport)]
    public class LoadTempBattleReportCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var report = session.Player.Data.AsDbPlayer().Temp.BattleReport;
            if (report == null)
            {
                session.SendError(ErrorCode.BattleReportMissing);
                return;
            }
            session.SendResponse(ID, report);
        }
    }
}
