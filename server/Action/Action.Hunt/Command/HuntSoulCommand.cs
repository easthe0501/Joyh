using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;
using Action.Utility;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.HuntSoul)]
    public class HuntSoulCommand : GameCommand<int>
    {
        protected override bool Ready(GameSession session, int args)
        {
            return base.Ready(session, args) && NumberHelper.Between(args, 1, 5);
        }

        protected override void Run(GameSession session, int args)
        {
            var player = session.Player.Data.AsDbPlayer();

            //没有点亮
            if (!player.LightSoulQualities.Contains(args))
            {
                session.SendError(ErrorCode.HuntQualityNotLight);
                return;
            }

            var thisHunt = APF.Settings.SoulHunts.Find(args);
            if (thisHunt == null)
                return;

            //铜钱不足
            if(player.Money < thisHunt.CostMoney)
            {
                session.SendError(ErrorCode.MoneyNotEnough);
                return;
            }

            //战魂仓库临时空间不足
            if (player.SoulWarehouse.TempSouls.Count >= APF.Settings.Role.SoulWarehouseTempSpace)
            {
                session.SendError(ErrorCode.SoulWarehouseTempSpaceNotEnough);
                return;
            }

            //计算出吉运值
            var randomValue = APF.Random.Next();
            var luckyValue = Math.Min(thisHunt.MaxValue, Math.Max(thisHunt.MinValue, randomValue));

            //自己消失，尝试开启高级项
            if(args != 1)
                player.LightSoulQualities.Remove(args);
            if (args != 5)
            {
                var nextHunt = APF.Settings.SoulHunts.Find(args + 1);
                if (nextHunt == null)
                    return;
                if (luckyValue >= nextHunt.MinValue)
                    player.LightSoulQualities.Add(nextHunt.Id);
            }

            //获取对应的战魂到临时空间
            var reachHunt = default(SoulHuntSetting);
            foreach (var hunt in APF.Settings.SoulHunts.All.OrderByDescending(s => s.Id))
            {
                if (luckyValue >= hunt.ReachValue)
                {
                    reachHunt = hunt;
                    break;
                }
            }
            var soulSettingId = reachHunt.OutputSouls[randomValue % (reachHunt.OutputSouls.Length - 1)];
            var soul = APF.Factory.Create<Soul>(player, soulSettingId);
            soul.Exp = soul.Setting.InitExp;
            player.SoulWarehouse.TempSouls.Add(soul);

            //花费钱
            if (thisHunt.CostMoney > 0)
            {
                player.Money -= thisHunt.CostMoney;
                session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            }

            //给客户端发消息：刷新点亮、临时空间增删
            HuntSoulArgs huntSoulArgs = new HuntSoulArgs();
            foreach (int i in player.LightSoulQualities)
                huntSoulArgs.LightSoulQualities.Add(i);
            huntSoulArgs.TempSoul = new TempSoulArgs() { Id = soul.Id, SettingId = soul.SettingId };
            session.SendResponse(ID, huntSoulArgs);

            //任务事件
            session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                player, TaskType.HuntSoul, soul.Setting.Quality);
        }
    }
}
