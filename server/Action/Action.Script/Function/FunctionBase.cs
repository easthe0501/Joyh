using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Engine;

namespace Action.Script.Function
{
    public class FunctionBase
    {
        public void Init(ScriptEngine engine)
        {
            _engine = engine;
        }

        private ScriptEngine _engine;
        public ScriptEngine Engine
        {
            get { return _engine; }
        }
    }
}
