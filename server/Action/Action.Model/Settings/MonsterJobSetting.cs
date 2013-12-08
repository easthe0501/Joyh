using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class MonsterJobSetting : JsonSetting, IFighter
    {
        public int HP { get; set; }
        public int XP { get; set; }
        public int CommonAttack { get; set; }
        public int CommonDefence { get; set; }
        public int SkillAttack { get; set; }
        public int SkillDefence { get; set; }
        public int MindAttack { get; set; }
        public int MindDefence { get; set; }
        public int Hit { get; set; }
        public int Dodge { get; set; }
        public int Crack { get; set; }
        public int Block { get; set; }
        public int Crit { get; set; }
        public int Tenacity { get; set; }
        public int SkillId { get; set; }
        public int[] ImmuneBuffs { get; set; }

        public int FirstStrike
        {
            get { return 0; }
        }

        public int Level
        {
            get { return 0; }
        }

        public int SettingId
        {
            get { return 0; }
        }
    }
}
