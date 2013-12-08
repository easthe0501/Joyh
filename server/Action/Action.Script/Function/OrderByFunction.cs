using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Core;
using Action.Utility;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class OrderByFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//返回排序后的集合\nobject[] orderBy(field,desc=false);"; }
        }

        public Type Class
        {
            get { return typeof(IEnumerable); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "orderBy(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1 || args.Length == 2)
            {
                if (data.Value is IEnumerable<object>)
                {
                    var collection = (IEnumerable<object>)data.Value;
                    var field = MyConvert.ToString(args[0]);
                    var desc = args.Length == 2 && MyConvert.ToBool(args[1]);
                    if (desc)
                        data.Update(collection.OrderByDescending(c => TypeHelper.GetPropertyValue(c, field)));
                    else
                        data.Update(collection.OrderBy(c => TypeHelper.GetPropertyValue(c, field)));
                }
                else
                    data.Update(null, 1, "Object is not a instance of IEnumerable<object>");
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("orderBy", 1, 2));
        }
    }
}
