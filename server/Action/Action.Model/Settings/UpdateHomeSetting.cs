using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class UpdateHomeSetting : JsonSetting, IExternalChannel
    {
        /// <summary>
        /// 家园等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 花费铜钱
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// 需要声望
        /// </summary>
        public int Repute { get; set; }

        /// <summary>
        /// 需求六大属性值
        /// </summary>
        public int[] Properties { get; set; }

        /// <summary>
        /// 花费材料
        /// </summary>
        public IdCountPair[] Materials { get; set; }

        public void Import(Dictionary<string, string> externalData)
        {
            foreach (KeyValuePair<string, string> data in externalData)
            {
                switch (data.Key)
                {
                    case "*Materials":
                        string[] ms = data.Value.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        Materials = new IdCountPair[ms.Length];
                        for (int i = 0; i < ms.Length; i++)
                        {
                            var id = int.Parse(ms[i].Split(':')[0]);
                            var count = int.Parse(ms[i].Split(':')[1]);
                            Materials[i] = new IdCountPair() { Id = id, Count = count };
                        }
                        break;
                }
            }
        }
    }
}
