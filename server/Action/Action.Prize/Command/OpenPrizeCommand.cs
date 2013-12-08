using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Prize.Command
{
    public abstract class OpenPrizeCommand : GameCommand
    {
        protected override void Run(GameSession session)
        {
            if (CheckBag && session.Server.ModuleFactory.Module<IBagModule>().IfBagFull(session))
                return;
            var player = session.Player.Data.AsDbPlayer();
            var prize = GetPrize(player);
            if (prize == null)
            {
                session.SendError(ErrorCode.PrizeMissing);
                return;
            }
            ClearPrize(player);
            prize.Open(session, (PrizeSource)ID);
        }

        protected abstract bool CheckBag { get; }
        protected abstract Model.Prize GetPrize(Player player);
        protected abstract void ClearPrize(Player player);
    }

    [GameCommand((int)PrizeSource.CopyGrid)]
    public class OpenCopyGridPrize : OpenPrizeCommand
    {
        protected override bool CheckBag
        {
            get { return false; }
        }

        protected override Model.Prize GetPrize(Player player)
        {
            return player.Temp.CopyGridPrize;
        }

        protected override void ClearPrize(Player player)
        {
            player.Temp.CopyGridPrize = null;
        }
    }

    [GameCommand((int)PrizeSource.CopyPass)]
    public class OpenCopyPassPrize : OpenPrizeCommand
    {
        protected override bool CheckBag
        {
            get { return false; }
        }

        protected override Model.Prize GetPrize(Player player)
        {
            return player.Temp.CopyPassPrize;
        }

        protected override void ClearPrize(Player player)
        {
            player.Temp.CopyPassPrize = null;
        }
    }

    [GameCommand((int)PrizeSource.BattleArena)]
    public class OpenBattleArenaPrize : OpenPrizeCommand
    {
        protected override bool CheckBag
        {
            get { return false; }
        }

        protected override Model.Prize GetPrize(Player player)
        {
            return player.Temp.BattleArenaPrize;
        }

        protected override void ClearPrize(Player player)
        {
            player.Temp.BattleArenaPrize = null;
        }
    }
}
