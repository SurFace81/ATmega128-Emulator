using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATmegaSim.ClockSys
{
    public class Clock
    {
        public ulong TotalCycles { get; private set; } = 0;
        private List<IClockSink> subscribers = new List<IClockSink>();

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
            TotalCycles += 1;
            foreach (var s in subscribers.ToArray())
            {
                s.OnClock();
            }
        }

        public void ChangeClockDelay(int delay)
        {
            execTimer.Interval = delay;
        }

        public void Register(IClockSink sink)
        {
            if (!subscribers.Contains(sink))
                subscribers.Add(sink);
        }

        public void Unregister(IClockSink sink)
        {
            subscribers.Remove(sink);
        }

        public void Start()
        {
            execTimer.Enabled = true;
            execTimer.Start();
        }

        public void Stop()
        {
            execTimer.Enabled = false;
            execTimer.Stop();
        }
    }
}
