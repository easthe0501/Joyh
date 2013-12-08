using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Equip.Command
{
    [GameCommand((int)CommandEnum.WashEquip)]
    public class WashEquipCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            throw new NotImplementedException();
        }
    }
}
