using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Utility
{
    public class Singleton<T> where T : new()
    {
        private static T _current;

        public static T Current
        {
            get
            {
                if (_current == null)
                    _current = new T();
                return _current;
            }
        }

        protected Singleton()
        {
           
        }
    }
}
