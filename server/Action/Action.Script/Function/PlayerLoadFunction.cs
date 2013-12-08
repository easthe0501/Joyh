using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Model;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class PlayerLoadFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//加载玩家数据\nGamePlayer load();"; }
        }

        public Type Class
        {
            get { return typeof(GamePlayer); }
        }

        public object[] Match(string expression)
        {
            return expression == "load()" ? new object[0] : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
            var player = data.Value as GamePlayer;
            var dbPlayer = APF.Database.LoadPlayer(player.Name);
            player.Data = dbPlayer;
        }
    }
}
