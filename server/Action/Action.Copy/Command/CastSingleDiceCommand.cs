using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Copy.Command
{
    [GameCommand((int)CommandEnum.CastSingleDice)]
    public class CastSingleDiceCommand : CastDiceCommandBase
    {
        protected override int GetDice(GameSession session)
        {
            return APF.Random.Range(1, 6);
        }
    }
}
