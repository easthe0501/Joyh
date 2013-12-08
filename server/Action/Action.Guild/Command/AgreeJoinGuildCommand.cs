using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Guild.Command
{
    [GameCommand((int)CommandEnum.AgreeJoinGuild)]
    public class AgreeJoinGuildCommand : GuildCommandBase<AgreeJoinArgs>
    {
        protected override void Run(GameSession session, AgreeJoinArgs args)
        {
            var player = session.Player.Data.AsDbPlayer();
            var world = session.Server.World.Data.AsDbWorld();
            Model.Guild guild = session.Player.GetGuild();

            if (guild.Members.GetValue(player.Name).Post == GuildPost.Common)
            {
                session.SendError(ErrorCode.NotHasGuildRight);
                return;
            }

            if (guild.Members.Count >= guild.MemberMaxCount)
            {
                session.SendError(ErrorCode.GuildIsFull);
                args.IfAgree = false;
            }

            if (!guild.ApplyJoinList.Contains(args.Name))
                return;

            if (guild.Members.Count >= guild.MemberMaxCount)
            {
                session.SendError(ErrorCode.GuildIsFull);
                return;
            }

            var memberSummaries = world.GetSummary(args.Name);
            if (args.IfAgree)
            {
                //被同意加入帮派的玩家
                var gmember = APF.LoadPlayer(session.Player, args.Name);
                GuildMember guildMember = GuildMember.Create(gmember);
                guildMember.Post = GuildPost.Common;
                guild.Members[args.Name] = guildMember;
                //加入帮派，玩家的帮派申请列表清空
                memberSummaries.ApplyGuildList.Clear();
                memberSummaries.GuildName = guild.Name;
                //如果玩家在线，发送玩家加入的宗派名称
                var memberPlayer = session.Server.World.GetPlayer(args.Name);
                if(memberPlayer != null)
                    memberPlayer.Session.SendResponse((int)CommandEnum.JoinGuild, guild.Name);
            }
            else
            {
                memberSummaries.ApplyGuildList.Remove(guild.Name);
            }
            //清除申请列表
            guild.ApplyJoinList.Remove(args.Name);
            foreach (var g in world.Guilds.Values)
            {
                if (g.ApplyJoinList.Contains(args.Name))
                    g.ApplyJoinList.Remove(args.Name);
            }
            if (args.IfAgree)
            {
                //记录日志
                GuildLogArgs log = new GuildLogArgs();
                log.Type = GuildLogType.Join;
                log.Player = args.Name;
                log.Time = MyConvert.ToSeconds(DateTime.Now);
                guild.AddLog(session.Server.World, log);
            }

            session.SendResponse(ID, args);
        }
    }
}