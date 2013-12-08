using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using SuperSocket.SocketBase.Command;
using Action.Utility;
using Action.Core;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class ServerFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//根据名称获取正在运行的服务实例\nIActionServer server(string name);"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "server(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                var name = MyConvert.ToString(args[0]);
                data.Update(ServerContext.GetServer(name));
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("server", 1, 1));
        }
    }
}
