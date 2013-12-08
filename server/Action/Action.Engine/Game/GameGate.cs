using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace Action.Engine
{
    public class GameGate
    {
        private ConcurrentDictionary<string, GamePlayer> _allPlayers
            = new ConcurrentDictionary<string, GamePlayer>();

        private GameServer _appServer;
        public GameServer AppServer
        {
            get { return _appServer; }
        }

        public GameGate(GameServer server)
        {
            _appServer = server;
        }

        public void AddPlayer(GamePlayer player)
        {
            _allPlayers[player.Account] = player;
        }

        public void RemovePlayer(GamePlayer player)
        {
            var ePlayer = GetPlayer(player.Account);
            if (ePlayer != null && player.Session == ePlayer.Session)
                _allPlayers.TryRemove(player.Account, out player);
        }

        public IEnumerable<GamePlayer> AllPlayers
        {
            get { return _allPlayers.Values; }
        }

        public GamePlayer GetPlayer(string account)
        {
            GamePlayer player = null;
            if (_allPlayers.TryGetValue(account, out player))
                return player;
            return null;
        }
    }
}
