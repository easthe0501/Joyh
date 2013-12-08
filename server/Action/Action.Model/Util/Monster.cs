using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class Monster : IFighter
    {
        public Monster(int id, int level, int jobId, int skillId, int hp, int xp)
        {
            SettingId = id;
            Level = level;
            var rate = APF.Common.GetLevelRate(level);

            var monsterJob = APF.Settings.MonsterJobs.Find(jobId);
            SkillId = skillId > 0 ? skillId : monsterJob.SkillId;
            HP = hp > 0 ? hp : (int)(monsterJob.HP * rate);
            XP = xp > 0 ? xp : monsterJob.XP;
            CommonAttack = (int)(monsterJob.CommonAttack * rate);
            CommonDefence = (int)(monsterJob.CommonDefence * rate);
            SkillAttack = (int)(monsterJob.SkillAttack * rate);
            SkillDefence = (int)(monsterJob.SkillDefence * rate);
            MindAttack = (int)(monsterJob.MindAttack * rate);
            MindDefence = (int)(monsterJob.MindDefence * rate);

            var hlv = level / 2;
            Hit = monsterJob.Hit + hlv;
            Dodge = monsterJob.Dodge + hlv;
            Crack = monsterJob.Crack + hlv;
            Block = monsterJob.Block + hlv;
            Crit = monsterJob.Crit + hlv;
            Tenacity = monsterJob.Tenacity + hlv;

            ImmuneBuffs = monsterJob.ImmuneBuffs;
        }

        public int Level { get; private set; }
        public int HP { get; private set; }
        public int XP { get; private set; }
        public int CommonAttack { get; private set; }
        public int CommonDefence { get; private set; }
        public int SkillAttack { get; private set; }
        public int SkillDefence { get; private set; }
        public int MindAttack { get; private set; }
        public int MindDefence { get; private set; }
        public int Hit { get; private set; }
        public int Dodge { get; private set; }
        public int Crack { get; private set; }
        public int Block { get; private set; }
        public int Crit { get; private set; }
        public int Tenacity { get; private set; }
        public int SkillId { get; private set; }
        public int SettingId { get; private set; }
        public int[] ImmuneBuffs { get; private set; }

        public int FirstStrike
        {
            get { return 0; }
        }
    }
}
