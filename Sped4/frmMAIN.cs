using LVS;
using LVS.InitValueSped4;
using LVS.ViewData;
using Sped4.Controls.AdminCockpit;
using Sped4.Settings;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmMAIN : Form
    {

        public Globals._GL_SYSTEM GL_System = new Globals._GL_SYSTEM();
        public Globals._GL_USER GL_User = new Globals._GL_USER();


        internal SystemSped systemSped = new SystemSped();   //-- neue ab 05.08.2025 wird nach und nach aufgebaut
        internal clsSystem system;

        internal frmLogin _frmLogin;
        internal frmWait _frmWait;
        public BackgroundWorker backgroundWorker;
        private BackgroundWorker bgwCOMInfo;
        //public delegate void AppendNumberDelegate(Decimal number);
        public delegate void ThreadCtrInvokeEventHandler();
        internal bool boStatusWork = false;
        internal bool bStopBGWorker = false;
        //internal bool btsmCommStatusIsActiv = false;
        public frmAdminCockpit AdminCockpit;

        public frmMAIN()
        {
            InitializeComponent();
            //SetDefaultColor();
            PropertySettings.SetBackColor_Default(this);

            //Globals.GLUserSetDefault(ref this.GL_User);
            //Globals.GLSystemSetDefault(ref this.GL_System);

            OpenFrmLogin();
            //this.ctrMenu.Preloader = new clsPreloader();
            if (this.system.IsTestsystem)
            {
                PropertySettings.SetBackColor_TestSystem(this);
            }
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

        //private int childFormNumber = 0;

        ///<summary>frmMain/ OpenFile</summary>
        ///<remarks></remarks>
        public void OpenFile(object sender, EventArgs e)
        {
            ofDialog = new OpenFileDialog();
            ofDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            ofDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (ofDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = ofDialog.FileName;
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

        public void Add(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #endregion

        public MdiClient MyClientArea;
        ///<summary>frmMain/ frmMDIParent_Load</summary>
        ///<remarks></remarks>  
        private void frmMDIParent_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.ctrMenu._frmMain = this;
            MdiClient ctlMDI = default(MdiClient);
            foreach (Control ctl in this.Controls)
            {
                if (ctl.GetType() == typeof(MdiClient))
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
        }
        /////<summary>frmMain/ SetDefaultColor</summary>
        /////<remarks></remarks>
        //private void SetDefaultColor()
        //{
        //    Sped4.Properties.Settings.Default.BackColor = clsSystem.const_DefaultColorLvsSped_BackColor;
        //    Sped4.Properties.Settings.Default.BaseColor = clsSystem.const_DefaultColorLvsSped_BaseColor;
        //    Sped4.Properties.Settings.Default.BaseColor2 = clsSystem.const_DefaultColorLvsSped_BaseColor2;
        //    Sped4.Properties.Settings.Default.EffectColor = clsSystem.const_DefaultColorLvsSped_EffecColor;
        //    Sped4.Properties.Settings.Default.EffectColor2 = clsSystem.const_DefaultColorLvsSped_EffectColor2;
        //}
        ///<summary>frmMain/ SetComTECSettings</summary>
        ///<remarks></remarks>
        private void SetComTECSettings()
        {
            this.tsmAdmin.Visible = this.GL_User.IsAdmin;
            this.tsmiArbeitsbereich.Visible = this.GL_User.IsAdmin;
        }
        ///<summary>frmMain/ OpenFrmLogin</summary>
        ///<remarks></remarks> 
        public void OpenFrmLogin()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            OpenFrmLogin();
                                                                        }
                                                                    )
                                 );
                return;
            }
            frmLogin _frmLogin = new frmLogin(this);
            _frmLogin.StartPosition = FormStartPosition.CenterScreen;
            _frmLogin._ctrMenu = this.ctrMenu;
            _frmLogin.Show();
            _frmLogin.BringToFront();
        }
        ///<summary>frmMain/ OpenFrmWait</summary>
        ///<remarks></remarks> 
        public void OpenFrmWait()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            OpenFrmWait();
                                                                        }
                                                                    )
                                 );
                return;
            }
            this._frmWait = new frmWait();
            this._frmWait._frmMain = this;
            this._frmWait.StartPosition = FormStartPosition.CenterScreen;
            this._frmWait.Show();
            this._frmWait.BringToFront();
        }
        ///<summary>frmMain/ SetUserAuthForCtr</summary>
        ///<remarks>Setzt je nach Berechtigung das Menü im Kopf enabled</remarks> 
        public void SetUserAuthForCtr()
        {
            //Menü im Kopf
            this.tsmSystem.Enabled = this.GL_User.Menu_System;
            this.tsmStammdaten.Enabled = this.GL_User.Menu_Stammdaten;
            this.tsmLager.Enabled = this.GL_User.Menu_Lager;
            this.tsmFibuExport.Enabled = this.GL_User.write_FibuExport;

            //+++++++++++++++++++++++++++++++++Submenüs
            //System
            this.tsmiUserverwaltung.Enabled = ((this.GL_User.read_User) || (this.GL_User.write_User));
            this.tsmiArbeitsbereich.Enabled = ((this.GL_User.read_Arbeitsbereich) || (this.GL_User.write_Arbeitsbereich));
            this.tsmiMandant.Enabled = ((this.GL_User.read_Mandant) || (this.GL_User.write_Mandant));

            //Stammdaten
            this.tsmiADR.Enabled = ((this.GL_User.read_ADR) || (this.GL_User.write_ADR));
            this.tsmiPersonal.Enabled = ((this.GL_User.read_Personal) || (this.GL_User.write_Personal));
            this.tsmiKFZ.Enabled = ((this.GL_User.read_KFZ) || (this.GL_User.write_KFZ));
            this.tsmiGut.Enabled = ((this.GL_User.read_Gut) || (this.GL_User.write_Gut));
            this.tsmiRelation.Enabled = ((this.GL_User.read_Relation) || (this.GL_User.write_Relation));
            this.tsmiLagerOrt.Enabled = ((this.GL_User.read_LagerOrt) || (this.GL_User.write_LagerOrt));
            this.tsmiSchaeden.Enabled = ((this.GL_User.read_Schaden) || (this.GL_User.write_Schaden));
            this.tsmiEinheiten.Enabled = ((this.GL_User.read_Einheit) || (this.GL_User.write_Einheit));

            //Lager
            this.tsmiLagerEinlagerung.Enabled = ((this.GL_User.read_LagerEingang) || (this.GL_User.write_LagerEingang));
            this.tsmiLagerAuslagerung.Enabled = ((this.GL_User.read_LagerAusgang) || (this.GL_User.write_LagerAusgang));
            this.tsmiLagerUmbuchung.Enabled = (this.tsmiLagerEinlagerung.Enabled) && (this.tsmiLagerAuslagerung.Enabled);
            this.tsmiLagerJournal.Enabled = ((this.GL_User.read_Bestand) || (this.tsmiLagerEinlagerung.Enabled) || (this.tsmiLagerAuslagerung.Enabled));
            this.tsmiLagerBestand.Enabled = ((this.GL_User.read_Bestand) || (this.tsmiLagerEinlagerung.Enabled) || (this.tsmiLagerAuslagerung.Enabled));

            this.tsmiLagerSPL.Enabled = (this.tsmiLagerEinlagerung.Enabled) && (this.tsmiLagerAuslagerung.Enabled);
            this.tsmiLagerArtikelSearch.Enabled = (this.tsmiLagerEinlagerung.Enabled) && (this.tsmiLagerAuslagerung.Enabled);

            //ASN
            this.bestandsinfoMitMindesbestandVDA4905ToolStripMenuItem.Enabled = this.GL_User.read_Bestand;
            this.aSNDFÜEinlesenToolStripMenuItem.Enabled = (this.GL_User.read_ASNTransfer);
            this.tsmCOMStatus.Enabled = (this.GL_User.read_ASNTransfer);

        }
        ///<summary>frmMain/ ShowMainFrm</summary>
        ///<remarks>Setzt je nach Berechtigung das Menü im Kopf enabled</remarks> 
        public void ShowMainFrm()
        {
            this.Visible = true;
            BuildMenuItems();
            this.SetGLUserAndSystemToFrm(this.GL_User, this.GL_System);
        }
        ///<summary>frmMain/ SetFrmMainText</summary>
        ///<remarks></remarks> 
        private void SetFrmMainText()
        {
            //string myStrText = "Sped4 ©by ComTec Nöker GmbH - User[" + GL_User.LoginName + "] / Arbeitsbereich[" + GL_System.sys_Arbeitsbereichsname + "]";
            string myStrText = "Sped4 ©by SoftKonzept GmbH - User[" + GL_User.LoginName + "] / Arbeitsbereich[" + GL_System.sys_Arbeitsbereichsname + "]";
            this.Text = myStrText;
        }
        ///<summary>frmMain/ ShowOrHideCtrByClient</summary>
        ///<remarks></remarks> 
        private void ShowOrHideCtrByClient()
        {
            this.tsmASNTransfer.Visible = this.GL_System.VE_Lager_ASNVerkehr;
        }
        ///<summary>frmMain/ SetGLUserAndSystemToFrm</summary>
        ///<remarks></remarks> 
        public void SetGLUserAndSystemToFrm(Globals._GL_USER myGL_User, Globals._GL_SYSTEM myGLSystem)
        {
            this.GL_System = myGLSystem;
            this.GL_User = myGL_User;

            clsUserBerechtigungen userber = new clsUserBerechtigungen();
            userber.GetUserBerechtigungLogin(ref this.GL_User);
            ctrMenu.GL_User = this.GL_User;
            ctrMenu.SetUserAuthForCtr();
            SetFrmMainText();
            ShowOrHideCtrByClient();

            SetComTECSettings();

            //Printeinstellungen pro mandandt aus INI lesen und setzen
            this.system.GetDocPathByMandant(ref this.GL_System, this.GL_System.sys_MandantenID);
            this.system.GetDocPaperSourceByMandant(ref this.GL_System, this.GL_System.sys_MandantenID);
            this.system.GetDocPrintCount(ref this.GL_System, this.GL_System.sys_MandantenID);
            //this.system.GetDocPrinterByMandantSaved(ref this.GL_System, this.GL_System.sys_MandantenID);
            this.system.GetDocPrinterByMandantSaved(ref this.GL_System, (int)this.GL_User.User_ID);

            //Menü ASNRead, da dies für die Arbeitsbereiche unterschiedlich sein kann
            this.aSNEinlesenToolStripMenuItem.Visible = this.system.Client.Modul.ASN_Create_Man;

            //Menü muss aktiv bleiben, damit Info
            this.tsmCOMStatus.Visible = this.system.Client.Modul.ASN_Create_Man; // this.system.AbBereich.ASNTransfer & this.system.Client.Modul.ASN_Create_Man;
            if (this.aSNEinlesenToolStripMenuItem.Visible)
            {
                this.bestandsinfoMitMindesbestandVDA4905ToolStripMenuItem.Visible = this.system.Client.Modul.ASN_VDA4905LiefereinteilungenAktiv;
            }
            //Hier wird die Höhe für das Menüpanel angepasst, entsprechend der angezeigten Menüs
            //Das oberste Menü soll immer angezeigt werden
            Int32 iHeight = this.afMenuStrip1.Height + 2;
            foreach (Control ctr in toolStripContainer1.TopToolStripPanel.Controls)
            {
                if (ctr.GetType() == typeof(Sped4.Controls.AFToolStrip))
                {
                    if (ctr.Visible)
                    {
                        iHeight = iHeight + ctr.Height + 1;
                    }
                }
            }
            this.toolStripContainer1.Height = iHeight;
            this.ctrMenu.Refresh();
            InitCOMInfoCheck();
        }
        ///<summary>frmMain/ InitCOMInfoCheck</summary>
        ///<remarks>Backgroundworker wird gestartet</remarks> 
        private void InitCOMInfoCheck()
        {
            //if (this.system.AbBereich.ASNTransfer)
            //{
            bgwCOMInfo = new BackgroundWorker();
            bgwCOMInfo.DoWork += new DoWorkEventHandler(WatchCom);
            bgwCOMInfo.WorkerSupportsCancellation = true;
            bgwCOMInfo.WorkerReportsProgress = false;
            if (bgwCOMInfo.IsBusy != true)
            {
                bgwCOMInfo.RunWorkerAsync();
            }
            //}
        }
        ///<summary>frmMain/ WatchCom</summary>
        ///<remarks>Funktion im Backgroundworker </remarks> 
        private void WatchCom(object sender, EventArgs e)
        {
            //IniTsmComInfo();
            while (!bStopBGWorker)
            {
                SetTsmItmForComInfoStatus();
                Thread.Sleep(10000);
            }
        }
        ///<summary>frmMain/ SetTsmItmForComInfoStatus</summary>
        ///<remarks> </remarks> 
        private void SetTsmItmForComInfoStatus()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            SetTsmItmForComInfoStatus();
                                                                        }
                                                                    )
                                 );
                return;
            }
            //Check DFÜ zum Einlesen
            //bool bNewASN = clsASN.ExistNewASNToProceed(this.GL_User.User_ID, this.system.AbBereich.ID);
            bool bNewASN = AsnReadViewData.ExistNewASNToProceed(this.GL_User.User_ID, this.system.AbBereich.ID);
            bool bNewCall = clsASNCall.ExistNewCallOrRebookingToProceed(this.GL_User.User_ID, this.system.AbBereich.ID, clsASNCall.const_AbrufAktion_Abruf);
            bool bNewRebooking = clsASNCall.ExistNewCallOrRebookingToProceed(this.GL_User.User_ID, this.system.AbBereich.ID, clsASNCall.const_AbrufAktion_UB);
            bool bNewMessage = false;
            bool bNewDelforCall = EdiDelforViewData.ExistNewDelforCallToProceed((int)this.GL_User.User_ID, (int)this.system.AbBereich.ID);


            foreach (Control ctr in this.toolStripContainer1.TopToolStripPanel.Controls)
            {
                if ("tsmCOMStatus" == ctr.Name)
                {
                    Sped4.Controls.AFToolStrip tmpTS = (Sped4.Controls.AFToolStrip)ctr;

                    foreach (var item in tmpTS.Items)
                    {
                        if (item.GetType() == typeof(ToolStripButton))
                        {
                            ToolStripButton tmpBtn = (ToolStripButton)item;
                            switch (tmpBtn.Name)
                            {
                                case "tsbtnASNReadStatus":
                                    if (bNewASN)
                                    {
                                        tmpBtn.Image = Sped4.Properties.Resources.bullet_ball_green_16x16;
                                        tmpBtn.Text = "ASN/DFÜ stehen zur Verarbeitung bereit...";
                                        tmpBtn.Tag = "1";
                                    }
                                    else
                                    {
                                        tmpBtn.Image = Sped4.Properties.Resources.bullet_ball_red_16x16;
                                        tmpBtn.Text = "keine ASN/DFÜ vorhanden...";
                                        tmpBtn.Tag = "0";
                                    }
                                    break;
                                case "tsbtnAbrufStatus":
                                    if (bNewCall)
                                    {
                                        tmpBtn.Image = Sped4.Properties.Resources.bullet_ball_green_16x16;
                                        tmpBtn.Text = "Abrufe stehen zur Verarbeitung bereit";
                                        tmpBtn.Tag = "1";
                                    }
                                    else
                                    {
                                        tmpBtn.Image = Sped4.Properties.Resources.bullet_ball_red_16x16;
                                        tmpBtn.Text = "keine Abrufe vorhanden";
                                        tmpBtn.Tag = "0";
                                    }
                                    break;
                                case "tsbtnRebookingStatus":
                                    if (bNewRebooking)
                                    {
                                        tmpBtn.Image = Sped4.Properties.Resources.bullet_ball_green_16x16;
                                        tmpBtn.Text = "Umbuchungen stehen zur Verarbeitung bereit";
                                        tmpBtn.Tag = "1";
                                    }
                                    else
                                    {
                                        tmpBtn.Image = Sped4.Properties.Resources.bullet_ball_red_16x16;
                                        tmpBtn.Text = "keine Umbuchungen vorhanden";
                                        tmpBtn.Tag = "0";
                                    }
                                    break;
                                case "tsbtnComMessage":
                                    if (bNewMessage)
                                    {
                                        tmpBtn.Image = Sped4.Properties.Resources.bullet_ball_red_16x16;
                                        tmpBtn.Text = "Fehlermeldungen liegen vor";
                                        tmpBtn.Tag = "1";
                                    }
                                    else
                                    {
                                        tmpBtn.Image = Sped4.Properties.Resources.bullet_ball_grey_16x16;
                                        tmpBtn.Text = "keine Fehlermeldungen vorhanden";
                                        tmpBtn.Tag = "0";

                                    }
                                    break;

                                case "tsbtnCallBentelerStatus":
                                    if (bNewDelforCall)
                                    {
                                        tmpBtn.Image = Sped4.Properties.Resources.bullet_ball_green_16x16;
                                        tmpBtn.Text = "Novelis: Abrufe stehen zur Verarbeitung bereit";
                                        tmpBtn.Tag = "1";
                                    }
                                    else
                                    {
                                        tmpBtn.Image = Sped4.Properties.Resources.bullet_ball_red_16x16;
                                        tmpBtn.Text = "Novelis: keine Abrufe vorhanden";
                                        tmpBtn.Tag = "0";

                                    }
                                    break;
                            }
                        }
                        if (item.GetType() == typeof(ToolStripLabel))
                        {
                            ToolStripLabel tmpL = (ToolStripLabel)item;
                            switch (tmpL.Name)
                            {
                                case "tslASNReadStatus":
                                    if (bNewASN)
                                    {
                                        tmpL.Text = "ASN/DFÜ stehen zur Verarbeitung bereit";
                                    }
                                    else
                                    {
                                        tmpL.Text = "keine ASN/DFÜ vorhanden";
                                    }
                                    break;
                                case "tslAbruf":
                                    if (bNewCall)
                                    {
                                        tmpL.Text = "Abrufe stehen zur Verarbeitung bereit";
                                    }
                                    else
                                    {
                                        tmpL.Text = "keine Abrufe vorhanden";
                                    }
                                    break;
                                case "tslRebooking":
                                    if (bNewRebooking)
                                    {
                                        tmpL.Text = "Umbuchungen stehen zur Verarbeitung bereit";
                                    }
                                    else
                                    {
                                        tmpL.Text = "keine Umbuchungen vorhanden";
                                    }
                                    break;
                                case "tslComMessage":
                                    if (bNewMessage)
                                    {
                                        tmpL.Text = "Fehlermeldungen liegen vor";
                                    }
                                    else
                                    {
                                        tmpL.Text = "keine Fehlermeldungen vorhanden";
                                    }
                                    break;

                                case "tslCallBenteler":
                                    if (bNewDelforCall)
                                    {
                                        tmpL.Text = "Novelis: Abrufe stehen zur Verarbeitung bereit";
                                    }
                                    else
                                    {
                                        tmpL.Text = "Novelis: keine Abrufe vorhanden";
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
        ///<summary>frmMain/ SetFrmMainText</summary>
        ///<remarks></remarks> 
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
        ///<summary>frmMain/ systeminfoToolStripMenuItem_Click</summary>
        ///<remarks>Systeminfo anzeigen</remarks> 
        private void systeminfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenFrmAppInfo();
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
            //Rectangle oRectPic = new Rectangle(statusStrip1.Width - 100,
            //                                    statusStrip1.Height - 20,
            //                                    Sped4.Properties.Resources.comtec.Width,
            //                                    Sped4.Properties.Resources.comtec.Height);
            //e.Graphics.DrawImage(Sped4.Properties.Resources.comtec, oRectPic);
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
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmUserverwaltung)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmUserverwaltung));
            }
            frmUserverwaltung uver = new frmUserverwaltung();
            uver.GL_User = GL_User;
            uver.StartPosition = FormStartPosition.CenterScreen;
            uver.Show();
            uver.BringToFront();
        }
        //
        //----------- Mandanten  -----------------
        private void mandantenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.GL_User.read_Mandant) && (this.GL_User.write_Mandant))
            {
                this.ctrMenu.OpenFrmMandanten();
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        //
        //------------ Arbeitsbereiche ---------------
        //
        private void arbeitsbereicheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.GL_User.write_Arbeitsbereich) && (this.GL_User.read_Arbeitsbereich))
            {
                this.ctrMenu.OpenFrmArbeitsbereiche();
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        //
        //---------------- Logbuch -------------------
        //
        private void logbuchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenLogbuch(0);
        }
        //
        //------------ Logout - Beenden
        //
        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsMessages.FrmMain_Close())
            {
                DoLogout();
            }
        }
        //
        private void DoLogout()
        {
            //Logout
            //Sperrung Eingang aufheben
            clsLEingang.unlockEingang(this.GL_User.User_ID);
            clsLAusgang.unlockAusgang(this.GL_User.User_ID);
            clsLogin lg = new clsLogin();
            lg.BenutzerID = GL_User.User_ID;
            lg.Logout();
            CloseSped4();
        }

        public void CloseSped4()
        {
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
        /************************************** FIBU EXPORT *****************************/
        //
        //
        //
        private void fIBUExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsMessages.Allgemein_FunctionOnConstruction();
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
            if (clsMessages.FrmMain_Close())
            {
                DoLogout();
            }
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
                                                                        delegate ()
                                                                        {
                                                                            InitStatusBar(iRowCount);
                                                                        }
                                                                    )
                                 );
                return;
            }
            //Gesamtanzahl der Datensätze wird übergeben
            this.toolStripProgressBar1.Maximum = iRowCount;
            this.toolStripProgressBar1.Value = 0;
            this.boStatusWork = true;
            this.tsStatLabelValue.Text = string.Empty;
        }
        //
        public void ResetStatusBar()
        {
            this.toolStripProgressBar1.Maximum = 1;
            this.toolStripProgressBar1.Value = 0;
            this.tsStatLabelValue.Text = string.Empty;
        }
        //
        //------------ STATUSBAR - WORK -------------------
        //
        public void StatusBarWork(bool myShowItemCount, string myInfoText)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            StatusBarWork(myShowItemCount, myInfoText);
                                                                        }
                                                                    )
                                 );
                return;
            }
            //Text festlegen
            string strTmp = string.Empty;
            if (this.toolStripProgressBar1.Value == 0)
            {
                strTmp = "Vorgang wird gestartet...";
            }
            else if (this.toolStripProgressBar1.Value == this.toolStripProgressBar1.Maximum)
            {
                strTmp = "ENDE";//""Vorgang erfolgreich beendet...";
            }
            else
            {
                strTmp = myInfoText;
            }

            //Berechnung
            if (this.toolStripProgressBar1.Maximum >= this.toolStripProgressBar1.Value + 1)
            {
                this.toolStripProgressBar1.Value = this.toolStripProgressBar1.Value + 1;
            }
            decimal decPro = 0;
            if (this.toolStripProgressBar1.Value > 0)
            {
                decPro = (decimal)this.toolStripProgressBar1.Value * 100 / ((decimal)this.toolStripProgressBar1.Maximum);
                //iVal = (Int32)decPro;
            }
            if (myShowItemCount)
            {
                if (!myInfoText.Equals(string.Empty))
                {
                    strTmp = myInfoText + " - ";
                }
                strTmp = strTmp + decPro.ToString("N0").Replace(".", ",") + " %  von " + this.toolStripProgressBar1.Maximum.ToString() + " Datensätze verarbeitet";

            }
            //this.tsStatLabelValue.Visible = true;
            this.tsStatLabelValue.Text = strTmp;

            this.toolStripProgressBar1.ProgressBar.Refresh();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMAIN_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (false)
            {
            }
            else
            {
                DoLogout();
            }
        }

        private void ctrMenu_Load(object sender, EventArgs e)
        {
            this.ctrMenu.InitCtr();
        }


        private void tsbtnReadASN_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenCtrASNRead();
        }


        private void tsbtnUnlockEA_Click(object sender, EventArgs e)
        {
            clsLEingang.unlockEingang();
            clsLAusgang.unlockAusgang();
        }
        /********************************************************************************************************
         *                                       Comtec Check Funktionen
         * ******************************************************************************************************/
        ///<summary>frmMain/ checkLagerortToolStripMenuItem_Click</summary>
        ///<remarks></remarks> 
        private void checkLagerortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LVS.clsComTecCheck comTecCheck = new clsComTecCheck();
            comTecCheck.InitClass(this.system, ref this.GL_User, ref this.GL_System);
            comTecCheck.DoLagerOrtCheck(true);
        }
        private void checkLagerOrtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            LVS.clsComTecCheck comTecCheck = new clsComTecCheck();
            comTecCheck.InitClass(this.system, ref this.GL_User, ref this.GL_System);
            comTecCheck.DoLagerOrtCheck(true);
        }
        ///<summary>frmMain/ infoToolStripMenuItem_Click_1</summary>
        ///<remarks></remarks> 
        private void infoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.ctrMenu.OpenFrmAppInfo();
        }
        ///<summary>frmMain/ checkLagerortoutStoreToolStripMenuItem_Click</summary>
        ///<remarks></remarks> 
        private void checkLagerortoutStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LVS.clsComTecCheck comTecCheck = new clsComTecCheck();
            comTecCheck.InitClass(this.system, ref this.GL_User, ref this.GL_System);
            comTecCheck.DoLagerOrtCheck(false);
        }
        ///<summary>frmMain/ BuildMenuItems</summary>
        ///<remarks>Menü Arbeitsbereich wird anhand der bekannten Arbeitsbereiche erstellt</remarks> 
        private void BuildMenuItems()
        {
            DataTable dtArbeitsbereich = clsArbeitsbereiche.GetArbeitsbereichListByUser(this.GL_User.User_ID, true);
            ToolStripMenuItem[] items = new ToolStripMenuItem[dtArbeitsbereich.Rows.Count]; // You would obviously calculate this value at runtime
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new ToolStripMenuItem();
                items[i].Name = dtArbeitsbereich.Rows[i]["Arbeitsbereich"].ToString();
                items[i].Tag = decimal.Parse(dtArbeitsbereich.Rows[i]["ID"].ToString());
                items[i].Text = dtArbeitsbereich.Rows[i]["Arbeitsbereich"].ToString();
                items[i].Click += new EventHandler(MenuItemClickHandler);

            }
            tsmChangeAB.DropDownItems.AddRange(items);
        }
        ///<summary>frmMain/ MenuItemClickHandler</summary>
        ///<remarks>Click Event zunm ARbeitsbereichwechsel</remarks> 
        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            //decimal workspaceID = (decimal)clickedItem.Tag;
            //Console.WriteLine(t.ToString());
            this.ctrMenu.closeAll();
            ChangeWorkspace((decimal)clickedItem.Tag);
            //this.system.ChangeWorkspace(ref this.GL_System, this.GL_User, workspaceID);
            //SetGLUserAndSystemToFrm(this.GL_User, this.GL_System);
            //this.ctrMenu.SetDefaultEStatus();
            //clsMessages.Allgemein_Arbeitsbereichswechsel(this.system.AbBereich.ABName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAbBereichID"></param>
        public void ChangeWorkspace(decimal myAbBereichID)
        {
            this.system.ChangeWorkspace(ref this.GL_System, this.GL_User, myAbBereichID);
            SetGLUserAndSystemToFrm(this.GL_User, this.GL_System);
            this.ctrMenu.SetDefaultEStatus();
            clsMessages.Allgemein_Arbeitsbereichswechsel(this.system.AbBereich.ABName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtToJump"></param>
        public void EingangJumpDierctToArtInWorkspace(clsArtikel myArtToJump)
        {
            if ((myArtToJump is clsArtikel) && (myArtToJump.ID > 0))
            {
                //Schliessen der ctr
                this.ctrMenu._ctrEinlagerung.bFromBestand = true;
                this.ctrMenu.CloseCtrEinlagerung();

                //Wechsel Arbeitsbereich
                ChangeWorkspace(myArtToJump.AbBereichID);

                //ctr Einlagerung öffnen und zum EIngang
                this.ctrMenu.Status = ctrMenu.EStatus.Lager;
                object objDummi = new object();
                this.ctrMenu.OpenCtrEinlagerung(objDummi, false);
                if (this.ctrMenu._ctrEinlagerung is ctrEinlagerung)
                {
                    //this.ctrMenu._ctrEinlagerung.JumpToLEingang(myArtToJump.Eingang.LEingangTableID);
                    this.ctrMenu._ctrEinlagerung.DoJumpToArtikel(myArtToJump.ID);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtToJump"></param>
        public void AusgangJumpDierctToArtInWorkspace(clsArtikel myArtToJump)
        {
            if ((myArtToJump is clsArtikel) && (myArtToJump.ID > 0))
            {
                //Schliessen der ctr
                //this.ctrMenu._ctrEinlagerung.bFromBestand = true;
                this.ctrMenu.CloseCtrAuslagerung();

                //Wechsel Arbeitsbereich
                ChangeWorkspace(myArtToJump.AbBereichID);

                //ctr Einlagerung öffnen und zum EIngang
                this.ctrMenu.Status = ctrMenu.EStatus.Lager;
                object objDummi = new object();
                this.ctrMenu.OpenCtrAuslagerung(objDummi, false);

                if (this.ctrMenu._ctrAuslagerung is ctrAuslagerung)
                {
                    this.ctrMenu._ctrAuslagerung.JumpToAusgang(myArtToJump.Ausgang.LAusgangID);
                }
            }
        }
        ///<summary>frmMain/ logbuchCommunicatorToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void logbuchCommunicatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.ctrMenu.OpenLogbuch(1);
            this.ctrMenu.OpenLogCOM();
        }
        ///<summary>frmMain/ backColorToolStripMenuItem_Click</summary>
        ///<remarks>BackColor</remarks>
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenFrmSettings();
        }
        ///<summary>frmMain/ RefreshColor</summary>
        ///<remarks>BackColor</remarks>
        public void RefreshColor()
        {
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    ctl.Refresh();
                }
                catch
                {
                }
            }
        }
        //********************************************************************************************** Menü ASN
        ///<summary>frmMain/ liefereinteilungenVDA4905ToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void liefereinteilungenVDA4905ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenCtrLiefereinteilungen();
        }
        ///<summary>frmMain/ aSNDFÜEinlesenToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void aSNDFÜEinlesenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenCtrASNRead();
        }
        ///<summary>frmMain/ bestandsinfoMitMindesbestandVDA4905ToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void bestandsinfoMitMindesbestandVDA4905ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenCtrBSInfo4905(null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logbuchEmaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenLogbuch(2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiPrinter_Click(object sender, EventArgs e)
        {
            this.ctrMenu.OpenCtrPrinter();
        }
        ///<summary>frmMain/ tsbtnComMessage_Click</summary>
        ///<remarks></remarks>
        private void tsbtnComMessage_Click(object sender, EventArgs e)
        {
            if ((this.tsbtnComMessage.Tag != null) && (this.tsbtnComMessage.Tag.ToString() == "1"))
            {
                this.ctrMenu.OpenLogCOM();
            }
            this.ctrMenu.OpenLogCOM();
        }
        ///<summary>frmMain/ tslComMessage_Click</summary>
        ///<remarks></remarks>
        private void tslComMessage_Click(object sender, EventArgs e)
        {
            if ((this.tsbtnComMessage.Tag != null) && (this.tsbtnComMessage.Tag.ToString() == "1"))
            {
                this.ctrMenu.OpenLogCOM();
            }
        }
        ///<summary>frmMain/ tslASNReadStatus_Click</summary>
        ///<remarks></remarks>
        private void tslASNReadStatus_Click(object sender, EventArgs e)
        {
            //if ((this.tsbtnASNReadStatus.Tag != null) && (this.tsbtnASNReadStatus.Tag.ToString() == "1"))
            //{
            //    this.ctrMenu.OpenCtrASNRead();
            //}
            OpenAsnRead();
        }
        ///<summary>frmMain/ tsbtnASNReadStatus_Click</summary>
        ///<remarks></remarks>
        private void tsbtnASNReadStatus_Click(object sender, EventArgs e)
        {
            //if ((this.tsbtnASNReadStatus.Tag != null) && (this.tsbtnASNReadStatus.Tag.ToString() == "1"))
            //{
            //    this.ctrMenu.OpenCtrASNRead();
            //}
            OpenAsnRead();
        }
        private void OpenAsnRead()
        {
            if ((this.tsbtnASNReadStatus.Tag != null) && (this.tsbtnASNReadStatus.Tag.ToString() == "1"))
            {
                this.ctrMenu.OpenCtrASNRead();
            }
        }
        ///<summary>frmMain/ tslAbruf_Click</summary>
        ///<remarks></remarks>
        private void tslAbruf_Click(object sender, EventArgs e)
        {
            if ((this.tsbtnAbrufStatus.Tag != null) && (this.tsbtnAbrufStatus.Tag.ToString() == "1"))
            {
                this.ctrMenu.OpenCtrASNCall(clsASNCall.const_AbrufAktion_Abruf);
            }
        }
        ///<summary>frmMain/ tsbtnAbrufStatus_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAbrufStatus_Click(object sender, EventArgs e)
        {
            if ((this.tsbtnAbrufStatus.Tag != null) && (this.tsbtnAbrufStatus.Tag.ToString() == "1"))
            {
                this.ctrMenu.OpenCtrASNCall(clsASNCall.const_AbrufAktion_Abruf);
            }
        }
        ///<summary>frmMain/ tslRebooking_Click</summary>
        ///<remarks></remarks>
        private void tslRebooking_Click(object sender, EventArgs e)
        {
            if (this.tsbtnRebookingStatus.Tag.ToString() == "1")
            {
                this.ctrMenu.OpenCtrASNCall(clsASNCall.const_AbrufAktion_UB);
            }
        }
        ///<summary>frmMain/ tsbtnRebookingStatus_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRebookingStatus_Click(object sender, EventArgs e)
        {
            if (this.tsbtnRebookingStatus.Tag.ToString() == "1")
            {
                this.ctrMenu.OpenCtrASNCall(clsASNCall.const_AbrufAktion_UB);
            }
        }
        ///<summary>frmMain/ aDROhneASNVerbindungToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void aDROhneASNVerbindungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsMessages.DoProzess())
            {
                LVS.clsComTecCheck comTecCheck = new clsComTecCheck();
                comTecCheck.InitClass(this.system, ref this.GL_User, ref this.GL_System);
                comTecCheck.CheckADRAndASNAction();
            }
        }
        ///<summary>frmMain/ adminCockpitToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void adminCockpitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminCockpit = new frmAdminCockpit(this.ctrMenu);
            AdminCockpit.Show();
            AdminCockpit.BringToFront();
        }
        /// <summary>
        ///             Admincockpit schliessen
        /// </summary>
        public void CloseAdminCockpit()
        {
            this.AdminCockpit.Close();
        }

        private void tsmiTests_Click(object sender, EventArgs e)
        {
            frmTestForm test = new frmTestForm();
            test._ctrMenu = this.ctrMenu;
            test.Show();
            test.BringToFront();
        }

        private void tslCallBenteler_Click(object sender, EventArgs e)
        {
            if (this.ctrMenu._frmMain.system.AbBereich.ID == 8)
            {
                //if (this.tsbtnCallBentelerStatus.Tag.ToString() == "1")
                //{
                this.ctrMenu.OpenCtrDelforDeliveryForecast();
                //}
            }
        }
    }
}
