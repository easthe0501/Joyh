using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Action.Core;

namespace Action.Utility
{
    /// <summary>
    /// 实例化帮助类
    /// </summary>
    public class TypeHelper
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="assemplyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static object CreateInstance(string assemblyName, string className)
        {
            try
            {
                Assembly assembly = Assembly.Load(assemblyName);
                return assembly.CreateInstance(className);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="classType"></param>
        /// <returns></returns>
        public static object CreateInstance(Type classType)
        {
            return CreateInstance(classType.Assembly.FullName, classType.FullName);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="class_assembly_Name"></param>
        /// <returns></returns>
        public static object CreateInstance(string class_assembly_Name)
        {
            int p = class_assembly_Name.IndexOf(",");
            if (p < 0)
                return null;

            string className = class_assembly_Name.Substring(0, p).Trim();
            string assemblyName = class_assembly_Name.Substring(p + 1).Trim();
            return CreateInstance(assemblyName, className);
        }

        public static T CreateInstance<T>(string class_assembly_Name) where T : class
        {
            return CreateInstance(class_assembly_Name) as T;
        }

        public static Type GetType(string assemblyName, string typeName)
        {
            try
            {
                Assembly assembly = Assembly.Load(assemblyName);
                return assembly.GetType(typeName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public static Type[] GetTypes(string assemblyName)
        {
            try
            {
                Assembly assembly = Assembly.Load(assemblyName);
                return assembly.GetTypes();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public static object InvokeMethod(object obj, string meth, object[] args)
        {
            var tm = obj.GetType().GetMethod(meth);
            var pms = tm.GetParameters();
            if (pms.Length < args.Length)
                return null;
            for (int i = 0; i < args.Length; i++)
            {
                var type = pms[i].ParameterType;
                if (type == typeof(bool))
                    args[i] = MyConvert.ToBool(args[i]);
                else if (type == typeof(int) || type.BaseType == typeof(Enum))
                    args[i] = MyConvert.ToInt32(args[i]);
                else if (type == typeof(long))
                    args[i] = MyConvert.ToInt64(args[i]);
                else if (type == typeof(float))
                    args[i] = MyConvert.ToFloat(args[i]);
                else if (type == typeof(string))
                    args[i] = MyConvert.ToString(args[i]);
                else if (type == typeof(DateTime))
                    args[i] = MyConvert.ToDateTime(MyConvert.ToString(args[i]));
            }
            var result = tm != null ? tm.Invoke(obj, args) : null;
            return tm.ReturnType == typeof(void) ? obj : result;
        }

        public static object GetPropertyValue(object obj, string prop)
        {
            var ps = prop.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var p in ps)
            {
                if (obj == null)
                    return null;
                if (p != "this")
                {
                    var tp = obj.GetType().GetProperty(p);
                    if (tp == null)
                        return null;
                    obj = tp.GetValue(obj, null);
                }
            }
            return obj;
        }

        public static bool SetPropertyValue(object obj, string prop, object val)
        {
            var ps = prop.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            var tp = default(PropertyInfo);
            object ov = null;
            foreach (var p in ps)
            {
                if (obj == null)
                    return false;
                if (ov != null)
                    obj = ov;
                tp = obj.GetType().GetProperty(p);
                if (tp == null)
                    return false;
                ov = tp.GetValue(obj, null);
            }
            if (obj == null || !tp.CanWrite)
                return false;
            try
            {
                if (tp.PropertyType == typeof(bool))
                    tp.SetValue(obj, MyConvert.ToBool(val), null);
                else if (tp.PropertyType == typeof(int) || tp.PropertyType.BaseType == typeof(Enum))
                    tp.SetValue(obj, MyConvert.ToInt32(val), null);
                else if (tp.PropertyType == typeof(long))
                    tp.SetValue(obj, MyConvert.ToInt64(val), null);
                else if (tp.PropertyType == typeof(float))
                    tp.SetValue(obj, MyConvert.ToFloat(val), null);
                else if (tp.PropertyType == typeof(string) || tp.PropertyType == typeof(object))
                    tp.SetValue(obj, MyConvert.ToString(val), null);
                else if (tp.PropertyType == typeof(DateTime))
                    tp.SetValue(obj, MyConvert.ToDateTime(MyConvert.ToString(val)), null);
                else
                    tp.SetValue(obj, JsonHelper.FromJson(tp.PropertyType, MyConvert.ToString(val)), null);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string GetPropertyString(object obj, string prop)
        {
            var result = GetPropertyValue(obj, prop);
            return result != null ? result.ToString() : "";
        }

        public static string ToAson(object obj, bool beauty)
        {
            if (obj == null)
                return "";
            var type = obj.GetType();
            var sb = new StringBuilder();
            sb.Append(type.Name);
            if (beauty)
                sb.Append("\n");
            sb.Append("{");
            foreach (var p in type.GetProperties())
            {
                if (beauty)
                    sb.Append("\n").Append("    ");
                sb.AppendFormat("{0}={1}&", p.Name, p.CanRead ?
                    p.GetValue(obj, null) : null);
            }
            if (sb.ToString().EndsWith("&"))
                sb.Length--;
            if (beauty)
                sb.Append("\n");
            sb.Append("}");            
            return sb.ToString();
        }
    }
}
