using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;

namespace Action.Copy.Strategy
{
    public interface ICopyGridStrategy
    {
        CopyGridArgs Run(GameSession session, CopyMember member, object data);
    }
}
