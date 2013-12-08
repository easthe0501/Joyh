using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;

namespace Action.Battle.Command
{
    [GameCommand((int)CommandEnum.PvpRefresh)]
    public class PvpRefreshCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {

            throw new NotImplementedException();
        }
    }
}
