using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Action.Utility
{
    public static class JsonHelper
    {
        private static JavaScriptSerializer _jss = new JavaScriptSerializer();

        public static string ToJson(object obj, bool throwError = true)
        {
            if (throwError)
                return _jss.Serialize(obj);
            else
            {
                try { return _jss.Serialize(obj); }
                catch { return null; }
            }
        }

        public static object FromJson(string json, bool throwError = true)
        {
            if(throwError)
                return _jss.DeserializeObject(json);
            else
            {
                try { return _jss.DeserializeObject(json); }
                catch { return null; }
            }
        }

        public static object FromJson(Type type, string json, bool throwError = true)
        {
            if(throwError)
                return _jss.Deserialize(json, type);
            else
            {
                try { return _jss.Deserialize(json, type); }
                catch { return null; }
            }
        }

        public static T FromJson<T>(string json, bool throwError = true)
        {
            if(throwError)
                return _jss.Deserialize<T>(json);
            else
            {
                try { return _jss.Deserialize<T>(json); }
                catch { return default(T); }
            }
        }
    }
}
