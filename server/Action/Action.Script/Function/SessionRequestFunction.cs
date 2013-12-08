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
    public class SessionRequestFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//像当前会话发起请求\nIActionSession request(int cmdId, string ason);"; }
        }

        public Type Class
        {
            get { return typeof(IActionSession); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "request(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1 || args.Length == 2)
            {
                var session = (IActionSession)data.Value;
                var id = MyConvert.ToInt32(args[0]);
                var strArg = args.Length == 2 ? MyConvert.ToString(args[1]) : null;
                var result = false;
                if (strArg == null)
                    result = session.ExecuteCommand(id);
                else if (MyConvert.CanToBool(strArg))
                    result = session.ExecuteCommand(id, MyConvert.ToBool(strArg));
                else if (MyConvert.CanToInt32(strArg))
                    result = session.ExecuteCommand(id, MyConvert.ToInt32(strArg));
                else if (MyConvert.CanToFloat(strArg))
                    result = session.ExecuteCommand(id, MyConvert.ToFloat(strArg));
                else if (strArg.Length > 1 && strArg.StartsWith("\"") && strArg.EndsWith("\""))
                    result = session.ExecuteCommand(id, strArg.Substring(1, strArg.Length - 2));
                else
                    result = session.ExecuteCommand(id, ScriptHelper.CreateInstance(strArg));
                if (result)
                    data.Update(session);
                else
                    data.Update(session, 1, string.Format("Command \"{0}\" not found.", id));
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("request", 1, 2));
        }
    }
}
