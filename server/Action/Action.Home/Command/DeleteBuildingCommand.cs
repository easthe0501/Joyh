using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using System.ComponentModel.Composition;

namespace Action.Home.Command
{
    [GameCommand((int)CommandEnum.DeleteBuilding)]
    public class DeleteBuildingCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            //判断背包是否满
            if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session))
                return;

            var player = session.Player.Data.AsDbPlayer();
            Building building = player.Home.Buildings.SingleOrDefault(p => p.Id == args);

            //判断building是否为玩家已创建的建筑
            if (building == null)
                return;

            //判断房屋是否被固定
            if (building.Fixed)
            {
                session.SendError(ErrorCode.BuildingFixed);
                return;
            }

            DelBuildingArgs delBuilding = new DelBuildingArgs();
            delBuilding.BuildingId = building.Id;
            //返材料和钱
            if (building.Setting.Consumable.Money != 0)
            {
                //if ((DateTime.Now - building.CreateTime).TotalSeconds <= APF.Settings.Home.FreeDeleteSec)
                //    player.Money += building.Setting.Consumable.Money;
                //else
                player.Money += (int)(building.Setting.Consumable.Money * APF.Settings.Home.ReturnMoneyRate);
                delBuilding.Money = (int)(building.Setting.Consumable.Money * APF.Settings.Home.ReturnMoneyRate);
                session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            }
            if (building.Setting.Consumable.Materials != null)
            {
                foreach (IdCountPair idcp in building.Setting.Consumable.Materials)
                {
                    int count = (int)(idcp.Count * APF.Settings.Home.ReturnMaterialRate);
                    delBuilding.Materials.Add(new MaterialsArgs() { SettingId = idcp.Id, Count = count });
                    if (!session.Server.ModuleFactory.Module<IBagModule>().AddItem(session, idcp.Id, count))
                        return;
                }
            }

            PropertiesArgs properties = new PropertiesArgs();
            //扣除建筑增益
            for (int i = 0; i < player.Home.Properties.Length; i++)
            {
                if (player.Home.Properties[i] - building.Setting.Product.Properties[i] < 0)
                {
                    player.Home.Properties[i] = 0;
                    continue;
                }
                player.Home.Properties[i] -= building.Setting.Product.Properties[i];
                properties.Properties.Add(player.Home.Properties[i]);
            }

            player.Home.Buildings.Remove(building);
            session.SendResponse(ID, delBuilding);
            session.SendResponse((int)CommandEnum.RefreshProperties, properties);

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.DeleteBuilding, building.SettingId);
        }
     
    }
}
