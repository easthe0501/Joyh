using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Core;
using System.ComponentModel.Composition;
using Action.Engine;

namespace Action.Bag.Strategy
{
    public class ItemStrategyFactory
    {
        private static ItemStrategyFactory _current = new ItemStrategyFactory();
        public static ItemStrategyFactory Current
        {
            get { return _current; }
        }

        [ImportMany]
        private IEnumerable<IItemStrategy> _strategies = null;

        private ItemStrategyFactory()
        {
            Composition.ComposeParts(this);
            foreach (var strategy in _strategies)
                _strategyHash[strategy.Type] = strategy;
        }

        private Dictionary<int, IItemStrategy> _strategyHash = new Dictionary<int, IItemStrategy>();
        public IItemStrategy GetStrategy(int type)
        {
            return _strategyHash.GetValue(type);
        }
    }

    public interface IItemStrategy
    {
        int Type { get; }
        bool Run(GameSession session, Item item);
    }
}
