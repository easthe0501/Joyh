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
    public class OpenPrizeFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//打开奖励\nGamePlayer openPrize(prizeAson);"; }
        }

        public Type Class
        {
            get { return typeof(GamePlayer); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "openPrize(", ")", false);
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                var player = (GamePlayer)data.Value;
                var json = MyConvert.ToString(args[0]);
                var prize = JsonHelper.FromJson<Prize>(json);
                prize.Open(player.Session, PrizeSource.GM);
                data.Update(data.Value, 0, "Open prize successful.");
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("addPrize", 1, 2));
        }
    }
}
