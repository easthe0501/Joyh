using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;
using Action.Core;

namespace Action.Copy.Strategy
{
    public class PrizeGridStrategy : ICopyGridStrategy
    {
        public CopyGridArgs Run(GameSession session, CopyMember member, object data)
        {
            member.Player.Temp.CopyGridPrize = data as Prize;
            return member.CurrentGrid.ToArgs();
        }
    }
}
