using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using System.ComponentModel.Composition;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketBase.Command;
using SuperSocket.Common;
using Action.Core;

namespace Action.Engine
{
    public interface IActionServer
    {
        bool Opened { get; set; }
        ScriptEngine ScriptEngine { get; }
        ILogger Logger { get; }
        IEnumerable<IActionSession> GetActionSessions();
        IActionCommand FindCommand(int commandId);
    }

    public abstract class ActionServer<TAppSession> : AppServer<TAppSession, BinaryCommandInfo>, IActionServer
        where TAppSession : SuperSocket.SocketBase.IAppSession<TAppSession, BinaryCommandInfo>, new()
    {
        public ActionServer()
            : base(new ActionProtocol())
        {
            _scriptEngine = new ScriptEngine(this);
        }

        protected bool _opened = false;
        public bool Opened
        {
            get { return _opened; }
            set { _opened = value; }
        }

        private ScriptEngine _scriptEngine;
        public ScriptEngine ScriptEngine
        {
            get { return _scriptEngine; }
        }

        private ILogger _logger;
        public new ILogger Logger
        {
            get { return _logger; }
        }

        private Dictionary<int, IActionCommand> _idCommands = new Dictionary<int, IActionCommand>();
        protected Dictionary<Type, ICommand<TAppSession, BinaryCommandInfo>> _typeCommands = new Dictionary<Type, ICommand<TAppSession, BinaryCommandInfo>>();
        //protected abstract IEnumerable<Lazy<ICommand<TAppSession, BinaryCommandInfo>, ICommandMetaData>> Commands { get; }
        public abstract IEnumerable<ActionCommandBase<TAppSession>> Commands{get;}

        protected override bool SetupCommands(Dictionary<string, ICommand<TAppSession, BinaryCommandInfo>> commandDict)
        {
            _logger = ServerContext.LoggerFactory.CreateLogger(Name);

            if (!Composition.ComposeParts(this))
            {
                Logger.LogError("反射Command失败");
                return false;
            }

            foreach (var cmd in Commands)
            {
                Logger.LogDebug(string.Format("发现Command: {0} - {1}", cmd.ToString(), cmd.GetType().AssemblyQualifiedName));
                
                if (commandDict.ContainsKey(cmd.Name.ToString()))
                {
                    Logger.LogError(string.Format("找到了相同编号的Command : {0}", cmd.ToString()));
                    return false;
                }

                commandDict.Add(cmd.ID.ToString(), cmd);
                _typeCommands.Add(cmd.GetType(), cmd);
                _idCommands.Add(cmd.ID, cmd);
            }

            CommandFilterFactory.GenerateCommandFilterLibrary(this.GetType(), commandDict.Values.Cast<ICommand>());
            return true;
        }

        protected override void OnStartup()
        {
            base.OnStartup();
            ServerContext.RegisterServer(Name, this);
        }

        public IEnumerable<IActionSession> GetActionSessions()
        {
            return GetAllSessions().Cast<IActionSession>();
        }

        public IActionCommand FindCommand(int commandId)
        {
            IActionCommand command = null;
            if (_idCommands.TryGetValue(commandId, out command))
                return command;
            return null;
        }
    }
}
