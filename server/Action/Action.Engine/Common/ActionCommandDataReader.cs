using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase;

namespace Action.Engine
{
    public class ActionCommandDataReader : CommandReaderBase<BinaryCommandInfo>
    {
        public int CommandId { get; set; }
        public int DataLength { get; set; }

        public ICommandReader<BinaryCommandInfo> PrevCommandReader { get; private set; }

        public ActionCommandDataReader(ICommandReader<BinaryCommandInfo> previousCommandReader)
            : base(previousCommandReader.AppServer)
        {
            PrevCommandReader = previousCommandReader;
        }
        public override BinaryCommandInfo FindCommandInfo(IAppSession session, byte[] readBuffer, int offset, int length, bool isReusableBuffer, out int left)
        {
            NextCommandReader = this;
            left = 0;
            if (LeftBufferSize + length <= DataLength)
            {
                AddArraySegment(readBuffer, offset, length, isReusableBuffer);

                if (LeftBufferSize < DataLength)
                    return null;
            }
            else
            {
                var len = DataLength - LeftBufferSize;
                AddArraySegment(readBuffer, offset, len, false);
                left = length - len;
            }
            NextCommandReader = PrevCommandReader;

            var cmdInfo = new BinaryCommandInfo(CommandId.ToString(), BufferSegments.ToArrayData());

            ClearBufferSegments();

            return cmdInfo;
        }
    }
}
