using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class RoleSetting
    {
        public int NameMinLength { get; set; }
        public int NameMaxLength { get; set; }
        public int FaceMaxId { get; set; }
        public int EnergyMax { get; set; }
        public int EnergyAutoPlus { get; set; }
        public int LevelMin { get; set; }
        public int LevelMax { get; set; }
        public int HeroInitId { get; set; }
        public int HeroInitPos { get; set; }
        public int HeroInitSpace { get; set; }
        public int HeroMaxSpace { get; set; }
        public int SoulInitSpace { get; set; }
        public int SoulMaxSpace { get; set; }
        public int SoulWarehouseTempSpace { get; set; }
        public int SoulWarehouseBackSpace { get; set; }
        public int SoulMaxLevel { get; set; }

        public int[] ExpandHeroSpaceCosts { get; set; }
        public int[] GetGroupPrizeCosts { get; set; }
        public int[] GoldBuyMoneyCosts { get; set; }
        public int[] GoldBuyEnergyCosts { get; set; }
        public int GoldBuyMoneyIncome { get; set; }
        public int GoldBuyEnergyIncome { get; set; }

        public int[] QualityReputes { get; set; }
        public Prize InitPrize { get; set; }

        public int MaxEmbattleCount { get; set; }
        public int MaxHeroQueueCount { get; set; }

        public int FriendsMax { get; set; }
        public int GetExpCostMoney { get; set; }

        public int LightQualityCost { get; set; }
        public int RubbishSoul { get; set; }

        public int DailyPvpCount { get; set; }
        public int GoldBuyPvpCount { get; set; }
        public int SignInConDays { get; set; }
        public int SignInSumDays { get; set; }
        public int FillSignCostGold { get; set; }

        public int[] SelectCardCosts { get; set; }

        public int DailyRandomTaskCount { get; set; }
        public int RefreshRandomTaskCost { get; set; }
    }
}
