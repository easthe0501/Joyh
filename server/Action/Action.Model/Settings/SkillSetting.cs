using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    /// <summary>
    /// 绝技设置
    /// </summary>
    public class SkillSetting : JsonSetting, IExternalChannel
    {
        public bool IsMind { get; set; }

        /// <summary>
        /// 保留气势
        /// </summary>
        public int RemainXp { get; set; }

        /// <summary>
        /// 绝技所带Buff集合
        /// </summary>
        public BuffGroup[] BuffGroups { get; set; }

        public void Import(Dictionary<string, string> externalData)
        {
            foreach (KeyValuePair<string, string> dataKY in externalData)
            {
                string[] ms = dataKY.Value.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                BuffGroups = new BuffGroup[ms.Length];
                for (int i = 0; i < ms.Length; i++)
                {
                    BuffRange range = (BuffRange)int.Parse(ms[i].Split(':')[0]);
                    string[] buffString = ms[i].Split(':')[1].Split(',');
                    Buff[] buff = new Buff[buffString.Length];
                    for (int j = 0; j < buff.Length; j++)
                    {
                        string[] bs = buffString[j].Split('-');
                        BuffEffect effect = (BuffEffect)int.Parse(bs[0]);
                        var data = int.Parse(bs[1]);
                        var round = int.Parse(bs[2]);
                        buff[j] = new Buff() { Data = data, Effect = effect, Round = round };
                    }
                    BuffGroups[i] = new BuffGroup() { Range = range, Buffs = buff };
                }
            }

        }
    }
}
