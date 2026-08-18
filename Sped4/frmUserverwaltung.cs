using LVS;
using LVS.ViewData;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class frmUserverwaltung : frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        //internal clsUser User;

        internal UsersViewData UserVM;

        internal DataTable dtBerechtigungen = new DataTable("Userberechtigungen");
        internal DataTable dtUser = new DataTable("Userdaten");
        internal DataTable dtAngUser = new DataTable("LoggedIn");
        internal clsUserBerechtigungen _ube = new clsUserBerechtigungen();
        internal bool bUpdate = true;
        internal bool diffUser = false;
        internal decimal showUserID = 0;

        public delegate void frmUserverwaltungEventHandler(Globals._GL_USER _GL_User);
        public event frmUserverwaltungEventHandler SetGLUser;


        /*******************************************************************************
         *                  Procedures - Methoden
         * ****************************************************************************/
        ///<summary>frmUserverwaltung / frmUserverwaltung_Load</summary>
        ///<remarks></remarks>
        public frmUserverwaltung()
        {
            InitializeComponent();
        }
        ///<summary>frmUserverwaltung / frmUserverwaltung_Load</summary>
        ///<remarks></remarks>
        private void frmUserverwaltung_Load(object sender, EventArgs e)
        {
            //User = new clsUser();
            //User._GL_User = this.GL_User;
            //User.ID = 0;

            //User.Berechtigung.ID = 0;
            //User.Berechtigung._GL_User = this.GL_User;
            //User.Berechtigung.InitDataTableUserAuth();
            SetInputFieldEnabled(false);
            //InitForm();
            //CF ANFGANG 
            bUpdate = true;


            //User.ID = this.GL_User.User_ID;
            //User.Fill();
            //dtBerechtigungen = User.Berechtigung.dtBerechtigungen;
            UserVM = new UsersViewData(this.GL_User, 1);
            dtBerechtigungen = UserVM.UserAuthorizationsVM.dtBerechtigungen;

            // CF ENDE
            SetCOMSettings();
            InitForm();
        }
        ///<summary>frmMain/ SetComTECSettings</summary>
        ///<remarks></remarks>
        private void SetCOMSettings()
        {
            if (!this.GL_User.IsAdmin)
            {
                Functions.HideTabPage(ref tabUser, this.tabPageArbeitsbereiche.Name);
            }
        }
        ///<summary>frmUserverwaltung / SetUserDatenToForm</summary>
        ///<remarks></remarks>
        private void SetUserDatenToForm()
        {
            //tbName.Text = User.Name;
            //tbLogin.Text = User.LoginName;
            //tbInitialen.Text = User.Initialen;
            //tbPass1.Text = User.pass;
            //tbPass2.Text = User.pass;
            //tbVorname.Text = User.Vorname;
            //tbTelefon.Text = User.Tel;
            //tbFax.Text = User.Fax;
            //tbMail.Text = User.Mail;
            //tsbSpeichern.Enabled = true;
            //tbSMTPUser.Text = User.SMTPUser;
            //tbSMTPPass.Text = User.SMTPPasswort;
            //tbSMTPServer.Text = User.SMTPServer;
            //tbSMTPPort.Text = User.SMTPPort.ToString();
            //cbIsAdmin.Checked = User.IsAdmin;

            // Credential-Name (falls Property vorhanden)
            try
            {
                var fileNameProp = UserVM.User.GetType().GetProperty("MailCredentialsFileName");
                if (fileNameProp != null)
                    tbCredentialName.Text = (fileNameProp.GetValue(UserVM.User) as string) ?? string.Empty;
            }
            catch
            {
                tbCredentialName.Text = string.Empty;
            }

            // Ermitteln, ob Credentials vorhanden sind (unterstützt verschiedene Modelle)
            bool hasCredentials = false;
            try
            {
                var propBytes = UserVM.User.GetType().GetProperty("MailCredentialsData"); // byte[]
                if (propBytes != null)
                {
                    var val = propBytes.GetValue(UserVM.User) as byte[];
                    hasCredentials = (val != null && val.Length > 0);
                }
                else
                {
                    // ggf. Base64-String im Model
                    var propBase64 = UserVM.User.GetType().GetProperty("MailCredentialsBase64");
                    if (propBase64 != null)
                    {
                        var s = propBase64.GetValue(UserVM.User) as string;
                        hasCredentials = !string.IsNullOrWhiteSpace(s);
                    }
                }
            }
            catch
            {
                hasCredentials = false;
            }

            var indicatorColor = hasCredentials ? System.Drawing.Color.LightGreen : System.Drawing.Color.Yellow;

            // tbCredentialName Background color setzen (sicher prüfen)
            if (this.tbCredentialName != null && !this.tbCredentialName.IsDisposed)
            {
                tbCredentialName.BackColor = indicatorColor;
            }

            tbName.Text = UserVM.User.Name;
            tbLogin.Text = UserVM.User.LoginName;
            tbInitialen.Text = UserVM.User.Initialen;
            tbPass1.Text = UserVM.User.pass;
            tbPass2.Text = UserVM.User.pass;
            tbVorname.Text = UserVM.User.Vorname;
            tbTelefon.Text = UserVM.User.Tel;
            tbFax.Text = UserVM.User.Fax;
            tbMail.Text = UserVM.User.Mail;
            tsbSpeichern.Enabled = true;
            //tbSMTPUser.Text = UserVM.User.SMTPUser;
            //tbSMTPPass.Text = UserVM.User.SMTPPasswort;
            //tbSMTPServer.Text = UserVM.User.SMTPServer;
            //tbSMTPPort.Text = UserVM.User.SMTPPort.ToString();

            cbIsAdmin.Checked = UserVM.User.IsAdmin;
            tbCredentialName.Text = UserVM.User.MailCredentialsFileName;


        }
        ///<summary>frmUserverwaltung / InitForm</summary>
        ///<remarks></remarks>
        private void InitForm(bool bUpdateDGV = true)
        {
            ClearInputField();
            if (bUpdateDGV == true)
            {
                InitDGVUserList();
            }
            InitLVAbBereich();
            InitDGVUserLoggedIn();
            InitDGVUserBerechtigungen();

            if (UserVM.User.Id > 0)
            {
                SetInputFieldEnabled(true);
                SetUserDatenToForm();
                this.dgvUserBerechtigungen.Enabled = true;
            }
            else
            {
                //Eingabefelder Enabled setzen
                SetInputFieldEnabled(false);
                this.dgvUserBerechtigungen.Enabled = false;
            }
        }
        ///<summary>frmUserverwaltung / SetInputFieldEnabled</summary>
        ///<remarks></remarks>
        private void SetInputFieldEnabled(bool bEnabled)
        {
            tbFax.Enabled = bEnabled;
            tbInitialen.Enabled = bEnabled;
            tbLogin.Enabled = bEnabled;
            tbMail.Enabled = bEnabled;
            tbName.Enabled = bEnabled;
            tbPass1.Enabled = bEnabled;
            tbPass2.Enabled = bEnabled;
            tbTelefon.Enabled = bEnabled;
            tbVorname.Enabled = bEnabled;


            tsbSpeichern.Enabled = bEnabled;
            //tsbtnUserDelete.Enabled = bEnabled;
        }
        ///<summary>frmUserverwaltung / ClearInputField</summary>
        ///<remarks></remarks>
        private void ClearInputField()
        {
            tbName.Text = string.Empty;
            tbLogin.Text = string.Empty;
            tbInitialen.Text = string.Empty;
            tbPass1.Text = string.Empty;
            tbPass2.Text = string.Empty;
            tbVorname.Text = string.Empty;
            tbTelefon.Text = string.Empty;
            tbMail.Text = string.Empty;
            tbFax.Text = string.Empty;
            tbSMTPUser.Text = string.Empty;
            tbSMTPPass.Text = string.Empty;
            tbSMTPServer.Text = string.Empty;
            tbSMTPPort.Text = string.Empty;
            cbSSL.Checked = true;
        }
        ///<summary>frmUserverwaltung / InitDGVUserLoggedIn</summary>
        ///<remarks></remarks>
        private void InitDGVUserLoggedIn()
        {
            //Angemeldete User im System
            dtAngUser = clsLogin.GetLoginDaten();
            dgvUserLoggedIn.DataSource = dtAngUser;
            dgvUserLoggedIn.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill; // CF
        }
        ///<summary>frmUserverwaltung / InitForm</summary>
        ///<remarks></remarks>
        private void InitDGVUserBerechtigungen()
        {
            //dgvUserBerechtigungen.DataSource = User.Berechtigung.dtBerechtigungen;
            dgvUserBerechtigungen.DataSource = UserVM.UserAuthorizationsVM.dtBerechtigungen;
            if (this.dgvUserBerechtigungen.Rows.Count > 0)
            {
                dgvUserBerechtigungen.Columns["dbCol"].Visible = false;
                dgvUserBerechtigungen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        ///<summary>frmUserverwaltung / toolStripButton1_Click</summary>
        ///<remarks>Neuen User anlegen</remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //tsbtnUserDelete.Enabled = true;
            tsbSpeichern.Enabled = true;
            bUpdate = false;
            this.dgvUserBerechtigungen.Enabled = true;
            ClearInputField();
            SetInputFieldEnabled(true);


            //User.ID = 0;
            //User.Berechtigung.ID = 0;
            //User.Berechtigung.InitDataTableUserAuth();

            UserVM = new UsersViewData(this.GL_User);
            InitDGVUserBerechtigungen();
        }
        ///<summary>frmUserverwaltung / SetKompetteFreigabe</summary>
        ///<remarks></remarks>
        private void SetKompetteFreigabe(bool Freigabe)
        {
            //for (Int32 i = 0; i <= User.Berechtigung.dtBerechtigungen.Rows.Count - 1; i++)
            //{
            //    User.Berechtigung.dtBerechtigungen.Rows[i]["Freigabe"] = Freigabe;
            //}
            for (Int32 i = 0; i <= UserVM.UserAuthorizationsVM.dtBerechtigungen.Rows.Count - 1; i++)
            {
                UserVM.UserAuthorizationsVM.dtBerechtigungen.Rows[i]["Freigabe"] = Freigabe;
            }
            dgvUserBerechtigungen.Refresh();
        }
        ///<summary>frmUserverwaltung / dgv_CellContentClick</summary>
        ///<remarks>Setzt die Änderungen in der Tabelle</remarks>
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 2. Spalte Freigabe Berechtigung
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 1)
                {
                    bool freigabe = false;
                    //if (e.RowIndex < User.Berechtigung.dtBerechtigungen.Rows.Count)
                    //{
                    //    if ((bool)User.Berechtigung.dtBerechtigungen.Rows[e.RowIndex]["Freigabe"])
                    //    {
                    //        freigabe = false;
                    //    }
                    //    else
                    //    {
                    //        freigabe = true;
                    //    }
                    //    User.Berechtigung.dtBerechtigungen.Rows[e.RowIndex]["Freigabe"] = freigabe;
                    //}

                    if (e.RowIndex < UserVM.UserAuthorizationsVM.dtBerechtigungen.Rows.Count)
                    {
                        if ((bool)UserVM.UserAuthorizationsVM.dtBerechtigungen.Rows[e.RowIndex]["Freigabe"])
                        {
                            freigabe = false;
                        }
                        else
                        {
                            freigabe = true;
                        }
                        UserVM.UserAuthorizationsVM.dtBerechtigungen.Rows[e.RowIndex]["Freigabe"] = freigabe;
                    }
                }
            }
        }
        ///<summary>frmUserverwaltung / btnKomplettFreigabe_Click</summary>
        ///<remarks></remarks>
        private void btnKomplettFreigabe_Click(object sender, EventArgs e)
        {
            SetKompetteFreigabe(true);
        }
        ///<summary>frmUserverwaltung / button1_Click</summary>
        ///<remarks></remarks>
        private void button1_Click(object sender, EventArgs e)
        {
            SetKompetteFreigabe(false);
        }
        ///<summary>frmUserverwaltung / tsbSpeichern_Click</summary>
        ///<remarks></remarks>
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            //Pass1 und Pass2 müssen identisch sein
            if (tbPass1.Text == tbPass2.Text)
            {
                AssignValue();
                InitForm();
                ClearInputField();
                SetKompetteFreigabe(false);
                SetInputFieldEnabled(false);
                this.dgvUserBerechtigungen.Enabled = false;
                this.tabUser.SelectedTab = tabPageUserdaten;
            }
            else
            {
                clsMessages.Userverwaltung_PassNichtIdentisch();
            }
        }
        ///<summary>frmUserverwaltung / AssignValue</summary>
        ///<remarks></remarks>
        private void AssignValue()
        {
            //Trimm
            tbName.Text = tbName.Text.ToString().Trim();
            tbLogin.Text = tbLogin.Text.ToString().Trim();
            tbInitialen.Text = tbInitialen.Text.ToString().Trim();
            tbPass1.Text = tbPass1.Text.ToString().Trim();
            tbPass2.Text = tbPass2.Text.ToString().Trim();
            tbVorname.Text = tbVorname.Text.ToString().Trim();
            tbTelefon.Text = tbTelefon.Text.ToString().Trim();
            tbFax.Text = tbFax.Text.ToString().Trim();
            tbMail.Text = tbMail.Text.ToString().Trim();
            tbSMTPUser.Text = tbSMTPUser.Text.ToString().Trim();
            tbSMTPPass.Text = tbSMTPPass.Text.ToString().Trim();
            tbSMTPServer.Text = tbSMTPServer.Text.ToString().Trim();
            tbSMTPPort.Text = tbSMTPPort.Text.ToString().Trim();

            if (CheckInput())
            {
                //User.Name = tbName.Text;
                //User.LoginName = tbLogin.Text;
                //User.pass = tbPass1.Text;
                //User.Initialen = tbInitialen.Text;
                //User.Vorname = tbVorname.Text;
                //User.Tel = tbTelefon.Text;
                //User.Fax = tbFax.Text;
                //User.Mail = tbMail.Text;
                //User.SMTPUser = tbSMTPUser.Text;
                //User.SMTPPasswort = tbSMTPPass.Text;
                //User.SMTPServer = tbSMTPServer.Text;
                //User.SMTPSSL = cbSSL.Checked;
                //Int32 iTmp = clsUser.Default_SMTPPort;
                //Int32.TryParse(tbSMTPPort.Text, out iTmp);
                //User.SMTPPort = iTmp;
                //User.IsAdmin = cbIsAdmin.Checked;

                //if (bUpdate)
                //{
                //    User.Update();
                //}
                //else
                //{
                //    User.ID = 0;
                //    User.AddUserDaten();
                //}
                //User.ID = 0;
                //User.Fill();

                UserVM.User.Name = tbName.Text;
                UserVM.User.LoginName = tbLogin.Text;
                UserVM.User.pass = tbPass1.Text;
                UserVM.User.Initialen = tbInitialen.Text;
                UserVM.User.Vorname = tbVorname.Text;
                UserVM.User.Tel = tbTelefon.Text;
                UserVM.User.Fax = tbFax.Text;
                UserVM.User.Mail = tbMail.Text;
                UserVM.User.SMTPUser = tbSMTPUser.Text;
                UserVM.User.SMTPPasswort = tbSMTPPass.Text;
                UserVM.User.SMTPServer = tbSMTPServer.Text;
                UserVM.User.SMTPSSL = cbSSL.Checked;
                Int32 iTmp = clsUser.Default_SMTPPort;
                Int32.TryParse(tbSMTPPort.Text, out iTmp);
                UserVM.User.SMTPPort = iTmp;
                UserVM.User.IsAdmin = cbIsAdmin.Checked;

                if (bUpdate)
                {
                    UserVM.Update();
                }
                else
                {
                    UserVM.User.Id = 0;
                    UserVM.AddUserDaten();
                    //UserVM.User.AddUserDaten();
                }
                UserVM = new UsersViewData(GL_User);
            }
        }
        ///<summary>frmUserverwaltung / tbPass1_TextChanged</summary>
        ///<remarks></remarks>
        private void tbPass1_TextChanged(object sender, EventArgs e)
        {
            if (tbPass1.Text == string.Empty)
            {
                tbPass2.Enabled = false;
            }
            else
            {
                tbPass2.Enabled = true;
            }
        }
        ///<summary>frmUserverwaltung / CheckInput</summary>
        ///<remarks>Check der Eingabefelder</remarks>
        private bool CheckInput()
        {
            bool OK = true;
            string strHelp = "Folender Fehler sind aufgetreten: \n\r";
            if (tbName.Text == string.Empty)
            {
                OK = false;
                strHelp = strHelp + "Es wurde kein Name eingegeben \n\r";
            }
            if (tbLogin.Text == string.Empty)
            {
                OK = false;
                strHelp = strHelp + "Es wurde kein Login eingegeben \n\r";
            }
            else
            {
                if (!bUpdate)
                {
                    //Check ob Login schon vorhanden
                    if (clsUser.CheckLoginNameIsUsed(tbLogin.Text))
                    {
                        OK = false;
                        strHelp = strHelp + "Loginname wird bereits verwendet \n\r";
                    }
                }
            }
            if (tbInitialen.Text == string.Empty)
            {
                OK = false;
                strHelp = strHelp + "Es wurden keine Initialen eingegeben \n\r";
            }
            if (tbPass1.Text == string.Empty)
            {
                OK = false;
                strHelp = strHelp + "Es wurde kein Passwort eingegeben \n\r";
            }
            if (tbPass2.Text == string.Empty)
            {
                OK = false;
                strHelp = strHelp + "Es wurde kein Passwort eingegeben \n\r";
            }

            if (!OK)
            {
                MessageBox.Show(strHelp, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            return OK;
        }
        /**********************************************************************************
         *                              dgvUser - Userliste 
         * ********************************************************************************/
        ///<summary>frmUserverwaltung / InitUserList</summary>
        ///<remarks></remarks>
        private void InitDGVUserList()
        {
            DataTable dt = new DataTable("Userlist");
            dt = clsUser.GetUserList(this.GL_User);
            dgvUserList.DataSource = dt;
            if (this.dgvUserList.Rows.Count > 0)
            {
                this.dgvUserList.Columns["ID"].IsVisible = false;
                //dgvUser.Columns["LoginName"].IsVisible = false;
                this.dgvUserList.Columns["LoginName"].Width = this.dgvUserBerechtigungen.Width - 10;

                //this.dgvUser.Columns["LoginName"].

                for (Int32 i = 0; i <= this.dgvUserList.Rows.Count - 1; i++)
                {
                    string strTmp = this.dgvUserList.Rows[i].Cells["ID"].Value.ToString();
                    decimal decTmp = 0;
                    if (Decimal.TryParse(strTmp, out decTmp))
                    {
                        //if (User.ID == decTmp)
                        if (UserVM.User.Id == decTmp)
                        {
                            this.dgvUserList.Rows[i].IsSelected = true;
                        }
                    }
                }
            }
        }
        ///<summary>frmUserverwaltung / dgvUser_CellFormatting</summary>
        ///<remarks>Check der Eingabefelder</remarks>
        private void dgvUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            decimal ID = 0;
            string user = string.Empty;
            try
            {
                if ((!object.ReferenceEquals(dgvUserList.Rows[e.RowIndex].Cells["ID"].Value, DBNull.Value)))
                {
                    if (dgvUserList.Rows[e.RowIndex].Cells["ID"].Value != null)
                    {
                        ID = (decimal)dgvUserList.Rows[e.RowIndex].Cells["ID"].Value;
                    }
                }
                if ((!object.ReferenceEquals(dgvUserList.Rows[e.RowIndex].Cells["LoginName"].Value, DBNull.Value)))
                {
                    if (dgvUserList.Rows[e.RowIndex].Cells["LoginName"].Value != null)
                    {
                        user = (string)dgvUserList.Rows[e.RowIndex].Cells["LoginName"].Value;
                    }
                }

                if (e.ColumnIndex == 0)
                {
                    e.Value = user;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        ///<summary>frmUserverwaltung / dgvUser_CellDoubleClick</summary>
        ///<remarks>User auswählen</remarks>
        private void dgvUser_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (this.dgvUserList.Rows.Count > 0)
            {
                if (e.RowIndex > -1)
                {
                    if (this.dgvUserList.Rows[e.RowIndex].Cells["ID"] != null)
                    {
                        decimal tempRowIndex = e.RowIndex;
                        this.dgvUserList.ClearSelection();

                        //Abfrage auf Änderung
                        bUpdate = true;
                        decimal decTmp = 0;
                        Decimal.TryParse(this.dgvUserList.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out decTmp);
                        if (decTmp > 0)
                        {
                            //User.ID = decTmp;
                            //User.Fill();
                            //dtBerechtigungen = User.Berechtigung.dtBerechtigungen;

                            UserVM = new UsersViewData(GL_User, (int)decTmp);
                            dtBerechtigungen = UserVM.UserAuthorizationsVM.dtBerechtigungen;

                            tsbtnUserDelete.Enabled = true;
                            this.dgvUserList.Rows[e.RowIndex].IsSelected = true;
                            this.dgvUserList.CurrentRow = this.dgvUserList.Rows[e.RowIndex];
                        }
                        InitForm(false);
                        this.tabUser.SelectedTab = tabPageUserdaten;
                    }
                }
            }
        }
        ///<summary>frmUserverwaltung / tsbtnClose_Click</summary>
        ///<remarks>User auswählen</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///<summary>frmUserverwaltung / tsbtnUserDelete_Click</summary>
        ///<remarks>ausgewählter User soll gelöscht werden</remarks>
        private void tsbtnUserDelete_Click(object sender, EventArgs e)
        {
            string delUsername = tbName.Text;
            string text = "Name: " + delUsername;

            if (delUsername != "Administrator")
            {
                if (clsMessages.User_Delete(text))
                {
                    //User.DeleteUser();
                    //User.ID = 0;
                    //User.Fill();
                    InitForm();
                    this.dgvUserBerechtigungen.Enabled = false;
                    tsbtnUserDelete.Enabled = false;
                    SetKompetteFreigabe(false);
                    ClearInputField();
                    SetInputFieldEnabled(false);
                }
            }
            else
            {
                clsMessages.User_DeleteNOT(text);
            }
        }
        ///<summary>frmUserverwaltung / tsbtnClearInputField_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClearInputField_Click(object sender, EventArgs e)
        {
            ClearInputField();
            SetInputFieldEnabled(false);
            SetKompetteFreigabe(false);
            this.dgvUserBerechtigungen.Enabled = false;
            tsbtnUserDelete.Enabled = false;
            tsbSpeichern.Enabled = false;

            //this.User.ID = 0;
            //this.User.Fill();

            UserVM = new UsersViewData(this.GL_User);

            InitLVAbBereich();
        }
        ///<summary>frmUserverwaltung / InitLVAbBereich</summary>
        ///<remarks></remarks>
        private void InitLVAbBereich()
        {
            lvAbBereiche.ShowCheckBoxes = true;
            DataTable dtAbBereich = clsArbeitsbereiche.GetArbeitsbereichList(this.GL_User.User_ID);
            if (this.lvAbBereiche != null)
            {
                if (this.lvAbBereiche.Items != null)
                {
                    if (this.lvAbBereiche.Items.Count > 0)
                    {
                        this.lvAbBereiche.Items.Clear();
                    }
                }
            }

            foreach (DataRow row in dtAbBereich.Rows)
            {
                string ArbeitsbereichName = row["Arbeitsbereich"].ToString();
                decimal decTmp = 0;
                if (Decimal.TryParse(row["ID"].ToString(), out decTmp))
                {
                    clsArbeitsbereichUser tmpAssign = new clsArbeitsbereichUser();
                    tmpAssign.AbBereichID = decTmp;
                    //tmpAssign.UserID = this.User.ID;
                    tmpAssign.UserID = UserVM.User.Id;
                    bool bIsAssin = tmpAssign.IsAssign;

                    ListViewDataItem Item = new ListViewDataItem();
                    Item.Tag = (object)tmpAssign;
                    Item.Key = decTmp;
                    Item.Text = ArbeitsbereichName;
                    Item.Value = ArbeitsbereichName;
                    if (bIsAssin)
                    {
                        Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                    }
                    else
                    {
                        Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                    }
                    Item.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
                    lvAbBereiche.Items.Add(Item);
                }
            }
        }
        ///<summary>frmUserverwaltung / InitLVAbBereich</summary>
        ///<remarks></remarks>
        private void lvAbBereiche_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            clsArbeitsbereichUser AbUserAssign = (clsArbeitsbereichUser)((ListViewDataItem)e.Item).Tag;
            clsUser tmpUser = new clsUser();
            tmpUser._GL_User = this.GL_User;
            tmpUser.ID = AbUserAssign.UserID;
            tmpUser.Fill();

            if (tmpUser.IsAdmin)
            {
                string strTxt = "Der gewählte User ist mit Administratorrechten ausgestattet und ist dadurch immer allen Arbeitsbereichen zugewiesen!";
                clsMessages.Allgemein_InfoTextShow(strTxt);
                //this.User.ArbeitsbereichAccess.AdminUpdate(ref this.User);

                UserVM.ArbeitsbereichAccess.AdminUpdate(UserVM);
                AbUserAssign.Delete();
                AbUserAssign.Add();
            }
            else
            {
                if (AbUserAssign.IsAssign)
                {
                    //Daten in DB ArbeitsbereichTarif löschen
                    AbUserAssign.Delete();
                }
                else
                {
                    //Daten in DB ArbeitsbereichTarif hinzufügen
                    AbUserAssign.Add();
                }
            }
            InitLVAbBereich();
        }

        private void btnMailUserCredentialsCreate_Click(object sender, EventArgs e)
        {
            // 1) Prüfen: Userdaten geladen
            if (UserVM == null || UserVM.User == null || UserVM.User.Id <= 0)
            {
                MessageBox.Show("Bitte zuerst einen Benutzer auswählen.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) Prüfen: Button verwendbar
            if (!btnMailUserCredentialsCreate.Enabled)
            {
                MessageBox.Show("Die Aktion ist derzeit deaktiviert.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 3) Prüfen, ob notwendige Daten vorhanden sind
            string server = (UserVM.User.SMTPServer ?? string.Empty).Trim();
            int port = UserVM.User.SMTPPort;
            if (port <= 0) port = clsUser.Default_SMTPPort;
            string smtpUser = (UserVM.User.SMTPUser ?? string.Empty).Trim();
            string smtpPass = UserVM.User.SMTPPasswort ?? string.Empty;
            string mailFrom = (UserVM.User.Mail ?? string.Empty).Trim();
            bool enableSsl = UserVM.User.SMTPSSL;

            var errors = new System.Collections.Generic.List<string>();
            if (string.IsNullOrWhiteSpace(server)) errors.Add("SMTP-Server fehlt.");
            if (string.IsNullOrWhiteSpace(mailFrom)) errors.Add("E-Mail-Adresse des Benutzers fehlt.");
            if (port < 1 || port > 65535) errors.Add("SMTP-Port ist ungültig.");
            // Wenn SMTP-Auth konfiguriert ist, müssen User und Passwort vorhanden sein
            if (!string.IsNullOrWhiteSpace(smtpUser) && string.IsNullOrWhiteSpace(smtpPass))
                errors.Add("SMTP-Passwort fehlt für den konfigurierten SMTP-Benutzer.");
            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors), "Fehlende Daten", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4) Ordnerauswahl für Export
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Wählen Sie einen Ordner zum Export der verschlüsselten Mail-Credentials";
                try
                {
                    var defaultPath = LVS.InitValue.DefaultPath_LvsExport.DefaultPath();
                    if (!string.IsNullOrWhiteSpace(defaultPath) && System.IO.Directory.Exists(defaultPath))
                        fbd.SelectedPath = defaultPath;
                }
                catch
                {
                    // ignore
                }

                if (fbd.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Export abgebrochen.", "Abbruch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string exportFolder = fbd.SelectedPath;
                // 5) Erstellen der Credentials und Export
                try
                {
                    string profileName = (UserVM.User.LoginName ?? $"User_{UserVM.User.Id}").Trim();
                    // Dateiname mit Zeitstempel, eindeutig pro Export
                    string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_user_" + UserVM.User.Id.ToString()+"_" + UserVM.User.Vorname+UserVM.User.Name + "_mail_credentials.xml";
                    string filePath = System.IO.Path.Combine(exportFolder, fileName);

                    var config = new Common.Models.MailCheckConfig
                    {
                        Server = server,
                        Port = port,
                        Username = smtpUser,
                        Password = smtpPass,
                        MailFrom = mailFrom,
                        EnableSsl = enableSsl,
                        MailBCC = string.Empty
                    };

                    var manager = new LVS.Mail.MailCredentialsManager(filePath);
                    bool saved = manager.SaveCredentials(profileName, config);

                    if (saved)
                    {
                        MessageBox.Show("Credentials erfolgreich verschlüsselt und exportiert." + Environment.NewLine + filePath,
                                        "Export erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim Export der Credentials. Bitte Dateiberechtigungen prüfen.",
                                        "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Export der Credentials: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddUserCredentials_Click(object sender, EventArgs e)
        {
            // 1) Prüfen: User ausgewählt
            if (UserVM == null || UserVM.User == null || UserVM.User.Id <= 0)
            {
                MessageBox.Show("Bitte zuerst einen Benutzer auswählen.", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) Dialog: Datei auswählen (verschlüsselte Credentials-Datei)
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Wählen Sie die verschlüsselte Mail-Credentials-Datei";
                ofd.Filter = "Mail-Credentials (*.xml)|*.xml|Alle Dateien (*.*)|*.*";
                try
                {
                    var defaultPath = LVS.InitValue.DefaultPath_LvsExport.DefaultPath();
                    if (!string.IsNullOrWhiteSpace(defaultPath) && System.IO.Directory.Exists(defaultPath))
                        ofd.InitialDirectory = defaultPath;
                }
                catch
                {
                    // ignore
                }

                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Import abgebrochen.", "Abbruch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string filePath = ofd.FileName;
                if (!System.IO.File.Exists(filePath))
                {
                    MessageBox.Show("Die ausgewählte Datei existiert nicht.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    // 3) Datei einlesen (binär)
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                    string fileName = System.IO.Path.GetFileName(filePath);

                    // 4) Laden des clsUser-Objekts und Speichern in DB
                    var dbUser = new clsUser((int)UserVM.User.Id);
                    dbUser._GL_User = this.GL_User; // sicherstellen, dass Kontext vorhanden ist

                    bool ok = dbUser.SaveMailCredentialsToUser(fileBytes, fileName);
                    if (ok)
                    {
                        MessageBox.Show("Credentials erfolgreich in der Datenbank gespeichert.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 5) UI aktualisieren: User neu laden und Felder aktualisieren
                        UserVM = new UsersViewData(this.GL_User, (int)dbUser.ID);
                        InitForm(false);                 // Form neu befüllen ohne Userliste neu zu laden
                        SetUserDatenToForm();            // Felder anzeigen
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim Speichern der Credentials in der Datenbank. Prüfen Sie Berechtigungen.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Importieren der Credentials: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
