using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace Action.Model
{
    public class Snapshot
    {
        private Dictionary<int, Entity> _entities = new Dictionary<int, Entity>();
        public Dictionary<int, Entity> Entities
        {
            get { return _entities; }
        }

        public T Find<T>(int id) where T : Entity
        {
            return _entities.GetValue(id) as T;
        }

        public void Save<T>(T obj) where T : Entity
        {
            _entities[obj.Id] = obj;
        }

        public void Remove<T>(T obj) where T : Entity
        {
            if (_entities.ContainsKey(obj.Id))
                _entities.Remove(obj.Id);
        }
    }
}
