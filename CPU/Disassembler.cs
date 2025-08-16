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
            if (opcode1 == 0x9598)
            {
                return Break(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x0C00)
            {
                return Add(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x1C00)
            {
                return Adc(opcode1);
            }
            if ((opcode1 & 0xFF00) == 0x9600)
            {
                return Adiw(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x1800)
            {
                return Sub(opcode1);
            }
            if ((opcode1 & 0xF000) == 0x5000)
            {
                return Subi(opcode1);
            }
            if ((opcode1 & 0x0800) == 0x0800)
            {
                return Sbc(opcode1);
            }
            if ((opcode1 & 0xF000) == 0x4000)
            {
                return Sbci(opcode1);
            }
            if ((opcode1 & 0xFF00) == 0x9700)
            {
                return Sbiw(opcode1);
            }
            if ((opcode1 & 0xF000) == 0xE000)
            {
                return Ldi(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x9C00)
            {
                return Mul(opcode1);
            }
            if ((opcode1 & 0xF800) == 0xB800)
            {
                return Out(opcode1);
            }
            if ((opcode1 & 0xF800) == 0xB000)
            {
                return In(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x2C00)
            {
                return Mov(opcode1);
            }
            if ((opcode1 & 0xFF00) == 0x100)
            {
                return Movw(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x900C)
            {
                return Ld1(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x900D)
            {
                return Ld2(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x900E)
            {
                return Ld3(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x8008)
            {
                return Ld4(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9009)
            {
                return Ld5(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x900A)
            {
                return Ld6(opcode1);
            }
            if ((opcode1 & 0xD208) == 0x8008)
            {
                return Ld7(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x8000)
            {
                return Ld8(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9001)
            {
                return Ld9(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9002)
            {
                return Ld10(opcode1);
            }
            if ((opcode1 & 0xD208) == 0x8000 && (((opcode1 & 0x000F) != 0x0000) || ((opcode1 & 0xF000) != 0b1000)))
            {
                return Ld11(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9000)
            {
                return Lds(opcode1, opcode2);
            }
            if ((opcode1 & 0xFE0F) == 0x920C)
            {
                return St1(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x920D)
            {
                return St2(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x920E)
            {
                return St3(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x8208)
            {
                return St4(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9209)
            {
                return St5(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x920A)
            {
                return St6(opcode1);
            }
            if ((opcode1 & 0xD208) == 0x8208 && (((opcode1 & 0x000F) != 0x0000) || ((opcode1 & 0x0F00) != 0b0010)))
            {
                return St7(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x8200)
            {
                return St8(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9201)
            {
                return St9(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9202)
            {
                return St10(opcode1);
            }
            if ((opcode1 & 0xD208) == 0x8200 && (((opcode1 & 0x000F) != 0x0000) || ((opcode1 & 0xF000) != 0b1000)))
            {
                return St11(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9200)
            {
                return Sts(opcode1, opcode2);
            }
            if (opcode1 == 0x95C8)
            {
                return Lpm(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9004)
            {
                return Lpm1(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9005)
            {
                return Lpm2(opcode1);
            }
            if (opcode1 == 0x95D8)
            {
                return Elpm(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9006)
            {
                return Elpm1(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9007)
            {
                return Elpm2(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x920F)
            {
                return Push(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x900F)
            {
                return Pop(opcode1);
            }

            return "???";
        }

        private string Nop(ushort opcode)
        {
            return $"NOP";
        }

        private string Break(ushort opcode)
        {
            return $"BREAK";
        }

        private string Add(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"ADD    R{d}, R{r}";
        }

        private string Adc(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"ADC    R{d}, R{r}";
        }

        private string Adiw(ushort opcode)
        {
            int[] temp = new int[] { 24, 26, 28, 30 };
            int k = (opcode & 0x0F) | (opcode >> 2) & 0x30;
            int d = temp[(opcode >> 4) & 0x03];

            return $"ADIW   R{d}, {k}";
        }

        private string Sub(ushort opcode)
        {
            int r = (opcode & 0x0F) | (opcode >> 5) & 0x10;
            int d = ((opcode >> 4) & 0x0F) | (opcode >> 4) & 0x10;

            return $"SUB    R{d}, R{r}";
        }

        private string Subi(ushort opcode)
        {
            int d = ((opcode >> 4) & 0x0F) + 16;
            int k = (opcode & 0x0F) | (opcode >> 4) & 0xF0;

            return $"SUBI   R{d}, 0x{k:X2}";
        }

        private string Sbc(ushort opcode)
        {
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);
            int d = ((opcode >> 4) & 0x0F) | ((opcode >> 4) & 0x10);

            return $"SBC    R{d}, R{r}";
        }

        private string Sbci(ushort opcode)
        {
            int d = ((opcode >> 4) & 0x0F) + 16;
            int k = (opcode & 0x0F) | (opcode >> 4) & 0xF0;

            return $"SBCI   R{d}, 0x{k:X2}";
        }

        private string Sbiw(ushort opcode)
        {
            int[] temp = new int[] { 24, 26, 28, 30 };
            int d = temp[(opcode >> 4) & 0x03];
            ushort k = (ushort)((opcode & 0x0F) | (opcode >> 2) & 0x30);

            return $"SBIW   R{d}, 0x{k:X2}";
        }

        private string Ldi(ushort opcode)
        {
            int d = 16 + ((opcode >> 4) & 0x0F);
            int k = (opcode & 0x0F) | ((opcode >> 4) & 0xF0);

            return $"LDI    R{d}, 0x{k:X2}";
        }

        private string Mul(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"MUL    R{d}, R{r}";
        }

        private string Out(ushort opcode)
        {
            int A = (opcode & 0x0F) | ((opcode >> 5) & 0x30);
            int r = (opcode >> 4) & 0x1F;

            if (A == 0x3B)
            {
                return $"OUT    RAMPZ, R{r}";
            }
            return $"OUT    0x{A:X2}, R{r}";
        }

        private string In(ushort opcode)
        {
            int A = (opcode & 0x0F) | ((opcode >> 5) & 0x30);
            int d = (opcode >> 4) & 0x1F;

            return $"IN     R{d}, 0x{A:X2}";
        }

        private string Mov(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"MOV    R{d}, R{r}";
        }

        private string Movw(ushort opcode)
        {
            int d = ((opcode & 0xF0) >> 4) * 2;
            int r = ((opcode & 0x0F)) * 2;

            return $"MOVW   R{d}, R{r}";
        }

        private string Ld1(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"LD     R{d}, X";
        }

        private string Ld2(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"LD     R{d}, X+";
        }

        private string Ld3(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"LD     R{d}, -X";
        }

        private string Ld4(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"LD     R{d}, Y";
        }

        private string Ld5(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"LD     R{d}, Y+";
        }

        private string Ld6(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"LD     R{d}, -Y";
        }

        private string Ld7(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;
            int q = (opcode & 0x07) | ((opcode & 0xC00) >> 7) | ((opcode & 0x2000) >> 8);

            return $"LDD    R{d}, Y+{q}";
        }

        private string Ld8(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"LD     R{d}, Z";
        }

        private string Ld9(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"LD     R{d}, Z+";
        }

        private string Ld10(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"LD     R{d}, -Z";
        }

        private string Ld11(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;
            int q = (opcode & 0x07) | ((opcode & 0xC00) >> 7) | ((opcode & 0x2000) >> 8);

            return $"LDD    R{d}, Z+{q}";
        }

        private string Lds(ushort opcode1, ushort opcode2)
        {
            int d = (opcode1 & 0x1F0) >> 4;

            return $"LDS    R{d}, 0x{opcode2:X4}";
        }

        private string St1(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;

            return $"ST     X, R{r}";
        }

        private string St2(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;

            return $"ST     X+, R{r}";
        }

        private string St3(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;

            return $"ST     -X, R{r}";
        }

        private string St4(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;

            return $"ST     Y, R{r}";
        }

        private string St5(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;

            return $"ST     Y+, R{r}";
        }

        private string St6(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;

            return $"ST     -Y, R{r}";
        }

        private string St7(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;
            int q = (opcode & 0x07) | ((opcode & 0xC00) >> 7) | ((opcode & 0x2000) >> 8);

            return $"STD    Y+{q}, R{r}";
        }

        private string St8(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;

            return $"ST     Z, R{r}";
        }

        private string St9(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;

            return $"ST     Z+, R{r}";
        }

        private string St10(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;

            return $"ST     -Z, R{r}";
        }

        private string St11(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;
            int q = (opcode & 0x07) | ((opcode & 0xC00) >> 7) | ((opcode & 0x2000) >> 8);

            return $"STD    Z+{q}, R{r}";
        }

        private string Sts(ushort opcode1, ushort opcode2)
        {
            int r = (opcode1 & 0x1F0) >> 4;
            return $"STS    0x{opcode2:X4}, R{r}";
        }

        private string Lpm(ushort opcode)
        {
            return $"LPM";
        }

        private string Lpm1(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"LPM    R{d}, Z";
        }

        private string Lpm2(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"LPM    R{d}, Z+";
        }

        private string Elpm(ushort opcode)
        {
            return $"ELPM";
        }

        private string Elpm1(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"ELPM   R{d}, Z";
        }

        private string Elpm2(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"ELPM   R{d}, Z+";
        }

        private string Push(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;

            return $"PUSH   R{r}";
        }

        private string Pop(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            return $"POP    R{d}";
        }
    }
}
