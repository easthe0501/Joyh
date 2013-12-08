using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class HeroShop : Entity
    {
        protected override void OnInit(Action.Model.IEntityRoot root)
        {
            HeroFeelings = new Dictionary<int, int>();
        }

        public Dictionary<int, int> HeroFeelings { get; private set; }
    }
}
