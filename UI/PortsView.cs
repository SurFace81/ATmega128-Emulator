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
        private const int PORTA = 0, PORTB = 1, PORTC = 2, PORTD = 3, PORTE = 4;

        public PortsView()
        {
            InitializeComponent();
            portAControl.OnPinsStateChanged += PortAControl_OnPinsStateChanged;
            portBControl.OnPinsStateChanged += PortBControl_OnPinsStateChanged;
            portCControl.OnPinsStateChanged += PortCControl_OnPinsStateChanged;
            portDControl.OnPinsStateChanged += PortDControl_OnPinsStateChanged;
            portEControl.OnPinsStateChanged += PortEControl_OnPinsStateChanged;
        }

        private void PortAControl_OnPinsStateChanged(object sender, byte e)
        {
            Cpu.IOPorts[PORTA].SetExternalInput(e);
        }
        private void PortBControl_OnPinsStateChanged(object sender, byte e)
        {
            Cpu.IOPorts[PORTB].SetExternalInput(e);
        }
        private void PortCControl_OnPinsStateChanged(object sender, byte e)
        {
            Cpu.IOPorts[PORTC].SetExternalInput(e);
        }
        private void PortDControl_OnPinsStateChanged(object sender, byte e)
        {
            Cpu.IOPorts[PORTD].SetExternalInput(e);
        }
        private void PortEControl_OnPinsStateChanged(object sender, byte e)
        {
            Cpu.IOPorts[PORTE].SetExternalInput(e);
        }

        private void SyncIOPorts()
        {
            Cpu.IOPorts[PORTA].PIN = Cpu.IORegs[0x19];
            Cpu.IOPorts[PORTA].DDR = Cpu.IORegs[0x1A];
            Cpu.IOPorts[PORTA].PORT = Cpu.IORegs[0x1B];

            Cpu.IOPorts[PORTB].PIN = Cpu.IORegs[0x16];
            Cpu.IOPorts[PORTB].DDR = Cpu.IORegs[0x17];
            Cpu.IOPorts[PORTB].PORT = Cpu.IORegs[0x18];

            Cpu.IOPorts[PORTC].PIN = Cpu.IORegs[0x13];
            Cpu.IOPorts[PORTC].DDR = Cpu.IORegs[0x14];
            Cpu.IOPorts[PORTC].PORT = Cpu.IORegs[0x15];

            Cpu.IOPorts[PORTD].PIN = Cpu.IORegs[0x10];
            Cpu.IOPorts[PORTD].DDR = Cpu.IORegs[0x11];
            Cpu.IOPorts[PORTD].PORT = Cpu.IORegs[0x12];

            Cpu.IOPorts[PORTE].PIN = Cpu.IORegs[0x01];
            Cpu.IOPorts[PORTE].DDR = Cpu.IORegs[0x02];
            Cpu.IOPorts[PORTE].PORT = Cpu.IORegs[0x03];
        }

        public void UpdatePorts()
        {
            //SyncIOPorts();

            // TODO: Надо бы сделать обозначения для разных направлений
            portAControl.SetPins(Cpu.IOPorts[PORTA].ReadPin());
            portBControl.SetPins(Cpu.IOPorts[PORTB].ReadPin());
            portCControl.SetPins(Cpu.IOPorts[PORTC].ReadPin());
            portDControl.SetPins(Cpu.IOPorts[PORTD].ReadPin());
            portEControl.SetPins(Cpu.IOPorts[PORTE].ReadPin());
        }
    }
}
