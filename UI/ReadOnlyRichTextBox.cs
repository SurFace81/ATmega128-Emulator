using System.Drawing;
using System.Windows.Forms;

namespace ATmegaSim.UI
{
    public partial class ReadOnlyRichTextBox : RichTextBox
    {
        public ReadOnlyRichTextBox()
        {
            InitializeComponent();
            this.TabStop = false;
            this.Font = new Font("Consolas", 9, FontStyle.Regular);
            this.DetectUrls = false;
            this.ReadOnly = true;
            this.ShortcutsEnabled = false;
            this.WordWrap = false;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SETFOCUS = 0x0007;

            if (m.Msg == WM_SETFOCUS)
                return;

            base.WndProc(ref m);
        }
    }
}
