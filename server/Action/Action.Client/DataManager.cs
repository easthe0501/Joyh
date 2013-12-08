//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Net.Sockets;
//using System.IO;

//namespace Action.Client
//{
//    class DataManager
//    {
//        private BinaryReader _reader;

//        public DataManager(int bufferSize)
//        {
//            _buffer = new byte[bufferSize];
//            //Reset();
//        }

//        private byte[] _buffer;
//        public byte[] Buffer
//        {
//            get { return _buffer; }
//        }

//        public CommandInfo Read(NetworkStream stream)
//        {
//            if (!stream.DataAvailable)
//                return null;
//            using (BinaryReader reader = new BinaryReader(stream))
//            {
//                var cmdInfo = new CommandInfo();
//                cmdInfo.Id = reader.ReadInt32();
//                cmdInfo.Args = reader.ReadBytes(reader.ReadInt32());
//                return cmdInfo;
//            }
//        }
//    }
//}
