using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using System.Collections;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class EachFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//循环执行脚本\nIEnumerable each(exp);"; }
        }

        public Type Class
        {
            get { return typeof(IEnumerable); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "each(", ")", false);
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                var script = args[0].ToString().Replace("::", "->");
                var source = data.Value;
                foreach (var ie in (IEnumerable)data.Value)
                {
                    data.Update(ie);
                    Engine.Run(data, script);
                }
                data.Update(source);
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("each", 1, 1));
        }
    }
}
