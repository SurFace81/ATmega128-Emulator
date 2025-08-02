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
    public partial class RegistersView : DockContent
    {
        private Dictionary<string, int> numSystems = new Dictionary<string, int>()
        {
            { "HEX", 0 },
            { "DEC", 1 },
            { "OCT", 2 },
            { "BIN", 3 },
        };

        private BindingList<RegisterItem> rows;

        public RegistersView()
        {
            InitializeComponent();
            numFormatCb.SelectedIndex = numSystems["HEX"];

            rows = new BindingList<RegisterItem>();
            regsGridView.DataSource = rows;
        }

        public void UpdateRegisters()
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    int scrollIndex = regsGridView.FirstDisplayedScrollingRowIndex;
                    int selectedRow = -1;
                    if (regsGridView.SelectedRows.Count > 0)
                    {
                        selectedRow = regsGridView.SelectedRows[0].Index;
                    }

                    rows.Clear();
                    rows.Add(new RegisterItem { Name = "X", Value = FormatToString(Cpu.X, 4) });
                    rows.Add(new RegisterItem { Name = "Y", Value = FormatToString(Cpu.Y, 4) });
                    rows.Add(new RegisterItem { Name = "Z", Value = FormatToString(Cpu.Z, 4) });
                    rows.Add(new RegisterItem { Name = "SREG", Value = "0b" + Convert.ToString(GetSreg(), 2).PadLeft(8, '0') }); // Always in binary format
                    // SP
                    rows.Add(new RegisterItem { Name = "PC", Value = FormatToString((uint)(Cpu.PC / 2), 4) });
                    // CYCLES
                    for (int i = 0; i < Cpu.R.Length; i++)
                    {
                        rows.Add(new RegisterItem { Name = $"R{i}", Value = FormatToString(Cpu.R[i], 2) });
                    }
                    // RAMPZ
                    // Z24

                    if (scrollIndex != -1)
                    {
                        regsGridView.FirstDisplayedScrollingRowIndex = scrollIndex;
                    }
                    if (selectedRow != -1)
                    {
                        regsGridView.Rows[selectedRow].Selected = true;
                    }
                });
            }
            catch { }
        }

        private byte GetSreg()
        {
            byte sreg = 0;
            sreg |= (byte)(Cpu.SREG.C ? 1 << 0 : 0);
            sreg |= (byte)(Cpu.SREG.Z ? 1 << 1 : 0);
            sreg |= (byte)(Cpu.SREG.N ? 1 << 2 : 0);
            sreg |= (byte)(Cpu.SREG.V ? 1 << 3 : 0);
            sreg |= (byte)(Cpu.SREG.S ? 1 << 4 : 0);
            sreg |= (byte)(Cpu.SREG.H ? 1 << 5 : 0);
            sreg |= (byte)(Cpu.SREG.T ? 1 << 6 : 0);
            sreg |= (byte)(Cpu.SREG.I ? 1 << 7 : 0);

            return sreg;
        }

        private string FormatToString(uint num, int size = 2)
        {
            if (numFormatCb.SelectedIndex == numSystems["HEX"])
            {
                return "0x" + num.ToString($"X{size}");
            }
            if (numFormatCb.SelectedIndex == numSystems["DEC"])
            {
                return num.ToString();
            }
            if (numFormatCb.SelectedIndex == numSystems["OCT"])
            {
                return "0" + Convert.ToString(num, 8).PadLeft(size, '0');
            }
            if (numFormatCb.SelectedIndex == numSystems["BIN"])
            {
                return "0b" + Convert.ToString(num, 2).PadLeft(size, '0');
            }

            return "";
        }
    }

    public class RegisterItem
    {
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Value")]
        public string Value { get; set; }
    }
}
