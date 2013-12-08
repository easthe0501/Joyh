using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;

namespace Action.Model
{
    public interface ITaskModule : IGameModule
    {
        void OnDayPast(GamePlayer gPlayer, Player dbPlayer);
        void OnEventHandled(GamePlayer gPlayer, Player dbPlayer, TaskType taskType, object taskData);
        bool LockTask(GamePlayer player, TaskSetting task);
        bool OpenTask(GamePlayer player, TaskSetting task);
        bool AcceptTask(GamePlayer player, TaskSetting task);
        bool FinishTask(GamePlayer player, TaskSetting task);
        bool CloseTask(GamePlayer player, TaskSetting task);
    }
}
