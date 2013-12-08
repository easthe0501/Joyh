using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Equip.Command
{
    [GameCommand((int)CommandEnum.UnwearEquip)]
    public class UnWearEquipCommand : GameCommand<HeroEquipArgs>
    {
        protected override void Run(GameSession session, HeroEquipArgs args)
        {
            //判断物品背包是否满
            if (session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session, BagType.GoodBag))
                return;

            var player = session.Player.Data.AsDbPlayer();
            var hero = player.Heros.SingleOrDefault(p => p.Id == args.HeroId);
            if (hero == null)
                return;
            var equip = hero.Equips.SingleOrDefault(p => p.Id == args.EquipId);
            if (equip == null)
                return;

            for (int i = 0; i <= player.Bag.GoodsBag.Count; i++)
            {
                if (player.Bag.GoodsBag.SingleOrDefault(p => p.SortId == i) == null)
                {
                    equip.SortId = i;
                    break;
                }
            }
            equip.Owner = null;
            player.Bag.GoodsBag.Add(equip);
            hero.Equips.Remove(equip);
            hero.Refresh();
            session.SendResponse(ID, args);
            BagItemCollectionArgs bagItemCollectionArgs = new BagItemCollectionArgs();
            bagItemCollectionArgs.Items.Add(new BagItemArgs() 
            { 
                Id = equip.Id, 
                SortId = equip.SortId, 
                SettingId = equip.SettingId,
                Quantity = equip.Count, 
                WhichBag = BagType.GoodBag
            });
            session.SendResponse((int)CommandEnum.ChangeItem, bagItemCollectionArgs);
        }
    }
}
