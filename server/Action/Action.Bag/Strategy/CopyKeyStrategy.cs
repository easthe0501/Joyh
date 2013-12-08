using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Utility;
using Action.Model;
using Action.Core;

namespace Action.Bag.Strategy
{
    [Export(typeof(IItemStrategy))]
    public class CopyKeyStrategy : IItemStrategy
    {
        public int Type
        {
            get { return (int)ItemType.CopyKey; }
        }

        public bool Run(Engine.GameSession session, Model.Item item)
        {
            var copyId = MyConvert.ToInt32(item.Setting.Data);
            session.Player.Data.AsDbPlayer().Permission.Copies.Add(copyId);
            return true;
        }
    }
}
