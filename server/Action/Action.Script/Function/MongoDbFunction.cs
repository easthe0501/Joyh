using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.DataAccess;
using Action.Core;
using Action.Model;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class MongoDbFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//获取数据库实例\nMongoDatabase db(name);"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "db(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                switch (MyConvert.ToString(args[0]).Trim())
                {
                    case "Game":
                        data.Update(APF.Database.Mongo.GameDB);
                        break;
                    case "Log":
                        data.Update(APF.Database.Mongo.LogDB);
                        break;
                }
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("db", 1, 1));
        }
    }
}
