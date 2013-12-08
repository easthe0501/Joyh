using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Prize.Command
{
    [GameCommand((int)CommandEnum.NoticePrizeObjs)]
    public class NoticePrizeObjsCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            session.Server.ModuleFactory.Module<IPrizeModule>().NoticePrizes(session.Player);
        }
    }
}
