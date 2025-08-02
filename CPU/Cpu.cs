using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATmegaSim.CPU
{
    public class Cpu
    {
        public struct SREG
        {
            bool C; // Carry flag
            bool Z; // Zero result in an arithmetic or logic operation
            bool N; // Negative result in an arithmetic or logic operation
            bool V; // Two's Complement Overflow Flag V supports two's complement arithmetic (?)
            bool S; // Sgin bit
            bool H; // Half carry in some arithmetic operations
            bool T; // Bit Copy Storage
            bool I; // Global Interrupt Enable
        }
        public static byte[] R = new byte[32];  // Base registers R0 - R31
        public static ushort PC { get; set; }   // Program Counter

        public void Step(ushort opcode)
        {
            ExecInstruction(opcode);
            PC += 2;
        }

        private void ExecInstruction(ushort opcode)
        {
            if (((opcode & 0xFC00) >> 10) == 0b0011)
            {
                Commands.Add(opcode);
            }
            if (((opcode & 0xF000) >> 12) == 0b1110)
            {
                Commands.Ldi(opcode);
            }
        }
    }
}
