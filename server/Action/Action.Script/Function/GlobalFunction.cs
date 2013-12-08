using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Core;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class GlobalFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//获取全局设置\nAppSettings global;"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return expression == "global" ? new object[0] : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
            //data.Update(Global.Settings);
        }
    }
}
