using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using Action.Engine;

namespace Action.Model
{
    public class Guild
    {
        public static Guild Create(string name)
        {
            var guild = new Guild()
            {
                Permission = new GuildPermission(),
                Level = 1,
                Key = Guid.NewGuid().ToString(),
                Name = name,
                Members = new ConcurrentDictionary<string, GuildMember>(),
                Logs = new List<GuildLogArgs>(),
                ApplyJoinList = new List<string>()
            };
            guild.OnLevelUp(0);
            return guild;
        }

        /// <summary>
        /// 帮派权限
        /// </summary>
        public GuildPermission Permission { get; private set; }

        /// <summary>
        /// 唯一标示（暂时不用）
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 帮派名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 帮派公告
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// 帮派等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 帮派经验
        /// </summary>
        public int Exp { get; set; }

        /// <summary>
        /// 帮派排名
        /// </summary>
        public int Rank { get; set; }

        public int GetNextLevelExp()
        {
            return APF.Settings.GuildLevels.Find(Level).Exp;
        }

        /// <summary>
        /// 获取经验
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public bool GetExp(int exp)
        {
            int befLevel = Level;
            exp += Exp;
            var maxLevel = APF.Settings.Guild.GuildMaxLevel;
            while (Level < maxLevel)
            {
                var upExp = GetNextLevelExp();
                if (exp < upExp)
                {
                    Exp = exp;
                    return befLevel != Level ? true : false;
                }
                Level++;
                exp -= upExp;
            }
            Exp = Level == APF.Settings.Guild.GuildMaxLevel ? 0 : exp;
            return true;
        }

        /// <summary>
        /// 最大成员数
        /// </summary>
        public int MemberMaxCount { get; set; }

        /// <summary>
        /// 帮派成员字典集合
        /// </summary>
        public ConcurrentDictionary<string, GuildMember> Members { get; private set; }

        /// <summary>
        /// 帮派日志
        /// </summary>
        public List<GuildLogArgs> Logs { get; private set; }

        /// <summary>
        /// 追加日志
        /// </summary>
        /// <param name="world"></param>
        public void AddLog(GameWorld world, GuildLogArgs log)
        {
            //加入日志队列
            if (Logs.Count >= APF.Settings.Guild.LogMaxCount)
                Logs.RemoveAt(Logs.Count - 1);
            Logs.Add(log);

            //通知在线帮派成员
            foreach (var member in Members.Values)
            {
                var player = world.GetPlayer(member.Name);
                if (player != null)
                    player.Session.SendResponse((int)CommandEnum.GuildLog, log);
            }
        }

        /// <summary>
        /// 请求入帮人员列表
        /// </summary>
        public List<string> ApplyJoinList { get; private set; }

        public void OnLevelUp(int oldLevel)
        {
            foreach (var lvSetting in APF.Settings.GuildLevels.All.Where(l => l.Id > oldLevel && l.Id <= Level))
            {
                if (lvSetting.CommandIds != null)
                {
                    foreach (var commandId in lvSetting.CommandIds)
                    {
                        Permission.Commands.Add(commandId);
                    }
                }
                if (lvSetting.CopyIds != null)
                {
                    foreach (var copyId in lvSetting.CopyIds)
                    {
                        Permission.Copies.Add(copyId);
                    }
                }
            }

            //是否通知刷新公会等级
        }
    }
}
