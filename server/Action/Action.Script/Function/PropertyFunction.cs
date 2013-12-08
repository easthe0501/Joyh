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
    public class PropertyFunction : FunctionBase, IScriptFunction, IDefaultFunction
    {
        public string Annotation
        {
            get { return "//获取对象的属性\nobject X;\n\n//设置对象的属性，并返回对象本身\nobject X=x;"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return expression.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
                data.Update(TypeHelper.GetPropertyValue(data.Value, MyConvert.ToString(args[0])));
            else if(args.Length == 2)
            {
                var b = TypeHelper.SetPropertyValue(data.Value, MyConvert.ToString(args[0]), args[1]);
                data.Update(data.Value, 0, b ? "" : "Failed on set property.");
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("property", 1, 1));
        }
    }
}
