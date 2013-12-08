using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;

namespace Action.Model
{
    public interface IPrizeModule : IGameModule
    {
        void NoticePrizes(GamePlayer player);
        bool AddPrize(GameWorld world, string playerName, string prizeJson);
        bool AddPrize(GameWorld world, string playerName, Prize prize);
        bool AddPrize(GameWorld world, PlayerSummary playerSum, Prize prize);
        int AddPrize(GameWorld world, IEnumerable<PlayerSummary> summaries, Prize prize);
    }
}
