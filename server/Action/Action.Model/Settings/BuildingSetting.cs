using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;

namespace Action.Model
{
    /// <summary>
    /// 家园设施基本配置
    /// </summary>
    public class BuildingSetting : JsonSetting, IExternalChannel
    {
        public BuildingSetting()
        {
            Requirement = new RequirementSetting();
            Consumable = new ConsumableSetting();
            Product = new ProductSetting();
        }

        /// <summary>
        /// 是否可以建造
        /// </summary>
        public bool IfCanBuild { get; set; }

        /// <summary>
        /// 设施宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 设施高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 设施面积
        /// </summary>
        public int Acreage
        {
            get { return Width * Height; }
        }

        /// <summary>
        /// 树木能量加成
        /// </summary>
        public int TreePower { get; set; }

        /// <summary>
        /// 道路能量加成
        /// </summary>
        public int RoadPower { get; set; }

        /// <summary>
        /// 建设必需品
        /// </summary>
        public class RequirementSetting
        {
            public RequirementSetting()
            {
                Properties = new int[5];
            }

            /// <summary>
            /// 家园等级
            /// </summary>
            public int Level { get; set; }

            /// <summary>
            /// 玩家声望
            /// </summary>
            public int Repute { get; set; }

            /// <summary>
            /// 家园属性
            /// </summary>
            public int[] Properties { get; set; }
        }
        public RequirementSetting Requirement { get; set; }

        /// <summary>
        /// 建设消耗品
        /// </summary>
        public class ConsumableSetting
        {
            /// <summary>
            /// 消耗的元宝
            /// </summary>
            public int Gold { get; set; }

            /// <summary>
            /// 消耗的铜钱
            /// </summary>
            public int Money { get; set; }

            /// <summary>
            /// 消耗的材料
            /// </summary>
            public IdCountPair[] Materials { get; set; }
        }
        public ConsumableSetting Consumable { get; set; }

        /// <summary>
        /// 是否有产出
        /// </summary>
        public bool IfProduct { get; set; }

        /// <summary>
        /// 建筑的功能
        /// </summary>
        public class ProductSetting
        {
            public ProductSetting()
            {
                BaseMaterial = new IdCountPair();
                SuperMaterial = new IdCountPair();
                Properties = new int[5];
            }

            /// <summary>
            /// 建筑增益
            /// </summary>
            public int[] Properties { get; set; }

            /// <summary>
            /// 花费时间
            /// </summary>
            public float SpendTime { get; set; }

            /// <summary>
            /// 产出的钱
            /// </summary>
            public int Money { get; set; }

            /// <summary>
            /// 产出的基础材料
            /// </summary>
            public IdCountPair BaseMaterial { get; set; }

            /// <summary>
            /// 产出的高级材料
            /// </summary>
            public IdCountPair SuperMaterial { get; set; }

            /// <summary>
            /// 产出的高级材料的概率
            /// </summary>
            public int SuperPercent { get; set; }
        }
        public ProductSetting Product { get; set; }

        public void Import(Dictionary<string, string> externalData)
        {
            foreach(KeyValuePair<string, string> data in externalData)
            {
                switch (data.Key)
                {
                    case "*Consumable.Materails":
                        if (data.Value.Equals(""))
                        {
                            Consumable.Materials = new IdCountPair[0];
                        }
                        string[] ms = data.Value.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        Consumable.Materials = new IdCountPair[ms.Length];
                        for (int i = 0; i < ms.Length; i++)
                        {
                            int id = int.Parse(ms[i].Split(':')[0]);
                            int count = int.Parse(ms[i].Split(':')[1]);
                            Consumable.Materials[i] = new IdCountPair() { Id = id, Count = count };
                        }
                        break;
                    case "*Product.Material":
                        if (data.Value.Equals(""))
                        {
                            Product.BaseMaterial = null;
                            Product.SuperMaterial = null;
                        }
                        else
                        {
                            Product.BaseMaterial = new IdCountPair();
                            Product.SuperMaterial = null;
                            var pbm = data.Value.Split(',')[0];
                            int pmId = int.Parse(pbm.Split(':')[0]);
                            int pmCount = int.Parse(pbm.Split(':')[1]);
                            Product.BaseMaterial.Id = pmId;
                            Product.BaseMaterial.Count = pmCount;
                            if (data.Value.Split(',').Length == 2)
                            {
                                Product.SuperMaterial = new IdCountPair();
                                var psm = data.Value.Split(',')[1];
                                int psmId = int.Parse(psm.Split(':')[0]);
                                int psmCount = int.Parse(psm.Split(':')[1]);
                                int psmPer = int.Parse(psm.Split(':')[2]);
                                Product.SuperMaterial.Id = psmId;
                                Product.SuperMaterial.Count = psmCount;
                                Product.SuperPercent = psmPer;
                            }
                        }
                        break;
                }
            }
            
        }

    }
}
