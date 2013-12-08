using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Prize.Command
{
    [GameCommand((int)CommandEnum.GetPrizeObj)]
    public class GetPrizeObjCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session))
                return;
            var playerSum = session.Player.GetSummary();
            var prizeObj = session.Player.GetSummary().PrizeObjs.SingleOrDefault(p => p.Id == args);
            if (prizeObj == null)
            {
                session.SendError(ErrorCode.PrizeMissing);
                return;
            }
            playerSum.PrizeObjs.Remove(prizeObj);
            prizeObj.Prize.Open(session, PrizeSource.GM);
            session.SendResponse(ID, args);
        }
    }
}
