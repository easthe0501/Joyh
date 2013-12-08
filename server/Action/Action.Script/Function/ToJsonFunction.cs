using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;
using System.ComponentModel.Composition;
using Action.Utility;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class ToJsonFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//转化为Json\nstring toJson();"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return expression == "toJson()" ? new object[0] : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
            data.Update(JsonHelper.ToJson(data.Value));
        }
    }
}
