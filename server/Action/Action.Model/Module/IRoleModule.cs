using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;

namespace Action.Model
{
    public interface IRoleModule : IGameModule
    {
        void AddRight(GamePlayer player, int cmdId);
    }
}
