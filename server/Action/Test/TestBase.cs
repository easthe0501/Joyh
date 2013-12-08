
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Configuration;

namespace Test
{
    [TestClass()]
    public class TestBase
    {
        public TestContext TestContext { get; set; }
        static TestBase()
        {
            rng = new Random(DateTime.Now.Second);
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            client = new TcpClient(ConfigurationManager.AppSettings["IP"], int.Parse(ConfigurationManager.AppSettings["Port"]));
            stream = client.GetStream();
            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            client.Close();
        }
        protected static Random rng;
        protected TcpClient client;
        protected NetworkStream stream;
        protected BinaryWriter writer;
        protected BinaryReader reader;
    }
}
