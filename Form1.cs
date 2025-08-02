using ATmegaSim.ClockSys;
using ATmegaSim.CPU;
using System;
using System.Timers;
using System.Windows.Forms;

namespace ATmegaSim
{
    public partial class Form1 : Form
    {
        Cpu atmega128;
        Clock systemClock;

        public Form1()
        {
            InitializeComponent();
            systemClock = new Clock();
        }

        private void loadFirmBtn_Click(object sender, EventArgs e)
        {
            if (openFirmDlg.ShowDialog() == DialogResult.OK)
            {
                firmPathText.Text = openFirmDlg.FileName;
                HexParser.Parse(openFirmDlg.FileName);

                atmega128 = new Cpu(HexParser.FirmFile);
                atmega128.OnClockCompleted += Atmega128_OnClockCompleted;
                systemClock.Register(atmega128);

                firmViewer.DisplayFirm(HexParser.FirmFile);
                firmViewer.SetProgPntr(0);
            }
        }

        private void Atmega128_OnClockCompleted(object sender, EventArgs e)
        {
            firmViewer.SetProgPntr(Cpu.PC);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            firmViewer.SetProgPntr(firmViewer.progPntr + 1);
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            systemClock.Start();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            systemClock.Stop();
        }
    }
}
