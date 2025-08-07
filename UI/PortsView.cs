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
        private CpuState cpuState;
        public PortsView(Cpu cpu)
        {
            InitializeComponent();
            this.cpuState = cpu.state;

            portAControl.OnPinsStateChanged += PortAControl_OnPinsStateChanged;
            portBControl.OnPinsStateChanged += PortBControl_OnPinsStateChanged;
            portCControl.OnPinsStateChanged += PortCControl_OnPinsStateChanged;
            portDControl.OnPinsStateChanged += PortDControl_OnPinsStateChanged;
            portEControl.OnPinsStateChanged += PortEControl_OnPinsStateChanged;
        }

        private void PortAControl_OnPinsStateChanged(object sender, byte e)
        {
            cpuState.PORTA.SetExternalInput(e);
        }
        private void PortBControl_OnPinsStateChanged(object sender, byte e)
        {
            cpuState.PORTB.SetExternalInput(e);
        }
        private void PortCControl_OnPinsStateChanged(object sender, byte e)
        {
            cpuState.PORTC.SetExternalInput(e);
        }
        private void PortDControl_OnPinsStateChanged(object sender, byte e)
        {
            cpuState.PORTD.SetExternalInput(e);
        }
        private void PortEControl_OnPinsStateChanged(object sender, byte e)
        {
            cpuState.PORTE.SetExternalInput(e);
        }

        public void UpdatePorts()
        {
            // TODO: Надо бы сделать обозначения для разных направлений
            portAControl.SetPins(cpuState.PORTA.ReadPin());
            portBControl.SetPins(cpuState.PORTB.ReadPin());
            portCControl.SetPins(cpuState.PORTC.ReadPin());
            portDControl.SetPins(cpuState.PORTD.ReadPin());
            portEControl.SetPins(cpuState.PORTE.ReadPin());
        }
    }
}
