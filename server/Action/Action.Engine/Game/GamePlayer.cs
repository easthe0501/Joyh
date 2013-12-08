using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;

namespace Action.Engine
{
    public class GamePlayerDisplay
    {
        public int Job { get; set; }
        public int Sex { get; set; }
        public int Face { get; set; }
    }

    public class GamePlayerPermission
    {
        private HashSet<int> _commandIds = new HashSet<int>();
        public HashSet<int> CommandIds
        {
            get { return _commandIds; }
        }

        public void Add(int commandId)
        {
            _commandIds.Add(commandId);
        }

        public void Remove(int commandId)
        {
            _commandIds.Remove(commandId);
        }

        public void Import(IEnumerable<int> commandIds)
        {
            _commandIds.Clear();
            foreach (var commandId in commandIds)
                _commandIds.Add(commandId);
        }

        public bool Contains(int commandId)
        {
            return _commandIds.Contains(commandId);
        }
    }

    public enum LoginStatus
    {
        CreateSocket,
        EnterGate,
        EnterGame
    }

    public class GamePlayer
    {
        private GameSession _session;
        public GameSession Session
        {
            get { return _session; }
        }

        private GamePlayerDisplay _display = new GamePlayerDisplay();
        public GamePlayerDisplay Display
        {
            get { return _display; }
            set { _display = value; }
        }

        private GamePlayerPermission _permission = new GamePlayerPermission();
        public GamePlayerPermission Permission
        {
            get { return _permission; }
            set { _permission = value; }
        }

        private HashSet<int> _attentions = new HashSet<int>();
        public HashSet<int> Attentions
        {
            get { return _attentions; }
        }

        public GamePlayer(GameSession session)
        {
            _session = session;
        }

        public string Key { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public LoginStatus Status { get; set; }
        public IPlayerData Data { get; set; }

        public GameWorld World
        {
            get { return _session.Server.World; }
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, Account);
        }
    }
}
