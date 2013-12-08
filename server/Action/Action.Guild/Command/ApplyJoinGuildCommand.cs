using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Guild.Command
{
    [GameCommand((int)CommandEnum.ApplyJoinGuild)]
    public class ApplyJoinGuildCommand : GameCommand<string>
    {
        protected override void Run(GameSession session, string args)
        {
            var player = session.Player.Data.AsDbPlayer();
            var world = session.Server.World.Data.AsDbWorld();
            var playerSummaries = world.GetSummary(player.Name);
            if (playerSummaries == null)
                return;
            if (!string.IsNullOrEmpty(playerSummaries.GuildName))
            {
                session.SendError(ErrorCode.HasJoinGuild);
                return;
            }
            Model.Guild guild = world.Guilds.GetValue(args);
            if (guild == null)
                return;
            if (guild.Members.Count >= guild.MemberMaxCount)
            {
                session.SendError(ErrorCode.GuildIsFull);
                return;
            }
            //玩家记录申请帮派列表，帮派记录申请列表
            playerSummaries.ApplyGuildList.Add(args);
            guild.ApplyJoinList.Add(player.Name);

            //给在线的帮派的管理人员发送申请信息
            //GuildLogArgs log = new guildlogargs();
            //log.type = guildlogtype.apply;
            //log.player = args;
            //foreach (GuildMember gm in guild.Members.Values)
            //{
            //    var playerSession = session.Server.World.GetPlayer(gm.Name);
            //    if (playerSession == null)
            //        continue;
            //    if (gm.Post != GuildPost.Common)
            //        playerSession.Session.SendResponse((int)CommandEnum.GuildMessage, new GuildMessageArgs() { MessageId = 3, Name = args });
            //}

            session.SendResponse(ID, args);

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.JoinGuild, null);
        }
    }
}
