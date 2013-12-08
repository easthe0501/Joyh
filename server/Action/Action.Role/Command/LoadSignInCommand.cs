using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Role.Command
{
    [GameCommand((int)CommandEnum.LoadSignIn)]
    public class LoadSignInCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();
            LoadSignInArgs loadSignIn = new LoadSignInArgs();
            var now = DateTime.Now;
            bool IfSignToday = player.SignIns.SignInDay.Contains(MyConvert.ToDays(now));
            loadSignIn.IfSignToday = IfSignToday;
            if (player.SignDayth() == 0)
            {
                if (!IfSignToday)
                    player.SignIns.ConDays = 0;
                player.SignIns.ContinuePrize = new Dictionary<int, bool>();
            }
            else
            {
                //若昨天今天都没有签到，连续签到为0
                if (!IfSignToday && !player.SignIns.SignInDay.Contains(MyConvert.ToDays(now.AddDays(-1))))
                    player.SignIns.ConDays = 0;
            }

            foreach (var d in player.SignIns.SignInDay)
            {
                loadSignIn.Days.Add(d);
            }
            loadSignIn.SumDays = player.SignIns.SumDays;
            loadSignIn.ContinueDays = player.SignIns.ConDays;
            foreach (var i in player.SignIns.ContinuePrize)
            {
                loadSignIn.ConPrizes.Add(new SignInPrize() { Index = i.Key, IfGet = i.Value });
            }
            foreach (var i in player.SignIns.SumPrize)
            {
                loadSignIn.SumPrizes.Add(new SignInPrize() { Index = i.Key, IfGet = i.Value });
            }
            var signDayth = MyConvert.ToDays(now.AddDays(-player.SignDayth()));
            player.SignIns.MissTimes = player.SignDayth() + 1 - player.SignIns.SignInDay.Count(d => d >= signDayth);
            loadSignIn.MissTimes = player.SignIns.MissTimes;
            session.SendResponse(ID, loadSignIn);
        }
    }
}
