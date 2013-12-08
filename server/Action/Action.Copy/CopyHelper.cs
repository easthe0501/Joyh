using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using System.Diagnostics;

namespace Action.Copy
{
    public static class CopyHelper
    {
        public static void LeaveCopy(this GameSession session)
        {
            session.Player.Data.AsDbPlayer().CurrentCopy = null;
            session.SendResponse((int)CommandEnum.LeaveCopy);
        }

        public static void PassCopy(this GameSession session)
        {
            var moduleFactory = session.Server.ModuleFactory;

            //解锁后续副本
            var player = session.Player.Data.AsDbPlayer();
            var copySetting = player.CurrentCopy.Setting;
            var nextCopy = copySetting.Next();
            if (nextCopy != null && nextCopy.Unlock(player))
                session.SendResponse((int)CommandEnum.UnlockCopy, nextCopy.Id);

            //获取帮派贡献度
            var guildContribution = copySetting.PassPrize.GuildContribution;
            if (guildContribution > 0)
                moduleFactory.Module<IGuildModule>().AddContribute(session.Player, guildContribution);

            if (!player.FinishedCopies.Contains(copySetting.Id))
            {
                //加入到已完成副本列表
                player.FinishedCopies.Add(copySetting.Id);

                //生成通关奖励
                player.Temp.CopyPassPrize = copySetting.PassPrize.Prize;

                //解锁新伙伴
                var unlockHeroIds = copySetting.PassPrize.UnlockHeros;
                if (unlockHeroIds != null && unlockHeroIds.Length > 0)
                {
                    moduleFactory.Module<IHeroModule>().UnlockHeros(session, player,
                        copySetting.PassPrize.UnlockHeros);
                }
            }

            //任务事件
            moduleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.PassCopy, copySetting.Id);
        }

        public static CopyMember GetMember(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var copyInstance = player.CurrentCopy;
            if (copyInstance == null)
            {
                session.SendError(ErrorCode.CopyInstanceMissing);
                return null;
            }
            var member = copyInstance.GetMemberOnTurn();
            if (member.Player != player)
            {
                session.SendError(ErrorCode.NotYourCopyTurn);
                return null;
            }
            
            return member;
        }

        public static CopyAction CastMove(GameSession session, CopyMember member, int dice)
        {
            //如果当前玩家从起点开始移动，则扣除体力
            if (member.Pos == 0)
            {
                member.Player.Energy -= member.Instance.Setting.EnterConsumable.Energy;
                session.SendResponse((int)CommandEnum.RefreshEnergy, member.Player.Energy);
            }

            //如果已经到达终点，则不进行操作
            var endPos = member.Instance.Grids.Length - 1;
            if (member.Pos == endPos)
                return null;

            //根据骰子点数更新当前位置
            member.Pos += dice;
            if (member.Pos > endPos)
                member.Pos = endPos;
            var grid = member.Instance.Grids[member.Pos];

            //执行当前位置所在格子的功能，并得到副本行为对象
            var strategy = Strategy.CopyGridStrategyFactory.GetStrategy(grid.Type);
            //Debug.Assert(strategy != null && member.Player != null);
            return new CopyAction()
            {
                Player = member.Player.Name,
                Pos = member.Pos,
                Grid = strategy.Run(session, member, grid.Data)
            };
        }
    }
}
