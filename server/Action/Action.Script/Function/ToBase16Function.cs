using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Engine;
using Action.Core;

namespace Action.Script.Function
{
    [Export(typeof(IScriptFunction))]
    public class ToBase16Function : FunctionBase, IScriptFunction
    {
        public string Annotation
        {
            get { return "//将二进制数组转换成十六进制字符串\nstring toBase16();"; }
        }

        public Type Class
        {
            get { return typeof(byte[]); }
        }

        public object[] Match(string expression)
        {
            return expression == "toBase16()" ? new object[0] : null;
        }

        public void Call(ScriptVar data, object[] args)
        {
            var bytes = (byte[])data.Value;
            data.Update(MyConvert.ToBase16(bytes));
        }
    }
}
