using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATmegaSim.CPU
{
    public class IOPort
    {
        private readonly byte mask;
        public byte DDR;   // Data Direction Register: 1 — output, 0 — input
        public byte PORT;  // Output value
        public byte PIN;   // Input value

        public IOPort(byte mask = 0xFF)
        {
            this.mask = mask;
        }

        public byte ReadPin()
        {
            return (byte)(((PIN & ~DDR) | (PORT & DDR)) & mask);
        }

        public void WritePort(byte value)
        {
            PORT = (byte)(value & mask);
        }

        public void WriteDDR(byte value)
        {
            DDR = (byte)(value & mask);
        }

        public void SetExternalInput(byte external)
        {
            PIN = (byte)(external & mask);
        }
    }
}
