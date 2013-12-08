using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Guild.Command
{
    [GameCommand((int)CommandEnum.LoadGuilds)]
    public class LoadGuildsCommand : GameCommand<AskForGuildsArgs>
    {
        protected override bool Ready(GameSession session, AskForGuildsArgs args)
        {
            return base.Ready(session, args) && args.Page > 0;
        }

        protected override void Run(GameSession session, AskForGuildsArgs args)
        {
            if (args.Page <= 0)
                return;
            LoadGuildsArgs loadGuildsArgs = new LoadGuildsArgs();
            var player = session.Player.Data.AsDbPlayer();
            var world = session.Server.World.Data.AsDbWorld();
            var playerSummaries = world.GetSummary(player.Name);
            if (playerSummaries == null)
                return;
            var guilds = world.SortedGuilds;
            List<Model.Guild> gs = new List<Model.Guild>();

            if (string.IsNullOrEmpty(args.GuildName) && string.IsNullOrEmpty(args.GuildLeader))
            {
                //查看人数未满的帮派信息
                if (!args.GuildIsFull)
                    guilds = world.SortedGuilds.Where(p => p.Members.Count < p.MemberMaxCount);
                //设置帮派总页数
                loadGuildsArgs.Pages = (guilds.Count() - 1) / APF.Settings.Guild.GuildsAPage + 1;
                //若页码超出了最大页数
                if (args.Page > loadGuildsArgs.Pages)
                    args.Page = loadGuildsArgs.Pages;
                gs = guilds.Skip(APF.Settings.Guild.GuildsAPage * (args.Page - 1)).Take(APF.Settings.Guild.GuildsAPage).ToList();
            }
            else if (!string.IsNullOrEmpty(args.GuildName))
            {
                loadGuildsArgs.Pages = 1;
                var guild = world.Guilds.GetValue(args.GuildName);
                if (guild != null)
                    gs.Add(guild);
            }
            else if (!string.IsNullOrEmpty(args.GuildLeader))
            {
                loadGuildsArgs.Pages = 1;
                var pSummaries = world.Summaries.GetValue(args.GuildLeader);
                if (pSummaries != null)
                    gs.Add(world.Guilds.GetValue(pSummaries.GuildName));
            }
            foreach(var g in gs)
            {
                if (g.Members.Count == 0)
                    continue;
                var leader = g.Members.Values.SingleOrDefault(p => p.Post == GuildPost.Leader);
                loadGuildsArgs.Guild.Add(new GuildBriefArgs()
                {
                    GuildName = g.Name,
                    MemberMaxCount = g.MemberMaxCount,
                    MemberCount = g.Members.Count,
                    LeaderName = leader.Name,
                    GuildLevel = g.Level,
                    LeaderVip = session.Server.World.GetSummary(leader.Name).Vip,
                    IfApply = (playerSummaries.ApplyGuildList == null || !string.IsNullOrEmpty(playerSummaries.GuildName)) 
                                ? false : playerSummaries.ApplyGuildList.Contains(g.Name)
                });
            }
            session.SendResponse(ID, loadGuildsArgs);
        }
    }
}
