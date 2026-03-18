using LVS;
using LVS.ASN;
using LVS.ASN.Defaults;
using LVS.Constants;
using System;
using System.Windows.Forms;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrEDIFACTClientOut : UserControl
    {
        internal clsASNWizzard asnWizz;
        internal ctrMenu _ctrMenu;

        string LastSelectedTextBox = string.Empty;
        TextBox LastSelTextBox = new TextBox();

        public int SearchButton { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ctrEDIFACTClientOut(ctrMenu myMenu, clsASNWizzard myAsnWizz)
        {
            InitializeComponent();
            _ctrMenu = myMenu;
            asnWizz = myAsnWizz;
            //asnWizz.AsnArt.FillByAsnArt(clsASNArt.const_Art_EdifactVDA4987);
            //asnWizz.VdaClientOut = new clsVDAClientValue();
            asnWizz.VdaClientOut.ASNArtId = (int)asnWizz.AsnArt.ID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrVDAClientOut_Load(object sender, EventArgs e)
        {
            if (
                (this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard)
              )
            {
                InitDGV();
                btnList_Add.Enabled = !(this.dgvSegment.Rows.Count > 0);
                InitDGV_VarAliasList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDGV()
        {


            this.dgvSegment.DataSource = this.asnWizz.VdaClientOut.dtEDIFACTClientOutByAdrId;
            Int32 x = 0;
            for (Int32 i = 0; i <= this.dgvSegment.Columns.Count - 1; i++)
            {
                string ColName = dgvSegment.Columns[i].Name.ToString();
                switch (ColName)
                {
                    case "ID":
                        this.dgvSegment.Columns[i].Width = 30;
                        this.dgvSegment.Columns.Move(i, 0);
                        break;
                    case "Segment":
                        this.dgvSegment.Columns[i].Width = 30;
                        this.dgvSegment.Columns.Move(i, 1);
                        break;

                    case "Element":
                        this.dgvSegment.Columns[i].Width = 30;
                        this.dgvSegment.Columns.Move(i, 2);
                        break;

                    case "Field":
                        this.dgvSegment.Columns[i].Width = 30;
                        this.dgvSegment.Columns.Move(i, 3);
                        break;

                    //case "Datenfeld":
                    //    this.dgvSegment.Columns[i].Width = 40;
                    //    this.dgvSegment.Columns.Move(i, 5);
                    //    break;

                    case "ValueArt":
                        //this.dgvVDAClientOut.Columns[i].HeaderText = "Auftraggeber";
                        this.dgvSegment.Columns[i].Width = 120;
                        this.dgvSegment.Columns.Move(i, 4);
                        break;

                    case "Value":
                        //this.dgv.Columns[i].HeaderText = "Empfänger";
                        this.dgvSegment.Columns[i].Width = 120;
                        this.dgvSegment.Columns.Move(i, 5);
                        break;
                    case "Fill()":
                        this.dgvSegment.Columns[i].Width = 20;
                        this.dgvSegment.Columns.Move(i, 6);
                        break;

                    case "aktiv":
                        this.dgvSegment.Columns[i].Width = 20;
                        this.dgvSegment.Columns.Move(i, 7);
                        break;
                    //case "NextSatz":
                    //    this.dgvSegment.Columns[i].Width = 30;
                    //    this.dgvSegment.Columns.Move(i, 8);
                    //    break;

                    case "ArtSatz":
                        this.dgvSegment.Columns[i].Width = 40;
                        this.dgvSegment.Columns.Move(i, 8);
                        break;

                    case "FillValue":
                        this.dgvSegment.Columns[i].Width = 100;
                        this.dgvSegment.Columns.Move(i, 9);
                        break;
                    case "FillLeft":
                        this.dgvSegment.Columns[i].Width = 30;
                        this.dgvSegment.Columns.Move(i, 10);
                        break;

                    //case "AdrID":
                    //    this.dgvSegment.Columns[i].Width = 40;
                    //    this.dgvSegment.Columns.Move(i, 13);
                    //    break;

                    case "Kennung":
                        this.dgvSegment.Columns[i].Width = 50;
                        //this.dgvVDAClientOut.Columns[i].FormatString = "{0:d}";
                        this.dgvSegment.Columns.Move(i, 11);
                        break;

                    default:
                        this.dgvSegment.Columns[i].IsVisible = false;
                        break;
                }
            }
            //this.dgvSegment.GroupDescriptors.Clear();


            //GroupDescriptor desSegment = new GroupDescriptor();
            //desSegment.GroupNames.Add("Segment", ListSortDirection.Ascending);
            //this.dgvSegment.GroupDescriptors.Add(desSegment);

            //GroupDescriptor desElement = new GroupDescriptor();
            //desElement.GroupNames.Add("Element", ListSortDirection.Ascending);
            //this.dgvSegment.GroupDescriptors.Add(desElement);


            this.dgvSegment.BestFitColumns();
            //this.dgvSegment.SortDescriptors.Expression = "ID ASC";
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDGV_VarAliasList()
        {
            this.dgvVarAliasList.DataSource = clsEdiVDAValueAlias.GetInputSelections();
            this.dgvVarAliasList.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnList_Refresh_Click(object sender, EventArgs e)
        {
            InitDGV();
            this.btnList_Add.Enabled = !(this.dgvSegment.Rows.Count > 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvVDAClientOut_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvSegment.Rows.Count > 0)
            {
                this.dgvSegment.CurrentRow.IsSelected = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSegment_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvSegment.RowCount > 0)
            {
                if (dgvSegment.SelectedRows.Count > 0)
                {
                    decimal decTmp = 0;
                    decimal.TryParse(dgvSegment.SelectedRows[0].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        this.asnWizz.VdaClientOut.ID = decTmp;
                        this.asnWizz.VdaClientOut.FillByID();
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
        private void ClearInputFields()
        {
            lId.Text = "0";
            tbFillValue.Text = string.Empty;
            tbValue.Text = string.Empty;
            tbValueArt.Text = string.Empty;
            tbNextSatz.Text = string.Empty;
            cbActiv.Checked = false;
            cbArtSatz.Checked = false;
            cbFill.Checked = false;
            cbFillLeft.Checked = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bEnabled"></param>
        private void SetInputFieldsEnabled(bool bEnabled)
        {
            tbFillValue.Enabled = bEnabled;
            tbValue.Enabled = bEnabled;
            tbValueArt.Enabled = bEnabled;
            tbNextSatz.Enabled = bEnabled;
            cbActiv.Enabled = bEnabled;
            cbArtSatz.Enabled = bEnabled;
            cbFill.Enabled = bEnabled;
            cbFillLeft.Enabled = bEnabled;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetVDAClientOutClsToCtr()
        {
            lId.Text = this.asnWizz.VdaClientOut.ID.ToString();
            tbFillValue.Text = this.asnWizz.VdaClientOut.FillValue.Trim();
            tbValue.Text = this.asnWizz.VdaClientOut.Value.Trim();
            tbValueArt.Text = this.asnWizz.VdaClientOut.ValueArt.Trim();
            tbNextSatz.Text = this.asnWizz.VdaClientOut.NextSatz.ToString();
            cbActiv.Checked = this.asnWizz.VdaClientOut.aktiv;
            cbArtSatz.Checked = this.asnWizz.VdaClientOut.IsArtSatz;
            cbFill.Checked = this.asnWizz.VdaClientOut.Fill0;
            cbFillLeft.Checked = this.asnWizz.VdaClientOut.FillLeft;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetValueToVDAClientOutCls()
        {
            this.asnWizz.VdaClientOut.FillValue = tbFillValue.Text.Trim();
            this.asnWizz.VdaClientOut.Value = tbValue.Text.Trim();
            this.asnWizz.VdaClientOut.ValueArt = tbValueArt.Text.Trim();
            int iTmp = 0;
            int.TryParse(tbNextSatz.Text, out iTmp);
            this.asnWizz.VdaClientOut.NextSatz = iTmp;
            this.asnWizz.VdaClientOut.aktiv = cbActiv.Checked;
            this.asnWizz.VdaClientOut.IsArtSatz = cbArtSatz.Checked;
            this.asnWizz.VdaClientOut.Fill0 = cbFill.Checked;
            this.asnWizz.VdaClientOut.FillLeft = cbFillLeft.Checked;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvVarAliasList_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvVarAliasList.Rows.Count > 0)
            {
                this.dgvVarAliasList.CurrentRow.IsSelected = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvVarAliasList_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (dgvVarAliasList.RowCount > 0)
            {
                if (dgvVarAliasList.SelectedRows.Count > 0)
                {
                    string strTmp = string.Empty;
                    int iTmp = 0;
                    int.TryParse(lId.Text.ToString(), out iTmp);
                    if (iTmp > 0)
                    {
                        if (LastSelTextBox.Equals(tbValueArt))
                        {
                            tbValueArt.Text = dgvVarAliasList.SelectedRows[0].Cells["Value"].Value.ToString();
                        }
                        else if (LastSelTextBox.Equals(tbValue))
                        {
                            tbValue.Text = dgvVarAliasList.SelectedRows[0].Cells["Value"].Value.ToString();
                        }
                        else if (LastSelTextBox.Equals(tbFillValue))
                        {
                            tbFillValue.Text = dgvVarAliasList.SelectedRows[0].Cells["Value"].Value.ToString();
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
        private void tbValueArt_TextChanged(object sender, EventArgs e)
        {
            if (tbValueArt.Text.ToUpper().Equals("CONST"))
            {
                tbValue.Enabled = true;
            }
            else
            {
                tbValue.Enabled = false;
                tbValue.Text = string.Empty;
            }
        }
        /// <summary>
        ///             Hier wird ein komplett neues Meldungsschema angelegt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnList_Add_Click(object sender, EventArgs e)
        {
            string strTxt = "Es ist ein Fehler aufgetreten. Der Vorgang konnte nicht durchgeführt werden!";
            strTxt += "Ein neues EDIDFACT Schema " + this.asnWizz.AsnArt.Typ.ToString() + " konnte NICHT angelegt werden!";

            this.asnWizz.VdaClientOut.AdrID = this.asnWizz.AuftragggeberAdr.ID;

            bool bSchemaCreated = false;
            switch (this.asnWizz.AsnArt.Typ)
            {
                case constValue_AsnArt.const_Art_EdifactVDA4987:
                    bSchemaCreated = clsVDAClientOutVDA4987DefaultSchema.CreateVDA4987DefaultClientSet((int)this.asnWizz.AuftragggeberAdr.ID, this.asnWizz.VdaClientOut.GL_User);
                    break;

                case constValue_AsnArt.const_Art_DESADV_BMW_4a:
                    bSchemaCreated = Default_DESADV_BMW_4a.CreateDESADV_BMW_4a((int)this.asnWizz.AuftragggeberAdr.ID, this.asnWizz.VdaClientOut.GL_User);
                    break;

                case constValue_AsnArt.const_Art_DESADV_BMW_4b:
                    bSchemaCreated = Default_DESADV_BMW_4b.CreateDefault_DESADV_BMW_4b((int)this.asnWizz.AuftragggeberAdr.ID, this.asnWizz.VdaClientOut.GL_User);
                    break;

                case constValue_AsnArt.const_Art_DESADV_BMW_4b_RL:
                    bSchemaCreated = Default_DESADV_BMW_4b_RL.CreateDefault_DESADV_BMW_4b_RL((int)this.asnWizz.AuftragggeberAdr.ID, this.asnWizz.VdaClientOut.GL_User);
                    break;

                case constValue_AsnArt.const_Art_DESADV_BMW_4b_ST:
                    bSchemaCreated = Default_DESADV_BMW_4b_ST.CreateDefault_DESADV_BMW_4b_ST((int)this.asnWizz.AuftragggeberAdr.ID, this.asnWizz.VdaClientOut.GL_User);
                    break;

                case constValue_AsnArt.const_Art_DESADV_BMW_6:
                    bSchemaCreated = Default_DESADV_BMW_6.CreateDefault_DESADV_BMW_6((int)this.asnWizz.AuftragggeberAdr.ID, this.asnWizz.VdaClientOut.GL_User);
                    break;
                case constValue_AsnArt.const_Art_DESADV_BMW_6_UB:
                    bSchemaCreated = Default_DESADV_BMW_6_UB.CreateDefault_DESADV_BMW_6_UB((int)this.asnWizz.AuftragggeberAdr.ID, this.asnWizz.VdaClientOut.GL_User);
                    break;

            }
            if (bSchemaCreated)
            {
                strTxt = "Ein neues EDIDFACT Schema " + this.asnWizz.AsnArt.Typ.ToString() + " wurde angelegt!";
                InitDGV();
            }
            clsMessages.Allgemein_InfoTextShow(strTxt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Save_Click(object sender, EventArgs e)
        {
            SetValueToVDAClientOutCls();
            if (this.asnWizz.VdaClientOut.ID > 0)
            {
                if (this.asnWizz.VdaClientOut.Update())
                {
                    this.ClearInputFields();
                    this.InitDGV();
                }
                else
                {
                    string strError = "Der Datensatz konnte nicht gespeichert werden!!!";
                    clsMessages.Allgemein_ERRORTextShow(strError);
                }
            }
            else
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbValueArt_Click(object sender, EventArgs e)
        {
            //LastSelectedTextBox = tbValueArt.Name;
            LastSelTextBox = tbValueArt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbValue_Click(object sender, EventArgs e)
        {
            //LastSelectedTextBox = tbValue.Name;
            LastSelTextBox = tbValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbFillValue_Click(object sender, EventArgs e)
        {
            //LastSelectedTextBox = tbFillValue.Name;
            LastSelTextBox = tbFillValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDecADR_ID"></param>
        public void TakeOverAdrID(decimal myDecADR_ID)
        {
            this.SearchButton = 1;
            //static funktion in VDAClientOut mit AdrId Übergabe
            clsVDAClientValue.InsertShapeByRefAdr((int)myDecADR_ID, (int)this.asnWizz.AuftragggeberAdr.ID, (int)this.asnWizz.AsnArt.ID, this.asnWizz.AuftragggeberAdr.BenutzerID);
            InitDGV();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddShape_Click(object sender, EventArgs e)
        {
            //Adressauswahl öffnen
            SearchButton = 1;
            _ctrMenu.OpenADRSearch(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVarAlias_Refresh_Click(object sender, EventArgs e)
        {
            InitDGV_VarAliasList();
        }
    }
}
