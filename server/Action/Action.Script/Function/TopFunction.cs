using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Core;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class TopFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//获取集合的前n行数据\nobject[] top(length);"; }
        }

        public Type Class
        {
            get { return typeof(IEnumerable); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "top(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                if (data.Value is IEnumerable<object>)
                {
                    var length = MyConvert.ToInt32(args[0]);
                    data.Update((data.Value as IEnumerable<object>).Take(length));
                }
                else
                    data.Update(null, 1, "Object is not a instance of IEnumerable<object>");
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("top", 1, 1));
        }
    }
}
