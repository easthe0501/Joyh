using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class EquipCompoundSetting : JsonSetting, IExternalChannel
    {
        public EquipCompoundSetting()
        {
            Lucky = new LuckySetting();
            Requirement = new RequirementSetting();
            Consumable = new ConsumableSetting();
        }

        /// <summary>
        /// 要合成的目标装备Id
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// 合成幸运值要求
        /// </summary>
        public class LuckySetting
        {
            public int BasicVal { get; set; }
            public int BasicPct { get; set; }
            public int MaxVal { get; set; }
            public int MaxPct { get; set; }
            public int MinPlus { get; set; }
            public int MaxPlus { get; set; }
        }
        public LuckySetting Lucky { get; set; }

        /// <summary>
        /// 合成必需品
        /// </summary>
        public class RequirementSetting
        {
            /// <summary>
            /// 玩家等级
            /// </summary>
            public int Level { get; set; }
        }
        public RequirementSetting Requirement { get; set; }

        /// <summary>
        /// 合成消耗品
        /// </summary>
        public class ConsumableSetting
        {
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

        public void Import(Dictionary<string, string> externalData)
        {
            foreach (KeyValuePair<string, string> dataKY in externalData)
            {
                string[] ms = dataKY.Value.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                Consumable.Materials = new IdCountPair[ms.Length];
                for (int i = 0; i < ms.Length; i++)
                {
                    int id = int.Parse(ms[i].Split(':')[0]);
                    int count = int.Parse(ms[i].Split(':')[1]);
                    Consumable.Materials[i] = new IdCountPair() { Id = id, Count = count };
                }
            }

        }
    }
}
