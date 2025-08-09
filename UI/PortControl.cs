using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ATmegaSim.UI
{
    public partial class PortControl : UserControl
    {
        private readonly Image truePicIn = Properties.Resources.pin_on_in;
        private readonly Image falsePicIn = Properties.Resources.pin_off_in;
        private readonly Image truePicOut = Properties.Resources.pin_on_out;
        private readonly Image falsePicOut = Properties.Resources.pin_off_out;
        private int _pinCount = 8;
        private byte ddrMask = 0;
        private ToolTip tip = new ToolTip();

        [Browsable(false)]
        public byte pins { get; private set; }

        private PictureBox[] pinBoxes;
        public PortControl()
        {
            InitializeComponent();
            pinBoxes = new PictureBox[] { pin0, pin1, pin2, pin3, pin4, pin5, pin6, pin7 };

            ApplyPinCount();
            RefreshPins();
        }

        private void pin_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox s)
            {
                byte offset = (byte)(1 << Convert.ToInt32(s.Tag));
                bool newState = s.BackgroundImage != truePicIn;
                s.BackgroundImage = newState ? truePicIn : falsePicIn;

                if (newState)
                    pins |= offset;
                else
                    pins &= offset;
            }

            InvokeOnPinsStateChanged(pins);
        }

        public void SetPins(byte value, byte ddrMask)
        {
            pins = value;
            this.ddrMask = ddrMask;
            RefreshPins();
        }

        public void RefreshPins()
        {
            for (int i = 0; i < 8; i++)
            {
                bool isSet = ((pins >> i) & 1) == 1;
                pinBoxes[i].BackgroundImage = isSet ? truePicIn : falsePicIn;
            }
        }

        private void ApplyPinCount()
        {
            if (pinBoxes == null) return;

            for (int i = 0; i < pinBoxes.Length; i++)
            {
                pinBoxes[i].Visible = i < _pinCount;
            }

            RefreshPins();
        }

        public event EventHandler<byte> OnPinsStateChanged;
        public void InvokeOnPinsStateChanged(byte state)
        {
            EventHandler<byte> handler = OnPinsStateChanged;
            if (handler != null) handler(this, state);
        }

        [Browsable(true)]
        [Category("Custom")]
        public string PortName
        {
            get => portNameLbl.Text;
            set => portNameLbl.Text = value;
        }

        [Browsable(true)]
        [Category("Custom")]
        public int PinCount
        {
            get => _pinCount;
            set
            {
                _pinCount = Math.Max(0, Math.Min(8, value));
                ApplyPinCount();
            }
        }
    }
}
