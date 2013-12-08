using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Core;
using Action.Engine;
using Action.Model;
using System.Diagnostics;

namespace Action.Task
{
    [Export(typeof(IGameModule))]
    public class TaskModule : GameModule, ITaskModule
    {
        public override void Load(GameWorld world)
        {
            var followTasks = new HashSet<int>();
            foreach (var task in APF.Settings.Tasks.All)
            {
                if (task.FollowTasks != null)
                {
                    foreach (var followTask in task.FollowTasks)
                        followTasks.Add(followTask);
                }
                //任务奖励物品验证
                string taskId = task.Id.ToString();
                Trace.Assert(task.Prize != null, taskId);
                if (task.Prize.Items != null)
                {
                    foreach (var item in task.Prize.Items)
                        Trace.Assert(APF.Settings.Items.Find(item.Id) != null, taskId);
                }
                //任务数据验证
                if (task.Type == TaskType.CollectItem)
                    Trace.Assert(APF.Settings.Items.Find(task.Data_IdCountPair.Id) != null, taskId);
            }
            foreach (var taskId in followTasks)
            {
                var task = APF.Settings.Tasks.Find(taskId);
                Trace.Assert(task != null, string.Format("Task {0} missing.", taskId));
                Trace.Assert(task.PreviousTask > 0, string.Format("Task {0} has previousTask.", taskId));
            }
        }

        public override void CreateRole(GamePlayer player)
        {
            var dbPlayer = player.Data.AsDbPlayer();
            var taskItems = APF.Settings.Tasks.All;

            //激活所有成就任务
            foreach (var task in taskItems.Where(t => t.Class == TaskClass.Achievement))
                dbPlayer.AcceptTask(task, player.Session, false);

            //激活1级主线任务
            var firstTask = taskItems.Where(t => t.Class == TaskClass.Main)
                .OrderBy(t => t.Level).FirstOrDefault();
            dbPlayer.AcceptTask(firstTask, player.Session, false);

            //激活无前置任务的支线任务
            var sbTasks = taskItems.Where(t => t.Class == TaskClass.Branch && t.PreviousTask == 0);
            dbPlayer.UnlockTasks(sbTasks, player.Session, false);
        }

        /// <summary>
        /// 每晚0点整，刷新日常任务并全部自动接取，同时随机接取一个随机任务
        /// </summary>
        public void OnDayPast(GamePlayer gPlayer, Player dbPlayer)
        {
            foreach (var task in APF.Settings.Tasks.All.Where(t => t.Class == TaskClass.Daily))
            {
                if (dbPlayer.Level >= task.Level)
                    dbPlayer.AcceptTask(task, gPlayer.Session, false);
            }

            //随机接取随机任务
            dbPlayer.RandomTask.Index = 0;
            dbPlayer.RefreshRandomTask();
            gPlayer.Session.UpdateRandomTask(dbPlayer.RandomTask.ToArgs());
        }

        /// <summary>
        /// 任务进度改变
        /// </summary>
        /// <param name="gPlayer"></param>
        /// <param name="dbPlayer"></param>
        /// <param name="taskType"></param>
        /// <param name="taskData"></param>
        public void OnEventHandled(GamePlayer gPlayer, Player dbPlayer, TaskType taskType, object taskData)
        {
            //任务事件响应
            var msg = new TaskArrayArgs();
            var taskPros = dbPlayer.ProcessTasks.Values.Where(t => !t.Finished);
            foreach (var taskPro in taskPros)
            {
                var taskArgs = HandleEventInProgress(taskPro, taskType, taskData, false);
                if (taskArgs != null)
                    msg.Tasks.Add(taskArgs);
            }
            gPlayer.Session.UpdateTasks(msg);

            //随机任务事件响应
            if (dbPlayer.RandomTask.Status == TaskStatus.Doing)
            {
                var rMsg = HandleEventInProgress(dbPlayer.RandomTask.Progress, taskType, taskData, true);
                if (rMsg != null)
                    gPlayer.Session.UpdateRandomTask(rMsg.ToRandomTaskArgs(dbPlayer.RandomTask.Index));
            }
        }

        private TaskArgs HandleEventInProgress(TaskProgress taskPro, TaskType taskType, object taskData, bool isRandomTask)
        {
            TaskSettingBase task = null;
            if (isRandomTask)
                task = APF.Settings.RandomTasks.Find(taskPro.Id);
            else
                task = APF.Settings.Tasks.Find(taskPro.Id);
            if (task.Type == taskType)
            {
                switch (taskType)
                {
                    case TaskType.CollectItem:
                    case TaskType.KillMonster:
                        var idCount = taskData as IdCountPair;
                        if (idCount != null && idCount.Id == task.Data_IdCountPair.Id)
                        {
                            taskPro.Step += idCount.Count;
                            if (taskPro.Step >= task.Data_IdCountPair.Count)
                                taskPro.Finished = true;
                        }
                        else
                            return null;
                        break;
                    case TaskType.GoldBuyMoney:
                    case TaskType.AddFriend:
                        taskPro.Step++;
                        if (taskPro.Step >= task.Data_Int32)
                            taskPro.Finished = true;
                        else
                            return null;
                        break;
                    case TaskType.Embattle:
                    case TaskType.JoinGuild:
                        taskPro.Finished = true;
                        break;
                    case TaskType.UpgradeHome:
                        if (MyConvert.ToInt32(taskData) >= task.Data_Int32)
                            taskPro.Finished = true;
                        else
                            return null;
                        break;
                    default:
                        var nTaskData = MyConvert.ToInt32(taskData);
                        if (task.Data_Int32 == 0 || nTaskData == task.Data_Int32)
                            taskPro.Finished = true;
                        else
                            return null;
                        break;
                }
                return taskPro.ToTaskArgs();
            }
            return null;
        }

        public override void LevelUp(GamePlayer player, int oldLevel)
        {
            var dbPlayer = player.Data.AsDbPlayer();
            if (dbPlayer.LockedTasks.Count > 0)
                dbPlayer.UnlockTasks(dbPlayer.LockedTasks, player.Session, true);
        }

        public bool LockTask(GamePlayer player, TaskSetting task)
        {
            return player.Data.AsDbPlayer().LockTask(task, player.Session, true);
        }

        public bool OpenTask(GamePlayer player, TaskSetting task)
        {
            return player.Data.AsDbPlayer().OpenTask(task, player.Session, true);
        }

        public bool AcceptTask(GamePlayer player, TaskSetting task)
        {
            return player.Data.AsDbPlayer().AcceptTask(task, player.Session, true);
        }

        public bool FinishTask(GamePlayer player, TaskSetting task)
        {
            return player.Data.AsDbPlayer().FinishTask(task, player.Session, true);
        }

        public bool CloseTask(GamePlayer player, TaskSetting task)
        {
            return player.Data.AsDbPlayer().CloseTask(task, player.Session, true);
        }
    }
}
