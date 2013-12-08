using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public static class ErrorCode
    {
        //Login
        public const int ErrorAccount = 1000;
        public const int RoleMissing = 1001;
        public const int RoleNameLengthError = 1002;
        public const int RoleNameNotValidate = 1003;
        public const int RoleNameExisted = 1004;

        //Role
        public const int CountOverflow = 1100;
        public const int GoldNotEnough = 1101;
        public const int MoneyNotEnough = 1102;
        public const int ReputeNotEnough = 1103;
        public const int EnergyNotEnough = 1104;
        public const int LevelNotEnough = 1105;
        public const int NotNeedFillSign = 1106;

        //Home
        public const int BuildingSettingIsWrong = 1200;
        public const int HomeLevelNotEnough = 1201;
        public const int HomeLevelLimited = 1202;
        public const int HomePropertiesNotEnough = 1203;
        public const int BuildingOutBorder = 1204;
        public const int BuildingIntersect = 1205;
        public const int BuildingFixed = 1206;
        public const int BuildingCannotHarvest = 1207;
        
        //Bag
        public const int BagSizeLimit = 1500;
        public const int ItemCannotSell = 1501;
        public const int BagIsFull = 1502;
        public const int MaterialsNotEnough = 1503;
        public const int GoodsBagIsFull = 1504;
        public const int MaterialsBagIsFull = 1505;
        public const int VipNotEnough = 1506;

        //Friend
        public const int CannotAddSelf = 1600;
        public const int RepeatAdd = 1601;
        public const int PlayerIsNotExist = 1602;
        public const int CannotTalkToBacklist = 1603;
        public const int OverFirsMax = 1604;
        public const int OverBlacksMax = 1605;
        public const int RepeatBlackAdd = 1606;
        public const int PlayerOffline = 1607;

        //Hero
        public const int HeroSpaceLimit = 1700;
        public const int HeroSpaceNotEnough = 1701;
        public const int HeroLevelNotEnough = 1702;
        public const int HeroLevelLimited = 1703;
        public const int HeroLevelOverMax = 1704;
        public const int MainHeroMustInBattle = 1705;
        public const int HeroRepeated = 1706;
        public const int HeroLocked = 1707;
        public const int MainHeroCannotEatItem = 1708;
        public const int EmbattleIsFull = 1709;
        public const int HeroQueueIsFull = 1710;
        public const int HeroInEmbattle = 1711;
        public const int HeroHasEquipCannotOffQueue = 1712;
        public const int MainHeroCannotOffQueue = 1713;
        public const int HeroHasSoulCannotOffQueue = 1714;
        public const int HeroShopNotOpen = 1715;

        //Equip
        public const int JobNotMatch = 1800;
        public const int EquipOverPlayerLevel = 1801;
        public const int CompoundNotSuccess = 1802;

        //Prize
        public const int PrizeMissing = 1900;
        public const int GetPrizeTimesOverflow = 1901;

        //Copy
        public const int EnterCopyRightNotEngouth = 2000;
        public const int CopyInstanceMissing = 2001;
        public const int NotYourCopyTurn = 2002;
        public const int AlreadyInCopy = 2003;
        public const int CopyNotFinished = 2004;
        public const int MeetingOptionMissing = 2005;
        public const int CardTimesOverflow = 2006;

        //Battle
        public const int BattleReportMissing = 2100;
        public const int DailyPvpCountLimit = 2101;

        //Hunt
        public const int HuntQualityNotLight = 2200;
        public const int SoulWarehouseTempSpaceNotEnough = 2201;
        public const int SoulWarehouseBackSpaceNotEnough = 2202;
        public const int CannotSwallowSoul = 2203;
        public const int SoulSpaceNotEnough = 2204;
        public const int HighQualityHasLight = 2205;
        public const int HasSameSoul = 2206;
        public const int BadQualitySoul = 2207;
        public const int SoulLevelLimit = 2208;

        //Task
        public const int TaskNotOpened = 2300;
        public const int TaskNotInProcess = 2301;
        public const int TaskNotFinished = 2302;

        //Guild
        public const int RepeatGuildName = 2400;
        public const int NotHasGuildRight = 2401;
        public const int HasJoinGuild = 2402;
        public const int GuildManagerOverMax = 2403;
        public const int IsNotDeputy = 2404;
        public const int CannotImpeach = 2405;
        public const int GuildNameNotNull = 2406;
        public const int NotInGuild = 2407;
        public const int LeaderCannotQuit = 2408;
        public const int GuildIsFull = 2409;
        public const int GuildLevelNotEnough = 2410;
        public const int GuildNoticeFilter = 2411;
        public const int GuildNameFilter = 2412;
        public const int GuildNameLengthError = 2413;
    }

    public static class AttentionCode
    {
        public const int HomeScene = 1200;
        public const int BagForm = 1500;
        public const int FriendForm = 1600;
        public const int HeroForm = 1700;
        public const int EmbattleForm = 1701;
        public const int StrengthenEquipForm = 1800;
        public const int CompoundEquipForm = 1801;
        public const int WashEquipForm = 1802;
        public const int CopyScene = 2000;
    }

    public static class BattleDefs
    {
        public const int SkillXp = 100;
        public const int StepXp = 25;
        public const int MaxXp = 250;
        public const float DodgeRatio = 0.4f;
        public const float BlockRatio = 0.5f;
        public const float CritRatio = 0.6f;
        public const float CritHurtRatio = 2f;
        public const int InitDodgePct = 0;
        public const int InitCritPct = 15;
        public const int MaxDodgePct = 60;
        public const int MaxBlockPct = 80;
    }
}
