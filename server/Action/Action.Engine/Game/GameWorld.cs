using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using Action.Core;

namespace Action.Engine
{
    public class GameWorld
    {
        private ConcurrentDictionary<string, GamePlayer> _allPlayers
            = new ConcurrentDictionary<string, GamePlayer>();

        private GameServer _appServer;
        public GameServer AppServer
        {
            get { return _appServer; }
        }

        /// <summary>
        /// 世界存档数据
        /// </summary>
        public IWorldData Data { get; set; }

        /// <summary>
        /// 后门登陆令牌
        /// </summary>
        public string Token { get; set; }

        public GameWorld(GameServer server)
        {
            _appServer = server;
        }

        public void AddPlayer(GamePlayer player)
        {
            _allPlayers[player.Name] = player;
        }

        public void RemovePlayer(GamePlayer player)
        {
            var ePlayer = GetPlayer(player.Name);
            if(ePlayer != null && player.Session == ePlayer.Session)
                _allPlayers.TryRemove(player.Name, out player);
        }

        public ICollection<GamePlayer> AllPlayers
        {
            get { return _allPlayers.Values; }
        }

        public GamePlayer GetPlayer(string name)
        {
            return _allPlayers.GetValue(name);
        }

        public bool IsOnline(string name)
        {
            return _allPlayers.ContainsKey(name);
        }
    }
}
