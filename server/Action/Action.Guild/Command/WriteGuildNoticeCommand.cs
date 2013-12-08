using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;
using Action.Utility;

namespace Action.Guild.Command
{
    [GameCommand((int)CommandEnum.WriteGuildNotice)]
    public class WriteGuildNoticeCommand : GameCommand<string>
    {
        protected override void Run(GameSession session, string args)
        {
            if (!WordValidateHelper.FilterForBool(args))
            {
                session.SendError(ErrorCode.GuildNoticeFilter);
                return;
            }
            var player = session.Player.Data.AsDbPlayer();
            Model.Guild guild = session.Player.GetGuild();

            if (guild.Members.GetValue(player.Name).Post == GuildPost.Common)
            {
                session.SendError(ErrorCode.NotHasGuildRight);
                return;
            }

            guild.Notice = args;
            session.SendResponse(ID, args);
        }
    }
}
