using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Equip.Command
{
    [GameCommand((int)CommandEnum.WearEquip)]
    public class WearEquipCommand : GameCommand<HeroEquipArgs>
    {
        protected override void Run(GameSession session, HeroEquipArgs args)
        {
            //args.Arg1英雄Id，args.Arg2装备Id
            var player = session.Player.Data.AsDbPlayer();
            var hero = player.Heros.SingleOrDefault(p => p.Id == args.HeroId);
            if (hero == null)
                return;
            var equip = player.Bag.GoodsBag.SingleOrDefault(e => e.Id == args.EquipId) as Model.Equip;
            if (equip == null)
                return;
            //if (!equip.Setting.SuitedJobs.Contains(hero.Setting.JobId))
            //{
            //    session.SendError(ErrorCode.JobNotMatch);
            //    return;
            //}

            //if (equip.Level > hero.Level)
            //{
            //    session.SendError(ErrorCode.HeroLevelNotEnough);
            //    return;
            //}

            //有装备，替换/无装备，穿上
            BagItemCollectionArgs bagItemCollectionArgs = new BagItemCollectionArgs();
            bagItemCollectionArgs.Items.Add(new BagItemArgs() { Id = equip.Id, SortId = -1, SettingId = equip.SettingId, Quantity = 0, WhichBag = BagType.GoodBag });
            var bodyEquip = hero.Equips.SingleOrDefault(p => p.Setting.Pos == equip.Setting.Pos);
            if (bodyEquip != null)
            {
                int tempSortId = equip.SortId;
                bodyEquip.SortId = tempSortId;
                bodyEquip.Owner = null;
                hero.Equips.Remove(bodyEquip);
                player.Bag.GoodsBag.Add(bodyEquip);
                bagItemCollectionArgs.Items.Add(new BagItemArgs() 
                { 
                    Id = bodyEquip.Id, 
                    SortId = bodyEquip.SortId, 
                    SettingId = bodyEquip.SettingId,
                    Quantity = bodyEquip.Count, 
                    WhichBag = BagType.GoodBag 
                });
            }
            equip.Owner = hero;
            hero.Equips.Add(equip);
            player.Bag.GoodsBag.Remove(equip);
            hero.Refresh();
            session.SendResponse(ID, new WearEquipArgs() { HeroId = args.HeroId, EquipId = args.EquipId, Pos = (int)equip.Setting.Pos });
            session.SendResponse((int)CommandEnum.ChangeItem, bagItemCollectionArgs);

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.WearEquip, equip.SettingId);
        }
    }
}
