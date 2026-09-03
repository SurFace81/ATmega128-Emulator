using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATmegaSim.ClockSys
{
    public class Clock
    {
        public ulong TotalCycles { get; private set; } = 0;
        private List<IClockSink> subscribers = new List<IClockSink>();
        private readonly object timerLock = new object();
        private int tickInProgress;

        System.Timers.Timer execTimer;
        public Clock(int delay)
        {
            execTimer = new System.Timers.Timer();
            execTimer.Interval = delay;
            execTimer.Elapsed += Tick;
            execTimer.AutoReset = true;
        }

        public void Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Interlocked.CompareExchange(ref tickInProgress, 1, 0) != 0)
                return;

            try
            {
                TotalCycles += 1;
                IClockSink[] sinks;
                lock (timerLock)
                {
                    sinks = subscribers.ToArray();
                }
                foreach (var s in sinks)
                {
                    s.OnClock();
                }
            }
            finally
            {
                Interlocked.Exchange(ref tickInProgress, 0);
            }
        }

        public void ChangeClockDelay(int delay)
        {
            if (delay < 1)
                throw new ArgumentOutOfRangeException(nameof(delay));
            lock (timerLock)
            {
                execTimer.Interval = delay;
            }
        }

        public void Register(IClockSink sink)
        {
            lock (timerLock)
            {
                if (!subscribers.Contains(sink))
                    subscribers.Add(sink);
            }
        }

        public void Unregister(IClockSink sink)
        {
            lock (timerLock)
            {
                subscribers.Remove(sink);
            }
        }

        public void Start()
        {
            lock (timerLock)
            {
                execTimer.Enabled = true;
                execTimer.Start();
            }
        }

        public void Stop()
        {
            lock (timerLock)
            {
                execTimer.Enabled = false;
                execTimer.Stop();
            }
        }
    }
}
