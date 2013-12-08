using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Model;
using Action.Core;

namespace Action.Bag.Strategy
{
    [Export(typeof(IItemStrategy))]
    public class HeroTokenStrategy : IItemStrategy
    {
        public int Type
        {
            get { return (int)ItemType.HeroToken; }
        }

        public bool Run(Engine.GameSession session, Model.Item item)
        {
            var heroId = MyConvert.ToInt32(item.Setting.Data);
            var permission = session.Player.Data.AsDbPlayer().Permission;
            if (!permission.Commands.Contains((int)CommandEnum.InviteHero))
            {
                session.SendError(ErrorCode.HeroShopNotOpen);
                return false;
            }
            if (permission.Heros.Contains(heroId))
            {
                session.SendError(ErrorCode.HeroRepeated);
                return false;
            }
            permission.Heros.Add(heroId);
            return true;
        }
    }
}
