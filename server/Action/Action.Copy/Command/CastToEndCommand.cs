using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Copy.Command
{
    [GameCommand((int)CommandEnum.CastToEnd)]
    public class CastToEndCommand : CastDiceCommandBase
    {
        protected override int GetDice(GameSession session)
        {
            var member = CopyHelper.GetMember(session);
            var moveCount = member.Instance.Grids.Length - 1 - member.Pos;

            //移动到倒数第二格，获取并通知一路上的奖励
            var prizeTips = new PrizeTipArray();
            for (int i = 1; i < moveCount; i++)
            {
                member.Player.Temp.CopyGridPrize = null;
                CopyHelper.CastMove(session, member, 1);
                var prize = member.Player.Temp.CopyGridPrize;
                if (prize != null)
                {
                    var tip = prize.Open(session, PrizeSource.CopyGrid, false);
                    prizeTips.Tips.Add(tip);
                }
            }
            session.SendResponse((int)CommandEnum.CastToEndPrizeTips, prizeTips);

            //移动到最后一格
            return moveCount;
        }
    }
}
