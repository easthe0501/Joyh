using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Role.Command
{
    [GameCommand((int)CommandEnum.GetSignPrize)]
    public class GetSignPrizeCommand : GameCommand<GetSignPrizeArgs>
    {
        protected override bool Ready(GameSession session, GetSignPrizeArgs args)
        {
            bool flag = false;
            if (args.ConOrSum == 1 && args.Index <= 6)
                flag = true;
            if (args.ConOrSum == 2 && args.Index <= 5)
                flag = true;
            return base.Ready(session, args) && flag;
        }

        protected override void Run(GameSession session, GetSignPrizeArgs args)
        {
            var player = session.Player.Data.AsDbPlayer();
            Dictionary<int, bool> prizeDic = null;
            Action.Model.SignPrizeSetting.SignPrizes[] signInPrize = null;
            //领取连续奖励
            if (args.ConOrSum == 1)
            {
                prizeDic = player.SignIns.ContinuePrize;
                signInPrize = APF.Settings.SignPrize.ContinuePrize;
            }

            //领取累积奖励
            if (args.ConOrSum == 2)
            {
                prizeDic = player.SignIns.SumPrize;
                signInPrize = APF.Settings.SignPrize.SumPrize;
            }

            if (!prizeDic.ContainsKey(args.Index))
                return;
            //奖励已经领取
            if (prizeDic[args.Index])
                return;
            var prize = signInPrize.SingleOrDefault(cp => cp.Index == args.Index);
            if (prize == null)
                return;
            Prize p = prize.Prize;
            if (p != null)
                p.Open(session, PrizeSource.Sign);
            prizeDic[args.Index] = true;

            session.SendResponse(ID);
        }
    }
}
