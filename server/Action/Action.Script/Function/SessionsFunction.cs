using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Utility;
using Action.Core;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class SessionsFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//根据条件获取当前服务的会话集合\nIActionSession[] sessions(string prop=null, string value=null);"; }
        }

        public Type Class
        {
            get { return typeof(IActionServer); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "sessions(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 0 || args.Length == 2)
            {
                var server = (IActionServer)data.Value;
                IEnumerable<IActionSession> sessions = server.GetActionSessions();
                if (args.Length == 2)
                {
                    var pName = MyConvert.ToString(args[0]);
                    var pValue = MyConvert.ToString(args[1]);
                    sessions = sessions.Where(s => TypeHelper.GetPropertyString(s, pName) == pValue);
                }
                data.Update(sessions);
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("sessions", 0, 1));
        }
    }
}
