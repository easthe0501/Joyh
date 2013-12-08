//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Action.Core;
//using Action.Engine;
//using Action.Model;

//namespace Action.Task.Command
//{
//    [GameCommand((int)CommandEnum.GiveUpTask)]
//    public class GiveUpTaskCommand : GameCommand<int>
//    {
//        protected override void Run(GameSession session, int args)
//        {
//            var player = session.Player.Data.AsDbPlayer();
//            if (player.ProcessTasks.ContainsKey(args))
//            {
//                player.ProcessTasks.Remove(args);
//                player.OpenedTasks.Add(args);
//                session.RefreshTask(new TaskArgs() { Id = args, Status = TaskStatus.Opened });
//            }
//            else
//                session.SendError(ErrorCode.TaskNotInProcess);
//        }
//    }
//}
