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
        public Clock(double freq = 1) // 1 Hz 
        {
            execTimer = new System.Timers.Timer();
            execTimer.Interval = 1000 / freq;   // in ms
            execTimer.Elapsed += Tick;
            execTimer.AutoReset = true;
        }

        public void Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (var s in subscribers)
            {
                s.OnClock();
            }
        }

        public void Register(IClockSink sink)
        {
            subscribers.Add(sink);
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
