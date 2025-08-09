using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ATmegaSim.CPU
{
    public class Disassembler
    {
        public string DisasmInstruction(ushort opcode1, ushort opcode2 = 0)
        {
            if (opcode1 == 0x0000)
            {
                return Nop(opcode1);
            }
            if (((opcode1 & 0xFC00) >> 10) == 0b0011)
            {
                return Add(opcode1);
            }
            if (((opcode1 & 0xFC00) >> 10) == 0b0111)
            {
                return Adc(opcode1);
            }
            if (((opcode1 & 0xF000) >> 12) == 0b1110)
            {
                return Ldi(opcode1);
            }
            if (((opcode1 & 0xFC00) >> 10) == 0b100111)
            {
                return Mul(opcode1);
            }
            if (((opcode1 & 0xF800) >> 11) == 0b10111)
            {
                return Out(opcode1);
            }
            if (((opcode1 & 0xF800) >> 11) == 0b10110)
            {
                return In(opcode1);
            }
            if (((opcode1 & 0xFC00) >> 10) == 0b001011)
            {
                return Mov(opcode1);
            }
            if (((opcode1 & 0xFF00) >> 8) == 0b00000001)
            {
                return Movw(opcode1);
            }
            if (((opcode1 & 0xFE00) >> 9) == 0b1001001)
            {
                return Sts(opcode1, opcode2);
            }
            if (((opcode1 & 0xFE00) >> 9) == 0b1001000)
            {
                return Lds(opcode1, opcode2);
            }

            return "???";
        }

        private string Nop(ushort opcode)
        {
            return $"NOP";
        }

        private string Add(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"ADD R{d}, R{r}";
        }

        private string Adc(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"ADC R{d}, R{r}";
        }

        private string Ldi(ushort opcode)
        {
            int d = 16 + ((opcode >> 4) & 0x0F);
            int k = (opcode & 0x0F) | ((opcode >> 4) & 0xF0);

            return $"LDI R{d}, 0x{k:X2}";
        }

        private string Mul(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"MUL R{d}, R{r}";
        }

        private string Out(ushort opcode)
        {
            int A = (opcode & 0x0F) | ((opcode >> 5) & 0x30);
            int r = (opcode >> 4) & 0x1F;

            return $"OUT 0x{A:X2}, R{r}";
        }

        private string In(ushort opcode)
        {
            int A = (opcode & 0x0F) | ((opcode >> 5) & 0x30);
            int d = (opcode >> 4) & 0x1F;

            return $"IN R{d}, 0x{A:X2}";
        }

        private string Mov(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"MOV R{d}, R{r}";
        }

        private string Movw(ushort opcode)
        {
            int d = ((opcode & 0xF0) >> 4) * 2;
            int r = ((opcode & 0x0F)) * 2;

            return $"MOVW R{d}, R{r}";
        }

        private string Sts(ushort opcode1, ushort opcode2)
        {
            int r = (opcode1 & 0x1F0) >> 4;
            return $"STS 0x{opcode2:X4}, R{r}";
        }

        private string Lds(ushort opcode1, ushort opcode2)
        {
            int d = (opcode1 & 0x1F0) >> 4;
            return $"STS R{d}, 0x{opcode2:X4}";
        }
    }
}
