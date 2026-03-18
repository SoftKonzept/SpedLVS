using LVS;
using LVS.ASN;
using System;
using System.Windows.Forms;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrVDAClientOut : UserControl
    {
        internal clsASNWizzard asnWizz;
        internal ctrMenu _ctrMenu;

        string LastSelectedTextBox = string.Empty;
        TextBox LastSelTextBox = new TextBox();

        public int SearchButton { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ctrVDAClientOut(ctrMenu myMenu, clsASNWizzard myAsnWizz)
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
        private void ctrVDAClientOut_Load(object sender, EventArgs e)
        {
            if (
                (this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard)
              )
            {
                InitDGVInputSelection();
                InitDVGVdaClientOut();
                btnList_Add.Enabled = !(this.dgvVDAClientOut.Rows.Count > 0);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDGVInputSelection()
        {
            this.dgvInputSelections.DataSource = clsEdiVDAValueAlias.GetInputSelections();
            this.dgvInputSelections.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDVGVdaClientOut()
        {
            //ClearInputFields();
            //SetInputFieldsEnabled(false);

            this.dgvVDAClientOut.DataSource = this.asnWizz.VdaClientOut.dtVDAClientOutByAdrId;
            Int32 x = 0;
            for (Int32 i = 0; i <= this.dgvVDAClientOut.Columns.Count - 1; i++)
            {
                string ColName = dgvVDAClientOut.Columns[i].Name.ToString();
                switch (ColName)
                {
                    case "ID":
                        this.dgvVDAClientOut.Columns[i].Width = 30;
                        this.dgvVDAClientOut.Columns.Move(i, 0);
                        break;
                    case "Satz":
                        this.dgvVDAClientOut.Columns[i].Width = 30;
                        this.dgvVDAClientOut.Columns.Move(i, 1);
                        break;
                    case "Kennung":
                        this.dgvVDAClientOut.Columns[i].Width = 50;
                        //this.dgvVDAClientOut.Columns[i].FormatString = "{0:d}";
                        this.dgvVDAClientOut.Columns.Move(i, 2);

                        break;
                    case "Datenfeld":
                        this.dgvVDAClientOut.Columns[i].Width = 40;
                        this.dgvVDAClientOut.Columns.Move(i, 3);
                        break;
                    case "ValueArt":
                        //this.dgvVDAClientOut.Columns[i].HeaderText = "Auftraggeber";
                        this.dgvVDAClientOut.Columns[i].Width = 120;
                        this.dgvVDAClientOut.Columns.Move(i, 4);
                        break;
                    case "Value":
                        //this.dgv.Columns[i].HeaderText = "Empfänger";
                        this.dgvVDAClientOut.Columns[i].Width = 120;
                        this.dgvVDAClientOut.Columns.Move(i, 5);
                        break;
                    case "Fill()":
                        this.dgvVDAClientOut.Columns[i].Width = 20;
                        this.dgvVDAClientOut.Columns.Move(i, 6);
                        break;
                    case "aktiv":
                        this.dgvVDAClientOut.Columns[i].Width = 20;
                        this.dgvVDAClientOut.Columns.Move(i, 7);
                        break;
                    case "NextSatz":
                        this.dgvVDAClientOut.Columns[i].Width = 30;
                        this.dgvVDAClientOut.Columns.Move(i, 8);
                        break;
                    case "ArtSatz":
                        this.dgvVDAClientOut.Columns[i].Width = 40;
                        this.dgvVDAClientOut.Columns.Move(i, 9);
                        break;
                    case "FillValue":
                        this.dgvVDAClientOut.Columns[i].Width = 100;
                        this.dgvVDAClientOut.Columns.Move(i, 10);
                        break;
                    case "FillLeft":
                        this.dgvVDAClientOut.Columns[i].Width = 30;
                        this.dgvVDAClientOut.Columns.Move(i, 11);
                        break;
                    case "AdrID":
                        this.dgvVDAClientOut.Columns[i].Width = 40;
                        this.dgvVDAClientOut.Columns.Move(i, 12);
                        break;

                    default:
                        this.dgvVDAClientOut.Columns[i].IsVisible = true;
                        break;
                }
            }
            this.dgvVDAClientOut.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnList_Refresh_Click(object sender, EventArgs e)
        {
            InitDVGVdaClientOut();
            this.btnList_Add.Enabled = !(this.dgvVDAClientOut.Rows.Count > 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvVDAClientOut_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvVDAClientOut.Rows.Count > 0)
            {
                this.dgvVDAClientOut.CurrentRow.IsSelected = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvVDAClientOut_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvVDAClientOut.RowCount > 0)
            {
                if (dgvVDAClientOut.SelectedRows.Count > 0)
                {
                    decimal decTmp = 0;
                    decimal.TryParse(dgvVDAClientOut.SelectedRows[0].Cells["ID"].Value.ToString(), out decTmp);
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
        private void dgvInputSelections_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvInputSelections.Rows.Count > 0)
            {
                this.dgvInputSelections.CurrentRow.IsSelected = true;
            }
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
                        if (LastSelTextBox.Equals(tbValueArt))
                        {
                            tbValueArt.Text = dgvInputSelections.SelectedRows[0].Cells["Value"].Value.ToString();
                        }
                        else if (LastSelTextBox.Equals(tbValue))
                        {
                            tbValue.Text = dgvInputSelections.SelectedRows[0].Cells["Value"].Value.ToString();
                        }
                        else if (LastSelTextBox.Equals(tbFillValue))
                        {
                            tbFillValue.Text = dgvInputSelections.SelectedRows[0].Cells["Value"].Value.ToString();
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
            this.asnWizz.VdaClientOut.AdrID = this.asnWizz.AuftragggeberAdr.ID;
            if (this.asnWizz.VdaClientOut.AddSchema(clsVDAClientOutVDA4913DefaultSchema.GeVDA4913Default((int)this.asnWizz.AuftragggeberAdr.ID, (int)clsASNArt.GetASNArtIdVDA4913(this._ctrMenu.GL_User.User_ID))))
            {
                strTxt = "Ein neue VDA4913 iDoc Schema wurde angelegt!";
                InitDVGVdaClientOut();
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
                    this.InitDVGVdaClientOut();
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
            InitDVGVdaClientOut();

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
    }
}
