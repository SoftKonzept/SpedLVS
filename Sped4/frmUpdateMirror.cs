using LVS;
using Sped4.Classes;
using System;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmUpdateMirror : Sped4.frmTEMPLATE
    {
        public Globals._GL_SYSTEM GL_System;
        public Globals._GL_USER GL_User;
        public frmLogin login;
        private clsUpdate up;
        private clsUpdateArchiv upArchive;
        internal string strMesTyp = string.Empty;
        internal string strMesDaten = string.Empty;
        public string strFortschritt = string.Empty;


        public frmUpdateMirror()
        {
            InitializeComponent();
        }
        //
        //
        //
        private void frmUpdateMirror_Load(object sender, EventArgs e)
        {
            if (login != null)
            {
                login.Hide();
            }
            GL_User = login.GL_User;
            GL_System = login._frmMain.GL_System;
            CreateMessageInfo();
        }
        //
        //
        //
        private void CreateMessageInfo()
        {
            //strMesTyp = "aktuelle LVS Version : " + Environment.NewLine;
            //strMesDaten = GL_System.sys_VersionApp + Environment.NewLine;

            //strMesTyp = "aktuelle ARCHIV Version : " + Environment.NewLine;
            //strMesDaten = GL_System.sys_VersionARCHIV + Environment.NewLine;

            //strMesTyp = strMesTyp + "neue Version : " + Environment.NewLine;
            //strMesDaten = strMesDaten + Functions.FormatDecimalVersion(login.decUpdateVersion) + Environment.NewLine;

            //strMesDaten = strMesDaten + "Bitte betätigen Sie den entsprechenden Button um das Update zu starten " +
            //                            "oder Sped4 zu beenden!";


            strMesTyp += "neue Version : " + Functions.FormatDecimalVersion(login.decUpdateVersion) + Environment.NewLine;
            strMesTyp += "aktuelle LVS Version : " + GL_System.sys_VersionApp + Environment.NewLine;
            strMesTyp += "aktuelle ARCHIV Version : " + GL_System.sys_VersionARCHIV + Environment.NewLine;
            tbInfoTyp.Text = strMesTyp;


            //strMesDaten = ;

            tbInfoDaten.Text = "Bitte betätigen Sie den entsprechenden Button um das Update zu starten oder Sped4 zu beenden!";

        }

        //
        //----------- Abbruch --------------------
        //
        private void btnAbbruch_Click(object sender, EventArgs e)
        {
            CloseUpdateMirror();
            if (!up.UpdateOK)
            {
                Application.Exit();
            }
        }
        //
        public void CloseUpdateMirror()
        {
            this.Close();
            ShowFrmLogin();
        }
        //
        public void CheckUpdateVersion()
        {
            up = new clsUpdate();
            up.upMirr = this;
            login.decUpdateVersion = Functions.GetMaxArray(AppVersion.UpdateVersions());

        }
        //
        //
        public void InitFrm()
        {
            if (login._frmMain.system.SystemVersionAppDecimal < login.decUpdateVersion)
            {
                //CreateMessageInfo();
                clsUpdate update = new clsUpdate();
                update.upMirr = this;
                update.InitUpdate();
            }
            if (login._frmMain.system.SystemVersionAppDecimalArchive < login.decUpdateVersion)
            {
                //CreateMessageInfo();
                clsUpdateArchiv updateArchive = new clsUpdateArchiv();
                updateArchive.upMirr = this;
                if (updateArchive.ExistDB())
                {
                    updateArchive.InitUpdate();
                }
                else
                {
                    strFortschritt = "Die Datenbank konnte nicht gefunden werden! Bitte setzen Sie sich mit dem Support der Firma COMTEC in Verbindung!";
                    SetInfoFortschritt();
                }
            }
        }
        //
        private void btnStartUpdate_Click(object sender, EventArgs e)
        {
            InitFrm();
        }
        //
        public void SetInfoFortschritt()
        {
            tbMessages.Text = strFortschritt;
            tbMessages.Refresh();
            this.Refresh();
        }
        //
        public void ShowFrmLogin()
        {
            login.Show();
            login.BringToFront();
        }
        //
        //------ Button Start nach Update Enabled -------------
        //
        public void VisibleStartUpdateButton(bool visible)
        {
            btnStartUpdate.Visible = visible;
        }
    }
}
