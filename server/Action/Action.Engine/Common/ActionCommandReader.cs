//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using SuperSocket.SocketBase.Protocol;
//using SuperSocket.SocketBase.Command;
//using SuperSocket.SocketBase;

//namespace Action.Engine
//{
//    public class ActionCommandReader : CommandReaderBase<BinaryCommandInfo>
//    {
//        private byte[] buffer = new byte[8];
//        private int currentReceived = 0;

//        //private int currentIndex = 0;
//        //private const int MAX_LENGTH = 5;
//        private int cmdId = 0;
//        private int dataLen = 0;

//        public ActionCommandReader(IAppServer server)
//            : base(server)
//        {
//        }



//        public override BinaryCommandInfo FindCommandInfo(IAppSession session, byte[] readBuffer, int offset, int length, bool isReusableBuffer, out int left)
//        {
//            NextCommandReader = this;

//            left = 0;

//            if (currentReceived + length <= buffer.Length)
//            {
//                Array.Copy(readBuffer, offset, buffer, currentReceived, length);
//                currentReceived += length;

//                if (currentReceived < buffer.Length)
//                    return null;
//            }
//            else
//            {
//                Array.Copy(readBuffer, offset, buffer, currentReceived, buffer.Length - currentReceived);
//                left = length - (buffer.Length - currentReceived);
//            }

//            currentReceived = 0;

//            cmdId = BitConverter.ToInt32(buffer, 0);

//            dataLen = BitConverter.ToInt32(buffer, 4);

//            if (dataLen > 0)
//            {
//                return null;
//            }
//            else
//            {
//                return new BinaryCommandInfo(cmdId.ToString(), null);
//            }

//            #region UNDONE ProtocolBuffer
//            /*   
//            switch (currentIndex)
//            {
//                case 0:
//                    tmp = readBuffer[offset];
//                    if (tmp < 128)
//                        cmdId = tmp;
//                    else
//                        cmdId = tmp & 0x7f;

//                    break;
//                case 1:
//                    tmp = readBuffer[offset + 1];
//                    if (tmp < 128)
//                        cmdId |= tmp << 7;
//                    else
//                        cmdId |= (tmp & 0x7f) << 7;

//                    break;
//                case 2:
//                    tmp = readBuffer[offset + 2];
//                    if (tmp < 128)
//                        cmdId |= tmp << 14;
//                    else
//                        cmdId |= (tmp & 0x7f) << 14;

//                    break;
//                case 3:
//                    tmp = readBuffer[offset + 3];
//                    if (tmp < 128)
//                        cmdId |= tmp << 21;
//                    else
//                        cmdId |= (tmp & 0x7f) << 21;

//                    break;
//                case 4:
//                    tmp = readBuffer[offset + 4];
//                    if (tmp < 128)
//                        cmdId |= tmp << 28;
//                    else
//                    {
//                        AppServer.Logger.LogError(session, "Invalid ProtocolBuffer Data - Malformed Varint");
//                        session.Close(CloseReason.ServerClosing);
//                    }

//                    break;
//            }
//            */
//            #endregion

//        }

//    }
//}