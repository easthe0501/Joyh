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
    public class FightWithFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//发起战斗\nBattleReport fightWith(battleId|\"playerName\");"; }
        }

        public Type Class
        {
            get { return typeof(GamePlayer); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "fightWith(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                var gPlayer = (GamePlayer)data.Value;
                var player = gPlayer.Data.AsDbPlayer();
                var module = gPlayer.Session.Server.ModuleFactory.Module<IBattleModule>();
                var arg = MyConvert.ToString(args[0]);
                if (arg.StartsWith("\"") && arg.EndsWith("\""))
                    data.Update(module.PVP(player, APF.LoadPlayer(gPlayer, arg.Replace("\"", ""))));
                else
                {
                    var battle = APF.Settings.Battles.Find(MyConvert.ToInt32(arg));
                    data.Update(module.PVE(player, battle));
                }
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("fightWith", 1, 1));
        }
    }
}
