using ATmegaSim.ClockSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ATmegaSim.CPU
{
    public class Cpu : IClockSink
    {
        public const int FLASH_SIZE = 0x20000; // 0x20000 - total, 0x2000 high - bootloader
        private byte[] flashMemory = new byte[FLASH_SIZE];
        private int firmSize;

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


        public bool LoadFirm(List<byte> firmFile)
        {
            if (firmFile.Count > FLASH_SIZE) return false;

            byte[] temp = firmFile.ToArray();
            firmSize = temp.Length;
            Array.Copy(temp, 0, flashMemory, 0, temp.Length);

            Cpu.PC = 0;
            return true;
        }

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

        public void OnClock()
        {
            ExecInstruction((ushort)((flashMemory[Cpu.PC + 1] << 8) | flashMemory[Cpu.PC])); // little-endian byte order
            Cpu.PC += 2;
            if (Cpu.PC >= firmSize)
            {
                Cpu.PC = 0;
            }

            InvokeOnClockCompleted();
        }

        public event EventHandler<EventArgs> OnClockCompleted;
        public void InvokeOnClockCompleted()
        {
            EventHandler<EventArgs> handler = OnClockCompleted;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
