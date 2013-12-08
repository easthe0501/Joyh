using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Guild
{
    public static class GuildHelper
    {
        public static bool IsCommandReady(this GameSession session, int commandId)
        {
            var guild = session.Player.GetGuild();
            if (guild == null)
            {
                session.SendError(ErrorCode.NotInGuild);
                return false;
            }
            if (!guild.Permission.Commands.Contains(commandId))
            {
                session.SendError(ErrorCode.NotHasGuildRight);
                return false;
            }
            return true;
        }
    }
}
