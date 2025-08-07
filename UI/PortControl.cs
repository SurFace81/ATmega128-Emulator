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
        private readonly Image truePic = Properties.Resources.pin_on;
        private readonly Image falsePic = Properties.Resources.pin_off;
        private int _pinCount = 8;

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
                bool newState = s.BackgroundImage != truePic;
                s.BackgroundImage = newState ? truePic : falsePic;

                if (newState)
                {
                    pins |= (byte)(1 << Convert.ToInt32(s.Tag));
                }
                else
                {
                    pins &= (byte)~(1 << Convert.ToInt32(s.Tag));
                }
            }

            InvokeOnPinsStateChanged(pins);
        }

        public void SetPins(byte value)
        {
            pins = value;
            RefreshPins();
        }

        public void RefreshPins()
        {
            for (int i = 0; i < 8; i++)
            {
                bool isSet = ((pins >> i) & 1) == 1;
                pinBoxes[i].BackgroundImage = isSet ? truePic : falsePic;
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
