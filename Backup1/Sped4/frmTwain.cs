using Common.Enumerations;
using LVS;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TwainLib;

namespace Sped4
{
    public class frmTwain : frmTEMPLATE, IMessageFilter
    {
        public Globals._GL_USER GL_User;
        public Globals._GL_SYSTEM GLSystem;

        public frmAuftragView _frmAV;
        public ctrAufträge _ctrAuftrag;
        public frmPrintRepViewer _frmPrintRepViewer;


        private System.Windows.Forms.MdiClient mdiClient1;
        private IContainer components;

        public decimal decAuftragID = 0;    //ID Table Auftrag
        public decimal decAuftragsNr = 0;   //Auftragsnummer
        public decimal decLEingangID = 0;
        public decimal decLAusgangID = 0;
        public decimal decAuftragPosTableID = 0;
        public string strImageArt;      // Auftragsnummer

        public Int32 Count = 0;
        public Int32 iAnzahl = 0;
        private Panel panMenue;
        private Button btnScanSelect;
        private ListBox listBox1;
        private Label label2;
        private Button btnClose;
        private Button btnScan;
        public Int32 FehlerCount = 0;

        private bool msgfilter;
        private Twain tw;
        private int picnumber = 0;


        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mdiClient1 = new System.Windows.Forms.MdiClient();
            this.panMenue = new System.Windows.Forms.Panel();
            this.btnScanSelect = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.panMenue.SuspendLayout();
            this.SuspendLayout();
            // 
            // mdiClient1
            // 
            this.mdiClient1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdiClient1.Location = new System.Drawing.Point(192, 0);
            this.mdiClient1.Name = "mdiClient1";
            this.mdiClient1.Size = new System.Drawing.Size(577, 384);
            this.mdiClient1.TabIndex = 0;
            this.mdiClient1.Text = "Scanvorgang: Auftrag";
            // 
            // panMenue
            // 
            this.panMenue.Controls.Add(this.btnScanSelect);
            this.panMenue.Controls.Add(this.listBox1);
            this.panMenue.Controls.Add(this.label2);
            this.panMenue.Controls.Add(this.btnClose);
            this.panMenue.Controls.Add(this.btnScan);
            this.panMenue.Dock = System.Windows.Forms.DockStyle.Left;
            this.panMenue.Location = new System.Drawing.Point(0, 0);
            this.panMenue.Name = "panMenue";
            this.panMenue.Size = new System.Drawing.Size(192, 384);
            this.panMenue.TabIndex = 8;
            // 
            // btnScanSelect
            // 
            this.btnScanSelect.Location = new System.Drawing.Point(15, 126);
            this.btnScanSelect.Name = "btnScanSelect";
            this.btnScanSelect.Size = new System.Drawing.Size(168, 39);
            this.btnScanSelect.TabIndex = 9;
            this.btnScanSelect.Text = "Scanner wählen";
            this.btnScanSelect.UseVisualStyleBackColor = true;
            this.btnScanSelect.Click += new System.EventHandler(this.btnScanSelect_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(15, 25);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(167, 95);
            this.listBox1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Dokumentenart:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(15, 218);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(165, 67);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "ENDE";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(15, 171);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(165, 41);
            this.btnScan.TabIndex = 3;
            this.btnScan.Text = "SCAN";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // frmTwain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(769, 384);
            this.Controls.Add(this.panMenue);
            this.Controls.Add(this.mdiClient1);
            this.IsMdiContainer = true;
            this.Name = "frmTwain";
            this.Text = "Scanvorgang Auftrag";
            this.Load += new System.EventHandler(this.frmTwain_Load);
            this.panMenue.ResumeLayout(false);
            this.panMenue.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        /*************************************************************************
         *
         *                          Methoden
         * 
         * ***********************************************************************/

        //public MainFrame(string strAuftrgID, string _ImageArt)
        public frmTwain()
        {

            InitializeComponent();
            tw = new Twain();
            tw.Init(this.Handle);
        }

        private void frmTwain_Load(object sender, EventArgs e)
        {
            //Check mindestens einer der vier IDs muss >0
            bool bInitCheck = false;

            if (decAuftragID > 0)
            {
                if (decAuftragsNr == 0)
                {
                    decAuftragsNr = clsAuftrag.GetANrByID(GL_User, decAuftragID);
                }
                bInitCheck = true;
            }
            if (decAuftragsNr > 0)
            {
                if (decAuftragID == 0)
                {
                    decAuftragID = clsAuftrag.GetAuftragIDbyANr(GL_User, decAuftragsNr);
                }
                bInitCheck = true;
            }
            if (decLAusgangID > 0)
            {
                bInitCheck = true;
            }
            if (decLEingangID > 0)
            {
                bInitCheck = true;
            }

            //Listbox mit ImageArt füllen


            if (bInitCheck == false)
            {
                clsMessages.Allgemein_FalscheUeberabeParameter("Scann");
                this.Close();
            }
            this.Text = this.Text + "[" + decAuftragsNr + "]";
        }


        private void btnScanSelect_Click(object sender, EventArgs e)
        {
            tw.Select();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this._ctrAuftrag != null)
            {
                this._ctrAuftrag.InitDGV();
            }
            this.Close();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (!msgfilter)
            {
                this.Enabled = false;
                msgfilter = true;
                Application.AddMessageFilter(this);
            }
            tw.Acquire();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                tw.Finish();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            TwainCommand cmd = tw.PassMessage(ref m);
            if (cmd == TwainCommand.Not)
                return false;

            switch (cmd)
            {
                case TwainCommand.CloseRequest:
                    {
                        EndingScan();
                        tw.CloseSrc();
                        break;
                    }
                case TwainCommand.CloseOk:
                    {
                        EndingScan();
                        tw.CloseSrc();
                        break;
                    }
                case TwainCommand.DeviceEvent:
                    {
                        break;
                    }
                case TwainCommand.TransferReady:
                    {
                        ArrayList pics = tw.TransferPictures();
                        iAnzahl = pics.Count;
                        // ArrayList pics1 = tw.TransferPictures();
                        EndingScan();
                        tw.CloseSrc();
                        picnumber++;
                        for (int i = 0; i < pics.Count; i++)
                        {

                            Bitmap bmp = GetBitMap(pics, i);
                            //Bitmap crBMP = CropBMP(bmp);
                            clsDocScan Scan = new clsDocScan();
                            Scan._GL_User = GL_User;
                            Scan._GLSystem = this.GLSystem;
                            Scan.AuftragPosTableID = decAuftragPosTableID;
                            Scan.LEingangTableID = decLAusgangID;
                            Scan.LAusgangTableID = decLAusgangID;

                            Scan.m_dec_AuftragID = decAuftragID;
                            Scan.m_dec_LAusgangID = decLAusgangID;
                            Scan.m_dec_LEingangID = decLEingangID;
                            Scan.m_dec_AuftragPosTableID = decAuftragPosTableID;
                            Scan.m_str_ImageArt = enumDokumentenArt.AuftragScan.ToString();

                            if (Scan.CheckAuftragScanIsIn())
                            {
                                Count = Scan.GetMaxPicNumByAuftrag();
                            }
                            Count++;
                            Scan.m_i_picnum = Count;
                            Scan.AuftragImageIn = bmp;
                            //Scan.bImage = Scan.imageToByteArray(bmp);
                            Scan.bImage = clsDocScan.imageToByteArray(bmp);
                            Scan.AddDocScan();
                            Scan.SaveScanDocToHDD();

                            this._frmPrintRepViewer = new frmPrintRepViewer();
                            this._frmPrintRepViewer.GL_System = this.GLSystem;
                            this._frmPrintRepViewer.iPrintCount = 1;
                            this._frmPrintRepViewer.DokumentenArt = "DocAuftragScan";
                            this._frmPrintRepViewer.DocScan = Scan.Copy();
                            this._frmPrintRepViewer.InitFrm();
                            this._frmPrintRepViewer.InitReportView();
                            this._frmPrintRepViewer.PrintDirectToPDF(this._frmPrintRepViewer.rViewer.Name, @"" + Scan.m_str_SavePathAndFilenamePDF);

                            // IntPtr img = (IntPtr)pics[i];
                            IntPtr img = BitmapToIntPtr(bmp);
                            PicForm newpic = new PicForm(decAuftragsNr.ToString(), picnumber.ToString());// Aufruf PicForm hier könnte ID übergeben werden
                            newpic.pbScanDoc.Image = Scan.AuftragImageOut;
                            newpic.MdiParent = this;
                            int picnum = i + 1;
                            //newpic.Text = "ScanPass" + picnumber.ToString() + "_Pic" + picnum.ToString();

                            newpic.Text = decAuftragsNr.ToString() + "_Nr._" + Count.ToString() + " - Dokument wurde in der Datenbank gespeichert ";
                            newpic.Show();

                        }
                        //Form soll direkt nach dem Scan geschlossen werden
                        EndingScan();
                        tw.CloseSrc();

                        //Refresh FrmAuftragView
                        if (_frmAV != null)
                        {
                            decimal decTmp = 0.0M;
                            if (Decimal.TryParse(decAuftragsNr.ToString(), out decTmp))
                            {
                                _frmAV.InitDGV();
                            }
                        }

                        MessageBox.Show("Scan-Vorgang erfolgreich abgeschlossen." + Environment.NewLine +
                                        "Es wurden " + iAnzahl.ToString() + " Dokumente gescannt."
                                        , "Information"
                                        , MessageBoxButtons.OK, MessageBoxIcon.Information);

                        break;
                    }
            }

            if (FehlerCount > 1)
            {
                /// MessageBox.Show("Es ist ein Fehler beim Scan-Vorgang aufgetreten" + Environment.NewLine+
                //                 "Der Scan-Vorgang wird abgebrochen!", "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                EndingScan();
                tw.CloseSrc();
                this.Close();
            }
            FehlerCount++;
            return true;
        }

        private void EndingScan()
        {
            if (msgfilter)
            {

                Application.RemoveMessageFilter(this);
                msgfilter = false;
                this.Enabled = true;
                this.Activate();
            }
        }

        public IntPtr BitmapToIntPtr(Bitmap bmp)
        {
            IntPtr ptr = IntPtr.Zero;
            System.Drawing.Imaging.BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            ptr = bd.Scan0;

            return ptr;
        }

        // ausm Netz
        public Bitmap GetBitMap(ArrayList alPics, Int32 iPic)
        {
            //int i = 0; // das habe ich der Methode hinzugefügt
            Rectangle rPic = new Rectangle(0, 0, 0, 0);
            IntPtr ipNonLockedBitMap = (IntPtr)alPics[iPic];
            IntPtr ipBitMap = GlobalLock(ipNonLockedBitMap); // TODO: Grab GlobalLock & GlobalFree from PicForm.cs    
            BITMAPINFOHEADER bmihInfo = new BITMAPINFOHEADER(); // TODO: Grab BITMAPINFOHEADER from PicForm.cs
            Marshal.PtrToStructure(ipBitMap, bmihInfo);
            rPic.X = rPic.Y = 0;
            rPic.Width = bmihInfo.biWidth;
            rPic.Height = bmihInfo.biHeight;

            if (bmihInfo.biSizeImage == 0)
                bmihInfo.biSizeImage = ((((bmihInfo.biWidth * bmihInfo.biBitCount) + 31) & ~31) >> 3) * bmihInfo.biHeight;

            int p = bmihInfo.biClrUsed;
            if ((p == 0) && (bmihInfo.biBitCount <= 8))
                p = 1 << bmihInfo.biBitCount;
            p = (p * 4) + bmihInfo.biSize + (int)ipBitMap;

            IntPtr ipPixel = (IntPtr)p;

            Bitmap bmp = new Bitmap(rPic.Width, rPic.Height);
            Graphics g = Graphics.FromImage((Image)bmp);
            IntPtr hdc = g.GetHdc();

            SetDIBitsToDevice(hdc, 0, 0, bmp.Width, bmp.Height, 0, 0, 0, bmp.Height, ipPixel, ipBitMap, 0);
            g.ReleaseHdc(hdc);
            return bmp;
        }


        private Bitmap CropBMP(Bitmap myBMP)
        {
            Int32 iWidth = 765;
            Int32 iHeight = 1085;


            //using (var absentRectangleImage = Bitmap.FromFile(strSourcePath))
            using (var absentRectangleImage = myBMP)
            {
                using (var currentTile = new Bitmap(iWidth, iHeight))
                {
                    currentTile.SetResolution(absentRectangleImage.HorizontalResolution, absentRectangleImage.VerticalResolution);
                    //currentTile.SetResolution(iHeight, iWidth);
                    using (var currentTileGraphics = Graphics.FromImage(currentTile))
                    {
                        currentTileGraphics.Clear(Color.Transparent);
                        var absentRectangleArea = new Rectangle(0, 0, iWidth, iHeight);
                        // currentTileGraphics.DrawImage( absentRectangleImage, 0, 0, absentRectangleArea, GraphicsUnit.Pixel );
                        currentTileGraphics.DrawImage(absentRectangleImage, 0, 0, absentRectangleArea, GraphicsUnit.Pixel);

                    }
                    //currentTile.Save(@"D:\test.bmp");
                    //AuftragImageIn = (Image)currentTile;
                    return currentTile;
                }
            }
        }



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



    } // class MainFrame

} // namespace TwainGui
