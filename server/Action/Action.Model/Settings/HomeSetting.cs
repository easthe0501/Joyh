using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class HomeSetting
    {
        public int[] InitProperties { get; set; }
        public Building[] InitBuildings { get; set; }
        public int InitMinX { get; set; }
        public int InitMinY { get; set; }
        public int InitMaxX { get; set; }
        public int InitMaxY { get; set; }
        public int TotalWidth { get; set; }
        public int TotalHeight { get; set; }
        public float ReturnMoneyRate { get; set; }
        public float ReturnMaterialRate { get; set; }
        public int DefaultExpandSize { get; set; }
        public int LevelToplimit { get; set; }
        public int FreeDeleteSec { get; set; }
        //public float BestTreeRate { get; set; }
        //public float BestRoadRate { get; set; }
        
    }
}
