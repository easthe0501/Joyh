using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using System.IO;

namespace Action.Engine
{
    public static class ServerContext
    {
        public static ILoggerFactory LoggerFactory{get;set;}
        //public static ICommandLogger CommandLogger { get; set; }

        private static Dictionary<string, IActionServer> _servers = new Dictionary<string, IActionServer>();
        public static IActionServer GetServer(string name)
        {
            IActionServer server = null;
            if (_servers.TryGetValue(name, out server))
                return server;
            return null;
        }
        public static void RegisterServer(string name, IActionServer server)
        {
            _servers[name] = server;
        }

        public static GameServer GameServer
        {
            get { return GetServer("Game") as GameServer; }
        }

        public static BackServer BackServer
        {
            get { return GetServer("Back") as BackServer; }
        }

        public static int SessionsCount
        {
            get
            {
                int sum = 0;
                foreach (var server in _servers.Values)
                    sum += server.GetActionSessions().Count();
                return sum;
            }
        }

        public static void Shutdown()
        {
            foreach (var server in _servers.Values)
            {
                server.Opened = false;
                foreach (var session in server.GetActionSessions())
                    session.Close();
            }
        }
    }
}
