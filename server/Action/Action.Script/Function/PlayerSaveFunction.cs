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
    public class PlayerSaveFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//保存玩家数据\nGamePlayer save();"; }
        }

        public Type Class
        {
            get { return typeof(GamePlayer); }
        }

        public object[] Match(string expression)
        {
            return expression == "save()" ? new object[0] : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
            var player = data.Value as GamePlayer;
            if(player.Data != null)
                APF.Database.SavePlayer(player.Data.AsDbPlayer());
        }
    }
}
