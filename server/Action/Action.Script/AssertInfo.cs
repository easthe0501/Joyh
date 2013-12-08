using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;

namespace Action.Script
{
    public class AssertInfo
    {
        private static string[] _opps = new string[] { "!=", "<=", ">=", "<<", ">>", "=", "<", ">", "_is_" };

        public string Key { get; set; }
        public string Value { get; set; }
        public string Operator { get; set; }

        public static AssertInfo Create(string exp)
        {
            int index = -1;
            string opp = "";
            foreach (string o in _opps)
            {
                int p = exp.LastIndexOf(o);
                if (p > index)
                {
                    if (o.Length == 1)
                    {
                        string o1 = exp.Substring(p - 1, 2);
                        string o2 = p < exp.Length - 1 ? exp.Substring(p, 2) : string.Empty;
                        if (_opps.Contains(o1) || _opps.Contains(o2))
                            continue;
                    }
                    index = p;
                    opp = o;
                }
            }
            if (index > -1)
            {
                AssertInfo info = new AssertInfo();
                info.Key = exp.Substring(0, index);
                info.Operator = opp;
                info.Value = exp.Substring(index + opp.Length);
                return info;
            }
            return null;
        }

        public bool Match(string val1, string val2)
        {
            switch (Operator)
            {
                case "=":
                    return string.Compare(val1, val2, true) == 0;
                case "!=":
                    return string.Compare(val1, val2, true) != 0;
                case "<":
                    return MyConvert.ToDouble(val1) < MyConvert.ToDouble(val2);
                case "<=":
                    return MyConvert.ToDouble(val1) <= MyConvert.ToDouble(val2);
                case ">":
                    return MyConvert.ToDouble(val1) > MyConvert.ToDouble(val2);
                case ">=":
                    return MyConvert.ToDouble(val1) >= MyConvert.ToDouble(val2);
                case "<<":
                    return val2.Contains(val1);
                case ">>":
                    return val1.Contains(val2);
            }
            return false;
        }
    }
}
