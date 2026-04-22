using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Sped4.Controls
{
    public partial class AFColorLabel : Label
    {
        public AFColorLabel()
        {
            this.AutoSize = false;
            InitializeComponent();
            _ColorFrom = Color.Azure;
            _ColorTo = Color.Blue;
            _Underlined = true;
            _UnderlinedColor = Color.White;
            _Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            _Text = "AFColorLabel";

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        Color _ColorFrom;
        Color _ColorTo;
        bool _Underlined;
        Color _UnderlinedColor;
        Font _Font;
        string _Text;


        [Category("Werte")]
        [Description("Text auf Button (Umbruch mit vbcrlf)")]
        [ToolboxItem(true)]
        public string myText
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
                this.Refresh();
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

        private void AFColorLabel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Color ColorTop = myColorFrom;
            Color ColorBot = Color.FromArgb(50, myColorTo);
            Rectangle oRect = new Rectangle(0, 0, this.Width, this.Height);
            LinearGradientBrush oBrush = new LinearGradientBrush(oRect, myColorFrom, myColorTo, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(oBrush, oRect);
            StringFormat DrawFormat = new StringFormat();
            DrawFormat.Alignment = StringAlignment.Center;
            DrawFormat.LineAlignment = StringAlignment.Center;
            DrawFormat.FormatFlags = StringFormatFlags.LineLimit;
            oRect.X++;
            oRect.Y++;
            e.Graphics.DrawString(myText, myFontStyle, Brushes.Black, oRect, DrawFormat);
            oRect.X--;
            oRect.Y--;
            e.Graphics.DrawString(myText, myFontStyle, Brushes.White, oRect, DrawFormat);
            if ((_Underlined == true))
            {
                Pen p = new Pen(_UnderlinedColor, 2);
                e.Graphics.DrawLine(p, 0, (this.Height - 1), this.Width, (this.Height - 1));
            }
        }


    }
}
