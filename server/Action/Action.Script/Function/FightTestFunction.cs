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
    public class FightTestFunction : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//生成测试战报\nBattleReport fightTest();"; }
        }

        public Type Class
        {
            get { return typeof(object); }
        }

        public object[] Match(string expression)
        {
            return expression == "fightTest()" ? new object[0] : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
            var report = ServerContext.GameServer.ModuleFactory
                .Module<IBattleModule>().CreateTestReport();
            data.Update(report);
        }
    }
}
