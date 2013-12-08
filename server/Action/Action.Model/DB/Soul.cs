using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Action.Model
{
    public class Soul : EntityEx, IComparable<Soul>
    {
        [BsonIgnore]
        /// <summary>
        /// 战魂的静态配置信息
        /// </summary>
        public SoulSetting Setting
        {
            get { return APF.Settings.Souls.Find(SettingId); }
        }

        /// <summary>
        /// 战魂所处位置
        /// </summary>
        public int Pos { get; set; }

        /// <summary>
        /// 战魂等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 战魂经验
        /// </summary>
        public int Exp { get; set; }

        private Buff _buff = new Buff();
        [BsonIgnore]
        /// <summary>
        /// 战魂Buff（受等级影响）
        /// </summary>
        public Buff Buff
        {
            get { return _buff; }
        }

        /// <summary>
        /// 刷新战魂属性
        /// </summary>
        public void Refresh()
        {
            _buff.Effect = Setting.Buff.Effect;
            _buff.Data = Setting.Buff.Data * Level;
        }

        public int GetNextLevelExp()
        {
            return Setting.InitExp * (int)Math.Pow(2, Level);
        }

        /// <summary>
        /// 比较战魂
        /// </summary>
        /// <param name="other"></param>
        /// <returns>other大返回1,相等返回0,other小返回-1</returns>
        public int CompareTo(Soul other)
        {
            if (this.Setting.Quality < other.Setting.Quality)
                return 1;
            if (this.Setting.Quality == other.Setting.Quality)
            {
                if (this.Level < other.Level)
                    return 1;
                if (this.Level == other.Level)
                {
                    if (this.Exp < other.Exp)
                        return 1;
                    if (this.Exp == other.Exp)
                        return 0;
                }
            }

            return -1;
        }

    }
}
