using ATmegaSim.CPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ATmegaSim.UI
{
    public partial class PortsView : DockContent
    {
        private static IOPort PORTA = new IOPort();
        private static IOPort PORTB = new IOPort();
        private static IOPort PORTC = new IOPort();
        private static IOPort PORTD = new IOPort();
        private static IOPort PORTE = new IOPort();
        // TODO: Доступ к ним только через LDS/STS и т.д.
        //private static IOPort PORTF = new IOPort();
        //private static IOPort PORTG = new IOPort();

        public PortsView()
        {
            InitializeComponent();

        }

        private void SyncIOPorts()
        {
            PORTA.PIN = Cpu.IORegs[0x19];
            PORTA.DDR = Cpu.IORegs[0x1A];
            PORTA.PORT = Cpu.IORegs[0x1B];

            PORTB.PIN = Cpu.IORegs[0x16];
            PORTB.DDR = Cpu.IORegs[0x17];
            PORTB.PORT = Cpu.IORegs[0x18];

            PORTC.PIN = Cpu.IORegs[0x13];
            PORTC.DDR = Cpu.IORegs[0x14];
            PORTC.PORT = Cpu.IORegs[0x15];

            PORTD.PIN = Cpu.IORegs[0x10];
            PORTD.DDR = Cpu.IORegs[0x11];
            PORTD.PORT = Cpu.IORegs[0x12];

            PORTE.PIN = Cpu.IORegs[0x01];
            PORTE.DDR = Cpu.IORegs[0x02];
            PORTE.PORT = Cpu.IORegs[0x03];
        }

        public void UpdatePorts()
        {
            SyncIOPorts();

            // TODO: Надо бы сделать обозначение для разных направлений
            portAControl.SetPins((byte)(PORTA.PIN | PORTA.PORT));
            portBControl.SetPins((byte)(PORTB.PIN | PORTB.PORT));
            portCControl.SetPins((byte)(PORTC.PIN | PORTC.PORT));
            portDControl.SetPins((byte)(PORTD.PIN | PORTD.PORT));
            portEControl.SetPins((byte)(PORTE.PIN | PORTE.PORT));
        }
    }
}
