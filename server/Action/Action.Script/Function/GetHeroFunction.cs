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
    public class GetHeroFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//获取英雄\nGamePlayer getHero(id,pos=0);"; }
        }

        public Type Class
        {
            get { return typeof(GamePlayer); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "getHero(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1 || args.Length == 2)
            {
                var gPlayer = (GamePlayer)data.Value;
                var dbPlayer = gPlayer.Data.AsDbPlayer();
                var hero = APF.Factory.Create<Hero>(dbPlayer, MyConvert.ToInt32(args[0]));
                hero.Pos = args.Length == 2 ? MyConvert.ToInt32(args[1], 1) : 0;
                dbPlayer.Heros.Add(hero);
                hero.Refresh();
                data.Update(data.Value, 0, "A hero added.");
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("getHero", 1, 2));
        }
    }
}
