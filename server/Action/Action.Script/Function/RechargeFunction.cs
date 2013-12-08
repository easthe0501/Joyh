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
    public class RechargeFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//充值\nGamePlayer recharge(gold);"; }
        }

        public Type Class
        {
            get { return typeof(GamePlayer); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "recharge(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                var player = (GamePlayer)data.Value;
                var gold = MyConvert.ToInt32(args[0]);
                player.Data.AsDbPlayer().Recharge(player.Session, gold);
                data.Update(data.Value, 0, string.Format("Recharge {0} golds successful.", gold));
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("recharge", 1, 1));
        }
    }
}
