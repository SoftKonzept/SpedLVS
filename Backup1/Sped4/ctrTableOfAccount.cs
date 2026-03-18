using LVS;
using LVS.Helper;
using System;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrTableOfAccount : UserControl
    {

        internal ctrMenu _ctrMenu;
        internal Globals._GL_USER _GL_User;
        internal clsTableOfAccount Kontenplan;
        internal bool bUpdate;
        internal Int32 iWidthList;
        internal Int32 iWidthEdit;



        ///<summary>ctrTableOfAccount / ctrTableOfAccount</summary>
        ///<remarks></remarks>
        public ctrTableOfAccount()
        {
            InitializeComponent();

            iWidthList = this.splitPanel1.Width + 10;
            iWidthEdit = this.splitPanel2.Width + 10;
            //ExtraCharge Edit Panel soll eingeklappt sein
            this.splitPanel2.Collapsed = true;
            ResetCtrTableOfAccountWidth();

            cbEditKontoText.DataSource = helper_Kontoarten.List_KontoText();
        }
        ///<summary>ctrTableOfAccount / ctrTableOfAccount_Load</summary>
        ///<remarks></remarks>
        private void ctrTableOfAccount_Load(object sender, EventArgs e)
        {
            InitDGVTableOfAccount();
        }
        ///<summary>ctrTableOfAccount / InitGlobals</summary>
        ///<remarks></remarks>
        public void InitGlobals(ctrMenu myCtrMenu)
        {
            if (myCtrMenu != null)
            {
                this._ctrMenu = myCtrMenu;
                this._GL_User = this._ctrMenu.GL_User;

                Kontenplan = new clsTableOfAccount();
                Kontenplan.InitClass(this._GL_User);
            }
        }
        ///<summary>ctrTableOfAccount / ResetCtrADRListWidth</summary>
        ///<remarks>Anpassen der Ctr-Breite</remarks>
        private void ResetCtrTableOfAccountWidth()
        {
            if (this.splitPanel2.Collapsed)
            {
                this.Width = iWidthList + 10;
            }
            else
            {
                this.Width = iWidthList + iWidthEdit + 10;
            }
            this.Refresh();
        }
        ///<summary>ctrTableOfAccount / ShowAndHideTableOfAccountsEdit</summary>
        ///<remarks></remarks>
        public void ShowAndHideTableOfAccountsEdit()
        {
            if (this.splitPanel2.Collapsed == true)
            {
                this.splitPanel2.Collapsed = false;
            }
            else
            {
                this.splitPanel2.Collapsed = true;
            }
            ResetCtrTableOfAccountWidth();
            SettsbtnOpenEditImage();
        }
        ///<summary>ctrTableOfAccount / SettsbtnOpenEditImage</summary>
        ///<remarks></remarks>
        private void SettsbtnOpenEditImage()
        {
            if (this.splitPanel2.Collapsed == true)
            {
                this.tsbtnLayoutOpen.Image = Sped4.Properties.Resources.layout_left;
            }
            else
            {
                this.tsbtnLayoutOpen.Image = Sped4.Properties.Resources.layout;
            }
        }
        ///<summary>ctrTableOfAccount / tsbtnLayoutOpen_Click</summary>
        ///<remarks></remarks>
        private void tsbtnLayoutOpen_Click(object sender, EventArgs e)
        {
            ShowAndHideTableOfAccountsEdit();
        }
        ///<summary>ctrTableOfAccount / tsbtnTableOfAccoutAdd_Click</summary>
        ///<remarks></remarks>
        private void tsbtnTableOfAccoutAdd_Click(object sender, EventArgs e)
        {
            bUpdate = false;
            this.splitPanel2.Collapsed = true;
            InitTableOfAccountEdit();
            SetTableOFAccountEditInputFieldEnabled(true);
            ShowAndHideTableOfAccountsEdit();
        }
        ///<summary>ctrTableOfAccount / tsbtnTableOfAccountChange_Click</summary>
        ///<remarks></remarks>
        private void tsbtnTableOfAccountChange_Click(object sender, EventArgs e)
        {
            bUpdate = true;
            this.splitPanel2.Collapsed = false;
            ResetCtrTableOfAccountWidth();
            InitTableOfAccountEdit();
            SetTableOFAccountEditInputFieldEnabled(true);
            SetTableOfAccountDatenToCtr();
        }
        ///<summary>ctrTableOfAccount / ctrTableOfAccount_Load</summary>
        ///<remarks></remarks>
        private void tsbtnTableOfAccountExcel_Click(object sender, EventArgs e)
        {
            string strFileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_Kontenplan.xls";
            saveFileDialog.FileName = strFileName;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            strFileName = this.saveFileDialog.FileName;
            bool openExportFile = false;

            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgvTableOfAccounts, strFileName, ref openExportFile, this._GL_User, true);

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(strFileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this._GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "Fibu-Kontenplan - LIST - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        ///<summary>ctrTableOfAccount / ctrTableOfAccount_Load</summary>
        ///<remarks></remarks>
        private void tsbtnTableOfAccountRefresh_Click(object sender, EventArgs e)
        {
            InitDGVTableOfAccount();
        }
        ///<summary>ctrTableOfAccount / ctrTableOfAccount_Load</summary>
        ///<remarks></remarks>
        private void tsbtnTableOfAccountDelete_Click(object sender, EventArgs e)
        {
            DeleteTableOfAccountItem();
        }
        ///<summary>ctrTableOfAccount / ctrTableOfAccount_Load</summary>
        ///<remarks></remarks>
        private void tsbtnTableOfAccountClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrTableOfAccount();
        }
        ///<summary>ctrTableOfAccount / DeleteTableOfAccountItem</summary>
        ///<remarks></remarks>
        private void DeleteTableOfAccountItem()
        {
            if (this.Kontenplan.ID > 0)
            {
                if (clsMessages.DeleteAllgemein())
                {
                    this.Kontenplan.Delete();
                    this.splitPanel2.Collapsed = true;
                    ResetCtrTableOfAccountWidth();
                    InitTableOfAccountEdit();
                }
            }
        }
        ///<summary>ctrTableOfAccount / dgvTableOfAccounts_MouseClick</summary>
        ///<remarks></remarks>
        private void dgvTableOfAccounts_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvTableOfAccounts.Rows.Count > 0)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvTableOfAccounts.Rows[this.dgvTableOfAccounts.CurrentRow.Index].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    Kontenplan.ID = decTmp;
                    Kontenplan.Fill();
                    this.dgvTableOfAccounts.CurrentRow.IsSelected = true;
                }
            }
        }
        ///<summary>ctrTableOfAccount / dgvTableOfAccounts_MouseDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvTableOfAccounts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dgvTableOfAccounts.Rows.Count > 0)
            {
                if (this.Kontenplan.ID > 0)
                {
                    InitDGVTableOfAccount();
                    SetTableOFAccountEditInputFieldEnabled(true);
                    SetTableOfAccountDatenToCtr();
                    this.splitPanel2.Collapsed = false;
                    SettsbtnOpenEditImage();
                    ResetCtrTableOfAccountWidth();
                }
                bUpdate = true;
            }
        }
        ///<summary>ctrTableOfAccount / InitDGVTableOfAccount</summary>
        ///<remarks></remarks>
        private void InitDGVTableOfAccount()
        {
            Kontenplan.FillDataTableOfAccount();
            this.dgvTableOfAccounts.DataSource = Kontenplan.dtTableOfAccount;
            for (Int32 i = 0; i <= this.dgvTableOfAccounts.Columns.Count - 1; i++)
            {
                string colName = this.dgvTableOfAccounts.Columns[i].Name.ToString();
                switch (colName)
                {
                    case "ID":
                    case "erstellt":
                        this.dgvTableOfAccounts.Columns[i].IsVisible = false;
                        break;
                }
                this.dgvTableOfAccounts.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;
            }
            //SetSelected and Current Row
            for (Int32 i = 0; i <= this.dgvTableOfAccounts.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(this.dgvTableOfAccounts.Rows[i].Cells["ID"].Value.ToString(), out decTmp);
                if (decTmp == Kontenplan.ID)
                {
                    this.dgvTableOfAccounts.Rows[i].IsSelected = true;
                    this.dgvTableOfAccounts.Rows[i].IsCurrent = true;
                }
            }
            //dgvTableOfAccounts.BestFitColumns(); // CF
            dgvTableOfAccounts.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

        }
        /**************************************************************************************
         *                  TableOfAccountEdit
         * ***********************************************************************************/
        ///<summary>ctrTableOfAccount / InitTableOfAccountEdit</summary>
        ///<remarks></remarks>
        private void InitTableOfAccountEdit()
        {
            ClearTableOfAccountEditInputFields();
            SetTableOFAccountEditInputFieldEnabled(true);
            InitDGVTableOfAccount();
        }
        ///<summary>ctrTableOfAccount / SetTableOFAccountEditInputFieldEnabled</summary>
        ///<remarks></remarks>
        private void SetTableOFAccountEditInputFieldEnabled(bool bEnabled)
        {
            nudEditKontoNr.Enabled = bEnabled;
            cbEditKontoText.Enabled = bEnabled;
            tbEditBeschreibung.Enabled = bEnabled;
            dtpEditBis.Enabled = bEnabled;
        }
        ///<summary>ctrTableOfAccount / ClearTableOfAccountEditInputFields</summary>
        ///<remarks></remarks>
        private void ClearTableOfAccountEditInputFields()
        {
            decimal decTmp = 0;
            nudEditKontoNr.Value = decTmp;
            cbEditKontoText.SelectedIndex = -1;
            tbEditBeschreibung.Text = string.Empty;
            dtpEditBis.Value = DateTime.Now;
        }
        ///<summary>ctrTableOfAccount / SetTableOfAccountDatenToCtr</summary>
        ///<remarks></remarks>
        private void SetTableOfAccountDatenToCtr()
        {
            decimal decTmp = 0;
            Decimal.TryParse(Kontenplan.KontoNr.ToString(), out decTmp);
            nudEditKontoNr.Value = decTmp;
            Functions.SetComboToSelecetedValue(ref cbEditKontoText, Kontenplan.KontoText);
            tbEditBeschreibung.Text = Kontenplan.Beschreibung;
            dtpEditBis.Value = Kontenplan.GueltigBis;
        }
        ///<summary>ctrTableOfAccount / tsbtnEditAdd_Click</summary>
        ///<remarks></remarks>
        private void tsbtnEditAdd_Click(object sender, EventArgs e)
        {
            bUpdate = false;
            InitTableOfAccountEdit();
        }
        ///<summary>ctrTableOfAccount / tsbtnEditSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnEditSave_Click(object sender, EventArgs e)
        {
            decimal decTmpKontenPlanID = this.Kontenplan.ID;
            Kontenplan = new clsTableOfAccount();
            Kontenplan.InitClass(this._GL_User);

            Kontenplan.KontoNr = (Int32)nudEditKontoNr.Value;
            Kontenplan.KontoText = cbEditKontoText.Text;
            Kontenplan.Beschreibung = tbEditBeschreibung.Text.ToString().Trim();
            Kontenplan.GueltigBis = dtpEditBis.Value;

            if (bUpdate)
            {
                Kontenplan.ID = decTmpKontenPlanID;
                Kontenplan.Update();
            }
            else
            {
                Kontenplan.Add();
            }
            ClearTableOfAccountEditInputFields();
            SetTableOFAccountEditInputFieldEnabled(false);
            bUpdate = false;
            InitDGVTableOfAccount();
        }
        ///<summary>ctrTableOfAccount / tsbtnEditDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnEditDelete_Click(object sender, EventArgs e)
        {
            DeleteTableOfAccountItem();
        }
        ///<summary>ctrTableOfAccount / tsbnEditClose_Click</summary>
        ///<remarks></remarks>
        private void tsbnEditClose_Click(object sender, EventArgs e)
        {
            this.splitPanel2.Collapsed = true;
            SettsbtnOpenEditImage();
            ResetCtrTableOfAccountWidth();
        }
    }
}
