using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class FactoryFacade
    {
        public T Create<T>(IEntityRoot root) where T : Entity, new()
        {
            T obj = new T();
            obj.Init(root);
            return obj;
        }

        public T Create<T>(IEntityRoot root, int settingId) where T : EntityEx, new()
        {
            T obj = new T();
            obj.Init(root, settingId);
            return obj;
        }
    }
}
