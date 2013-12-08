using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;

namespace Action.Role.Command
{
    [GameCommand((int)CommandEnum.LoadVip)]
    public class LoadVipCommand:GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();

            session.SendResponse(ID, new VipArgs() 
            { 
                VipLevel = player.Vip,
                RechargeGold = player.RechargeGold 
            });
        }
    }
}
