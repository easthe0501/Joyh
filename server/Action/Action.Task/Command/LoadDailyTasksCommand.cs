//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel.Composition;
//using Action.Core;
//using Action.Engine;
//using Action.Model;

//namespace Action.Task.Command
//{
//    //[GameCommand((int)CommandEnum.LoadDailyTasks)]
//    public class LoadDailyTasksCommand : LoadTasksCommand
//    {
//        protected override IEnumerable<TaskSetting> Filter(IEnumerable<TaskSetting> tasks)
//        {
//            return tasks.Where(t => t.Class == TaskClass.Daily);
//        }
//    }
//}
