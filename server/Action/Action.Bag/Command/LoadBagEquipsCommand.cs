using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;

namespace Action.Bag.Command
{
    [GameCommand((int)CommandEnum.LoadBagEquips)]
    public class LoadBagEquipsCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            List<Item> equips = player.Bag.GoodsBag.FindAll(p => p.Setting.Type == ItemType.Equip);
            BagEquipsArgs bagEquips = new BagEquipsArgs();
            foreach (Item i in equips)
            {
                bagEquips.Equips.Add(new BagItemArgs() { Id = i.Id, SortId = i.SortId, Quantity = i.Count, SettingId = i.SettingId });
            }

            session.SendResponse(ID, bagEquips);
        }
    }
}
