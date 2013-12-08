//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel.Composition;
//using Action.Engine;
//using Action.Model;

//namespace Action.Task.Command
//{
//    //[GameCommand((int)CommandEnum.LoadMainBranchTasks)]
//    public class LoadMainBranchTasksCommand : GameCommand
//    {
//        protected override void Run(GameSession session)
//        {
//            var player = session.Player.Data.AsDbPlayer();
//            var msg = new TaskArrayArgs();

//            //已完成任务、正在处理的任务
//            foreach (var taskPro in player.ProcessTasks.Values)
//            {
//                var task = APF.Settings.Tasks.GetItem(taskPro.Id);
//                if (task.Class == TaskClass.Main || task.Class == TaskClass.Branch)
//                {
//                    msg.Tasks.Add(new TaskArgs()
//                    {
//                        Id = taskPro.Id,
//                        Progress = taskPro.Step,
//                        Status = taskPro.Finished ? TaskStatus.Finished : TaskStatus.Doing
//                    });
//                }
//            }

//            //可接任务
//            foreach (var taskId in player.OpenedTasks)
//            {
//                var task = APF.Settings.Tasks.GetItem(taskId);
//                if (task.Class == TaskClass.Main || task.Class == TaskClass.Branch)
//                    msg.Tasks.Add(new TaskArgs() { Id = taskId, Status = TaskStatus.Opened });
//            }

//            //未解锁任务
//            foreach (var taskId in player.LockedTasks)
//            {
//                var task = APF.Settings.Tasks.GetItem(taskId);
//                if (task.Class == TaskClass.Main || task.Class == TaskClass.Branch)
//                    msg.Tasks.Add(new TaskArgs() { Id = taskId, Status = TaskStatus.Locked });
//            }

//            session.SendResponse(ID, msg);
//        }
//    }
//}
