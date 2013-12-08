using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using System.IO;

namespace Action.Core
{
    public static class ActionCommandDataDeserializer
    {
        private static Dictionary<Type, IGameCommandDataDeserializer> _hash;

        static ActionCommandDataDeserializer()
        {
            _hash = new Dictionary<Type, IGameCommandDataDeserializer>();
            _hash.Add(typeof(bool), new BoolDeserializer());
            _hash.Add(typeof(int), new IntDeserializer());
            _hash.Add(typeof(float), new FloatDeserializer());
            _hash.Add(typeof(string), new StringDeserializer());
        }

        public static T Deserialize<T>(byte[] data)
        {
            Type type = typeof(T);
            IGameCommandDataDeserializer des = null;
            if (_hash.TryGetValue(type, out des))
                return (T)des.Deserialize(data);
            else
            {
                using (var ms = new MemoryStream(data))
                {
                    return (T)Serializer.Deserialize<T>(ms);
                }
            }
        }
    }

    public interface IGameCommandDataDeserializer
    {
        object Deserialize(byte[] data);
    }

    class BoolDeserializer : IGameCommandDataDeserializer
    {
        public object Deserialize(byte[] data)
        {
            return BitConverter.ToBoolean(data, 0);
        }
    }

    class IntDeserializer : IGameCommandDataDeserializer
    {
        public object Deserialize(byte[] data)
        {
            return BitConverter.ToInt32(data, 0);
        }
    }

    class FloatDeserializer : IGameCommandDataDeserializer
    {
        public object Deserialize(byte[] data)
        {
            return BitConverter.ToSingle(data, 0);
        }
    }

    class StringDeserializer : IGameCommandDataDeserializer
    {
        public object Deserialize(byte[] data)
        {
            return data != null ? Encoding.UTF8.GetString(data) : string.Empty;
        }
    }

}
