using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;

namespace Action.Guild.Command
{
    [GameCommand((int)CommandEnum.GuildLogList)]
    public class GuildLogListCommand : GuildCommandBase
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            Model.Guild guild = session.Player.GetGuild();

            GuildLogListArgs guildLogList = new GuildLogListArgs();
            foreach (var l in guild.Logs)
            {
                GuildLogArgs log = new GuildLogArgs();
                if (l.Args != null)
                {
                    foreach (int i in l.Args)
                        log.Args.Add(i);
                }
                log.Player = l.Player;
                log.Time = l.Time;
                log.Type = l.Type;
                guildLogList.Logs.Add(log);
            }

            session.SendResponse(ID, guildLogList);
        }
    }
}
