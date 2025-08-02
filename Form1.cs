using ATmegaSim.ClockSys;
using ATmegaSim.CPU;
using ATmegaSim.UI;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ATmegaSim
{
    public partial class Form1 : Form
    {
        Cpu atmega128;
        Clock systemClock;

        MemoryView memView;

        public Form1()
        {
            InitializeComponent();
            dockPanel.Theme = new VS2015LightTheme();
            memView = new MemoryView();
            memView.Show(dockPanel, DockState.Document);
            memView.SetProgCntr(0);

            systemClock = new Clock();
        }

        private void Atmega128_OnClockCompleted(object sender, EventArgs e)
        {
            memView.SetProgCntr(Cpu.PC);
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            systemClock.Start();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
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
                atmega128.OnClockCompleted += Atmega128_OnClockCompleted;
                systemClock.Register(atmega128);

                memView.DisplayFirm(HexParser.FirmFile);
            }
        }

        private void registersMenuItem_Click(object sender, EventArgs e)
        {
            var regsView = new RegistersView();
            regsView.Show(dockPanel, DockState.DockRight);
        }

        private void memoryMenuItem_Click(object sender, EventArgs e)
        {
            memView = new MemoryView();
            memView.Show(dockPanel, DockState.Document);
            memView.DisplayFirm(HexParser.FirmFile);
        }

        private void disasmMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
