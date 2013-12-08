using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action.Core;

namespace Action.Client
{
    public interface ICommand
    {
        int Id { get; }
        void Execute(ActionTcpClient client, byte[] args = null);
    }

    public abstract class ActionCommand : ICommand
    {
        public abstract int Id { get; }
        protected abstract void Run(ActionTcpClient client);

        public void Execute(ActionTcpClient client, byte[] args = null)
        {
            Run(client);
        }
    }

    public abstract class ActionCommand<T> : ICommand
    {
        public abstract int Id { get; }
        protected abstract void Run(ActionTcpClient client, T args);

        public void Execute(ActionTcpClient client, byte[] args = null)
        {
            Run(client, ActionCommandDataDeserializer.Deserialize<T>(args));
        }
    }

}
