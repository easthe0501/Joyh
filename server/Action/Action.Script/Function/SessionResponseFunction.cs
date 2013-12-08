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
    public class SessionResponseFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//向当前会话发送响应\nIActionSession response(int cmdId, string ason);"; }
        }

        public Type Class
        {
            get { return typeof(IActionSession); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "response(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1 || args.Length == 2)
            {
                var session = (IActionSession)data.Value;
                var id = MyConvert.ToInt32(args[0]);
                var strArg = args.Length == 2 ? MyConvert.ToString(args[1]) : null;
                if (strArg == null)
                    session.SendResponse(id);
                else if (MyConvert.CanToBool(strArg))
                    session.SendResponse(id, MyConvert.ToBool(strArg));
                else if (MyConvert.CanToInt32(strArg))
                    session.SendResponse(id, MyConvert.ToInt32(strArg));
                else if (MyConvert.CanToFloat(strArg))
                    session.SendResponse(id, MyConvert.ToFloat(strArg));
                else if (strArg.Length > 1 && strArg.StartsWith("\"") && strArg.EndsWith("\""))
                    session.SendResponse(id, strArg.Substring(1, strArg.Length - 2));
                else
                    session.SendResponse(id, ScriptHelper.CreateInstance(strArg));
                data.Update(session);
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("request", 1, 2));
        }
    }
}
