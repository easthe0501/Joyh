using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Guild.Command
{
    [GameCommand((int)CommandEnum.LoadGuild)]
    public class LoadGuildCommand : GameCommand<string>
    {
        protected override void Run(GameSession session, string arg)
        {
            var player = APF.LoadPlayer(session.Player, arg);
            if (player == null)
                return;
            var world = session.Server.World.Data.AsDbWorld();
            var playerSummary = session.Server.World.GetSummary(player.Name);
            if (playerSummary == null)
                return;
            if (arg == session.Player.Name && string.IsNullOrEmpty(playerSummary.GuildName))
            {
                session.SendResponse((int)CommandEnum.PlayerNoGuild, -1);
                return;
            }
            Model.Guild guild = world.Guilds.GetValue(playerSummary.GuildName);
            
            GuildArgs guildArgs = new GuildArgs();
            guildArgs.LeaderName = guild.Members.SingleOrDefault(p => p.Value.Post == GuildPost.Leader).Value.Name;
            guildArgs.GuildName = guild.Name;
            guildArgs.Notice = guild.Notice;
            guildArgs.Level = guild.Level;
            guildArgs.GuildExp = guild.Exp;
            guildArgs.MemberMaxCount = guild.MemberMaxCount;
            guildArgs.GuildRank = guild.Rank;
            foreach (GuildMember gm in guild.Members.Values.OrderByDescending(m => m.Post))
            {
                var gmSummary = session.Server.World.GetSummary(gm.Name);
                guildArgs.Members.Add(new GuildMemberArgs()
                {
                    Name = gm.Name,
                    Post = (PostType)gm.Post,
                    TodayContribution = gm.TodayContribution,
                    TotalContribution = gm.TotalContribution,
                    IfOnline = session.Server.World.IsOnline(gm.Name),
                    Level = gmSummary.Level,
                    MemberVip = gmSummary.Vip,
                    ArenaRank = gmSummary.ArenaRank,
                    Sex = gmSummary.Sex
                });
            }
            session.SendResponse(ID, guildArgs);
        }
    }
}
