using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using SuperSocket.SocketBase.Command;
using Action.Utility;
using Action.Core;
using Action.Model;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class FacadeFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//根据名称获取外观\nobject facade(string name);"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "facade(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                var name = MyConvert.ToString(args[0]);
                switch (name)
                {
                    case "Settings":
                        data.Update(APF.Settings);
                        break;
                    case "Database":
                        data.Update(APF.Database);
                        break;
                    case "Factory":
                        data.Update(APF.Factory);
                        break;
                    case "Random":
                        data.Update(APF.Random);
                        break;
                    default:
                        data.Update(null, 1, "No such facade existed.");
                        break;
                }
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("facade", 1, 1));
        }
    }
}
