using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ProtoBuf;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.Common;
using Action.Core;

namespace Action.Engine
{
    public interface IActionSession
    {
        bool Enabled { get; set; }
        string IdentityKey { get; }
        IActionServer ActionServer { get; }
        void Close();
        bool ExecuteCommand(int commandId);
        bool ExecuteCommand(int commandId, bool data);
        bool ExecuteCommand(int commandId, int data);
        bool ExecuteCommand(int commandId, float data);
        bool ExecuteCommand(int commandId, string data);
        bool ExecuteCommand(int commandId, object data);
        void SendResponse(int commandId);
        void SendResponse(int commandId, bool data);
        void SendResponse(int commandId, int data);
        void SendResponse(int commandId, float data);
        void SendResponse(int commandId, string data);
        void SendResponse(int commandId, object data);
    }

    public class ActionSession<TAppSession> : AppSession<TAppSession, BinaryCommandInfo>, IActionSession
        where TAppSession : SuperSocket.SocketBase.IAppSession, SuperSocket.SocketBase.IAppSession<TAppSession, BinaryCommandInfo>, new()
    {
        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = true; }
        }

        public IActionServer ActionServer
        {
            get { return (IActionServer)base.AppServer; }
        }

        public new ILogger Logger
        {
            get { return ActionServer.Logger; }
        }

        public override void StartSession()
        {
            if (!ActionServer.Opened)
                Close();
        }

        public override void HandleUnknownCommand(BinaryCommandInfo cmdInfo)
        {
            Close();
        }

        public bool ExecuteCommand(int commandId)
        {
            var command = ActionServer.FindCommand(commandId);
            if (command != null)
            {
                command.ExecuteCommand2(this, new BinaryCommandInfo(commandId.ToString(), new byte[0]));
                return true;
            }
            return false;
        }

        public bool ExecuteCommand(int commandId, bool data)
        {
            var command = ActionServer.FindCommand(commandId);
            if (command != null)
            {
                byte[] bytes = BitConverter.GetBytes(data);
                command.ExecuteCommand2(this, new BinaryCommandInfo(commandId.ToString(), bytes));
                return true;
            }
            return false;
        }

        public bool ExecuteCommand(int commandId, int data)
        {
            var command = ActionServer.FindCommand(commandId);
            if (command != null)
            {
                byte[] bytes = BitConverter.GetBytes(data);
                command.ExecuteCommand2(this, new BinaryCommandInfo(commandId.ToString(), bytes));
                return true;
            }
            return false;
        }

        public bool ExecuteCommand(int commandId, float data)
        {
            var command = ActionServer.FindCommand(commandId);
            if (command != null)
            {
                byte[] bytes = BitConverter.GetBytes(data);
                command.ExecuteCommand2(this, new BinaryCommandInfo(commandId.ToString(), bytes));
                return true;
            }
            return false;
        }

        public bool ExecuteCommand(int commandId, string data)
        {
            var command = ActionServer.FindCommand(commandId);
            if (command != null)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                command.ExecuteCommand2(this, new BinaryCommandInfo(commandId.ToString(), bytes));
                return true;
            }
            return false;
        }

        public bool ExecuteCommand(int commandId, object data)
        {
            var command = ActionServer.FindCommand(commandId);
            if (command != null)
            {
                byte[] bytes = null;
                using (var ms = new MemoryStream())
                {
                    Serializer.NonGeneric.Serialize(ms, data);
                    bytes = ms.ToArray();
                }
                command.ExecuteCommand2(this, new BinaryCommandInfo(commandId.ToString(), bytes));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Only send cmdId, no data, package size is 0
        /// </summary>
        /// <param name="commandId"></param>
        public void SendResponse(int commandId)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(BitConverter.GetBytes(commandId));
                ms.Write(BitConverter.GetBytes(0));
                SendResponse(ms.ToArray());
            }
        }

        public void SendResponse(int commandId, bool data)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(BitConverter.GetBytes(commandId));
                ms.Write(BitConverter.GetBytes(1));
                ms.Write(BitConverter.GetBytes(data));
                SendResponse(ms.ToArray());
            }
        }

        public void SendResponse(int commandId, int data)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(BitConverter.GetBytes(commandId));
                ms.Write(BitConverter.GetBytes(4));
                ms.Write(BitConverter.GetBytes(data));
                SendResponse(ms.ToArray());
            }
        }

        public void SendResponse(int commandId, float data)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(BitConverter.GetBytes(commandId));
                ms.Write(BitConverter.GetBytes(4));
                ms.Write(BitConverter.GetBytes(data));
                SendResponse(ms.ToArray());
            }
        }

        public void SendResponse(int commandId, string data)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(BitConverter.GetBytes(commandId));
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                ms.Write(BitConverter.GetBytes(bytes.Length));
                ms.Write(bytes);
                SendResponse(ms.ToArray());
            }
        }

        /// <summary>
        /// Send cmdId and protobuf data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandId"></param>
        /// <param name="data"></param>
        public void SendResponse<T>(int commandId, T data) where T : class
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(BitConverter.GetBytes(commandId));
                using (var ms2 = new MemoryStream())
                {
                    Serializer.Serialize(ms2, data);
                    ms.Write(BitConverter.GetBytes((int)ms2.Length));
                    ms.Write(ms2.ToArray());
                }
                SendResponse(ms.ToArray());
            }
        }

        /// <summary>
        /// Send cmdId and protobuf data
        /// </summary>
        /// <param name="commandId"></param>
        /// <param name="data"></param>
        public void SendResponse(int commandId, object data)
        {
            SendResponse(BitConverter.GetBytes(commandId));
            using (var ms = new MemoryStream())
            {
                Serializer.NonGeneric.Serialize(ms, data);
                SendResponse(BitConverter.GetBytes((int)ms.Length));
                SendResponse(ms.ToArray());
            }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", base.ToString(), IdentityKey);
        }
    }
}
