using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ProtoBuf;
using Action.Core;

namespace Action.Model
{
    public partial class BattleReport
    {
        public override string ToString()
        {
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, this);
                return MyConvert.ToBase16(ms.ToArray());                
            }
        }
    }
}
