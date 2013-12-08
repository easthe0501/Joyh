using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using ProtoBuf;

namespace Action.Client
{
    public class ActionTcpClient
    {
        private object _lock = new object();
        private TcpClient _socket;
        private MemoryStream _writerStream;
        private NetworkStream _sharedStream;
        private BinaryReader _reader;
        private BinaryWriter _writer;
        private Thread _thread;

        public object Context { get; set; }
        public event EventHandler SocketClosed;

        public ActionTcpClient()
        {
            Reset();
        }

        private void Reset()
        {
            _socket = new TcpClient();
            _socket.NoDelay = true;
            if (_reader != null)
            {
                _reader.Close();
                _reader.Dispose();
                _reader = null;
            }
            if (_writer != null)
            {
                _writer.Close();
                _writer.Dispose();
            }
            _writer = new BinaryWriter(_writerStream = new MemoryStream());
            //if (_thread != null && _thread.ThreadState == ThreadState.Running)
            //    _thread.Abort();
        }

        public bool Connected
        {
            get { return _socket.Connected; }
        }

        public bool Connect(string host, int port)
        {
            try
            {
                if (!_socket.Connected)
                {
                    _socket.Connect(host, port);
                    _reader = new BinaryReader(_sharedStream = _socket.GetStream());
                    _thread = new Thread(new ThreadStart(ReadWriteStream));
                    _thread.Start();
                }
                return true;
            }
            catch(SocketException)
            {
                return false;
            }
        }

        public void Close()
        {
            if (_socket.Connected)
            {
                _socket.Close();
                OnClosed();
            }
        }

        private void OnClosed()
        {
            Reset();
            if (SocketClosed != null)
                SocketClosed(this, null);
        }

        private void ReadWriteStream()
        {
            while (_socket.Connected)
            {
                ReadStream();
                WriteStream();
                //Thread.Sleep(100);
            }
        }

        private void ReadStream()
        {
            if (_sharedStream.DataAvailable)
            {
                var command = CommandFactory.Current.FindCommand(_reader.ReadInt32());
                if (command != null)
                {
                    var length = _reader.ReadInt32();
                    command.Execute(this, length > 0 ? _reader.ReadBytes(length) : new byte[0]);
                }
            }
        }

        private void WriteStream()
        {
            try
            {
                if (_writerStream.Length > 0)
                {
                    lock (_lock)
                    {
                        var bytes = _writerStream.ToArray();
                        _sharedStream.Write(bytes, 0, bytes.Length);
                        _sharedStream.Flush();
                        _writerStream.SetLength(0);
                    }
                }
            }
            catch (Exception)
            {
                OnClosed();
            }
        }

        public void Send(int id)
        {
            lock (_lock)
            {
                if (_socket.Connected)
                {
                    _writer.Write(id);
                    _writer.Write(0);
                    _writer.Flush();
                }
            }
        }

        public void Send(int id, bool args)
        {
            lock (_lock)
            {
                if (_socket.Connected)
                {
                    _writer.Write(id);
                    _writer.Write(1);
                    _writer.Write(args);
                    _writer.Flush();
                }
            }
        }

        public void Send(int id, int args)
        {
            lock (_lock)
            {
                if (_socket.Connected)
                {
                    _writer.Write(id);
                    _writer.Write(4);
                    _writer.Write(args);
                    _writer.Flush();
                }
            }
        }

        public void Send(int id, float args)
        {
            lock (_lock)
            {
                if (_socket.Connected)
                {
                    _writer.Write(id);
                    _writer.Write(4);
                    _writer.Write(args);
                    _writer.Flush();
                }
            }
        }

        public void Send(int id, string args)
        {
            lock (_lock)
            {
                if (_socket.Connected)
                {
                    var bytes = Encoding.UTF8.GetBytes(args);
                    _writer.Write(id);
                    _writer.Write(bytes.Length);
                    _writer.Write(bytes);
                    _writer.Flush();
                }
            }
        }

        public void Send<T>(int id, T args) where T : class
        {
            lock (_lock)
            {
                if (_socket.Connected)
                {
                    using (var ms = new MemoryStream())
                    {
                        Serializer.Serialize<T>(ms, args);
                        _writer.Write(id);
                        _writer.Write((int)ms.Length);
                        _writer.Write(ms.ToArray());
                    }
                    _writer.Flush();
                }
            }
        }

        public void Send(int id, byte[] args)
        {
            lock (_lock)
            {
                if (_socket.Connected)
                {
                    _writer.Write(id);
                    _writer.Write(args.Length);
                    _writer.Write(args);
                    _writer.Flush();
                }
            }
        }
    }
}
