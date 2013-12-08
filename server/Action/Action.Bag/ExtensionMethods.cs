using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;
using Action.Model;

namespace Action.Bag
{
    public static class ExtensionMethods
    {
        public static Item CreateItem(this FactoryFacade factory, Player player, ItemType type, int settingId)
        {
            return type == ItemType.Equip ? factory.Create<Equip>(player, settingId) : factory.Create<Item>(player, settingId);
        }
    }
}
