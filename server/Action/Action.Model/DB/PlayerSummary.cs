using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using Action.Engine;

namespace Action.Model
{
    public class PlayerSummary
    {
        public static PlayerSummary Create(World world, string account, string name, int sex)
        {
            return new PlayerSummary()
            {
                World = world,
                Account = account,
                Name = name,
                Sex = sex,
                Level = 1,
                Vip = 0,
                OfflineCache = new OfflineCache(),
                PrizeObjs = new List<PrizeObj>(),
                ApplyGuildList = new List<string>(),
                PayOrders = new HashSet<string>(),
                FirstJoinGuild = false,
                ArenaLogs = new List<ArenaLog>()
            };
        }

        [BsonIgnore]
        /// <summary>
        /// 世界
        /// </summary>
        public World World { get; set; }

        [BsonIgnore]
        /// <summary>
        /// 腾讯平台参数
        /// </summary>
        public GameContext Context { get; set; }

        /// <summary>
        /// 玩家账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 玩家名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 玩家等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 玩家vip等级
        /// </summary>
        public int Vip { get; set; }

        /// <summary>
        /// 竞技场积分
        /// </summary>
        public int ArenaScore { get; set; }

        /// <summary>
        /// 竞技场排名
        /// </summary>
        public int ArenaRank { get; set; }

        /// <summary>
        /// 竞技场最好排名
        /// </summary>
        public int ArenaBestRank { get; set; }

        /// <summary>
        /// 竞技场日志
        /// </summary>
        public List<ArenaLog> ArenaLogs { get; set; }

        /// <summary>
        /// 是否第一次入公会
        /// </summary>
        public bool FirstJoinGuild { get; set; }

        /// <summary>
        /// 所属公会名
        /// </summary>
        public string GuildName { get; set; }

        /// <summary>
        /// 申请的公会列表
        /// </summary>
        public List<string> ApplyGuildList { get; set; }

        /// <summary>
        /// 竞技场对手
        /// </summary>
        public string[] ArenaTargets { get; set; }

        /// <summary>
        /// 离线缓存
        /// </summary>
        public OfflineCache OfflineCache { get; private set; }

        /// <summary>
        /// 奖励集合
        /// </summary>
        public List<PrizeObj> PrizeObjs { get; private set; }

        /// <summary>
        /// 已创建的副本实例
        /// </summary>
        public Copy Copy { get; set; }

        /// <summary>
        /// 未完成的支付订单
        /// </summary>
        public HashSet<string> PayOrders { get; private set; }

        private DateTime _unlockTime;
        /// <summary>
        /// 解锁时间
        /// </summary>
        public DateTime UnlockTime
        {
            get { return _unlockTime; }
            set { _unlockTime = value.ToLocalTime(); }
        }

        private DateTime _speakTime;
        /// <summary>
        /// 恢复说话能力的时间
        /// </summary>
        public DateTime SpeakTime
        {
            get { return _speakTime; }
            set { _speakTime = value.ToLocalTime(); }
        }
    }
}
