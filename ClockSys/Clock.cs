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

        public void Register(IClockSink sink)
        {
            subscribers.Add(sink);
        }

        public void Tick(int cycles)
        {
            TotalCycles += (ulong)cycles;
            foreach (var s in subscribers)
            {
                s.OnClock(cycles);
            }                
        }
    }
}
