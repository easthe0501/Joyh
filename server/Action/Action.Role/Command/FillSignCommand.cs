using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Role.Command
{
    [GameCommand((int)CommandEnum.FillSign)]
    public class FillSignCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var now = DateTime.Now;
            var nowDay = MyConvert.ToDays(now);

            bool flag = false;
            for (var d = now.AddDays(-player.SignDayth()); d < now; d = d.AddDays(1))
            {
                if (!player.SignIns.SignInDay.Contains(MyConvert.ToDays(d)))
                {
                    if (player.Gold < APF.Settings.Role.FillSignCostGold)
                    {
                        session.SendError(ErrorCode.GoldNotEnough);
                        return;
                    }
                    player.Gold -= APF.Settings.Role.FillSignCostGold;
                    player.SignIns.SignInDay.Add(MyConvert.ToDays(d));
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                session.SendError(ErrorCode.NotNeedFillSign);
                return;
            }
            player.SignIns.SumDays += 1;
            int conDs = 0;
            var tempDay = now;
            //若昨日没有签到，不算今日
            bool ifcontinue = player.SignIns.SignInDay.Contains(MyConvert.ToDays(now.AddDays(-1)));
            if (!ifcontinue)
                tempDay = now.AddDays(-1);
            bool tip = false;
            for (; tempDay >= tempDay.AddDays(-player.SignDayth()); tempDay = tempDay.AddDays(-1))
            {
                bool ifContains = player.SignIns.SignInDay.Contains(MyConvert.ToDays(tempDay));
                if (!tip && !ifContains)
                    continue;
                if (!tip && ifContains)
                {
                    tip = true;
                    conDs += 1;
                    continue;
                }
                if (tip && ifContains)
                    conDs += 1;
                if (tip && !ifContains)
                    break;
            }
            if (ifcontinue || (!ifcontinue && !player.SignIns.SignInDay.Contains(nowDay)))
                player.SignIns.ConDays = conDs;
            //判断是否满足5,10,15,20,25,30连续签到
            int conIndex = conDs / 5;
            if (APF.Settings.SignPrize.ContinuePrize.Count(c => c.Index <= conIndex) != 0)
            {
                for (int i = 1; i <= conIndex; i++)
                {
                    if (!player.SignIns.ContinuePrize.Keys.Contains(conIndex))
                        player.SignIns.ContinuePrize.Add(conIndex, false);
                }
            }
            //判断是否满足10,20,30,40,50累计签到
            if (player.SignIns.SumDays <= APF.Settings.Role.SignInSumDays)
            {
                int sumIndex = player.SignIns.SumDays / 10;
                if (sumIndex < 6 && APF.Settings.SignPrize.SumPrize.Count(c => c.Index <= sumIndex) != 0)
                {
                    for (int j = 1; j <= sumIndex; j++)
                    {
                        if (!player.SignIns.SumPrize.Keys.Contains(sumIndex))
                            player.SignIns.SumPrize.Add(sumIndex, false);
                    }
                }
            }
            session.SendResponse((int)CommandEnum.RefreshGold, player.Gold);
            session.SendResponse(ID);
        }
    }
}
