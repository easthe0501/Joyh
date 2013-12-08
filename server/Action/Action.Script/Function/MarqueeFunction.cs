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
    public class MarqueeFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//全服跑马灯公告\nobject marquee(content);"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "marquee(", ")", false);
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                foreach (var player in ServerContext.GameServer.World.AllPlayers)
                    player.Session.SendResponse((int)CommandEnum.Marquee, MyConvert.ToString(args[0]));
                data.Update(data.Value, 0, "Send marquee content successful.");
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("marquee", 1, 1));
        }
    }
}
