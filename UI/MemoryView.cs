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
        public MemoryView()
        {
            InitializeComponent();
        }

        public void DisplayFirm(List<byte> firm)
        {
            hexViewer.DisplayFirm(firm);
            SetProgCntr(0);
        }

        public void SetProgCntr(int pc)
        {
            hexViewer.SetProgCntr(pc);
        }
    }
}
