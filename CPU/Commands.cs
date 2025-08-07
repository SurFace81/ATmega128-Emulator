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
            if (opcode == 0x0000)
            {
                //Nop();
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

            return 1;
        }

        public void Add(ushort opcode)
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
            bool R3  = ((R  & (1 << 3)) != 0);
            bool Rd7 = ((Rd & (1 << 7)) != 0);
            bool Rr7 = ((Rr & (1 << 7)) != 0);
            bool R7  = ((R  & (1 << 7)) != 0);

            cpuState.SREG.H = (Rd3 && Rr3) || (Rr3 && !R3) || (!R3 && Rd3);
            cpuState.SREG.V = (Rd7 && Rr7 && !R7) || (!Rd7 && !Rr7 && R7);
            cpuState.SREG.N = R7;
            cpuState.SREG.S = cpuState.SREG.N ^ cpuState.SREG.V;
            cpuState.SREG.Z = (R == 0);
            cpuState.SREG.C = (Rd7 && Rr7) || (Rr7 && !R7) || (!R7 && Rd7);
        }

        public void Adc(ushort opcode)
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

        public void Ldi(ushort opcode)
        {
            int d = 16 + ((opcode >> 4) & 0x0F);                  // 16 <= Rd <= 31
            int k = (opcode & 0x0F) | ((opcode >> 4) & 0xF0);

            cpuState.R[d] = (byte)k;
        }

        public void Mul(ushort opcode)
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

        public void Out(ushort opcode)
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
    }
}
