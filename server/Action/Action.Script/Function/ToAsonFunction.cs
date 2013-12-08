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
    public class ToAsonFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//转化为Ason格式\nstring toAson(bool beauty=false);"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "toAson(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 0 || args.Length == 1)
                data.Update(TypeHelper.ToAson(data.Value, args.Length == 1 && MyConvert.ToBool(args[0])));
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("toAson", 0, 1));
        }
    }
}
