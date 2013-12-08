using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;
using Action.Core;

namespace Action.Copy.Strategy
{
    public class RandomGridStrategy : ICopyGridStrategy
    {
        public CopyGridArgs Run(GameSession session, CopyMember member, object data)
        {
            var copyInstance = member.Instance;
            var copySetting = copyInstance.Setting;
            var currentGrid = copyInstance.Grids[member.Pos];
            currentGrid.Update(copySetting, (GridStyle)APF.Random.Range(1, 4));
            return Strategy.CopyGridStrategyFactory.GetStrategy(currentGrid.Type)
                .Run(session, member, currentGrid.Data);
        }
    }
}
