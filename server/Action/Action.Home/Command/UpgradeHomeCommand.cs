using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Model;
using Action.Engine;

namespace Action.Home.Command
{
    [GameCommand((int)CommandEnum.UpgradeHome)]
    public class UpgradeHomeCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            if (player.Home.Level >= APF.Settings.Home.LevelToplimit)
            {
                session.SendError(ErrorCode.HomeLevelLimited);
                return;
            }

            var upgradeSetting = APF.Settings.UpdateHomes.Find(player.Home.Level + 1);

            //声望限制
            if (player.Repute < upgradeSetting.Repute)
            {
                session.SendError(ErrorCode.ReputeNotEnough);
                return;
            }

            //六大属性限制
            for (int i = 0; i < player.Home.Properties.Length; i++)
            {
                if (player.Home.Properties[i] < upgradeSetting.Properties[i])
                {
                    session.SendError(ErrorCode.HomePropertiesNotEnough);
                    return;
                }
            }

            //花费钱
            if (player.Money < upgradeSetting.Money)
            {
                session.SendError(ErrorCode.MoneyNotEnough);
                return;
            }

            //家园范围扩大
            player.Home.MinX -= APF.Settings.Home.DefaultExpandSize;
            player.Home.MinY -= APF.Settings.Home.DefaultExpandSize;
            player.Home.MaxX += APF.Settings.Home.DefaultExpandSize;
            player.Home.MaxY += APF.Settings.Home.DefaultExpandSize;

            //家园等级+1
            player.Home.Level += 1;

            UpgradeSuccessArgs upgradeSuccessArgs = new UpgradeSuccessArgs();
            PropertiesArgs properties = new PropertiesArgs();
            foreach (int i in player.Home.Properties)
                properties.Properties.Add(i);
            upgradeSuccessArgs.HomeLevel = player.Home.Level;
            upgradeSuccessArgs.MinX = player.Home.MinX;
            upgradeSuccessArgs.MinY = player.Home.MinY;
            upgradeSuccessArgs.MaxX = player.Home.MaxX;
            upgradeSuccessArgs.MaxY = player.Home.MaxY;

            player.Money -= upgradeSetting.Money;
            session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            session.SendResponse((int)CommandEnum.RefreshProperties, properties);
            session.SendResponse(ID, upgradeSuccessArgs);

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.UpgradeHome, player.Home.Level);
        }
    }
}
