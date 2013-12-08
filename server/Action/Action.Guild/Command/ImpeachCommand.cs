using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Guild.Command
{
    [GameCommand((int)CommandEnum.Impeach)]
    public class ImpeachCommand:GuildCommandBase
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            Model.Guild guild = session.Player.GetGuild();
            //花费元宝
            if (player.Gold < APF.Settings.Guild.ImpeachNeedGold)
            {
                session.SendError(ErrorCode.GoldNotEnough);
                return;
            }
            //帮主连续n天不在线
            GuildMember playerInGuild = guild.Members.Values.SingleOrDefault(m => m.Post == GuildPost.Leader);
            var guildLeader = APF.LoadPlayer(session.Player, playerInGuild.Name);
            if (guildLeader.LeaveTime > guildLeader.EnterTime || (DateTime.Now - guildLeader.LeaveTime).TotalDays < 5.0)
            {
                session.SendError(ErrorCode.CannotImpeach);
                return;
            }

            //弹劾成功，弹劾者成为帮主，原帮主变为普通成员
            GuildMember meGuild = guild.Members.GetValue(player.Name);
            playerInGuild.Post = GuildPost.Common;
            meGuild.Post = GuildPost.Leader;

            //记录日志
            GuildLogArgs log = new GuildLogArgs();
            log.Type = GuildLogType.Impeach;
            log.Player = player.Name;
            log.Time = MyConvert.ToSeconds(DateTime.Now);
            log.Args.Add((int)GuildPost.Leader);
            guild.AddLog(session.Server.World, log);

            session.SendResponse(ID);

        }
    }
}
