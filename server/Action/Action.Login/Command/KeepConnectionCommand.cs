using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;
using Action.Engine;
using Action.Model;

namespace Action.Login.Command
{
    [GameCommand((int)CommandEnum.KeepConnection)]
    public class KeepConnectionCommand : GameCommand
    {
        protected override bool Ready(GameSession session)
        {
            return true;
        }

        protected override void Run(GameSession session)
        {
            
        }
    }
}
