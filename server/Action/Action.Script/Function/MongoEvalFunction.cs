using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using MongoDB.Driver;
using Action.Core;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class MongoEvalFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//执行Mongo脚本\nCommandResult eval(script);"; }
        }

        public Type Class
        {
            get { return typeof(MongoDatabase); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "eval(", ")", false);
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                var db = (MongoDatabase)data.Value;
                var script = MyConvert.ToString(args[0]);
                data.Update(db.Eval(script));
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("eval", 1, 1));
        }
    }
}
