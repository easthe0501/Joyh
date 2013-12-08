using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;
using Action.Core;

namespace Action.Copy.Strategy
{
    public class MeetingGridStrategy : ICopyGridStrategy
    {
        public CopyGridArgs Run(GameSession session, CopyMember member, object data)
        {
            member.Player.Temp.MeetingOptions = data as MeetingOption[];
            return member.CurrentGrid.ToArgs();
        }
    }
}
