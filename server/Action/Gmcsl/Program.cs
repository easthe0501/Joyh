using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;
using Action.Client;

namespace Gmcsl
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandFactory.Current.AddCommand(new RunScriptCommand());
            new Program(args);
        }

        private ActionTcpClient _client = new ActionTcpClient();

        public Program(string[] args)
        {
            _client.SocketClosed += (o, e) => 
            { 
                Console.WriteLine("Connection closed.\n");
            };
            ConnectServer(args);
        }

        private void ConnectServer(string[] args)
        {
            while (true)
            {
                if (args != null && args.Length == 2)
                {
                    string host = args[0];
                    int port = -1;
                    if (int.TryParse(args[1], out port) && _client.Connect(host, port))
                    {
                        Console.WriteLine("Server {0}:{1} connected.\n", host, port);
                        StartConsole();
                        break;
                    }
                    else
                        Console.WriteLine("Server {0}:{1} is unreachable.\n", host, port);
                }
                args = new string[2];
                Console.Write("ip:");
                args[0] = Console.ReadLine();
                Console.Write("port:");
                args[1] = Console.ReadLine();
            }
        }

        private void StartConsole()
        {
            while (_client.Connected)
            {
                Console.Write("Gmcsl> ");
                var cmd = Console.ReadLine();
                switch (cmd.Trim().ToLower())
                {
                    case "exit":
                        _client.Close();
                        return;
                    case "cls":
                        Console.Clear();
                        break;
                    case "close":
                        _client.Close();
                        break;
                    default:
                        SendScript(cmd);
                        break;
                }                    
            }
            ConnectServer(null);
        }

        private void SendScript(string script)
        {
            _client.Send(500, script);
        }
    }
}
