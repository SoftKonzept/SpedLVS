using Common.Helper;
using Common.Models;
using LVS;
using LVS.ASN;
using LVS.Constants;
using LVS.ViewData;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrAdrVerweis : UserControl
    {
        internal clsASNWizzard asnWizz;
        internal ctrMenu _ctrMenu;
        internal DataTable dtArbeitsbereiche;
        public int SearchButton;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAsnWiz"></param>
        public ctrAdrVerweis(clsASNWizzard myAsnWiz)
        {
            InitializeComponent();
            asnWizz = myAsnWiz;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrAdrVerweis_Load(object sender, EventArgs e)
        {
            if (asnWizz is clsASNWizzard)
            {
                //--- comboboxen füllen             
                dtArbeitsbereiche = clsArbeitsbereiche.GetArbeitsbereichList(this._ctrMenu._frmMain.GL_User.User_ID);
                cbArbeitsbereich.DataSource = dtArbeitsbereiche;
                cbArbeitsbereich.ValueMember = "ID";
                cbArbeitsbereich.DisplayMember = "Arbeitsbereich";

                //-- combo Mandanden
                comboMandant.DataSource = clsMandanten.GetMandatenList(1);
                comboMandant.DisplayMember = "Matchcode";
                comboMandant.ValueMember = "Mandanten_ID";

                //List<string> ListVerweisArtTmp = clsADRVerweis.ListVerweisArt();
                comboVerweisArt.DataSource = clsADRVerweis.ListVerweisArt();
                comboVerweisArt.SelectedIndex = 0;

                AsnArtViewData asnArtViewData = new AsnArtViewData();
                cbASNFileTyp.DataSource = asnArtViewData.ListAsnArt;
                cbASNFileTyp.DisplayMember = "Typ";
                cbASNFileTyp.ValueMember = "ID";
                //cbASNFileTyp.DataSource = clsASN.GetListASNFileTyp();
                InitDgv();
            }
        }
        /// <summary>
        ///             Setzt die Datasource für das Datagridview
        /// </summary>
        private void InitDgv()
        {
            //dgvAdrVerweis.DataSource = asnWizz.dtAdrVerweise;
            dgvAdrVerweis.DataSource = asnWizz.ListAdrReferenceViews;

            for (Int32 i = 0; i <= this.dgvAdrVerweis.Columns.Count - 1; i++)
            {
                this.dgvAdrVerweis.Columns[i].IsVisible = true;
                string colName = this.dgvAdrVerweis.Columns[i].Name.ToString();
                switch (colName)
                {
                    case "Id":
                    case "SenderAdrId":
                    case "VerweisAdrId":
                    case "MandantenId":
                    case "Arbeitsbereich":
                    case "ReferenceArt":
                    case "Verweis":
                    case "ASNFileTyp":
                    case "Activ":
                    case "Bemerkung":
                    case "WorkspaceId":
                    case "UseS712F04":
                    case "UseS713F13":
                        this.dgvAdrVerweis.Columns[i].IsVisible = true;
                        break;

                    case "SenderReference":
                        this.dgvAdrVerweis.Columns[i].Name = "Sender-Ref";
                        this.dgvAdrVerweis.Columns[i].IsVisible = true;
                        break;
                    case "SupplierRefrence":
                        this.dgvAdrVerweis.Columns[i].Name = "Lieferanten-Ref";
                        this.dgvAdrVerweis.Columns[i].IsVisible = true;
                        break;
                    case "SupplierNo":
                        this.dgvAdrVerweis.Columns[i].Name = "LieferantenNr";
                        this.dgvAdrVerweis.Columns[i].IsVisible = true;
                        break;

                    case "ReferencePart1":
                        this.dgvAdrVerweis.Columns[i].Name = "Ref-Part 1";
                        this.dgvAdrVerweis.Columns[i].IsVisible = true;
                        break;

                    case "ReferencePart2":
                        this.dgvAdrVerweis.Columns[i].Name = "Ref-Part 2";
                        this.dgvAdrVerweis.Columns[i].IsVisible = true;
                        break;

                    case "ReferencePart3":
                        this.dgvAdrVerweis.Columns[i].Name = "Ref-Part 3";
                        this.dgvAdrVerweis.Columns[i].IsVisible = true;
                        break;

                    case "Description":
                    case "AdrReference":
                    case "Workspace":
                        this.dgvAdrVerweis.Columns[i].IsVisible = false;
                        break;


                    default:
                        this.dgvAdrVerweis.Columns[i].IsVisible = false;
                        break;
                }
                this.dgvAdrVerweis.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;
            }
            this.dgvAdrVerweis.BestFitColumns();
        }
        /// <summary>
        ///             Setzt Current Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAdrVerweis_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvAdrVerweis.Rows.Count > 0)
            {
                this.dgvAdrVerweis.CurrentRow.IsSelected = true;
            }
        }
        /// <summary>
        ///             Doubble Mouseklick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAdrVerweis_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SetADRVerweisToCtr();
            SetASNEditInputFieldEnabled(true);
        }
        /// <summary>
        ///             aktivieren / deaktivieren der EIngabefelder
        /// </summary>
        /// <param name="bEnabled"></param>
        private void SetASNEditInputFieldEnabled(bool bEnabled)
        {
            tbId.Enabled = false;
            tbVerweisADR.Enabled = bEnabled;
            cbArbeitsbereich.Enabled = bEnabled;
            comboVerweisArt.Enabled = bEnabled;
            tbASNVerweis.Enabled = bEnabled;
            tbASNLieferantenNummer.Enabled = bEnabled;
            tbASNSenderVerweis.Enabled = bEnabled;
            tbADRVerweisBemerkung.Enabled = bEnabled;
            tbSupplierNo.Enabled = bEnabled;
            cbAktiv.Enabled = bEnabled;
            cbUser712F04.Enabled = bEnabled;
            cbUserS713F13.Enabled = bEnabled;
            tbRefPart1.Enabled = bEnabled;
            tbRefPart2.Enabled = bEnabled;
            tbRefPart3.Enabled = bEnabled;
        }
        /// <summary>
        ///             Werte der Klasse ADRVerweis werden den EIngabefeldern zugewiesen
        /// </summary>
        private void SetADRVerweisToCtr()
        {
            if (dgvAdrVerweis.RowCount > 0)
            {
                if (dgvAdrVerweis.SelectedRows.Count > 0)
                {
                    asnWizz.InitAdressVerweisById(decimal.Parse(dgvAdrVerweis.SelectedRows[0].Cells["ID"].Value.ToString()));
                    tbId.Text = asnWizz.AdrReferenceVD.adrReference.Id.ToString();
                    tbVerweisADR.Text = asnWizz.AdrReferenceVD.adrReference.SenderAddress.ViewId;
                    Functions.SetComboToSelecetedValue(ref cbArbeitsbereich, asnWizz.AdrReferenceVD.adrReference.WorkspaceId.ToString());
                    Functions.SetComboToSelecetedItem(ref comboVerweisArt, asnWizz.AdrReferenceVD.adrReference.ReferenceArt);
                    Functions.SetComboToSelecetedValue(ref comboMandant, asnWizz.AdrReferenceVD.adrReference.MandantenId.ToString());
                    tbASNVerweis.Text = asnWizz.AdrReferenceVD.adrReference.Reference;
                    tbASNLieferantenNummer.Text = asnWizz.AdrReferenceVD.adrReference.SupplierReference;
                    tbASNSenderVerweis.Text = asnWizz.AdrReferenceVD.adrReference.SenderReference;
                    tbSupplierNo.Text = asnWizz.AdrReferenceVD.adrReference.SupplierNo;
                    cbAktiv.Checked = asnWizz.AdrReferenceVD.adrReference.IsActive;
                    cbUser712F04.Checked = asnWizz.AdrReferenceVD.adrReference.UseS712F04;
                    cbUserS713F13.Checked = asnWizz.AdrReferenceVD.adrReference.UseS713F13;
                    Functions.SetComboToSelecetedItem(ref cbASNFileTyp, asnWizz.AdrReferenceVD.adrReference.ASNFileTyp);
                    tbADRVerweisBemerkung.Text = asnWizz.AdrReferenceVD.adrReference.Remark;
                    tbRefPart1.Text = asnWizz.AdrReferenceVD.adrReference.ReferencePart1;
                    tbRefPart2.Text = asnWizz.AdrReferenceVD.adrReference.ReferencePart2;
                    tbRefPart3.Text = asnWizz.AdrReferenceVD.adrReference.ReferencePart3;
                }
            }
        }
        /// <summary>
        ///             Liste der Adressverweise refreshen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefreshAdrVerweis_Click(object sender, EventArgs e)
        {
            RefreshAdrVerweis();
        }
        /// <summary>
        ///             ADRVerweisliste neuladen
        /// </summary>
        private void RefreshAdrVerweis()
        {
            this.asnWizz.InitAdrVerweisList();
            InitDgv();
        }
        /// <summary>
        ///             neue ADRVerweis anlegen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnASNVerweisNew_Click(object sender, EventArgs e)
        {
            //asnWizz.AdrVerweis = new clsADRVerweis();
            //asnWizz.AdrVerweis.InitClass(this._ctrMenu._frmMain.GL_User);

            asnWizz.AdrReferenceVD = new AddressReferenceViewData();
            ClearASNInputField();
            SetASNEditInputFieldEnabled(true);
        }
        /// <summary>
        ///             Editierten Datensatz speichern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnASNSave_Click(object sender, EventArgs e)
        {
            string strError = string.Empty;

            //Wenn VDA4905 dann darf die Lieferantennummer 0 sein, sonst nicht
            if ((tbASNLieferantenNummer.Text == "0" || tbASNLieferantenNummer.Text == string.Empty) & (cbASNFileTyp.Text != constValue_AsnArt.const_Art_VDA4905))
            {
                strError += "Es wurde keine Lieferantennummer eingetragen!\n";
            }

            if (strError == string.Empty)
            {
                if (tbASNLieferantenNummer.Text != string.Empty)
                {
                    SetValueToAdrVerweis();
                    if (this.asnWizz.AdrReferenceVD.adrReference.Id > 0)
                    {
                        this.asnWizz.AdrReferenceVD.Update();
                    }
                    else
                    {
                        this.asnWizz.AdrReferenceVD.Add();
                    }
                    RefreshAdrVerweis();
                    ClearASNInputField();
                    SetASNEditInputFieldEnabled(false);
                }
            }
            else
            {
                clsMessages.ADRVerweis_InputError(strError);
            }
        }
        /// <summary>
        ///             Eingabefelder leeren
        /// </summary>
        private void ClearASNInputField()
        {
            tbId.Text = "0";
            tbVerweisADR.Text = string.Empty;
            cbArbeitsbereich.SelectedIndex = -1;
            comboVerweisArt.SelectedIndex = -1;
            comboMandant.SelectedIndex = -1;
            tbASNVerweis.Text = string.Empty;
            tbSupplierNo.Text = string.Empty;
            tbASNLieferantenNummer.Text = string.Empty;
            tbASNSenderVerweis.Text = string.Empty;
            nudAdrIdDirect.Value = 0;
            tbADRVerweisBemerkung.Text = string.Empty;
            tbRefPart1.Text = string.Empty;
            tbRefPart2.Text = string.Empty;
            tbRefPart3.Text = string.Empty;
        }
        /// <summary>
        ///             Wertzuweisung der Klasse ADRVerweis
        /// </summary>
        private void SetValueToAdrVerweis()
        {
            try
            {
                asnWizz.AdrReferenceVD.adrReference.SupplierNo = string.Empty;
                asnWizz.AdrReferenceVD.adrReference.SupplierReference = tbASNLieferantenNummer.Text.Trim();
                asnWizz.AdrReferenceVD.adrReference.SenderReference = tbASNSenderVerweis.Text.Trim();
                asnWizz.AdrReferenceVD.adrReference.VerweisAdrId = (int)clsADR.GetIDByMatchcode(tbVerweisADR.Text.Trim());
                asnWizz.AdrReferenceVD.adrReference.SenderAdrId = (int)asnWizz.AuftragggeberAdr.ID;

                int iTmp = 0;
                int.TryParse(cbArbeitsbereich.SelectedValue.ToString(), out iTmp);
                asnWizz.AdrReferenceVD.adrReference.WorkspaceId = iTmp;
                //asnWizz.AdrReferenceVD.adrReference.WorkspaceId = (int)cbArbeitsbereich.SelectedValue;
                asnWizz.AdrReferenceVD.adrReference.Reference = tbASNVerweis.Text.Trim();

                iTmp = 0;
                int.TryParse(comboMandant.SelectedValue.ToString(), out iTmp);
                asnWizz.AdrReferenceVD.adrReference.MandantenId = iTmp;

                string strTmp = comboVerweisArt.SelectedItem.ToString();
                asnWizz.AdrReferenceVD.adrReference.ReferenceArt = strTmp;
                asnWizz.AdrReferenceVD.adrReference.SupplierNo = tbSupplierNo.Text.Trim();
                asnWizz.AdrReferenceVD.adrReference.IsActive = cbAktiv.Checked;
                asnWizz.AdrReferenceVD.adrReference.UseS712F04 = cbUser712F04.Checked;
                asnWizz.AdrReferenceVD.adrReference.UseS713F13 = cbUserS713F13.Checked;
                asnWizz.AdrReferenceVD.adrReference.ASNFileTyp = cbASNFileTyp.Text;
                asnWizz.AdrReferenceVD.adrReference.Remark = tbADRVerweisBemerkung.Text.Replace("'", "");
                asnWizz.AdrReferenceVD.adrReference.Description = tbADRVerweisBemerkung.Text;
                asnWizz.AdrReferenceVD.adrReference.ReferencePart1 = tbRefPart1.Text.Trim();
                asnWizz.AdrReferenceVD.adrReference.ReferencePart2 = tbRefPart2.Text.Trim();
                asnWizz.AdrReferenceVD.adrReference.ReferencePart3 = tbRefPart3.Text.Trim();
            }
            catch (Exception ex)
            { }
        }
        /// <summary>
        ///             Löschen eines ADRVerweis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnASNVerweisDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                this.asnWizz.AdrReferenceVD.Delete();
                //this.asnWizz.AdrVerweis.Delete();
                RefreshAdrVerweis();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbVerweisADR_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this._ctrMenu._frmMain.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            string SearchText = tbVerweisADR.Text.ToString();
            string Ausgabe = "";

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();

            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);

            }
            if (dtTmp.Rows.Count > 0)
            {
                int iTmp = 0;
                int.TryParse(dtTmp.Rows[0]["ID"].ToString(), out iTmp);
                if ((iTmp > 0) && (this.asnWizz.AdrReferenceVD.adrReference.VerweisAdrId != iTmp))
                {
                    TakeOverAdrID(iTmp);
                }
                //tbVerweisADRLong.Text = Functions.GetADRStringFromTable(dtTmp);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDecADR_ID"></param>
        public void TakeOverAdrID(decimal myDecADR_ID)
        {
            this.SearchButton = 1;
            this.asnWizz.AdrReferenceVD.adrViewData = new AddressViewData((int)myDecADR_ID, (int)_ctrMenu._frmMain.GL_User.User_ID);
            this.asnWizz.AdrReferenceVD.adrReference.ReceiverAddress = this.asnWizz.AdrReferenceVD.adrViewData.Address.Copy();
            this.asnWizz.AdrReferenceVD.adrReference.VerweisAdrId = this.asnWizz.AdrReferenceVD.adrReference.ReceiverAddress.Id;
            this.tbVerweisADR.Text = this.asnWizz.AdrReferenceVD.adrReference.ReceiverAddress.ViewId;
            this.tbVerweisADRLong.Text = this.asnWizz.AdrReferenceVD.adrReference.ReceiverAddress.AddressStringShort;
            this.nudAdrIdDirect.Value = this.asnWizz.AdrReferenceVD.adrReference.ReceiverAddress.Id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            _ctrMenu.OpenADRSearch(this);
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

        private void tsbtnSettingExport_Click(object sender, EventArgs e)
        {
            //--- export per json
            string Message = string.Empty;
            //Check ist ein Verweis ausgewählt
            int iId = 0;
            int.TryParse(tbId.Text, out iId);
            if (iId > 0)
            {
                AddressReferenceViewData adrRefVD = new AddressReferenceViewData(asnWizz.AdrReferenceVD.adrReference.Id, (int)_ctrMenu.GL_User.User_ID);
                if (adrRefVD.adrReference.Id > 0)
                {
                    adrRefVD.ListAddressReferences.Clear();
                    adrRefVD.ListAddressReferences.Add(adrRefVD.adrReference);
                    string fileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_AdrId_" + this.asnWizz.AuftragggeberAdr.ID.ToString() + "_AdrVerweis_" + asnWizz.AdrReferenceVD.adrReference.ReferenceArt + ".json";
                    string exPath = _ctrMenu._frmMain.systemSped.DefaultPath_LVS_Export;
                    var exJson = new FileExportToJson<AddressReferences>(
                                                                            fileName,          // Dateiname
                                                                            exPath,          // Exportpfad
                                                                             adrRefVD.ListAddressReferences                // Liste der zu exportierenden Objekte
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
            }
            else
            {
                Message += "Es wurde kein Verweis ausgewählt zum exportieren!" + Environment.NewLine;
            }
            clsMessages.Allgemein_InfoTextShow(Message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSettingImport_Click(object sender, EventArgs e)
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
                    FileImportToJson<AddressReferences> fileImport = new FileImportToJson<AddressReferences>(openFileDialog.FileName);
                    if (fileImport.IsImported)
                    {
                        var importedList = fileImport.ListImported;

                        //--- check korrekt Sender = Auftraggeber
                        var check = importedList.FirstOrDefault(x => x.SenderAdrId == this.asnWizz.AuftragggeberAdr.ID);
                        if ((check is AddressReferences) && (check.SenderAdrId == this.asnWizz.AuftragggeberAdr.ID))
                        {
                            AddressReferenceViewData adrRefVD = new AddressReferenceViewData();
                            bool IsAdded = adrRefVD.AddbyImport(fileImport.ListImported, (int)this.asnWizz.AuftragggeberAdr.ID);
                            if (IsAdded)
                            {
                                Message += "Import aus der Datei war erfolgreich!" + Environment.NewLine;
                                clsMessages.Allgemein_InfoTextShow(Message);
                            }
                        }
                        else
                        {
                            Message += "Die importierten Datensätze entsprechend nicht dem Auftraggeber!!!" + Environment.NewLine;
                            Message += "Soll die Daten dennoch importiert werden?" + Environment.NewLine;

                            if (clsMessages.Allgemein_SelectionInfoTextShow(Message))
                            {
                                AddressReferenceViewData adrRefVD = new AddressReferenceViewData();
                                bool IsAdded = adrRefVD.AddbyImport(fileImport.ListImported, (int)this.asnWizz.AuftragggeberAdr.ID);
                                if (IsAdded)
                                {
                                    Message += "Import aus der Datei war erfolgreich!" + Environment.NewLine;
                                    clsMessages.Allgemein_InfoTextShow(Message);
                                }
                            }
                        }
                    }
                    else
                    {
                        Message += "Import konnte nicht durchgeführt werden!" + Environment.NewLine;
                        Message += fileImport.ErrorMessage + Environment.NewLine;
                        clsMessages.Allgemein_InfoTextShow(Message);
                    }
                }
                
            }
            RefreshAdrVerweis();
        }
    }
}
