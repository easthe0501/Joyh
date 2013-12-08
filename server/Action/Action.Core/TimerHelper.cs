using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Action.Core
{
    public static class TimerHelper
    {
        public static void Delay(Callback callback, int interval, bool replay = false)
        {
            var timer = new Timer(interval);
            timer.Elapsed += (o, e) =>
            {
                if (!replay)
                    timer.Stop();
                callback();
            };
            timer.Start();
        }

        public static void Loop(Callback callback, int interval)        
        {
            callback();
            Delay(callback, interval, true);
        }
    }
}
