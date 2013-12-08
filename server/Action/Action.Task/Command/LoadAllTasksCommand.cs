using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Task.Command
{
    [GameCommand((int)CommandEnum.LoadAllTasks)]
    public class LoadAllTasksCommand : LoadTasksCommand
    {
        protected override IEnumerable<TaskSetting> Filter(IEnumerable<TaskSetting> tasks)
        {
            return tasks;
        }
    }
}
