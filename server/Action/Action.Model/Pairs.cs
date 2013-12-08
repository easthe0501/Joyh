using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Action.Model
{
    public class IdCountPair
    {
        public static IdCountPair Create(int id, int count)
        {
            return new IdCountPair() { Id = id, Count = count };
        }

        public IdCountPair()
        {
            Percent = 100;
        }

        public int Id { get; set; }
        public int Count { get; set; }
        public int Percent { get; set; }
    }

    public class LevelPosPair
    {
        public int Level { get; set; }
        public int Pos { get; set; }
    }

    public class MonsterLayout
    {
        public Monster Monster { get; private set; }
        public int Pos { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public int JobId { get; set; }
        public int SkillId { get; set; }
        public int HP { get; set; }
        public int XP { get; set; }

        public void Init()
        {
            Monster = new Monster(Id, Level, JobId, SkillId, HP, XP);
        }
    }
}
