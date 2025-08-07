using ATmegaSim.CPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ATmegaSim.UI
{
    public partial class DisassemblyView : DockContent
    {
        private List<byte> mem = new List<byte>();
        private Cpu cpu;
        private Disassembler disasm;
        public DisassemblyView(Cpu cpu)
        {
            InitializeComponent();
            this.cpu = cpu;
            this.disasm = new Disassembler();
            DisplayDisasm(HexParser.FirmFile);
        }

        public void DisplayDisasm(List<byte> mem)
        {
            this.mem = mem;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < mem.Count; i += 2)
            {
                ushort opcode = (ushort)((mem[i + 1] << 8) | mem[i]);
                sb.AppendLine($"{i:X6}: 0x{opcode:X4}  {Disassemble(opcode)}");
            }

            //for (int i = mem.Count; i < Cpu.FLASH_SIZE; i += 2)
            //{
            //    sb.AppendLine($"{i:X6}: 0xFFFF  ???");
            //}

            disasmTextBox.Text = sb.ToString();
            SetProgCntr(cpu.state.PC);
        }

        public void SetProgCntr(int pc)
        {
            if (mem != null && pc >= mem.Count)
            {
                pc = 0;
            }
            pc /= 2;

            // Сброс цвета
            disasmTextBox.SelectAll();
            disasmTextBox.SelectionBackColor = disasmTextBox.BackColor;
            disasmTextBox.SelectionColor = disasmTextBox.ForeColor;

            // Выделение
            int start = disasmTextBox.GetFirstCharIndexFromLine(pc);
            if (pc < disasmTextBox.Lines.Length)
            {
                int length = disasmTextBox.Lines[pc].Length;

                disasmTextBox.SelectionStart = start;
                disasmTextBox.SelectionLength = length;
                disasmTextBox.SelectionBackColor = Color.Lime;
                disasmTextBox.SelectionColor = Color.Black;
            }
        }

        private string Disassemble(ushort opcode)
        {
            return disasm.DisasmInstruction(opcode);
        }
    }
}
