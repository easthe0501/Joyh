using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Role.Command
{
    [GameCommand((int)CommandEnum.LoadUnlockedCommands)]
    public class LoadUnlockedCommandsCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var msg = new IntArrayArgs();
            foreach (var cmdId in session.Player.Data.AsDbPlayer().Permission.Commands)
                msg.Items.Add(cmdId);
            session.SendResponse(ID, msg);
        }
    }
}
