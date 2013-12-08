using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Action.Utility;

namespace Action.Equip
{
    [Export(typeof(IGameModule))]
    public class EquipModule : GameModule
    {
        public override void Load(GameWorld world)
        {
            foreach (var equip in APF.Settings.Equips.All)
            {
                var strEquipId = equip.Id.ToString();

                //合法性验证
                //Trace.Assert(equip.Buff.Data > 0, strEquipId);
            }
        }
    }
}
