using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class HuntSetting : JsonSetting
    {
        public int CostMoney { get; set; }
        public int ReachValue { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }

    public class SoulHuntSetting : HuntSetting, IExternalChannel
    {
        public int[] OutputSouls { get; set; }

        public void Import(Dictionary<string, string> externalData)
        {
            foreach (KeyValuePair<string, string> data in externalData)
            {
                if (data.Value.Equals(""))
                    continue;
                switch (data.Key)
                {
                    case "*OutputSouls":
                        int begin = int.Parse(data.Value.Split('-')[0]);
                        int end = int.Parse(data.Value.Split('-')[1]);
                        OutputSouls = new int[end - begin + 1];
                        for (int i = 0; i < OutputSouls.Length; i++)
                            OutputSouls[i] = begin + i;
                        break;
                }
            }
        }
    }

}
