using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Collections;
using Action.Core;
using Action.Engine;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class GetValueFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//根据键名获取字典的值\ngetValue(key);"; }
        }

        public Type Class
        {
            get { return typeof(IDictionary); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "getValue(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                var hash = (IDictionary)data.Value;
                data.Update(hash.GetValue(args[0]));
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("getValue", 1, 1));
        }
    }
}
