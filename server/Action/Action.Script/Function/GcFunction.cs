using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class GcFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//垃圾回收，返回占用内存\nstring gc();"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return expression == "gc()" ? new object[0] : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
            data.Update(GC.GetTotalMemory(true).ToString("###,###,###,###"));
        }
    }
}
