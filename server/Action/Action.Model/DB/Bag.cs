using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;

namespace Action.Model
{
    public class Bag : Entity
    {
        protected override void OnInit(IEntityRoot root)
        {
            GoodsBagSize = APF.Settings.Bag.CapacityExpandSize;
            MaterialsBagSize = APF.Settings.Bag.CapacityExpandSize;
            GoodsBag = new List<Item>();
            MaterialsBag = new List<Item>();
            TempBag = new List<Item>();
        }

        /// <summary>
        /// 物品背包容量
        /// </summary>
        public int GoodsBagSize { get; set; }

        /// <summary>
        /// 材料背包容量
        /// </summary>
        public int MaterialsBagSize { get; set; }

        /// <summary>
        /// 物品背包
        /// </summary>
        public List<Item> GoodsBag { get; private set; }

        /// <summary>
        /// 材料容量
        /// </summary>
        public List<Item> MaterialsBag { get; private set; }

        /// <summary>
        /// 临时容量
        /// </summary>
        public List<Item> TempBag { get; set; }
    }
}
