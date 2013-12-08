using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;
using Action.Engine;
using Action.Model;

namespace Action.Guild
{
    public class GuildModule : GameModule, IGuildModule
    {
        public void AddContribute(GamePlayer player, int contribution)
        {
            var guild = player.GetGuild();
            if(guild == null)
                return;
            var member = guild.Members.GetValue(player.Name);
            if(member == null)
                return;
            member.TodayContribution += contribution;
            member.TotalContribution += contribution;
        }
    }
}
