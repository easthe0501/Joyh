using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Guild.Command
{
    [GameCommand((int)CommandEnum.TransferGuild)]
    public class TransferGuildCommand : GameCommand<string>
    {
        protected override void Run(GameSession session, string args)
        { 
            var player = session.Player.Data.AsDbPlayer();
            Model.Guild guild = session.Player.GetGuild();

            //是否有权限
            GuildMember playerInGuild = guild.Members.GetValue(player.Name);
            if (playerInGuild.Post != GuildPost.Leader)
            {
                session.SendError(ErrorCode.NotHasGuildRight);
                return;
            }

            GuildMember member = guild.Members.GetValue(args);
            if (member.Post != GuildPost.Deputy)
            {
                session.SendError(ErrorCode.IsNotDeputy);
                return;
            }
            //更改头衔
            playerInGuild.Post = GuildPost.Deputy;
            member.Post = GuildPost.Leader;
            
            //记录日志
            GuildLogArgs log = new GuildLogArgs();
            log.Type = GuildLogType.Transfer;
            log.Player = member.Name;
            log.Time = MyConvert.ToSeconds(DateTime.Now);
            guild.AddLog(session.Server.World, log);

            session.SendResponse(ID, args);
        }
    }
}
