using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATmegaSim.CPU
{
    public class Disassembler
    {
        public static string Add(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"ADD R{d}, R{r}";
        }

        public static string Ldi(ushort opcode)
        {
            int d = 16 + ((opcode >> 4) & 0x0F);
            int k = (opcode & 0x0F) | ((opcode >> 4) & 0xF0);

            return $"LDI R{d}, 0x{k:X2}";
        }

        public static string Mul(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"MUL R{d}, R{r}";
        }
    }
}
