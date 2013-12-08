using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Utility
{
    public static class NumberHelper
    {
        public static bool In(int num, IEnumerable<int> array)
        {
            return array.Contains(num);
        }

        public static bool Between(int num, int min, int max)
        {
            return num >= min && num <= max;
        }
    }
}
