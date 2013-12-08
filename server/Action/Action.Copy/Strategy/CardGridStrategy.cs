using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;
using Action.Core;

namespace Action.Copy.Strategy
{
    public class CardGridStrategy : ICopyGridStrategy
    {
        public CopyGridArgs Run(GameSession session, CopyMember member, object data)
        {
            member.Player.Temp.CardProcess = new CardProcess()
            {
                TryTimes = 0,
                Cards = data as Card[]
            };
            return member.CurrentGrid.ToArgs();
        }
    }
}
