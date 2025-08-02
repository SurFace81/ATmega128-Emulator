using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATmegaSim.CPU
{
    public class Commands
    {
        public static void Add(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            byte Rd = Cpu.R[d];
            byte Rr = Cpu.R[r];
            Cpu.R[d] = (byte)(Rd + Rr);

            // Flags
            // ...
        }

        public static void Ldi(ushort opcode)
        {
            int d = 16 + ((opcode >> 4) & 0x0F);                  // 16 <= Rd <= 31
            int k = (opcode & 0x0F) | ((opcode >> 4) & 0xF0);

            Cpu.R[d] = (byte)k;
        }
    }
}
