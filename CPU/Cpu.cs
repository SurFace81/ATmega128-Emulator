using ATmegaSim.ClockSys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATmegaSim.CPU
{
    public class Cpu : IClockSink
    {
        public const int FLASH_SIZE = 0x20000;
        public const int DATA_SIZE = 0x10FF;
        public const int IO_SIZE = 0x40;
        private int firmSize;

        private readonly Dictionary<byte, Action<byte>> IOWriteHandlers = new Dictionary<byte, Action<byte>>();
        private readonly Dictionary<byte, Func<byte>> IOReadHandlers = new Dictionary<byte, Func<byte>>();

        private static Cpu _instance;
        private Commands commands;
        public CpuState state;
        public static Cpu Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Cpu();
                }
                return _instance;
            }
        }
        private Cpu()
        {
            state = new CpuState();
            commands = new Commands(this);
            InitializeIOHandlers();
        }

        public bool LoadFirm(List<byte> firmFile)
        {
            if (firmFile.Count > FLASH_SIZE) return false;

            byte[] temp = firmFile.ToArray();
            firmSize = temp.Length;
            Array.Copy(temp, 0, state.FLASH, 0, temp.Length);

            state.PC = 0;
            return true;
        }

        public void Step()
        {
            OnClock();
        }

        public void Reset()
        {
            state.PC = 0;
            state.CYCLES = 0;
            state.SREG = new CpuState.SREGStruct();
            Array.Clear(state.R, 0, 32);
            Array.Clear(state.IORegs, 0, 64);
            Array.Clear(state.ExtIORegs, 0, 160);
            Array.Clear(state.SRAM, 0, 65279);

            InvokeOnClockCompleted();
        }

        private void InitializeIOHandlers()
        {
            // PORTA (0x1B - PORT, 0x1A - DDR, 0x19 - PIN)
            IOWriteHandlers[0x1B] = value => state.PORTA.WritePort(value);
            IOWriteHandlers[0x1A] = value => state.PORTA.WriteDDR(value);
            IOReadHandlers[0x1B] = () => state.PORTA.PORT;
            IOReadHandlers[0x1A] = () => state.PORTA.DDR;
            IOReadHandlers[0x19] = () => state.PORTA.ReadPin();

            // PORTB (0x18 - PORT, 0x17 - DDR, 0x16 - PIN)
            IOWriteHandlers[0x18] = value => state.PORTB.WritePort(value);
            IOWriteHandlers[0x17] = value => state.PORTB.WriteDDR(value);
            IOReadHandlers[0x18] = () => state.PORTB.PORT;
            IOReadHandlers[0x17] = () => state.PORTB.DDR;
            IOReadHandlers[0x16] = () => state.PORTB.ReadPin();

            // PORTC (0x15 - PORT, 0x14 - DDR, 0x13 - PIN)
            IOWriteHandlers[0x15] = value => state.PORTC.WritePort(value);
            IOWriteHandlers[0x14] = value => state.PORTC.WriteDDR(value);
            IOReadHandlers[0x15] = () => state.PORTC.PORT;
            IOReadHandlers[0x14] = () => state.PORTC.DDR;
            IOReadHandlers[0x13] = () => state.PORTC.ReadPin();

            // PORTD (0x12 - PORT, 0x11 - DDR, 0x10 - PIN)
            IOWriteHandlers[0x12] = value => state.PORTD.WritePort(value);
            IOWriteHandlers[0x11] = value => state.PORTD.WriteDDR(value);
            IOReadHandlers[0x12] = () => state.PORTD.PORT;
            IOReadHandlers[0x11] = () => state.PORTD.DDR;
            IOReadHandlers[0x10] = () => state.PORTD.ReadPin();

            // PORTE (0x03 - PORT, 0x02 - DDR, 0x01 - PIN)
            IOWriteHandlers[0x03] = value => state.PORTE.WritePort(value);
            IOWriteHandlers[0x02] = value => state.PORTE.WriteDDR(value);
            IOReadHandlers[0x03] = () => state.PORTE.PORT;
            IOReadHandlers[0x02] = () => state.PORTE.DDR;
            IOReadHandlers[0x01] = () => state.PORTE.ReadPin();
        }

        public void WriteIO(byte address, byte value)
        {
            state.IORegs[address] = value;

            if (IOWriteHandlers.ContainsKey(address))
            {
                IOWriteHandlers[address](value);
            }
        }

        public byte ReadIO(byte address)
        {
            if (IOReadHandlers.ContainsKey(address))
            {
                byte value = IOReadHandlers[address]();
                state.IORegs[address] = value;
                return value;
            }

            return state.IORegs[address];
        }

        public byte GetDataMem(ushort addr)
        {
            if (addr < 0x20)   // R0 - R31
            {
                return state.R[addr];
            }
            else if (addr < 0x60) // IO Regs
            {
                return state.IORegs[addr - 0x20];
            }
            else if (addr < 0x100) // Ext IO Regs
            {
                return state.ExtIORegs[addr - 0x60];
            }
            else // SRAM
            {
                return state.SRAM[addr - 0x100];
            }
        }

        public void SetDataMem(ushort addr, byte value)
        {
            if (addr < 0x20)   // R0 - R31
            {
                state.R[addr] = value;
            }
            else if (addr < 0x60) // IO Regs
            {
                state.IORegs[addr - 0x20] = value;
            }
            else if (addr < 0x100) // Ext IO Regs
            {
                state.ExtIORegs[addr - 0x60] = value;
            }
            else // SRAM
            {
                state.SRAM[addr - 0x100] = value;
            }
        }

        private bool _shouldReset = false;
        int cyclesToWait = 0;
        public void OnClock()
        {
            state.CYCLES += 1;
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

            cyclesToWait = commands.ExecInsruction(GetOpcodeAt(state.PC));
            state.PC += 2;

            InvokeOnClockCompleted();

            if (state.PC >= firmSize)
            {
                _shouldReset = true;
            }
        }

        public ushort GetOpcodeAt(ushort pntr)
        {
            return (ushort)((state.FLASH[pntr + 1] << 8) | state.FLASH[pntr]); // little-endian byte order
        }

        public event EventHandler<EventArgs> OnClockCompleted;
        public void InvokeOnClockCompleted()
        {
            EventHandler<EventArgs> handler = OnClockCompleted;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }

    public class CpuState
    {
        public byte[] FLASH = Enumerable.Repeat((byte)0xFF, Cpu.FLASH_SIZE).ToArray();
        public byte[] R { get; set; } = new byte[32];
        public byte[] IORegs { get; set; } = new byte[64];
        public byte[] ExtIORegs { get; set; } = new byte[160];
        public byte[] SRAM { get; set; } = new byte[65279];
        public ushort PC { get; set; }
        public uint CYCLES { get; set; }

        public struct SREGStruct
        {
            public bool C, Z, N, V, S, H, T, I;
        }
        public SREGStruct SREG;

        // Свойства для регистровых пар
        public ushort X
        {
            get => (ushort)((R[27] << 8) | R[26]);
            set { R[27] = (byte)(value >> 8); R[26] = (byte)(value & 0xFF); }
        }

        public ushort Y
        {
            get => (ushort)((R[29] << 8) | R[28]);
            set { R[29] = (byte)(value >> 8); R[28] = (byte)(value & 0xFF); }
        }

        public ushort Z
        {
            get => (ushort)((R[31] << 8) | R[30]);
            set { R[31] = (byte)(value >> 8); R[30] = (byte)(value & 0xFF); }
        }

        public byte RAMPZ
        {
            get => (byte)(IORegs[0x3B]);
            set { IORegs[0x3B] = (byte)value; }
        }

        public uint Z24
        {
            get => (uint)(((uint)RAMPZ << 16) | Z);
            set { RAMPZ = (byte)(value >> 16); Z = (ushort)(value & 0xFFFF); }
        }

        public IOPort PORTA { get; private set; } = new IOPort();
        public IOPort PORTB { get; private set; } = new IOPort();
        public IOPort PORTC { get; private set; } = new IOPort();
        public IOPort PORTD { get; private set; } = new IOPort();
        public IOPort PORTE { get; private set; } = new IOPort();
    }
}
