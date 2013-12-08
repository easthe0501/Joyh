using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;

namespace Action.Model
{
    public interface IBattleModule : IGameModule
    {
        BattleReport LoadReport(GameWorld world, string guid);
        BattleReport PVP(Player player1, Player player2);
        BattleReport PVP(GameSession session, Player player);
        BattleReport PVE(Player player, BattleSetting battle);
        BattleReport PVE(GameSession session, int battleId);
        BattleReport CreateTestReport();
    }
}
