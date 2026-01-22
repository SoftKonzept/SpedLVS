using Common.Helper;
using LVS;
using LVS.ASN;
using LVS.ASN.GlobalValues;
using LVS.Constants;
using LVS.Models;
using LVS.ViewData;
using LVS.Views;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrASNArtFieldAssignment : UserControl
    {
        public int SearchButton = 0;
        public clsASNWizzard asnWizz;
        public ctrMenu _ctrMenu;
        //string LastSelectedTextBox = string.Empty;
        TextBox LastSelTextBox = new TextBox();
        bool IsReceiverSearch = false;
        public ctrASNArtFieldAssignSelectToCopy _ctrASNArtFieldAssignSelectToCopy;
        //internal List<string> List_dgvEDIFACTSubFieldsSource = new List<string>();
        internal Dictionary<string, ctrAsnArtFieldAssignment_DgvEdifactView> Dict_DgvEdifactView = new Dictionary<string, ctrAsnArtFieldAssignment_DgvEdifactView>();
        internal DataTable Source_dgvASNArtFieldAssignment = new DataTable();
        //internal ctrAsnArtFieldAssignment_DgvEdifactView SelectedEdiSegment;
        /// <summary>
        /// 
        /// </summary>
        public ctrASNArtFieldAssignment(ctrMenu myMenu, clsASNWizzard myAsnWizz)
        {
            InitializeComponent();
            this._ctrMenu = myMenu;
            this.asnWizz = myAsnWizz;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrASNArtFieldAssignment_Load(object sender, EventArgs e)
        {
            if (
                (this._ctrMenu is ctrMenu) &&
                (this.asnWizz is clsASNWizzard) &&
                (this.asnWizz.ASNArtFieldAssign is clsASNArtFieldAssignment)
              )
            {
                //comboArbeitsbereich
                comboArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(_ctrMenu._frmMain.system._GL_User.User_ID);
                comboArbeitsbereich.DisplayMember = "Arbeitsbereich";
                comboArbeitsbereich.ValueMember = "ID";

                //comboGlobalFieldVars
                comboGlobalFieldVars.DataSource = GlobalFieldVal_ListVar.ListVar();
                Dict_DgvEdifactView = constValue_Edifact.DictEdifactSubSegmentList();
                RefreshDGVs();
            }
        }
        /// <summary>
        ///             Refresh ASNArtFieldAssignmentlist
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
        private void RefreshDGVs()
        {
            ClearInputFields();
            SetInputFieldsEnabled(true);
            InitDgvASNArtFieldAssignment();
            //ASN-Fields
            InitDgvASNFieldList();
            InitDgvEdifactFieldList();
            //InitDgvSubAsnFieldList();
            //ArtikelFields
            InitDgvArtikelFunctions();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgvASNArtFieldAssignment()
        {
            Source_dgvASNArtFieldAssignment = new DataTable();
            Source_dgvASNArtFieldAssignment = clsASNArtFieldAssignment.GetASNArtFieldAssignmentsBySender(this._ctrMenu.GL_User, this.asnWizz.AuftragggeberAdr.ID);
            this.dgvASNArtFieldAssignment.DataSource = Source_dgvASNArtFieldAssignment;
            for (Int32 i = 0; i <= this.dgvASNArtFieldAssignment.Columns.Count - 1; i++)
            {
                string ColName = dgvASNArtFieldAssignment.Columns[i].Name.ToString();
                switch (ColName)
                {
                    case "SubAsnField":
                        //this.dgvEDIFACTFields.Columns[i].HeaderText = "ASN Field";
                        //this.dgvEDIFACTFields.Columns[i].Width = 80;
                        this.dgvEDIFACTFields.Columns.Move(i, 5);
                        break;

                }
            }
            this.dgvASNArtFieldAssignment.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgvASNFieldList()
        {
            this.dgvVDAFields.DataSource = clsASNArtSatzFeld.GetASNFieldsByASNArt((int)this.asnWizz.AsnArt.ID, this._ctrMenu.GL_User);
            this.dgvVDAFields.BestFitColumns();
        }

        private void InitDgvEdifactFieldList()
        {
            this.dgvEDIFACTFields.DataSource = constValue_Edifact.EdifactSegmentList();
            for (Int32 i = 0; i <= this.dgvEDIFACTFields.Columns.Count - 1; i++)
            {
                string ColName = dgvEDIFACTFields.Columns[i].Name.ToString();
                switch (ColName)
                {
                    case "AsnField":
                        this.dgvEDIFACTFields.Columns[i].HeaderText = "ASN Field";
                        this.dgvEDIFACTFields.Columns[i].Width = 80;
                        this.dgvEDIFACTFields.Columns.Move(i, 0);
                        break;
                }
            }
            this.dgvEDIFACTFields.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgvArtikelFunctions()
        {
            this.dgvArtikelFunction.DataSource = this.asnWizz.dtArtikelFieldsAndFunctions;
            this.dgvArtikelFunction.BestFitColumns();
        }

        ///
        /// 
        /// 
        private void InitDgvSubAsnFieldList(List<ctrAsnArtFieldAssignment_DgvEdifactViewSub> myDgvSource)
        {
            //this.dgvSubAsnField.Columns.Clear();
            //if (dgvSubAsnField.Columns.Contains("Length"))
            //{
            //    dgvSubAsnField.Columns.Remove("Length");
            //}
            //dgvSubAsnField.AutoGenerateColumns = false; 
            this.dgvSubAsnField.DataSource = myDgvSource;
            for (Int32 i = 0; i <= this.dgvSubAsnField.Columns.Count - 1; i++)
            {
                string ColName = dgvSubAsnField.Columns[i].Name.ToString();
                switch (i)
                {
                    case 0:
                        this.dgvSubAsnField.Columns[i].Width = 80;
                        //this.dgvSubAsnField.Columns.Move(i, 0);
                        break;
                }
            }
            this.dgvSubAsnField.BestFitColumns();
        }
        /// <summary>
        ///             Einzelnes Element hinzufügen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnList_Add_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        ///             Mapping von einer Musteradresse übernehmen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddShape_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            this.IsReceiverSearch = false;
            _ctrMenu.OpenADRSearch(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchReceiver_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            this.IsReceiverSearch = true;
            _ctrMenu.OpenADRSearch(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDecADR_ID"></param>
        public void TakeOverAdrID(decimal myDecADR_ID)
        {
            if (this.IsReceiverSearch)
            {
                this.asnWizz.ASNArtFieldAssign.Receiver = myDecADR_ID;
                SetASNArtFieldAssignmentClsToCtr();
            }
            else
            {
                this.asnWizz.ASNArtFieldAssign.Receiver = myDecADR_ID;
                this._ctrASNArtFieldAssignSelectToCopy = new ctrASNArtFieldAssignSelectToCopy(this._ctrMenu, this.asnWizz);
                this._ctrMenu.OpenFrmTMP(this._ctrASNArtFieldAssignSelectToCopy);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvASNArtFieldAssignment_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvASNArtFieldAssignment.Rows.Count > 0)
            {
                this.dgvASNArtFieldAssignment.CurrentRow.IsSelected = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvASNArtFieldAssignment_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvASNArtFieldAssignment.RowCount > 0)
            {
                if (dgvASNArtFieldAssignment.SelectedRows.Count > 0)
                {
                    decimal decTmp = 0;
                    decimal.TryParse(dgvASNArtFieldAssignment.SelectedRows[0].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        this.asnWizz.ASNArtFieldAssign.InitClassByID(this._ctrMenu.GL_User, this._ctrMenu._frmMain.GL_System, decTmp);
                        ClearInputFields();
                        SetInputFieldsEnabled(true);
                        SetASNArtFieldAssignmentClsToCtr();
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void ClearInputFields()
        {
            tbSenderADRShort.Text = string.Empty;
            tbSenderMatchCode.Text = string.Empty;
            tbReceiverAdrShort.Text = string.Empty;
            tbReceiverMatchCode.Text = string.Empty;
            tbASNField.Text = string.Empty;
            tbArtikelField.Text = string.Empty;
            tbCopyToField.Text = string.Empty;
            tbFormatFunction.Text = string.Empty;
            tbDefaultValue.Text = string.Empty;
            cbIsDefaultValue.Checked = false;
            comboArbeitsbereich.SelectedIndex = -1;
            lASNArtFieldId.Text = "ID: 0000000";
            nudAdrIdDirectReceiver.Value = 0;
            tbGlobalFieldVar.Text = string.Empty;
            comboGlobalFieldVars.SelectedIndex = -1;
            cbIsGlobalFieldVar.Checked = false;
            tbSubAsnField.Text = string.Empty;

        }
        /// <summary>
        /// 
        /// </summary>
        private void SetInputFieldsEnabled(bool bEnabled)
        {
            tbSenderADRShort.Enabled = bEnabled;
            tbSenderMatchCode.Enabled = bEnabled;
            tbReceiverAdrShort.Enabled = bEnabled;
            tbReceiverMatchCode.Enabled = bEnabled;
            tbASNField.Enabled = bEnabled;
            tbArtikelField.Enabled = bEnabled;
            tbCopyToField.Enabled = bEnabled;
            tbFormatFunction.Enabled = bEnabled;
            tbDefaultValue.Enabled = bEnabled;
            comboArbeitsbereich.Enabled = bEnabled;
            cbIsDefaultValue.Enabled = bEnabled;
            btnSearchReceiver.Enabled = bEnabled;
            tbGlobalFieldVar.Enabled = bEnabled;
            comboGlobalFieldVars.Enabled = bEnabled;
            cbIsGlobalFieldVar.Enabled = bEnabled;
            tbSubAsnField.Enabled = bEnabled;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetASNArtFieldAssignmentClsToCtr()
        {
            tbSenderADRShort.Text = this.asnWizz.ASNArtFieldAssign.AdrSender.ADRStringShort;
            tbSenderMatchCode.Text = this.asnWizz.ASNArtFieldAssign.AdrSender.ViewID;
            tbReceiverAdrShort.Text = this.asnWizz.ASNArtFieldAssign.AdrReceiver.ADRStringShort;
            tbReceiverMatchCode.Text = this.asnWizz.ASNArtFieldAssign.AdrReceiver.ViewID;
            tbASNField.Text = this.asnWizz.ASNArtFieldAssign.ASNField;
            tbArtikelField.Text = this.asnWizz.ASNArtFieldAssign.ArtField;
            tbCopyToField.Text = this.asnWizz.ASNArtFieldAssign.CopyToField;
            tbFormatFunction.Text = this.asnWizz.ASNArtFieldAssign.FormatFunction;
            tbDefaultValue.Text = this.asnWizz.ASNArtFieldAssign.DefValue;
            cbIsDefaultValue.Checked = this.asnWizz.ASNArtFieldAssign.IsDefValue;
            Functions.SetComboToSelecetedValue(ref comboArbeitsbereich, this.asnWizz.ASNArtFieldAssign.AbBereichID.ToString());
            lASNArtFieldId.Text = "ID: " + this.asnWizz.ASNArtFieldAssign.ID.ToString("#0");
            tbGlobalFieldVar.Text = string.Empty;
            if (this.asnWizz.ASNArtFieldAssign.GlobalFieldVar != null)
            {
                tbGlobalFieldVar.Text = this.asnWizz.ASNArtFieldAssign.GlobalFieldVar.ToString();
            }
            Functions.SetComboToSelecetedValue(ref comboGlobalFieldVars, this.asnWizz.ASNArtFieldAssign.AbBereichID.ToString());
            cbIsGlobalFieldVar.Checked = this.asnWizz.ASNArtFieldAssign.IsGlobalFieldVar;
            tbSubAsnField.Text = this.asnWizz.ASNArtFieldAssign.SubASNField;
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
                this.asnWizz.ASNArtFieldAssign.ASNField = tbASNField.Text;
                this.asnWizz.ASNArtFieldAssign.ArtField = tbArtikelField.Text;
                this.asnWizz.ASNArtFieldAssign.CopyToField = tbCopyToField.Text;
                this.asnWizz.ASNArtFieldAssign.FormatFunction = tbFormatFunction.Text;
                this.asnWizz.ASNArtFieldAssign.DefValue = tbDefaultValue.Text;
                this.asnWizz.ASNArtFieldAssign.IsDefValue = cbIsDefaultValue.Checked;
                int iTmp = 0;
                int.TryParse(comboArbeitsbereich.SelectedValue.ToString(), out iTmp);
                this.asnWizz.ASNArtFieldAssign.AbBereichID = iTmp;
                this.asnWizz.ASNArtFieldAssign.IsGlobalFieldVar = cbIsGlobalFieldVar.Checked;
                if (comboGlobalFieldVars.SelectedValue != null)
                {
                    this.asnWizz.ASNArtFieldAssign.GlobalFieldVar = comboGlobalFieldVars.SelectedValue.ToString();
                }
                this.asnWizz.ASNArtFieldAssign.SubASNField = tbSubAsnField.Text;
                this.asnWizz.ASNArtFieldAssign.Save();

                RefreshDGVs();
            }
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
            if (cbIsGlobalFieldVar.Checked)
            {
                if (tbGlobalFieldVar.Text.Equals(string.Empty))
                {
                    strError += "- GlobalFieldVar ist leer!";
                }
            }
            else
            {
                if (tbASNField.Text.Equals(string.Empty))
                {
                    strError += "- ASNField ist leer!";
                }
                if (tbArtikelField.Text.Equals(string.Empty))
                {
                    strError += "- Artikelfeld ist leer!";
                }
                if (
                      //(this.asnWizz.ASNArtFieldAssign.IsDefValue) &&
                      (this.cbIsDefaultValue.Checked) &&
                      (tbDefaultValue.Text.Equals(string.Empty))
                   )
                {
                    strError += "- DefValue ist leer!";
                }
                if (comboArbeitsbereich.SelectedIndex == -1)
                {
                    strError += "- Arbeitsbereich ist nicht ausgewählt!";
                }
            }
            if (!strError.Equals(string.Empty))
            {
                clsMessages.Allgemein_ERRORTextShow(strError);
                bReturn = false;
            }
            return bReturn;
        }
        /// <summary>
        ///             Delete Selected-Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Delete_Click(object sender, EventArgs e)
        {
            if ((clsMessages.DeleteAllgemein()) &&
                (this.asnWizz.ASNArtFieldAssign is clsASNArtFieldAssignment) &&
                (this.asnWizz.ASNArtFieldAssign.ID > 0))
            {
                this.asnWizz.ASNArtFieldAssign.Delete();
                RefreshDGVs();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvASNField_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvVDAFields.Rows.Count > 0)
            {
                this.dgvVDAFields.CurrentRow.IsSelected = true;
            }
        }

        private void dgvEdifactField_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvEDIFACTFields.Rows.Count > 0)
            {
                this.dgvEDIFACTFields.CurrentRow.IsSelected = true;
            }
        }
        private void dgvSubAsnField_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvSubAsnField.Rows.Count > 0)
            {
                this.dgvSubAsnField.CurrentRow.IsSelected = true;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvASNField_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvVDAFields.RowCount > 0)
            {
                if (dgvVDAFields.SelectedRows.Count > 0)
                {
                    if (this.asnWizz.ASNArtFieldAssign.ID > 0)
                    {
                        if (LastSelTextBox.Equals(tbASNField))
                        {
                            tbASNField.Text = dgvVDAFields.SelectedRows[0].Cells["Kennung"].Value.ToString();

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
        private void dgvEdifactField_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvEDIFACTFields.RowCount > 0)
            {
                if (dgvEDIFACTFields.SelectedRows.Count > 0)
                {
                    if (LastSelTextBox.Equals(tbASNField))
                    {
                        tbASNField.Text = dgvEDIFACTFields.SelectedRows[0].Cells[0].Value.ToString();
                        if (Dict_DgvEdifactView.ContainsKey(tbASNField.Text))
                        {
                            InitDgvSubAsnFieldList(Dict_DgvEdifactView[tbASNField.Text].List_SubAsnField);
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
        private void dgvSubAsnField_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvSubAsnField.RowCount > 0)
            {
                if (dgvSubAsnField.SelectedRows.Count > 0)
                {
                    tbSubAsnField.Text = dgvSubAsnField.SelectedRows[0].Cells[0].Value.ToString();
                    //if (LastSelTextBox.Equals(tbSubAsnField))
                    //{
                    //    tbSubAsnField.Text = dgvSubAsnField.SelectedRows[0].Cells[0].Value.ToString();
                    //}
                }
            }
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
                    if (this.asnWizz.ASNArtFieldAssign.ID > 0)
                    {
                        string strCellValue = dgvArtikelFunction.SelectedRows[0].Cells[0].Value.ToString();
                        if (
                                (LastSelTextBox.Equals(tbArtikelField)) &&
                                (strCellValue.Contains("Artikel")) ||
                                (strCellValue.Contains("EA"))
                           )

                        {
                            tbArtikelField.Text = strCellValue;
                        }
                        else if (
                                    (LastSelTextBox.Equals(tbCopyToField)) &&
                                    (strCellValue.Contains("Artikel")) ||
                                    (strCellValue.Contains("EA"))
                                )
                        {
                            tbCopyToField.Text = strCellValue;
                        }
                        else if (
                                    (LastSelTextBox.Equals(tbFormatFunction)) &&
                                    (strCellValue.Contains("#"))
                                )
                        {
                            tbFormatFunction.Text = strCellValue;
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
        private void tbASNField_Click(object sender, EventArgs e)
        {
            LastSelTextBox = tbASNField;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbArtikelField_Click(object sender, EventArgs e)
        {
            LastSelTextBox = tbArtikelField;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCopyToField_Click(object sender, EventArgs e)
        {
            LastSelTextBox = tbCopyToField;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFormatFunction_Click(object sender, EventArgs e)
        {
            LastSelTextBox = tbFormatFunction;
        }
        /// <summary>
        ///             Datensatz Kopieren
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Copy_Click(object sender, EventArgs e)
        {
            if (
                    (this.dgvASNArtFieldAssignment.Rows.Count > 0) &&
                    (this.asnWizz.ASNArtFieldAssign.ID > 0)
               )
            {
                List<int> tmpList = new List<int>();
                tmpList.Add((int)this.asnWizz.ASNArtFieldAssign.ID);
                clsASNArtFieldAssignment.InsertShapeByRefAdr(tmpList, (int)this.asnWizz.AuftragggeberAdr.ID, this._ctrMenu._frmMain.system._GL_User.User_ID, this._ctrMenu._frmMain.system.AbBereich.ID);
                InitDgvASNArtFieldAssignment();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudAdrIdDirect_Leave(object sender, EventArgs e)
        {
            if (nudAdrIdDirectReceiver.Value > 0)
            {
                this.IsReceiverSearch = true;
                TakeOverAdrID(nudAdrIdDirectReceiver.Value);
            }
            this.IsReceiverSearch = false;
        }

        private void cbIsGlobalFieldVar_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (((RadCheckBox)sender).Checked)
            {
                tbASNField.Text = string.Empty;
                tbArtikelField.Text = string.Empty;
                tbCopyToField.Text = string.Empty;
                tbFormatFunction.Text = string.Empty;
                cbIsDefaultValue.Checked = false;
                tbDefaultValue.Text = string.Empty;

                comboGlobalFieldVars.SelectedIndex = 0;
                tbGlobalFieldVar.Text = comboGlobalFieldVars.Text.Trim();
            }
            else
            {
                comboGlobalFieldVars.SelectedIndex = -1;
                tbGlobalFieldVar.Text = string.Empty;
            }
        }

        private void tabAsnFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((TabControl)sender).SelectedIndex)
            {
                case 0:
                case 1:
                    break;
                case 2:

                    //InitDgvSubAsnFieldList();
                    break;
                default:
                    break;
            }
        }

        private void tbSubAsnField_Enter(object sender, EventArgs e)
        {
            //-- check ausgewählte ASN Field
            if (!tbASNField.Text.Equals(string.Empty))
            {
                if (Dict_DgvEdifactView.ContainsKey(tbASNField.Text))
                {
                    InitDgvSubAsnFieldList(Dict_DgvEdifactView[tbASNField.Text].List_SubAsnField);
                    tabAsnFields.SelectedTab = pageEDIFACTSubFields;
                }
            }
        }
        /// <summary>
        ///             Export Settings to File 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportSettings_Click(object sender, EventArgs e)
        {
            //--- export per json
            string Message = string.Empty;
            ASNArtFieldAssignmentViewData vd = new ASNArtFieldAssignmentViewData((int)this.asnWizz.AuftragggeberAdr.ID, 1, 0, true);
            if (vd.ListASNArtFieldAssignment.Count > 0)
            {
                string fileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_AdrId_" + this.asnWizz.AuftragggeberAdr.ID.ToString() + "_ASNArtFieldAssignment.json";
                string exPath = _ctrMenu._frmMain.systemSped.DefaultPath_LVS_Export;
                //var exJson = new FileExportToJson(fileName, exPath, vd.ListASNArtFieldAssignment);

                var exJson = new FileExportToJson<ASNArtFieldAssignment>(
                                                                            fileName,          // Dateiname
                                                                            exPath,          // Exportpfad
                                                                            vd.ListASNArtFieldAssignment                 // Liste der zu exportierenden Objekte
                                                                        );
                if (exJson.IsExported)
                {
                    Message += "Export erfolgreich!" + Environment.NewLine;
                    Message += "Pfad: " + exJson.FilePath + Environment.NewLine;
                    Message += "Datei: " + exJson.FileName + Environment.NewLine;
                }
                else
                {
                    Message += "Export konnte nicht durchgeführt werden!" + Environment.NewLine;
                }
            }
            else
            {
                Message += "Es sind keine Daten vorhanden!" + Environment.NewLine;
            }
            clsMessages.Allgemein_InfoTextShow(Message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportAssignment_Click(object sender, EventArgs e)
        {
            // Öffnet einen Datei-Dialog, um die JSON-Datei auszuwählen
            string Message = string.Empty;
            string impPath = _ctrMenu._frmMain.systemSped.DefaultPath_LVS_Export;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON Dateien (*.json)|*.json|Alle Dateien (*.*)|*.*";
                openFileDialog.Title = "Importiere Einstellungen";
                openFileDialog.InitialDirectory = impPath;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileImportToJson<ASNArtFieldAssignment> fileImport = new FileImportToJson<ASNArtFieldAssignment>(openFileDialog.FileName);
                    if (fileImport.IsImported)
                    {
                        var importedList = fileImport.ListImported;

                        //--- check korrekt Sender = Auftraggeber
                        var check = importedList.FirstOrDefault(x => x.Sender == this.asnWizz.AuftragggeberAdr.ID);
                        if ((check is ASNArtFieldAssignment) && (check.Sender == this.asnWizz.AuftragggeberAdr.ID))
                        {
                            Message += "Import aus der Datei war erfolgreich!" + Environment.NewLine;

                            ASNArtFieldAssignmentViewData vd = new ASNArtFieldAssignmentViewData();
                            bool IsAdded = vd.AddbyImport(fileImport.ListImported);
                        }
                        else
                        {
                            Message += "Die importierten Datensätze entsprechend nicht dem Auftraggeber!!!" + Environment.NewLine;
                            Message += "Es ist kein Import möglich!" + Environment.NewLine;
                        }
                    }
                    else
                    {
                        Message += "Import konnte nicht durchgeführt werden!" + Environment.NewLine;
                        Message += fileImport.ErrorMessage + Environment.NewLine;
                    }
                }
                clsMessages.Allgemein_InfoTextShow(Message);
            }
            InitDgvASNArtFieldAssignment();
        }
    }
}
