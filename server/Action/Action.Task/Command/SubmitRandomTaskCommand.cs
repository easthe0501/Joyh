using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Task.Command
{
    [GameCommand((int)CommandEnum.SubmitRandomTask)]
    public class SubmitRandomTaskCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            if (player.RandomTask.Status != TaskStatus.Finished)
            {
                session.SendError(ErrorCode.TaskNotFinished);
                return;
            }

            //打开奖励
            var prize = APF.Settings.FixedPrizes.Find(player.RandomTask.Index).Prize;
            prize.Open(session, PrizeSource.Task, true);

            //计数增加
            player.RandomTask.Index++;

            //刷新下一个任务
            player.RefreshRandomTask();
            session.UpdateRandomTask(player.RandomTask.ToArgs());
        }
    }
}
