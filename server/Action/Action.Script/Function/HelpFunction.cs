using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Core;
using Action.Engine;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class HelpFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//获取帮助\nstring help();"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return expression == "help()" ? new object[0] : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
            var functions = Engine.Functions
                .Where(f => data.Value.GetType().IsChildOf(f.Class));
            var sb = new StringBuilder();
            foreach (var fun in functions)
                sb.AppendFormat("{0}\n\n", fun.Annotation);
            data.Update(sb.ToString());
        }
    }
}
