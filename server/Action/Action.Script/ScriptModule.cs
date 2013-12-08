using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using SuperSocket.Common;
using Action.Engine;
using Action.Model;
using Action.Utility;

namespace Action.Login
{
    [Export(typeof(IGameModule))]
    public class ScriptModule : GameModule
    {
        
    }
}
