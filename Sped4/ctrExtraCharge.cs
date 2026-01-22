using LVS;
using Sped4.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrExtraCharge : UserControl
    {
        ctrMenu _ctrMenu;
        Globals._GL_USER _GL_User;
        clsExtraCharge ExtraCharge;
        internal bool bUpdate;
        internal Int32 iWidthList;
        internal Int32 iWidthEdit;
        private DataTable dtTableOfAccount;

        ///<summary>ctrExtraCharge / ctrExtraCharge</summary>
        ///<remarks></remarks>
        public ctrExtraCharge()
        {
            InitializeComponent();

            //
            iWidthList = this.splitPanel1.Width + 10;
            iWidthEdit = this.splitPanel2.Width + 10;
            //ExtraCharge Edit Panel soll eingeklappt sein
            this.splitPanel2.Collapsed = true;
            ResetCtrExtraChargeWidth();

        }
        ///<summary>ctrExtraCharge / ctrExtraCharge_Load</summary>
        ///<remarks></remarks>
        private void ctrExtraCharge_Load(object sender, EventArgs e)
        {
            //
            SetExtraChargeEditInputFieldEnabled(false);
            //DGV laden
            InitDGVExtraCharge();

            //Einheit
            cbEinheit.DataSource = clsEinheiten.GetEinheiten(this._GL_User);
            cbEinheit.DisplayMember = "Bezeichnung";
            cbEinheit.ValueMember = "Bezeichnung";
        }
        ///<summary>ctrExtraCharge / InitGlobals</summary>
        ///<remarks></remarks>
        public void InitGlobals(ctrMenu myCtrMenu)
        {
            if (myCtrMenu != null)
            {
                this._ctrMenu = myCtrMenu;
                this._GL_User = this._ctrMenu.GL_User;

                ExtraCharge = new clsExtraCharge();
                ExtraCharge.InitClass(this._GL_User);
            }
        }
        ///<summary>ctrExtraCharge / InitGlobals</summary>
        ///<remarks></remarks>
        private void InitDGVExtraCharge()
        {
            DataTable dt = clsExtraCharge.GetExtraCharge(this._GL_User);

            this.dgvExtraCharge.DataSource = dt;
            //Spalten ausschalten
            if (this.dgvExtraCharge.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvExtraCharge.Columns.Count - 1; i++)
                {
                    string colName = this.dgvExtraCharge.Columns[i].Name.ToString();
                    switch (colName)
                    {
                        case "ID":
                        case "erstellt":
                        //case "IsGlobal":
                        //case "ArbeitsbereichID":
                        case "UserID":
                        case "KontoID":
                        case "BEschreibung":
                        case "AdrID":
                            this.dgvExtraCharge.Columns[i].IsVisible = false;
                            break;
                        case "Preis":
                            this.dgvExtraCharge.Columns[i].FormatString = "{0:#,##0.00}";
                            break;
                    }
                    //this.dgvExtraCharge.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;
                }
                this.dgvExtraCharge.BestFitColumns();
                this.dgvExtraCharge.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill; // CF

                //SetSelected and Current Row
                for (Int32 i = 0; i <= this.dgvExtraCharge.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(this.dgvExtraCharge.Rows[i].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp == ExtraCharge.ID)
                    {
                        this.dgvExtraCharge.Rows[i].IsSelected = true;
                        this.dgvExtraCharge.Rows[i].IsCurrent = true;
                    }
                }
            }
        }
        ///<summary>ctrADR_List / ResetCtrADRListWidth</summary>
        ///<remarks>Anpassen der Ctr-Breite</remarks>
        private void ResetCtrExtraChargeWidth()
        {
            if (this.splitPanel2.Collapsed)
            {
                this.Width = iWidthList + 10;
            }
            else
            {
                this.Width = iWidthList + iWidthEdit + 10;
            }
            Console.WriteLine("ExtraChargeCtr: " + this.Width.ToString());
            this.Refresh();
        }
        ///<summary>ctrExtraCharge / dgvExtraCharge_MouseDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvExtraCharge_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dgvExtraCharge.Rows.Count > 0)
            {
                if (this.ExtraCharge.ID > 0)
                {
                    InitDGVExtraCharge();
                    SetExtraChargeEditInputFieldEnabled(true);
                    SetExtraChargeDatenToCtr();
                    this.splitPanel2.Collapsed = false;
                    SettsbtnOpenEditImage();
                    ResetCtrExtraChargeWidth();
                }
                bUpdate = true;
            }
        }
        ///<summary>ctrExtraCharge / dgvExtraCharge_MouseClick</summary>
        ///<remarks></remarks>
        private void dgvExtraCharge_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvExtraCharge.Rows.Count > 0)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvExtraCharge.Rows[this.dgvExtraCharge.CurrentRow.Index].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    ExtraCharge.ID = decTmp;
                    ExtraCharge.Fill();
                    this.dgvExtraCharge.CurrentRow.IsSelected = true;
                }
            }
        }
        ///<summary>ctrExtraCharge / tsbtnListRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnListRefresh_Click(object sender, EventArgs e)
        {
            InitDGVExtraCharge();
        }
        ///<summary>ctrExtraCharge / tsbtnListNew_Click</summary>
        ///<remarks></remarks>
        private void tsbtnListNew_Click(object sender, EventArgs e)
        {
            bUpdate = false;
            this.splitPanel2.Collapsed = true;
            InitExtraChargeEdit();
            SetExtraChargeEditInputFieldEnabled(true);
            ShowAndHideExtraChargeEdit();
        }
        ///<summary>ctrExtraCharge / tsbtnListChange_Click</summary>
        ///<remarks></remarks>
        private void tsbtnListChange_Click(object sender, EventArgs e)
        {
            bUpdate = true;
            this.splitPanel2.Collapsed = false;
            ResetCtrExtraChargeWidth();
            InitExtraChargeEdit();
            SetExtraChargeEditInputFieldEnabled(true);
            SetExtraChargeDatenToCtr();
        }
        ///<summary>ctrExtraCharge / tsbListExcelExport_Click</summary>
        ///<remarks></remarks>
        private void tsbListExcelExport_Click(object sender, EventArgs e)
        {
            string strFileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_Sonderkosten.xls";
            saveFileDialog.FileName = strFileName;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            strFileName = this.saveFileDialog.FileName;
            bool openExportFile = false;

            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgvExtraCharge, strFileName, ref openExportFile, this._GL_User, true);

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
                    error.Aktion = "Sonderkosten - LIST - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        ///<summary>ctrExtraCharge / tsbtnOpenEdit_Click</summary>
        ///<remarks></remarks>
        private void tsbtnOpenEdit_Click(object sender, EventArgs e)
        {
            ShowAndHideExtraChargeEdit();
        }
        ///<summary>ctrExtraCharge / tsbtnListDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnListDelete_Click(object sender, EventArgs e)
        {
            DelelteExtraChargeItem();
        }
        ///<summary>ctrExtraCharge / tsbtnListDelete_Click</summary>
        ///<remarks></remarks>
        public void ShowAndHideExtraChargeEdit()
        {
            if (this.splitPanel2.Collapsed == true)
            {
                this.splitPanel2.Collapsed = false;
            }
            else
            {
                this.splitPanel2.Collapsed = true;
            }
            ResetCtrExtraChargeWidth();
            SettsbtnOpenEditImage();
        }
        ///<summary>ctrExtraCharge / SettsbtnOpenEditImage</summary>
        ///<remarks></remarks>
        private void SettsbtnOpenEditImage()
        {
            if (this.splitPanel2.Collapsed == true)
            {
                this.tsbtnOpenEdit.Image = Sped4.Properties.Resources.layout_left;
            }
            else
            {
                this.tsbtnOpenEdit.Image = Sped4.Properties.Resources.layout;
            }
        }
        ///<summary>ctrExtraCharge / tsbClose_Click</summary>
        ///<remarks></remarks>
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrExtraCharge();
        }
        /**********************************************************************
         *              ExtraCHarge EDIT
         * ********************************************************************/
        ///<summary>ctrExtraCharge / InitExtraChargeEdit</summary>
        ///<remarks></remarks>
        private void InitExtraChargeEdit()
        {
            ClearExtraChargeInputField();
            SetExtraChargeEditInputFieldEnabled(true);
            InitDGVExtraCharge();

            // LADEN DER KONTO DATEN
            clsTableOfAccount temp = new clsTableOfAccount();
            temp.FillDataTableOfAccount(true);
            dtTableOfAccount = temp.dtTableOfAccount;
            cbExtraChargeKontoText.Items.Clear();
            for (int i = 0; i < dtTableOfAccount.Rows.Count; i++)
            {
                cbExtraChargeKontoText.Items.Add(dtTableOfAccount.Rows[i]["KontoText"]);
            }

        }
        ///<summary>ctrExtraCharge / tsbtnExtraChargeAdd_Click</summary>
        ///<remarks></remarks>
        private void tsbtnExtraChargeAdd_Click(object sender, EventArgs e)
        {
            bUpdate = false;
            InitExtraChargeEdit();
        }
        ///<summary>ctrExtraCharge / tsbtnExtraChargeAdd_Click</summary>
        ///<remarks></remarks>
        private void ClearExtraChargeInputField()
        {
            this.tbExtraChargeBeschreibung.Text = string.Empty;
            this.tbExtraChargeBezeichnung.Text = string.Empty;
            this.tbExtraChargeRGText.Text = string.Empty;
            this.tbExtraChargePreis.Text = string.Empty;
            if (cbEinheit.Items.Count > 0)
            {
                cbEinheit.SelectedIndex = 0;
            }
            cbIsGlobal.Checked = false;
        }
        ///<summary>ctrExtraCharge / SetExtraChargeEditInputFieldEnabled</summary>
        ///<remarks></remarks>
        private void SetExtraChargeEditInputFieldEnabled(bool bEnabled)
        {
            this.tbExtraChargeBeschreibung.Enabled = bEnabled;
            this.tbExtraChargeBezeichnung.Enabled = bEnabled;
            this.cbEinheit.Enabled = bEnabled;
            this.tbExtraChargeRGText.Enabled = bEnabled;
            this.tbExtraChargePreis.Enabled = bEnabled;
            this.cbExtraChargeKontoText.Enabled = bEnabled;
            this.cbIsGlobal.Enabled = bEnabled;
        }
        ///<summary>ctrExtraCharge / tsbtnExtraChargeSave_Click</summary>
        ///<remarks>Eingabedaten speichern</remarks>
        private void tsbtnExtraChargeSave_Click(object sender, EventArgs e)
        {
            if (!tbExtraChargeBezeichnung.Text.Equals(string.Empty))
            {
                decimal iTmpExtraChargeID = ExtraCharge.ID;
                ExtraCharge = new clsExtraCharge();
                ExtraCharge.InitClass(this._GL_User);
                ExtraCharge.Bezeichnung = tbExtraChargeBezeichnung.Text.Trim();
                ExtraCharge.Beschreibung = tbExtraChargeBeschreibung.Text.Trim();
                ExtraCharge.RGText = tbExtraChargeRGText.Text.Trim();
                decimal decTmp = 0;
                Decimal.TryParse(tbExtraChargePreis.Text, out decTmp);
                ExtraCharge.Preis = decTmp;
                ExtraCharge.Einheit = cbEinheit.Text;
                ExtraCharge.ArbeitsbereichID = this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID;
                ExtraCharge.IsGlobal = this.cbIsGlobal.Checked;

                if (bUpdate)
                {
                    ExtraCharge.ID = iTmpExtraChargeID;
                    ExtraCharge.Update();
                }
                else
                {
                    ExtraCharge.Add();
                }
                InitDGVExtraCharge();
            }
            ClearExtraChargeInputField();
            SetExtraChargeEditInputFieldEnabled(false);
            bUpdate = false;
        }
        ///<summary>ctrExtraCharge / SetExtraChargeDatenToCtr</summary>
        ///<remarks>Eingabedaten speichern</remarks>
        private void SetExtraChargeDatenToCtr()
        {
            if (ExtraCharge.ID > 0)
            {
                this.tbExtraChargeBezeichnung.Text = ExtraCharge.Bezeichnung;
                this.tbExtraChargeBeschreibung.Text = ExtraCharge.Beschreibung;
                this.tbExtraChargeRGText.Text = ExtraCharge.RGText;
                Functions.SetComboToSelecetedValue(ref cbEinheit, ExtraCharge.Einheit);
                this.tbExtraChargePreis.Text = Functions.FormatDecimal(ExtraCharge.Preis);
                this.cbIsGlobal.Checked = ExtraCharge.IsGlobal;
            }
            else
            {
                ClearExtraChargeInputField();
                SetExtraChargeEditInputFieldEnabled(false);
            }
        }
        ///<summary>ctrExtraCharge / tsbnExtraChargeEditClose_Click</summary>
        ///<remarks>Editbereich einklappen</remarks>
        private void tsbnExtraChargeEditClose_Click(object sender, EventArgs e)
        {
            this.splitPanel2.Collapsed = true;
            SettsbtnOpenEditImage();
            ResetCtrExtraChargeWidth();
        }
        ///<summary>ctrExtraCharge / tsbtnExtraChargeDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnExtraChargeDelete_Click(object sender, EventArgs e)
        {
            DelelteExtraChargeItem();
        }
        ///<summary>ctrExtraCharge / DelelteExtraChargeItem</summary>
        ///<remarks>Löschen des gewählten Datensatzes</remarks>
        private void DelelteExtraChargeItem()
        {
            if (this.ExtraCharge.ID > 0)
            {
                if (!ExtraCharge.IsUsed)
                {
                    if (clsMessages.ExtraCharge_DeleteDatenSatz())
                    {
                        clsExtraChargeADR ExtraChargeADR = new clsExtraChargeADR();
                        ExtraChargeADR.Delete(this.ExtraCharge.ID);


                        this.ExtraCharge.Delete();
                        InitDGVExtraCharge();
                        this.splitPanel2.Collapsed = true;
                        ResetCtrExtraChargeWidth();
                        InitExtraChargeEdit();
                    }
                }
                else
                {
                    clsMessages.DeleteDenied();
                }
            }
        }
        ///<summary>ctrExtraCharge / tbExtraChargePreis_TextChanged</summary>
        ///<remarks></remarks>
        private void tbExtraChargePreis_TextChanged(object sender, EventArgs e)
        {

        }
        ///<summary>ctrExtraCharge / tbExtraChargePreis_Validated</summary>
        ///<remarks></remarks>
        private void tbExtraChargePreis_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbExtraChargePreis.Text.Trim(), out decTmp))
            {
                if (tbExtraChargePreis.Text != string.Empty)
                {
                    clsMessages.Allgemein_EingabeIstKeineDecimalzahl();
                }
                tbExtraChargePreis.Text = "0";
            }
            else
            {
                tbExtraChargePreis.Text = Functions.FormatDecimal(decTmp);
            }
        }
        ///<summary>ctrExtraCharge / cbExtraChargeKontoText_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void cbExtraChargeKontoText_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbExtraChargeKontoText.Items.Count > 0)//1) 
            {
                Int32 index = cbExtraChargeKontoText.SelectedIndex; // -1;
                //nudExtraChargeKontoNr.Value = (Int32)dtTableOfAccount.Rows[index]["KontoNr"];
                tbExtraChargeKontoNr.Text = dtTableOfAccount.Rows[index]["KontoNr"].ToString();
            }
        }
    }
}
