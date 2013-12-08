using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Core;
using Action.Utility;
using System.Collections;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class WhereFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//根据表达式筛选结果\nobject[] where(expression);"; }
        }

        public Type Class
        {
            get { return typeof(IEnumerable); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "where(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                var collection = (data.Value as IEnumerable).ToObjects();
                var assert = AssertInfo.Create(MyConvert.ToString(args[0]));
                if (assert != null)
                {
                    var result = collection.Where(item => assert.Match(
                        TypeHelper.GetPropertyString(item, assert.Key),
                        assert.Value));
                    data.Update(result);
                }
                else
                    data.Update(null, 1, "Unknow where condition.");
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("where", 1, 1));
        }
    }
}
