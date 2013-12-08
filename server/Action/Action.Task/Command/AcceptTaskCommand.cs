using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Core;
using Action.Engine;
using Action.Model;

namespace Action.Task.Command
{
    [GameCommand((int)CommandEnum.AcceptTask)]
    public class AcceptTaskCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            var player = session.Player.Data.AsDbPlayer();
            if (player.OpenedTasks.Contains(args))
            {
                var task = APF.Settings.Tasks.Find(args);
                player.AcceptTask(task, session, true);
            }
            else
                session.SendError(ErrorCode.TaskNotOpened);
        }
    }
}
