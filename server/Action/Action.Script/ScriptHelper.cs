using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Utility;

namespace Action.Script
{
    public static class ScriptHelper
    {
        public static object[] MatchFunArgs(string expression, string prefix = "", string postfix = "", bool split=true)
        {
            if (expression.StartsWith(prefix) && expression.EndsWith(postfix))
            {
                var strArgs = expression.Substring(prefix.Length, expression.Length - postfix.Length - prefix.Length);
                if (split)
                {
                    var args = strArgs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < args.Length; i++)
                        args[i] = args[i].Trim();
                    return args;
                }
                else
                    return new object[] { strArgs };
            }
            return null;
        }

        public static string DescForArgsCount(string function, int min=0, int max=0)
        {
            return string.Format("Function \"{0}\" has {1} to {2} arguments.", function, min, max);
        }

        public static string DescForIndexOutOfArray()
        {
            return "Index is out of array.";
        }

        public static object CreateInstance(string text)
        {
            var p = text.IndexOf("{");
            if (p < 0)
                return null;

            var typeName = string.Format("Action.Model.{0}", text.Substring(0, p).Trim());
            var json = AsonToJson(text.Substring(p));
            var type = TypeHelper.GetType("Action.Model", typeName);
            if(type == null)
                return null;

            return JsonHelper.FromJson(type, json);
        }

        public static string AsonToJson(string ason)
        {
            return ason.Replace("&", ",").Replace("=", ":");
        }
    }
}
