using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Action.Core;

namespace Action.Client
{
    public class CommandFactory
    {
        private static CommandFactory _current = new CommandFactory();
        public static CommandFactory Current
        {
            get { return _current; }
        }

        private Dictionary<int, ICommand> _commandHash = new Dictionary<int, ICommand>();

        public void AddCommand(ICommand command)
        {
            _commandHash.Add(command.Id, command);
        }

        public ICommand FindCommand(int id)
        {
            return _commandHash.GetValue(id);
        }
    }
}
