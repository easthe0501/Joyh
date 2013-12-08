using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Core;
using Action.Model;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class GetItemsFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//直接获得道具\nGamePlayer getItems(id, count=1);"; }
        }

        public Type Class
        {
            get { return typeof(GamePlayer); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "getItems(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length <= 2)
            {
                var session = ((GamePlayer)data.Value).Session;
                var itemId = MyConvert.ToInt32(args[0]);
                var itemCount = args.Length == 2 ? MyConvert.ToInt32(args[1]) : 1;
                var desc = session.Server.ModuleFactory.Module<IBagModule>()
                    .AddItem(session, itemId, itemCount)
                    ? string.Format("{0} items added.", itemCount) : "Bag is full.";
                data.Update(data.Value, 0, desc);
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("getItems", 1, 2));
        }
    }
}
