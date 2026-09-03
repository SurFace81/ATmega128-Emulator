using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATmegaSim.CPU
{
    public class Commands
    {
        private Cpu cpu;
        private CpuState cpuState;
        public Commands(Cpu cpu)
        {
            this.cpu = cpu;
            this.cpuState = cpu.state;
        }

        public int ExecInsruction(ushort opcode)
        {
            // Точные совпадения (1 слово, без второго слова) — первыми,
            // чтобы их не съели более общие маски.
            if (opcode == 0x0000)
            {
                //Nop(); //Break();
                return 1;
            }
            if (opcode == 0x9598)
            {
                // BREAK — как NOP
                return 1;
            }
            if (opcode == 0x9588)
            {
                Sleep(opcode);
                return 1;
            }
            if (opcode == 0x95A8)
            {
                Wdr(opcode);
                return 1;
            }
            if (opcode == 0x95E8)
            {
                Spm(opcode);
                return 1;
            }
            if (opcode == 0x9508)
            {
                Ret(opcode);
                return 4;
            }
            if (opcode == 0x9518)
            {
                Reti(opcode);
                return 4;
            }
            if (opcode == 0x9509)
            {
                Icall(opcode);
                return 3;
            }
            if (opcode == 0x9409)
            {
                Ijmp(opcode);
                return 2;
            }
            if (opcode == 0x95C8)
            {
                Lpm(opcode);
                return 3;
            }
            if (opcode == 0x95D8)
            {
                Elpm(opcode);
                return 3;
            }
            // 2-словные: JMP / CALL / LDS / STS.
            // Внутри Exec делаем PC+=2 чтобы пропустить второе слово,
            // затем ставим PC = target - 2 (внешний OnClock добавит +2).
            if ((opcode & 0xFE0E) == 0x940C)
            {
                cpuState.PC += 2;
                Jmp(opcode, cpu.GetOpcodeAt(cpuState.PC));
                return 3;
            }
            if ((opcode & 0xFE0E) == 0x940E)
            {
                cpuState.PC += 2;
                Call(opcode, cpu.GetOpcodeAt(cpuState.PC));
                return 4;
            }
            if ((opcode & 0xFE0F) == 0x9000)
            {
                cpuState.PC += 2;
                Lds(opcode, cpu.GetOpcodeAt(cpuState.PC));
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x9200)
            {
                cpuState.PC += 2; // Команда занимает 2 слова, учитываем одно из них
                Sts(opcode, cpu.GetOpcodeAt(cpuState.PC));
                return 2;
            }
            // Битовые операции с SREG (точные маски FF8F) — до общих.
            if ((opcode & 0xFF8F) == 0x9408)
            {
                Bset(opcode);
                return 1;
            }
            if ((opcode & 0xFF8F) == 0x9488)
            {
                Bclr(opcode);
                return 1;
            }
            // Работа с битами IO. Поле A занимает биты 7..3, b - биты 2..0;
            // маска должна исключать только эти изменяемые поля.
            if ((opcode & 0xFF00) == 0x9A00)
            {
                Sbi(opcode);
                return 2;
            }
            if ((opcode & 0xFF00) == 0x9800)
            {
                Cbi(opcode);
                return 2;
            }
            if ((opcode & 0xFF00) == 0x9900)
            {
                return Sbic(opcode);
            }
            if ((opcode & 0xFF00) == 0x9B00)
            {
                return Sbis(opcode);
            }
            // Умножения с фиксированными регистрами (FF88) — до общих.
            if ((opcode & 0xFF88) == 0x0300)
            {
                Mulsu(opcode);
                return 2;
            }
            if ((opcode & 0xFF88) == 0x0308)
            {
                Fmul(opcode);
                return 2;
            }
            if ((opcode & 0xFF88) == 0x0380)
            {
                Fmuls(opcode);
                return 2;
            }
            if ((opcode & 0xFF88) == 0x0388)
            {
                Fmulsu(opcode);
                return 2;
            }
            if ((opcode & 0xFF00) == 0x0200)
            {
                Muls(opcode);
                return 2;
            }
            if ((opcode & 0xFF00) == 0x9600)
            {
                Adiw(opcode);
                return 2;
            }
            if ((opcode & 0xFF00) == 0x9700)
            {
                Sbiw(opcode);
                return 2;
            }
            if ((opcode & 0xFF00) == 0x0100)
            {
                Movw(opcode);
                return 1;
            }
            if ((opcode & 0xFF0F) == 0xEF0F)
            {
                Ser(opcode);
                return 1;
            }
            // Однословные 1001 010... (маска FE0F) — точные.
            if ((opcode & 0xFE0F) == 0x9400)
            {
                Com(opcode);
                return 1;
            }
            if ((opcode & 0xFE0F) == 0x9401)
            {
                Neg(opcode);
                return 1;
            }
            if ((opcode & 0xFE0F) == 0x9402)
            {
                Swap(opcode);
                return 1;
            }
            if ((opcode & 0xFE0F) == 0x9403)
            {
                Inc(opcode);
                return 1;
            }
            if ((opcode & 0xFE0F) == 0x9405)
            {
                Asr(opcode);
                return 1;
            }
            if ((opcode & 0xFE0F) == 0x9406)
            {
                Lsr(opcode);
                return 1;
            }
            if ((opcode & 0xFE0F) == 0x9407)
            {
                Ror(opcode);
                return 1;
            }
            if ((opcode & 0xFE0F) == 0x940A)
            {
                Dec(opcode);
                return 1;
            }
            if ((opcode & 0xFE0F) == 0x9004)
            {
                Lpm1(opcode);
                return 3;
            }
            if ((opcode & 0xFE0F) == 0x9005)
            {
                Lpm2(opcode);
                return 3;
            }
            if ((opcode & 0xFE0F) == 0x9006)
            {
                Elpm1(opcode);
                return 3;
            }
            if ((opcode & 0xFE0F) == 0x9007)
            {
                Elpm2(opcode);
                return 3;
            }
            if ((opcode & 0xFE0F) == 0x920F)
            {
                Push(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x900F)
            {
                Pop(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x900C)
            {
                Ld1(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x900D)
            {
                Ld2(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x900E)
            {
                Ld3(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x8008)
            {
                Ld4(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x9009)
            {
                Ld5(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x900A)
            {
                Ld6(opcode);
                return 2;
            }
            if ((opcode & 0xD208) == 0x8008)
            {
                Ld7(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x8000)
            {
                Ld8(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x9001)
            {
                Ld9(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x9002)
            {
                Ld10(opcode);
                return 2;
            }
            if ((opcode & 0xD208) == 0x8000 && (((opcode & 0x000F) != 0x0000) || ((opcode & 0xF000) != 0b1000)))
            {
                Ld11(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x920C)
            {
                St1(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x920D)
            {
                St2(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x920E)
            {
                St3(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x8208)
            {
                St4(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x9209)
            {
                St5(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x920A)
            {
                St6(opcode);
                return 2;
            }
            if ((opcode & 0xD208) == 0x8208 && (((opcode & 0x000F) != 0x0000) || ((opcode & 0xF000) != 0b1000)))
            {
                St7(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x8200)
            {
                St8(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x9201)
            {
                St9(opcode);
                return 2;
            }
            if ((opcode & 0xFE0F) == 0x9202)
            {
                St10(opcode);
                return 2;
            }
            if ((opcode & 0xD208) == 0x8200 && (((opcode & 0x000F) != 0x0000) || ((opcode & 0xF000) != 0b1000)))
            {
                St11(opcode);
                return 2;
            }
            // Пропуск по биту регистра (FE08) — до общих BRBC (FC00==F400),
            // т.к. 1111 10../1111 11.. иначе съедаются маской F800.
            if ((opcode & 0xFE08) == 0xFC00)
            {
                return Sbrc(opcode);
            }
            if ((opcode & 0xFE08) == 0xFE00)
            {
                return Sbrs(opcode);
            }
            if ((opcode & 0xFE08) == 0xFA00)
            {
                Bst(opcode);
                return 1;
            }
            if ((opcode & 0xFE08) == 0xF800)
            {
                Bld(opcode);
                return 1;
            }
            // Условные переходы (FC00) — после SBRC/SBRS/BST/BLD.
            if ((opcode & 0xFC00) == 0xF000)
            {
                return Brbs(opcode);
            }
            if ((opcode & 0xFC00) == 0xF400)
            {
                return Brbc(opcode);
            }
            // Арифметика/ up to 5 бит (FC00) — строгие маски.
            // LSL == ADD Rd,Rd, ROL == ADC Rd,Rd: обрабатываем внутри.
            if ((opcode & 0xFC00) == 0x0C00)
            {
                int d = (opcode >> 4) & 0x1F;
                int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);
                if (d == r)
                    Lsl(opcode);
                else
                    Add(opcode);
                return 1;
            }
            if ((opcode & 0xFC00) == 0x1C00)
            {
                int d = (opcode >> 4) & 0x1F;
                int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);
                if (d == r)
                    Rol(opcode);
                else
                    Adc(opcode);
                return 1;
            }
            if ((opcode & 0xFC00) == 0x1800)
            {
                Sub(opcode);
                return 1;
            }
            if ((opcode & 0xFC00) == 0x0800)
            {
                Sbc(opcode);
                return 1;
            }
            if ((opcode & 0xFC00) == 0x1400)
            {
                Cp(opcode);
                return 1;
            }
            if ((opcode & 0xFC00) == 0x0400)
            {
                Cpc(opcode);
                return 1;
            }
            if ((opcode & 0xFC00) == 0x1000)
            {
                return Cpse(opcode);
            }
            if ((opcode & 0xFC00) == 0x2000)
            {
                And(opcode);
                return 1;
            }
            if ((opcode & 0xFC00) == 0x2800)
            {
                Or(opcode);
                return 1;
            }
            if ((opcode & 0xFC00) == 0x2400)
            {
                Eor(opcode);
                return 1;
            }
            if ((opcode & 0xFC00) == 0x2C00)
            {
                Mov(opcode);
                return 1;
            }
            if ((opcode & 0xFC00) == 0x9C00)
            {
                Mul(opcode);
                return 2;
            }
            // Немедленные 8-бит (F000).
            if ((opcode & 0xF000) == 0x5000)
            {
                Subi(opcode);
                return 1;
            }
            if ((opcode & 0xF000) == 0x4000)
            {
                Sbci(opcode);
                return 1;
            }
            if ((opcode & 0xF000) == 0x3000)
            {
                Cpi(opcode);
                return 1;
            }
            if ((opcode & 0xF000) == 0x7000)
            {
                Andi(opcode);
                return 1;
            }
            if ((opcode & 0xF000) == 0x6000)
            {
                Ori(opcode);
                return 1;
            }
            if ((opcode & 0xF000) == 0xE000)
            {
                Ldi(opcode);
                return 1;
            }
            if ((opcode & 0xF000) == 0xC000)
            {
                Rjmp(opcode);
                return 2;
            }
            if ((opcode & 0xF000) == 0xD000)
            {
                Rcall(opcode);
                return 3;
            }
            if ((opcode & 0xF800) == 0xB800)
            {
                Out(opcode);
                return 1;
            }
            if ((opcode & 0xF800) == 0xB000)
            {
                In(opcode);
                return 1;
            }

            return 1;
        }

        private void Add(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            byte Rd = cpuState.R[d];
            byte Rr = cpuState.R[r];
            byte R = (byte)(Rd + Rr);
            cpuState.R[d] = R;

            // Flags
            bool Rd3 = ((Rd & (1 << 3)) != 0);
            bool Rr3 = ((Rr & (1 << 3)) != 0);
            bool R3 = ((R & (1 << 3)) != 0);
            bool Rd7 = ((Rd & (1 << 7)) != 0);
            bool Rr7 = ((Rr & (1 << 7)) != 0);
            bool R7 = ((R & (1 << 7)) != 0);

            cpuState.SREG.H = (Rd3 && Rr3) || (Rr3 && !R3) || (!R3 && Rd3);
            cpuState.SREG.V = (Rd7 && Rr7 && !R7) || (!Rd7 && !Rr7 && R7);
            cpuState.SREG.N = R7;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (R == 0);
            cpuState.SREG.C = (Rd7 && Rr7) || (Rr7 && !R7) || (!R7 && Rd7);
        }

        private void Adc(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            byte Rd = cpuState.R[d];
            byte Rr = cpuState.R[r];
            // R уже включает входной Carry, поэтому битовые формулы
            // H/V/C через R корректно учитывают Carry.
            byte R = (byte)(Rd + Rr + Convert.ToByte(cpuState.SREG.C));
            cpuState.R[d] = R;

            // Flags
            bool Rd3 = ((Rd & (1 << 3)) != 0);
            bool Rr3 = ((Rr & (1 << 3)) != 0);
            bool R3 = ((R & (1 << 3)) != 0);
            bool Rd7 = ((Rd & (1 << 7)) != 0);
            bool Rr7 = ((Rr & (1 << 7)) != 0);
            bool R7 = ((R & (1 << 7)) != 0);

            cpuState.SREG.H = (Rd3 && Rr3) || (Rr3 && !R3) || (!R3 && Rd3);
            cpuState.SREG.V = (Rd7 && Rr7 && !R7) || (!Rd7 && !Rr7 && R7);
            cpuState.SREG.N = R7;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (R == 0);
            cpuState.SREG.C = (Rd7 && Rr7) || (Rr7 && !R7) || (!R7 && Rd7);
        }

        private void Adiw(ushort opcode)
        {
            int[] temp = new int[] { 24, 26, 28, 30 };
            ushort k = (ushort)(((opcode) & 0x0F) | (((opcode) >> 2) & 0x30));
            int d = temp[(opcode >> 4) & 0x03];

            ushort oldWord = (ushort)((cpuState.R[d + 1] << 8) | cpuState.R[d]);
            ushort word = (ushort)(oldWord + k);

            cpuState.R[d + 1] = (byte)(word >> 8);
            cpuState.R[d] = (byte)(word & 0xFF);

            // По мануалу (ADIW): Rdh7 — бит15 ДО операции, R15 — бит15 результата.
            // V = !Rdh7_old & R15, C = !R15 & Rdh7_old, N = R15, Z = (R==0).
            bool Rdh7 = (oldWord & (1 << 15)) != 0;
            bool R15 = (word & (1 << 15)) != 0;

            cpuState.SREG.Z = (word == 0);
            cpuState.SREG.N = R15;
            cpuState.SREG.V = !Rdh7 && R15;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.C = !R15 && Rdh7;
        }

        private void Sub(ushort opcode)
        {
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);
            int d = (opcode >> 4) & 0x1F;

            var Rd = cpuState.R[d];
            var Rr = cpuState.R[r];
            cpuState.R[d] -= cpuState.R[r];

            // Flags
            bool Rd3 = (Rd & (1 << 3)) != 0;
            bool Rr3 = (Rr & (1 << 3)) != 0;
            bool R3 = (cpuState.R[d] & (1 << 3)) != 0;
            bool Rd7 = (Rd & (1 << 7)) != 0;
            bool Rr7 = (Rr & (1 << 7)) != 0;
            bool R7 = (cpuState.R[d] & (1 << 7)) != 0;

            cpuState.SREG.H = (!Rd3 && Rr3) || (Rr3 && R3) || (R3 && !Rd3);
            cpuState.SREG.V = (Rd7 && !Rr7 && !R7) || (!Rd7 && Rr7 && R7);
            cpuState.SREG.N = R7;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (cpuState.R[d] == 0);
            cpuState.SREG.C = (!Rd7 && Rr7) || (Rr7 && R7) || (R7 && !Rd7);
        }

        private void Subi(ushort opcode)
        {
            // ВНИМАНИЕ: скобки обязательны, иначе & свяжет с (0x0F+16).
            int d = (((opcode >> 4) & 0x0F) + 16);
            byte k = (byte)(((opcode) & 0x0F) | (((opcode) >> 4) & 0xF0));

            int Rd = cpuState.R[d];
            cpuState.R[d] -= k;

            // Flags
            bool Rd3 = (Rd & (1 << 3)) != 0;
            bool K3 = (k & (1 << 3)) != 0;
            bool R3 = (cpuState.R[d] & (1 << 3)) != 0;
            bool Rd7 = (Rd & (1 << 7)) != 0;
            bool K7 = (k & (1 << 7)) != 0;
            bool R7 = (cpuState.R[d] & (1 << 7)) != 0;

            cpuState.SREG.H = (!Rd3 && K3) || (K3 && R3) || (R3 && !Rd3);
            cpuState.SREG.V = (Rd7 && !K7 && !R7) || (!Rd7 && K7 && R7);
            cpuState.SREG.N = R7;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (cpuState.R[d] == 0);
            cpuState.SREG.C = (!Rd7 && K7) || (K7 && R7) || (R7 && !Rd7);
        }

        private void Sbc(ushort opcode)
        {
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);
            int d = (opcode >> 4) & 0x1F;

            int Rd = cpuState.R[d];
            int Rr = cpuState.R[r];
            int carry = cpuState.SREG.C ? 1 : 0;
            bool oldZ = cpuState.SREG.Z;
            int subtrahend = Rr + carry;
            int result = Rd - subtrahend;
            byte R = (byte)result;
            cpuState.R[d] = R;

            cpuState.SREG.H = ((Rd & 0x0F) - ((Rr & 0x0F) + carry)) < 0;
            cpuState.SREG.V = ((Rd ^ Rr) & (Rd ^ R) & 0x80) != 0;
            cpuState.SREG.N = (R & 0x80) != 0;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = oldZ && R == 0;
            cpuState.SREG.C = result < 0;
        }

        private void Sbci(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x0F) + 16);
            int k = ((opcode & 0x0F) | (((opcode) >> 4) & 0xF0));

            int Rd = cpuState.R[d];
            cpuState.R[d] = (byte)(cpuState.R[d] - k - Convert.ToByte(cpuState.SREG.C));

            // Flags
            bool Rd3 = (Rd & (1 << 3)) != 0;
            bool K3 = (k & (1 << 3)) != 0;
            bool R3 = (cpuState.R[d] & (1 << 3)) != 0;
            bool Rd7 = (Rd & (1 << 7)) != 0;
            bool K7 = (k & (1 << 7)) != 0;
            bool R7 = (cpuState.R[d] & (1 << 7)) != 0;

            cpuState.SREG.H = (!Rd3 && K3) || (K3 && R3) || (R3 && !Rd3);
            cpuState.SREG.V = (Rd7 && !K7 && !R7) || (!Rd7 && K7 && R7);
            cpuState.SREG.N = R7;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (cpuState.R[d] == 0) && cpuState.SREG.Z;
            cpuState.SREG.C = (!Rd7 && K7) || (K7 && R7) || (R7 && !Rd7);
        }

        private void Sbiw(ushort opcode)
        {
            int[] temp = new int[] { 24, 26, 28, 30 };
            int d = temp[(opcode >> 4) & 0x03];
            ushort k = (ushort)(((opcode) & 0x0F) | (((opcode) >> 2) & 0x30));

            ushort oldWord = (ushort)((cpuState.R[d + 1] << 8) | cpuState.R[d]);
            ushort word = (ushort)(oldWord - k);

            cpuState.R[d + 1] = (byte)(word >> 8);
            cpuState.R[d] = (byte)(word & 0xFF);

            // По мануалу (SBIW): Rdh7 — бит15 ДО, R15 — бит15 результата.
            // V = !R15 & Rdh7_old, C = R15 & !Rdh7_old, N = R15, Z = (R==0).
            bool Rdh7 = (oldWord & (1 << 15)) != 0;
            bool R15 = (word & (1 << 15)) != 0;

            cpuState.SREG.Z = (word == 0);
            cpuState.SREG.N = R15;
            cpuState.SREG.V = !R15 && Rdh7;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.C = R15 && !Rdh7;
        }

        private void And(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            cpuState.R[d] &= cpuState.R[r];

            // Flags
            cpuState.SREG.V = false;
            cpuState.SREG.N = (cpuState.R[d] & (1 << 7)) != 0;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (cpuState.R[d] == 0);
        }

        private void Andi(ushort opcode)
        {
            int k = ((opcode & 0x0F) | (((opcode) >> 4) & 0xF0));
            int d = (((opcode >> 4) & 0x0F) + 16);

            cpuState.R[d] = (byte)(cpuState.R[d] & k);

            // Flags
            cpuState.SREG.V = false;
            cpuState.SREG.N = (cpuState.R[d] & (1 << 7)) != 0;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (cpuState.R[d] == 0);
        }

        private void Or(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            cpuState.R[d] |= cpuState.R[r];

            // Flags
            cpuState.SREG.V = false;
            cpuState.SREG.N = (cpuState.R[d] & (1 << 7)) != 0;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (cpuState.R[d] == 0);
        }

        private void Ori(ushort opcode)
        {
            int k = ((opcode & 0x0F) | (((opcode) >> 4) & 0xF0));
            int d = (((opcode >> 4) & 0x0F) + 16);

            cpuState.R[d] = (byte)(cpuState.R[d] | k);

            // Flags
            cpuState.SREG.V = false;
            cpuState.SREG.N = (cpuState.R[d] & (1 << 7)) != 0;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (cpuState.R[d] == 0);
        }

        private void Eor(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            cpuState.R[d] ^= cpuState.R[r];

            // Flags
            cpuState.SREG.V = false;
            cpuState.SREG.N = (cpuState.R[d] & (1 << 7)) != 0;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (cpuState.R[d] == 0);
        }

        private void Com(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;

            cpuState.R[d] = (byte)(0xFF - cpuState.R[d]);

            // Flags
            cpuState.SREG.V = false;
            cpuState.SREG.N = (cpuState.R[d] & 0x80) != 0;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (cpuState.R[d] == 0);
            cpuState.SREG.C = true;
        }

        private void Neg(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int Rd = cpuState.R[d];

            cpuState.R[d] = (byte)(0x00 - Rd);

            // Flags
            cpuState.SREG.H = ((cpuState.R[d] & 0x08) != 0) || ((Rd & 0x08) != 0);
            cpuState.SREG.V = (cpuState.R[d] == 0x80);
            cpuState.SREG.N = (cpuState.R[d] & 0x80) != 0;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (cpuState.R[d] == 0);
            cpuState.SREG.C = (cpuState.R[d] != 0);
        }

        private void Inc(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int Rd = cpuState.R[d];

            cpuState.R[d] += 1;

            // Flags
            cpuState.SREG.V = (Rd == 0x7F);
            cpuState.SREG.N = (cpuState.R[d] & 0x80) != 0;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (cpuState.R[d] == 0);
        }

        private void Dec(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int Rd = cpuState.R[d];

            cpuState.R[d] -= 1;

            // Flags
            cpuState.SREG.V = (Rd == 0x80);
            cpuState.SREG.N = (cpuState.R[d] & 0x80) != 0;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (cpuState.R[d] == 0);
        }

        private void Ser(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x0F) + 16);

            cpuState.R[d] = 0xFF;
        }

        private void Ldi(ushort opcode)
        {
            int d = 16 + ((opcode >> 4) & 0x0F);                  // 16 <= Rd <= 31
            int k = ((opcode & 0x0F) | (((opcode >> 4) & 0xF0)));

            cpuState.R[d] = (byte)k;
        }

        private void Mul(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            byte Rd = cpuState.R[d];
            byte Rr = cpuState.R[r];
            ushort R = (ushort)(Rd * Rr);

            cpuState.R[0] = (byte)(R & 0xFF);
            cpuState.R[1] = (byte)((R >> 8) & 0xFF);

            // Flags
            cpuState.SREG.C = ((cpuState.R[1] & 0x80) != 0);
            cpuState.SREG.Z = (R == 0);
        }

        private void Muls(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x0F) + 16);
            int r = ((opcode & 0x0F) + 16);

            byte Rd = cpuState.R[d];
            byte Rr = cpuState.R[r];
            short R = (short)((sbyte)Rd * (sbyte)Rr);

            cpuState.R[0] = (byte)(R & 0xFF);
            cpuState.R[1] = (byte)((R >> 8) & 0xFF);

            // Flags
            cpuState.SREG.C = ((cpuState.R[1] & 0x80) != 0);
            cpuState.SREG.Z = (R == 0);
        }

        private void Mulsu(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x07) + 16);
            int r = ((opcode & 0x07) + 16);

            byte Rd = cpuState.R[d];
            byte Rr = cpuState.R[r];
            short R = (short)((sbyte)Rd * (byte)Rr);

            cpuState.R[0] = (byte)(R & 0xFF);
            cpuState.R[1] = (byte)((R >> 8) & 0xFF);

            // Flags
            cpuState.SREG.C = ((cpuState.R[1] & 0x80) != 0);
            cpuState.SREG.Z = (R == 0);
        }

        private void Fmul(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x07) + 16);
            int r = ((opcode & 0x07) + 16);

            byte Rd = cpuState.R[d];
            byte Rr = cpuState.R[r];
            ushort temp = (ushort)(Rd * Rr);
            ushort R = (ushort)(temp << 1);

            cpuState.R[0] = (byte)(R & 0xFF);
            cpuState.R[1] = (byte)((R >> 8) & 0xFF);

            // Flags
            cpuState.SREG.C = ((temp & 0x8000) != 0);
            cpuState.SREG.Z = (R == 0);
        }

        private void Fmuls(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x07) + 16);
            int r = ((opcode & 0x07) + 16);

            byte Rd = cpuState.R[d];
            byte Rr = cpuState.R[r];
            short temp = (short)((sbyte)Rd * (sbyte)Rr);
            ushort R = (ushort)(temp << 1);

            cpuState.R[0] = (byte)(R & 0xFF);
            cpuState.R[1] = (byte)((R >> 8) & 0xFF);

            // Flags
            cpuState.SREG.C = ((temp & 0x8000) != 0);
            cpuState.SREG.Z = (R == 0);
        }

        private void Fmulsu(ushort opcode)
        {
            int d = (((opcode >> 4) & 0x07) + 16);
            int r = ((opcode & 0x07) + 16);

            byte Rd = cpuState.R[d];
            byte Rr = cpuState.R[r];
            short temp = (short)((sbyte)Rd * (byte)Rr);
            ushort R = (ushort)(temp << 1);

            cpuState.R[0] = (byte)(R & 0xFF);
            cpuState.R[1] = (byte)((R >> 8) & 0xFF);

            // Flags
            cpuState.SREG.C = ((temp & 0x8000) != 0);
            cpuState.SREG.Z = (R == 0);
        }

        private void Rjmp(ushort opcode)
        {
            // k — знаковый 12-бит (-2048..+2047) слов.
            int k = opcode & 0x0FFF;
            if ((k & 0x0800) != 0) k -= 0x1000;
            // Хотим после OnClock PC == old + 2 + k*2.
            // OnClock сделает +2 после Exec, значит внутри PC = old + k*2.
            cpuState.PC = (uint)((int)cpuState.PC + k * 2);
        }

        private void Ijmp(ushort opcode)
        {
            // Для mega128: PC(word) = Z, у нас PC байтовый => target = Z*2.
            // С учётом внешнего +2 ставим PC = target - 2.
            int target = cpuState.Z * 2;
            cpuState.PC = (uint)(target - 2);
        }

        private void Jmp(ushort opcode1, ushort opcode2)
        {
            // JMP k: 1001 010k kkkk 110k + kkkk kkkk kkkk kkkk (22-бит word-адрес).
            // Для mega128 (64K слов) достаточно младших 16 бит (второе слово),
            // старшие биты первого слова игнорируются.
            int k22 = (int)opcode2
                | (((int)opcode1 & 0x01) << 16)
                | ((((int)opcode1 & 0x01F0) << 13) & 0x3F0000);
            int k16 = k22 & 0xFFFF;
            int target = k16 * 2;
            // На входе PC уже += 2 (пропуск второго слова), внешний OnClock добавит ещё +2.
            cpuState.PC = (uint)(target - 2);
        }

        private void Out(ushort opcode)
        {
            int A = (opcode & 0x0F) | ((opcode >> 5) & 0x30);
            int r = (opcode >> 4) & 0x1F;

            cpu.WriteIO((byte)A, cpuState.R[r]);
        }

        public void In(ushort opcode)
        {
            int A = (opcode & 0x0F) | ((opcode >> 5) & 0x30);
            int d = (opcode >> 4) & 0x1F;

            cpuState.R[d] = cpu.ReadIO((byte)A);
        }

        private void Mov(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            cpuState.R[d] = cpuState.R[r];
        }

        private void Movw(ushort opcode)
        {
            int d = ((opcode & 0xF0) >> 4) * 2;
            int r = ((opcode & 0x0F)) * 2;

            cpuState.R[d] = cpuState.R[r];
            cpuState.R[d + 1] = cpuState.R[r + 1];
        }

        private void Ld1(ushort opcode) // LD Rd, X
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.R[d] = cpu.GetDataMem(cpuState.X);
        }

        private void Ld2(ushort opcode) // LD Rd, X+
        {
            int d = (opcode & 0x1F0) >> 4;

            ushort x = cpuState.X;
            cpuState.R[d] = cpu.GetDataMem(x);
            cpuState.X = (ushort)(x + 1);
        }

        private void Ld3(ushort opcode) // LD Rd, -X
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.X = (ushort)(cpuState.X - 1);
            cpuState.R[d] = cpu.GetDataMem(cpuState.X);
        }

        private void Ld4(ushort opcode) // LD Rd, Y
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.R[d] = cpu.GetDataMem(cpuState.Y);
        }

        private void Ld5(ushort opcode) // LD Rd, Y+
        {
            int d = (opcode & 0x1F0) >> 4;

            ushort y = cpuState.Y;
            cpuState.R[d] = cpu.GetDataMem(y);
            cpuState.Y = (ushort)(y + 1);
        }

        private void Ld6(ushort opcode) // LD Rd, -Y
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.Y = (ushort)(cpuState.Y - 1);
            cpuState.R[d] = cpu.GetDataMem(cpuState.Y);
        }

        private void Ld7(ushort opcode) // LD Rd, Y+q
        {
            int d = (opcode & 0x1F0) >> 4;
            int q = (opcode & 0x07) | ((opcode & 0xC00) >> 7) | ((opcode & 0x2000) >> 8);

            cpuState.R[d] = cpu.GetDataMem((ushort)(cpuState.Y + q));
        }

        private void Ld8(ushort opcode) // LD Rd, Z
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.R[d] = cpu.GetDataMem(cpuState.Z);
        }

        private void Ld9(ushort opcode) // LD Rd, Z+
        {
            int d = (opcode & 0x1F0) >> 4;

            ushort z = cpuState.Z;
            cpuState.R[d] = cpu.GetDataMem(z);
            cpuState.Z = (ushort)(z + 1);
        }

        private void Ld10(ushort opcode) // LD Rd, -Z
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.Z = (ushort)(cpuState.Z - 1);
            cpuState.R[d] = cpu.GetDataMem(cpuState.Z);
        }

        private void Ld11(ushort opcode) // LD Rd, Z+q
        {
            int d = (opcode & 0x1F0) >> 4;
            int q = (opcode & 0x07) | ((opcode & 0xC00) >> 7) | ((opcode & 0x2000) >> 8);

            cpuState.R[d] = cpu.GetDataMem((ushort)(cpuState.Z + q));
        }

        private void Lds(ushort opcode1, ushort opcode2)
        {
            int d = (opcode1 & 0x1F0) >> 4;
            ushort k = opcode2;

            cpuState.R[d] = cpu.GetDataMem(k);
        }

        private void St1(ushort opcode) // ST X, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            cpu.SetDataMem(cpuState.X, cpuState.R[r]);
        }

        private void St2(ushort opcode) // ST X+, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            ushort x = cpuState.X;
            cpu.SetDataMem(x, cpuState.R[r]);
            cpuState.X = (ushort)(x + 1);
        }

        private void St3(ushort opcode) // ST -X, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            cpuState.X = (ushort)(cpuState.X - 1);
            cpu.SetDataMem(cpuState.X, cpuState.R[r]);
        }

        private void St4(ushort opcode) // ST Y, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            cpu.SetDataMem(cpuState.Y, cpuState.R[r]);
        }

        private void St5(ushort opcode) // ST Y+, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            ushort y = cpuState.Y;
            cpu.SetDataMem(y, cpuState.R[r]);
            cpuState.Y = (ushort)(y + 1);
        }

        private void St6(ushort opcode) // ST -Y, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            cpuState.Y = (ushort)(cpuState.Y - 1);
            cpu.SetDataMem(cpuState.Y, cpuState.R[r]);
        }

        private void St7(ushort opcode) // STD Y+q, R
        {
            int r = (opcode & 0x1F0) >> 4;
            int q = (opcode & 0x07) | ((opcode & 0xC00) >> 7) | ((opcode & 0x2000) >> 8);

            cpu.SetDataMem((ushort)(cpuState.Y + q), cpuState.R[r]);
        }

        private void St8(ushort opcode) // ST Z, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            cpu.SetDataMem(cpuState.Z, cpuState.R[r]);
        }

        private void St9(ushort opcode) // ST Z+, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            ushort z = cpuState.Z;
            cpu.SetDataMem(z, cpuState.R[r]);
            cpuState.Z = (ushort)(z + 1);
        }

        private void St10(ushort opcode) // ST -Z, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            cpuState.Z = (ushort)(cpuState.Z - 1);
            cpu.SetDataMem(cpuState.Z, cpuState.R[r]);
        }

        private void St11(ushort opcode) // STD Z+q, R
        {
            int r = (opcode & 0x1F0) >> 4;
            int q = (opcode & 0x07) | ((opcode & 0xC00) >> 7) | ((opcode & 0x2000) >> 8);

            cpu.SetDataMem((ushort)(cpuState.Z + q), cpuState.R[r]);
        }

        private void Sts(ushort opcode1, ushort opcode2)
        {
            int r = (opcode1 & 0x1F0) >> 4;
            ushort k = opcode2;

            cpu.SetDataMem(k, cpuState.R[r]);
        }

        private void Lpm(ushort opcode)
        {
            cpuState.R[0] = cpuState.FLASH[Math.Min((int)cpuState.Z, 0x1FFFF)];
        }

        private void Lpm1(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.R[d] = cpuState.FLASH[Math.Min((int)cpuState.Z, 0x1FFFF)];
        }

        private void Lpm2(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            ushort z = cpuState.Z;
            cpuState.R[d] = cpuState.FLASH[Math.Min((int)z, 0x1FFFF)];
            cpuState.Z = (ushort)(z + 1);
        }

        private void Elpm(ushort opcode)
        {
            cpuState.R[0] = cpuState.FLASH[(int)Math.Min(cpuState.Z24, (uint)0x1FFFF)];
        }

        private void Elpm1(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.R[d] = cpuState.FLASH[(int)Math.Min(cpuState.Z24, (uint)0x1FFFF)];
        }

        private void Elpm2(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            uint addr = cpuState.Z24;
            cpuState.R[d] = cpuState.FLASH[(int)Math.Min(addr, (uint)0x1FFFF)];
            cpuState.Z24 = addr + 1;
            if (cpuState.Z24 > 0x1FFFF) cpuState.Z24 = 0;
        }

        private void Push(ushort opcode)
        {
            int r = (opcode & 0x1F0) >> 4;

            cpu.SetDataMem(cpuState.SP, cpuState.R[r]);
            cpuState.SP = (ushort)(cpuState.SP - 1);
        }

        private void Pop(ushort opcode)
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.SP = (ushort)(cpuState.SP + 1);
            cpuState.R[d] = cpu.GetDataMem(cpuState.SP);
        }

        // ---------- Сравнения и пропуски ----------

        private void Cp(ushort opcode)
        {
            // 0001 01rd dddd rrrr — флаги как SUB, без записи.
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);
            int d = (opcode >> 4) & 0x1F;

            byte Rd = cpuState.R[d];
            byte Rr = cpuState.R[r];
            byte R = (byte)(Rd - Rr);

            bool Rd3 = (Rd & (1 << 3)) != 0;
            bool Rr3 = (Rr & (1 << 3)) != 0;
            bool R3 = (R & (1 << 3)) != 0;
            bool Rd7 = (Rd & (1 << 7)) != 0;
            bool Rr7 = (Rr & (1 << 7)) != 0;
            bool R7 = (R & (1 << 7)) != 0;

            cpuState.SREG.H = (!Rd3 && Rr3) || (Rr3 && R3) || (R3 && !Rd3);
            cpuState.SREG.V = (Rd7 && !Rr7 && !R7) || (!Rd7 && Rr7 && R7);
            cpuState.SREG.N = R7;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (R == 0);
            cpuState.SREG.C = (!Rd7 && Rr7) || (Rr7 && R7) || (R7 && !Rd7);
        }

        private void Cpc(ushort opcode)
        {
            // 0000 01rd dddd rrrr — флаги как SBC, без записи. Z: только сброс.
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);
            int d = (opcode >> 4) & 0x1F;

            int Rd = cpuState.R[d];
            int Rr = cpuState.R[r];
            int carry = cpuState.SREG.C ? 1 : 0;
            bool oldZ = cpuState.SREG.Z;
            int subtrahend = Rr + carry;
            int result = Rd - subtrahend;
            byte R = (byte)result;

            cpuState.SREG.H = ((Rd & 0x0F) - ((Rr & 0x0F) + carry)) < 0;
            cpuState.SREG.V = ((Rd ^ Rr) & (Rd ^ R) & 0x80) != 0;
            cpuState.SREG.N = (R & 0x80) != 0;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = oldZ && R == 0;
            cpuState.SREG.C = result < 0;
        }

        private void Cpi(ushort opcode)
        {
            // 0011 KKKK dddd KKKK — флаги как SUBI, без записи.
            int d = (((opcode >> 4) & 0x0F) + 16);
            int k = ((opcode & 0x0F) | (((opcode) >> 4) & 0xF0));

            int Rd = cpuState.R[d];
            byte R = (byte)(Rd - k);

            bool Rd3 = (Rd & (1 << 3)) != 0;
            bool K3 = (k & (1 << 3)) != 0;
            bool R3 = (R & (1 << 3)) != 0;
            bool Rd7 = (Rd & (1 << 7)) != 0;
            bool K7 = (k & (1 << 7)) != 0;
            bool R7 = (R & (1 << 7)) != 0;

            cpuState.SREG.H = (!Rd3 && K3) || (K3 && R3) || (R3 && !Rd3);
            cpuState.SREG.V = (Rd7 && !K7 && !R7) || (!Rd7 && K7 && R7);
            cpuState.SREG.N = R7;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (R == 0);
            cpuState.SREG.C = (!Rd7 && K7) || (K7 && R7) || (R7 && !Rd7);
        }

        private int Cpse(ushort opcode)
        {
            // 0001 00rd dddd rrrr — флаги не трогает.
            // Если Rd==Rr — пропустить следующую инструкцию (1 или 2 слова).
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);
            int d = (opcode >> 4) & 0x1F;

            if (cpuState.R[d] != cpuState.R[r])
                return 1;

            int words = NextInstructionWords();
            cpuState.PC += (uint)(words * 2);
            return words == 1 ? 2 : 3;
        }

        // ---------- Ветвления ----------

        private bool GetSregBit(int s)
        {
            switch (s & 0x07)
            {
                case 0: return cpuState.SREG.C;
                case 1: return cpuState.SREG.Z;
                case 2: return cpuState.SREG.N;
                case 3: return cpuState.SREG.V;
                case 4: return cpuState.SREG.S;
                case 5: return cpuState.SREG.H;
                case 6: return cpuState.SREG.T;
                default: return cpuState.SREG.I;
            }
        }

        private int DoBranch(ushort opcode, bool branchIfSet)
        {
            int s = opcode & 0x07;
            int k = (opcode >> 3) & 0x7F;
            if ((k & 0x40) != 0) k -= 0x80; // знаковое расширение 7 бит (-64..+63 слов)
            bool bit = GetSregBit(s);
            bool take = branchIfSet ? bit : !bit;
            if (!take)
                return 1;
            // target(byte) = old + 2 + k*2; внутри PC=old, внешний +2 => PC = old + k*2.
            cpuState.PC = (uint)((int)cpuState.PC + k * 2);
            return 2;
        }

        private int Brbs(ushort opcode)
        {
            // 1111 00kk kkkk ksss — переход если SREG(s)==1.
            return DoBranch(opcode, true);
        }

        private int Brbc(ushort opcode)
        {
            // 1111 01kk kkkk ksss — переход если SREG(s)==0.
            return DoBranch(opcode, false);
        }

        private int NextInstructionWords()
        {
            // Длина следующей инструкции в словах: 2 для LDS/STS/JMP/CALL, иначе 1.
            // Вызывается когда PC указывает на текущую инструкцию (old).
            // Следующая начинается по адресу old+2 (байты).
            ushort next = cpu.GetOpcodeAt(cpuState.PC + 2);
            if ((next & 0xFE0F) == 0x9000) return 2; // LDS
            if ((next & 0xFE0F) == 0x9200) return 2; // STS
            if ((next & 0xFE0E) == 0x940C) return 2; // JMP
            if ((next & 0xFE0E) == 0x940E) return 2; // CALL
            return 1;
        }

        private int SkipNextInstruction()
        {
            int words = NextInstructionWords();
            cpuState.PC += (uint)(words * 2);
            return words == 1 ? 2 : 3;
        }

        private int Sbrc(ushort opcode)
        {
            // 1111 100r rrrr 0bbb — skip если бит сброшен.
            int r = (opcode >> 4) & 0x1F;
            int b = opcode & 0x07;
            if (((cpuState.R[r] >> b) & 0x01) != 0)
                return 1;
            return SkipNextInstruction();
        }

        private int Sbrs(ushort opcode)
        {
            // 1111 101r rrrr 0bbb — skip если бит установлен.
            int r = (opcode >> 4) & 0x1F;
            int b = opcode & 0x07;
            if (((cpuState.R[r] >> b) & 0x01) == 0)
                return 1;
            return SkipNextInstruction();
        }

        private int Sbic(ushort opcode)
        {
            // 1001 1001 AAAA Abbb — skip если бит IO сброшен. A 0..31.
            int a = (opcode >> 3) & 0x1F;
            int b = opcode & 0x07;
            if (((cpu.ReadIO((byte)a) >> b) & 0x01) != 0)
                return 1;
            return SkipNextInstruction();
        }

        private int Sbis(ushort opcode)
        {
            // 1001 1011 AAAA Abbb — skip если бит IO установлен.
            int a = (opcode >> 3) & 0x1F;
            int b = opcode & 0x07;
            if (((cpu.ReadIO((byte)a) >> b) & 0x01) == 0)
                return 1;
            return SkipNextInstruction();
        }

        private void Sbi(ushort opcode)
        {
            // 1001 1010 AAAA Abbb
            int a = (opcode >> 3) & 0x1F;
            int b = opcode & 0x07;
            byte v = cpu.ReadIO((byte)a);
            v |= (byte)(1 << b);
            cpu.WriteIO((byte)a, v);
        }

        private void Cbi(ushort opcode)
        {
            // 1001 1000 AAAA Abbb
            int a = (opcode >> 3) & 0x1F;
            int b = opcode & 0x07;
            byte v = cpu.ReadIO((byte)a);
            v &= (byte)(~(1 << b));
            cpu.WriteIO((byte)a, v);
        }

        // ---------- Сдвиги ----------

        private void Lsl(ushort opcode)
        {
            // LSL == ADD Rd,Rd. H=Rd3_old, C=Rd7_old, V=N^C.
            int d = (opcode >> 4) & 0x1F;
            byte Rd = cpuState.R[d];
            byte R = (byte)(Rd << 1);
            cpuState.R[d] = R;

            bool c = (Rd & 0x80) != 0;
            cpuState.SREG.H = (Rd & 0x08) != 0;
            cpuState.SREG.N = (R & 0x80) != 0;
            cpuState.SREG.V = cpuState.SREG.N ^ c;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (R == 0);
            cpuState.SREG.C = c;
        }

        private void Lsr(ushort opcode)
        {
            // 1001 010d dddd 0110. H не меняется.
            int d = (opcode >> 4) & 0x1F;
            byte Rd = cpuState.R[d];
            byte R = (byte)(Rd >> 1);
            cpuState.R[d] = R;

            bool c = (Rd & 0x01) != 0;
            cpuState.SREG.N = false;
            cpuState.SREG.V = cpuState.SREG.N ^ c;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (R == 0);
            cpuState.SREG.C = c;
        }

        private void Rol(ushort opcode)
        {
            // ROL == ADC Rd,Rd.
            int d = (opcode >> 4) & 0x1F;
            byte Rd = cpuState.R[d];
            bool cOld = cpuState.SREG.C;
            bool oldZ = cpuState.SREG.Z;
            int sum = Rd + Rd + (cOld ? 1 : 0);
            byte R = (byte)sum;
            cpuState.R[d] = R;

            cpuState.SREG.H = ((Rd & 0x0F) + (Rd & 0x0F) + (cOld ? 1 : 0)) > 0x0F;
            cpuState.SREG.N = (R & 0x80) != 0;
            cpuState.SREG.V = ((Rd ^ R) & 0x80) != 0;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = oldZ && R == 0;
            cpuState.SREG.C = sum > 0xFF;
        }

        private void Ror(ushort opcode)
        {
            // 1001 010d dddd 0111. R=(C_old<<7)|(Rd>>1). H не меняется.
            int d = (opcode >> 4) & 0x1F;
            byte Rd = cpuState.R[d];
            bool cOld = cpuState.SREG.C;
            byte R = (byte)(((cOld ? 0x80 : 0x00)) | (Rd >> 1));
            cpuState.R[d] = R;

            bool c = (Rd & 0x01) != 0;
            cpuState.SREG.N = (R & 0x80) != 0;
            cpuState.SREG.V = cpuState.SREG.N ^ c;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (R == 0);
            cpuState.SREG.C = c;
        }

        private void Asr(ushort opcode)
        {
            // 1001 010d dddd 0101. Бит7 сохраняется. H не меняется.
            int d = (opcode >> 4) & 0x1F;
            byte Rd = cpuState.R[d];
            byte R = (byte)((Rd & 0x80) | (Rd >> 1));
            cpuState.R[d] = R;

            bool c = (Rd & 0x01) != 0;
            cpuState.SREG.N = (R & 0x80) != 0;
            cpuState.SREG.V = cpuState.SREG.N ^ c;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (R == 0);
            cpuState.SREG.C = c;
        }

        private void Swap(ushort opcode)
        {
            // 1001 010d dddd 0010. Флаги не меняются.
            int d = (opcode >> 4) & 0x1F;
            byte Rd = cpuState.R[d];
            cpuState.R[d] = (byte)(((Rd & 0x0F) << 4) | ((Rd & 0xF0) >> 4));
        }

        // ---------- Биты SREG ----------

        private void Bst(ushort opcode)
        {
            // 1111 101d dddd 0bbb : T = Rd(b).
            int d = (opcode >> 4) & 0x1F;
            int b = opcode & 0x07;
            cpuState.SREG.T = ((cpuState.R[d] >> b) & 0x01) != 0;
        }

        private void Bld(ushort opcode)
        {
            // 1111 100d dddd 0bbb : Rd(b) = T.
            int d = (opcode >> 4) & 0x1F;
            int b = opcode & 0x07;
            if (cpuState.SREG.T)
                cpuState.R[d] |= (byte)(1 << b);
            else
                cpuState.R[d] &= (byte)(~(1 << b));
        }

        private void SetSregBit(int s, bool value)
        {
            switch (s & 0x07)
            {
                case 0: cpuState.SREG.C = value; break;
                case 1: cpuState.SREG.Z = value; break;
                case 2: cpuState.SREG.N = value; break;
                case 3: cpuState.SREG.V = value; break;
                case 4: cpuState.SREG.S = value; break;
                case 5: cpuState.SREG.H = value; break;
                case 6: cpuState.SREG.T = value; break;
                default: cpuState.SREG.I = value; break;
            }
        }

        private void Bset(ushort opcode)
        {
            // 1001 0100 0sss 1000 : SREG(s)=1.
            int s = (opcode >> 4) & 0x07;
            SetSregBit(s, true);
        }

        private void Bclr(ushort opcode)
        {
            // 1001 0100 1sss 1000 : SREG(s)=0.
            int s = (opcode >> 4) & 0x07;
            SetSregBit(s, false);
        }

        // ---------- Вызовы/возвраты ----------

        private void PushReturnAddress(uint retByteAddr)
        {
            // AVR сохраняет word-PC. У ATmega128 он 16-битный, даже при
            // 128 KiB (17-битной) байтовой адресации FLASH.
            ushort retWordAddr = (ushort)(retByteAddr >> 1);
            byte hi = (byte)(retWordAddr >> 8);
            byte lo = (byte)retWordAddr;
            // PUSH stores then decrements: low at SP, high at SP-1. POP increments
            // then reads, so RET obtains high first and then low to reconstruct PC.
            cpu.SetDataMem(cpuState.SP, lo);
            cpuState.SP = (ushort)(cpuState.SP - 1);
            cpu.SetDataMem(cpuState.SP, hi);
            cpuState.SP = (ushort)(cpuState.SP - 1);
        }

        private uint PopReturnAddress()
        {
            cpuState.SP = (ushort)(cpuState.SP + 1);
            byte hi = cpu.GetDataMem(cpuState.SP);
            cpuState.SP = (ushort)(cpuState.SP + 1);
            byte lo = cpu.GetDataMem(cpuState.SP);
            return (uint)(((hi << 8) | lo) * 2);
        }

        private void Rcall(ushort opcode)
        {
            // 1101 kkkk kkkk kkkk, k знаковый 12-бит слов.
            int k = opcode & 0x0FFF;
            if ((k & 0x0800) != 0) k -= 0x1000;
            uint ret = cpuState.PC + 2; // следующая инструкция (1 слово)
            PushReturnAddress(ret);
            int target = (int)cpuState.PC + 2 + k * 2;
            // Внешний OnClock добавит +2.
            cpuState.PC = (uint)(target - 2);
        }

        private void Icall(ushort opcode)
        {
            uint ret = cpuState.PC + 2;
            PushReturnAddress(ret);
            int target = cpuState.Z * 2;
            cpuState.PC = (uint)(target - 2);
        }

        private void Call(ushort opcode1, ushort opcode2)
        {
            // CALL k: 1001 010k kkkk 111k + kkkk... (22-бит word-адрес).
            // Для mega128 достаточно младших 16 бит.
            int k22 = (int)opcode2
                | (((int)opcode1 & 0x01) << 16)
                | ((((int)opcode1 & 0x01F0) << 13) & 0x3F0000);
            int k16 = k22 & 0xFFFF;
            int target = k16 * 2;
            // На входе PC уже += 2 (пропуск второго слова), возврат — old+4.
            uint ret = cpuState.PC + 2;
            PushReturnAddress(ret);
            cpuState.PC = (uint)(target - 2);
        }

        private void Ret(ushort opcode)
        {
            uint ret = PopReturnAddress();
            cpuState.PC = ret - 2;
        }

        private void Reti(ushort opcode)
        {
            uint ret = PopReturnAddress();
            cpuState.PC = ret - 2;
            cpuState.SREG.I = true;
        }

        private void Sleep(ushort opcode)
        {
            // Hardware sleep is not modeled; this documented stub has no behavior.
        }

        private void Wdr(ushort opcode)
        {
            // Hardware watchdog reset is not modeled; this documented stub has no behavior.
        }

        private void Spm(ushort opcode)
        {
            // Hardware self-programming is not modeled; this documented stub has no behavior.
        }
    }
}
