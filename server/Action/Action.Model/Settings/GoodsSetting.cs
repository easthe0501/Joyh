using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public enum GoodsClass
    {
        Money,
        Gold
    }

    public class GoodsSetting : JsonSetting
    {
        public GoodsClass Class { get; set; }
        public int ItemId { get; set; }
        public int ItemCount { get; set; }
        public int OldPrice { get; set; }
        public int NewPrice { get; set; }
        public bool IsNew { get; set; }
        public bool IsDiscount { get; set; }
    }
}
