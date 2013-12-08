using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class SoulWarehouse : Entity
    {
        protected override void OnInit(IEntityRoot root)
        {
            TempSouls = new List<Soul>();
            BackSouls = new List<Soul>();
        }

        public List<Soul> TempSouls { get; private set; }
        public List<Soul> BackSouls { get; private set; }
    }
}
