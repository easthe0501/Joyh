using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;

namespace Action.Model
{
    public class Account : BsonClass, IPlayerData, IInitialization
    {
        public const string Cls = "Account";
        public string Key { get; set; }
        public string Name { get; set; }
        public string Player { get; set; }

        public void Init()
        {            
        }

        public void Load()
        {
        }
    }
}
