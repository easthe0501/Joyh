using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Utility;

namespace Action.Copy.Command
{
    [GameCommand((int)CommandEnum.CastCustomDice)]
    public class CastCustomDiceCommand : CastDiceCommandBase<int>
    {
        protected override bool Ready(GameSession session, int args)
        {
            return base.Ready(session, args) && NumberHelper.Between(args, 1, 6);
        }

        protected override int GetDice(GameSession session, int args)
        {
            return args;
        }
    }
}
