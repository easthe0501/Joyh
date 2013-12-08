using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;
using Action.Model;

namespace Action.Copy.Strategy
{
    public static class CopyGridStrategyFactory
    {
        private static Dictionary<GridType, ICopyGridStrategy> _hash;
        static CopyGridStrategyFactory()
        {
            _hash = new Dictionary<GridType, ICopyGridStrategy>();
            _hash[GridType.Battle] = new BattleGridStrategy();
            _hash[GridType.Prize] = new PrizeGridStrategy();
            _hash[GridType.Random] = new RandomGridStrategy();
            _hash[GridType.Card] = new CardGridStrategy();
            _hash[GridType.Meeting] = new MeetingGridStrategy();
        }

        public static ICopyGridStrategy GetStrategy(GridType effect)
        {
            return _hash.GetValue(effect);
        }
    }
}
