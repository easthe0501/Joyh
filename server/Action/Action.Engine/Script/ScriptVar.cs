using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Action.Engine
{
    public class ScriptVar
    {
        public ScriptVar(object value, int code, string desc)
        {
            Update(value, code, desc);
        }

        private object _value = null;
        public object Value
        {
            get { return _value;}
        }

        private int _code = 0;
        public int Code
        {
            get { return _code; }
        }

        private string _desc = string.Empty;
        public string Desc
        {
            get { return _desc; }
        }

        public void Update(object value, int code = 0, string desc = "")
        {
            _value = value;
            _code = code;
            _desc = desc;
        }

        public string GetValueString()
        {
            if (_value == null)
                return "NULL";
            if (_value is string)
                return (string)_value;
            if (_value is IEnumerable)
                return StringFromIEnumerable((IEnumerable)_value);
            return _value.ToString();
        }

        private string StringFromIEnumerable(IEnumerable ie)
        {
            var sb = new StringBuilder();
            var i=0;
            foreach (var e in ie)
                sb.AppendFormat("[{0}]: {1}\n", i++, e != null ? e.ToString() : "NULL");
            return sb.ToString();
        }
    }
}
