using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public interface IExternalChannel
    {
        void Import(Dictionary<string, string> externalData);
    }
}
