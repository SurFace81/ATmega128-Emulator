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
using System.Windows.Navigation;

namespace ATmegaSim.CPU
{
    public class Cpu : IClockSink
    {
        public const int FLASH_SIZE = 0x20000;
        public const int DATA_SIZE = 0x10000;
        public const int IO_SIZE = 0x40;
        public const ushort RAMEND = 0x10FF;
        private int firmSize;

        private readonly Dictionary<byte, Action<byte>> IOWriteHandlers = new Dictionary<byte, Action<byte>>();
        private readonly Dictionary<byte, Func<byte>> IOReadHandlers = new Dictionary<byte, Func<byte>>();
        // Точка расширения для ExtIO (data 0x60..0xFF): порты F/G добавит другой агент
        // регистрацией handlers в этих словарях. Пока пусты -> прямой доступ к ExtIORegs.
        private readonly Dictionary<byte, Action<byte>> ExtIOWriteHandlers = new Dictionary<byte, Action<byte>>();
        private readonly Dictionary<byte, Func<byte>> ExtIOReadHandlers = new Dictionary<byte, Func<byte>>();

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
            if (firmFile == null) return false;
            if (firmFile.Count > FLASH_SIZE) return false;

            // Залить FLASH 0xFF чтобы хвост старой прошивки не оставался
            for (int i = 0; i < FLASH_SIZE; i++) state.FLASH[i] = 0xFF;

            byte[] temp = firmFile.ToArray();
            firmSize = temp.Length;
            Array.Copy(temp, 0, state.FLASH, 0, temp.Length);

            Reset();
            return true;
        }

        public void Step()
        {
            // Шаг по инструкции, а не по такту: выполнить первое слово
            // и дождаться multi-cycle хвоста (cyclesToWait), не задевая reset-тикт.
            OnClock();
            while (cyclesToWait > 1 && !_shouldReset)
            {
                OnClock();
            }
        }

        public void Reset()
        {
            state.PC = 0;
            state.CYCLES = 0;
            state.SREG = new CpuState.SREGStruct();
            Array.Clear(state.R, 0, state.R.Length);
            Array.Clear(state.IORegs, 0, state.IORegs.Length);
            Array.Clear(state.ExtIORegs, 0, state.ExtIORegs.Length);
            Array.Clear(state.SRAM, 0, state.SRAM.Length);
            state.IORegs[0x3F] = 0;
            state.SP = RAMEND; // пишет SPL/SPH (IORegs[0x3D]/[0x3E])

            // Сброс PORT-объектов напрямую (IOPort.cs - чужая зона, метод Reset туда не добавляем)
            state.PORTA.DDR = 0; state.PORTA.PORT = 0; state.PORTA.PIN = 0;
            state.PORTB.DDR = 0; state.PORTB.PORT = 0; state.PORTB.PIN = 0;
            state.PORTC.DDR = 0; state.PORTC.PORT = 0; state.PORTC.PIN = 0;
            state.PORTD.DDR = 0; state.PORTD.PORT = 0; state.PORTD.PIN = 0;
            state.PORTE.DDR = 0; state.PORTE.PORT = 0; state.PORTE.PIN = 0;
            state.PORTF.DDR = 0; state.PORTF.PORT = 0; state.PORTF.PIN = 0;
            state.PORTG.DDR = 0; state.PORTG.PORT = 0; state.PORTG.PIN = 0;

            _shouldReset = false;
            cyclesToWait = 0;
            // FLASH и firmSize не трогаем

            InvokeOnClockCompleted();
        }

        private void InitializeIOHandlers()
        {
            // PIN-write = toggle PORT (datasheet: writing '1' to PINxn toggles PORTxn).
            // PORTA (0x1B - PORT, 0x1A - DDR, 0x19 - PIN)
            IOWriteHandlers[0x1B] = value => state.PORTA.WritePort(value);
            IOWriteHandlers[0x1A] = value => state.PORTA.WriteDDR(value);
            IOWriteHandlers[0x19] = value => state.PORTA.WritePort((byte)(state.PORTA.PORT ^ value));
            IOReadHandlers[0x1B] = () => state.PORTA.PORT;
            IOReadHandlers[0x1A] = () => state.PORTA.DDR;
            IOReadHandlers[0x19] = () => state.PORTA.ReadPin();

            // PORTB (0x18 - PORT, 0x17 - DDR, 0x16 - PIN)
            IOWriteHandlers[0x18] = value => state.PORTB.WritePort(value);
            IOWriteHandlers[0x17] = value => state.PORTB.WriteDDR(value);
            IOWriteHandlers[0x16] = value => state.PORTB.WritePort((byte)(state.PORTB.PORT ^ value));
            IOReadHandlers[0x18] = () => state.PORTB.PORT;
            IOReadHandlers[0x17] = () => state.PORTB.DDR;
            IOReadHandlers[0x16] = () => state.PORTB.ReadPin();

            // PORTC (0x15 - PORT, 0x14 - DDR, 0x13 - PIN)
            IOWriteHandlers[0x15] = value => state.PORTC.WritePort(value);
            IOWriteHandlers[0x14] = value => state.PORTC.WriteDDR(value);
            IOWriteHandlers[0x13] = value => state.PORTC.WritePort((byte)(state.PORTC.PORT ^ value));
            IOReadHandlers[0x15] = () => state.PORTC.PORT;
            IOReadHandlers[0x14] = () => state.PORTC.DDR;
            IOReadHandlers[0x13] = () => state.PORTC.ReadPin();

            // PORTD (0x12 - PORT, 0x11 - DDR, 0x10 - PIN)
            IOWriteHandlers[0x12] = value => state.PORTD.WritePort(value);
            IOWriteHandlers[0x11] = value => state.PORTD.WriteDDR(value);
            IOWriteHandlers[0x10] = value => state.PORTD.WritePort((byte)(state.PORTD.PORT ^ value));
            IOReadHandlers[0x12] = () => state.PORTD.PORT;
            IOReadHandlers[0x11] = () => state.PORTD.DDR;
            IOReadHandlers[0x10] = () => state.PORTD.ReadPin();

            // PORTE (0x03 - PORT, 0x02 - DDR, 0x01 - PIN)
            IOWriteHandlers[0x03] = value => state.PORTE.WritePort(value);
            IOWriteHandlers[0x02] = value => state.PORTE.WriteDDR(value);
            IOWriteHandlers[0x01] = value => state.PORTE.WritePort((byte)(state.PORTE.PORT ^ value));
            IOReadHandlers[0x03] = () => state.PORTE.PORT;
            IOReadHandlers[0x02] = () => state.PORTE.DDR;
            IOReadHandlers[0x01] = () => state.PORTE.ReadPin();

            // PINF (IO 0x00, data 0x20) - стандартный IO, читается как PIN порта F.
            // Запись '1' toggles PORTF (как у остальных PIN).
            IOWriteHandlers[0x00] = value => state.PORTF.WritePort((byte)(state.PORTF.PORT ^ value));
            IOReadHandlers[0x00] = () => state.PORTF.ReadPin();

            // PORTF/G в Extended IO (только LD/ST/LDS/STS, не IN/OUT):
            // data 0x60 PINF(зеркало? оставлено для совместимости, основной PINF - IO 0x00),
            // data 0x61 DDRF, 0x62 PORTF, 0x63 PING, 0x64 DDRG, 0x65 PORTG.
            // Ключ словарей - extAddr = dataAddr - 0x60.
            ExtIOWriteHandlers[0x01] = value => state.PORTF.WriteDDR(value);   // DDRF
            ExtIOWriteHandlers[0x02] = value => state.PORTF.WritePort(value);  // PORTF
            ExtIOWriteHandlers[0x00] = value => state.PORTF.WritePort((byte)(state.PORTF.PORT ^ value)); // PINF alias toggle
            ExtIOWriteHandlers[0x03] = value => state.PORTG.WritePort((byte)(state.PORTG.PORT ^ value)); // PING toggle
            ExtIOWriteHandlers[0x04] = value => state.PORTG.WriteDDR(value);   // DDRG
            ExtIOWriteHandlers[0x05] = value => state.PORTG.WritePort(value);  // PORTG
            ExtIOReadHandlers[0x00] = () => state.PORTF.ReadPin();  // PINF alias
            ExtIOReadHandlers[0x01] = () => state.PORTF.DDR;
            ExtIOReadHandlers[0x02] = () => state.PORTF.PORT;
            ExtIOReadHandlers[0x03] = () => state.PORTG.ReadPin();  // PING
            ExtIOReadHandlers[0x04] = () => state.PORTG.DDR;
            ExtIOReadHandlers[0x05] = () => state.PORTG.PORT;

            // RAMPZ (0x3B): property уже маппится на IORegs[0x3B], handlers для явного OUT/IN
            IOWriteHandlers[0x3B] = value => state.IORegs[0x3B] = value;
            IOReadHandlers[0x3B] = () => state.IORegs[0x3B];
            // SPL/SPH (0x3D/0x3E): синхронизация автоматическая — SP property читает/пишет
            // IORegs[0x3D]/IORegs[0x3E] напрямую, WriteIO/ReadIO идут через IORegs.

            // SREG (0x3F): разбор байта в struct / сборка struct в байт.
            // Порядок бит: C=0,Z=1,N=2,V=3,S=4,H=5,T=6,I=7 (как в RegistersView).
            IOWriteHandlers[0x3F] = value => state.SetSregByte(value);
            IOReadHandlers[0x3F] = () => state.GetSregByte();
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
            else if (addr < 0x60) // IO Regs (data 0x20..0x5F -> IO 0x00..0x3F): обязательно через handlers
            {
                return ReadIO((byte)(addr - 0x20));
            }
            else if (addr < 0x100) // Ext IO Regs (точка расширения: handlers добавит другой агент)
            {
                byte extAddr = (byte)(addr - 0x60);
                Func<byte> handler;
                if (ExtIOReadHandlers.TryGetValue(extAddr, out handler))
                {
                    byte value = handler();
                    state.ExtIORegs[addr - 0x60] = value;
                    return value;
                }
                return state.ExtIORegs[addr - 0x60];
            }
            else // SRAM (0x100..0xFFFF)
            {
                int idx = addr - 0x100;
                if ((uint)idx >= (uint)state.SRAM.Length) return 0xFF;
                return state.SRAM[idx];
            }
        }

        public void SetDataMem(ushort addr, byte value)
        {
            if (addr < 0x20)   // R0 - R31
            {
                state.R[addr] = value;
            }
            else if (addr < 0x60) // IO Regs: обязательно через handlers
            {
                WriteIO((byte)(addr - 0x20), value);
            }
            else if (addr < 0x100) // Ext IO Regs (точка расширения)
            {
                byte extAddr = (byte)(addr - 0x60);
                Action<byte> handler;
                state.ExtIORegs[addr - 0x60] = value;
                if (ExtIOWriteHandlers.TryGetValue(extAddr, out handler))
                {
                    handler(value);
                }
            }
            else // SRAM
            {
                int idx = addr - 0x100;
                if ((uint)idx >= (uint)state.SRAM.Length) return;
                state.SRAM[idx] = value;
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

            // Защита границ FLASH. AVR не хранит размер загруженного HEX:
            // незапрограммированная область также должна быть исполнимой.
            // Контракт PC+=2 сохраняется для нормальных инструкций (ветвления ставят
            // PC=target-2, 2-словные делают внутренний PC+=2 — внешний PC+=2 не меняем).
            if (state.PC + 1u >= (uint)FLASH_SIZE)
            {
                InvokeOnClockCompleted();
                _shouldReset = true;
                return;
            }

            cyclesToWait = commands.ExecInsruction(GetOpcodeAt(state.PC));
            // Commands мутирует SREG struct напрямую — синхронизировать байт для UI/MemoryView
            state.IORegs[0x3F] = state.GetSregByte();
            state.PC += 2;

            InvokeOnClockCompleted();

        }

        public ushort GetOpcodeAt(uint pntr)
        {
            if ((uint)pntr + 1u >= (uint)FLASH_SIZE) return 0x0000; // NOP вместо исключения
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
        // У ATmega128 внутренняя SRAM занимает только 0x0100..0x10FF (4 KiB).
        // Внешняя память намеренно не моделируется как обычная SRAM.
        public byte[] SRAM { get; set; } = new byte[0x1000];
        // PC хранится в байтовых адресах для прямого индексирования FLASH. Для
        // 128 KiB Flash ему требуется 17 бит, поэтому ushort здесь недостаточен.
        public uint PC { get; set; }
        // SP как маппинг на SPL/SPH (без backing field): get=(SPH<<8)|SPL, set=разложить.
        // Тогда IN/OUT/LD/ST к 0x5D/0x5E (data) и 0x3D/0x3E (IO) синхронны через IORegs.
        public ushort SP
        {
            get => (ushort)((IORegs[0x3E] << 8) | IORegs[0x3D]);
            set { IORegs[0x3D] = (byte)(value & 0xFF); IORegs[0x3E] = (byte)(value >> 8); }
        }
        public uint CYCLES { get; set; }

        public struct SREGStruct
        {
            public bool C, Z, N, V, S, H, T, I;
        }
        public SREGStruct SREG;

        // Порядок бит: C=0,Z=1,N=2,V=3,S=4,H=5,T=6,I=7 (как в RegistersView)
        public byte GetSregByte()
        {
            byte b = 0;
            if (SREG.C) b |= (byte)(1 << 0);
            if (SREG.Z) b |= (byte)(1 << 1);
            if (SREG.N) b |= (byte)(1 << 2);
            if (SREG.V) b |= (byte)(1 << 3);
            if (SREG.S) b |= (byte)(1 << 4);
            if (SREG.H) b |= (byte)(1 << 5);
            if (SREG.T) b |= (byte)(1 << 6);
            if (SREG.I) b |= (byte)(1 << 7);
            return b;
        }

        public void SetSregByte(byte value)
        {
            SREG.C = (value & (1 << 0)) != 0;
            SREG.Z = (value & (1 << 1)) != 0;
            SREG.N = (value & (1 << 2)) != 0;
            SREG.V = (value & (1 << 3)) != 0;
            SREG.S = (value & (1 << 4)) != 0;
            SREG.H = (value & (1 << 5)) != 0;
            SREG.T = (value & (1 << 6)) != 0;
            SREG.I = (value & (1 << 7)) != 0;
        }

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
        public IOPort PORTF { get; private set; } = new IOPort();
        public IOPort PORTG { get; private set; } = new IOPort(0x1F);
    }
}
