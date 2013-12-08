using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;
using Action.Model;
using Action.Engine;

namespace Action.Task
{
    public static class TaskHelper
    {
        public static void UpdateTasks(this GameSession session, TaskArrayArgs args)
        {
            if(args.Tasks.Count > 0)
                session.SendResponse((int)CommandEnum.UpdateTasks, args);
        }

        public static void UpdateRandomTask(this GameSession session, RandomTaskArgs args)
        {
            session.SendResponse((int)CommandEnum.UpdateRandomTask, args);
        }

        public static TaskArrayArgs ToTaskArrayArgs(int taskId, TaskStatus status, int progress = 0)
        {
            var msg = new TaskArrayArgs();
            msg.Tasks.Add(new TaskArgs() { Id = taskId, Status = status, Progress = progress });
            return msg;
        }

        private static bool IsTaskFinished(Player player, TaskSetting task)
        {
            switch (task.Type)
            {
                case TaskType.UpgradeHome:
                    if (player.Home.Level >= task.Data_Int32)
                        return true;
                    break;
            }
            return false;
        }

        public static bool LockTask(this Player player, TaskSetting task, GameSession session, bool sendMsg)
        {
            if (player.LockedTasks.Contains(task.Id))
                return false;

            player.LockedTasks.Add(task.Id);
            player.OpenedTasks.Remove(task.Id);
            player.ProcessTasks.Remove(task.Id);
            player.ClosedTasks.Remove(task.Id);

            if(sendMsg)
                session.UpdateTasks(ToTaskArrayArgs(task.Id, TaskStatus.Locked));
            return true;
        }

        public static bool OpenTask(this Player player, TaskSetting task, GameSession session, bool sendMsg)
        {
            if (player.OpenedTasks.Contains(task.Id))
                return false;

            player.LockedTasks.Remove(task.Id);
            player.OpenedTasks.Add(task.Id);
            player.ProcessTasks.Remove(task.Id);
            player.ClosedTasks.Remove(task.Id);

            if(sendMsg)
                session.UpdateTasks(ToTaskArrayArgs(task.Id, TaskStatus.Opened));
            return true;
        }

        public static bool AcceptTask(this Player player, TaskSetting task, GameSession session, bool sendMsg)
        {
            var taskPro = player.ProcessTasks.GetValue(task.Id);
            if (taskPro == null)
            {
                player.LockedTasks.Remove(task.Id);
                player.OpenedTasks.Remove(task.Id);
                player.ProcessTasks.Add(task.Id, TaskProgress.Create(task.Id));
                player.ClosedTasks.Remove(task.Id);
            }
            else
            {
                taskPro.Step = 0;
                taskPro.Finished = false;
            }

            //解锁功能
            player.UnlockCommands(session, task.UnlockCommands);

            if(sendMsg)
                session.UpdateTasks(ToTaskArrayArgs(task.Id, TaskStatus.Doing));

            //检查是否已完成
            var finished = IsTaskFinished(player, task);
            if (finished)
                player.FinishTask(task, session, true);
            return true;
        }

        public static bool FinishTask(this Player player, TaskSetting task, GameSession session, bool sendMsg)
        {
            var taskPro = player.ProcessTasks.GetValue(task.Id);
            if (taskPro == null)
            {
                player.LockedTasks.Remove(task.Id);
                player.OpenedTasks.Remove(task.Id);
                player.ProcessTasks.Add(task.Id, TaskProgress.Create(task.Id, true, task.Data_Int32));
                player.ClosedTasks.Remove(task.Id);
            }
            else
            {
                taskPro.Step = task.Data_Int32;
                taskPro.Finished = true;
            }
            if(sendMsg)
                session.UpdateTasks(ToTaskArrayArgs(task.Id, TaskStatus.Finished, taskPro.Step));
            return true;
        }

        public static bool CloseTask(this Player player, TaskSetting task, GameSession session, bool sendMsg)
        {
            if (player.ClosedTasks.Contains(task.Id))
                return false;

            player.LockedTasks.Remove(task.Id);
            player.OpenedTasks.Remove(task.Id);
            player.ProcessTasks.Remove(task.Id);
            player.ClosedTasks.Add(task.Id);

            //激活后续任务
            var msg = ToTaskArrayArgs(task.Id, TaskStatus.Closed);
            if (task.FollowTasks != null && task.FollowTasks.Length > 0)
            {
                var result = player.UnlockTasks(task.FollowTasks, session, false);
                msg.Tasks.AddRange(result.Tasks);
            }
            if(sendMsg)
                session.UpdateTasks(msg);
            return true;
        }

        public static TaskArrayArgs UnlockTasks(this Player player, IEnumerable<TaskSetting> tasks, GameSession session, bool sendMsg)
        {
            var msg = new TaskArrayArgs();
            foreach (var task in tasks.ToArray())  //这边加ToArray是为了避免在循环中修改集合
            {
                var locked = player.LockedTasks.Contains(task.Id);
                if (player.Level >= task.Level)
                {
                    if (task.Class == TaskClass.Main)
                    {
                        player.AcceptTask(task, session, false);
                        msg.Tasks.Add(TaskProgress.Create(task.Id).ToTaskArgs());
                    }
                    else
                    {
                        player.OpenedTasks.Add(task.Id);
                        msg.Tasks.Add(new TaskArgs() { Id = task.Id, Status = TaskStatus.Opened });
                    }
                    if (locked)
                        player.LockedTasks.Remove(task.Id);
                }
                else
                {
                    if (!locked)
                    {
                        player.LockedTasks.Add(task.Id);
                        msg.Tasks.Add(new TaskArgs() { Id = task.Id, Status = TaskStatus.Locked });
                    }
                }
            }
            if (sendMsg && msg.Tasks.Count > 0)
                session.UpdateTasks(msg);
            return msg;
        }

        public static TaskArrayArgs UnlockTasks(this Player player, IEnumerable<int> taskIds, GameSession session, bool sendMsg)
        {
            return UnlockTasks(player, taskIds.Select(id => APF.Settings.Tasks.Find(id)), session, sendMsg);
        }

        public static void RefreshRandomTask(this Player player)
        {
            if (player.RandomTask.Index < APF.Settings.Role.DailyRandomTaskCount)
            {
                var task = APF.Settings.RandomTasks.All.Where(t => t.PreviousTask == 0
                    || player.ClosedTasks.Contains(t.PreviousTask)).Random();
                player.RandomTask.Accept(task.Id);
            }
            else
                player.RandomTask.SetEmpty();
        }

        public static RandomTaskArgs ToRandomTaskArgs(this TaskArgs args, int index)
        {
            return new RandomTaskArgs()
            {
                Id = args.Id,
                Progress = args.Progress,
                Status = args.Status,
                Index = index
            };
        }
    }
}
