using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sped4.Controls
{
    public partial class AFToolStrip : ToolStrip
    {
        public AFToolStrip()
        {
            InitializeComponent();
            _ColorFrom = Color.Azure;
            _ColorTo = Color.Blue;
            _Underlined = true;
            _UnderlinedColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        Color _ColorFrom;
        Color _ColorTo;
        bool _Underlined;
        Color _UnderlinedColor;


        [Category("Werte")]
        [Description("Startfarbe")]
        public Color myColorFrom
        {
            get
            {
                return _ColorFrom;
            }
            set
            {
                _ColorFrom = value;
            }
        }

        [Category("Werte")]
        [Description("Zielfarbe")]
        public Color myColorTo
        {
            get
            {
                return _ColorTo;
            }
            set
            {
                _ColorTo = value;
            }
        }

        [Category("Werte")]
        [Description("Unterstrich-Farbe")]
        public Color myUnderlineColor
        {
            get
            {
                return _UnderlinedColor;
            }
            set
            {
                _UnderlinedColor = value;
            }
        }

        [Category("Werte")]
        [Description("Unterstrichen?")]
        public bool myUnderlined
        {
            get
            {
                return _Underlined;
            }
            set
            {
                _Underlined = value;
            }
        }

        private void AFToolStrip_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //Color ColorTop = myColorFrom;
            //Color ColorBot = Color.FromArgb(50, myColorTo);
            //Rectangle oRect = new Rectangle(0, 0, this.Width, this.Height);
            //LinearGradientBrush oBrush = new LinearGradientBrush(oRect, myColorFrom, myColorTo, LinearGradientMode.Vertical);
            //e.Graphics.FillRectangle(oBrush, oRect);

            Color ColorTop1;
            Color ColorTop2;
            Color ColorBot1;
            Color ColorBot2;

            ColorTop1 = Color.FromArgb(100, myColorFrom);
            ColorTop2 = Color.FromArgb(150, myColorFrom);
            ColorBot1 = Color.FromArgb(255, myColorFrom);
            ColorBot2 = Color.FromArgb(150, myColorFrom);

            //Rectangle oRectPic = new Rectangle(10, Convert.ToInt32((double)(Picture.Height - 24) / 2), 24, 24);
            //Rectangle oRect = new Rectangle(oRectPic.Left + oRectPic.Width + 10, 0, Picture.Width, Picture.Height);
            Rectangle oRectTop = new Rectangle(0, 0, this.Width, Convert.ToInt32((double)this.Height / 100 * 25));
            Rectangle oRectBot = new Rectangle(0, Convert.ToInt32((double)(this.Height) / 100 * 25), this.Width, Convert.ToInt32((double)(this.Height) / 100 * 75));

            LinearGradientBrush oBrushTop = new LinearGradientBrush(oRectTop, ColorTop1, ColorTop2, LinearGradientMode.Vertical);
            LinearGradientBrush oBrushBot = new LinearGradientBrush(oRectBot, ColorBot1, ColorBot2, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(oBrushTop, oRectTop);
            e.Graphics.FillRectangle(oBrushBot, oRectBot);
            if ((_Underlined == true))
            {
                Pen p = new Pen(_UnderlinedColor, 1);
                e.Graphics.DrawLine(p, 0, (this.Height - 1), this.Width, (this.Height - 1));
            }
        }

    }
}
