using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Action.Engine
{
    public interface IActionPlatformBridge
    {
        bool Connect(byte[] readBuffer, int offset, int length, out int left);
    }

    public class TencentTgwBridge : IActionPlatformBridge
    {
        private MemoryStream _ms;

        public TencentTgwBridge()
        {
            _ms = new MemoryStream();
        }

        public bool Connect(byte[] readBuffer, int offset, int length, out int left)
        {
            left = 0;
            var index = Math.Max((int)_ms.Length, 3);
            _ms.Write(readBuffer, offset, length);

            var bytes = _ms.ToArray();
            for (var i = index; i < bytes.Length; i++)
            {
                if (bytes[i - 3] == 13 && bytes[i - 2] == 10 && bytes[i - 1] == 13 && bytes[i] == 10)
                {
                    left = bytes.Length - i - 1;
                    Debug.WriteLine(Encoding.GetEncoding("GBK").GetString(bytes));
                    return true;
                }
            }
            return false;
        }
    }
}
