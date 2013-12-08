using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;

namespace Action.Model
{
    public interface IHuntModule : IGameModule
    {
        bool HuntSoul(Player player, int arg, SoulHuntSetting thisHunt);
    }
}
