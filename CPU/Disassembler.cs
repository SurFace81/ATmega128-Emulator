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
            if (opcode1 == 0x9588)
            {
                return Sleep(opcode1);
            }
            if (opcode1 == 0x95A8)
            {
                return Wdr(opcode1);
            }
            if (opcode1 == 0x95E8)
            {
                return Spm(opcode1);
            }
            if (opcode1 == 0x9508)
            {
                return Ret(opcode1);
            }
            if (opcode1 == 0x9518)
            {
                return Reti(opcode1);
            }
            if (opcode1 == 0x9509)
            {
                return Icall(opcode1);
            }
            if (opcode1 == 0x9409)
            {
                return Ijmp(opcode1);
            }
            if (opcode1 == 0x95C8)
            {
                return Lpm(opcode1);
            }
            if (opcode1 == 0x95D8)
            {
                return Elpm(opcode1);
            }
            if ((opcode1 & 0xFE0E) == 0x940C)
            {
                return Jmp(opcode1, opcode2);
            }
            if ((opcode1 & 0xFE0E) == 0x940E)
            {
                return Call(opcode1, opcode2);
            }
            if ((opcode1 & 0xFE0F) == 0x9000)
            {
                return Lds(opcode1, opcode2);
            }
            if ((opcode1 & 0xFE0F) == 0x9200)
            {
                return Sts(opcode1, opcode2);
            }
            if ((opcode1 & 0xFF8F) == 0x9408)
            {
                return Bset(opcode1);
            }
            if ((opcode1 & 0xFF8F) == 0x9488)
            {
                return Bclr(opcode1);
            }
            if ((opcode1 & 0xFF00) == 0x9A00)
            {
                return Sbi(opcode1);
            }
            if ((opcode1 & 0xFF00) == 0x9800)
            {
                return Cbi(opcode1);
            }
            if ((opcode1 & 0xFF00) == 0x9900)
            {
                return Sbic(opcode1);
            }
            if ((opcode1 & 0xFF00) == 0x9B00)
            {
                return Sbis(opcode1);
            }
            if ((opcode1 & 0xFF88) == 0x0300)
            {
                return Mulsu(opcode1);
            }
            if ((opcode1 & 0xFF88) == 0x0308)
            {
                return Fmul(opcode1);
            }
            if ((opcode1 & 0xFF88) == 0x0380)
            {
                return Fmuls(opcode1);
            }
            if ((opcode1 & 0xFF88) == 0x0388)
            {
                return Fmulsu(opcode1);
            }
            if ((opcode1 & 0xFF00) == 0x0200)
            {
                return Muls(opcode1);
            }
            if ((opcode1 & 0xFF00) == 0x9600)
            {
                return Adiw(opcode1);
            }
            if ((opcode1 & 0xFF00) == 0x9700)
            {
                return Sbiw(opcode1);
            }
            if ((opcode1 & 0xFF00) == 0x0100)
            {
                return Movw(opcode1);
            }
            if ((opcode1 & 0xFF0F) == 0xEF0F)
            {
                return Ser(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9400)
            {
                return Com(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9401)
            {
                return Neg(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9402)
            {
                return Swap(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9403)
            {
                return Inc(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9405)
            {
                return Asr(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9406)
            {
                return Lsr(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9407)
            {
                return Ror(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x940A)
            {
                return Dec(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9004)
            {
                return Lpm1(opcode1);
            }
            if ((opcode1 & 0xFE0F) == 0x9005)
            {
                return Lpm2(opcode1);
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
            if ((opcode1 & 0xD208) == 0x8208 && (((opcode1 & 0x000F) != 0x0000) || ((opcode1 & 0xF000) != 0b1000)))
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
            // SBRC/SBRS/BST/BLD (FE08) — до общих BRBC (FC00==F400).
            if ((opcode1 & 0xFE08) == 0xFC00)
            {
                return Sbrc(opcode1);
            }
            if ((opcode1 & 0xFE08) == 0xFE00)
            {
                return Sbrs(opcode1);
            }
            if ((opcode1 & 0xFE08) == 0xFA00)
            {
                return Bst(opcode1);
            }
            if ((opcode1 & 0xFE08) == 0xF800)
            {
                return Bld(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0xF000)
            {
                return Brbs(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0xF400)
            {
                return Brbc(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x0C00)
            {
                int d = (opcode1 >> 4) & 0x1F;
                int r = (opcode1 & 0x0F) | ((opcode1 >> 5) & 0x10);
                if (d == r)
                    return Lsl(opcode1);
                return Add(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x1C00)
            {
                int d = (opcode1 >> 4) & 0x1F;
                int r = (opcode1 & 0x0F) | ((opcode1 >> 5) & 0x10);
                if (d == r)
                    return Rol(opcode1);
                return Adc(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x1800)
            {
                return Sub(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x0800)
            {
                return Sbc(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x1400)
            {
                return Cp(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x0400)
            {
                return Cpc(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x1000)
            {
                return Cpse(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x2000)
            {
                int d = (opcode1 >> 4) & 0x1F;
                int r = (opcode1 & 0x0F) | ((opcode1 >> 5) & 0x10);
                if (d == r)
                    return Tst(opcode1);
                return And(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x2800)
            {
                return Or(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x2400)
            {
                int d = (opcode1 >> 4) & 0x1F;
                int r = (opcode1 & 0x0F) | ((opcode1 >> 5) & 0x10);
                if (d == r)
                    return Clr(opcode1);
                return Eor(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x2C00)
            {
                return Mov(opcode1);
            }
            if ((opcode1 & 0xFC00) == 0x9C00)
            {
                return Mul(opcode1);
            }
            if ((opcode1 & 0xF000) == 0x5000)
            {
                return Subi(opcode1);
            }
            if ((opcode1 & 0xF000) == 0x4000)
            {
                return Sbci(opcode1);
            }
            if ((opcode1 & 0xF000) == 0x3000)
            {
                return Cpi(opcode1);
            }
            if ((opcode1 & 0xF000) == 0x7000)
            {
                return Andi(opcode1);
            }
            if ((opcode1 & 0xF000) == 0x6000)
            {
                return Ori(opcode1);
            }
            if ((opcode1 & 0xF000) == 0xE000)
            {
                return Ldi(opcode1);
            }
            if ((opcode1 & 0xF000) == 0xC000)
            {
                return Rjmp(opcode1);
            }
            if ((opcode1 & 0xF000) == 0xD000)
            {
                return Rcall(opcode1);
            }
            if ((opcode1 & 0xF800) == 0xB800)
            {
                return Out(opcode1);
            }
            if ((opcode1 & 0xF800) == 0xB000)
            {
                return In(opcode1);
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

        private string Sleep(ushort opcode)
        {
            return $"SLEEP";
        }

        private string Wdr(ushort opcode)
        {
            return $"WDR";
        }

        private string Spm(ushort opcode)
        {
            return $"SPM";
        }

        private string Ret(ushort opcode)
        {
            return $"RET";
        }

        private string Reti(ushort opcode)
        {
            return $"RETI";
        }

        private string Icall(ushort opcode)
        {
            return $"ICALL";
        }

        private string Add(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            return $"ADD    R{d}, R{r}";
        }

        private string Lsl(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            return $"LSL    R{d}";
        }

        private string Rol(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            return $"ROL    R{d}";
        }

        private string Tst(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            return $"TST    R{d}";
        }

        private string Clr(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            return $"CLR    R{d}";
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
            int k = ((opcode & 0x0F) | (((opcode) >> 2) & 0x30));
            int d = temp[(opcode >> 4) & 0x03];

            return $"ADIW   R{d}, {k}";
        }

        private string Sub(ushort opcode)
        {
            int r = ((opcode & 0x0F) | (((opcode) >> 5) & 0x10));
            int d = (opcode >> 4) & 0x1F;

            return $"SUB    R{d}, R{r}";
        }

        private string Subi(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x0F) + 16);
            int k = ((opcode & 0x0F) | (((opcode) >> 4) & 0xF0));

            return $"SUBI   R{d}, 0x{k:X2}";
        }

        private string Sbc(ushort opcode)
        {
            int r = ((opcode & 0x0F) | (((opcode) >> 5) & 0x10));
            int d = (opcode >> 4) & 0x1F;

            return $"SBC    R{d}, R{r}";
        }

        private string Sbci(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x0F) + 16);
            int k = ((opcode & 0x0F) | (((opcode) >> 4) & 0xF0));

            return $"SBCI   R{d}, 0x{k:X2}";
        }

        private string Cp(ushort opcode)
        {
            int r = ((opcode & 0x0F) | (((opcode) >> 5) & 0x10));
            int d = (opcode >> 4) & 0x1F;

            return $"CP     R{d}, R{r}";
        }

        private string Cpc(ushort opcode)
        {
            int r = ((opcode & 0x0F) | (((opcode) >> 5) & 0x10));
            int d = (opcode >> 4) & 0x1F;

            return $"CPC    R{d}, R{r}";
        }

        private string Cpi(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x0F) + 16);
            int k = ((opcode & 0x0F) | (((opcode) >> 4) & 0xF0));

            return $"CPI    R{d}, 0x{k:X2}";
        }

        private string Cpse(ushort opcode)
        {
            int r = ((opcode & 0x0F) | (((opcode) >> 5) & 0x10));
            int d = (opcode >> 4) & 0x1F;

            return $"CPSE   R{d}, R{r}";
        }

        private string Brbs(ushort opcode)
        {
            return Branch(opcode, true);
        }

        private string Brbc(ushort opcode)
        {
            return Branch(opcode, false);
        }

        private string Branch(ushort opcode, bool branchIfSet)
        {
            // Красивые алиасы по таблице p.17: s + set/clear => мнемоника.
            int s = opcode & 0x07;
            int k = (opcode >> 3) & 0x7F;
            if ((k & 0x40) != 0) k -= 0x80;
            string name;
            if (branchIfSet)
            {
                switch (s)
                {
                    case 0: name = "BRCS"; break;
                    case 1: name = "BREQ"; break;
                    case 2: name = "BRMI"; break;
                    case 3: name = "BRVS"; break;
                    case 4: name = "BRLT"; break;
                    case 5: name = "BRHS"; break;
                    case 6: name = "BRTS"; break;
                    default: name = "BRIE"; break;
                }
            }
            else
            {
                switch (s)
                {
                    case 0: name = "BRCC"; break;
                    case 1: name = "BRNE"; break;
                    case 2: name = "BRPL"; break;
                    case 3: name = "BRVC"; break;
                    case 4: name = "BRGE"; break;
                    case 5: name = "BRHC"; break;
                    case 6: name = "BRTC"; break;
                    default: name = "BRID"; break;
                }
            }
            return $"{name}   {k}";
        }

        private string Sbrc(ushort opcode)
        {
            int r = (opcode >> 4) & 0x1F;
            int b = opcode & 0x07;

            return $"SBRC   R{r}, {b}";
        }

        private string Sbrs(ushort opcode)
        {
            int r = (opcode >> 4) & 0x1F;
            int b = opcode & 0x07;

            return $"SBRS   R{r}, {b}";
        }

        private string Sbic(ushort opcode)
        {
            int a = (opcode >> 3) & 0x1F;
            int b = opcode & 0x07;

            return $"SBIC   0x{a:X2}, {b}";
        }

        private string Sbis(ushort opcode)
        {
            int a = (opcode >> 3) & 0x1F;
            int b = opcode & 0x07;

            return $"SBIS   0x{a:X2}, {b}";
        }

        private string Sbi(ushort opcode)
        {
            int a = (opcode >> 3) & 0x1F;
            int b = opcode & 0x07;

            return $"SBI    0x{a:X2}, {b}";
        }

        private string Cbi(ushort opcode)
        {
            int a = (opcode >> 3) & 0x1F;
            int b = opcode & 0x07;

            return $"CBI    0x{a:X2}, {b}";
        }

        private string Lsr(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            return $"LSR    R{d}";
        }

        private string Ror(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            return $"ROR    R{d}";
        }

        private string Asr(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            return $"ASR    R{d}";
        }

        private string Swap(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            return $"SWAP   R{d}";
        }

        private string Bst(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int b = opcode & 0x07;

            return $"BST    R{d}, {b}";
        }

        private string Bld(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int b = opcode & 0x07;

            return $"BLD    R{d}, {b}";
        }

        private string Bset(ushort opcode)
        {
            int s = (opcode >> 4) & 0x07;
            // Красивые алиасы SEC/SEZ/... вместо generic BSET.
            switch (s)
            {
                case 0: return $"SEC";
                case 1: return $"SEZ";
                case 2: return $"SEN";
                case 3: return $"SEV";
                case 4: return $"SES";
                case 5: return $"SEH";
                case 6: return $"SET";
                default: return $"SEI";
            }
        }

        private string Bclr(ushort opcode)
        {
            int s = (opcode >> 4) & 0x07;
            switch (s)
            {
                case 0: return $"CLC";
                case 1: return $"CLZ";
                case 2: return $"CLN";
                case 3: return $"CLV";
                case 4: return $"CLS";
                case 5: return $"CLH";
                case 6: return $"CLT";
                default: return $"CLI";
            }
        }

        private string Rcall(ushort opcode)
        {
            int k = opcode & 0x0FFF;
            if ((k & 0x0800) != 0) k -= 0x1000;

            return $"RCALL  {k}";
        }

        private string Sbiw(ushort opcode)
        {
            int[] temp = new int[] { 24, 26, 28, 30 };
            int d = temp[(opcode >> 4) & 0x03];
            int k = ((opcode & 0x0F) | (((opcode) >> 2) & 0x30));

            return $"SBIW   R{d}, 0x{k:X2}";
        }

        private string And(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = ((opcode & 0x0F) | (((opcode) >> 5) & 0x10));

            return $"AND    R{d}, R{r}";
        }

        private string Andi(ushort opcode)
        {
            int k = ((opcode & 0x0F) | (((opcode) >> 4) & 0xF0));
            int d = (((opcode >> 4) & 0x0F) + 16);

            return $"ANDI   R{d}, 0x{k:X2}";
        }

        private string Or(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = ((opcode & 0x0F) | (((opcode) >> 5) & 0x10));

            return $"OR     R{d}, R{r}";
        }

        private string Ori(ushort opcode)
        {
            int k = ((opcode & 0x0F) | (((opcode) >> 4) & 0xF0));
            int d = (((opcode >> 4) & 0x0F) + 16);

            return $"ORI    R{d}, 0x{k:X2}";
        }

        private string Eor(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = ((opcode & 0x0F) | (((opcode) >> 5) & 0x10));

            return $"EOR    R{d}, R{r}";
        }

        private string Com(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            return $"COM    R{d}";
        }

        private string Neg(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            return $"NEG    R{d}";
        }

        private string Inc(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            return $"INC    R{d}";
        }

        private string Dec(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            return $"DEC    R{d}";
        }

        private string Ser(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x0F) + 16);

            return $"SER    R{d}";
        }

        private string Ldi(ushort opcode)
        {
            int d = 16 + ((opcode >> 4) & 0x0F);
            int k = ((opcode & 0x0F) | (((opcode >> 4) & 0xF0)));

            return $"LDI    R{d}, 0x{k:X2}";
        }

        private string Mul(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = ((opcode & 0x0F) | (((opcode) >> 5) & 0x10));

            return $"MUL    R{d}, R{r}";
        }

        private string Muls(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x0F) + 16);
            int r = ((opcode & 0x0F) + 16);

            return $"MULS   R{d}, R{r}";
        }

        private string Mulsu(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x07) + 16);
            int r = ((opcode & 0x07) + 16);

            return $"MULSU  R{d}, R{r}";
        }

        private string Fmul(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x07) + 16);
            int r = ((opcode & 0x07) + 16);

            return $"FMUL   R{d}, R{r}";
        }

        private string Fmuls(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x07) + 16);
            int r = ((opcode & 0x07) + 16);

            return $"FMULS  R{d}, R{r}";
        }

        private string Fmulsu(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x07) + 16);
            int r = ((opcode & 0x07) + 16);

            return $"FMULSU R{d}, R{r}";
        }

        private string Rjmp(ushort opcode)
        {
            int k = opcode & 0x0FFF;
            if ((k & 0x0800) != 0) k -= 0x1000;

            return $"RJMP   {k}";
        }

        private string Ijmp(ushort opcode)
        {
            return $"IJMP";
        }

        private string Jmp(ushort opcode1, ushort opcode2)
        {
            // 22-бит word-адрес, для mega128 — младшие 16 бит.
            int k22 = (int)opcode2
                | (((int)opcode1 & 0x01) << 16)
                | ((((int)opcode1 & 0x01F0) << 13) & 0x3F0000);
            int k16 = k22 & 0xFFFF;

            return $"JMP    0x{k16:X}";
        }

        private string Call(ushort opcode1, ushort opcode2)
        {
            int k22 = (int)opcode2
                | (((int)opcode1 & 0x01) << 16)
                | ((((int)opcode1 & 0x01F0) << 13) & 0x3F0000);
            int k16 = k22 & 0xFFFF;

            return $"CALL   0x{k16:X}";
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
