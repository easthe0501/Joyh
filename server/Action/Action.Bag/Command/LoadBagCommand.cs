using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using System.ComponentModel.Composition;

namespace Action.Bag.Command
{
    [GameCommand((int)CommandEnum.LoadBag)]
    public class LoadBagCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var Bag = player.Bag;

            LoadBagArgs loadBagArgs = new LoadBagArgs();
            BagsArgs bagsArgs = new BagsArgs();
            foreach (Item ig in player.Bag.GoodsBag)
            {
                bagsArgs.GoodsBag.Add(new BagItemArgs() { Id = ig.Id, SortId = ig.SortId, Quantity = ig.Count, SettingId = ig.SettingId });
            }
            foreach (Item im in player.Bag.MaterialsBag)
            {
                bagsArgs.MaterialsBag.Add(new BagItemArgs() { Id = im.Id, SortId = im.SortId, Quantity = im.Count, SettingId = im.SettingId });
            }
            foreach (Item it in player.Bag.TempBag)
            {
                bagsArgs.TempBag.Add(new BagItemArgs() { Id = it.Id, SortId = it.SortId, Quantity = it.Count, SettingId = it.SettingId });
            }
            loadBagArgs.Bags = bagsArgs;
            loadBagArgs.GoodsBagSize = Bag.GoodsBagSize;
            loadBagArgs.MaterialsBagSize = Bag.MaterialsBagSize;
            loadBagArgs.Gold = player.Gold;
            loadBagArgs.Money = player.Money;

            session.SendResponse(ID, loadBagArgs);
        }
    }
}
