using ATmegaSim.CPU;
using System;
using System.Timers;
using System.Windows.Forms;

namespace ATmegaSim
{
    public partial class Form1 : Form
    {
        Cpu atmega128;
        public Form1()
        {
            InitializeComponent();
            atmega128 = new Cpu();
        }

        private void loadFirmBtn_Click(object sender, EventArgs e)
        {
            if (openFirmDlg.ShowDialog() == DialogResult.OK)
            {
                firmPathText.Text = openFirmDlg.FileName;
                HexParser.Parse(openFirmDlg.FileName);
                firmViewer.DisplayFirm(HexParser.FirmFile);
                firmViewer.SetProgPntr(0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            firmViewer.SetProgPntr(firmViewer.progPntr + 1);
        }

        System.Timers.Timer execTimer;
        private void runBtn_Click(object sender, EventArgs e)
        {
            execTimer = new System.Timers.Timer();
            execTimer.Elapsed += ExecTimer_Elapsed;
            execTimer.Interval = 500;
            execTimer.Start();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            execTimer.Stop();
        }

        private void ExecTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            atmega128.Step((ushort)((HexParser.FirmFile[Cpu.PC + 1] << 8) | HexParser.FirmFile[Cpu.PC])); // little-endian byte order
            if (Cpu.PC >= HexParser.FirmFile.Count)
            {
                Cpu.PC = 0;
            }
            firmViewer.SetProgPntr(Cpu.PC);
        }
    }
}
