using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class SessionCloseFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//强制关闭回话\nIActionSession close();"; }
        }

        public Type Class
        {
            get { return typeof(IActionSession); }
        }

        public object[] Match(string expression)
        {
            return expression == "close()" ? new object[0] : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
            var session = (IActionSession)data.Value;
            session.Close();
            data.Update(data.Value, 0, string.Format("session \"{0}\" is closed.", session.IdentityKey));
        }
    }
}
