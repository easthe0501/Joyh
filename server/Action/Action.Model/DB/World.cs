using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Timers;
using MongoDB.Bson.Serialization.Attributes;
using Action.Core;
using Action.Utility;
using Action.Engine;

namespace Action.Model
{
    public class World : BsonClass, IWorldData
    {
        public const string Cls = "World";
        private const int TopCount = 25;
        private const int Interval = 1000;
        private Timer _timer;

        public World()
        {
            //_gameWorld = ServerContext.GameServer.World;
            _timer = new Timer(Interval);
            _timer.Elapsed += OnUpdate;
            _timer.Start();
        }

        public void Dispose()
        {
            _timer.Stop();
        }

        [BsonIgnore]
        public IEnumerable<PlayerSummary> LevelTops { get; private set; }

        [BsonIgnore]
        public IEnumerable<Guild> SortedGuilds { get; private set; }

        /// <summary>
        /// Id生成器
        /// </summary>
        public IdGen IdGen { get; private set; }

        /// <summary>
        /// 玩家摘要字典集合
        /// </summary>
        public ConcurrentDictionary<string, PlayerSummary> Summaries { get; private set; }

        /// <summary>
        /// 玩家账户和名称的映射
        /// </summary>
        public ConcurrentDictionary<string, string> Accounts { get; private set; }

        /// <summary>
        /// 玩家支付流水集合
        /// </summary>
        public ConcurrentDictionary<string, PayOrder> PayOrders { get; private set; }

        /// <summary>
        /// 战报字典集合
        /// </summary>
        public ConcurrentDictionary<string, BattleReport> BattleReports { get; private set; }

        /// <summary>
        /// 帮派字典集合
        /// </summary>
        public ConcurrentDictionary<string, Guild> Guilds { get; private set; }

        /// <summary>
        /// 竞技场对象
        /// </summary>
        public BattleArena BattleArena { get; private set; }

        public void Init()
        {
            IdGen = new IdGen();
            BattleArena = new Model.BattleArena();
            Summaries = new ConcurrentDictionary<string, PlayerSummary>();
            Accounts = new ConcurrentDictionary<string, string>();
            PayOrders = new ConcurrentDictionary<string, PayOrder>();
            BattleReports = new ConcurrentDictionary<string, BattleReport>();
            Guilds = new ConcurrentDictionary<string, Guild>();
            Load();
        }

        public void Load()
        {
            foreach (var summary in Summaries.Values)
                summary.World = this;
            OnUpdate(null, null);
        }

        private void OnUpdate(object sender, ElapsedEventArgs e) 
        {
            SortedGuilds = Guilds.Values.OrderByDescending(g => g.Level);
            var guildRank = 0;
            foreach (var guild in SortedGuilds)
                guild.Rank = ++ guildRank;

            var now = DateTime.Now;
            if (now.Minute == 0 && now.Second == 0)
            {
                LevelTops = Summaries.Values.OrderByDescending(s => s.Level).Take(TopCount);
                ////ArenaSorts = Summaries.Values.OrderByDescending(s => s.ArenaScore);
                ////ArenaTops = ArenaSorts.Take(TopCount);
                ////var arenaRank = 0;
                ////foreach (var ast in ArenaSorts)
                ////    ast.ArenaRank = ++arenaRank;
            }
        }

        public PlayerSummary CreateSummary(string account, string name, int sex)
        {
            var summary = PlayerSummary.Create(this, account, name, sex);
            Summaries[name] = summary;
            Accounts[account] = name;
            return summary;
        }

        public PlayerSummary GetSummary(string name)
        {
            return Summaries.GetValue(name);
        }

        public PlayerSummary GetSummaryByAcc(string acc)
        {
            var name = Accounts.GetValue(acc);
            return name != null ? GetSummary(name) : null;
        }

        public bool ContainsSummary(string name)
        {
            return Summaries.ContainsKey(name);
        }
    }
}
