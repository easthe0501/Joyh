using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Guild.Command
{
    [GameCommand((int)CommandEnum.ChangeGuildPost)]
    public class ChangeGuildPostCommand : GuildCommandBase<ChangeGuildPostArgs>
    {
        protected override void Run(GameSession session, ChangeGuildPostArgs args)
        {
            var player = session.Player.Data.AsDbPlayer();
            Model.Guild guild = session.Player.GetGuild();

            if (guild.Members.GetValue(player.Name).Post != GuildPost.Leader)
            {
                session.SendError(ErrorCode.NotHasGuildRight);
                return;
            }

            if (args.ToPost == (PostType)GuildPost.Deputy)
            {
                if (guild.Members.Values.Where(m => m.Post == GuildPost.Deputy).Count() >= APF.Settings.Guild.ManagerMaxCount)
                {
                    session.SendError(ErrorCode.GuildManagerOverMax);
                    return;
                }
            }

            GuildMember guildMember = guild.Members.GetValue(args.MemberName);
            //职位无变化
            if (guildMember.Post == (GuildPost)args.ToPost)
                return;

            guildMember.Post = (GuildPost)args.ToPost;

            //记录日志
            GuildLogArgs log = new GuildLogArgs();
            log.Type = GuildLogType.Post;
            log.Player = guildMember.Name;
            log.Time = MyConvert.ToSeconds(DateTime.Now);
            log.Args.Add((int)args.ToPost);
            guild.AddLog(session.Server.World, log);

            session.SendResponse(ID, args);
            
        }
    }
}
