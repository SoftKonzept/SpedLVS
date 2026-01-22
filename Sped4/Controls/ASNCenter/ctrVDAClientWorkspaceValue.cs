using LVS;
using LVS.ASN;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrVDAClientWorkspaceValue : UserControl
    {
        internal clsASNWizzard asnWizz;
        internal ctrMenu _ctrMenu;

        string LastSelectedTextBox = string.Empty;
        TextBox LastSelTextBox = new TextBox();
        internal DataTable dtASNFieldsSource = new DataTable();
        internal ctrVDAClientWorkspaceValueSelectToCopy _ctrVDAClientWorkspaceValueSelectToCopy;


        public int SearchButton { get; private set; }
        public ctrVDAClientWorkspaceValue(ctrMenu myMenu, clsASNWizzard myAsnWizz)
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
        private void ctrVDAClientWorkspaceValue_Load(object sender, EventArgs e)
        {
            //comboArbeitsbereich
            comboArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(1);
            comboArbeitsbereich.DisplayMember = "Arbeitsbereich";
            comboArbeitsbereich.ValueMember = "ID";
            comboArbeitsbereich.SelectedIndex = 0;
            if (
                (this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard)
              )
            {
                RefreshDGVs();
                //btnList_Add.Enabled = !(this.dgvVDAClientWorkspaceValue.Rows.Count > 0);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDGVInputSelection()
        {
            dtASNFieldsSource = new DataTable();
            dtASNFieldsSource = clsASNArtSatzFeld.GetASNFieldsByASNArt((int)this.asnWizz.AsnArt.ID, this._ctrMenu.GL_User);
            this.dgvInputSelections.DataSource = dtASNFieldsSource;
            this.dgvInputSelections.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDVGVdaClientWorkspaceValue()
        {
            this.dgvVDAClientWorkspaceValue.DataSource = this.asnWizz.VdaWorkspaceValue.dtVdaClientWorkspaceValue;
            this.dgvVDAClientWorkspaceValue.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvInputSelections_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvInputSelections.RowCount > 0)
            {
                if (dgvInputSelections.SelectedRows.Count > 0)
                {
                    string strTmp = string.Empty;
                    int iTmp = 0;
                    int.TryParse(lId.Text.ToString(), out iTmp);
                    if (iTmp > 0)
                    {
                        if (LastSelTextBox.Equals(tbKennung))
                        {
                            tbASNFieldId.Text = dgvInputSelections.SelectedRows[0].Cells["ID"].Value.ToString();
                            tbKennung.Text = dgvInputSelections.SelectedRows[0].Cells["Kennung"].Value.ToString();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClearInputFields()
        {
            lId.Text = "0";
            tbReceiverMatchCode.Text = string.Empty;
            tbReceiverAdrShort.Text = string.Empty;
            comboArbeitsbereich.SelectedIndex = 0;
            tbASNFieldId.Text = string.Empty;
            tbKennung.Text = string.Empty;
            tbValue.Text = string.Empty;
            cbActiv.Checked = false;
            cbIsFunction.Checked = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bEnabled"></param>
        private void SetInputFieldsEnabled(bool bEnabled)
        {
            tbReceiverMatchCode.Enabled = bEnabled;
            tbReceiverAdrShort.Enabled = bEnabled;
            comboArbeitsbereich.Enabled = bEnabled;
            tbASNFieldId.Enabled = bEnabled;
            tbKennung.Enabled = bEnabled;
            tbValue.Enabled = bEnabled;
            cbActiv.Enabled = bEnabled;
            cbIsFunction.Enabled = bEnabled;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetVDAClientOutClsToCtr()
        {
            lId.Text = this.asnWizz.VdaWorkspaceValue.ID.ToString(); ;
            tbReceiverMatchCode.Text = this.asnWizz.VdaWorkspaceValue.ReceiverAdr.ViewID;
            tbReceiverAdrShort.Text = this.asnWizz.VdaWorkspaceValue.ReceiverAdr.ADRStringShort;
            comboArbeitsbereich.SelectedIndex = 0;
            Functions.SetComboToSelecetedValue(ref comboArbeitsbereich, ((int)this.asnWizz.VdaWorkspaceValue.AbBereichID).ToString());
            tbASNFieldId.Text = this.asnWizz.VdaWorkspaceValue.ASNFieldID.ToString();
            tbKennung.Text = this.asnWizz.VdaWorkspaceValue.Kennung;
            tbValue.Text = this.asnWizz.VdaWorkspaceValue.Value;
            cbActiv.Checked = this.asnWizz.VdaWorkspaceValue.aktiv;
            cbIsFunction.Checked = this.asnWizz.VdaWorkspaceValue.IsFunction;
            nudVDAFieldId.Value = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbKennung_Click(object sender, EventArgs e)
        {
            LastSelTextBox = tbKennung;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvVDAClientWorkspaceValue_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (this.dgvVDAClientWorkspaceValue.Rows.Count > 0)
            {
                this.dgvVDAClientWorkspaceValue.CurrentRow.IsSelected = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvVDAClientWorkspaceValue_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvVDAClientWorkspaceValue.RowCount > 0)
            {
                if (dgvVDAClientWorkspaceValue.SelectedRows.Count > 0)
                {
                    decimal decTmp = 0;
                    decimal.TryParse(dgvVDAClientWorkspaceValue.SelectedRows[0].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        this.asnWizz.VdaWorkspaceValue.ID = decTmp;
                        this.asnWizz.VdaWorkspaceValue.Fill();
                        ClearInputFields();
                        SetInputFieldsEnabled(true);
                        SetVDAClientOutClsToCtr();
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Save_Click(object sender, EventArgs e)
        {
            if (CheckInputValue())
            {
                int iTmp = 0;
                int.TryParse(comboArbeitsbereich.SelectedValue.ToString(), out iTmp);
                this.asnWizz.VdaWorkspaceValue.AbBereichID = iTmp;
                decimal decTmp = 0;
                decimal.TryParse(tbASNFieldId.Text, out decTmp);
                this.asnWizz.VdaWorkspaceValue.ASNFieldID = decTmp;
                this.asnWizz.VdaWorkspaceValue.Kennung = tbKennung.Text;
                this.asnWizz.VdaWorkspaceValue.Value = tbValue.Text;
                this.asnWizz.VdaWorkspaceValue.aktiv = cbActiv.Checked;
                this.asnWizz.VdaWorkspaceValue.IsFunction = cbIsFunction.Checked;
                if (this.asnWizz.VdaWorkspaceValue.Save())
                {
                    RefreshDGVs();
                    ClearInputFields();
                    SetInputFieldsEnabled(false);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void RefreshDGVs()
        {
            InitDVGVdaClientWorkspaceValue();
            InitDGVInputSelection();
            InitDgvArtikelFunctions();
        }
        /// <summary>
        ///             Check Pfichtfelder
        ///             - ASNField
        ///             - ArtField
        /// </summary>
        /// <returns></returns>
        private bool CheckInputValue()
        {
            bool bReturn = true;
            string strError = string.Empty;
            if (tbKennung.Text.Equals(string.Empty))
            {
                strError += "- Kennung ist leer!";
            }
            if (tbValue.Text.Equals(string.Empty))
            {
                strError += "- Value ist leer!";
            }
            if (tbReceiverAdrShort.Text.Equals(string.Empty))
            {
                strError += "- Receiver ist nicht ausgewählt!";
            }

            if (!strError.Equals(string.Empty))
            {
                clsMessages.Allgemein_ERRORTextShow(strError);
                bReturn = false;
            }
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Delete_Click(object sender, EventArgs e)
        {
            if (this.asnWizz.VdaWorkspaceValue.ID > 0)
            {
                if (clsMessages.DeleteAllgemein())
                {
                    this.asnWizz.VdaWorkspaceValue.Delete();
                }
                RefreshDGVs();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Copy_Click(object sender, EventArgs e)
        {
            if (this.asnWizz.VdaWorkspaceValue.ID > 0)
            {
                if (!this.asnWizz.VdaWorkspaceValue.Add())
                {
                    this.asnWizz.VdaWorkspaceValue.Fill();
                }
                RefreshDGVs();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnList_Refresh_Click(object sender, EventArgs e)
        {
            RefreshDGVs();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnList_Add_Click(object sender, EventArgs e)
        {
            clsVDAClientWorkspaceValue tmpVal = this.asnWizz.VdaWorkspaceValue.Copy();
            tmpVal.ID = 0;
            tmpVal.Receiver = tmpVal.AdrID;
            tmpVal.AbBereichID = this._ctrMenu._frmMain.system.AbBereich.ID;
            tmpVal.ASNFieldID = 0;
            tmpVal.Kennung = string.Empty;
            tmpVal.Value = string.Empty;
            tmpVal.aktiv = false;
            tmpVal.IsFunction = false;
            if (tmpVal.Add())
            {
                this.asnWizz.VdaWorkspaceValue = tmpVal.Copy();
            }
            RefreshDGVs();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchReceiver_Click(object sender, EventArgs e)
        {
            SearchButton = 3;
            _ctrMenu.OpenADRSearch(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDecADR_ID"></param>
        public void TakeOverAdrId(decimal myDecADR_ID)
        {
            this.asnWizz.VdaWorkspaceValue.Receiver = myDecADR_ID;
            this.asnWizz.VdaWorkspaceValue.ReceiverAdr.Fill();
            this.tbReceiverMatchCode.Text = this.asnWizz.VdaWorkspaceValue.ReceiverAdr.ViewID;
            this.tbReceiverAdrShort.Text = this.asnWizz.VdaWorkspaceValue.ReceiverAdr.ADRStringShort;

            //this.asnWizz.AsnAction.AdrEmpfaenger.FillClassOnly();
            //this.asnWizz.AsnAction.AdrAuftraggeber = this.asnWizz.AuftragggeberAdr;
            //this.asnWizz.AsnAction.Auftraggeber = this.asnWizz.AsnAction.AdrAuftraggeber.ID;
            //this.tbReceiverMatchCode.Text = this.asnWizz.VdaWorkspaceValue.ReceiverAdr.ViewID;
            //this.tbReceiverAdrShort.Text = this.asnWizz.VdaWorkspaceValue.ReceiverAdr.ADRStringShort;
            //this._ctrVDAClientWorkspaceValueSelectToCopy = new ctrVDAClientWorkspaceValueSelectToCopy(this._ctrMenu, this.asnWizz);
            //this._ctrMenu.OpenFrmTMP(this._ctrVDAClientWorkspaceValueSelectToCopy);
            //this.asnWizz.VdaWorkspaceValue.AdrID = this.asnWizz.AuftragggeberAdr.ID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvArtikelFunction_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvArtikelFunction.RowCount > 0)
            {
                if (dgvArtikelFunction.SelectedRows.Count > 0)
                {
                    if (this.asnWizz.VdaWorkspaceValue.ID > 0)
                    {
                        string strCellValue = dgvArtikelFunction.SelectedRows[0].Cells[0].Value.ToString();
                        if (
                                (LastSelTextBox.Equals(tbValue))
                           )

                        {
                            tbValue.Text = strCellValue;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbValue_Click(object sender, EventArgs e)
        {
            LastSelTextBox = tbValue;
        }
        ///
        /// <summary>
        /// 
        /// </summary>
        private void InitDgvArtikelFunctions()
        {
            this.dgvArtikelFunction.DataSource = this.asnWizz.dtArtikelFieldsAndFunctions;
            this.dgvArtikelFunction.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudAdrReceiverDirect_Leave(object sender, EventArgs e)
        {
            if (nudAdrReceiverDirect.Value > 0)
            {
                TakeOverAdrId(nudAdrReceiverDirect.Value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudVDAFieldId_ValueChanged(object sender, EventArgs e)
        {
            if (nudVDAFieldId.Value > 0)
            {
                if ((dtASNFieldsSource is DataTable) && (dtASNFieldsSource.Rows.Count > 0))
                {
                    DataRow[] rows = dtASNFieldsSource.Select("ID=" + (int)nudVDAFieldId.Value, "ID");
                    foreach (DataRow row in rows)
                    {
                        tbASNFieldId.Text = row["ID"].ToString();
                        tbKennung.Text = row["Kennung"].ToString();
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddShape_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            //this.IsReceiverSearch = false;
            _ctrMenu.OpenADRSearch(this);
        }
    }
}
