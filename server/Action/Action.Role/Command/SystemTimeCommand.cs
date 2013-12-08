using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Role.Command
{
    [GameCommand((int)CommandEnum.SystemTime)]
    public class SystemTimeCommand:GameCommand
    {
        protected override void Run(GameSession session)
        {
            session.SendResponse(ID, MyConvert.ToSeconds(DateTime.Now));
        }
    }
}
