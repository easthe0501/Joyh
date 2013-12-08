using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class ThisFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//获取当前对象\nobject this;"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return expression == "" || expression == "this" ? new object[0] : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
            data.Update(data.Value);
        }
    }
}
