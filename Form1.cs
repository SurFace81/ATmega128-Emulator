using ATmegaSim.ClockSys;
using ATmegaSim.CPU;
using ATmegaSim.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ATmegaSim
{
    public partial class Form1 : Form
    {
        Cpu atmega128;
        Clock systemClock;

        List<MemoryView> memViews = new List<MemoryView>();
        List<RegistersView> regsViews = new List<RegistersView>();
        List<DisassemblyView> disViews = new List<DisassemblyView>();
        List<PortsView> portsViews = new List<PortsView>();

        public Form1()
        {
            InitializeComponent();
            dockPanel.Theme = new VS2015LightTheme();

            var mv = new MemoryView();
            mv.FormClosed += ((object sender, FormClosedEventArgs e) => memViews.Remove((MemoryView)sender));
            memViews.Add(mv);
            memViews[memViews.Count - 1].Show(dockPanel, DockState.Document);
            memViews[memViews.Count - 1].SetProgCntr(0);

            var rv = new RegistersView();
            rv.FormClosed += ((object sender, FormClosedEventArgs e) => regsViews.Remove((RegistersView)sender));
            regsViews.Add(rv);
            regsViews[regsViews.Count - 1].Show(dockPanel, DockState.DockRight);

            systemClock = new Clock();
        }

        private void Atmega128_OnClockCompleted(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                foreach (var regsView in regsViews)
                    regsView.UpdateRegisters();
                foreach (var memView in memViews)
                    memView.SetProgCntr(Cpu.PC);
                foreach (var disView in disViews)
                    disView.SetProgCntr(Cpu.PC);
                foreach (var portView in portsViews)
                    portView.UpdatePorts();
            });
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            stopBtn.Enabled = true;
            runBtn.Enabled = false;
            systemClock.Start();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            stopBtn.Enabled = false;
            runBtn.Enabled = true;
            systemClock.Stop();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openFileMenuItem_Click(object sender, EventArgs e)
        {
            if (openFirmDlg.ShowDialog() == DialogResult.OK)
            {
                firmPathText.Text = openFirmDlg.FileName;
                HexParser.Parse(openFirmDlg.FileName);

                atmega128 = new Cpu();
                if (!atmega128.LoadFirm(HexParser.FirmFile))
                {
                    MessageBox.Show($"Размер прошивки {HexParser.FirmFile.Count} байт превышает размер FLASH памяти ATmega128 (128Кб)!");
                    return;
                }
                Cpu.OnClockCompleted += Atmega128_OnClockCompleted;
                systemClock.Register(atmega128);

                foreach (var memView in memViews)
                    memView.DisplayFirm(HexParser.FirmFile);
                foreach (var disView in disViews)
                    disView.DisplayDisasm(HexParser.FirmFile);

                resetBtn.Enabled = true;
                stepBtn.Enabled = true;
            }
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            HexParser.FirmFile = new List<byte>();

            foreach (var memView in memViews)
                memView.DisplayFirm(HexParser.FirmFile);

            resetBtn.Enabled = false;
            stepBtn.Enabled = false;
        }

        private void registersMenuItem_Click(object sender, EventArgs e)
        {
            if (regsViews.Count >= 4) return;

            var rv = new RegistersView();
            rv.FormClosed += ((object sender, FormClosedEventArgs e) => regsViews.Remove((RegistersView)sender));
            regsViews.Add(rv);
            regsViews[regsViews.Count - 1].Show(dockPanel, DockState.DockRight);
        }

        private void memoryMenuItem_Click(object sender, EventArgs e)
        {
            if(memViews.Count >= 4) return;

            var mv = new MemoryView();
            mv.FormClosed += ((object sender, FormClosedEventArgs e) => memViews.Remove((MemoryView)sender));
            memViews.Add(mv);
            memViews[memViews.Count - 1].Show(dockPanel, DockState.DockRight);
            memViews[memViews.Count - 1].DisplayFirm(HexParser.FirmFile);
        }

        private void disasmMenuItem_Click(object sender, EventArgs e)
        {
            if (disViews.Count >= 4) return;

            var dv = new DisassemblyView();
            dv.FormClosed += ((object sender, FormClosedEventArgs e) => disViews.Remove((DisassemblyView)sender));
            disViews.Add(dv);
            disViews[disViews.Count - 1].Show(dockPanel, DockState.DockRight);
        }
        private void portsMenuItem_Click(object sender, EventArgs e)
        {
            if (portsViews.Count >= 4) return;

            var pv = new PortsView();
            pv.FormClosed += ((object sender, FormClosedEventArgs e) => portsViews.Remove((PortsView)sender));
            pv.FormBorderStyle = FormBorderStyle.FixedDialog;
            portsViews.Add(pv);
            portsViews[portsViews.Count - 1].Show(dockPanel, DockState.Float);
        }

        private void stepBtn_Click(object sender, EventArgs e)
        {
            atmega128?.Step();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            atmega128?.Reset();
        }
    }
}
