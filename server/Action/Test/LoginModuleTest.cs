
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ProtoBuf;
using Action.Model;

namespace Test
{
    [TestClass()]
    public class LoginModuleTest : TestBase
    {
        [ClassInitialize()]
        //[AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
            for (int i = 0; i < 1000; i++)
                playerNames.Add("test" + i);

            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["mongodb"].ConnectionString;
                var mongoDB = MongoServer.Create(connectionString).GetDatabase("Game");
                var table = mongoDB.GetCollection<Player>("Player");
                table.InsertBatch(playerNames.Select(p => new Player { Account = p, Name = p }));
            }
            catch (Exception)
            {
            }
        }
        [ClassCleanup()]
        //[AssemblyCleanup]
        public static void ClassCleanup()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["mongodb"].ConnectionString;
            //var mongoDB = MongoServer.Create(connectionString).GetDatabase("Game");
            //var table = mongoDB.GetCollection<Player>("Player");

            //var query = Query<Player>.Where(p => p.Name.StartsWith("test"));
            //table.Remove(query);
        }
        private static List<string> playerNames = new List<string>();

        [TestMethod()]
        public void BackdoorLoginCommandTest()
        {
            var name = playerNames[rng.Next(1, 1000)];
            var arg = new BackdoorLoginArgs { Account = name };
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize<BackdoorLoginArgs>(ms, arg);

                writer.Write(1000);    // cmdId
                writer.Write((int)ms.Length);        // package length
                ms.WriteTo(stream);
            }
            if (client.Connected)
            {
                var cmdId = reader.ReadInt32();
                Assert.AreEqual(1011, cmdId);
                var length = reader.ReadInt32();
                Assert.AreEqual(0, length);
                if (length != 0)
                    reader.ReadBytes(length);
            }
        }

    }
}
