using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Home
{
    [Export(typeof(IGameModule))]
    public class HomeModule : GameModule
    {
        public override void Load(GameWorld world)
        {
            var home = APF.Settings.Home;

            Trace.Assert(home.InitProperties != null && home.InitProperties.Length == 5);
            Trace.Assert(home.InitBuildings != null);
            Trace.Assert(home.InitMaxX - home.InitMinX <= home.TotalWidth);
            Trace.Assert(home.InitMaxY - home.InitMinY <= home.TotalHeight);

            foreach (var build in APF.Settings.Buildings.All)
            {
                var strBuildingId = build.Id.ToString();

                //合法性验证
                Trace.Assert(build.Width > 0, strBuildingId);
                Trace.Assert(build.Height > 0, strBuildingId);

                //建造消耗的材料验证
                foreach (var conM in build.Consumable.Materials)
                    Trace.Assert(APF.Settings.Items.Find(conM.Id) != null, strBuildingId);
                //建筑物产出的材料验证
                if (build.IfProduct)
                {
                    Trace.Assert(APF.Settings.Items.Find(build.Product.BaseMaterial.Id) != null, strBuildingId);
                    if (build.Product.SuperMaterial != null)
                        Trace.Assert(APF.Settings.Items.Find(build.Product.SuperMaterial.Id) != null, strBuildingId);
                }
            }
        }
        
    }
}
