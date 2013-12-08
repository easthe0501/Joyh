using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using System.Collections;
using Action.Utility;
using Action.Core;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class IndexFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//根据索引获取集合中对象\nobject index();"; }
        }

        public Type Class
        {
            get { return typeof(IEnumerable); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "[", "]");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                var index = MyConvert.ToInt32(args[0]);
                var iterator = (IEnumerable<object>)data.Value;
                var array = iterator.ToArray();
                if (index < array.Length)
                    data.Update(array[index]);
                else
                    data.Update(null, 1, ScriptHelper.DescForIndexOutOfArray());
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("index", 1, 1));
        }
    }
}
