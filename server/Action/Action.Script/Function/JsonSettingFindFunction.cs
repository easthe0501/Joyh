using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Collections;
using Action.Core;
using Action.Engine;
using Action.Model;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class JsonSettingFindFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//根据Id获取json配置\nfind(json);"; }
        }

        public Type Class
        {
            get { return typeof(IJsonSettings); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "find(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                var settings = (IJsonSettings)data.Value;
                var id = MyConvert.ToInt32(args[0]);
                data.Update(settings.Find(id));
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("find", 1, 1));
        }
    }
}
