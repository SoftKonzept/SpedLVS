using LVS;
using LVS.ASN;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrASNMain : UserControl
    {
        public ctrAdrVerweis _ctrAdrVerweis;
        public ctrOrga _ctrOrga;
        public ctrASNAction _ctrASNAction;
        public ctrJob _ctrJob;
        public ctrVDAClientOut _ctrVdaClientOut;
        public ctrEDIFACTClientOut _ctrEDIFACTClientOut;
        public ctrVDAClientWorkspaceValue _ctrVdaClientWorkspaceValue;
        public ctrASNArtFieldAssignment _ctrASNArtFieldAssignment;
        public ctrASNMessage _ctrASNMessageTest;
        public ctrEdifactImport _ctrEdifactImport;
        public ctrVDAClientOutUpdate _ctrVdaClientUpdate;

        internal int SearchButton;
        internal ctrMenu _ctrMenu;
        public clsASNWizzard asnWizz;
        //public clsASNArt SelectedAsnArt = new clsASNArt();

        internal enum enumTabPageASNSettings
        {
            tabPageASNSetting_AdrVerweise
            , tabPageASNSetting_ASNAction
            , tabPageASNSetting_Orga
            , tabPageASNSetting_Jobs
            , tabPageASNSetting_VDAClientOut
            , tabPageASNSetting_EDIFACTClientOut
            , tabPageASNSetting_VDAClientWorkspaceValue
            , tabPageASNSetting_ASNArtFieldAssignment
            , tabPageASNSetting_ASNMessagesTest
            , tabPageASNSetting_tabPageImportEdifact
            , tabPageASNSetting_VDAClientOutUpdate
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myMenu"></param>
        public ctrASNMain(ctrMenu myMenu)
        {
            InitializeComponent();
            _ctrMenu = myMenu;

            pageViewASNSetting.SelectedPage = tabPageASNSetting_AdrVerweise;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrASNMain_Load(object sender, EventArgs e)
        {
            //comboAsnArt füllen
            comboASNArt.DataSource = clsASNArt.GetASNArtList(this._ctrMenu._frmMain.GL_User.User_ID);
            comboASNArt.ValueMember = "ID";
            comboASNArt.DisplayMember = "Typ";
            comboASNArt.SelectedIndex = 0;
        }
        /// <summary>
        ///             Initialisiert den ASN-Wizzard
        /// </summary>
        /// <param name="myAdrId"></param>
        private void IniWizzardCls(decimal myAdrId)
        {
            clsSQLCOM tmpSQLCon = new clsSQLCOM();
            tmpSQLCon.init();
            clsASNArt tmpASNArt = new clsASNArt();
            tmpASNArt.InitClass(ref this._ctrMenu._frmMain.GL_User, tmpSQLCon);
            tmpASNArt.ID = (decimal)comboASNArt.SelectedValue;
            tmpASNArt.Fill();

            asnWizz = new clsASNWizzard(myAdrId, this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.GL_System, tmpASNArt);
        }
        /// <summary>Öffnet die Adresssuche</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrASNMain / tbSearchA_TextChanged</summary>
        ///<remarks>Auftraggeber Adresssuche</remarks>
        private void tbSearchA_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this._ctrMenu.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            string SearchText = tbSearchA.Text.ToString();
            string Ausgabe = string.Empty;

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();
            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            tbAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
            decimal decAdrIdTmp = 0;
            if (dtTmp.Rows.Count > 0)
            {
                decAdrIdTmp = Functions.GetADR_IDFromTable(dtTmp);
            }
            TakeOverAdrID(decAdrIdTmp);
        }
        ///<summary>ctrASNMain/TakeOverAdrID</summary>
        ///<remarks>Übergabe der gesuchten AdrId</remarks>
        public void TakeOverAdrID(decimal myDecADR_ID)
        {
            this.SearchButton = 1;
            IniWizzardCls(myDecADR_ID);
            SetAdrAuftraggeberToCtr();
            StartSearch();
        }
        /// <summary>
        ///             Setzt die Auftraggeberadressdaten auf das Form / CTR
        /// </summary>
        private void SetAdrAuftraggeberToCtr()
        {
            if (this.asnWizz is clsASNWizzard)
            {
                this.tbSearchA.Text = this.asnWizz.AuftragggeberAdr.ViewID;
                this.tbAuftraggeber.Text = this.asnWizz.AuftragggeberAdr.ADRStringShort;
            }
            else
            {
                ClearAuftraggeberAdrInputfields();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClearAuftraggeberAdrInputfields()
        {
            this.tbSearchA.Text = string.Empty;
            this.tbAuftraggeber.Text = string.Empty;
            this.nudAdrIdDirect.Value = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageViewASNSetting_SelectedPageChanged(object sender, EventArgs e)
        {
            enumTabPageASNSettings tmpTabPage = enumTabPageASNSettings.tabPageASNSetting_ASNAction;
            if (Enum.TryParse(pageViewASNSetting.SelectedPage.Name.ToString(), out tmpTabPage))
            {
                switch (tmpTabPage)
                {
                    case enumTabPageASNSettings.tabPageASNSetting_AdrVerweise:
                        InitTabPage_AdrVerweise();
                        break;
                    case enumTabPageASNSettings.tabPageASNSetting_Orga:
                        OpenCtr_ctrOrga();
                        break;
                    case enumTabPageASNSettings.tabPageASNSetting_ASNAction:
                        OpenCtr_ctrASNAction();
                        break;
                    case enumTabPageASNSettings.tabPageASNSetting_Jobs:
                        OpentCtr_ctrJobs();
                        break;
                    case enumTabPageASNSettings.tabPageASNSetting_VDAClientOut:
                        OpentCtr_ctrVDAClientOut();
                        break;
                    case enumTabPageASNSettings.tabPageASNSetting_EDIFACTClientOut:
                        OpentCtr_ctrEDIFACTClientOut();
                        break;
                    case enumTabPageASNSettings.tabPageASNSetting_ASNArtFieldAssignment:
                        OpentCtr_ctrASNArtFieldAssignment();
                        break;
                    case enumTabPageASNSettings.tabPageASNSetting_VDAClientWorkspaceValue:
                        OpentCtr_ctrVDAClientWorkspaceValue();
                        break;
                    case enumTabPageASNSettings.tabPageASNSetting_ASNMessagesTest:
                        OpenCtr_ctrASNMessageTest();
                        break;
                    case enumTabPageASNSettings.tabPageASNSetting_tabPageImportEdifact:
                        OpenCtr_ctrImportEdifact();
                        break;
                    case enumTabPageASNSettings.tabPageASNSetting_VDAClientOutUpdate:
                        OpenCtr_ctrVdaClientOutUpdate();
                        break;

                    default:

                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitTabPage_AdrVerweise()
        {
            OpenCtr_ctrAdrVerweis();
        }
        /// <summary>
        ///             Öffnet das CtrAdrVerweis
        /// </summary>
        private void OpenCtr_ctrAdrVerweis()
        {
            if ((this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard))
            {
                _ctrAdrVerweis = new ctrAdrVerweis(this.asnWizz);
                _ctrAdrVerweis._ctrMenu = this._ctrMenu;
                _ctrAdrVerweis.Parent = this.tabPageASNSetting_AdrVerweise;
                _ctrAdrVerweis.Dock = DockStyle.Fill;
                _ctrAdrVerweis.Show();
                _ctrAdrVerweis.BringToFront();
            }
        }
        /// <summary>
        ///             Öffnet das CtrAdrVerweis
        /// </summary>
        private void OpenCtr_ctrOrga()
        {
            if ((this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard))
            {
                //_ctrAdrVerweis = null;
                _ctrOrga = new ctrOrga(this._ctrMenu, this.asnWizz);
                _ctrOrga.Parent = this.tabPageASNSetting_Orga;
                _ctrOrga.Dock = DockStyle.Fill;
                _ctrOrga.Show();
                _ctrOrga.BringToFront();
            }
        }
        /// <summary>
        ///             Öffnet das CtrAdrVerweis
        /// </summary>
        private void OpenCtr_ctrASNAction()
        {
            if ((this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard))
            {
                //_ctrAdrVerweis = null;
                _ctrASNAction = new ctrASNAction(this._ctrMenu, this.asnWizz);
                _ctrASNAction.Parent = this.tabPageASNSetting_ASNAction;
                _ctrASNAction.Dock = DockStyle.Fill;
                _ctrASNAction.Show();
                _ctrASNAction.BringToFront();
            }
        }
        /// <summary>
        ///             Öffnet das CtrJob
        /// </summary>
        private void OpentCtr_ctrJobs()
        {
            if ((this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard))
            {
                _ctrJob = new ctrJob(this._ctrMenu, this.asnWizz);
                _ctrJob.Parent = this.tabPageASNSetting_Jobs;
                _ctrJob.Dock = DockStyle.Fill;
                _ctrJob.Show();
                _ctrJob.BringToFront();
            }
        }
        /// <summary>
        ///             Öffnet das CtrVDAClientOut
        /// </summary>
        private void OpentCtr_ctrVDAClientOut()
        {
            if ((this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard))
            {
                _ctrVdaClientOut = new ctrVDAClientOut(this._ctrMenu, this.asnWizz);
                _ctrVdaClientOut.Parent = this.tabPageASNSetting_VDAClientOut;
                _ctrVdaClientOut.Dock = DockStyle.Fill;
                _ctrVdaClientOut.Show();
                _ctrVdaClientOut.BringToFront();
            }
        }
        /// <summary>
        ///             Öffnet das CtrEDIFACTClientOut
        /// </summary>
        private void OpentCtr_ctrEDIFACTClientOut()
        {
            if ((this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard))
            {
                _ctrEDIFACTClientOut = new ctrEDIFACTClientOut(this._ctrMenu, this.asnWizz);
                _ctrEDIFACTClientOut.Parent = this.tabPageASNSetting_EDIFACTClientOut;
                _ctrEDIFACTClientOut.Dock = DockStyle.Fill;
                _ctrEDIFACTClientOut.Show();
                _ctrEDIFACTClientOut.BringToFront();
            }
        }
        /// <summary>
        ///             Öffnet das CtrVDAClientOut
        /// </summary>
        private void OpentCtr_ctrASNArtFieldAssignment()
        {
            if ((this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard))
            {
                _ctrASNArtFieldAssignment = new ctrASNArtFieldAssignment(this._ctrMenu, this.asnWizz);
                _ctrASNArtFieldAssignment.Parent = this.tabPageASNSetting_ASNArtFieldAssignment;
                _ctrASNArtFieldAssignment.Dock = DockStyle.Fill;
                _ctrASNArtFieldAssignment.Show();
                _ctrASNArtFieldAssignment.BringToFront();
            }
        }
        /// <summary>
        ///             Öffnet das CtrVDAClientWorkspaceValue
        /// </summary>
        private void OpentCtr_ctrVDAClientWorkspaceValue()
        {
            if ((this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard))
            {
                _ctrVdaClientWorkspaceValue = new ctrVDAClientWorkspaceValue(this._ctrMenu, this.asnWizz);
                _ctrVdaClientWorkspaceValue.Parent = this.tabPageASNSetting_VDAClientWorkspaceValue;
                _ctrVdaClientWorkspaceValue.Dock = DockStyle.Fill;
                _ctrVdaClientWorkspaceValue.Show();
                _ctrVdaClientWorkspaceValue.BringToFront();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void OpenCtr_ctrASNMessageTest()
        {
            if ((this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard))
            {
                _ctrASNMessageTest = new ctrASNMessage(this._ctrMenu, this.asnWizz);
                _ctrASNMessageTest.Parent = this.tabPageASNSetting_ASNMessagesTest;
                _ctrASNMessageTest.Dock = DockStyle.Fill;
                _ctrASNMessageTest.Show();
                _ctrASNMessageTest.BringToFront();
            }
        }

        private void OpenCtr_ctrImportEdifact()
        {
            if (this._ctrMenu is ctrMenu)
            {
                _ctrEdifactImport = new ctrEdifactImport();
                _ctrEdifactImport.GLUser = this._ctrMenu.GL_User;
                _ctrEdifactImport.Parent = this.tabPageASNSetting_tabPageImportEdifact;
                _ctrEdifactImport.Dock = DockStyle.Fill;
                _ctrEdifactImport.InitCtr();
                _ctrEdifactImport.Show();
                _ctrEdifactImport.BringToFront();
            }
        }

        private void OpenCtr_ctrVdaClientOutUpdate()
        {
            if (this._ctrMenu is ctrMenu)
            {
                _ctrVdaClientUpdate = new ctrVDAClientOutUpdate(this);
                _ctrVdaClientUpdate.GLUser = this._ctrMenu.GL_User;
                _ctrVdaClientUpdate.Parent = this.tabPageASNSetting_VDAClientOutUpdate;
                _ctrVdaClientUpdate.Dock = DockStyle.Fill;
                _ctrVdaClientUpdate.InitCtr();
                _ctrVdaClientUpdate.Show();
                _ctrVdaClientUpdate.BringToFront();
            }
        }
        /// <summary>
        ///             Ermittelt alle Daten der selektierten Adresse und wechselt auf die erste Tabpage 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            StartSearch();
        }
        /// <summary>
        /// 
        /// </summary>
        private void StartSearch()
        {
            //pageViewASNSetting.SelectedPage = null;
            if (pageViewASNSetting.SelectedPage == tabPageASNSetting_AdrVerweise)
            {
                pageViewASNSetting.SelectedPage = tabPageASNSetting_ASNAction;
            }
            pageViewASNSetting.SelectedPage = tabPageASNSetting_AdrVerweise;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseMain_Click(object sender, EventArgs e)
        {
            this._ctrMenu._frmMain.AdminCockpit.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudAdrIdDirect_Leave(object sender, EventArgs e)
        {
            if (nudAdrIdDirect.Value > 0)
            {
                TakeOverAdrID(nudAdrIdDirect.Value);
            }
        }

        private void comboASNArt_SelectedValueChanged(object sender, EventArgs e)
        {
            int iTmp = 0;
            int.TryParse(comboASNArt.SelectedValue.ToString(), out iTmp);
            if ((iTmp > 0) && (this.asnWizz != null))
            {
                this.asnWizz.AsnArt = new clsASNArt();
                //this.asnWizz.AsnArt.InitClass(ref _ctrMenu._frmMain.GL_User, new clsSQLCOM());
                this.asnWizz.AsnArt.ID = iTmp;
                this.asnWizz.AsnArt.Fill();
            }
        }

        private void nudAdrIdDirect_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
