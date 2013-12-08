using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Core;
using Action.Utility;
using Action.Model;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class AddPrizeFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//添加奖励\nobject addPrize(prizeAson, playerQuery=null);"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "addPrize(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1 || args.Length == 2)
            {
                var world = ServerContext.GameServer.World;
                var json = ScriptHelper.AsonToJson(MyConvert.ToString(args[0]));
                var prize = JsonHelper.FromJson<Prize>(json);
                var query = args.Length == 2 ? MyConvert.ToString(args[1]) : "";
                IEnumerable<PlayerSummary> summaries = world.Data.AsDbWorld().Summaries.Values;
                if (!string.IsNullOrEmpty(query))
                {
                    var assert = AssertInfo.Create(MyConvert.ToString(args[1]));
                    if (assert != null)
                    {
                        summaries = summaries.Where(item => assert.Match(
                            TypeHelper.GetPropertyString(item, assert.Key),
                            assert.Value));
                    }
                }
                data.Update(data.Value, 0, world.AppServer.ModuleFactory.Module<IPrizeModule>()
                    .AddPrize(world, summaries, prize).ToString());
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("addPrize", 1, 2));
        }
    }
}
