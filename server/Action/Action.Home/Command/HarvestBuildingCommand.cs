using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Model;
using Action.Engine;

namespace Action.Home.Command
{
    [GameCommand((int)CommandEnum.HarvestBuilding)]
    public class HarvestBuildingCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            //判断背包是否满
            if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session))
                return;

            var player = session.Player.Data.AsDbPlayer();
            Building building = player.Home.Buildings.SingleOrDefault(p => p.Id == args);
            if (building == null)
                return;
            if (!building.Setting.IfProduct)
                return;

            if ((DateTime.Now - building.AcquireTime).TotalMinutes < building.Setting.Product.SpendTime)
            {
                session.SendError(ErrorCode.BuildingCannotHarvest);
                return;
            }


            //收获基础材料
            if (building.Setting.Product.BaseMaterial != null)
                if (!session.Server.ModuleFactory.Module<IBagModule>().AddItem(session, building.Setting.Product.BaseMaterial.Id, building.Setting.Product.BaseMaterial.Count))
                    return;
            //收获高级材料
            if (APF.Random.Percent(building.Setting.Product.SuperPercent) && building.Setting.Product.SuperMaterial != null)
            {
                if (!session.Server.ModuleFactory.Module<IBagModule>().AddItem(session, building.Setting.Product.SuperMaterial.Id, building.Setting.Product.SuperMaterial.Count))
                    return;
            }


            //收获钱
            if (building.Setting.Product.Money != 0)
            {
                player.Money += building.Setting.Product.Money;
                session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            }
            building.AcquireTime = DateTime.Now;
            int timeRest = (int)(building.Setting.Product.SpendTime * 60);

            session.SendResponse(ID, new HarvestSuccessArgs { Id = building.Id, TimeRest = timeRest });

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.HarvestBuilding, building.SettingId);
        }
    }
}
