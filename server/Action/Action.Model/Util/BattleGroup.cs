using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;

namespace Action.Model
{
    public class BattleGroup
    {
        public BattleGroup(BattleGroupType type, string name, int sex, IEnumerable<BattleFighter> fighters)
        {
            _type = type;
            _name = name;
            _sex = sex;
            foreach (var fighter in fighters)
            {
                fighter.FriendGroup = this;
                _fighters[fighter.Pos] = fighter;
            }
        }

        private BattleGroupType _type;
        public BattleGroupType Type
        {
            get { return _type; }
        }

        private string _name;
        public string Name 
        {
            get { return _name;}
        }

        private int _sex;
        public int Sex
        {
            get { return _sex; }
        }

        public void GenId()
        {

        }

        private Dictionary<int, BattleFighter> _fighters = new Dictionary<int, BattleFighter>();
        public BattleFighter GetAliveFighter(int pos)
        {
            var fighter = _fighters.GetValue(pos);
            return fighter != null && fighter.IsAlive ? fighter : null;
        }

        public IEnumerable<BattleFighter> GetAliveFighters()
        {
            return _fighters.Values.Where(f=>f.IsAlive).OrderBy(f => f.Pos);
        }

        public bool AllDied()
        {
            return _fighters.Values.Count(f => f.IsAlive) == 0;
        }

        /// <summary>
        /// 获取总先攻
        /// </summary>
        /// <returns></returns>
        public int GetSumFirstStrike()
        {
            return _fighters.Values.Sum(f => f.FirstStrike);
        }
    }

    public enum BattleGroupType
    {
        Player = 1,
        Environment = 2
    }
}
