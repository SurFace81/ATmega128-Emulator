using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATmegaSim.CPU
{
    public class Disassembler
    {
        public string DisasmInstruction(ushort opcode)
        {
            if (opcode == 0x0000)
            {
                return Nop(opcode);
            }
            if (((opcode & 0xFC00) >> 10) == 0b0011)
            {
                return Add(opcode);
            }
            if (((opcode & 0xFC00) >> 10) == 0b0111)
            {
                return Adc(opcode);
            }
            if (((opcode & 0xF000) >> 12) == 0b1110)
            {
                return Ldi(opcode);
            }
            if (((opcode & 0xFC00) >> 10) == 0b100111)
            {
                return Mul(opcode);
            }
            if (((opcode & 0xF800) >> 11) == 0b10111)
            {
                return Out(opcode);
            }
            if (((opcode & 0xF800) >> 11) == 0b10110)
            {
                return In(opcode);
            }

            return "???";
        }

        public string Nop(ushort opcode)
        {
            return $"NOP";
        }

        public string Add(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"ADD R{d}, R{r}";
        }

        public string Adc(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"ADC R{d}, R{r}";
        }

        public string Ldi(ushort opcode)
        {
            int d = 16 + ((opcode >> 4) & 0x0F);
            int k = (opcode & 0x0F) | ((opcode >> 4) & 0xF0);

            return $"LDI R{d}, 0x{k:X2}";
        }

        public string Mul(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"MUL R{d}, R{r}";
        }

        public string Out(ushort opcode)
        {
            int A = (opcode & 0x0F) | ((opcode >> 5) & 0x30);
            int r = (opcode >> 4) & 0x1F;

            return $"OUT 0x{A.ToString("X2")}, R{r}";
        }

        public string In(ushort opcode)
        {
            int A = (opcode & 0x0F) | ((opcode >> 5) & 0x30);
            int d = (opcode >> 4) & 0x1F;

            return $"IN R{d}, 0x{A.ToString("X2")}";
        }
    }
}
