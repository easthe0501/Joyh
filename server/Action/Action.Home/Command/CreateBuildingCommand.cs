using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using System.ComponentModel.Composition;
using System.Drawing;

namespace Action.Home.Command
{
    [GameCommand((int)CommandEnum.CreateBuilding)]
    public class CreateBuildingCommand : GameCommand<CreateBuildingArgs>
    {
        protected override void Run(GameSession session, CreateBuildingArgs args)
        {
            var player = session.Player.Data.AsDbPlayer();
            //创建建筑
            var building = player.Home.CreateBuilding(player, args.SettingId);
            building.X = args.X;
            building.Y = args.Y;
            
            //判断建筑物的配置是否正确
            if (building.Setting == null)
            {
                session.SendError(ErrorCode.BuildingSettingIsWrong);
                return;
            }
            if (!building.Setting.IfCanBuild)
                return;

            //判断需求是否满足要求
            if (player.Home.Level < building.Setting.Requirement.Level)
            {
                session.SendError(ErrorCode.HomeLevelNotEnough);
                return;
            }
            if (player.Repute < building.Setting.Requirement.Repute)
            {
                session.SendError(ErrorCode.ReputeNotEnough);
                return;
            }

            //判断建筑物是否超过边界
            if (!player.Home.GetBounds().Contains(building.GetBounds()))
            {
                session.SendError(ErrorCode.BuildingOutBorder);
                return;
            }

            //判断是否重叠
            List<Building> buildings = player.Home.Buildings;
            foreach (Building b in buildings)
            {
                if (b.Intersect(building))
                {
                    session.SendError(ErrorCode.BuildingIntersect);
                    return;
                }
            }

            //判断钱是否充足
            if (player.Money < building.Setting.Consumable.Money)
            {
                session.SendError(ErrorCode.MoneyNotEnough);
                return;
            }
            if (player.Gold < building.Setting.Consumable.Gold)
            {
                session.SendError(ErrorCode.GoldNotEnough);
                return;
            }
            
            //判断家园属性是否满足
            for (int i = 0; i < player.Home.Properties.Length; i++)
            {
                if (player.Home.Properties[i] < building.Setting.Requirement.Properties[i])
                {
                    session.SendError(ErrorCode.HomePropertiesNotEnough);
                    return;
                }
            }

            //消耗钱和材料
            if (building.Setting.Consumable.Materials != null)
            {
                if (!session.Server.ModuleFactory.Module<IBagModule>().ConsumeItem(session, building.Setting.Consumable.Materials))
                    return;
            }
            if (building.Setting.Consumable.Money != 0)
            {
                player.Money -= building.Setting.Consumable.Money;
                session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            }
            if (building.Setting.Consumable.Gold != 0)
            {
                player.Gold -= building.Setting.Consumable.Gold;
                session.SendResponse((int)CommandEnum.RefreshGold, player.Gold);
            }

            PropertiesArgs properties = new PropertiesArgs();
            //建筑增加增益
            for (int i = 0; i < player.Home.Properties.Length; i++)
            {
                player.Home.Properties[i] += building.Setting.Product.Properties[i];
                properties.Properties.Add(player.Home.Properties[i]);
            }

            //保存建筑
            int timeRest = (int)(building.Setting.Product.SpendTime * 60);
            player.Home.Buildings.Add(building);
            CreateReturnArgs createBuilding = new CreateReturnArgs();
            createBuilding.Building =new BuildingArgs() { Id = building.Id, X = building.X, Y = building.Y, SettingId = building.SettingId, TimeRest = timeRest };
            foreach (var m in building.Setting.Consumable.Materials)
            {
                createBuilding.Materials.Add(new MaterialsArgs() { SettingId = m.Id, Count = m.Count });
            }
            createBuilding.Money = building.Setting.Consumable.Money;
            session.SendResponse(ID, createBuilding);
            session.SendResponse((int)CommandEnum.RefreshProperties, properties);

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.CreateBuilding, building.SettingId);
        }
    }
}
