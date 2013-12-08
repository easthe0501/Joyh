using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.HuntSouls)]
    public class HuntSoulsCommand:GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            //铜钱不足停止问卦
            //点亮最后一个战停止魂品质停止问卦
            //战魂仓库临时空间不足停止问卦
            //session.Server.ModuleFactory.Module<IHuntModule>().HuntSoul();
            HuntSoulsArgs huntSouls = new HuntSoulsArgs();
            while(true)
            {
                var arg = player.LightSoulQualities.Max();
                if (arg == 5)
                    return;
                
                var thisHunt = APF.Settings.SoulHunts.Find(arg);
                if (thisHunt == null)
                    return;
                if (player.Money < thisHunt.CostMoney)
                {
                    session.SendError(ErrorCode.MoneyNotEnough);
                    break;
                }

                //战魂仓库临时空间不足
                if (player.SoulWarehouse.TempSouls.Count >= APF.Settings.Role.SoulWarehouseTempSpace)
                    break;

                player.Money -= thisHunt.CostMoney;
                //计算出吉运值
                var randomValue = APF.Random.Next();
                var luckyValue = Math.Min(thisHunt.MaxValue, Math.Max(thisHunt.MinValue, randomValue));

                //自己消失，尝试开启高级项
                if (arg != 1)
                    player.LightSoulQualities.Remove(arg);
                var nextHunt = APF.Settings.SoulHunts.Find(arg + 1);
                if (nextHunt == null)
                    return;
                if (luckyValue >= nextHunt.MinValue)
                    player.LightSoulQualities.Add(nextHunt.Id);

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
                HuntSoulArgs huntsoul = new HuntSoulArgs();
                foreach (int i in player.LightSoulQualities)
                    huntsoul.LightSoulQualities.Add(i);
                huntsoul.TempSoul = new TempSoulArgs { Id = soul.Id, SettingId = soul.SettingId };
                huntSouls.HuntSouls.Add(huntsoul);

                if (player.LightSoulQualities.Contains(5))
                    break;
            }
            session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            session.SendResponse(ID, huntSouls);

            //任务事件
            foreach(var soul in player.SoulWarehouse.TempSouls)
            {
                session.Server.ModuleFactory.Module<ITaskModule>().OnEventHandled(session.Player,
                    player, TaskType.HuntSoul, soul.Setting.Quality);
            }
        }
    }
}
