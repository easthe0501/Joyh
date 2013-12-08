using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Utility;
using Action.Engine;

namespace Action.Chat
{
    public static class ChatHelper
    {
        public static string Filter(string text)
        {
            return WordValidateHelper.FilterForStr(text);
        }
    }
}
