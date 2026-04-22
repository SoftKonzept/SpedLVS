using LVS;
using Sped4.Controls.AdminCockpit;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;



namespace Sped4
{
    public partial class frmLogin : frmTEMPLATE
    {
        ///<summary>frmLogin</summary>
        ///<remarks>Zugangskontrolle für das Sped4. Hierbei laufen folgende Chechs ab:
        ///         - Auswahl Sprache
        ///         - Auswahl Arbeitsbereich
        ///         - Check auf Loginname und Passwort</remarks>

        public Globals._GL_USER GL_User;
        public Globals._GL_SYSTEM GL_System;
        internal frmMAIN _frmMain;
        public ctrMenu _ctrMenu;

        public decimal decUpdateVersion = 0;
        internal Int32 CountLogin = 0;
        internal DataTable dtArbeitsbereich;
        internal decimal decArbeitsbereichID;
        public delegate void ThreadCtrInvokeEventHandler();

        internal bool bLogin = false;
        internal bool bUpdate = false;
        internal clsUser user;

        /********************************************************************************************
         *                              Methoden und Funktionen
         * *****************************************************************************************/
        ///<summary>frmLogin / frmLogin</summary>
        ///<remarks>Initialisierung der Form frmLogin. Dabei wird eine Überprüfung der Anwendungs- und
        ///         Datenbankversion vorgenommen.</remarks>
        public frmLogin(frmMAIN myFrmMain)
        {
            InitializeComponent();
            this._frmMain = myFrmMain;
            this.GL_System = this._frmMain.GL_System;
            this.GL_User = this._frmMain.GL_User;
            this.splitPanel2.Collapsed = true;
        }
        ///<summary>frmLogin / frmLogin_Load</summary>
        ///<remarks>Initialisierung der verschiedenen Komponenten und Variablen.</remarks>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            user = new clsUser();
            user._GL_User = this.GL_User;
            this._frmMain._frmLogin = this;
            AddInfo("Sped 4 System wird initialisiert!");
            this._frmMain.system = new clsSystem(Application.StartupPath);
            CheckConnection();
        }
        ///<summary>frmLogin / InitCtr</summary>
        ///<remarks></remarks>
        private void InitCtr()
        {
            SetFrmText();
            dtArbeitsbereich = new DataTable("Arbeitsbereiche");
            decArbeitsbereichID = 0.0M;
        }
        ///<summary>frmLogin / CheckConnection</summary>
        ///<remarks></remarks>
        public void CheckConnection()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            CheckConnection();
                                                                        }
                                                                    )
                                 );
                return;
            }

            if (this._frmMain != null)
            {
                Int32 iTry = 1;
                while (
                        (iTry <= 5) &&
                        (!this._frmMain.system.InitDBConnection(ref this._frmMain.GL_System, ref this._frmMain.system.listLogToFileSystem))
                      )
                {
                    //Thread.Sleep(1000);
                    AddInfo(iTry.ToString() + ". Versuch eine Datenbankverbindung hergestellt ist fehlgeschlagen.");
                    iTry++;
                    if ((this._frmMain.system.SystemExit) || (iTry == 6))
                    {
                        //    Thread.Sleep(1000);
                        //    Application.Exit();
                        iTry = 6;
                    }
                }

                if ((this._frmMain.system.SystemExit) || (iTry == 6))
                {
                    Thread.Sleep(500);
                    string strMes = "Es konnte keine Verbindung zur Datenbank hergestellt werden! " + Environment.NewLine;
                    strMes += "Setzen Sie sich mit dem Support von ComTEC Nöker GmbH in Verbindung. Die Anwendung wird geschlossen!";
                    clsMessages.Allgemein_ERRORTextShow(strMes);
                    this.Close();
                    this._frmMain.CloseSped4();
                }
                else
                {
                    AddInfo("Sped 4 gestartet!");
                    //Thread.Sleep(1000);
                    //Login Form wird ertellt
                    if (this._frmMain.GL_System.sys_VersionAppDecimal > 0)
                    {
                        CheckOldLogin();
                        //Evetn für Entertaste in Textbox Passwort definieren
                        this.tbPasswort.KeyDown += new KeyEventHandler(this.tbPasswort_KeyDown);
                        OpenUpdateMirror();
                    }
                    this._frmMain.system.InitSystem(ref this._frmMain.GL_System);
                    InitCtr();
                    InitLBArbeitsbereich();
                    this.splitPanel2.Collapsed = false;
                    lbArbeitsbereich.Focus();
                }
            }
            else
            {
                tbInfo.Text = string.Empty;
                AddInfo("Bitte warten...");
                AddInfo("Aktion wird durchgeführt...");
                this.Focus();
            }
        }
        ///<summary>frmLogin / AddInfo</summary>
        ///<remarks></remarks>  
        public void AddInfo(string myInfo)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            AddInfo(myInfo);
                                                                        }
                                                                    )
                                 );
                return;
            }

            tbInfo.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + myInfo +
                          Environment.NewLine +
                          tbInfo.Text;
        }
        ///<summary>frmLogin / btnClose_Click</summary>
        ///<remarks>Schließen / Beendern der  Form frmLogin.</remarks>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        ///<summary>frmLogin / btnLogin_Click</summary>
        ///<remarks>Aufruf der Funktion  CheckEntry().</remarks>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            CheckEntry();
        }
        ///<summary>frmLogin / SetFrmText</summary>
        ///<remarks>Anpassung der Frm.Text Eigenschaft mit der aktuellen Version.</remarks>
        public void SetFrmText()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            SetFrmText();
                                                                        }
                                                                    )
                                 );
                return;
            }
            this.Text = "Sped4 Login [Version: " + this.GL_System.sys_VersionApp + "]";
        }
        ///<summary>frmLogin / InitLBArbeitsbereich</summary>
        ///<remarks>Listbox lbArbeitsbereiche wird gefüllt mit allen aktiven Arbeitsbereichen. Der Selected Index
        ///         wird auf -1 gesetzt, da hier explizit ein Arbeitsbereich vom User gewählt werden soll.</remarks>
        public void InitLBArbeitsbereich()
        {
            //Ermittel alle aktiven Arbeitsbereiche
            dtArbeitsbereich.Clear();
            lbArbeitsbereich.Items.Clear();
            if (this.user.ID > 0)
            {
                dtArbeitsbereich = clsArbeitsbereiche.GetArbeitsbereichListByUser(this.user.ID, true);
            }
            else
            {
                dtArbeitsbereich = clsArbeitsbereiche.GetArbeitsbereichListByStatus(this.user.ID, true);
            }
            if (dtArbeitsbereich.Rows.Count > 0)
            {
                //Füllen der Listbox
                for (Int32 i = 0; i <= dtArbeitsbereich.Rows.Count - 1; i++)
                {
                    lbArbeitsbereich.Items.Add(dtArbeitsbereich.Rows[i]["Arbeitsbereich"].ToString());
                }
                //Wenn nur ein Arbeitsbereich vorhanden ist, dann direkte aus Auswahl dieses 
                //einen Arbeitsbereichs
                if (dtArbeitsbereich.Rows.Count > 0)
                {
                    lbArbeitsbereich.SelectedIndex = 0;
                    lbArbeitsbereich.Focus();
                }
                else
                {
                    lbArbeitsbereich.SelectedIndex = -1;
                    tbLoginName.Focus();
                }
            }
            else
            {
                //tsbtnArbeitsbereichNEw sichtbar machen
                lbArbeitsbereich.SelectedIndex = -1;

                //Neuinstallation und es müsse erst eine Adresse und der Arbeitsbereich angelegt werden
                if (decArbeitsbereichID == 0)
                {
                    this.Hide();
                    //User Admin laden, wg Berechtigungen die folgenden Schritte durchführen zu können
                    clsUser Admin = new clsUser();
                    Admin.ID = clsUser.GetUserIDByLoginNameAndPass("Admin", "lvs");
                    if (Admin.ID == 0)
                    {
                        Admin.CreateAdminUser();
                    }
                    this.GL_User = Admin.Fill();
                    clsUserBerechtigungen userber = new clsUserBerechtigungen();
                    userber.GetUserBerechtigungLogin(ref this.GL_User);
                    this._ctrMenu.GL_User = this.GL_User;

                    //umggekehrte Reihenfolge werde die Frms geöffnet, da die letzte Frm dann als erstes 
                    //sichtbar ist und dann nach und nach die Daten eingegeben werden können
                    //Arbeitsbereich
                    this._ctrMenu.OpenFrmArbeitsbereiche();
                    //Mandanten eingabe
                    this._ctrMenu.OpenFrmMandanten();
                    //neue Adresseeingabe
                    this._ctrMenu.OpenFrmADR(null);

                    this.btnLogin.Visible = false;
                    this.Show();
                }
            }
        }
        ///<summary>frmLogin / OpenUpdateMirror</summary>
        ///<remarks>Öffnet die Update-Routine zum Check der Version der Datenbank.</remarks>
        private void OpenUpdateMirror()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmUpdateMirror)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmUpdateMirror));
            }
            frmUpdateMirror um = new frmUpdateMirror();
            um.GL_User = this.GL_User;
            um.GL_System = this._frmMain.GL_System;
            um.login = this;
            um.StartPosition = FormStartPosition.CenterScreen;
            um.CheckUpdateVersion();
            um.Hide();
            //check auf UpdateVersion
            //if (um.login.decUpdateVersion > this._frmMain.GL_System.sys_VersionAppDecimal)
            //{
            //    //Update muss durchgeführt werden
            //    this.Hide();
            //    um.Show();
            //    um.BringToFront();
            //    bUpdate = true;
            //    this.Close();
            //}

            if (
                    ((this._frmMain.system.SystemVersionApp != null) && (um.login.decUpdateVersion > this._frmMain.system.SystemVersionAppDecimal))
                    ||
                    ((this._frmMain.system.SystemVersionAppArchive != null) && (um.login.decUpdateVersion > this._frmMain.system.SystemVersionAppDecimalArchive))
               )
            {
                //Update muss durchgeführt werden
                this.Hide();
                um.Show();
                um.BringToFront();
                bUpdate = true;
                this.Close();
            }
            else
            {
                // kein Update notwendig 
                um.Close();
                this.Show();
                this.BringToFront();
            }
        }
        ///<summary>frmLogin / tbPasswort_KeyDown</summary>
        ///<remarks>Das Drücken der Return-Taste hat den gleichen Effekt wie das Drücken des 
        ///         Speicher-Buttons. Hier wird jeweils die Funktion CheckEntry() aufgerufen.</remarks>
        void tbPasswort_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    CheckEntry();
            //}
        }
        ///<summary>frmLogin / CheckEntry</summary>
        ///<remarks>Ablauf der Prüfungen:
        ///         - Init des Zählers für die Einlogversuche
        ///         - Init clsUser
        ///         - Check Eingabefelder korrekt
        ///         - Check der Auswahlfelder
        ///         - User exist >>> Berechtigungen werden geladen
        ///         - Löschen der stornierten Aufträge 
        ///         - Weiterleitung zur Hauptform
        ///         - schließen dieser Form</remarks>
        private void CheckEntry()
        {
            //Zähler für die Anzhal der Versuche
            CountLogin = CountLogin + 1;
            tbLoginName.Text = tbLoginName.Text.ToString().Trim();
            tbPasswort.Text = tbPasswort.Text.ToString().Trim();

            //Init User-Klasse

            user.BenutzerID = GL_User.User_ID;

            if (
                (user.CheckLogin(tbLoginName.Text, tbPasswort.Text)) &
                (lAusArbeitsbereich.Text != "leer")
                )
            {
                decimal userID = clsUser.GetUserIDByLoginNameAndPass(tbLoginName.Text, tbPasswort.Text);
                user.ID = userID;
                this.GL_User = user.Fill();

                //prüfen, ob der User für den Arbeitsbereich freigegeben ist
                if (user.ListArbeitsbereichAccess.Contains(decArbeitsbereichID))
                {
                    this._frmMain.GL_User = this.GL_User;
                    //Arbeitsbereichsdaten
                    this._frmMain.system.ChangeWorkspace(ref this._frmMain.GL_System, this.GL_User, decArbeitsbereichID);
                    //Sprache
                    this._frmMain.system.ChangeLanguage(ref this._frmMain.GL_System, lAusSprache.Text);

                    if (GL_User.User_ID > 0)
                    {
                        //SetGLUser(GL_User);
                        //LOGIN eintrag in DB
                        clsLogin lg = new clsLogin();
                        lg.BenutzerID = GL_User.User_ID;
                        lg.Login();

                        //Check auf Stornierte Aufträge
                        //ein stornierter Auftrag soll nach 4 Wochen nach Auftragsdaten automatisch gelöscht werden
                        CheckForStornoAuftrag();

                        //MainForm visible = true
                        this._frmMain.ShowMainFrm();
                        bLogin = true;
                        this.Close();
                    }
                    else
                    {
                        AddInfo("Der" + CountLogin.ToString() + ". Versuch sich anzumelden ist fehlgeschlagen!");
                        AddInfo("Bitte versuchen Sie es erneut...");
                        tbLoginName.Text = string.Empty;
                        tbPasswort.Text = string.Empty;
                        tbLoginName.Focus();
                    }
                }
                else
                {
                    //User ist nicht freigegeben für den Arbeitsbereich
                    //Arbeitsbereichsliste neu füllen, damit eine Auswahl mit den entsprechenden Arbeitsbereichen neu erstellt wird
                    string strTxt = "Für den gewählten Arbeitsbereich liegt keine Berechtigung vor. Bitte wählen Sie erneut einen Arbeitsbereich!";
                    clsMessages.Allgemein_InfoTextShow(strTxt);
                    InitLBArbeitsbereich();
                }
            }
            else
            {
                //Fehlversuch wird in Log eingetragen
                clsLogin.Logbuch_LoginFehlversuch(CountLogin);
                if (CountLogin > 3)
                {
                    AddInfo("Der mehrfache Versuch sich anzumelden ist fehlgeschlagen!");
                    AddInfo("LVS/Sped4 wird beendet!");
                    Thread.Sleep(15000);
                    Application.Exit();
                }
                else
                {
                    AddInfo("Der" + CountLogin.ToString() + ". Versuch sich anzumelden ist fehlgeschlagen!");
                    AddInfo("Bitte versuchen Sie es erneut...");
                    tbLoginName.Text = string.Empty;
                    tbPasswort.Text = string.Empty;
                    tbLoginName.Focus();
                }
            }
        }
        ///<summary>frmLogin / CheckForStornoAuftrag</summary>
        ///<remarks>Löschen aller stornierten Dispo-Aufträge</remarks>
        private void CheckForStornoAuftrag()
        {
            clsAuftrag.CheckAndDeleteStornoAuftrag(GL_User, 0.0M);
        }
        ///<summary>frmLogin / CheckOldLogin</summary>
        ///<remarks>Alte und nicht ordnungsgemäß abgemeldet Logins werden gelöscht.</remarks>
        private void CheckOldLogin()
        {
            clsLogin.DeleteOldLogin();
        }
        ///<summary>frmLogin / lbArbeitsbereich_SelectedIndexChanged</summary>
        ///<remarks>Name und ID für den gewählten Arbeitsbereich werden ermittelt.</remarks>
        private void lbArbeitsbereich_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbArbeitsbereich.SelectedIndex > -1)
            {
                lAusArbeitsbereich.Text = lbArbeitsbereich.SelectedItem.ToString();
                DataTable dt = Functions.FilterDataTable(dtArbeitsbereich, lAusArbeitsbereich.Text, "Arbeitsbereich");
                if (dt.Rows.Count > 0)
                {
                    decArbeitsbereichID = (decimal)dt.Rows[0]["ID"];
                }
                else
                {
                    decArbeitsbereichID = 0.0M;
                    lAusArbeitsbereich.Text = "leer";
                }
            }
        }
        ///<summary>frmLogin / lvSprache_SelectedIndexChanged</summary>
        ///<remarks>Die gewählte Sprache wird übernommen.</remarks>
        private void lvSprache_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSprache.SelectedItems.Count > 0)
            {
                lAusSprache.Text = lvSprache.SelectedItems[0].Text;
            }
        }
        ///<summary>frmLogin / tbPasswort_KeyPress</summary>
        ///<remarks></remarks>
        private void tbPasswort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                CheckEntry();
            }
        }
        ///<summary>frmLogin / frmLogin_FormClosing</summary>
        ///<remarks></remarks>
        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bLogin == false && bUpdate == false)
            {
                Application.Exit();
            }

        }
        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tbLoginName_KeyDown(object sender, KeyEventArgs e)
        {
            if (
                e.KeyData == (Keys.Shift | Keys.Enter)
                )
            {

                frmAdminCockpit frmTest = new frmAdminCockpit(null);
                frmTest.Show();
                frmTest.BringToFront();


            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
