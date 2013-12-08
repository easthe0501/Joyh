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
    public class ShortcutFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//快捷访问函数\nGameSession $Account|@Name;"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return expression.StartsWith("$") || expression.StartsWith("@")
                ? new object[] { expression } : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
            var server = ServerContext.GameServer;
            var exp = MyConvert.ToString(args[0]);
            if (exp.StartsWith("$"))
                data.Update(server.GetSessions(s => s.Player.Account == exp.Substring(1)).SingleOrDefault());
            else if (exp.StartsWith("@"))
                data.Update(server.GetSessions(s => s.Player.Name == exp.Substring(1)).SingleOrDefault());
        }
    }
}
