using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class TestFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//各种测试\ntest();"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return expression == "test()" ? new object[0] : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
        }
    }
}
