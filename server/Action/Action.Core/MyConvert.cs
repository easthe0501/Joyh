using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Globalization;

namespace Action.Core
{
    /// <summary>
    /// 智能转换类
    /// </summary>
    public static class MyConvert
    {
        public static bool CanToBool(object val)
        {
            if(val == null)
                return false;
            bool result = false;
            return bool.TryParse(val.ToString(), out result);
        }

        public static bool CanToInt8(object val)
        {
            if (val == null)
                return false;
            byte result = 0;
            return byte.TryParse(val.ToString(), out result);
        }

        public static bool CanToInt16(object val)
        {
            if (val == null)
                return false;
            short result = 0;
            return short.TryParse(val.ToString(), out result);
        }

        public static bool CanToInt32(object val)
        {
            if (val == null)
                return false;
            int result = 0;
            return int.TryParse(val.ToString(), out result);
        }

        public static bool CanToInt64(object val)
        {
            if (val == null)
                return false;
            long result = 0;
            return long.TryParse(val.ToString(), out result);
        }

        public static bool CanToFloat(object val)
        {
            if (val == null)
                return false;
            float result = 0;
            return float.TryParse(val.ToString(), out result);
        }

        public static bool CanToDouble(object val)
        {
            if (val == null)
                return false;
            double result = 0;
            return double.TryParse(val.ToString(), out result);
        }

        public static bool CanToString(object val)
        {
            return val != null;
        }

        public static bool CanToDateTime(object val)
        {
            if (val == null)
                return false;
            DateTime result = Global.DefaultDateTime;
            return DateTime.TryParse(val.ToString(), out result);
        }

        public static bool ToBool(object val, bool failVal = false)
        {
            if (CanToBool(val))
                return bool.Parse(val.ToString());
            return ToInt32(val) > 0 || failVal;
        }        

        public static byte ToInt8(object val, byte failVal = 0)
        {
            return CanToInt8(val) ? byte.Parse(val.ToString()) : failVal;
        }        

        public static short ToInt16(object val, short failVal = 0)
        {
            return CanToInt16(val) ? short.Parse(val.ToString()) : failVal;
        }        

        public static int ToInt32(object val, int failVal = 0)
        {
            return CanToInt32(val) ? int.Parse(val.ToString()) : failVal;
        }        

        public static long ToInt64(object val, long failVal = 0)
        {
            return CanToInt64(val) ? long.Parse(val.ToString()) : failVal;
        }        

        public static float ToFloat(object val, float failVal = 0)
        {
            return CanToFloat(val) ? float.Parse(val.ToString()) : failVal;
        }        

        public static double ToDouble(object val, double failVal = 0)
        {
            return CanToDouble(val) ? double.Parse(val.ToString()) : failVal;
        }        

        public static string ToString(object val, string failVal = "")
        {
            if (failVal == null || failVal.ToLower() == "null")
                return null;
            return CanToString(val) ? val.ToString() : failVal;
        }

        public static DateTime ToDateTime(string time, DateTime failVal = default(DateTime))
        {

            if (CanToDateTime(time))
                return DateTime.Parse(time, null, DateTimeStyles.AssumeLocal);
            else
            {
                if (failVal == default(DateTime))
                    failVal = Global.DefaultDateTime;
                return failVal;
            }
        }

        public static DateTime ToDateTime(long ticks)
        {
            return Global.DefaultDateTime.AddTicks(ticks);
        }

        public static DateTime ToDateTime(int seconds)
        {
            return Global.DefaultDateTime.AddSeconds(seconds);
        }

        public static long ToTicks(this DateTime dt)
        {
            return dt.Ticks - Global.DefaultDateTime.Ticks;
        }

        public static int ToSeconds(this DateTime dt)
        {
            return (int)((dt - Global.DefaultDateTime).TotalSeconds);
        }

        public static int ToUnixTicks(this DateTime dt)
        {
            return (int)((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
        }

        public static int ToDays(this DateTime dt)
        {
            return (int)((dt - Global.DefaultDateTime).TotalDays);
        }

        /// <summary>
        /// 转换为键值对
        /// </summary>
        /// <param name="nameValueString"></param>
        /// <returns></returns>
        public static NameValueCollection ToNameValueCollection(string nameValues)
        {
            string[] arrNameValues = nameValues.Split('&');
            NameValueCollection coll = new NameValueCollection();
            foreach (string nameValue in arrNameValues)
            {
                string[] arrNameValue = nameValue.Split('=');
                coll.Add(arrNameValue[0], arrNameValue[1]);
            }
            return coll;
        }

        public static string Trim(string text)
        {
            return text != null ? text.Trim() : "";
        }

        public static string ToBase16(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
