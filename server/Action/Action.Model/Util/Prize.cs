using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;

namespace Action.Model
{
    public enum PrizeSource
    {
        GM = 1910,
        Task = 1911,
        Daily = 1912,
        Achievement = 1913,
        BattleArena = 1914,
        Login = 1915,
        Online = 1916,
        CopyGrid = 1917,
        CopyPass = 1918,
        Box = 1919,
        Sign = 1920
    }

    public class Prize
    {
        public string Title { get; set; }
        public int Gold { get; set; }
        public int Money { get; set; }
        public int Energy { get; set; }
        public int Repute { get; set; }
        public int Exp { get; set; }
        public IdCountPair[] Items { get; set; }

        public PrizeTip ToTip(bool includeItems = true)
        {
            var tip = new PrizeTip()
            {
                Gold = Gold,
                Money = Money,
                Energy = Energy,
                Repute = Repute,
                Exp = Exp
            };
            if (includeItems && Items != null)
            {
                foreach (var item in Items)
                    tip.Items.Add(new IdCountArgs() { Id = item.Id, Count = item.Count });
            }
            return tip;
        }

        public PrizeTip Open(GameSession session, PrizeSource source, bool noteTip = true)
        {
            var self = session.Player.Data.AsDbPlayer();
            if (Gold > 0)
            {
                self.Gold += Gold;
                session.SendResponse((int)CommandEnum.RefreshGold, self.Gold);
            }
            if (Money > 0)
            {
                self.Money += Money;
                session.SendResponse((int)CommandEnum.RefreshMoney, self.Money);
            }
            if (Energy > 0)
            {
                self.Energy += Energy;
                session.SendResponse((int)CommandEnum.RefreshEnergy, self.Energy);
            }
            if (Repute > 0)
                self.AddRepute(session, Repute);
            if (Exp > 0)
            {
                var hero = self.GetMainHero();
                var oldLevel = hero.Level;
                if (hero.GetExp(Exp))
                {
                    self.OnLevelUp(session, oldLevel);
                    session.SendResponse((int)CommandEnum.RefreshMainHeroLevel, hero.Level);
                }
                session.SendResponse((int)CommandEnum.RefreshMainHeroExp, hero.Exp);
            }
            var tip = ToTip(false);         
            if (Items != null)
            {
                foreach (var item in Items)
                {
                    if (item.Percent >= 100 || APF.Random.Percent(item.Percent))
                    {
                        session.Server.ModuleFactory.Module<IBagModule>().AddItem(session, item.Id, item.Count);
                        tip.Items.Add(new IdCountArgs() { Id = item.Id, Count = item.Count });
                    }
                }
            }
            if(noteTip)
                session.SendResponse((int)source, tip);
            return tip;
        }

        public Prize Merge(Prize other)
        {
            Gold += other.Gold;
            Money += other.Money;
            Energy += other.Energy;
            Repute += other.Repute;
            Exp += other.Exp;

            if (other.Items != null)
            {
                List<IdCountPair> list = null;
                foreach (var otherItem in other.Items)
                {
                    var thisItem = Items != null ? Items.SingleOrDefault(i => i.Id == otherItem.Id) : null;
                    if (thisItem == null)
                    {
                        if (list == null)
                            list = Items != null ? Items.ToList() : new List<IdCountPair>();
                        thisItem = new IdCountPair() { Id = otherItem.Id };
                        list.Add(thisItem);
                    }
                    thisItem.Count += otherItem.Count;
                }
                if (list != null)
                {
                    Items = list.ToArray();
                    list.Clear();
                }
            }
            return this;
        }

        public PrizeObj CreateObj(int id)
        {
            return new PrizeObj()
            {
                Id = id,
                Prize = this
            };
        }
    }

    public class PrizeEx : Prize
    {
        public int Quality { get; set; }
    }

    public class PrizeObj
    {
        public int Id { get; set; }
        public Prize Prize { get; set; }
    }
}
