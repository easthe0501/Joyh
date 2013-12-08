using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class IdGen
    {
        public int Id { get; private set; }
        public int Next()
        {
            return ++Id;
        }
    }
}
