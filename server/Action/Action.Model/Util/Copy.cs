using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;

namespace Action.Model
{
    public class Copy
    {
        public Copy(CopySetting setting, params Player[] players)
        {
            _setting = setting;

            _members = new CopyMember[players.Length];
            for (int i = 0; i < players.Length; i++)
                _members[i] = new CopyMember(this, players[i]);

            _grids = new CopyGrid[_setting.Styles.Length];
            for (int i = 0; i < _grids.Length; i++)
            {
                _grids[i] = new CopyGrid();
                _grids[i].Update(_setting, (GridStyle)_setting.Styles[i]);
            }
        }

        private CopySetting _setting;
        public CopySetting Setting
        {
            get { return _setting; }
        }

        private CopyGrid[] _grids;
        public CopyGrid[] Grids
        {
            get { return _grids; }
        }

        private CopyMember[] _members;
        private int _turn = 0;

        public CopyMember[] GetAllMembers()
        {
            return _members;
        }

        public CopyMember GetMemberByName(string name)
        {
            return _members.SingleOrDefault(m => m.Player.Name == name);
        }

        public CopyMember GetMemberOnTurn()
        {
            return _members[_turn];
        }

        public bool TurnUp()
        {
            if (_members.Length > 1)
            {
                _turn++;
                if (_turn > _members.Length - 1)
                    _turn = 0;
                return true;
            }
            return false;
        }
    }
}
