using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Guild.Command
{
    [GameCommand((int)CommandEnum.RemoveGuildMember)]
    public class RemoveGuildMemberCommand : GuildCommandBase<string>
    {
        protected override void Run(GameSession session, string args)
        {
            var player = session.Player.Data.AsDbPlayer();
            var world = session.Server.World.Data.AsDbWorld();
            Model.Guild guild = session.Player.GetGuild();

            if (guild.Members.GetValue(player.Name).Post == GuildPost.Common)
            {
                session.SendError(ErrorCode.NotHasGuildRight);
                return;
            }

            //删除帮派中成员信息
            GuildMember guildMember = guild.Members.GetValue(args);
            if (guildMember == null)
                return;
            if (guildMember.Post != GuildPost.Common)
            {
                session.SendError(ErrorCode.NotHasGuildRight);
                return;
            }
            guild.Members.TryRemove(args, out guildMember);
            //删除玩家的所属帮派名
            var playerSummariesMember = world.GetSummary(guildMember.Name);
            if (playerSummariesMember != null)
                playerSummariesMember.GuildName = null;

            //记录日志
            GuildLogArgs log = new GuildLogArgs();
            log.Type = GuildLogType.Leave;
            log.Player = args;
            log.Time = MyConvert.ToSeconds(DateTime.Now);
            guild.AddLog(session.Server.World, log);
            
            session.SendResponse(ID, args);

        }
    }
}