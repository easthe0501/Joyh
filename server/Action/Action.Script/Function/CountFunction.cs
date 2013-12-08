using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel.Composition;
using Action.Engine;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class CountFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//获取集合中对象的个数\nint count();"; }
        }

        public Type Class
        {
            get { return typeof(IEnumerable); }
        }

        public object[] Match(string expression)
        {
            return expression == "count()" ? new object[0] : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (data.Value is IEnumerable<object>)
                data.Update((data.Value as IEnumerable<object>).Count());
            else
                data.Update(null, 1, "Object is not a instance of IEnumerable<object>");
        }
    }
}
