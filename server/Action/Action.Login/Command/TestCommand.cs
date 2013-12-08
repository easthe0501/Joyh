using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Login.Command
{
    [GameCommand(1)]
    public class TestCommand : GameCommand
    {
        protected override bool Ready(GameSession session)
        {
            return true;
        }

        protected override void Run(GameSession session)
        {
            session.SendResponse(ID);
        }
    }
}
