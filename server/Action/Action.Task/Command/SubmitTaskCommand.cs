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
    [GameCommand((int)CommandEnum.SubmitTask)]
    public class SubmitTaskCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session))
                return;
            var player = session.Player.Data.AsDbPlayer();
            if (player.ProcessTasks.ContainsKey(args))
            {
                var taskPro = player.ProcessTasks[args];
                if (taskPro.Finished)
                {
                    //获取奖励，关闭任务
                    var task = APF.Settings.Tasks.Find(taskPro.Id);
                    task.Prize.Open(session, task.GetPrizeSource());
                    player.CloseTask(task, session, true);
                }
                else
                    session.SendError(ErrorCode.TaskNotFinished);
            }
            else
                session.SendError(ErrorCode.TaskNotInProcess);
        }
    }
}
