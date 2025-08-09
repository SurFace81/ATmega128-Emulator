using ATmegaSim.CPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ATmegaSim.UI
{
    public partial class MemoryView : DockContent
    {
        private const int BYTES_PER_ROW = 16;
        private const int CODE_MEM = 0, DATA_MEM = 1;
        private List<byte> firm;

        private Cpu cpu;
        private CpuState cpuState;
        public MemoryView(Cpu cpu)
        {
            InitializeComponent();
            this.cpu = cpu;
            this.cpuState = cpu.state;
            DisplayEmpty();
            memZoneCb.SelectedIndex = 0;
        }

        private void DisplayEmpty()
        {
            DisplayMemory(new byte[0], Cpu.FLASH_SIZE);
        }

        public void DisplayFirm(List<byte> firm)
        {
            if (memZoneCb.SelectedIndex != CODE_MEM) return;
            
            this.firm = firm;
            if (firm != null)
            {
                DisplayMemory(firm, Cpu.FLASH_SIZE);
            }
            else
            {
                DisplayMemory(new byte[0], Cpu.FLASH_SIZE);
            }
        }

        public void DisplayData()
        {
            firmTextBox.SelectAll();
            firmTextBox.SelectionBackColor = firmTextBox.BackColor;
            firmTextBox.SelectionColor = firmTextBox.ForeColor;

            List<byte> data = new List<byte>();
            data.AddRange(cpuState.R);
            data.AddRange(cpuState.IORegs);
            data.AddRange(cpuState.ExtIORegs);
            data.AddRange(cpuState.SRAM);
            DisplayMemory(data, Cpu.DATA_SIZE);
        }

        private void DisplayMemory(IReadOnlyList<byte> data, int limit)
        {
            StringBuilder sb = new StringBuilder(data.Count * 4);

            sb.AppendLine("Address   00 01 02 03 04 05 06 07  08 09 0A 0B 0C 0D 0E 0F | ASCII");
            sb.AppendLine("-----------------------------------------------------------|-----------------");

            for (int i = 0; i < limit; i += BYTES_PER_ROW)
            {
                sb.Append(i.ToString("X8")).Append(": ");

                for (int j = 0; j < BYTES_PER_ROW; j++)
                {
                    int index = i + j;
                    byte b = index < data.Count ? data[index] : (byte)0xFF;
                    sb.Append(b.ToString("X2")).Append(" ");
                    if (j == 7) sb.Append(" ");
                }

                sb.Append("| ");

                for (int j = 0; j < BYTES_PER_ROW; j++)
                {
                    int index = i + j;
                    byte b = index < data.Count ? data[index] : (byte)0xFF;
                    char c = (char)b;
                    sb.Append((c >= 32 && c <= 126) ? c : '.');
                }

                sb.AppendLine();
            }

            firmTextBox.Text = sb.ToString();
            SetProgCntr(0);
        }

        public void SetProgCntr(int pc)
        {
            if (memZoneCb.SelectedIndex != CODE_MEM) return;

            if (firm != null && pc >= firm.Count)
            {
                pc = 0;
            }

            int rowIndex = pc / BYTES_PER_ROW + 2;
            int byteIndexInRow = pc % BYTES_PER_ROW;

            // Сброс цвета
            firmTextBox.SelectAll();
            firmTextBox.SelectionBackColor = firmTextBox.BackColor;
            firmTextBox.SelectionColor = firmTextBox.ForeColor;

            int baseCharIndx = firmTextBox.GetFirstCharIndexFromLine(rowIndex);
            int offset = 10 + (byteIndexInRow * 3);
            if (byteIndexInRow >= 8) offset += 1;
            int selStart = baseCharIndx + offset;

            // Выделение
            firmTextBox.SelectionStart = selStart;
            firmTextBox.SelectionLength = 5;
            firmTextBox.SelectionBackColor = Color.Lime;
            firmTextBox.SelectionColor = Color.Black;
        }

        public void UpdateOnClock(int pc)
        {
            if (memZoneCb.SelectedIndex == CODE_MEM)
                SetProgCntr(pc);
            if (memZoneCb.SelectedIndex == DATA_MEM)
                DisplayData();
        }

        private void memZoneCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (memZoneCb.SelectedIndex)
            {
                case CODE_MEM:
                    DisplayFirm(firm);
                    break;
                case DATA_MEM:
                    DisplayData();
                    break;
            }
        }
    }
}
