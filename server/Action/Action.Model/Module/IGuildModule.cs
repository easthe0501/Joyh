using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;

namespace Action.Model
{
    public interface IGuildModule : IGameModule
    {
        void AddContribute(GamePlayer player, int contribution);
    }
}
