using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Guild.Command
{
    public abstract class GuildCommandBase : GameCommand
    {
        protected override bool Ready(GameSession session)
        {
            return base.Ready(session) && session.IsCommandReady(ID);
        }
    }

    public abstract class GuildCommandBase<T> : GameCommand<T>
    {
        protected override bool Ready(GameSession session, T args)
        {
            return base.Ready(session, args) && session.IsCommandReady(ID);
        }
    }
}
