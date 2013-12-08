using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Model;

namespace Action.Script.Command
{
    [BackCommand(500)]
    public class RunScriptCommand : BackCommand<string>
    {
        protected override void Run(BackSession session, string args)
        {
            if (!session.Authorized)
            {
                if (session.Validate(args))
                    session.SendResponse(ID, new ScriptVarArgs() { Desc = "Session authorized." });
                else
                    session.Close();
                return;
            }

            var data = new ScriptVar(session, 0, "");
            session.Server.ScriptEngine.Run(data, args);

            var result = new ScriptVarArgs() { Value = data.GetValueString(),
                Code = data.Code, Desc = data.Desc };
            session.SendResponse(ID, result);
        }
    }

#if DEBUG
    [GameCommand(500)]
    public class RunScriptCommand_1 : GameCommand<string>
    {
        protected override void Run(GameSession session, string args)
        {
            var data = new ScriptVar(session, 0, "");
            session.Server.ScriptEngine.Run(data, args);

            var result = new ScriptVarArgs()
            {
                Value = data.GetValueString(),
                Code = data.Code,
                Desc = data.Desc
            };
            session.SendResponse(ID, result);
        }
    }
#endif
}
