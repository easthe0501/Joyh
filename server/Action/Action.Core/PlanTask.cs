using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Action.Core
{
    public class PlanTask
    {
        private int _hour = -1;
        private int _minute = -1;
        private int _second = -1;
        private Timer _initTimer;
        private Timer _tickTimer;
        public event ElapsedEventHandler Elapsed;

        public PlanTask(int hour, int minute, int second, int interval)
        {
            _hour = hour;
            _minute = minute;
            _second = second;
            _initTimer = new Timer(1000);
            _initTimer.Elapsed += _initTimer_Tick;
            _tickTimer = new Timer(interval);
            _tickTimer.Elapsed += _tickTimer_Tick;
        }

        private void _initTimer_Tick(object sender, ElapsedEventArgs e)
        {
            var now = DateTime.Now;
            if (_hour != -1 && now.Hour != _hour)
                return;
            if (_minute != -1 && now.Minute != _minute)
                return;
            if (_second != -1 && now.Second != _second)
                return;

            _initTimer.Stop();
            _tickTimer.Start();
            _tickTimer_Tick(sender, e);
        }

        private void _tickTimer_Tick(object sender, ElapsedEventArgs e)
        {
            if (Elapsed != null)
                Elapsed(this, e);
        }

        private bool _started;
        public bool Started
        {
            get { return _started; }
        }

        public void Start()
        {
            _tickTimer.Stop();
            _initTimer.Start();
            _started = true;
        }

        public void Stop()
        {
            _initTimer.Stop();
            _tickTimer.Stop();
            _started = false;
        }
    }
}
