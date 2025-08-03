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
            byte R = (byte)(Rd + Rr);
            Cpu.R[d] = R;

            // Flags
            bool Rd3 = ((Rd & (1 << 3)) != 0);
            bool Rr3 = ((Rr & (1 << 3)) != 0);
            bool R3  = ((R  & (1 << 3)) != 0);
            bool Rd7 = ((Rd & (1 << 7)) != 0);
            bool Rr7 = ((Rr & (1 << 7)) != 0);
            bool R7  = ((R  & (1 << 7)) != 0);

            Cpu.SREG.H = (Rd3 && Rr3) || (Rr3 && !R3) || (!R3 && Rd3);
            Cpu.SREG.V = (Rd7 && Rr7 && !R7) || (!Rd7 && !Rr7 && R7);
            Cpu.SREG.N = R7;
            Cpu.SREG.S = Cpu.SREG.N ^ Cpu.SREG.V;
            Cpu.SREG.Z = (R == 0);
            Cpu.SREG.C = (Rd7 && Rr7) || (Rr7 && !R7) || (!R7 && Rd7);
        }

        public static void Adc(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            byte Rd = Cpu.R[d];
            byte Rr = Cpu.R[r];
            byte R = (byte)(Rd + Rr + Convert.ToByte(Cpu.SREG.C));
            Cpu.R[d] = R;

            // Flags
            bool Rd3 = ((Rd & (1 << 3)) != 0);
            bool Rr3 = ((Rr & (1 << 3)) != 0);
            bool R3 = ((R & (1 << 3)) != 0);
            bool Rd7 = ((Rd & (1 << 7)) != 0);
            bool Rr7 = ((Rr & (1 << 7)) != 0);
            bool R7 = ((R & (1 << 7)) != 0);

            Cpu.SREG.H = (Rd3 && Rr3) || (Rr3 && !R3) || (!R3 && Rd3);
            Cpu.SREG.V = (Rd7 && Rr7 && !R7) || (!Rd7 && !Rr7 && R7);
            Cpu.SREG.N = R7;
            Cpu.SREG.S = Cpu.SREG.N ^ Cpu.SREG.V;
            Cpu.SREG.Z = (R == 0);
            Cpu.SREG.C = (Rd7 && Rr7) || (Rr7 && !R7) || (!R7 && Rd7);
        }

        public static void Ldi(ushort opcode)
        {
            int d = 16 + ((opcode >> 4) & 0x0F);                  // 16 <= Rd <= 31
            int k = (opcode & 0x0F) | ((opcode >> 4) & 0xF0);

            Cpu.R[d] = (byte)k;
        }

        public static void Mul(ushort opcode)
        {
            int d = (opcode >> 4) & 0x1F;
            int r = (opcode & 0x0F) | ((opcode >> 5) & 0x10);

            byte Rd = Cpu.R[d];
            byte Rr = Cpu.R[r];
            ushort R = (ushort)(Rd * Rr);

            Cpu.R[0] = (byte)(R & 0xFF);
            Cpu.R[1] = (byte)((R >> 8) & 0xFF);

            // Flags
            Cpu.SREG.C = ((Cpu.R[1] & 0x80) != 0);
            Cpu.SREG.Z = (R == 0);
        }

        public static void Out(ushort opcode)
        {
            int A = (opcode & 0x0F) | ((opcode >> 9) & 0x03);
            int r = (opcode >> 4) & 0x1F;

            // TODO
        }
    }
}
