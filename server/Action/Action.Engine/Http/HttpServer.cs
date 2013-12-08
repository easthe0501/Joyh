using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using SuperSocket.Common;

namespace Action.Engine
{
    public class HttpServer
    {
        private ILogger _logger;
        public ILogger Logger
        {
            get { return _logger; }
        }

        private HttpListener _listener;
        public HttpDataReceivedEventHandler DataReceived;

        public HttpServer(string name, string url)
        {
            _logger = ServerContext.LoggerFactory.CreateLogger(name);
            _listener = new HttpListener();
            _listener.Prefixes.Add(url);
        }

        public void Start()
        {
            _listener.Start();
            _listener.BeginGetContext(new AsyncCallback(OnGetContext), _listener);
        }

        public void Stop()
        {
            _listener.Stop();
        }

        private void OnGetContext(IAsyncResult ar)
        {
            var socket = ar.AsyncState as HttpListener;
            try
            {
                if (socket.IsListening)
                {
                    var context = socket.EndGetContext(ar);
                    if (DataReceived != null)
                        DataReceived(context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
            if (socket.IsListening)
                socket.BeginGetContext(new AsyncCallback(OnGetContext), socket);
        }
    }
}
