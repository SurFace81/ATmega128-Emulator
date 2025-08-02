using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATmegaSim.ClockSys
{
    public interface IClockSink
    {
        void OnClock(int cycles);
    }
}
