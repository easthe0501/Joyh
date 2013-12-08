using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class BattleSetting : JsonSetting, IFighterContainer, IExternalChannel
    {
        public BattleSetting()
        {
            WinnerPrize = new Prize();
            LoserPrize = new Prize();
        }

        public MonsterLayout[] Layouts { get; set; }
        public Prize WinnerPrize { get; set; }
        public Prize LoserPrize { get; set; }

        public void Init()
        {
            foreach (var layout in Layouts)
                layout.Init();
        }

        public BattleGroup CreateBattleGroup(int startId)
        {
            return new BattleGroup(BattleGroupType.Environment, Id.ToString(), 0,
                Layouts.Select(l => new BattleFighter(l.Monster, startId++, l.Pos)));
        }

        public void Import(Dictionary<string, string> externalData)
        {
            Dictionary<int, string> posData = new Dictionary<int,string>();
            foreach (string key in externalData.Keys)
            {
                if (key.Contains("Pos"))
                    posData.Add(int.Parse(key.Split('.')[1]), externalData[key]);
            }
            Layouts = new MonsterLayout[posData.Count(p => !p.Value.Equals(""))];
            int li = 0;
            foreach (KeyValuePair<int, string> kv in posData)
            {
                if (kv.Value.Equals(""))
                    continue;
                //怪物Id-等级-种族Id-技能Id--生命-气势
                int[] lms = kv.Value.Split('-').Select(v=>int.Parse(v)).ToArray();
                Layouts[li] = new MonsterLayout();
                Layouts[li].Pos = kv.Key;
                Layouts[li].Id = lms[0];
                Layouts[li].Level = lms[1];
                Layouts[li].JobId = lms[2];
                Layouts[li].SkillId = lms.Length > 3 ? lms[3] : 0;
                Layouts[li].HP = lms.Length > 4 ? lms[4] : 0;
                Layouts[li].XP = lms.Length > 5 ? lms[5] : 0;
                li++;
            }
            foreach (KeyValuePair<string, string> data in externalData)
            {
                if (data.Value.Equals(""))
                    continue;
                switch (data.Key)
                {
                    case "*WinnerPrize":
                        string[] w = data.Value.Split('-');
                        WinnerPrize.Money = int.Parse(w[0]);
                        WinnerPrize.Exp = int.Parse(w[1]);
                        if (w.Length < 3)
                            break;
                        string[] wi = w[2].Split(',');
                        WinnerPrize.Items = new IdCountPair[wi.Length];
                        for (int i = 0; i < wi.Length; i++)
                        {
                            WinnerPrize.Items[i] = new IdCountPair();
                            var wiss = wi[i].Split(':');
                            WinnerPrize.Items[i].Id = int.Parse(wiss[0]);
                            WinnerPrize.Items[i].Count = int.Parse(wiss[1]);
                            if (wiss.Length >= 3)
                                WinnerPrize.Items[i].Percent = int.Parse(wiss[2]);
                            else
                                WinnerPrize.Items[i].Percent = 100;
                        }
                        break;
                    case "*LoserPrize":
                        string[] lm = data.Value.Split('-');
                        LoserPrize.Money = int.Parse(lm[0]);
                        LoserPrize.Exp = int.Parse(lm[1]);
                        if (lm.Length < 3)
                            break;
                        string[] lmi = lm[2].Split(',');
                        LoserPrize.Items = new IdCountPair[lmi.Length];
                        for (int i = 0; i < lmi.Length; i++)
                        {
                            LoserPrize.Items[i] = new IdCountPair();
                            LoserPrize.Items[i].Id = int.Parse(lmi[i].Split(':')[0]);
                            LoserPrize.Items[i].Count = int.Parse(lmi[i].Split(':')[1]);
                        }
                        break;
                }
            }
        }

        public IEnumerable<IdCountPair> ToMonsterCounts()
        {
            return Layouts.GroupBy(m => m.Monster.SettingId)
                    .Select(p => new IdCountPair() { Id = p.Key, Count = p.Count() });
        }
    }
}
