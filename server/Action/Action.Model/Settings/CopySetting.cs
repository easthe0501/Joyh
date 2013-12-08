using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;
using Action.Utility;

namespace Action.Model
{
    public enum CopyGrade
    {
        Common,
        Advanced,
        Super,
        Guild
    }

    public class CopySetting : JsonSetting, IExternalChannel
    {
        public CopySetting()
        {
            Grade = CopyGrade.Common;
            StyleOptions = new StyleOptionsSetting();
            EnterRequirement = new EnterRequirementSetting();
            EnterConsumable = new EnterConsumableSetting();
            PassPrize = new PassPrizeSetting();
        }

        public CopyGrade Grade { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int NextId { get; set; }
        public int[] Styles { get; set; }

        public class EnterRequirementSetting
        {
            public int PlayerLevel { get; set; }
            public int GuildLevel { get; set; }
            public int TaskId { get; set; }
        }
        public EnterRequirementSetting EnterRequirement { get; set; }

        public class EnterConsumableSetting
        {
            public int Energy { get; set; }
            public IdCountPair[] Materials { get; set; }
        }
        public EnterConsumableSetting EnterConsumable { get; set; }

        public class StyleOptionsSetting
        {
            public Prize[] Money{ get; set; }
            public Prize[] Material { get; set; }
            public PrizeEx[] Box { get; set; }
            public int[] Monster { get; set; }
            public int Boss { get; set; }
            public MeetingOption[] Meeting { get; set; }
            public Card[] Card { get; set; }
        }
        public StyleOptionsSetting StyleOptions { get; set; }

        public class PassPrizeSetting
        {
            public int GuildContribution { get; set; }
            public int[] UnlockHeros { get; set; }
            public Prize Prize { get; set; }
        }
        public PassPrizeSetting PassPrize { get; set; }

        public CopySetting Next()
        {
            return APF.Settings.Copies.Find(NextId);
        }

        public bool IsSingle
        {
            get { return MinPlayers == 1 && MaxPlayers == 1; }
        }

        public bool Unlock(Player player)
        {
            return player.Permission.Copies.Add(Id);
        }

        public void Import(Dictionary<string, string> externalData)
        {
            foreach (KeyValuePair<string, string> data in externalData)
            {
                switch (data.Key)
                {
                    case "*StyleOptions.Money":
                        string[] sm = data.Value.Split('|');
                        StyleOptions.Money = new Prize[sm.Length];
                        for (int i = 0; i < sm.Length; i++)
                        {
                            StyleOptions.Money[i] = new Prize();
                            StyleOptions.Money[i].Money = int.Parse(sm[i]);
                        }
                        break;
                    case "*StyleOptions.Material":
                        string[] sml = data.Value.Split('|');
                        StyleOptions.Material = new Prize[sml.Length];
                        for (int j = 0; j < sml.Length; j++)
                        {
                            string[] m = sml[j].Split(',');
                            StyleOptions.Material[j] = new Prize();
                            StyleOptions.Material[j].Items = new IdCountPair[m.Length];
                            for (int mi = 0; mi < m.Length; mi++)
                            {
                                StyleOptions.Material[j].Items[mi] = new IdCountPair();
                                StyleOptions.Material[j].Items[mi].Id = int.Parse(m[mi].Split(':')[0]);
                                StyleOptions.Material[j].Items[mi].Count = int.Parse(m[mi].Split(':')[1]);
                            }
                        }
                        break;
                    case "*StyleOptions.Box":
                        string[] sb = data.Value.Split('|');
                        StyleOptions.Box = new PrizeEx[sb.Length];
                        for (int k = 0; k < sb.Length; k++)
                        {
                            string[] sbm = sb[k].Split('-');
                            StyleOptions.Box[k] = new PrizeEx();
                            StyleOptions.Box[k].Quality = int.Parse(sbm[0]);
                            if(!sbm[1].Contains(":"))
                                StyleOptions.Box[k].Money = int.Parse(sbm[1]);
                            if (sbm.Length > 2)
                            {
                                string[] mm = sbm[2].Split(',');
                                StyleOptions.Box[k].Items = new IdCountPair[mm.Length];
                                for (int mmi = 0; mmi < mm.Length; mmi++)
                                {
                                    StyleOptions.Box[k].Items[mmi] = new IdCountPair();
                                    StyleOptions.Box[k].Items[mmi].Id = int.Parse(mm[mmi].Split(':')[0]);
                                    StyleOptions.Box[k].Items[mmi].Count = int.Parse(mm[mmi].Split(':')[1]);
                                }
                            }
                        }
                        break;
                    case "*StyleOptions.Card":
                        if (data.Value.Equals(""))
                        {
                            StyleOptions.Card = null;
                            break;
                        }
                        var card = data.Value.Split('|');
                        StyleOptions.Card = new Card[card.Length];
                        for (int k = 0; k < card.Length; k++)
                        {
                            
                            StyleOptions.Card[k] = new Card();
                            if (card[k].Equals(""))
                                continue;
                            var cs = card[k].Split('-');
                            StyleOptions.Card[k].Quality = int.Parse(cs[1]);
                            StyleOptions.Card[k].Prize = new Prize();
                            var index = cs[0];
                            switch (index)
                            {
                                case "G":
                                    StyleOptions.Card[k].Type = CardType.G;
                                    StyleOptions.Card[k].Prize.Gold = int.Parse(cs[2]);
                                    break;
                                case "M":
                                    StyleOptions.Card[k].Type = CardType.M;
                                    StyleOptions.Card[k].Prize.Money = int.Parse(cs[2]);
                                    break;
                                case "P":
                                    StyleOptions.Card[k].Type = CardType.P;
                                    StyleOptions.Card[k].Prize.Energy = int.Parse(cs[2]);
                                    break;
                                case "R":
                                    StyleOptions.Card[k].Type = CardType.R;
                                    StyleOptions.Card[k].Prize.Repute = int.Parse(cs[2]);
                                    break;
                                case "E":
                                    StyleOptions.Card[k].Type = CardType.E;
                                    StyleOptions.Card[k].Prize.Exp = int.Parse(cs[2]);
                                    break;
                                case "I":
                                    StyleOptions.Card[k].Type = CardType.I;
                                    StyleOptions.Card[k].Prize.Items = new IdCountPair[1];
                                    StyleOptions.Card[k].Prize.Items[0] = new IdCountPair();
                                    StyleOptions.Card[k].Prize.Items[0].Id = int.Parse(cs[2].Split(':')[0]);
                                    StyleOptions.Card[k].Prize.Items[0].Count = int.Parse(cs[2].Split(':')[1]);
                                    StyleOptions.Card[k].Prize.Items[0].Percent = 100;
                                    break;
                            }
                            StyleOptions.Card[k].Data = cs[2];
                            if (cs.Length > 3)
                                StyleOptions.Card[k].Rate = int.Parse(cs[3]);
                            else
                                StyleOptions.Card[k].Rate = 100;
                        }
                        break;
                    case "*EnterConsumable.Materials":
                        if (data.Value.Equals(""))
                        {
                            EnterConsumable.Materials = null;
                            break;
                        }
                        string[] em = data.Value.Split(',');
                        EnterConsumable.Materials = new IdCountPair[em.Length];
                        for (int q = 0; q < em.Length; q++)
                        {
                            EnterConsumable.Materials[q] = new IdCountPair();
                            EnterConsumable.Materials[q].Id = int.Parse(em[q].Split(':')[0]);
                            EnterConsumable.Materials[q].Count = int.Parse(em[q].Split(':')[1]);
                        }
                        break;
                    case "*PassPrize.Prize":
                        if (data.Value.Equals(""))
                        {
                            PassPrize.Prize = null;
                            break;
                        }
                        string[] w = data.Value.Split('-');
                        var prize = PassPrize.Prize = new Prize();
                        prize.Money = int.Parse(w[0]);
                        prize.Exp = int.Parse(w[1]);
                        if (w.Length < 3)
                            break;
                        string[] wi = w[2].Split(',');
                        prize.Items = new IdCountPair[wi.Length];
                        for (int i = 0; i < wi.Length; i++)
                        {
                            prize.Items[i] = new IdCountPair();
                            var wiss = wi[i].Split(':');
                            prize.Items[i].Id = int.Parse(wiss[0]);
                            prize.Items[i].Count = int.Parse(wiss[1]);
                            if (wiss.Length >= 3)
                                prize.Items[i].Percent = int.Parse(wiss[2]);
                            else
                                prize.Items[i].Percent = 100;
                        }
                        break;
                }
            }
        }
    }
}
