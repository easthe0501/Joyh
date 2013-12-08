using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Guild.Command
{
    public class ContributeForGuildCommandBase : GuildCommandBase
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            Model.Guild guild = session.Player.GetGuild();

            var member = guild.Members.GetValue(player.Name);
            if (player.DailyCountHistory.ContributeForGuild >= APF.Settings.Guild.DailyContributeCount)
                return;
            var setting = APF.Settings.GuildContributes.Find(ID);
            if (player.Vip < setting.Vip)
            {
                session.SendError(ErrorCode.VipNotEnough);
                return;
            }
            if (setting.CostMoney > 0)
            {
                if (player.Money < setting.CostMoney)
                {
                    session.SendError(ErrorCode.MoneyNotEnough);
                    return;
                }
                player.Money -= setting.CostMoney;
                session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            }
            if (setting.CostGold > 0)
            {
                if (player.Gold < setting.CostGold)
                {
                    session.SendError(ErrorCode.GoldNotEnough);
                    return;
                }
                player.Gold -= setting.CostGold;
                session.SendResponse((int)CommandEnum.RefreshGold, player.Gold);
            }

            player.DailyCountHistory.ContributeForGuild++;
            member.TodayContribution += setting.IncomeContribution;
            member.TotalContribution += setting.IncomeContribution;
            player.Repute += setting.IncomeRepute;
            session.SendResponse((int)CommandEnum.RefreshRepute, player.Repute);

            var oldLevel = guild.Level;
            if (guild.GetExp(setting.IncomeContribution))
                guild.OnLevelUp(oldLevel);

            session.SendResponse(ID);
        }
    }

    [GameCommand((int)CommandEnum.ContributeForGuild1)]
    public class ContributeForGuild1Command : ContributeForGuildCommandBase
    {
    }

    [GameCommand((int)CommandEnum.ContributeForGuild2)]
    public class ContributeForGuild2Command : ContributeForGuildCommandBase
    {
    }

    [GameCommand((int)CommandEnum.ContributeForGuild3)]
    public class ContributeForGuild3Command : ContributeForGuildCommandBase
    {
    }

    [GameCommand((int)CommandEnum.ContributeForGuild5)]
    public class ContributeForGuild5Command : ContributeForGuildCommandBase
    {
    }
    
    [GameCommand((int)CommandEnum.ContributeForGuild7)]
    public class ContributeForGuild7Command : ContributeForGuildCommandBase
    {
    }
    
    [GameCommand((int)CommandEnum.ContributeForGuild9)]
    public class ContributeForGuild9Command : ContributeForGuildCommandBase
    {
    }
    
    [GameCommand((int)CommandEnum.ContributeForGuild11)]
    public class ContributeForGuild11Command : ContributeForGuildCommandBase
    {
    }
}
