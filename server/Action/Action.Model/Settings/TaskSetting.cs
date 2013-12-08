using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Utility;
using Action.Core;

namespace Action.Model
{
    public enum TaskClass
    {
        Main = 1,
        Branch,
        Daily,
        Achievement,
        Random
    }

    public enum TaskType
    {
        Login = 1000,
        Online,
        GoldBuyMoney,
        CreateBuilding = 1200,
        DeleteBuilding,
        HarvestBuilding,
        UpgradeHome,
        CollectItem = 1500,
        UseItem,
        AddFriend = 1600,
        InviteHero = 1700,
        Embattle,
        GetExpByItem,
        WearEquip = 1800,
        StrengthenEquip,
        CompoundEquip,
        WashEquip,
        PassCopy = 2000,
        ChangeDice,
        HandleMeeting,
        KillMonster = 2100,
        HuntSoul = 2200,
        JoinGuild = 2400,
        BuyGoods = 2500
    }

    public class TaskSettingBase : JsonSetting
    {
        public TaskType Type { get; set; }
        public object Data { get; set; }
        public int Data_Int32 { get; private set; }
        public IdCountPair Data_IdCountPair { get; private set; }
        public int PreviousTask { get; set; }

        public void Init()
        {
            switch (Type)
            {
                case TaskType.CollectItem:
                case TaskType.KillMonster:
                    Data_IdCountPair = JsonHelper.FromJson<IdCountPair>(Data.ToString());
                    break;
                case TaskType.Embattle:
                case TaskType.JoinGuild:
                    break;
                default:
                    Data_Int32 = MyConvert.ToInt32(Data);
                    break;
            }
        }
    }

    public class TaskSetting : TaskSettingBase
    {
        public int Level { get; set; }
        public TaskClass Class { get; set; }
        public Prize Prize { get; set; }
        public int[] FollowTasks { get; set; }
        public int[] UnlockCopies { get; set; }
        public int[] UnlockCommands { get; set; }

        public PrizeSource GetPrizeSource()
        {
            switch (Class)
            {
                case TaskClass.Daily:
                    return PrizeSource.Daily;
                case TaskClass.Achievement:
                    return PrizeSource.Achievement;
                default:
                    return PrizeSource.Task;
            }
        }
    }

    public class RandomTaskSetting : TaskSettingBase
    {
    }
}
