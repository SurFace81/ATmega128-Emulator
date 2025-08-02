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
        const int BYTES_PER_ROW = 16;
        private List<byte> firm;

        public MemoryView()
        {
            InitializeComponent();
            DisplayEmpty();
        }

        private void DisplayEmpty()
        {
            DisplayMemory(new byte[0], Cpu.FLASH_SIZE);
        }

        public void DisplayFirm(List<byte> firm)
        {
            this.firm = firm;
            DisplayMemory(firm, 0x20000);
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
            if (firm != null && pc >= firm.Count)
            {
                pc = 0;
            }

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    InvokeMethod(pc);
                });
            }
            else
            {
                InvokeMethod(pc);
            }
        }

        private void InvokeMethod(int pc)
        {
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
    }
}
