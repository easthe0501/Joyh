using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Engine
{
    public interface IScriptFunction
    {
        void Init(ScriptEngine engine);
        ScriptEngine Engine { get; }
        string Annotation { get; }
        Type Class { get; }
        object[] Match(string expression);
        void Call(ScriptVar data, object[] args);
    }

    public interface IDefaultFunction
    {
    }
}
