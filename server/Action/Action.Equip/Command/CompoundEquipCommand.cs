using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Equip.Command
{
    [GameCommand((int)CommandEnum.CompoundEquip)]
    public class CompoundEquipCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            var player = session.Player.Data.AsDbPlayer();
            var equip = player.Snapshot.Find<Model.Equip>(args);
            if (equip == null)
                return;

            if (player.Level < equip.EquipCompoundSetting.Requirement.Level)
            {
                session.SendError(ErrorCode.LevelNotEnough);
                return;
            }

            //判断钱是否充足
            if (player.Money < equip.EquipCompoundSetting.Consumable.Money)
            {
                session.SendError(ErrorCode.MoneyNotEnough);
                return;
            }

            //消耗材料和钱
            if (!session.Server.ModuleFactory.Module<IBagModule>().ConsumeItem(session, equip.EquipCompoundSetting.Consumable.Materials))
                return;
            if (equip.EquipCompoundSetting.Consumable.Money != 0)
            {
                player.Money -= equip.EquipCompoundSetting.Consumable.Money;
                session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            }

            //合成成功几率
            bool flag = false;
            var luckySetting = equip.EquipCompoundSetting.Lucky;
            if (equip.CompoundLucky >= equip.EquipCompoundSetting.Lucky.BasicVal)
            {
                int pct;
                pct = (equip.CompoundLucky - luckySetting.BasicVal) /
                     (luckySetting.MaxVal - luckySetting.BasicVal) *
                     (luckySetting.MaxPct - luckySetting.BasicPct) +
                     luckySetting.BasicPct;
                if (APF.Random.Percent(pct))
                    flag = true;
            }
            if (!flag)
            {
                int lucky = APF.Random.Range(luckySetting.MinPlus, luckySetting.MaxPlus);
                equip.CompoundLucky += lucky;
                session.SendResponse(ID, new CompoundEquipArgs() { Flag = false, EquipId = equip.Id, TargetId = equip.SettingId, CompoundLucky = equip.CompoundLucky });
                return;
            }
            equip.CompoundLucky = 0;

            int costSum = equip.StrengthenCostSum();
            equip.SettingId = equip.EquipCompoundSetting.TargetId;
            //强化等级
            equip.Level = 1;
            while (costSum > equip.GetEquipStrenthenCost())
            {
                costSum -= equip.GetEquipStrenthenCost();
                equip.Level += 1;
            }
            
            equip.Refresh();
            if (equip.Owner != null)
            {
                equip.Owner.Refresh();
            }
            session.SendResponse(ID, new CompoundEquipArgs() { Flag = true, EquipId = args, TargetId = equip.SettingId, CompoundLucky = 0 });

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.CompoundEquip, equip.SettingId);
        }
    }
}
