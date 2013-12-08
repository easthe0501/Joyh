using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase;

namespace Action.Engine
{
    public class ActionCommandHeaderReader : ICommandReader<BinaryCommandInfo>
    {
        private byte[] _buffer = new byte[8];
        private int _currentReceived = 0;

        //private int currentIndex = 0;
        //private const int MAX_LENGTH = 5;
        private int _cmdId = 0;
        private int _dataLen = 0;
        private IActionPlatformBridge _platformBridge;

        public IAppServer AppServer { get; private set; }

        public int LeftBufferSize { get { return _currentReceived; } }

        public ActionCommandDataReader DataReader { get; private set; }

        public ICommandReader<BinaryCommandInfo> NextCommandReader { get; private set; }

        public ActionCommandHeaderReader(IAppServer appServer)
        {
            AppServer = appServer;
            DataReader = new ActionCommandDataReader(this);
            _platformBridge = new TencentTgwBridge();
        }

        public BinaryCommandInfo FindCommandInfo(IAppSession session, byte[] readBuffer, int offset, int length, bool isReusableBuffer, out int left)
        {
            NextCommandReader = this;
            left = 0;

            var aSession = session as IActionSession;
            if (!aSession.Enabled)
            {
                if (_platformBridge.Connect(readBuffer, offset, length, out left))
                {
                    aSession.Enabled = true;
                    if (left > 0)
                    {
                        offset = offset + length - left;
                        length = left;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }

            if (_currentReceived + length <= _buffer.Length)
            {
                Array.Copy(readBuffer, offset, _buffer, _currentReceived, length);
                _currentReceived += length;

                if (_currentReceived < _buffer.Length)
                    return null;
            }
            else
            {
                Array.Copy(readBuffer, offset, _buffer, _currentReceived, _buffer.Length - _currentReceived);
                left = length - (_buffer.Length - _currentReceived);
            }

            _currentReceived = 0;
            _cmdId = BitConverter.ToInt32(_buffer, 0);
            _dataLen = BitConverter.ToInt32(_buffer, 4);

            BinaryCommandInfo cmdInfo;
            if (_dataLen > 0)
            {
                DataReader.CommandId = _cmdId;
                DataReader.DataLength = _dataLen;

                cmdInfo = DataReader.FindCommandInfo(session, readBuffer, offset + (length - left), left, isReusableBuffer, out left);
                NextCommandReader = DataReader.NextCommandReader;
            }
            else
            {
                cmdInfo = new BinaryCommandInfo(_cmdId.ToString(), null);
            }
            return cmdInfo;
        }

    }
}

