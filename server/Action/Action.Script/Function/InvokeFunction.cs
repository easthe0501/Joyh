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
    public class InvokeFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//获取对象的属性\nobject invoke(fun,p1,p2,...,pn);"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "invoke(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length > 0)
            {
                var method = MyConvert.ToString(args[0]);
                var parameters = new object[args.Length - 1];
                for (var i = 1; i < args.Length; i++)
                    parameters[i - 1] = args[i];
                data.Update(TypeHelper.InvokeMethod(data.Value, method, parameters));
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("invoke", 1, 10));
        }
    }
}
