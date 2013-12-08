using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.SellSoul)]
    public class SellSoulCommand : GameCommand<int>
    {
        protected override void Run(GameSession session, int args)
        {
            var player = session.Player.Data.AsDbPlayer();
            //防止在身上的被卖掉，判断是否在临时背包或者仓库里
            bool flag = false;
            var soul = player.SoulWarehouse.TempSouls.SingleOrDefault(s => s.Id == args);
            if (soul == null)
            {
                soul = player.SoulWarehouse.BackSouls.SingleOrDefault(s => s.Id == args);
                flag = true;
            }
            if (soul == null)
                return;
            if (soul.Setting.Price != 0)
            {
                player.Money += soul.Setting.Price;
                session.SendResponse((int)CommandEnum.RefreshMoney, player.Money);
            }
            if (flag)
                player.SoulWarehouse.BackSouls.Destory(soul);
            else
                player.SoulWarehouse.TempSouls.Destory(soul);
            session.SendResponse(ID, args);
        }
    }
}
