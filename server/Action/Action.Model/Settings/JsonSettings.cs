using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Action.Utility;

namespace Action.Model
{
    public interface IJsonSettings
    {
        object Find(int id);
    }

    public class JsonSetting
    {
        public int Id { get; set; }
        public static T Deserialize<T>()
        {
            var path = APF.Settings.GetFullPath(string.Format("Settings/Json/{0}.json", typeof(T).Name));
            var json = File.ReadAllText(path);
            return JsonHelper.FromJson<T>(json);
        }
    }

    public class JsonSettings<T> : IJsonSettings where T : JsonSetting
    {
        private Dictionary<int, T> _hash = new Dictionary<int, T>();

        public JsonSettings()
        {
            var path = APF.Settings.GetFullPath(string.Format("Settings/Json/{0}s.json", typeof(T).Name));
            var json = File.ReadAllText(path);
            IEnumerable<T> collection = JsonHelper.FromJson<IEnumerable<T>>(json);
            foreach (var item in collection)
                _hash[item.Id] = item;
        }

        public T Find(int id)
        {
            T item = default(T);
            if (_hash.TryGetValue(id, out item))
                return item;
            return default(T);
        }

        public IEnumerable<T> All
        {
            get { return _hash.Values; }
        }

        object IJsonSettings.Find(int id)
        {
            return Find(id);
        }
    }
}
