using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATmegaSim.CPU
{
    public class IOPort
    {
        public byte DDR;   // Data Direction Register: 1 — output, 0 — input
        public byte PORT;  // In/Out values

        public byte ReadPort()
        {
            return PORT;
        }

        public void WritePort(byte value)
        {
            PORT = value;
        }

        public void WriteDDR(byte value)
        {
            DDR = value;
        }
    }
}
