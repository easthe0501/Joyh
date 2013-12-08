using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Guild.Command
{
    [GameCommand((int)CommandEnum.QuitGuild)]
    public class QuitGuildCommand : GuildCommandBase
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var world = session.Server.World.Data.AsDbWorld();
            var playerSummaries = world.GetSummary(player.Name);
            Model.Guild guild = session.Player.GetGuild();

            //帮会中不止帮主一人时，帮主不能退出帮会
            if (guild.Members.GetValue(player.Name).Post == GuildPost.Leader && guild.Members.Count > 1)
            {
                session.SendError(ErrorCode.LeaderCannotQuit);
                return;
            }

            playerSummaries.GuildName = null;
            GuildMember member = null;
            guild.Members.TryRemove(player.Name, out member);
            //记录日志
            GuildLogArgs log = new GuildLogArgs();
            log.Type = GuildLogType.Leave;
            log.Player = member.Name;
            log.Time = MyConvert.ToSeconds(DateTime.Now);
            guild.AddLog(session.Server.World, log);

            if (guild.Members.Count == 0)
            {
                world.Guilds.TryRemove(guild.Name, out guild);
                foreach (var summaries in world.Summaries.Values)
                {
                    if (summaries.ApplyGuildList.Contains(guild.Name))
                        summaries.ApplyGuildList.Remove(guild.Name);
                }
            }
            session.SendResponse(ID);
        }
    }
}
