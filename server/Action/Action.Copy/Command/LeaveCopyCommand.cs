using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Copy.Command
{
    [GameCommand((int)CommandEnum.LeaveCopy)]
    public class LeaveCopyCommand : GameCommand
    {
        protected override bool Ready(GameSession session)
        {
            return base.Ready(session) && session.Player.Data.AsDbPlayer().CurrentCopy != null;
        }

        protected override void Run(GameSession session)
        {
            CopyHelper.LeaveCopy(session);
        }
    }
}
