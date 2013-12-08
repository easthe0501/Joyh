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
    public class FightBetweenFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//两者进行战斗\nBattleReport fightBetween(\"playerName\", battleId|\"playerName2\");"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "fightBetween(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 2)
            {
                var module = ServerContext.GameServer.ModuleFactory.Module<IBattleModule>();
                var arg1 = MyConvert.ToString(args[0]);
                var arg2 = MyConvert.ToString(args[1]);
                var player1 = APF.Database.LoadPlayer(arg1.Replace("\"", ""));
                if (arg2.StartsWith("\"") && arg2.EndsWith("\""))
                    data.Update(module.PVP(player1, APF.Database.LoadPlayer(arg2.Replace("\"", ""))));
                else
                {
                    var battle = APF.Settings.Battles.Find(MyConvert.ToInt32(arg2));
                    data.Update(module.PVE(player1, battle));
                }
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("fightBetween", 2, 2));
        }
    }
}
