using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class CommonFacade
    {
        public float GetLevelRate(int level)
        {
            return level / 10f + 1;
        }
    }
}
