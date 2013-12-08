
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace Test
{
    [TestClass()]
    public class CommandTest : TestBase
    {
        private const int COUNT = 100000;

        [TestMethod()]
        public void TestCommandTest()
        {
            writer.Write(979);    // cmdId
            writer.Write(0);        // package length

            Assert.AreEqual(979, reader.ReadInt32());
            Assert.AreEqual(0, reader.ReadInt32());
        }

        [TestMethod()]
        public void TestCommandLongTest()
        {
            byte[] send = new byte[8];
            Buffer.BlockCopy(BitConverter.GetBytes(979), 0, send, 0, 4);// cmdId
            Buffer.BlockCopy(BitConverter.GetBytes(0), 0, send, 4, 4);// package length

            byte[] receive = new byte[8];

            for (int i = 0; i < COUNT; i++)
            {
                //writer.Write(979);    // cmdId
                //writer.Write(0);        // package length
                //stream.BeginWrite(send, 0, 8, null, null);
                stream.Write(send, 0, 8);

                //Assert.AreEqual(979, reader.ReadInt32());
                //Assert.AreEqual(0, reader.ReadInt32());
                stream.Read(receive, 0, 8);
                //Assert.AreEqual(979, BitConverter.ToInt32(receive, 0));
                //Assert.AreEqual(0, BitConverter.ToInt32(receive, 4));

                //Debug.WriteLine("Round : {0}", i);
                //Console.WriteLine("Round : ", i);
            }
        }

        [TestMethod()]
        public void TestIntCommandTest()
        {
            var value = rng.Next(int.MinValue, int.MaxValue);
            writer.Write(981);    // cmdId
            writer.Write(4);        // package length
            writer.Write(value);

            Assert.AreEqual(981, reader.ReadInt32());
            Assert.AreEqual(4, reader.ReadInt32());
            Assert.AreEqual(value, reader.ReadInt32());
        }

        [TestMethod()]
        public void TestBoolCommandTest()
        {
            var value = rng.Next(0, 2) == 1 ? true : false;
            writer.Write(978);    // cmdId
            writer.Write(1);        // package length
            writer.Write(value);

            Assert.AreEqual(978, reader.ReadInt32());
            Assert.AreEqual(1, reader.ReadInt32());
            Assert.AreEqual(value, reader.ReadBoolean());
        }

        [TestMethod()]
        public void TestFloatCommandTest()
        {
            var value = (float)(float.MaxValue * (rng.NextDouble() - 0.5));
            writer.Write(980);    // cmdId
            writer.Write(4);        // package length
            writer.Write(value);

            Assert.AreEqual(980, reader.ReadInt32());
            Assert.AreEqual(4, reader.ReadInt32());
            Assert.AreEqual(value, reader.ReadSingle());
        }

        [TestMethod()]
        public void TestStringCommandTest()
        {
            var text = "摘要：今天试探性的打开了66.178.223.73的VS2005，添加了个Form和几个控件";
            var bytes = Encoding.UTF8.GetBytes(text);
            writer.Write(982);    // cmdId
            writer.Write(bytes.Length);        // package length
            writer.Write(bytes);

            Assert.AreEqual(982, reader.ReadInt32());
            Assert.AreEqual(bytes.Length, reader.ReadInt32());
            Assert.AreEqual(text, Encoding.UTF8.GetString(reader.ReadBytes(bytes.Length)));
        }

        [TestMethod()]
        public void TestQueryMongoDBCommandTest()
        {
            byte[] send = new byte[1024];
            Buffer.BlockCopy(BitConverter.GetBytes(983), 0, send, 0, 4);// cmdId

            byte[] receive = new byte[1024];

            for (int i = 0; i < COUNT; i++)
            {
                var name = "test" + rng.Next(1, 1000);

                var bytes = Encoding.UTF8.GetBytes(name);
                //writer.Write(983);    // cmdId
                //writer.Write(bytes.Length);        // package length
                //writer.Write(bytes);
                Buffer.BlockCopy(BitConverter.GetBytes(bytes.Length), 0, send, 4, 4);// package length
                Buffer.BlockCopy(bytes, 0, send, 8, bytes.Length);
                //stream.BeginWrite(send, 0, 8 + bytes.Length, null, null);
                stream.Write(send, 0, 8 + bytes.Length);

                var cmdId = reader.ReadInt32();
                //Assert.AreEqual(983, cmdId);
                var length = reader.ReadInt32();
                //Assert.AreNotEqual(0, length);
                if (length != 0)
                {
                    reader.ReadBytes(bytes.Length);
                    //var text = Encoding.UTF8.GetString(reader.ReadBytes(bytes.Length));
                }

                //stream.Read(receive, 0, 8);
                //var cmdId = BitConverter.ToInt32(receive, 0);
                //Assert.AreEqual(983, cmdId);
                //length = BitConverter.ToInt32(receive, 4);
                //if (length != 0)
                //{
                //    try
                //    {
                //        stream.Read(receive, 8, length);
                //    }
                //    catch (Exception)
                //    {
                //        throw;
                //    }
                //}
            }
        }
    }
}
