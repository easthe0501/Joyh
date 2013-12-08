using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Copy.Command
{
    [GameCommand((int)CommandEnum.LoadUnlockedCopies)]
    public class LoadUnlockedCopiesCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var msg = new CopyArgsArray();
            foreach (var copyId in player.Permission.Copies)
            {
                var copyArgs = new CopyArgs()
                {
                    CopyId = copyId,
                    Finished = player.FinishedCopies.Contains(copyId)
                };
                var copySetting = APF.Settings.Copies.Find(copyId);
                if (copySetting.Grade == CopyGrade.Common)
                {
                    var taskId = copySetting.EnterRequirement.TaskId;
                    if (taskId != 0 && !player.ClosedTasks.Contains(taskId))
                        copyArgs.Locked = true;
                    msg.Copies.Add(copyArgs);
                }
            }
            session.SendResponse(ID, msg);
        }
    }
}
