using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Role.Command
{
    [GameCommand((int)CommandEnum.SignIn)]
    public class SignInCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            var now = DateTime.Now;
            int nowToDay = MyConvert.ToDays(now);
            if (player.SignIns.SignInDay.Contains(nowToDay))
                return;
            //今日签到
            player.SignIns.SignInDay.Add(nowToDay);
            player.SignIns.SumDays += 1;
            player.SignIns.ConDays += 1;
            //只显示上个月，这个月，和下个月
            int befMonthTime = MyConvert.ToDays(new DateTime(now.Year, now.Month - 1, 1));
            player.SignIns.SignInDay.RemoveAll(d => d < befMonthTime);
            //判断是否满足5,10,15,20,25,30连续签到
            int conIndex = player.SignIns.ConDays / 5;
            if (player.SignIns.ConDays % 5 == 0 && !player.SignIns.ContinuePrize.Keys.Contains(conIndex))
            {
                player.SignIns.ContinuePrize.Add(conIndex, false);
            }
            //判断是否满足10,20,30,40,50累计签到
            if (player.SignIns.SumDays <= APF.Settings.Role.SignInSumDays)
            {
                int sumIndex = player.SignIns.SignInDay.Count / 10;
                if (sumIndex < 6 && player.SignIns.SignInDay.Count % 10 == 0 && !player.SignIns.SumPrize.Keys.Contains(sumIndex))
                {
                    player.SignIns.SumPrize.Add(sumIndex, false);
                }
            }
            
            session.SendResponse(ID);
        }
    }
}
