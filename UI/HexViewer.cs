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
            firmTextBox.Font = new Font("Consolas", 9, FontStyle.Regular);
        }

        public void DisplayFirm(List<byte> firm)
        {
            this.firm = firm;

            for (int i = 0; i < firm.Count; i += BYTES_PER_ROW)
            {
                firmTextBox.AppendText($"{i.ToString("X8")}: ");

                for (int j = 0; j < BYTES_PER_ROW; j++)
                {
                    if (i + j < firm.Count)
                    {
                        firmTextBox.AppendText($"{firm[i + j].ToString("X2")} ");
                    }
                    else
                    {
                        firmTextBox.AppendText("   ");
                    }

                    if (j == 7) firmTextBox.AppendText(" ");
                }
                firmTextBox.AppendText("| ");

                for (int j = 0; j < BYTES_PER_ROW; j++)
                {
                    if (i + j < firm.Count)
                    {
                        char c = (char)firm[i + j];
                        if (!char.IsLetterOrDigit(c) && !char.IsPunctuation(c))
                        {
                            firmTextBox.AppendText(".");
                        }
                        else
                        {
                            firmTextBox.AppendText(c.ToString());
                        }
                    }
                    else
                    {
                        firmTextBox.AppendText(" ");
                    }
                }

                firmTextBox.AppendText("\n");
            }
        }

        public void SetProgPntr(int pp)
        {
            if (pp >= firm.Count)
            {
                pp = 0;
            }

            progPntr = pp;
            int rowIndex = pp / BYTES_PER_ROW;
            int byteIndexInRow = pp % BYTES_PER_ROW;

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
    }
}
