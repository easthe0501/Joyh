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
    public class GenTestUsersFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//生成测试用户\nobject genTestUsers(count=1)"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return ScriptHelper.MatchFunArgs(expression, "genTestUsers(", ")");
        }

        public void Call(ScriptVar data, object[] args)
        {
            if (args.Length == 1)
            {
                var list = new List<TestUserSetting>();
                var count = MyConvert.ToInt32(args[0]);
                for (int i = 0; i < count; i++)
                {
                    var user = new TestUserSetting();
                    user.Id = APF.Random.Range(10, 99) * 10000 + APF.Random.Next();
                    user.Password = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
                    list.Add(user);
                }
                data.Update(list.ToArray());
            }
            else
                data.Update(null, 1, ScriptHelper.DescForArgsCount("genTestUsers", 1, 1));
        }
    }
}
