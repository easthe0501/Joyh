using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using Action.Utility;

namespace Action.Script.Command
{
    [BackCommand(501)]
    public class RunScriptCommandReturnString : BackCommand<string>
    {
        protected override void Run(BackSession session, string args)
        {
            var p = args.IndexOf(":");
            if (p < 0)
                return;

            var tag = args.Substring(0, p);
            var script = args.Substring(p + 1);
            
            if (!session.Authorized)
            {
                if (session.Validate(args))
                    session.SendResponse(ID, string.Format("{0}|{1}|{2}|{3}", tag, "NULL", 0, "Session authorized."));
                else
                    session.Close();
                return;
            }

            var data = new ScriptVar(session, 0, "");
            session.Server.ScriptEngine.Run(data, script);
            var result = string.Format("{0}|{1}|{2}|{3}", tag,
                data.GetValueString(), data.Code, data.Desc);
            session.SendResponse(ID, result);
        }
    }
}
