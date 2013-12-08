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
    public class ToListFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//转换为对象本身或某一个属性的列表ArrayList toList(string prop=null);"; }
        }

        public Type Class
        {
            get { return typeof(IEnumerable); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "toList(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length <= 1)
            {
                var list = new ArrayList();
                if (args.Length == 0)
                {
                    foreach (var item in (IEnumerable)data.Value)
                        list.Add(item);
                }
                else
                {
                    var prop = MyConvert.ToString(args[0]);
                    foreach (var item in (IEnumerable)data.Value)
                        list.Add(TypeHelper.GetPropertyValue(item, prop));
                }
                data.Update(list);
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("toList", 0, 1));
        }
    }
}
