using ATmegaSim.ClockSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATmegaSim.CPU
{
    public class Cpu : IClockSink
    {
        public const int FLASH_SIZE = 0x20000; // 0x20000 - total, 0x2000 high - bootloader
        private byte[] flashMemory = new byte[FLASH_SIZE];
        private int firmSize;

        public Cpu()
        {
            InitializeIOHandlers();
        }

        public struct SREG
        {
            public static bool C; // Carry flag
            public static bool Z; // Zero result in an arithmetic or logic operation
            public static bool N; // Negative result in an arithmetic or logic operation
            public static bool V; // Two's Complement Overflow Flag V supports two's complement arithmetic (?)
            public static bool S; // Sgin bit
            public static bool H; // Half carry in some arithmetic operations
            public static bool T; // Bit Copy Storage
            public static bool I; // Global Interrupt Enable
        }
        public static byte[] R = new byte[32];       // Base registers R0 - R31
        public static byte[] IORegs = new byte[64];  // I/O registers
        // TODO: Доступ к ним только через LDS/STS и т.д.
        //private static IOPort PORTF = new IOPort();
        //private static IOPort PORTG = new IOPort();
        public static IOPort[] IOPorts = new IOPort[5] { new IOPort(), new IOPort(), new IOPort(), new IOPort(), new IOPort() };
        private static readonly Dictionary<byte, Action<byte>> IOWriteHandlers = new Dictionary<byte, Action<byte>>();
        private static readonly Dictionary<byte, Func<byte>> IOReadHandlers = new Dictionary<byte, Func<byte>>();
        public static ushort PC { get; set; }   // Program Counter
        public static uint CYCLES { get; set; }

        public static ushort X
        {
            get => (ushort)((R[27] << 8) | R[26]);
            set
            {
                R[27] = (byte)(value >> 8);
                R[26] = (byte)(value & 0xFF);
            }
        }

        public static ushort Y
        {
            get => (ushort)((R[29] << 8) | R[28]);
            set
            {
                R[29] = (byte)(value >> 8);
                R[28] = (byte)(value & 0xFF);
            }
        }

        public static ushort Z
        {
            get => (ushort)((R[31] << 8) | R[30]);
            set
            {
                R[31] = (byte)(value >> 8);
                R[30] = (byte)(value & 0xFF);
            }
        }

        public bool LoadFirm(List<byte> firmFile)
        {
            if (firmFile.Count > FLASH_SIZE) return false;

            byte[] temp = firmFile.ToArray();
            firmSize = temp.Length;
            Array.Copy(temp, 0, flashMemory, 0, temp.Length);

            Cpu.PC = 0;
            return true;
        }

        public void Step()
        {
            OnClock();
        }

        public void Reset()
        {
            Cpu.PC = 0;
            Cpu.CYCLES = 0;
            Cpu.SREG.C = false;
            Cpu.SREG.Z = false;
            Cpu.SREG.N = false;
            Cpu.SREG.V = false;
            Cpu.SREG.S = false;
            Cpu.SREG.H = false;
            Cpu.SREG.T = false;
            Cpu.SREG.I = false;
            Cpu.R = new byte[32];
            Cpu.IORegs = new byte[64];

            InvokeOnClockCompleted();
        }

        int cyclesToWait = 0;
        private void ExecInstruction(ushort opcode)
        {
            if (opcode == 0x0000)
            {
                //Commands.Nop();
                cyclesToWait = 1;
            }
            if (((opcode & 0xFC00) >> 10) == 0b0011)
            {
                Commands.Add(opcode);
                cyclesToWait = 1;
            }
            if (((opcode & 0xFC00) >> 10) == 0b0111)
            {
                Commands.Adc(opcode);
                cyclesToWait = 1;
            }
            if (((opcode & 0xF000) >> 12) == 0b1110)
            {
                Commands.Ldi(opcode);
                cyclesToWait = 1;
            }
            if (((opcode & 0xFC00) >> 10) == 0b100111)
            {
                Commands.Mul(opcode);
                cyclesToWait = 2;
            }
            if (((opcode & 0xF800) >> 11) == 0b10111)
            {
                Commands.Out(opcode);
                cyclesToWait = 1;
            }
            if (((opcode & 0xF800) >> 11) == 0b10110)
            {
                Commands.In(opcode);
                cyclesToWait = 1;
            }
        }

        private static void InitializeIOHandlers()
        {
            // PORTA (0x1B - PORT, 0x1A - DDR, 0x19 - PIN)
            IOWriteHandlers[0x1B] = value => IOPorts[0].WritePort(value);
            IOWriteHandlers[0x1A] = value => IOPorts[0].WriteDDR(value);
            IOReadHandlers[0x1B] = () => IOPorts[0].PORT;
            IOReadHandlers[0x1A] = () => IOPorts[0].DDR;
            IOReadHandlers[0x19] = () => IOPorts[0].ReadPin();

            // PORTB (0x18 - PORT, 0x17 - DDR, 0x16 - PIN)
            IOWriteHandlers[0x18] = value => IOPorts[1].WritePort(value);
            IOWriteHandlers[0x17] = value => IOPorts[1].WriteDDR(value);
            IOReadHandlers[0x18] = () => IOPorts[1].PORT;
            IOReadHandlers[0x17] = () => IOPorts[1].DDR;
            IOReadHandlers[0x16] = () => IOPorts[1].ReadPin();

            // PORTC (0x15 - PORT, 0x14 - DDR, 0x13 - PIN)
            IOWriteHandlers[0x15] = value => IOPorts[2].WritePort(value);
            IOWriteHandlers[0x14] = value => IOPorts[2].WriteDDR(value);
            IOReadHandlers[0x15] = () => IOPorts[2].PORT;
            IOReadHandlers[0x14] = () => IOPorts[2].DDR;
            IOReadHandlers[0x13] = () => IOPorts[2].ReadPin();

            // PORTD (0x12 - PORT, 0x11 - DDR, 0x10 - PIN)
            IOWriteHandlers[0x12] = value => IOPorts[3].WritePort(value);
            IOWriteHandlers[0x11] = value => IOPorts[3].WriteDDR(value);
            IOReadHandlers[0x12] = () => IOPorts[3].PORT;
            IOReadHandlers[0x11] = () => IOPorts[3].DDR;
            IOReadHandlers[0x10] = () => IOPorts[3].ReadPin();

            // PORTE (0x03 - PORT, 0x02 - DDR, 0x01 - PIN)
            IOWriteHandlers[0x03] = value => IOPorts[4].WritePort(value);
            IOWriteHandlers[0x02] = value => IOPorts[4].WriteDDR(value);
            IOReadHandlers[0x03] = () => IOPorts[4].PORT;
            IOReadHandlers[0x02] = () => IOPorts[4].DDR;
            IOReadHandlers[0x01] = () => IOPorts[4].ReadPin();
        }

        public static void WriteIO(byte address, byte value)
        {
            IORegs[address] = value;

            if (IOWriteHandlers.ContainsKey(address))
            {
                IOWriteHandlers[address](value);
            }
        }

        public static byte ReadIO(byte address)
        {
            if (IOReadHandlers.ContainsKey(address))
            {
                byte value = IOReadHandlers[address]();
                IORegs[address] = value;
                return value;
            }

            return IORegs[address];
        }

        private bool _shouldReset = false;
        public void OnClock()
        {
            Cpu.CYCLES += 1;
            if (cyclesToWait > 1)
            {
                InvokeOnClockCompleted();
                cyclesToWait -= 1;
                return;
            }

            if (_shouldReset)
            {
                Reset();
                _shouldReset = false;
                InvokeOnClockCompleted();
                return;
            }

            ExecInstruction((ushort)((flashMemory[Cpu.PC + 1] << 8) | flashMemory[Cpu.PC])); // little-endian byte order
            Cpu.PC += 2;

            InvokeOnClockCompleted();

            if (Cpu.PC >= firmSize)
            {
                _shouldReset = true;
            }
        }

        public static event EventHandler<EventArgs> OnClockCompleted;
        public void InvokeOnClockCompleted()
        {
            EventHandler<EventArgs> handler = OnClockCompleted;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
