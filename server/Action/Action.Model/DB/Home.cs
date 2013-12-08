using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Action.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace Action.Model
{
    public class Home : Entity
    {
        public Home()
        {
        }

        protected override void OnInit(IEntityRoot root)
        {
            //初始属性
            Level = 1;
            Properties = new int[5];
            APF.Settings.Home.InitProperties.CopyTo(Properties, 0);

            //初始范围
            MinX = APF.Settings.Home.InitMinX;
            MinY = APF.Settings.Home.InitMinY;
            MaxX = APF.Settings.Home.InitMaxX;
            MaxY = APF.Settings.Home.InitMaxY;

            //初始建筑
            Buildings = new List<Building>(APF.Settings.Home.InitBuildings);
            
        }

        [BsonIgnore]
        /// <summary>
        /// 家园得分
        /// </summary>
        public int Score
        {
            get { return Properties.Sum(); }
        }


        /// <summary>
        /// 家园等级（暂时不用）
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 家园属性
        /// </summary>
        public int[] Properties { get; private set; }

        /// <summary>
        /// 设施集合
        /// </summary>
        public List<Building> Buildings { get; private set; }

        /// <summary>
        /// 创建一个设施，并自动生成其Id
        /// </summary>
        /// <returns></returns>
        public Building CreateBuilding(Player player, int settingId)
        {
            return APF.Factory.Create<Building>(player, settingId);
        }

        /// <summary>
        /// 有效区域最小X
        /// </summary>
        public int MinX { get; set; }

        /// <summary>
        /// 有效区域最小Y
        /// </summary>
        public int MinY { get; set; }

        /// <summary>
        /// 有效区域最大X
        /// </summary>
        public int MaxX { get; set; }

        /// <summary>
        /// 有效区域最大Y
        /// </summary>
        public int MaxY { get; set; }

        /// <summary>
        /// 当前宽度
        /// </summary>
        public int Width
        {
            get { return MaxX - MinX; }
        }

        /// <summary>
        /// 当前高度
        /// </summary>
        public int Height
        {
            get { return MaxY - MinY; }
        }

        /// <summary>
        /// 获取家园界限
        /// </summary>
        /// <returns></returns>
        public Rectangle GetBounds()
        {
            return new Rectangle(MinY, MinY, Width, Height);
        }

        /// <summary>
        /// 刷新分数
        /// </summary>
        //public void Refresh()
        //{
        //    var setting = APF.Settings.Home;
        //    int sumAcreage = 0;
        //    int sumProps = Properties.Sum();
        //    int sumTrees = 0;
        //    int sumRoads = 0;
        //    foreach (var building in Buildings)
        //    {
        //        sumAcreage += building.Setting.Acreage;
        //        sumTrees += building.Setting.TreePower;
        //        sumRoads += building.Setting.RoadPower;
        //    }
        //    Score = (int)(sumAcreage * Math.Min(setting.BestTreeRate, (float)sumTrees / (float)sumAcreage)
        //        * Math.Min(setting.BestRoadRate, (float)sumRoads / (float)sumAcreage));
        //}
    }
}
