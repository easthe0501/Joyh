using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Concurrent;
using System.Collections;
using System.Net;

namespace Action.Core
{
    public static class ExtensionMethods
    {
        private static object _lock = new object();

        public static bool IsChildOf(this Type source, Type target)
        {
            return source == target || source.IsSubclassOf(target) || source.GetInterfaces().Contains(target);
        }

        public static void WriteFlush(this FileStream fs, string text, Encoding encoding = null)
        {
            lock (_lock)
            {
                if (encoding == null)
                    encoding = Encoding.Default;
                var bytes = encoding.GetBytes(text);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
            }
        }

        public static void Write(this Stream stream, byte[] buffer)
        {
            stream.Write(buffer, 0, buffer.Length);
        }

        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> hash, TKey key)
        {
            TValue value = default(TValue);
            if (hash.TryGetValue(key, out value))
                return value;
            return default(TValue);
        }

        public static void DelValue<TKey, TValue>(this IDictionary<TKey, TValue> hash, TKey key)
        {
            if (hash.ContainsKey(key))
                hash.Remove(key);
        }

        public static object GetValue(this IDictionary hash, object key)
        {
            return hash.Contains(key) ? hash[key] : null;
        }

        public static T Dequeue<T>(this ConcurrentQueue<T> queue)
        {
            T result = default(T);
            if (queue.TryDequeue(out result))
                return result;
            return default(T);
        }

        public static IEnumerable<object> ToObjects(this IEnumerable ie)
        {
            foreach (var i in ie)
                yield return i;
        }

        public static void Write(this HttpListenerResponse response, string text, string encode = null)
        {
            var encoding = encode != null ? Encoding.GetEncoding(encode) : Encoding.UTF8;
            var bytes = encoding.GetBytes(text);
            var output = response.OutputStream;
            output.Write(bytes, 0, bytes.Length);
            output.Flush();
            output.Close();
        }

        public static StringBuilder AppendLine(this StringBuilder sb, string format, params object[] args)
        {
            return sb.AppendFormat(format, args).AppendLine();
        }
    }
}
