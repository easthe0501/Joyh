using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using Action.Engine;
using System.Collections;

namespace Action.Model
{
    public abstract class Entity
    {
        /// <summary>
        /// 动态Id
        /// </summary>
        public int Id { get; private set; }

        [BsonIgnore]
        /// <summary>
        /// 根节点
        /// </summary>
        public IEntityRoot Root { get; private set; }

        public void Init(IEntityRoot root)
        {
            Id = root.IdGen.Next();
            OnInit(root);
            Load(root);
        }

        public void Load(IEntityRoot root)
        {
            (Root = root).Snapshot.Save(this);
            OnLoad(root);
        }

        public void Destory()
        {
            Root.Snapshot.Remove(this);
        }

        protected virtual void OnInit(IEntityRoot root)
        {
        }

        protected virtual void OnLoad(IEntityRoot root)
        {
        }
    }

    public abstract class EntityEx : Entity
    {
        /// <summary>
        /// 配置Id
        /// </summary>
        public int SettingId { get; set; }

        public void Init(IEntityRoot root, int settingId)
        {
            SettingId = settingId;
            base.Init(root);
        }
    }
}
