using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Task.Command
{
    [GameCommand((int)CommandEnum.UpdateRandomTask)]
    public class UpdateRandomTaskCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            session.UpdateRandomTask(player.RandomTask.ToArgs());
        }
    }
}
