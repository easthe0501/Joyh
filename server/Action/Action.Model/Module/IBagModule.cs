using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;

namespace Action.Model
{
    public interface IBagModule : IGameModule
    {
        bool AddItem(GameSession session, int itemId, int itemCount);

        bool IfBagFull(GameSession session, BagType whichBag = BagType.AllBag);

        bool ConsumeItem(GameSession session, IdCountPair[] idcps);
    }
}
