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
    public partial class DisassemblyView : DockContent
    {
        private Dictionary<int, int> pcToLine = new Dictionary<int, int>();
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
            pcToLine.Clear();
            StringBuilder sb = new StringBuilder();

            int line = 0;
            for (int i = 0; i < mem.Count;)
            {
                ushort opcode1 = (ushort)((mem[i + 1] << 8) | mem[i]);
                int length = GetInstructionLength(opcode1);

                ushort opcode2 = 0;
                if (length == 2)
                    opcode2 = (ushort)((mem[i + 3] << 8) | mem[i + 2]);

                if (length == 1)
                    sb.AppendLine($"{i:X6}: 0x{opcode1:X4} {"".PadRight(6, ' ')} {disasm.DisasmInstruction(opcode1, opcode2)}");
                else if (length == 2)
                    sb.AppendLine($"{i:X6}: 0x{opcode1:X4} 0x{opcode2:X4} {disasm.DisasmInstruction(opcode1, opcode2)}");

                pcToLine[i] = line;

                i += length * 2;
                line += 1;
            }

            //for (int i = mem.Count; i < Cpu.FLASH_SIZE; i += 2)
            //{
            //    sb.AppendLine($"{i:X6}: 0xFFFF  ???");
            //}

            disasmTextBox.Text = sb.ToString();
            SetProgCntr(cpu.state.PC);
        }

        public int GetInstructionLength(ushort opcode)
        {
            if ((opcode & 0xFE0F) == 0x9000) return 2; // LDS
            if ((opcode & 0xFE0F) == 0x9200) return 2; // STS
            if ((opcode & 0xFE0E) == 0x940C) return 2; // JMP
            if ((opcode & 0xFE0E) == 0x940E) return 2; // CALL
            return 1;
        }

        public void SetProgCntr(int pc)
        {
            if (!pcToLine.TryGetValue(pc, out int line))
                return;

            // Сброс цвета
            disasmTextBox.SelectAll();
            disasmTextBox.SelectionBackColor = disasmTextBox.BackColor;
            disasmTextBox.SelectionColor = disasmTextBox.ForeColor;

            // Выделение
            int start = disasmTextBox.GetFirstCharIndexFromLine(line);
            if (line < disasmTextBox.Lines.Length)
            {
                int length = disasmTextBox.Lines[line].Length;

                disasmTextBox.SelectionStart = start;
                disasmTextBox.SelectionLength = length;
                disasmTextBox.SelectionBackColor = Color.Lime;
                disasmTextBox.SelectionColor = Color.Black;
            }
        }
    }
}
