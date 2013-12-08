using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Utility;
using Action.Model;

namespace Action.Bag.Strategy
{
    [Export(typeof(IItemStrategy))]
    public class BoxStrategy : IItemStrategy
    {
        public int Type
        {
            get { return (int)ItemType.GiftBag; }
        }

        public bool Run(Engine.GameSession session, Model.Item item)
        {
            var self = session.Player.Data.AsDbPlayer();
            var prize = JsonHelper.FromJson<Prize>(item.Setting.Data);
            prize.Open(session, PrizeSource.Box);
            return true;
        }
    }
}
