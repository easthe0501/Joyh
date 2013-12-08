using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public enum CommandEnum
    {
        //System
        CommandInCD = 100,
        ShowInfo = 110,
        ShowWarning,
        ShowError,
        KeepConnection = 200,

        //Login
        BackdoorLogin = 1000,
        TencentLogin,
        CreateRole = 1010,
        EnterGame = 1011,
        CheckRoleName = 1012,
        AccountLocked = 1013,

        //Role
        ViewRolePanel = 1100,
        RefreshMoney = 1110,
        RefreshEnergy,
        RefreshGold,
        RefreshRepute,
        RefreshDailyCount,
        RefreshVip,
        GoldBuyMoney = 1120,
        GoldBuyEnergy,
        GoldBuyFailed,
        PayAttention = 1130,
        CancelAttention,
        LoadUnlockedCommands = 1140,
        UnlockCommand = 1141,
        DailyCount = 1150,
        LoadVip = 1160,
        SignIn = 1170,
        LoadSignIn = 1171,
        FillSign = 1172,
        SystemTime = 1173,
        GetSignPrize = 1174,
        LoadBuyMoney = 1175,
        LoadBuyEnergy = 1176,

        //Home
        LoadHome = 1200,
        CreateBuilding = 1210,
        MoveBuilding = 1220,//废弃
        DeleteBuilding = 1230,
        CanHarvest = 1240,
        HarvestBuilding = 1250,
        UpgradeHome = 1260,
        RefreshProperties = 1261,

        //Chat
        TalkToWorld = 1300,
        TalkToLocal,
        TalkToGuild,
        TalkToPrivate,
        TalkToGM,
        TalkToSelf,
        TalkDisabled = 1310,
        ShowEquip = 1320,
        Marquee = 1330,
        SysNoteGoodEquip = 1340,
        SysNoteGoodSoul,

        //Bag
        LoadBag = 1500,
        LoadBagEquips = 1501,//废弃
        SortBag = 1510,
        Sell = 1520,
        Abandon = 1530,
        ExpandBag = 1540,
        TempToBag = 1550,   //废弃
        MoveItem = 1560,
        AddItem = 1570,
        ChangeItem = 1571,
        BagPoint = 1580,
        UseItem = 1590,
        CompoundItem = 1591,

        //Friend
        LoadFriend = 1600,
        AddFriend = 1610,
        DelFriend = 1620,
        AddedFriend = 1630,
        LoadBacklist = 1631,
        AddBacklist = 1632,

        //Hero
        LoadQueueHeros = 1700,
        LoadHero,
        LoadAllHeros,
        GetExpByItem = 1710,
        RefreshMainHeroLevel = 1720,
        RefreshMateHeroLevel = 1721,
        Embattle = 1730,
        LoadHeroPermission = 1740,
        InviteHero = 1750,
        UnlockHeros = 1751,
        ExpandHeroSpace = 1760,
        LoadEmbattle = 1770,
        HeroSettingChanged = 1780,
        UpDownQueue = 1781,
        RefreshMainHeroExp = 1782,

        //Equip
        LoadEquip = 1800,
        WearEquip = 1810,
        UnwearEquip = 1820,
        StrengthenEquip = 1830,
        CompoundEquip = 1840,
        WashEquip = 1850,
        LockEquipPlugin,
        CompoundExpect,
        RefreshLucky,

        //Prize
        NoticePrizeObjs = 1900,
        GetPrizeObj = 1901,
        //GetCopyGridPrize = 1902,
        //GetCopyPassPrize = 1903,
        //OpenPrize = 1905,//仅用于调试，之后去除

        //Copy
        EnterCopy = 2000,
        LeaveCopy = 2001,
        CopyProxy = 2002,
        CastSingleDice = 2010,
        CastDoubleDice = 2011,
        CastCustomDice = 2012,
        CastMinDice = 2013,
        CastToEnd = 2014,
        CastToEndPrizeTips = 2015,
        CastDiceError = 2016,
        LoadUnlockedCopies = 2030,
        UnlockCopy = 2031,
        ViewMeetingOptions = 2040,
        SelectMeetingOption = 2041,
        ViewCards = 2050,
        SelectCard = 2051,

        //Battle
        LoadBattleReport = 2100,
        LoadTempBattleReport = 2101,
        LoadArenaTargets = 2110,
        ChallengeInArena = 2111,
        TestBattle = 2120,
        LoadArena = 2121,
        AskForReport = 2122,
        PvpRefresh = 2123,
        LoadArenaTop10 = 2124,
        RefreshTargets = 2125,
        BuyPvpCount = 2126,
        ArenaLog = 2127,
        RefreshArena = 2128,

        //Hunt
        HuntSoul = 2200,
        LoadHunt = 2201,
        LoadBackSouls = 2202,
        PickupSoul = 2203,
        PickupSouls = 2204,
        SwallowSoul = 2205,
        SwallowSouls = 2206,
        SellSoul = 2207,
        WearSoul = 2208,
        UnwearSoul = 2209,
        LoadHeroHunt = 2210,
        SellSouls = 2212,
        HuntSouls = 2213,
        OpenQuality = 2214,
        SortSouls = 2215,

        //Task
        LoadAllTasks = 2300,
        UpdateTasks = 2310,
        //UpdateTask = 2311,
        AcceptTask = 2320,
        //GiveUpTask = 2321,
        SubmitTask = 2322,
        UpdateRandomTask = 2330,
        RefreshRandomTask = 2331,
        SubmitRandomTask = 2332,

        //Guild
        LoadGuilds = 2400,
        LoadGuild = 2401,
        CreateGuild = 2402,
        ApplyJoinGuild = 2403,
        AgreeJoinGuild = 2404,
        ChangeGuildPost = 2405,
        TransferGuild = 2406,
        RemoveGuildMember = 2407,
        QuitGuild = 2408,
        WriteGuildNotice = 2410,
        ContributeForGuild1 = 2411,
        ContributeForGuild2 = 2412,
        ContributeForGuild3 = 2413,
        Impeach = 2414,
        PlayerNoGuild = 2415,
        ApplyList = 2417,
        GuildLogList = 2418,
        GuildMessage = 2419,
        GuildLog = 2420,
        ContributeForGuild5 = 2422,
        ContributeForGuild7 = 2424,
        ContributeForGuild9 = 2426,
        ContributeForGuild11 = 2428,
        JoinGuild = 2429,

        //LoadShop
        LoadShop = 2500,
        BuyGoods = 2501,

        //Recharge
        GmRecharge = 2600,
        TencentRecharge
    }
}
