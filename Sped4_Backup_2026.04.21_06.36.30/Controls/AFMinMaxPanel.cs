using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4.Controls
{
    public partial class AFMinMaxPanel : Panel
    {
        public AFMinMaxPanel()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            this.label1.Width = (this.Width - 64);
            this.label1.Top = Convert.ToInt32((double)(this.PictureBox1.Height - label1.Height) / 2);
            this.PictureBox1.Left = (this.Width - 26);
            this.PictureBox2.Left = 2;
            this.label1.Left = 30;
            pe.Graphics.DrawLine(new Pen(Color.Black, 1),
                                    (float)(this.label1.Left + this.label1.Width),
                                    (float)(this.PictureBox1.Height) / 2,
                                    (float)(this.PictureBox1.Left) - 5,
                                    (float)(this.PictureBox1.Height) / 2);
        }

        public enum EStatus
        {
            Expanded = 1,
            Collapsed = 2,
        }

        EStatus ExpCol = EStatus.Expanded;
        Int32 TempHeight = 0;

        public EStatus ExpandedCallapsed
        {
            get
            {
                return ExpCol;
            }
            set
            {
                // ExpandCollapse()
                ExpCol = value;
            }
        }

        [Category("Werte")]
        [Description("Bild")]
        public Image myImage
        {
            get
            {
                return this.PictureBox2.Image;
            }
            set
            {
                this.PictureBox2.Image = value;
            }
        }

        [Category("Werte")]
        [Description("Expanded Schriftfarbe")]
        public Color myFontColor
        {
            get
            {
                return this.label1.ForeColor;
            }
            set
            {
                this.label1.ForeColor = value;
            }
        }

        [Category("Werte")]
        [Description("Font (dynamisch mit new font(Family,Size,Style)")]
        public Font myFontStyle
        {
            get
            {
                return label1.Font;
            }
            set
            {
                label1.Font = value;
            }
        }

        [Category("Werte")]
        [Description("Text")]
        public string myText
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        private void PictureBox1_Click(object sender, System.EventArgs e)
        {
            ExpandCollapse();
        }

        private void AFMinMaxPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if ((TempHeight == 0))
            {
                TempHeight = this.Size.Height;
            }
        }

        public void SetExpandCollapse(EStatus myExpandStatus)
        {
            this.ExpandedCallapsed = myExpandStatus;
            this.ExpandCollapse();
        }

        private void ExpandCollapse()
        {

            if ((TempHeight == 0))
            {
                TempHeight = this.Size.Height;
            }

            if ((ExpandedCallapsed == EStatus.Expanded))
            {
                // Collapse
                this.Height = (3 + label1.Height + 10);
                this.PictureBox1.Image = Sped4.Properties.Resources.navigate_down2;
                ExpandedCallapsed = EStatus.Collapsed;
            }
            else
            {
                // Expand
                this.Height = TempHeight;
                this.PictureBox1.Image = Sped4.Properties.Resources.navigate_up2;
                ExpandedCallapsed = EStatus.Expanded;
            }
        }

        private void AFMinMaxPanel_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
