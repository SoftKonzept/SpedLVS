using GdiPlusLib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sped4
{

    public class PicForm : frmTEMPLATE
    {
        private IContainer components;
        public string AuftragID = string.Empty;
        public PictureBox pbScanDoc;
        public string PicNum = string.Empty;
        //public ArrayList _imageListe = new ArrayList():

        //public PicForm( IntPtr dibhandp, string Auftrag, string picnum)
        public PicForm(string Auftrag, string picnum)
        {
            InitializeComponent();

            /***
            SetStyle( ControlStyles.DoubleBuffer, false );
            SetStyle( ControlStyles.AllPaintingInWmPaint, true );
            SetStyle( ControlStyles.Opaque, true );
            SetStyle( ControlStyles.ResizeRedraw, true );
            SetStyle( ControlStyles.UserPaint, true );

            bmprect = new Rectangle( 0, 0, 0, 0 );
            dibhand = dibhandp;
            bmpptr = GlobalLock( dibhand );
            pixptr = GetPixelInfo( bmpptr );
             * ***/

            AuftragID = Auftrag;    //Übergabe Auftragsdaten
            PicNum = picnum;


            //this.AutoScrollMinSize = new System.Drawing.Size( bmprect.Width, bmprect.Height );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dibhand != IntPtr.Zero)
                {
                    GlobalFree(dibhand);
                    dibhand = IntPtr.Zero;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbScanDoc = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbScanDoc)).BeginInit();
            this.SuspendLayout();
            // 
            // pbScanDoc
            // 
            this.pbScanDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbScanDoc.Location = new System.Drawing.Point(0, 0);
            this.pbScanDoc.Name = "pbScanDoc";
            this.pbScanDoc.Size = new System.Drawing.Size(367, 301);
            this.pbScanDoc.TabIndex = 0;
            this.pbScanDoc.TabStop = false;
            // 
            // PicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(256, 256);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(367, 301);
            this.Controls.Add(this.pbScanDoc);
            this.ForeColor = System.Drawing.Color.Black;
            this.MinimumSize = new System.Drawing.Size(80, 80);
            this.Name = "PicForm";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.Text = "PicForm";
            this.Load += new System.EventHandler(this.PicForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbScanDoc)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            /***
            Rectangle	cltrect = ClientRectangle;
            Rectangle	clprect = e.ClipRectangle;
            Point		scrol = AutoScrollPosition;

            Rectangle	realrect = clprect;
            realrect.X -= scrol.X;
            realrect.Y -= scrol.Y;

            SolidBrush brbg = new SolidBrush( Color.Black );
            if( realrect.Right > bmprect.Width )
                {
                Rectangle	bgri = clprect;
                int ovri = bmprect.Width - realrect.X;
                if( ovri > 0 )
                    {
                    bgri.X += ovri;
                    bgri.Width -= ovri;
                    }
                e.Graphics.FillRectangle( brbg, bgri );
                }

            if( realrect.Bottom > bmprect.Height )
                {
                Rectangle	bgbo = clprect;
                int ovbo = bmprect.Height - realrect.Y;
                if( ovbo > 0 )
                    {
                    bgbo.Y += ovbo;
                    bgbo.Height -= ovbo;
                    }
                e.Graphics.FillRectangle( brbg, bgbo );
                }

            realrect.Intersect( bmprect );
            if( ! realrect.IsEmpty )
                {
                int bot = bmprect.Height - realrect.Bottom;
                IntPtr hdc = e.Graphics.GetHdc();
                SetDIBitsToDevice( hdc, clprect.X, clprect.Y, realrect.Width, realrect.Height,
                        realrect.X, bot, 0, bmprect.Height, pixptr, bmpptr, 0 );
                e.Graphics.ReleaseHdc(hdc);
                }
             * ***/
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //this.Menu.MenuItems.Clear();
            base.OnClosing(e);
        }



        private void menuItemClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void menuItemInfo_Click(object sender, System.EventArgs e)
        {
            //InfoForm iform = new InfoForm( bmi );
            //iform.ShowDialog( this );
        }

        private void menuItemSaveAs_Click(object sender, System.EventArgs e)
        {
            Gdip.SaveDIBAs(this.Text, bmpptr, pixptr, AuftragID, PicNum);
        }


        protected IntPtr GetPixelInfo(IntPtr bmpptr)
        {

            bmi = new BITMAPINFOHEADER();
            Marshal.PtrToStructure(bmpptr, bmi);

            bmprect.X = bmprect.Y = 0;
            bmprect.Width = bmi.biWidth;
            bmprect.Height = bmi.biHeight;

            if (bmi.biSizeImage == 0)
                bmi.biSizeImage = ((((bmi.biWidth * bmi.biBitCount) + 31) & ~31) >> 3) * bmi.biHeight;

            int p = bmi.biClrUsed;
            if ((p == 0) && (bmi.biBitCount <= 8))
                p = 1 << bmi.biBitCount;
            p = (p * 4) + bmi.biSize + (int)bmpptr;
            return (IntPtr)p;
        }

        BITMAPINFOHEADER bmi;
        Rectangle bmprect;
        IntPtr dibhand;
        IntPtr bmpptr;
        IntPtr pixptr;

        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern int SetDIBitsToDevice(IntPtr hdc, int xdst, int ydst,
                                                int width, int height, int xsrc, int ysrc, int start, int lines,
                                                IntPtr bitsptr, IntPtr bmiptr, int color);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalFree(IntPtr handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string outstr);

        private void PicForm_Load(object sender, EventArgs e)
        {

        }



    } // class PicForm


    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class BITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
    }

} // namespace TwainGui
