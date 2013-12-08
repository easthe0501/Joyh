using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;
using Action.Engine;
using Action.Model;

namespace Action.Task.Command
{
    public abstract class LoadTasksCommand : GameCommand
    {
        protected abstract IEnumerable<TaskSetting> Filter(IEnumerable<TaskSetting> tasks);

        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var msg = new TaskArrayArgs();

            foreach (var task in Filter(APF.Settings.Tasks.All))
            {
                if (player.OpenedTasks.Contains(task.Id))
                {
                    var taskArgs = new TaskArgs() { Id = task.Id };
                    taskArgs.Status = TaskStatus.Opened;
                    msg.Tasks.Add(taskArgs);
                }
                else if (player.LockedTasks.Contains(task.Id))
                {
                    if (task.Class == TaskClass.Main)
                    {
                        var taskArgs = new TaskArgs() { Id = task.Id };
                        taskArgs.Status = TaskStatus.Locked;
                        msg.Tasks.Add(taskArgs);
                    }
                }
                else if (player.ClosedTasks.Contains(task.Id))
                {
                    if (task.Class == TaskClass.Daily || task.Class == TaskClass.Achievement)
                    {
                        var taskArgs = new TaskArgs() { Id = task.Id };
                        taskArgs.Status = TaskStatus.Closed;
                        msg.Tasks.Add(taskArgs);
                    }
                }
                else if (player.ProcessTasks.ContainsKey(task.Id))
                {
                    var taskArgs = new TaskArgs() { Id = task.Id };
                    var taskPro = player.ProcessTasks[task.Id];
                    taskArgs.Status = taskPro.Finished ? TaskStatus.Finished : TaskStatus.Doing;
                    taskArgs.Progress = taskPro.Step;
                    msg.Tasks.Add(taskArgs);
                }
            }

            session.SendResponse(ID, msg);
        }
    }
}
