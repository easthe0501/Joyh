using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SuperSocket.SocketEngine.Configuration;
using SuperSocket.SocketEngine;
using SuperSocket.Common;
using Action.Model;
using Action.Engine;
using Action.Utility;
using Action.Log;

namespace StartUp
{
    class Program
    {
        static void Main(string[] args)
        {
            LogUtil.Setup();
            ServerContext.LoggerFactory = new CompositeLoggerFactory(
                new ColorLoggerFactory(), new FileLoggerFactory(), new MongoLoggerFactory());
            WordValidateHelper.Init();
            APF.Init();

            SocketServiceConfig serverConfig = ConfigurationManager.GetSection("socketServer") as SocketServiceConfig;
            if (!SocketServerManager.Initialize(serverConfig))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("初始化配置失败，请根据错误日志获取更多信息");
                Console.WriteLine("按任意键退出程序");
                Console.ReadKey();
                return;
            }

            if (!SocketServerManager.Start())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("启动服务失败，请根据错误日志获取更多信息");
                Console.WriteLine("按任意键退出程序");
                Console.ReadKey();
                SocketServerManager.Stop();
                return;
            }

            while (true)
            {
                var input = Console.ReadLine().Trim().ToLower();
                if (input == "exit")
                    break;
                if (input == "cls")
                    Console.Clear();
            }

            SocketServerManager.Stop();

            Console.WriteLine();
            Console.WriteLine("按任意键退出程序");
            Console.ReadKey();
        }
    }
}
