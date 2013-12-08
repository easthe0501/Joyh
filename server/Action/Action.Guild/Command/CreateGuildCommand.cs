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
    [GameCommand((int)CommandEnum.CreateGuild)]
    public class CreateGuildCommand : GameCommand<string>
    {
        protected override void Run(GameSession session, string args)
        {
            if (string.IsNullOrEmpty(args))
            {
                session.SendError(ErrorCode.GuildNameNotNull);
                return;
            }
            if (args.Length > APF.Settings.Guild.NameMaxLength)
            {
                session.SendError(ErrorCode.GuildNameLengthError);
                return;
            }
            if (!WordValidateHelper.FilterForBool(args))
            {
                session.SendError(ErrorCode.GuildNameFilter);
                return;
            }
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
            if (player.Level < APF.Settings.Guild.CreateNeedLevel)
            {
                session.SendError(ErrorCode.LevelNotEnough);
                return;
            }
            if (player.Money < APF.Settings.Guild.CreateCostMoney)
            {
                session.SendError(ErrorCode.MoneyNotEnough);
                return;
            }
            if (world.Guilds.Keys.Contains(args))
            {
                session.SendError(ErrorCode.RepeatGuildName);
                return;
            }

            
            //记录玩家所属帮派名
            playerSummaries.GuildName = args;

            Model.Guild newGuild = Model.Guild.Create(args);
            GuildMember leader = GuildMember.Create(player);
            leader.Post = GuildPost.Leader;
            newGuild.Members[player.Name] = leader;
            newGuild.MemberMaxCount = APF.Settings.Guild.InitMemberMaxCount;
            world.Guilds[newGuild.Name] = newGuild;
            //清除申请列表
            playerSummaries.ApplyGuildList.Clear();
            foreach (var g in world.Guilds.Values)
            {
                if (g.ApplyJoinList.Contains(player.Name))
                    g.ApplyJoinList.Remove(player.Name);
            }

            player.Money -= APF.Settings.Guild.CreateCostMoney;
            session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            //GuildArgs guildArgs = new GuildArgs();
            //guildArgs.GuildName = args;
            //guildArgs.MemberMaxCount = APF.Settings.Guild.InitMemberMaxCount;
            //guildArgs.Members.Add(new GuildMemberArgs() { Name = player.Name, Post = (int)GuildPost.Leader, TodayContribution = 0, TotalContribution = 0 });

            session.SendResponse((int)CommandEnum.JoinGuild, args);
            session.SendResponse(ID);

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.JoinGuild, null);
        }
    }
}
