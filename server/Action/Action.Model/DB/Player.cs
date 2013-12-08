using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Action.Core;
using Action.Engine;

namespace Action.Model
{
    public class Player : BsonClass, IPlayerData, IInitialization, IEntityRoot, IFighterContainer
    {
        public const string Cls = "Player";

        public Player()
        {
            Snapshot = new Snapshot();
            Temp = new PlayerTemp();
        }

        public void Init()
        {
            Key = Guid.NewGuid().ToString();
            CreateTime = DateTime.Now;
            Permission = new PlayerPermission();
            IdGen = new IdGen();
            DailyCountHistory = new DailyCountHistory();

            //家园、背包、好友、黑名单
            Home = APF.Factory.Create<Home>(this);
            Bag = APF.Factory.Create<Bag>(this);
            Friends = new List<string>();
            Blacklist = new List<string>();
            SignIns = APF.Factory.Create<SignIn>(this);

            //伙伴
            var roleSetting = APF.Settings.Role;
            Heros = new List<Hero>();
            Heros.Add(APF.Factory.Create<Hero>(this, roleSetting.HeroInitId));
            Heros[0].Pos = roleSetting.HeroInitPos;
            HeroSpace = roleSetting.HeroInitSpace;

            //战魂
            LightSoulQualities = new HashSet<int>();
            LightSoulQualities.Add(1);
            LightHeroQualities = new HashSet<int>();
            SoulWarehouse = APF.Factory.Create<SoulWarehouse>(this);

            //任务
            LockedTasks = new HashSet<int>();
            OpenedTasks = new HashSet<int>();
            ProcessTasks = new Dictionary<int, TaskProgress>();
            ClosedTasks = new HashSet<int>();
            RandomTask = new Model.RandomTask();

            //副本
            FinishedCopies = new HashSet<int>();

            //初始建筑
            foreach (var building in Home.Buildings)
                building.Init(this);

            //初始家园属性
            foreach (var b in Home.Buildings)
            {
                Home.Properties[0] += b.Setting.Product.Properties[0];
                Home.Properties[1] += b.Setting.Product.Properties[1];
                Home.Properties[2] += b.Setting.Product.Properties[2];
                Home.Properties[3] += b.Setting.Product.Properties[3];
                Home.Properties[4] += b.Setting.Product.Properties[4];
            }

            Load();
        }

        private Player _lookup;
        [BsonIgnore]
        /// <summary>
        /// 最近关注的其它玩家
        /// </summary>
        public Player Lookup
        {
            get { return _lookup; }
            set { _lookup = value; }
        }

        /// <summary>
        /// 唯一标示（暂时不用）
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 玩家账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 角色职业（1-4）
        /// </summary>
        public int Job { get; set; }

        /// <summary>
        /// 角色性别（0-1）
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 角色外形
        /// </summary>
        public int Face { get; set; }

        [BsonIgnore]
        /// <summary>
        /// 玩家等级
        /// </summary>
        public int Level
        {
            get { return Heros[0].Level; }
        }

        [BsonIgnore]
        /// <summary>
        /// 玩家经验
        /// </summary>
        public int Exp
        {
            get { return Heros[0].Exp; }
        }

        /// <summary>
        /// 铜钱
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// 元宝
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// 累计充值元宝
        /// </summary>
        public int RechargeGold { get; private set; }

        /// <summary>
        /// 声望
        /// </summary>
        public int Repute { get; set; }

        /// <summary>
        /// 活力（0-200）
        /// </summary>
        public int Energy { get; set; }

        /// <summary>
        /// 角色称号
        /// </summary>
        public int Title { get; set; }

        /// <summary>
        /// VIP等级
        /// </summary>
        public int Vip { get; private set; }

        /// <summary>
        /// 进入游戏时间
        /// </summary>
        public DateTime EnterTime { get; set; }

        /// <summary>
        /// 离开游戏时间
        /// </summary>
        public DateTime LeaveTime { get; set; }

        /// <summary>
        /// 许可权限集合
        /// </summary>
        public PlayerPermission Permission { get; private set; }

        /// <summary>
        /// Id生成器
        /// </summary>
        public IdGen IdGen { get; private set; }

        [BsonIgnore]
        /// <summary>
        /// 对象快照
        /// </summary>
        public Snapshot Snapshot { get; private set; }

        /// <summary>
        /// 家园
        /// </summary>
        public Home Home { get; private set; }

        /// <summary>
        /// 背包
        /// </summary>
        public Bag Bag { get; private set; }

        /// <summary>
        /// 每日功能次数使用历史
        /// </summary>
        public DailyCountHistory DailyCountHistory { get; private set; }

        /// <summary>
        /// 好友列表
        /// </summary>
        public List<string> Friends { get; private set; }

        /// <summary>
        /// 黑名单
        /// </summary>
        public List<string> Blacklist { get; private set; }

        /// <summary>
        /// 英雄列表
        /// </summary>
        public List<Hero> Heros { get; private set; }

        /// <summary>
        /// 英雄最大容量
        /// </summary>
        public int HeroSpace { get; set; }

        /// <summary>
        /// 点亮的战魂品质集合,1-5
        /// </summary>
        public HashSet<int> LightSoulQualities { get; private set; }

        /// <summary>
        /// 点亮的英雄品质集合
        /// </summary>
        public HashSet<int> LightHeroQualities { get; private set; }

        /// <summary>
        /// 战魂仓库
        /// </summary>
        public SoulWarehouse SoulWarehouse { get; private set; }

        /// <summary>
        /// 锁定的任务
        /// </summary>
        public HashSet<int> LockedTasks { get; private set; }

        /// <summary>
        /// 可以接受的任务（由于任务自动接取，此属性无效）
        /// </summary>
        public HashSet<int> OpenedTasks { get; private set; }

        /// <summary>
        /// 已经接受的任务
        /// </summary>
        public Dictionary<int, TaskProgress> ProcessTasks { get; private set; }

        /// <summary>
        /// 已结束的任务
        /// </summary>
        public HashSet<int> ClosedTasks { get; private set; }

        /// <summary>
        /// 随机任务
        /// </summary>
        public RandomTask RandomTask { get; private set; }

        /// <summary>
        /// 已完成的副本
        /// </summary>
        public HashSet<int> FinishedCopies { get; private set; }

        [BsonIgnore]
        /// <summary>
        /// 当前副本实例
        /// </summary>
        public Copy CurrentCopy { get; set; }

        [BsonIgnore]
        /// <summary>
        /// 玩家临时数据
        /// </summary>
        public PlayerTemp Temp { get; private set; }

        /// <summary>
        /// 签到
        /// </summary>
        public SignIn SignIns { get; set; }

        public Hero GetMainHero()
        {
            return Heros.FirstOrDefault(h => h.Setting.IsMain);
        }

        public void Load()
        {
            OnLoad();
            foreach (var entity in GetAllEntities())
                entity.Load(this);
        }

        private IEnumerable<Entity> GetAllEntities()
        {
            yield return Home;
            foreach (var building in Home.Buildings)
                yield return building;
            yield return Bag;
            foreach (var item in Bag.GoodsBag)
                yield return item;
            foreach (var item in Bag.MaterialsBag)
                yield return item;
            foreach (var item in Bag.TempBag)
                yield return item;
            foreach (var hero in Heros)
            {
                yield return hero;
                foreach (var equip in hero.Equips)
                    yield return equip;
                foreach (var soul in hero.Souls)
                    yield return soul;
            }
            foreach (var soul in SoulWarehouse.TempSouls)
                yield return soul;
            foreach (var soul in SoulWarehouse.BackSouls)
                yield return soul;
        }

        private void OnLoad()
        {
            CreateTime = CreateTime.ToLocalTime();
            EnterTime = EnterTime.ToLocalTime();
            LeaveTime = LeaveTime.ToLocalTime();
        }

        public BattleGroup CreateBattleGroup(int startId)
        {
            return new BattleGroup(BattleGroupType.Player, Name, Sex, Heros.Where(h => h.Pos > 0).Select(h =>
                { h.Refresh(); return new BattleFighter(h, startId++, h.Pos); }));
        }

        public void OnLevelUp(GameSession session, int oldLevel)
        {
            session.Player.GetSummary().Level = Level;
            foreach (var module in session.Server.ModuleFactory.Modules)
                module.RaiseLevelUp(session.Player, oldLevel);
        }

        public void UnlockCommands(GameSession session, int[] commandIds)
        {
            if (commandIds != null && commandIds.Length > 0)
            {
                foreach (var commandId in commandIds)
                {
                    if (commandId > 0)
                    {
                        Permission.Commands.Add(commandId);
                        session.Player.Permission.Add(commandId);
                    }
                }
                session.SendResponse((int)CommandEnum.UnlockCommand, commandIds[0]);
            }
        }

        public void AddRepute(GameSession session, int repute)
        {
            if (repute > 0)
            {
                Repute += repute;
                session.SendResponse((int)CommandEnum.RefreshRepute, Repute);

                var hero = GetMainHero();
                var curIndex = hero.Setting.Quality - 1;

                //声望达到一定值，主角自动转生
                var qualityReputes = APF.Settings.Role.QualityReputes;
                for (var i = qualityReputes.Length - 1; i > curIndex; i--)
                {
                    if (Repute >= qualityReputes[i])
                    {
                        hero.SettingId = APF.Settings.Role.HeroInitId + i + 1;
                        session.SendResponse((int)CommandEnum.HeroSettingChanged, hero.SettingId);
                        break;
                    }
                }              
            }
        }

        public void Recharge(GameSession session, int gold)
        {
            Gold += gold;
            RechargeGold += gold;
            session.SendResponse((int)CommandEnum.RefreshGold, Gold);

            var prizeModule = session.Server.ModuleFactory.Module<IPrizeModule>();
            var vipChanged = false;
            foreach (var vipSetting in APF.Settings.Vips.All.Where(v => v.Id > Vip && v.Gold <= RechargeGold))
            {
                vipChanged = true;
                Vip = vipSetting.Id;
                session.SendResponse((int)CommandEnum.RefreshVip, Vip);
                UnlockCommands(session, vipSetting.CommandIds);
                prizeModule.AddPrize(session.Player.World, Name, vipSetting.OncePrize);
                int befGSize = Bag.GoodsBagSize;
                int befMSize = Bag.MaterialsBagSize; ;
                Bag.GoodsBagSize = Bag.MaterialsBagSize = vipSetting.BagSize * APF.Settings.Bag.CapacityExpandSize;
                if (befGSize != Bag.GoodsBagSize)
                {
                    session.SendResponse((int)CommandEnum.ExpandBag, new ExpandBagArgs() { Capacity = Bag.GoodsBagSize, whichBag = BagType.GoodBag });
                }
                if (befMSize != Bag.MaterialsBagSize)
                {
                    session.SendResponse((int)CommandEnum.ExpandBag, new ExpandBagArgs() { Capacity = Bag.MaterialsBagSize, whichBag = BagType.MaterialBag });
                }            
            }
            if (vipChanged)
                session.Player.GetSummary().Vip = Vip;
        }

        //从开服时间算起签到第n天,30天一个循环，从0开始
        public int SignDayth() 
        {
            var totalDays = (int)(DateTime.Now - Global.Config.PublishDate).TotalDays;
            return (int)totalDays % APF.Settings.Role.SignInConDays;
        }
    }
}
