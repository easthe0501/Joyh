using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class BagSetting
    {
        public int CapacityExpandSize { get; set; }
        public int ItemsStackLimit { get; set; }
        public int BagMaxPage { get; set; }
        public int[] ExpandBagPageCosts { get; set; }
        public float ItemSellRate { get; set; }
        public int ExpandBagVip { get; set; }
    }
}
