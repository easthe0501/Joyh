using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;
using Action.Core;

namespace Action.Guild.Command
{
    [GameCommand((int)CommandEnum.ApplyList)]
    public class ApplyListCommand : GuildCommandBase
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            Model.Guild guild = session.Player.GetGuild();

            if (guild.Members.GetValue(player.Name).Post == GuildPost.Common)
            {
                session.SendError(ErrorCode.NotHasGuildRight);
                return;
            }

            ApplyListArgs applyListArgs = new ApplyListArgs();
            foreach (var applyPlayer in guild.ApplyJoinList)
            {
                var p = APF.LoadPlayer(session.Player, applyPlayer);
                applyListArgs.Players.Add(new ApplyPlayerArgs() { Name = p.Name, Level = p.Level, Viplevel = p.Vip });
            }

            session.SendResponse(ID, applyListArgs);
        }
    }
}
