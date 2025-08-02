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

namespace ATmegaSim.UI
{
    public partial class HexViewer : UserControl
    {
        const int BYTES_PER_ROW = 16;
        public int progPntr { get; private set; }
        private List<byte> firm;

        public HexViewer()
        {
            InitializeComponent();
            firmTextBox.TabStop = false;
            firmTextBox.Font = new Font("Consolas", 9, FontStyle.Regular);

            StringBuilder sb = new StringBuilder(Cpu.FLASH_SIZE * 4);

            sb.AppendLine("Address   00 01 02 03 04 05 06 07  08 09 0A 0B 0C 0D 0E 0F | ASCII");
            sb.AppendLine("-----------------------------------------------------------|-----------------");

            for (int i = 0; i < Cpu.FLASH_SIZE; i += BYTES_PER_ROW)
            {
                sb.Append(i.ToString("X8")).Append(": ");

                for (int j = 0; j < BYTES_PER_ROW; j++)
                {
                    sb.Append("FF ");
                    if (j == 7) sb.Append(" ");
                }

                sb.Append("| ");
                for (int j = 0; j < BYTES_PER_ROW; j++)
                {
                    sb.Append(".");
                }

                sb.AppendLine();
            }

            firmTextBox.Clear();
            firmTextBox.AppendText(sb.ToString());
        }

        public void DisplayFirm(List<byte> firm)
        {
            this.firm = firm;

            firmTextBox.Clear();
            StringBuilder sb = new StringBuilder(2_000_000); // с запасом

            sb.AppendLine("Address   00 01 02 03 04 05 06 07  08 09 0A 0B 0C 0D 0E 0F | ASCII");
            sb.AppendLine("-----------------------------------------------------------|-----------------");

            const int limit = 0x20000;

            for (int i = 0; i < limit; i += BYTES_PER_ROW)
            {
                sb.Append(i.ToString("X8")).Append(": ");

                for (int j = 0; j < BYTES_PER_ROW; j++)
                {
                    int index = i + j;
                    byte value = index < firm.Count ? firm[index] : (byte)0xFF;
                    sb.Append(value.ToString("X2")).Append(" ");
                    if (j == 7) sb.Append(" ");
                }

                sb.Append("| ");

                for (int j = 0; j < BYTES_PER_ROW; j++)
                {
                    int index = i + j;
                    byte value = index < firm.Count ? firm[index] : (byte)0xFF;
                    char c = (char)value;
                    if (!char.IsLetterOrDigit(c) && !char.IsPunctuation(c) || c > 127)
                        sb.Append('.');
                    else
                        sb.Append(c);
                }

                sb.AppendLine();
            }

            firmTextBox.Clear();
            firmTextBox.AppendText(sb.ToString());
        }

        public void SetProgCntr(int pc)
        {
            if (firm != null && pc >= firm.Count)
            {
                pc = 0;
            }

            progPntr = pc;
            int rowIndex = pc / BYTES_PER_ROW + 2;
            int byteIndexInRow = pc % BYTES_PER_ROW;

            Invoke((MethodInvoker)delegate
            {
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
            });
        }

        private void firmTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
        }
    }
}
