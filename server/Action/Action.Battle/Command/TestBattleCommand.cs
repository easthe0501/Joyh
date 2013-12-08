using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Battle.Command
{
    //[GameCommand((int)CommandEnum.TestBattle)]
    public class TestBattleCommand : GameCommand<string>
    {
        protected override void Run(GameSession session, string args)
        {
            for (int i = 0; i < 1000; i++)
            {
                var player = APF.LoadPlayer(session.Player, args);
                if (player == null)
                    player = session.Player.Data.AsDbPlayer();
                player.Temp.BattleReport = session.Server.ModuleFactory.Module<IBattleModule>().PVP(session, player);
                //session.SendResponse((int)CommandEnum.LoadBattleReport, report);
            }
        }
    }
}
