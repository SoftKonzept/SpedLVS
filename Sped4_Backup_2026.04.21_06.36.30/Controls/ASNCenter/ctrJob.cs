using LVS;
using LVS.ASN;
using LVS.Enumerations;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrJob : UserControl
    {
        internal clsASNWizzard asnWizz;
        internal ctrMenu _ctrMenu;
        public int SearchButton = 0;
        internal ctrJobSelectToCopy _ctrJobSelectToCopy;
        /// <summary>
        /// 
        /// </summary>
        public ctrJob(ctrMenu myMenu, clsASNWizzard myAsnWizz)
        {
            InitializeComponent();
            _ctrMenu = myMenu;
            asnWizz = myAsnWizz;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrJob_Load(object sender, EventArgs e)
        {
            if (
                (this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard)
              )
            {
                //-- comboASNFileTyp
                comboASNFileTyp.DataSource = clsASNArt.GetASNArtList(this._ctrMenu._frmMain.GL_User.User_ID);
                comboASNFileTyp.DisplayMember = "Typ";
                comboASNFileTyp.ValueMember = "ID";


                //-- comboDirection
                comboDirection.DataSource = Enum.GetValues(typeof(Directions));

                //-- comboASNTyp
                comboASNTyp.DataSource = clsASNTyp.GetASNTypList(this._ctrMenu._frmMain.GL_User.User_ID);
                comboASNTyp.ValueMember = "TypID";
                comboASNTyp.DisplayMember = "Typ";

                //-- comboDirection
                comboPost.DataSource = Enum.GetValues(typeof(enumPostBy));

                //comboArbeitsbereich
                comboArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(1);
                comboArbeitsbereich.DisplayMember = "Arbeitsbereich";
                comboArbeitsbereich.ValueMember = "ID";

                //-- combo Mandanden
                comboMandant.DataSource = clsMandanten.GetMandatenList(1);
                comboMandant.DisplayMember = "Matchcode";
                comboMandant.ValueMember = "Mandanten_ID";

                //--- combo Periode
                comboPeriode.DataSource = Enum.GetValues(typeof(enumPeriode));

                //-- combo EinheitLM
                comboEinheitLM.DataSource = Enum.GetValues(typeof(enumEinheitLM));

                InitDGV();
            }
        }
        /// <summary>
        ///             Setzt die Datasource für das dgvJobs
        /// </summary>
        private void InitDGV()
        {
            ClearInputFields();
            SetInputFieldsEnabled(false);
            this.dgvJobs.DataSource = this.asnWizz.Jobs.dtJobsByAdrVerweis;
            this.dgvJobs.BestFitColumns();

        }
        /// <summary>
        ///             Läd die akteullen Daten der Datenbank
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJobRefresh_Click(object sender, EventArgs e)
        {
            InitDGV();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bEnabled"></param>
        private void SetInputFieldsEnabled(bool bEnabled)
        {
            comboASNFileTyp.Enabled = bEnabled;
            comboDirection.Enabled = bEnabled;
            tbPath.Enabled = bEnabled;
            tbFileName.Enabled = bEnabled;
            tbSearchFileName.Enabled = bEnabled;
            comboASNTyp.Enabled = bEnabled;
            comboPost.Enabled = bEnabled;
            tbFTPPass.Enabled = bEnabled;
            tbFTPServer.Enabled = bEnabled;
            tbFTPUser.Enabled = bEnabled;
            tbFTPFilename.Enabled = bEnabled;
            comboArbeitsbereich.Enabled = bEnabled;
            comboMandant.Enabled = bEnabled;
            comboEinheitLM.Enabled = bEnabled;
            dtpValidFrom.Enabled = bEnabled;
            tbVDA4905Verweis.Enabled = bEnabled;
            tbDelforVerweise.Enabled = bEnabled;
            cbActiv.Enabled = bEnabled;
            cbCTRLF.Enabled = bEnabled;
            cbOdetteStart.Enabled = bEnabled;
            tbCheckTransferPath.Enabled = bEnabled;
            tbStoreFilePath.Enabled = bEnabled;
            tbErrorPath.Enabled = bEnabled;
            tbTransferFileName.Enabled = bEnabled;
            comboPeriode.Enabled = bEnabled;
            tbProcessViewName.Enabled = bEnabled;
            dtpActionDate.Enabled = bEnabled;
            tpActionTime.Enabled = bEnabled;
        }
        /// <summary>
        ///             Reset der Eingabefelder
        /// </summary>
        private void ClearInputFields()
        {
            lJobId.Text = "0";
            comboASNFileTyp.SelectedIndex = -1;
            comboDirection.SelectedIndex = -1;
            tbPath.Text = string.Empty;
            tbFileName.Text = string.Empty;
            tbSearchFileName.Text = string.Empty;
            comboASNTyp.SelectedIndex = -1;
            comboPost.SelectedIndex = -1;
            tbFTPPass.Text = string.Empty;
            tbFTPServer.Text = string.Empty;
            tbFTPUser.Text = string.Empty;
            tbFTPFilename.Text = string.Empty;
            comboArbeitsbereich.SelectedIndex = -1;
            comboMandant.SelectedIndex = -1;
            comboEinheitLM.SelectedIndex = -1;
            dtpValidFrom.Value = dtpValidFrom.MinDate;
            tbVDA4905Verweis.Text = string.Empty;
            tbDelforVerweise.Text = string.Empty;
            cbActiv.Checked = false;
            cbCTRLF.Checked = false;
            cbOdetteStart.Checked = false;
            tbCheckTransferPath.Text = string.Empty;
            tbStoreFilePath.Text = string.Empty;
            tbErrorPath.Text = string.Empty;
            tbTransferFileName.Text = string.Empty;
            comboPeriode.SelectedIndex = -1;
            tbCloneFilePath.Text = string.Empty;
            tbCloneFileName.Text = string.Empty;
            cbCheckCloneFileAndTransfer.Checked = false;
            tbProcessViewName.Text = string.Empty;
            Functions.SetComboToSelecetedValue(ref comboPeriode, enumPeriode.NotSet.ToString());
            dtpActionDate.Value = Globals.DefaultDateTimeMinValue;
            tpActionTime.Value = Globals.DefaultDateTimeMinValue;
        }
        /// <summary>
        ///             Eingabefelder mit Werte der Klasse Job füllen
        /// </summary>
        private void SetJobClassValueToCtr()
        {
            if (this.asnWizz.Jobs is clsJobs)
            {
                lJobId.Text = this.asnWizz.Jobs.ID.ToString();
                //Functions.SetComboToSelecetedValue(ref comboASNFileTyp, this.asnWizz.Jobs.ASNFileTyp.ToString());
                Functions.SetComboToSelecetedValue(ref comboASNFileTyp, this.asnWizz.Jobs.ASNArtID.ToString());
                Functions.SetComboToSelecetedValue(ref comboDirection, this.asnWizz.Jobs.Direction.ToString());

                tbPath.Text = this.asnWizz.Jobs.Path;
                tbFileName.Text = this.asnWizz.Jobs.FileName;
                tbSearchFileName.Text = this.asnWizz.Jobs.SearchFileName;
                Functions.SetComboToSelecetedValue(ref comboASNTyp, this.asnWizz.Jobs.ASNTypID.ToString());
                Functions.SetComboToSelecetedValue(ref comboPost, this.asnWizz.Jobs.PostBy.ToString());
                tbFTPPass.Text = this.asnWizz.Jobs.FTPPass;
                tbFTPServer.Text = this.asnWizz.Jobs.FTPServer;
                tbFTPUser.Text = this.asnWizz.Jobs.FTPUser;
                tbFTPFilename.Text = this.asnWizz.Jobs.FTPFileName;
                Functions.SetComboToSelecetedValue(ref comboArbeitsbereich, this.asnWizz.Jobs.ArbeitsbereichID.ToString());
                Functions.SetComboToSelecetedValue(ref comboMandant, this.asnWizz.Jobs.MandantenID.ToString());
                Functions.SetComboToSelecetedValue(ref comboEinheitLM, this.asnWizz.Jobs.EinheitLM.ToString());
                tbVDA4905Verweis.Text = this.asnWizz.Jobs.VerweisVDA4905;
                tbDelforVerweise.Text = this.asnWizz.Jobs.DelforVerweis;
                dtpValidFrom.Value = this.asnWizz.Jobs.ValidFrom;
                cbActiv.Checked = this.asnWizz.Jobs.activ;
                cbCTRLF.Checked = this.asnWizz.Jobs.UseCRLF;
                cbOdetteStart.Checked = this.asnWizz.Jobs.CreateOdetteStart;
                tbCheckTransferPath.Text = this.asnWizz.Jobs.CheckTransferPath;
                tbStoreFilePath.Text = this.asnWizz.Jobs.ASNFileStorePathDirectory;
                tbErrorPath.Text = this.asnWizz.Jobs.ErrorPath;
                tbTransferFileName.Text = this.asnWizz.Jobs.TransferFileName;
                Functions.SetComboToSelecetedValue(ref comboPeriode, this.asnWizz.Jobs.Periode.ToString());
                //comboPeriode.SelectedIndex = -1;
                tbCloneFilePath.Text = this.asnWizz.Jobs.CheckCloneFilePath;
                tbCloneFileName.Text = this.asnWizz.Jobs.CheckCloneFileName;
                cbCheckCloneFileAndTransfer.Checked = this.asnWizz.Jobs.CheckCloneFile;

                tbProcessViewName.Text = this.asnWizz.Jobs.ViewProzessName;
                dtpActionDate.Value = this.asnWizz.Jobs.ActionDate.Date;
                tpActionTime.Value = this.asnWizz.Jobs.ActionDate;

            }
        }
        /// <summary>
        ///             Zuweisung der Eingabefelder der Klasse
        /// </summary>
        private void SetValueToJobCls()
        {
            //this.asnWizz.Jobs.ASNFileTyp = comboASNFileTyp.SelectedText.ToString();
            int iTmp = 0;
            int.TryParse(comboASNFileTyp.SelectedValue.ToString(), out iTmp);
            this.asnWizz.Jobs.ASNArtID = iTmp;

            DataRowView selectedRow = (DataRowView)comboASNFileTyp.SelectedItem;
            this.asnWizz.Jobs.ASNFileTyp = selectedRow["Typ"].ToString();

            iTmp = 0;
            int.TryParse(comboASNTyp.SelectedValue.ToString(), out iTmp);
            this.asnWizz.Jobs.ASNTypID = iTmp;

            this.asnWizz.Jobs.Direction = comboDirection.SelectedValue.ToString();
            this.asnWizz.Jobs.Path = tbPath.Text.Trim();
            this.asnWizz.Jobs.FileName = tbFileName.Text.Trim();
            this.asnWizz.Jobs.SearchFileName = tbSearchFileName.Text.Trim();
            this.asnWizz.Jobs.PostBy = comboPost.SelectedValue.ToString();
            this.asnWizz.Jobs.FTPPass = tbFTPPass.Text.Trim();
            this.asnWizz.Jobs.FTPServer = tbFTPServer.Text.Trim();
            this.asnWizz.Jobs.FTPUser = tbFTPUser.Text.Trim();
            this.asnWizz.Jobs.FTPFileName = tbFTPFilename.Text.Trim();
            this.asnWizz.Jobs.ArbeitsbereichID = (decimal)comboArbeitsbereich.SelectedValue;
            this.asnWizz.Jobs.MandantenID = (decimal)comboMandant.SelectedValue;
            this.asnWizz.Jobs.EinheitLM = comboEinheitLM.SelectedText;
            this.asnWizz.Jobs.ValidFrom = dtpValidFrom.Value;
            this.asnWizz.Jobs.VerweisVDA4905 = tbVDA4905Verweis.Text.Trim();
            this.asnWizz.Jobs.DelforVerweis = tbDelforVerweise.Text.Trim();
            this.asnWizz.Jobs.activ = cbActiv.Checked;
            this.asnWizz.Jobs.UseCRLF = cbCTRLF.Checked;
            this.asnWizz.Jobs.CreateOdetteStart = cbOdetteStart.Checked;
            this.asnWizz.Jobs.ASNFileStorePath = tbStoreFilePath.Text.Trim();
            this.asnWizz.Jobs.ErrorPath = tbErrorPath.Text.Trim();
            this.asnWizz.Jobs.TransferFileName = tbTransferFileName.Text.Trim();
            this.asnWizz.Jobs.CheckTransferPath = tbCheckTransferPath.Text.Trim();
            if (comboPeriode.SelectedValue != null)
            {
                this.asnWizz.Jobs.Periode = comboPeriode.SelectedValue.ToString();
            }
            else
            {
                this.asnWizz.Jobs.Periode = enumPeriode.NotSet.ToString();
            }
            this.asnWizz.Jobs.ViewProzessName = tbProcessViewName.Text;
            this.asnWizz.Jobs.CheckCloneFilePath = tbCloneFilePath.Text.Trim();
            this.asnWizz.Jobs.CheckCloneFileName = tbCloneFileName.Text.Trim();
            this.asnWizz.Jobs.CheckCloneFile = cbCheckCloneFileAndTransfer.Checked;
            //DateTime dtTmp = dtpActionDate.Value;
            DateTime dtTime = (DateTime)tpActionTime.Value;
            //TimeSpan tsTmp = new TimeSpan(dtTime.Hour, dtTime.Minute, 0);
            this.asnWizz.Jobs.ActionDate = LVS.Helper.helper_DateTimeFunctions.AddTimeToDate(dtpActionDate.Value, new TimeSpan(dtTime.Hour, dtTime.Minute, 0));
        }
        /// <summary>
        ///             Setzt die Current Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvJobs_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvJobs.Rows.Count > 0)
            {
                this.dgvJobs.CurrentRow.IsSelected = true;
            }
        }
        /// <summary>
        ///             Setzt die ausgewählten Werte in die Eingabefelder 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvJobs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvJobs.RowCount > 0)
            {
                if (dgvJobs.SelectedRows.Count > 0)
                {
                    decimal decTmp = 0;
                    decimal.TryParse(dgvJobs.SelectedRows[0].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        this.asnWizz.Jobs.ID = decTmp;
                        this.asnWizz.Jobs.Fill();
                        ClearInputFields();
                        SetInputFieldsEnabled(true);
                        SetJobClassValueToCtr();
                    }
                }
            }
        }
        /// <summary>
        ///             Neuen Datensatz anlegen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJobsAdd_Click(object sender, EventArgs e)
        {
            this.asnWizz.Jobs = new clsJobs();
            this.asnWizz.Jobs.InitClass(this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.GL_User, false);
            this.asnWizz.Jobs.AdrVerweisID = this.asnWizz.AuftragggeberAdr.ID;
            ClearInputFields();
            SetInputFieldsEnabled(true);
        }
        /// <summary>
        ///             Datensatz speichern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJobsSave_Click(object sender, EventArgs e)
        {
            if (
                    (this.asnWizz.Jobs is clsJobs) &&
                    (comboASNFileTyp.SelectedIndex > -1) &&
                    (comboDirection.SelectedIndex > -1) &&
                    (comboASNTyp.SelectedIndex > -1) &&
                    (comboPost.SelectedIndex > -1) &&
                    (comboArbeitsbereich.SelectedIndex > -1) &&
                    (comboMandant.SelectedIndex > -1)
               //&& (comboEinheitLM.SelectedIndex > -1)
               )
            {
                SetValueToJobCls();
                if (this.asnWizz.Jobs.ID > 0)
                {
                    this.asnWizz.Jobs.Update();
                }
                else
                {
                    this.asnWizz.Jobs.Add();
                }
                ClearInputFields();
                SetInputFieldsEnabled(false);
                InitDGV();
            }
            else
            {
                string strError = "FEHLER - Bitte prüfen Sie die Eingaben!";
                clsMessages.Allgemein_ERRORTextShow(strError);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJobsDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                string strTxt = string.Empty;
                if (this.asnWizz.Jobs.Delete())
                {
                    strTxt = "Der Datensatz wurde gelöscht!";
                }
                else
                {
                    strTxt = "Der Lösch-Vorgang konnte nicht durchgeführt werden!";
                }
                clsMessages.Allgemein_InfoTextShow(strTxt);
                InitDGV();
            }
        }
        /// <summary>
        ///             Legt eine Kopie des ausgewählten Datensätzes neu an.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJobsCopy_Click(object sender, EventArgs e)
        {
            if (this.asnWizz.Jobs is clsJobs)
            {
                this.asnWizz.Jobs.ID = 0;
                if (this.asnWizz.Jobs.ID == 0)
                {
                    this.asnWizz.Jobs.Add();
                    ClearInputFields();
                    SetInputFieldsEnabled(false);
                    InitDGV();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSelectedJobRange_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            //this.IsReceiverSearch = false;
            _ctrMenu.OpenADRSearch(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDecADR_ID"></param>
        public void TakeOverAdrID(decimal myDecADR_ID)
        {
            this.asnWizz.Jobs.AdrVerweisID = myDecADR_ID;
            this._ctrJobSelectToCopy = new ctrJobSelectToCopy(this._ctrMenu, this.asnWizz);
            this._ctrMenu.OpenFrmTMP(this._ctrJobSelectToCopy);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboASNFileTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboASNFileTyp.SelectedIndex > 0)
            {
                string str = string.Empty;
                int iAsnArtId = 0;
                int.TryParse(comboASNFileTyp.SelectedValue.ToString(), out iAsnArtId);
                this.asnWizz.Jobs.ASNArtID = iAsnArtId;

                DataRowView selectedRow = (DataRowView)comboASNFileTyp.SelectedItem;
                this.asnWizz.Jobs.ASNFileTyp = selectedRow["Typ"].ToString();
            }
        }
    }
}
