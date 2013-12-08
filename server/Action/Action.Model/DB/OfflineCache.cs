using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class OfflineCache
    {
        public OfflineCache()
        {
            ChatMessages = new List<PrivateChatArgs>();
            NewFriends = new List<string>();
            Prizes = new List<int>();
        }

        public List<PrivateChatArgs> ChatMessages { get; private set; }
        public List<string> NewFriends { get; private set; }
        public List<int> Prizes { get; private set; }
    }
}
