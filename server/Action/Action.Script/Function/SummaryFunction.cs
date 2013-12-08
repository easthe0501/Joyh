using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;
using Action.Core;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class SummaryFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "\\获取玩家摘要\nPlayerSummary summary(name=null);" ; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "summary(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                data.Update(ServerContext.GameServer.World.Data.AsDbWorld()
                    .GetSummary(MyConvert.ToString(args[0])));
            }
            else if (args.Length == 0 && data.Value is GamePlayer)
            {
                data.Update((data.Value as GamePlayer).GetSummary());
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("summary", 0, 1));
        }
    }
}
