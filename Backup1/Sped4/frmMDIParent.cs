using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using Sped4;
using Sped4.Classes;

namespace Sped4
{
    public partial class frmMDIParent : Form
    {

        public Globals._GL_USER GL_User = new Globals._GL_USER();
        internal frmLogin _frmLogin;
        public BackgroundWorker backgroundWorker;
        public delegate void AppendNumberDelegate(Decimal number);
        public delegate void ThreadCtrInvokeEventHandler();

        internal Int32 iLast2Step1 = 0;
        internal Int32 iLast2Step2 = 0;
        internal Int32 iItemGesamt = 0;
        internal bool boStatusWork = false;


        public frmMDIParent()
        {
            InitializeComponent();
        }

        #region Win32

        private const int WM_PAINT = 0x000F;
        private const int WM_ERASEBKGND = 0x0014;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_THEMECHANGED = 0x031A;
        private const int WM_NCCALCSIZE = 0x0083;
        private const int WM_SIZE = 0x0005;
        private const int WM_PRINTCLIENT = 0x0318;

        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOREDRAW = 0x0008;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_FRAMECHANGED = 0x0020;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint SWP_HIDEWINDOW = 0x0080;
        private const uint SWP_NOCOPYBITS = 0x0100;
        private const uint SWP_NOOWNERZORDER = 0x0200;
        private const uint SWP_NOSENDCHANGING = 0x0400;

        private const int WS_BORDER = 0x00800000;
        private const int WS_EX_CLIENTEDGE = 0x00000200;
        private const int WS_DISABLED = 0x08000000;

        private const int GWL_STYLE = -16;
        private const int GWL_EXSTYLE = -20;

        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;
        private const int SB_CTL = 2;
        private const int SB_BOTH = 3;


        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(Rectangle rect)
            {
                this.left = rect.Left;
                this.top = rect.Top;
                this.right = rect.Right;
                this.bottom = rect.Bottom;
            }

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public int fErase;
            public RECT rcPaint;
            public int fRestore;
            public int fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NCCALCSIZE_PARAMS
        {
            public RECT rgrc0, rgrc1, rgrc2;
            public IntPtr lppos;
        }


        [DllImport("user32.dll")]
        private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);

        [DllImport("user32.dll")]
        private static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT paintStruct);

        [DllImport("user32.dll")]
        private static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT paintStruct);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowLong(IntPtr hWnd, int Index);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowLong(IntPtr hWnd, int Index, int Value);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        #endregion // Win32

        #region am Anfang mit erzeugt, kann man einiges von gebrauchen
        
        private int childFormNumber = 0;

        

/**       private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new ctrADRList();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }
**/
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #endregion

        public MdiClient MyClientArea;

        private void frmMDIParent_Load(object sender, EventArgs e)
        {
            this.ctrMenu._frmMDIParen = this;
            this.ctrMenu.Sped4Close +=new ctrMenu.Sped4CloseEventHandler(Sped4Close);
            this.ctrMenu.SetGLUser += new ctrMenu.AuthUserRefreshEventHandler(ctrMenu_SetGLUser);
            //this.ctrMenu.ProzessBarStatusChangeMenu += new ctrMenu.StatusProzessChangeMenuEventHandler(ProzessBarStatusChangeOnFrm);
          
            this.Visible = false;
            MdiClient ctlMDI = default(MdiClient);
            foreach (Control ctl in this.Controls) 
            { 
                try 
                { 
                    ctlMDI = (MdiClient)ctl;
                    MyClientArea = ctlMDI;
                    ctlMDI.BackColor = Sped4.Properties.Settings.Default.BackColor;
                } 
                catch 
                { 
                } 
            }
            //TextArea.Width = this.Width - 250;
            ResetStatusBar();
            Int32 style = GetWindowLong(MyClientArea.Handle, GWL_STYLE);
            Int32 exStyle = GetWindowLong(MyClientArea.Handle, GWL_EXSTYLE);
            exStyle &= ~WS_EX_CLIENTEDGE;
            style |= WS_BORDER;
            SetWindowLong(MyClientArea.Handle, GWL_STYLE, style);
            SetWindowLong(MyClientArea.Handle, GWL_EXSTYLE, exStyle);
            UpdateStyles();
            SetWindowPos(MyClientArea.Handle, IntPtr.Zero, 0, 0, 0, 0,
                SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER |
                SWP_NOOWNERZORDER | SWP_FRAMECHANGED);
            if (Functions.init_con() == true) 
            {
                //Check LOGIN
                if (Functions.frm_IsFormAlreadyOpen(typeof(frmLogin)) != null)
                {
                    Functions.frm_FormClose(typeof(frmLogin));
                }
                _frmLogin = new frmLogin();
                _frmLogin.Sped4Close += new frmLogin.frmLoginEventHandler(Sped4Close);
                _frmLogin.SetGLUser += new frmLogin.frmLoginGLUserEventHandler(SetGLUserInFrm);
                _frmLogin.MainFormShow += new frmLogin.MainFrmEventHandler(MainFormShow);
                _frmLogin.StartPosition = FormStartPosition.CenterScreen;
                this.ctrMenu._frmLogin = this._frmLogin;
                _frmLogin._ctrMenu = this.ctrMenu;
            } 
            else 
            { 
                string strText ="Verbindung zum Server / zur Datenbank konnte nicht hergestellt werden. Die Anwendung wird geschlossen!";
                MessageBox.Show(strText, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                this.Close();
            } 
        }
        //
        private void ctrMenu_SetGLUser(Globals._GL_USER _GL_User)
        {
          GL_User = _GL_User;
        }
        //
        //
        private void MainFormShow()
        {
          this.Visible = true;

        }
        //
        //
        private void Sped4Close()
        {
          this.Close();
        }
        //
        //
        public void SetGLUserInFrm(Globals._GL_USER _GL_User)
        {
          GL_User = _GL_User;
          clsUserBerechtigungen userber = new clsUserBerechtigungen();
          userber.BenutzerID = GL_User.User_ID;
          userber.GetUserBerechtigungLogin(ref GL_User);
          ctrMenu.GL_User = GL_User;
        }
        //
        //
        private void toolbtnShowHideMenu_Click(object sender, EventArgs e)
        {
            if (ctrMenu.Visible == true)
            {
                ctrMenu.Visible = false;
                SplitterMenu.Visible = false;
                toolbtnShowHideMenu.Image = Sped4.Properties.Resources.layout_left;
            }
            else
            {
                ctrMenu.Visible = true;
                SplitterMenu.Visible = true;
                toolbtnShowHideMenu.Image = Sped4.Properties.Resources.layout;
            } 
        }
        //****************************************************************************************
        //                        Kundenlogo rechts oben
        //
        private void toolStripContainer1_TopToolStripPanel_Paint(object sender, PaintEventArgs e)
        {
            // Heisiep Logo oben links
            /***
             Rectangle oRectPic = new Rectangle(toolStripContainer1.Width - 100,
                                                toolStripContainer1.Height - 25,
                                                Sped4.Properties.Resources.heisiep.Width,
                                                Sped4.Properties.Resources.heisiep.Height);
            e.Graphics.DrawImage(Sped4.Properties.Resources.heisiep, oRectPic);
            ***/ 
            
        }

        private void afStatusStrip1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle oRectPic = new Rectangle(statusStrip1.Width - 100,
                                                statusStrip1.Height - 20,
                                                Sped4.Properties.Resources.comtec.Width,
                                                Sped4.Properties.Resources.comtec.Height);
            e.Graphics.DrawImage(Sped4.Properties.Resources.comtec, oRectPic);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void frmMDIParent_Paint(object sender, PaintEventArgs e)
        {
            MyClientArea.BackColor = Sped4.Properties.Settings.Default.BackColor;
        }
        
        /*************************************************************************************************
         *                        Hauptmenü - AFMenüleiste
         * 
         * ***********************************************************************************************/
        //
        //
        /************************************ SYSTEM ************************************/
        //
        //------------------------- USER Verwaltung -----------------------------
        //
        private void userverwaltungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Functions.frm_IsFormAlreadyOpen(typeof(frmUserverwaltung)) != null)
          {
              Functions.frm_FormClose(typeof(frmUserverwaltung));
          }
          frmUserverwaltung uver = new frmUserverwaltung();
          uver.GL_User = GL_User;
          uver.StartPosition = FormStartPosition.CenterScreen;
          uver.Show();
          uver.BringToFront();
        }
        //
        //---------------- Logbuch -------------------
        //
        private void logbuchToolStripMenuItem_Click(object sender, EventArgs e)
        {
          this.ctrMenu.OpenLogbuch();
        }
        //
        //------------ Logout - Beenden
        //
        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoLogout();
        }
        //
        private void DoLogout()
        {
            //Logout
            clsLogin lg = new clsLogin();
            lg.BenutzerID = GL_User.User_ID;
            lg.Logout();
            Application.Exit();
        }     



        /***********************************  STAMMDATEN **************************/
        //
        //
        //ADR
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
          this.ctrMenu.OpenStammdatenFromMDI("ADR");
        }
        //Fahrzeuge
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
          this.ctrMenu.OpenStammdatenFromMDI("Fahrzeuge");
        }
        //GArten
        private void güterartenToolStripMenuItem_Click(object sender, EventArgs e)
        {
          this.ctrMenu.OpenStammdatenFromMDI("GArt");
        }
        //Personal
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
          this.ctrMenu.OpenStammdatenFromMDI("Personal");
        }
        //Relation
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
          this.ctrMenu.OpenStammdatenFromMDI("Relation");
        }
        



        //
        //
        /*******************************   LAGER    **************************/
        //
        //---------- Einlagerung --------------
        //
        private void einlagerungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Functions.frm_IsFormAlreadyOpen(typeof(frmEinlagerung)) != null)
          {
              Functions.frm_FormClose(typeof(frmEinlagerung));
          }

          frmEinlagerung lager = new frmEinlagerung();
          lager.GL_User = GL_User;
          lager.StartPosition = FormStartPosition.CenterScreen;
          lager.Show();
          lager.BringToFront();
        }
        //
        //----------------  Auslagerung ----------------
        //
        private void auslagerungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Functions.frm_IsFormAlreadyOpen(typeof(frmAuslagerung)) != null)
          {
              Functions.frm_FormClose(typeof(frmAuslagerung));
          }

          frmAuslagerung lager = new frmAuslagerung();
          lager.GL_User = GL_User;
          lager.StartPosition = FormStartPosition.CenterScreen;
          lager.Show();
          lager.BringToFront();
        }
        //
        //
        /************************************** FIBU EXPORT *****************************/
        //
        //
        //
        private void fIBUExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //Abfrage Berechtigung

            clsMessages.Allgemein_FunctionOnConstruction();

          /**** Baustelle mr
          if (Functions.IsFormAlreadyOpen(typeof(frmFibuExport)) != null)
          {
            Functions.FormClose(typeof(frmFibuExport));
          }
          frmFibuExport export = new frmFibuExport();
          export.StartPosition = FormStartPosition.CenterScreen;
          export.Show();
          export.BringToFront();
           * ***/
        }
        //
        //------------- Dispoplan integrieren  ------------
        //
        private void tsbtnDispoPlan_Click(object sender, EventArgs e)
        {
          ctrMenu.CloseDispoKaledner();
          ctrMenu.DispoKalenderOpenSplitter();
        }
        //
        //------------- Dispoplan eigenes Fenster ----------------
        //
        private void tsbtnDispoKalenderInWindow_Click(object sender, EventArgs e)
        {
          ctrMenu.CloseDispoKaledner();
          ctrMenu.DispoKalenderOpen();
        }
        //
        //------------ splitter Resize -------------------
        //
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
          ctrMenu.SplitterResize();
          
        }
        //
        //------------------- Logout ------------------------
        //
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DoLogout();
        }
        //
        //---------------- Open Hilfe ---------------------
        //
        private void hilfeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clsMessages.Allgemein_FunctionOnConstruction();#
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "Handbuch/HandbuchSped4.pdf");
                p.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //
        //
        /******************************************************************************************+
         *                          
         * ****************************************************************************************/

        /**************************************************************************+
         * 
         *                              Status Bar
         * 
         * ************************************************************************/
        //
        //public void InitStatusBar(ref DataTable dt)
        public void InitStatusBar(Int32 iRowCount)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate()
                                                                        {
                                                                            InitStatusBar(iRowCount);
                                                                        }
                                                                    )
                                 );
                return;
            }
            iLast2Step1 = iRowCount / 10;
            iLast2Step2 = iRowCount / 10;
            Int32 iMax = (iRowCount * 2)+iLast2Step1 + iLast2Step2;
            iItemGesamt = iRowCount;
            //Gesamtanzahl der Datensätze wird übergeben
            this.toolStripProgressBar1.Maximum = iMax;
            this.toolStripProgressBar1.Value = 0;
            this.boStatusWork = true;
        }
        //
        public void ResetStatusBar()
        {
            this.toolStripProgressBar1.Maximum = 1;
            this.toolStripProgressBar1.Value = 0;
            //this.toolStripStatusLabel1.Text = string.Empty;
            iItemGesamt = 0;
            iLast2Step1 = 0;
            iLast2Step2 = 0;
        }        
        //
        //------------ STATUSBAR - WORK -------------------
        //
        public void StatusBarWork(Int32 iVal)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate()
                                                                        {
                                                                            StatusBarWork(iVal);
                                                                        }
                                                                    )
                                 );
                return;
            }
            //aktuelle Row wird übergeben
            if (boStatusWork)
            {
                if (this.toolStripProgressBar1.Value <= this.toolStripProgressBar1.Maximum)
                {
                    decimal decPro = 0;
                    if (this.toolStripProgressBar1.Value > 0)
                    {
                        decPro = (decimal)this.toolStripProgressBar1.Value * 100 / ((decimal)this.toolStripProgressBar1.Maximum);
                        //iVal = (Int32)decPro;
                    }
                    this.toolStripProgressBar1.Value = this.toolStripProgressBar1.Value + iVal;
                    this.toolStripStatusLabel1.Text = decPro.ToString("0.##").Replace(".", ",") + " %  von " + iItemGesamt + " Datensätze verarbeitet";
                }
                else
                {
                    this.boStatusWork = false;
                }
                if (this.toolStripProgressBar1.Value == this.toolStripProgressBar1.Maximum)
                {
                    this.toolStripStatusLabel1.Text = iItemGesamt + " Datensätze geladen und verarbeitet";
                }
            }
        }



        //Test Button
        private void button1_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenFrmArbeitsbereich();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenFrmMandanten();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenFrmTarifErfassung();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenFrmSchaeden();
        }


    }
}
