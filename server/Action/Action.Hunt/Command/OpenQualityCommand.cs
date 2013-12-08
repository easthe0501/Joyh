using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Model;
using Action.Engine;

namespace Action.Hunt.Command
{
    [GameCommand((int)CommandEnum.OpenQuality)]
    public class OpenQualityCommand:GameCommand
    {
        protected override void Run(GameSession session)
        {
            var player = session.Player.Data.AsDbPlayer();

            if (player.LightSoulQualities.Contains(4))
            {
                session.SendError(ErrorCode.HighQualityHasLight);
                return;
            }

            if (player.Gold < APF.Settings.Role.LightQualityCost)
            {
                session.SendError(ErrorCode.GoldNotEnough);
                return;
            }

            player.LightSoulQualities.Add(4);
            player.Gold -= APF.Settings.Role.LightQualityCost;
            session.SendResponse((int)CommandEnum.RefreshGold, player.Gold);
            session.SendResponse(ID);
        }
    }
}
