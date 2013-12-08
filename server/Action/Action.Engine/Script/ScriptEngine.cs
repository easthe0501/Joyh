using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Core;

namespace Action.Engine
{
    public class ScriptEngine
    {
        private IActionServer _server;

        [ImportMany]
        private IEnumerable<IScriptFunction> _functions = null;
        public IEnumerable<IScriptFunction> Functions
        {
            get { return _functions; }
        }

        public ScriptEngine(IActionServer server)
        {
            _server = server;
            Composition.ComposeParts(this);
            var defFun = _functions.SingleOrDefault(f => f is IDefaultFunction);
            if (defFun != null)
            {
                _functions = _functions.Where(f => !(f is IDefaultFunction))
                    .Concat(new IScriptFunction[] { defFun });
            }
            foreach (var fun in _functions)
                fun.Init(this);
        }

        public void Run(ScriptVar data, string script)
        {
            var exps = script.Trim().Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var exp in exps)
            {
                if (data.Value == null)
                    return;
                RunFunction(data, exp.Trim());
            }
        }

        private void RunFunction(ScriptVar data, string expression)
        {
            foreach (var function in _functions)
            {
                if (data.Value.GetType().IsChildOf(function.Class))
                {
                    var args = function.Match(expression);
                    if (args != null)
                    {
                        try
                        {
                            function.Call(data, args);
                        }
                        catch (Exception ex)
                        {
                            data.Update(null, 2, ex.Message);
                            _server.Logger.LogError(ex);
                        }
                        return;
                    }
                }
            }
            data.Update(null, 1, "Unkown function.");
        }
    }
}
