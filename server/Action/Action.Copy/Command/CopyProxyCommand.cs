using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Copy.Command
{
    [GameCommand((int)CommandEnum.CopyProxy)]
    public class CopyProxyCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            var copySetting = APF.Settings.Copies.Find(args);
            if (copySetting == null)
                return;

            //验证是否已在副本中
            var player = session.Player.Data.AsDbPlayer();
            if (player.CurrentCopy != null)
            {
                session.SendError(ErrorCode.AlreadyInCopy);
                return;
            }

            //验证是否有权限
            if (!player.Permission.Copies.Contains(args))
            {
                session.SendError(ErrorCode.EnterCopyRightNotEngouth);
                return;
            }

            //验证是否已通关
            if (!player.FinishedCopies.Contains(args))
            {
                session.SendError(ErrorCode.CopyNotFinished);
                return;
            }

            //验证体力是否足够
            if (player.Energy < copySetting.EnterConsumable.Energy)
            {
                session.SendError(ErrorCode.EnergyNotEnough);
                return;
            }

            //扣除体力
            player.Energy -= copySetting.EnterConsumable.Energy;
            session.SendResponse((int)CommandEnum.RefreshEnergy, player.Energy);

            //开始扫荡逻辑
            var styles = copySetting.Styles;
            var styleOptions = copySetting.StyleOptions;
            var battleSettings = APF.Settings.Battles;
            var prize = new Prize();
            var monsterList = new List<IdCountPair>();
            foreach (var pos in GetRandomPoses(styles.Length))
            {
                switch ((GridStyle)styles[pos])
                {
                    case GridStyle.Money:
                        prize.Merge(styleOptions.Money.Random());
                        break;
                    case GridStyle.Material:
                        prize.Merge(styleOptions.Material.Random());
                        break;
                    case GridStyle.Box:
                        prize.Merge(styleOptions.Box.Random());
                        break;
                    case GridStyle.Monster:
                        var battle = battleSettings.Find(styleOptions.Monster.Random());
                        prize.Merge(battle.WinnerPrize);
                        monsterList.AddRange(battle.ToMonsterCounts());
                        break;
                    case GridStyle.Random:
                        var rnd = APF.Random.Range(1, 4);
                        if (rnd == 1)
                            goto case GridStyle.Money;
                        else if (rnd == 2)
                            goto case GridStyle.Material;
                        else if (rnd == 3)
                            goto case GridStyle.Card;
                        else
                            goto case GridStyle.Monster;
                    case GridStyle.Boss:
                        battle = battleSettings.Find(styleOptions.Boss);
                        prize.Merge(battle.WinnerPrize);
                        monsterList.AddRange(battle.ToMonsterCounts());
                        break;
                    case GridStyle.Card:
                        prize.Merge(styleOptions.Card.Where(c => APF.Random.Percent(c.Rate)).Random().Prize);
                        break;
                }
            }
            var msg = new CopyProxyArgs();
            msg.CopyId = args;
            msg.Prize = prize.Open(session, PrizeSource.CopyPass, false);
            session.SendResponse(ID, msg);

            //任务处理
            var taskModule = session.Server.ModuleFactory.Module<ITaskModule>();
            var idCounts = monsterList.GroupBy(m => m.Id)
                .Select(p => new IdCountPair() { Id = p.Key, Count = p.Count() });
            foreach (var idCount in idCounts)
                taskModule.OnEventHandled(session.Player, player, TaskType.KillMonster, idCount);
            taskModule.OnEventHandled(session.Player, player, TaskType.PassCopy, args);
        }

        /// <summary>
        /// 模拟投骰子，获取路径
        /// </summary>
        /// <param name="girdCount"></param>
        /// <returns></returns>
        private IEnumerable<int> GetRandomPoses(int girdCount)
        {
            var list = new List<int>();
            var currentPos = 0;
            while (currentPos < girdCount - 1)
            {
                var dice = APF.Random.Range(1, 6);
                currentPos += dice;
                list.Add(currentPos);
            }
            list[list.Count - 1] = girdCount - 1;
            return list;
        }
    }
}
