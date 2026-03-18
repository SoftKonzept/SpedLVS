using LVS;
using System;
using System.Windows.Forms;



namespace Sped4.Controls.AdminCockpit
{
    public partial class ctrCronJobs : UserControl
    {
        internal ctrMenu _ctrMenu;
        internal clsCronJobs cJob;
        public int SearchButton;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myMenu"></param>
        public ctrCronJobs(ctrMenu myMenu)
        {
            InitializeComponent();
            this._ctrMenu = myMenu;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrCronJobs_Load(object sender, EventArgs e)
        {
            //-- combos füllen
            //---- Aktionen
            comboAction.DataSource = Enum.GetValues(typeof(enumCronJobAction));
            //--- Perioden
            comboPeriode.DataSource = Enum.GetValues(typeof(enumPeriode));
            ClearInputFields();
            SetInputFieldsEnabled(false);
            InitDgv();
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClearInputFields()
        {
            tbCronJobId.Text = "0";
            comboAction.SelectedIndex = -1;
            tbBeschreibung.Text = string.Empty;
            comboPeriode.SelectedIndex = -1;
            dtpActionDate.MinDate = DateTime.Now; // DateTime.MinValue;
            dtpActionVon.MinDate = DateTime.Now.AddDays(-1); // DateTime.MinValue;
            dtpActionBis.MinDate = DateTime.Now.AddDays(1); //DateTime.MinValue;
            nudAdrIdDirect.Value = 0;
            tbAuftraggeber.Text = string.Empty;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitDgv();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgv()
        {
            this.dgvCronJob.DataSource = clsCronJobs.GetCronJobList(this._ctrMenu.GL_User);
            this.dgvCronJob.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCronJob_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvCronJob.Rows.Count > 0)
            {
                this.dgvCronJob.CurrentRow.IsSelected = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCronJob_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dgvCronJob.RowCount > 0)
            {
                if (this.dgvCronJob.SelectedRows.Count > 0)
                {
                    cJob = new clsCronJobs();
                    cJob.InitClass(this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.system);
                    cJob.ID = decimal.Parse(dgvCronJob.SelectedRows[0].Cells["ID"].Value.ToString());
                    cJob.Fill();

                    ClearInputFields();
                    SetInputFieldsEnabled(true);
                    SetCronJobValuToCtr();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bEnabled"></param>
        private void SetInputFieldsEnabled(bool bEnabled)
        {
            tbCronJobId.Enabled = bEnabled;
            comboAction.Enabled = bEnabled;
            tbBeschreibung.Enabled = bEnabled;
            comboPeriode.Enabled = bEnabled;
            dtpActionDate.Enabled = bEnabled;
            dtpActionVon.Enabled = bEnabled;
            dtpActionBis.Enabled = bEnabled;
            nudAdrIdDirect.Enabled = bEnabled;
            btnSearchA.Enabled = bEnabled;
            cbAktiv.Enabled = bEnabled;
            tbAuftraggeber.Enabled = bEnabled;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetCronJobValuToCtr()
        {
            tbCronJobId.Text = this.cJob.ID.ToString();
            Functions.SetComboToSelecetedValue(ref comboAction, cJob.Aktion.ToString());
            Functions.SetComboToSelecetedValue(ref comboPeriode, cJob.Periode.ToString());
            tbBeschreibung.Text = cJob.Beschreibung;
            DateTime dtTmp = DateTime.MinValue;
            DateTime.TryParse(cJob.Aktionsdatum.ToString(), out dtTmp);
            dtpActionDate.Value = dtTmp;
            dtTmp = DateTime.MinValue;
            DateTime.TryParse(cJob.vZeitraum.ToString(), out dtTmp);
            dtpActionVon.Value = dtTmp;
            dtTmp = DateTime.MinValue;
            DateTime.TryParse(cJob.bZeitraum.ToString(), out dtTmp);
            dtpActionBis.Value = dtTmp;
            cbAktiv.Checked = cJob.aktiv;
            TakeOverAdrID(cJob.AdrId);
        }
        /// <summary>
        ///             Neuen Cronjob anlegen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnCronJobNew_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            SetInputFieldsEnabled(true);
            this.cJob = new clsCronJobs();
            cJob.InitClass(this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.system);
        }
        /// <summary>
        ///             Save Cronjob Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            cJob.Aktion = (enumCronJobAction)comboAction.SelectedItem;
            cJob.aktiv = cbAktiv.Checked;
            cJob.Beschreibung = tbBeschreibung.Text.Trim();
            cJob.Aktionsdatum = dtpActionDate.Value;
            cJob.vZeitraum = dtpActionVon.Value;
            cJob.bZeitraum = dtpActionBis.Value;
            cJob.Periode = comboPeriode.Text;
            cJob.AdrId = (int)nudAdrIdDirect.Value;

            //if (this.cJob.ID > 0)
            //{

            if (cJob.ID > 0)
            {
                cJob.Update();
            }
            else
            {
                cJob.Add();
            }
            ClearInputFields();
            SetInputFieldsEnabled(false);
            InitDgv();
            //}
            //else
            //{
            //    clsMessages.Allgemein_KeinDatensatzAusgewaehlt();
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            if (this.cJob.ID > 0)
            {
                if (clsMessages.DeleteAllgemein())
                {
                    if (this.cJob.Delete())
                    {
                        ClearInputFields();
                        SetInputFieldsEnabled(false);
                        InitDgv();
                    }
                }
            }
            else
            {
                clsMessages.Allgemein_KeinDatensatzAusgewaehlt();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCronJob_SelectionChanged(object sender, EventArgs e)
        {
            this.cJob = new clsCronJobs();
            ClearInputFields();
            SetInputFieldsEnabled(false);
        }

        private void nudAdrIdDirect_Leave(object sender, EventArgs e)
        {
            if (nudAdrIdDirect.Value > 0)
            {
                TakeOverAdrID(nudAdrIdDirect.Value);
            }
        }
        ///<summary>ctrASNMain/TakeOverAdrID</summary>
        ///<remarks>Übergabe der gesuchten AdrId</remarks>
        public void TakeOverAdrID(decimal myDecADR_ID)
        {
            this.SearchButton = 1;
            LVS.ViewData.AddressViewData adrVD = new LVS.ViewData.AddressViewData((int)myDecADR_ID, (int)this._ctrMenu.GL_User.User_ID);
            nudAdrIdDirect.Value = myDecADR_ID;
            tbAuftraggeber.Text = adrVD.Address.AddressStringShort;
        }

        private void btnSearchA_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            _ctrMenu.OpenADRSearch(this);
        }
    }
}
