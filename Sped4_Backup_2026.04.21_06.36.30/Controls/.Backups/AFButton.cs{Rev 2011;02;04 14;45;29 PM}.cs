using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Sped4.Controls
{
    public partial class AFButton : UserControl
    {
        public AFButton()
        {
            InitializeComponent();
            myBorderSize = 2;
            myColorActivate = Color.Red;
            myColorBase = Color.Blue;
            myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Abgerundet = false;
            myText = "AFButton";
            Active = false;
        }

        private Color _ColorActivate; 
        private Color _ColorBase; 
        private int _BordreSize; 
        private Font _Font; 
        private string _Text; 
        private bool _Abgerundet;
        private Image _Pic;
        private Boolean _Active;
        private Boolean _MouseOn = false;
        
        public new event ClickEventHandler Click; 
        public delegate void ClickEventHandler(); 
        
        [Category("Werte"), Description("Ein Quadratischer Button lässt sich so als Kreis darstellen"), ToolboxItem(true)] 
        public bool Abgerundet 
        { 
            get { return _Abgerundet; } 
            set { _Abgerundet = value; } 
        }

        public Image Pic
        {
            get { return _Pic; }
            set { _Pic = value; }
        }

        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        } 
        
        [Category("Werte"), Description("Text auf Button (Umbruch mit vbcrlf)"), ToolboxItem(true)] 
        public string myText { 
            get { return _Text; } 
            set { _Text = value; } 
        } 
        
        [Category("Werte"), Description("Font (dynamisch mit new font(Family,Size,Style)")] 
        public Font myFontStyle { 
            get { return _Font; } 
            set { _Font = value; } 
        } 
        
        [Category("Werte"), Description("Rahmendicke")] 
        public int myBorderSize { 
            get { return _BordreSize; } 
            set { _BordreSize = value; } 
        } 
        
        [Category("Werte"), Description("Startfarbe")] 
        public Color myColorActivate { 
            get { return _ColorActivate; } 
            set { _ColorActivate = value; } 
        } 
        
        [Category("Werte"), Description("Zeilfarbe")] 
        public Color myColorBase { 
            get { return _ColorBase; } 
            set { _ColorBase = value; } 
        } 
        
        private void Picture_Click(object sender, System.EventArgs e) 
        { 
            if (Click != null) { 
                Click(); 
            } 
        } 
        
        private void Picture_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) 
        {  
            Picture.BorderStyle = BorderStyle.Fixed3D;
        } 
        
        private void Picture_MouseLeave(object sender, System.EventArgs e) 
        { 
            Picture.BorderStyle = BorderStyle.FixedSingle;
            _MouseOn = false;
            this.Refresh();
        } 
        
        private void Picture_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) 
        { 
            Picture.BorderStyle = BorderStyle.FixedSingle;            
        } 
        
        public void abrunden(AFButton was, int x, int y, int width, int height, int radius) 
        { 
            
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath(); 
            
            gp.AddLine(x + radius, y, x + width - radius, y); 
            gp.AddArc(x + width - radius, y, radius, radius, 270, 90); 
            
            gp.AddLine(x + width, y + radius, x + width, y + height - radius); 
            gp.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90); 
            
            gp.AddLine(x + width - radius, y + height, x + radius, y + height); 
            gp.AddArc(x, y + height - radius, radius, radius, 90, 90); 
            
            gp.AddLine(x, y + height - radius, x, y + radius); 
            gp.AddArc(x, y, radius, radius, 180, 90); 
            
            gp.CloseFigure(); 
            
            was.Region = new System.Drawing.Region(gp); 
            gp.Dispose(); 
        } 


        private void AFButton_Resize(object sender, EventArgs e)
        {
            this.Refresh(); 
        }

        private void AFButton_Paint(object sender, PaintEventArgs e)
        {
            Picture.Left = myBorderSize;
            Picture.Top = myBorderSize;
            Picture.Width = this.Width - myBorderSize * 2;
            Picture.Height = this.Height - myBorderSize * 2; 
        }

        private void Picture_Paint(object sender, PaintEventArgs e)
        {
            Color ColorTop1;
            Color ColorTop2;
            Color ColorBot1;
            Color ColorBot2;

            if (Active)
            {
                ColorTop1 = Color.FromArgb(100, myColorActivate);
                ColorTop2 = Color.FromArgb(150, myColorActivate);
                ColorBot1 = Color.FromArgb(255, myColorActivate);
                ColorBot2 = Color.FromArgb(150, myColorActivate);
            }
            else
            {
                ColorTop1 = Color.FromArgb(100, myColorBase);
                ColorTop2 = Color.FromArgb(150, myColorBase);
                ColorBot1 = Color.FromArgb(255, myColorBase);
                ColorBot2 = Color.FromArgb(150, myColorBase);
            }            

            
            Rectangle oRectPic = new Rectangle(10, Convert.ToInt32((double)(Picture.Height - 24) / 2), 24, 24);
            Rectangle oRect = new Rectangle(oRectPic.Left + oRectPic.Width + 10, 0, Picture.Width, Picture.Height);
            Rectangle oRectTop = new Rectangle(0, 0, Picture.Width, Convert.ToInt32((double)Picture.Height / 100 * 25));
            Rectangle oRectBot = new Rectangle(0, Convert.ToInt32((double)(Picture.Height) / 100 * 25), Picture.Width, Convert.ToInt32((double)(Picture.Height) / 100 * 75));

            LinearGradientBrush oBrushTop = new LinearGradientBrush(oRectTop, ColorTop1, ColorTop2, LinearGradientMode.Vertical);
            LinearGradientBrush oBrushBot = new LinearGradientBrush(oRectBot, ColorBot1, ColorBot2, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(oBrushTop, oRectTop);
            e.Graphics.FillRectangle(oBrushBot, oRectBot);

            if (_MouseOn == true)
            {
                GraphicsPath gP = new GraphicsPath();
                gP.AddEllipse(0, 5, this.Picture.Width, this.Picture.Height);
                PathGradientBrush PathBrush = new PathGradientBrush(gP);
                PathBrush.CenterPoint = new Point(this.Picture.Width / 2, this.Picture.Height);
                PathBrush.CenterColor = Color.FromArgb(200, Color.White);
                PathBrush.SurroundColors = new Color[] { Color.Transparent };
                e.Graphics.FillRectangle(PathBrush, this.Picture.Bounds);
            }

            StringFormat DrawFormat = new StringFormat();
            //DrawFormat.Alignment = StringAlignment.Center;
            DrawFormat.Alignment = StringAlignment.Near;
            DrawFormat.LineAlignment = StringAlignment.Center;
            DrawFormat.FormatFlags = StringFormatFlags.LineLimit;            
            oRect.X += 1;
            oRect.Y += 1;
            e.Graphics.DrawString(myText, myFontStyle, Brushes.Black, oRect, DrawFormat);
            oRect.X -= 1;
            oRect.Y -= 1;
            e.Graphics.DrawString(myText, myFontStyle, Brushes.White, oRect, DrawFormat);
            if (_Pic != null)
            {
                e.Graphics.DrawImage(_Pic, oRectPic);
            }
            
            if (Abgerundet == true)
            {
                abrunden(this, 0, 0, this.Width, this.Height, this.Height);
            }
        }

        private void Picture_MouseEnter(object sender, EventArgs e)
        {
            _MouseOn = true;
            this.Refresh(); 
        }
    }
}
