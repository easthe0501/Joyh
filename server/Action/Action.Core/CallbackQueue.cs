using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace Action.Core
{
    public class CallbackQueue
    {
        private const int _refreshInterval = 50;
        private ConcurrentQueue<Callback> _queue = new ConcurrentQueue<Callback>();

        public CallbackQueue(string name)
        {
            _name = name;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
        }

        private bool _started;
        public bool Started
        {
            get { return _started; }
        }

        private bool _busy;
        public bool Busy
        {
            get { return _busy; }
        }

        public int Count
        {
            get { return _queue.Count; }
        }

        public void Add(Callback callback)
        {
            _queue.Enqueue(callback);
        }

        public void Start(bool singlePool)
        {
            _started = true;
            if (singlePool)
                System.Threading.Tasks.Task.Factory.StartNew(SyncLoop);
            else
                TimerHelper.Loop(OnLoop, _refreshInterval);
        }

        public void Stop()
        {
            _started = false;
        }

        private void SyncLoop()
        {
            while (_started)
            {
                OnLoop();
                System.Threading.Thread.Sleep(_refreshInterval);
            }
        }

        private void OnLoop()
        {
            if (!_busy)
            {
                _busy = true;
                var callback = _queue.Dequeue();
                while (callback != null)
                {
                    callback();
                    callback = _queue.Dequeue();
                }
                _busy = false;
            }
        }

        private void Trace()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0}Queue started on thread {1}", _name,
                System.Threading.Thread.CurrentThread.ManagedThreadId));
        }
    }
}
