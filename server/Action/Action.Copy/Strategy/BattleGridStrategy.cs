using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;
using Action.Core;

namespace Action.Copy.Strategy
{
    public class BattleGridStrategy : ICopyGridStrategy
    {
        public CopyGridArgs Run(GameSession session, CopyMember member, object data)
        {
            var battleSetting = APF.Settings.Battles.Find(MyConvert.ToInt32(data));
            var player = member.Player;
            player.Temp.BattleReport = session.Server.ModuleFactory
                .Module<IBattleModule>().PVE(player, battleSetting);

            if (player.Temp.BattleReport.Win)
            {
                player.Temp.CopyGridPrize = battleSetting.WinnerPrize;

                //如果到达终点，离开副本并解锁下一副本
                if (member.CurrentGrid.Style == GridStyle.Boss)
                    session.PassCopy();

                //任务事件
                var taskModule = session.Server.ModuleFactory.Module<ITaskModule>();
                var idCounts = battleSetting.ToMonsterCounts();
                foreach (var idCount in idCounts)
                    taskModule.OnEventHandled(session.Player, player, TaskType.KillMonster, idCount);
            }
            else
                player.Temp.CopyGridPrize = battleSetting.LoserPrize;

            return member.CurrentGrid.ToArgs();
        }
    }
}
