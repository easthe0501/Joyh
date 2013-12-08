using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class BattleArena
    {
        private List<string> _playerList = new List<string>();

        public List<string> PlayerList
        {
            get { return _playerList; }
            private set { _playerList = value; }
        }

        public void Join(PlayerSummary player)
        {
            if (!_playerList.Contains(player.Name))
            {
                _playerList.Add(player.Name);
                player.ArenaRank = _playerList.Count;
            }
        }

        public void Exhange(PlayerSummary player1, PlayerSummary player2)
        {
            var rank = player1.ArenaRank;
            player1.ArenaRank = player2.ArenaRank;
            player2.ArenaRank = rank;
            _playerList[player1.ArenaRank - 1] = player1.Name;
            _playerList[player2.ArenaRank - 1] = player2.Name;
        }
    }
}
