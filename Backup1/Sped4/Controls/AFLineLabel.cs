using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4.Controls
{
    public partial class AFLineLabel : UserControl
    {
        public AFLineLabel()
        {
            InitializeComponent();
        }

        Font _Font;
        string _Text;
        Color _FontColor;

        [Category("Werte")]
        [Description("Schriftfarbe")]
        public Color myFontColor
        {
            get
            {
                return _FontColor;
            }
            set
            {
                _FontColor = value;
            }
        }

        [Category("Werte")]
        [Description("Font (dynamisch mit new font(Family,Size,Style)")]
        public Font myFontStyle
        {
            get
            {
                return _Font;
            }
            set
            {
                _Font = value;
            }
        }

        [Category("Werte")]
        [Description("Text")]
        public string myText
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        private void AFLineLabel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            LabelText.Font = myFontStyle;
            LabelText.Text = myText;
            LabelText.ForeColor = myFontColor;
            Pen p = new Pen(Color.Gray, 1);
            e.Graphics.DrawLine(p, (LabelText.Width + 1), Convert.ToInt32((decimal)this.Height / 2), this.Width, Convert.ToInt32((decimal)this.Height / 2));
            this.Height = LabelText.Height;
        }

    }
}
