using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;

namespace Action.Model
{
    public interface IHeroModule : IGameModule
    {
        void UnlockHeros(GameSession session, Player player, params int[] heroIds);
    }
}
