using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using System.Drawing;
using Action.Core;

namespace Action.Model
{
    public class Building : EntityEx
    {
        protected override void OnInit(IEntityRoot root)
        {
            AcquireTime = DateTime.Now;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 设施所处位置X
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// 设施所处位置Y
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// 是否是固定（不可删除不可移动）的
        /// </summary>
        public bool Fixed { get; set; }

        /// <summary>
        /// 收获时间
        /// </summary>
        public DateTime AcquireTime { get; set; }

        /// <summary>
        /// 建造时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        [BsonIgnore]
        /// <summary>
        /// 设施的静态配置信息
        /// </summary>
        public BuildingSetting Setting
        {
            get { return APF.Settings.Buildings.Find(SettingId); }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(X, Y, Setting.Width, Setting.Height);
        }

        public bool Intersect(Building other)
        {
            var curBound = this.GetBounds();
            var tarBound = other.GetBounds();
            return curBound.IntersectsWith(tarBound)
                || curBound.Contains(tarBound)
                || tarBound.Contains(curBound);
        }

        protected override void OnLoad(IEntityRoot root)
        {
            AcquireTime = AcquireTime.ToLocalTime();
            CreateTime = CreateTime.ToLocalTime();
        }
    }
}
