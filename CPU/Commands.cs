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
            if (opcode == 0x0000 || opcode == 0x9598)
            {
                //Nop(); //Break();
                return 1;
            }
            if (((opcode & 0xFC00) >> 10) == 0b0011)
            {
                Add(opcode);
                return 1;
            }
            if (((opcode & 0xFC00) >> 10) == 0b0111)
            {
                Adc(opcode);
                return 1;
            }
            if (((opcode & 0xF000) >> 12) == 0b1110)
            {
                Ldi(opcode);
                return 1;
            }
            if (((opcode & 0xFC00) >> 10) == 0b100111)
            {
                Mul(opcode);
                return 2;
            }
            if (((opcode & 0xF800) >> 11) == 0b10111)
            {
                Out(opcode);
                return 1;
            }
            if (((opcode & 0xF800) >> 11) == 0b10110)
            {
                In(opcode);
                return 1;
            }
            if (((opcode & 0xFC00) >> 10) == 0b001011)
            {
                Mov(opcode);
                return 1;
            }
            if (((opcode & 0xFF00) >> 8) == 0b00000001)
            {
                Movw(opcode);
                return 1;
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
            if (((opcode & 0xFE00) >> 9) == 0b1001000)
            {
                cpuState.PC += 2;
                Lds(opcode, cpu.GetOpcodeAt(cpuState.PC));
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
            if ((opcode & 0xFE0F) == 0x9200)
            {
                cpuState.PC += 2; // Команда занимает 2 слова, учитываем одно из них
                Sts(opcode, cpu.GetOpcodeAt(cpuState.PC));
                return 2;
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

        private void Ldi(ushort opcode)
        {
            int d = 16 + ((opcode >> 4) & 0x0F);                  // 16 <= Rd <= 31
            int k = (opcode & 0x0F) | ((opcode >> 4) & 0xF0);

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

            cpuState.R[d] = cpu.GetDataMem(cpuState.X++);
        }

        private void Ld3(ushort opcode) // LD Rd, -X
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.R[d] = cpu.GetDataMem(--cpuState.X);
        }

        private void Ld4(ushort opcode) // LD Rd, Y
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.R[d] = cpu.GetDataMem(cpuState.Y);
        }

        private void Ld5(ushort opcode) // LD Rd, Y+
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.R[d] = cpu.GetDataMem(cpuState.Y++);
        }

        private void Ld6(ushort opcode) // LD Rd, -Y
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.R[d] = cpu.GetDataMem(--cpuState.Y);
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

            cpuState.R[d] = cpu.GetDataMem(cpuState.Z++);
        }

        private void Ld10(ushort opcode) // LD Rd, -Z
        {
            int d = (opcode & 0x1F0) >> 4;

            cpuState.R[d] = cpu.GetDataMem(--cpuState.Z);
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

            cpu.SetDataMem(cpuState.X++, cpuState.R[r]);
        }

        private void St3(ushort opcode) // ST -X, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            cpu.SetDataMem(--cpuState.X, cpuState.R[r]);
        }

        private void St4(ushort opcode) // ST Y, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            cpu.SetDataMem(cpuState.Y, cpuState.R[r]);
        }

        private void St5(ushort opcode) // ST Y+, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            cpu.SetDataMem(cpuState.Y++, cpuState.R[r]);
        }

        private void St6(ushort opcode) // ST -Y, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            cpu.SetDataMem(--cpuState.Y, cpuState.R[r]);
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

            cpu.SetDataMem(cpuState.Z++, cpuState.R[r]);
        }

        private void St10(ushort opcode) // ST -Z, Rr
        {
            int r = (opcode & 0x1F0) >> 4;

            cpu.SetDataMem(--cpuState.Z, cpuState.R[r]);
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
    }
}
