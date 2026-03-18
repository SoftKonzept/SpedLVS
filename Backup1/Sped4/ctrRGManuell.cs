using Common.Enumerations;
using LVS;
using LVS.ViewData;
using Sped4.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrRGManuell : UserControl
    {
        internal const string const_LableInfo_Korrektur = "Korrektur";
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        private DataTable dtRechnung;
        internal clsRechnung Rechnung;
        internal clsRGVorlagenTxt RGVorlagenTxt;
        internal clsReportDocSetting RepDocSettings;
        private Int32 EditRow = -1;
        DateTime dTimeMin;
        public string DokumentenArt { get; set; }
        internal bool bUpdate = false;

        internal AddressViewData adrInvoiceReceiver { get; set; }


        ///<summary>ctrRGManuell / ctrRGManuell</summary>
        ///<remarks></remarks>
        public ctrRGManuell()
        {
            InitializeComponent();
            clsTableOfAccount temp = new clsTableOfAccount();
            temp.FillDataTableOfAccount(true);
            cbExtraChargeKontoText.Items.Clear();
            cbExtraChargeKontoText.ValueMember = "KontoNr";
            cbExtraChargeKontoText.DisplayMember = "KontoText";
            cbExtraChargeKontoText.DataSource = temp.dtTableOfAccount;
            //newRechnung();
            scRechnungspositionen.Enabled = false;
            cbEinheit.DataSource = clsEinheiten.GetEinheiten(this.GL_User);
            cbEinheit.DisplayMember = "Bezeichnung";
            cbEinheit.ValueMember = "Bezeichnung";
            SetVorschauLabel(string.Empty);
        }
        ///<summary>ctrRGManuell / ctrRGManuell_Load</summary>
        ///<remarks></remarks>
        private void ctrRGManuell_Load(object sender, EventArgs e)
        {
            adrInvoiceReceiver = new AddressViewData();

            RGVorlagenTxt = new clsRGVorlagenTxt();
            RGVorlagenTxt._GL_User = this.GL_User;

            CustomerSettings();
            //Combo DocName füllen
            FillComboDocName();
            InitDgvNotPrintedInvoices();
            InitDGVVorlagen();
            newRechnung();
        }
        ///<summary>ctrRGManuell / CustomizeCtr</summary>
        ///<remarks></remarks>
        private void CustomerSettings()
        {
            this._ctrMenu._frmMain.system.Client.ctrRGManuell_CustomizeNudMengeMaxValue(ref this.nudMenge);
        }
        ///<summary>ctrRGManuell / ctrRGManuell_Load</summary>
        ///<remarks></remarks>
        private void FillComboDocName()
        {
            List<string> listDocName = new List<string>();
            if (cbGutschrift.Checked)
            {
                listDocName.Add(clsRechnung.const_RGDocTitel_GS);
                listDocName.Add(clsRechnung.const_RGDocTitel_RGStorno);
                listDocName.Add(clsRechnung.const_RGDocTitel_RGKorrektur);
            }
            else
            {
                listDocName.Add(clsRechnung.const_RGDocTitel_RG);
                listDocName.Add(clsRechnung.const_RGDocTitel_GSStorno);
                listDocName.Add(clsRechnung.const_RGDocTitel_GSKorrektur);
            }
            cbDocName.DataSource = listDocName;
            if (cbDocName.Items.Count > 0)
            {
                cbDocName.SelectedIndex = 0;
            }
        }
        ///<summary>ctrRGManuell / resizeRows</summary>
        ///<remarks></remarks>
        private void resizeRows()
        {
            if (this.dgvPositionen.Columns.Contains("Pos"))
            {
                dgvPositionen.Columns["Pos"].BestFit();
            }
            if (this.dgvPositionen.Columns.Contains("Netto €"))
            {
                dgvPositionen.Columns["Netto €"].BestFit();
            }
            if (this.dgvPositionen.Columns.Contains("Menge"))
            {
                dgvPositionen.Columns["Menge"].BestFit();
            }
            if (this.dgvPositionen.Columns.Contains("€/Einheit"))
            {
                dgvPositionen.Columns["€/Einheit"].BestFit();
            }
            if (this.dgvPositionen.Columns.Contains("Einheit"))
            {
                dgvPositionen.Columns["Einheit"].BestFit();
            }
            dgvPositionen.Columns["Text"].Width = dgvPositionen.Width - dgvPositionen.Columns["Netto €"].Width - dgvPositionen.Columns["Pos"].Width -
            dgvPositionen.Columns["Menge"].Width - dgvPositionen.Columns["€/Einheit"].Width - dgvPositionen.Columns["Einheit"].Width - 25;
        }
        ///<summary>ctrRGManuell / InitDataTableRechnung</summary>
        ///<remarks></remarks>
        private void InitDataTableRechnung()
        {
            dtRechnung = new DataTable("Rechnung");
            dtRechnung.Columns.Add("ID", typeof(Decimal));
            dtRechnung.Columns.Add("Pos", typeof(Int32));
            dtRechnung.Columns.Add("Text", typeof(String));
            dtRechnung.Columns.Add("Menge", typeof(Decimal));
            dtRechnung.Columns.Add("Einheit", typeof(String));
            dtRechnung.Columns.Add("€/Einheit", typeof(Decimal));
            dtRechnung.Columns.Add("Netto €", typeof(Decimal));

            dtRechnung.Columns.Add("Abrechnungsart", typeof(String));
            dtRechnung.Columns.Add("TarifPosID", typeof(Decimal));
            dtRechnung.Columns.Add("Tariftext", typeof(String));
            dtRechnung.Columns.Add("Marge €", typeof(Decimal));
            dtRechnung.Columns.Add("Marge %", typeof(Decimal));
            dtRechnung.Columns.Add("Anfangsbestand", typeof(String));
            dtRechnung.Columns.Add("Abgang", typeof(String));
            dtRechnung.Columns.Add("Zugang", typeof(String));
            dtRechnung.Columns.Add("Endbestand", typeof(String));
            dtRechnung.Columns.Add("RGPosText", typeof(String));
            dtRechnung.Columns.Add("FibuKto", typeof(Int32));
            dtRechnung.Columns.Add("SumCalc", typeof(Boolean));
            dtRechnung.Columns.Add("PricePerUnitFactor", typeof(Decimal));
            dtRechnung.Columns.Add("TarifPricePerUnit", typeof(Decimal));
        }
        ///<summary>ctrRGManuell / InitDGVVorlagen</summary>
        ///<remarks></remarks>
        private void InitDGVVorlagen()
        {
            this.dgvVorlagen.Templates.Clear();
            RGVorlagenTxt.FillDictRGVorlagenTxt();
            this.dgvVorlagen.DataSource = RGVorlagenTxt.dtVorlagen;
            for (Int32 i = 0; i <= this.dgvVorlagen.Columns.Count - 1; i++)
            {
                string strColName = this.dgvVorlagen.Columns[i].Name;
                switch (strColName)
                {
                    case "Vorlage":
                        this.dgvVorlagen.Columns[i].HeaderText = "Vorlage";
                        this.dgvVorlagen.Columns[i].Name = "VorlagenBezeichnung";
                        this.dgvVorlagen.Columns[i].Width = this.splitPanel4.Width;
                        break;
                        //case "Value":
                        //    this.dgvVorlagen.Columns[i].IsVisible = false;
                        //    break;
                }
            }
            GridViewTemplate tmpVorlage = new GridViewTemplate();
            tmpVorlage.DataSource = this.RGVorlagenTxt.dtVorlagenDetails;
            //foreach (GridViewColumn colVor in tmpVorlage.Columns)
            //{
            for (Int32 i = 0; i <= tmpVorlage.Columns.Count - 1; i++)
            {
                string strColName = tmpVorlage.Columns[i].Name;
                switch (strColName)
                {
                    case "Vorlagentext":
                        tmpVorlage.Columns[i].IsVisible = true;
                        tmpVorlage.Columns[i].WrapText = true;
                        tmpVorlage.Columns[i].HeaderText = "Text";
                        tmpVorlage.Columns[i].Name = "Text";
                        //tmpVorlage.Columns[i].UniqueName = "Text";
                        tmpVorlage.Columns[i].Width = this.splitPanel4.Width - 30;
                        tmpVorlage.Columns[i].AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        break;
                    case "EinzelPreis":
                        tmpVorlage.Columns[i].IsVisible = true;
                        tmpVorlage.Columns[i].WrapText = true;
                        tmpVorlage.Columns[i].HeaderText = "€/Einheit";
                        tmpVorlage.Columns[i].Name = "€/Einheit";
                        //tmpVorlage.Columns[i].UniqueName = "€/Einheit";
                        tmpVorlage.Columns[i].Width = 30;
                        tmpVorlage.Columns[i].AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        tmpVorlage.Columns[i].FormatString = "{0:N2}";
                        break;

                    default:
                        tmpVorlage.Columns[i].IsVisible = false;
                        break;
                }
            }
            tmpVorlage.BestFitColumns();
            this.dgvVorlagen.MasterTemplate.Templates.Add(tmpVorlage);

            GridViewRelation relVorlage = new GridViewRelation(this.dgvVorlagen.MasterTemplate);
            relVorlage.ChildTemplate = tmpVorlage;
            relVorlage.RelationName = "CustomVorlagen";
            relVorlage.ParentColumnNames.Add("VorlagenBezeichnung");
            relVorlage.ChildColumnNames.Add("Vorlage");
            this.dgvVorlagen.Relations.Add(relVorlage);
        }
        ///<summary>ctrRGManuell / dgvVorlagen_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvVorlagen_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            tbRGText.Text = RGVorlagenTxt.Vorlagentext;
            nudNettoPreis.Value = RGVorlagenTxt.EinzelPreis;
        }
        ///<summary>ctrRGManuell / dgvVorlagen_CellClick</summary>
        ///<remarks></remarks>
        private void dgvVorlagen_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                RGVorlagenTxt = new clsRGVorlagenTxt();
                RGVorlagenTxt._GL_User = this.GL_User;
                if (this.dgvVorlagen.Columns.Contains("VorlagenBezeichnung"))
                {
                    RGVorlagenTxt.Vorlage = this.dgvVorlagen.Rows[e.RowIndex].Cells["VorlagenBezeichnung"].Value.ToString();
                    DataTable dtTmp = ((DataTable)this.dgvVorlagen.MasterTemplate.Templates[0].DataSource).Copy();
                    try
                    {
                        DataRow[] row = dtTmp.Select("Vorlage = '" + RGVorlagenTxt.Vorlage + "'", "Vorlage");
                        RGVorlagenTxt.Vorlagentext = row[0]["Vorlagentext"].ToString();
                        decimal decTmp = 0;
                        decimal.TryParse(row[0]["EinzelPreis"].ToString(), out decTmp);
                        RGVorlagenTxt.EinzelPreis = decTmp;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        ///<summary>ctrRGManuell / newRechnung</summary>
        ///<remarks></remarks>
        private void newRechnung()
        {
            bUpdate = false;
            tsbtnSave.Enabled = false;
            tsbtnPrint.Enabled = false;
            tsbtnPrintPrev.Enabled = false;
            tsbtnRGRevision.Enabled = false;
            InitManuelleRechnung();
            InitDataTableRechnung();

            Rechnung = new clsRechnung();
            Rechnung.sys = this._ctrMenu._frmMain.system;
            Rechnung.MandantenID = this._ctrMenu._frmMain.system.AbBereich.MandantenID;
            Rechnung.ArBereichID = this._ctrMenu._frmMain.system.AbBereich.ID;
            Rechnung.GS = cbGutschrift.Checked;

            Rechnung._GL_User = this.GL_User;

            if (!bUpdate)
            {
                Rechnung.GetRechnungsnummer(false, false);
            }
            tbRechnungID.Text = Rechnung.RGNr.ToString();
            DateTime ZeitRaumMonat = DateTime.Now.Date;
            this.dtpAbrVon.Value = Functions.GetFirstDayOfMonth(ZeitRaumMonat);
            this.dtpAbrBis.Value = Functions.GetLastDayOfMonth(ZeitRaumMonat);
            InitDGV();
        }
        ///<summary>ctrRGManuell / InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGV()
        {
            dgvPositionen.DataSource = dtRechnung;
            for (int i = 0; i < dgvPositionen.ColumnCount; i++)
            {
                switch (dgvPositionen.Columns[i].Name)
                {
                    case "ID":
                        dgvPositionen.Columns[i].IsVisible = false;
                        break;

                    case "Position":
                    case "Pos":
                        dgvPositionen.Columns[i].HeaderText = "Pos";
                        dgvPositionen.Columns[i].IsVisible = true;
                        dgvPositionen.Columns.Move(dgvPositionen.Columns[i].Index, 0);
                        break;

                    case "RGText":
                    case "Text":
                        dgvPositionen.Columns[i].HeaderText = "Text";
                        dgvPositionen.Columns[i].FieldName = "Text";
                        dgvPositionen.Columns[i].IsVisible = true;
                        dgvPositionen.Columns.Move(dgvPositionen.Columns[i].Index, 1);
                        break;
                    case "Abrechnungseinheit":
                    case "Einheit":
                        dgvPositionen.Columns[i].HeaderText = "Einheit";
                        dgvPositionen.Columns[i].FieldName = "Einheit";
                        dgvPositionen.Columns[i].IsVisible = true;
                        dgvPositionen.Columns.Move(dgvPositionen.Columns[i].Index, 4);
                        break;
                    case "Menge":
                        dgvPositionen.Columns[i].FormatString = "{0:N3}";
                        dgvPositionen.Columns[i].IsVisible = true;
                        dgvPositionen.Columns.Move(dgvPositionen.Columns[i].Index, 2);
                        break;
                    case "EinzelPreis":
                    case "€/Einheit":
                        dgvPositionen.Columns[i].HeaderText = "€/Einheit";
                        dgvPositionen.Columns[i].FieldName = "€/Einheit";
                        dgvPositionen.Columns[i].FormatString = "{0:N2}";
                        dgvPositionen.Columns[i].IsVisible = true;
                        dgvPositionen.Columns.Move(dgvPositionen.Columns[i].Index, 5);
                        break;
                    case "NettoPreis":
                    case "Netto €":
                        dgvPositionen.Columns[i].HeaderText = "Netto €";
                        dgvPositionen.Columns[i].FieldName = "Netto €";
                        dgvPositionen.Columns[i].FormatString = "{0:N2}";
                        dgvPositionen.Columns[i].IsVisible = true;
                        dgvPositionen.Columns.Move(dgvPositionen.Columns[i].Index, 6);
                        break;
                    default:
                        dgvPositionen.Columns[i].IsVisible = false;
                        break;
                }
            }
            resizeRows();
            //this.dgvPositionen.BestFitColumns();
        }
        ///<summary>ctrRGManuell / toolStripButton3_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _ctrMenu.CloseCtrRGManuell();
            this.Dispose();
        }
        ///<summary>ctrRGManuell / cbExtraChargeKontoText_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void cbExtraChargeKontoText_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbExtraChargeKontoText.SelectedValue != null)
                tbKontoNr.Text = cbExtraChargeKontoText.SelectedValue.ToString();
        }
        ///<summary>ctrRGManuell / btnRGAdresse_Click</summary>
        ///<remarks></remarks>
        private void btnRGAdresse_Click(object sender, EventArgs e)
        {
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrRGManuell / tbRGMatchcode_TextChanged</summary>
        ///<remarks></remarks>
        private void tbRGMatchcode_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            string SearchText = tbRGMatchcode.Text.ToString();
            string Ausgabe = string.Empty;

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();
            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            tbRGAdresse.Text = Functions.GetADRStringFromTable(dtTmp);
            if (dtTmp.Rows.Count > 0)
            {
                Rechnung.ADR_RGEmpfaenger.ID = Functions.GetADR_IDFromTable(dtTmp);
                Rechnung.ADR_RGEmpfaenger.Fill();
                tbInfoText.Text = Rechnung.ADR_RGEmpfaenger.ADRTexte.RechnungManuell_Text;
            }
            else
            {
                Rechnung.ADR_RGEmpfaenger.ID = 0;
            }
        }
        ///<summary>ctrRGManuell / SetRechnungsEmpfaenger</summary>
        ///<remarks></remarks>
        public void SetRechnungsEmpfaenger(decimal myDecADR_ID)
        {
            string strMC = string.Empty;
            Rechnung.ADR_RGEmpfaenger.ID = myDecADR_ID;
            Rechnung.ADR_RGEmpfaenger.Fill();
            tbInfoText.Text = Rechnung.ADR_RGEmpfaenger.ADRTexte.RechnungManuell_Text;
            strMC = Rechnung.ADR_RGEmpfaenger.ViewID;
            tbRGMatchcode.Text = strMC;
        }
        ///<summary>ctrRGManuell / tbRGAdresse_TextChanged</summary>
        ///<remarks></remarks>
        private void tbRGAdresse_TextChanged(object sender, EventArgs e)
        {
            if (tbRGAdresse.Text == string.Empty)
            {
                scRechnungspositionen.Enabled = false;
            }
            else
            {
                scRechnungspositionen.Enabled = true;
            }
        }
        ///<summary>ctrRGManuell / tsbtnAdd_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            clsRGVorlagenTxt Vorlage = new clsRGVorlagenTxt();
            Vorlage._GL_User = this.GL_User;
            Vorlage.Vorlage = tbVorlage.Text.Trim();

            string strError = string.Empty;
            decimal decPreis = -1;
            if ((cbVorlage.Checked) && (tbVorlage.Text == string.Empty))
            {
                strError += "Vorlagenname fehlt." + Environment.NewLine;
            }
            if ((cbVorlage.Checked) && (Vorlage.Vorlage != String.Empty))
            {
                if (Vorlage.ExistVorlage())
                {
                    strError += "Vorlagenname existiert bereits. Bitte ändern." + Environment.NewLine;
                }
            }
            if (tbRGText.Text == string.Empty)
            {
                strError += "Rechnungstext fehlt." + Environment.NewLine;
            }
            if (nudNettoPreis.Value == 0)
            {
                strError += "Positionspreis fehlt." + Environment.NewLine;
            }
            if ((nudNettoPreis.Value == 0) || (nudMenge.Value == 0))
            {
                strError += "Rechnungsposition muss > 0,00 € sein!" + Environment.NewLine;
            }


            if (strError == string.Empty)
            {
                string rgtext = tbRGText.Text; //.Replace(System.Environment.NewLine, " ");  // CHAR(13) + CHAR(10) 
                clsRGPositionen RGPos = new clsRGPositionen();
                RGPos.RGText = rgtext;
                RGPos.NettoPreis = nudNettoPreis.Value;
                //RGPos.Position = RowNumber+1 // Später
                Int32 iTmp = 0;
                Int32.TryParse(tbKontoNr.Text, out iTmp);
                RGPos.FibuKto = iTmp;
                RGPos.AbrechnungsArt = clsRechnung.const_RechnungsArt_Manuell;
                decimal decTmp = 0;
                if (EditRow == -1)
                {
                    DataRow row = dtRechnung.NewRow();
                    row["Pos"] = dtRechnung.Rows.Count + 1;
                    row["Text"] = rgtext;
                    //row["Netto €"] = nudNettoPreis.Value * (Int32)nudMenge.Value;
                    row["Netto €"] = nudNettoPreis.Value * Decimal.Round(nudMenge.Value, 3);
                    row["Abrechnungsart"] = clsRechnung.const_RechnungsArt_Manuell;
                    row["Einheit"] = cbEinheit.SelectedValue;
                    row["RGPosText"] = clsRechnung.const_RechnungsArt_Manuell;
                    //row["RGPosText"] = rgtext;
                    row["€/Einheit"] = nudNettoPreis.Value;
                    iTmp = 0;
                    Int32.TryParse(tbKontoNr.Text, out iTmp);
                    row["FibuKto"] = iTmp;
                    row["Menge"] = nudMenge.Value;
                    row["SumCalc"] = true;
                    row["PricePerUnitFactor"] = nudMenge.Value;
                    row["TarifPricePerUnit"] = nudNettoPreis.Value;
                    dtRechnung.Rows.Add(row);
                }
                else
                {
                    dtRechnung.Rows[EditRow]["Text"] = rgtext;
                    dtRechnung.Rows[EditRow]["Netto €"] = nudNettoPreis.Value * Decimal.Round(nudMenge.Value, 3);
                    dtRechnung.Rows[EditRow]["Abrechnungsart"] = clsRechnung.const_RechnungsArt_Manuell;
                    dtRechnung.Rows[EditRow]["Einheit"] = cbEinheit.SelectedValue;
                    dtRechnung.Rows[EditRow]["RGPosText"] = clsRechnung.const_RechnungsArt_Manuell;
                    //dtRechnung.Rows[EditRow]["RGPosText"] = rgtext;
                    iTmp = 0;
                    Int32.TryParse(tbKontoNr.Text, out iTmp);
                    dtRechnung.Rows[EditRow]["FibuKto"] = iTmp;
                    dtRechnung.Rows[EditRow]["Menge"] = nudMenge.Value;
                    dtRechnung.Rows[EditRow]["SumCalc"] = true;
                    dtRechnung.Rows[EditRow]["€/Einheit"] = nudNettoPreis.Value;
                    dtRechnung.Rows[EditRow]["PricePerUnitFactor"] = nudMenge.Value;
                    dtRechnung.Rows[EditRow]["TarifPricePerUnit"] = nudNettoPreis.Value;
                }
                //Reset der Positionen
                Int32 iPos = 1;
                foreach (DataRow row in this.dtRechnung.Rows)
                {
                    row["Pos"] = iPos;
                    iPos++;
                }

                //speichern der Vorlage
                if (cbVorlage.Checked)
                {
                    Vorlage.Vorlage = tbVorlage.Text.Trim();
                    Vorlage.Vorlagentext = tbRGText.Text;
                    Vorlage.EinzelPreis = nudNettoPreis.Value;
                    Vorlage.Add();
                    InitDGVVorlagen();
                }
                tsbtnSave.Enabled = true;
                CalcPreis();
                EditRow = -1;
                InitPositionEdit();
                resizeRows();
            }
            else
            {
                clsMessages.Allgemein_ERRORTextShow(strError);
            }
        }
        ///<summary>ctrRGManuell / nudNettoPreis_ValueChanged</summary>
        ///<remarks></remarks>
        private void nudNettoPreis_ValueChanged(object sender, EventArgs e)
        {
            if (nudNettoPreis.Value < 0)
            {
                nudNettoPreis.Value = 0;
            }
        }
        ///<summary>ctrRGManuell / dgvPositionen_CellClick</summary>
        ///<remarks></remarks>
        private void dgvPositionen_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (this.dgvPositionen.Rows.Count > 0)
            {
                if (e.RowIndex > -1)
                {
                    decimal decTmp = 0;
                    decimal.TryParse(e.Row.Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        this.Rechnung.RGPos.ID = decTmp;
                        this.Rechnung.RGPos.Fill();
                    }
                }
            }
        }

        ///<summary>ctrRGManuell / dgvPositionen_MouseDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvPositionen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SetRGPostoFrm();
        }
        ///<summary>ctrRGManuell / SetRGPostoFrm</summary>
        ///<remarks></remarks>
        private void SetRGPostoFrm()
        {
            EditRow = dgvPositionen.CurrentRow.Index;
            Functions.SetComboToSelecetedValue(ref cbExtraChargeKontoText, dtRechnung.Rows[EditRow]["FibuKto"].ToString());
            tbRGText.Text = dtRechnung.Rows[EditRow]["Text"].ToString();
            //Menge
            decimal decTmp = 0; ;
            decimal.TryParse(dtRechnung.Rows[EditRow]["Menge"].ToString(), out decTmp);
            nudMenge.Value = decTmp;
            //Einheit
            Functions.SetComboToSelecetedValue(ref cbEinheit, dtRechnung.Rows[EditRow]["Einheit"].ToString());
            //Nettopreis
            decTmp = 0;
            Decimal.TryParse(dtRechnung.Rows[EditRow]["€/Einheit"].ToString(), out decTmp);
            nudNettoPreis.Value = decTmp;
        }
        ///<summary>ctrRGManuell / tsbtnDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPositionen.Rows.Count > 0)
            {
                if (bUpdate)
                {
                    if (this.Rechnung.RGPos.ID > 0)
                    {
                        this.Rechnung.RGPos.Delete();
                        this.Rechnung.Fill();
                        FilldtRechnungForUpdate();
                        InitDGV();
                    }
                }
                else
                {
                    dtRechnung.Rows.Remove(dtRechnung.Rows[dgvPositionen.CurrentRow.Index]);
                }
                tsbtnSave.Enabled = false;

                for (int i = 0; i < dtRechnung.Rows.Count; i++)
                {
                    dtRechnung.Rows[i]["Pos"] = i + 1;
                    tsbtnSave.Enabled = true;
                }
            }
            CalcPreis();
        }
        ///<summary>ctrRGManuell / toolStripButton1_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Rechnung.dtRGPositionen = dtRechnung;
            Rechnung.Datum = dtpRechnungsDate.Value;
            Rechnung.AbrZeitraumVon = dtpAbrVon.Value;
            Rechnung.AbrZeitraumBis = dtpAbrBis.Value;
            int iZZ = 14;
            if (Rechnung.ADR_RGEmpfaenger.Kunde.Zahlungziel > 0)
            {
                iZZ = Rechnung.ADR_RGEmpfaenger.Kunde.Zahlungziel;
            }
            Rechnung.faellig = dtpRechnungsDate.Value.AddDays(iZZ);
            Rechnung.NettoBetrag = decimal.Parse(tbRGNetto.Text);
            Rechnung.BruttoBetrag = decimal.Parse(tbRGBrutto.Text);
            Rechnung.MwStBetrag = decimal.Parse(tbRGMwStBetrag.Text);
            Rechnung.MwStSatz = nudRGMwSt.Value;
            Rechnung.bezahlt = dtpRechnungsDate.MaxDate;
            Rechnung.Druckdatum = dtpRechnungsDate.MaxDate;
            Rechnung.RGBookPrintDate = Globals.DefaultDateTimeMinValue;
            Rechnung.Empfaenger = Rechnung.ADR_RGEmpfaenger.ID;
            Rechnung.Auftraggeber = Rechnung.ADR_RGEmpfaenger.ID;
            Rechnung.RGArt = clsRechnung.const_RechnungsArt_Manuell;
            Rechnung.InfoText = this.tbInfoText.Text;
            Rechnung.FibuInfo = this.tbInfoTextFIBU.Text;
            Rechnung.DocName = this.cbDocName.Text;

            bool bGS = false;
            bool bStorno = false;

            switch (this.cbDocName.Text)
            {
                case clsRechnung.const_RGDocTitel_RGStorno:
                case clsRechnung.const_RGDocTitel_RGKorrektur:
                    bGS = false;
                    bStorno = true;
                    break;

                case clsRechnung.const_RGDocTitel_GSStorno:
                case clsRechnung.const_RGDocTitel_GSKorrektur:
                    bGS = true;
                    bStorno = true;
                    break;

                case clsRechnung.const_RGDocTitel_RG:
                    bGS = false;
                    bStorno = false;
                    break;

                case clsRechnung.const_RGDocTitel_GS:
                    bGS = true;
                    bStorno = false;
                    break;
            }
            Rechnung.GS = bGS;
            Rechnung.Storno = bStorno;

            //Rechnung.GetRechnungsnummer(true,false);
            this.tbRechnungID.Text = Rechnung.RGNr.ToString();
            if (
                   (bUpdate) &&
                   (Rechnung.ID > 0)
               )
            {
                Rechnung.Update(false);
            }
            else
            {
                Rechnung.Add(true);
            }

            tsbtnPrint.Enabled = true;
            tsbtnPrintPrev.Enabled = true;
            tsbtnRGRevision.Enabled = (!Rechnung.Druck);
            tsbtnSave.Enabled = false;
            scRechnungspositionen.Enabled = false;
            panRechnungsdaten.Enabled = false;
            SetVorschauLabel(string.Empty);

            InitDgvNotPrintedInvoices();
        }
        ///<summary>ctrRGManuell / SetVorschauLabel</summary>
        ///<remarks>Setzt die Vorschau Info.</remarks>
        private void SetVorschauLabel(string strTxt)
        {
            lInfo.Text = strTxt;
        }
        ///<summary>ctrRGManuell / CalcPreis</summary>
        ///<remarks></remarks>
        private void CalcPreis()
        {
            decimal neuPreis = 0;
            if (dgvPositionen.Rows.Count > 0)
            {
                for (int i = 0; i < dtRechnung.Rows.Count; i++)
                {
                    neuPreis += Decimal.Round(((decimal)dtRechnung.Rows[i]["Netto €"]), 2);
                }
            }
            tbRGNetto.Text = Functions.FormatDecimal(neuPreis);
            decimal decMwStBetrag = Decimal.Round((neuPreis * (nudRGMwSt.Value / 100)), 2);
            tbRGMwStBetrag.Text = Functions.FormatDecimal(decMwStBetrag);
            //decimal decBruttoBetrag = Decimal.Round((neuPreis + (neuPreis * (nudRGMwSt.Value / 100))), 2);
            decimal decBruttoBetrag = Decimal.Round((neuPreis + decMwStBetrag), 2);
            tbRGBrutto.Text = Functions.FormatDecimal(decBruttoBetrag);
            if (bUpdate)
            {
                if (this.Rechnung.ID > 0)
                {
                    this.Rechnung.NettoBetrag = neuPreis;
                    this.Rechnung.MwStBetrag = decMwStBetrag;
                    this.Rechnung.MwStSatz = nudRGMwSt.Value;
                    this.Rechnung.BruttoBetrag = decBruttoBetrag;
                    this.Rechnung.Update(true);
                    this.Rechnung.Fill();
                }
            }
        }
        ///<summary>ctrRGManuell / nudRGMwSt_ValueChanged</summary>
        ///<remarks></remarks>
        private void nudRGMwSt_ValueChanged(object sender, EventArgs e)
        {
            if (nudRGMwSt.Value < 0)
            {
                nudRGMwSt.Value = 0;
            }
            else
            {
                CalcPreis();
            }
        }
        ///<summary>ctrRGManuell / tsbtnPositionNeu_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPositionNeu_Click(object sender, EventArgs e)
        {
            InitPositionEdit();
        }
        ///<summary>ctrRGManuell / InitPositionEdit</summary>
        ///<remarks></remarks>
        private void InitPositionEdit()
        {
            cbVorlage.Checked = false;
            tbVorlage.Text = string.Empty;

            if (cbExtraChargeKontoText.Items.Count > 0)
            {
                cbExtraChargeKontoText.SelectedIndex = 0;
            }
            tbRGText.Text = string.Empty;
            nudNettoPreis.Value = 0;
            nudMenge.Value = 0;
        }
        ///<summary>ctrRGManuell / InitManuelleRechnung</summary>
        ///<remarks></remarks>
        private void InitManuelleRechnung()
        {
            cbGutschrift.Checked = false;
            tbRGNetto.Text = "0";
            tbRGBrutto.Text = "0";
            tbRGMwStBetrag.Text = "0";
            tbRGMatchcode.Text = string.Empty;
            tbInfoText.Text = string.Empty;
            tbInfoTextFIBU.Text = string.Empty;
            InitPositionEdit();
        }
        ///<summary>ctrRGManuell / tsbtnRechnungNeu_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRechnungNeu_Click(object sender, EventArgs e)
        {
            SetVorschauLabel(string.Empty);
            newRechnung();
            panRechnungsdaten.Enabled = true;
        }
        ///<summary>ctrRGManuell / tsbtnRechnutsbtnEdit_ClickngNeu_Click</summary>
        ///<remarks></remarks>
        private void tsbtnEdit_Click(object sender, EventArgs e)
        {
            SetRGPostoFrm();
        }
        ///<summary>ctrRGManuell / toolStripButton2_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            PrintOrPreview(true);
            InitManuelleRechnung();
            tsbtnRGRevision.Enabled = (!Rechnung.Druck);
            newRechnung();
        }
        ///<summary>ctrRGManuell / PrintOrPreview</summary>
        ///<remarks></remarks>
        private void PrintOrPreview(bool bDirectPrint)
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                if (!this.Rechnung.Druck)
                {
                    //REchnung
                    if (this.Rechnung.GS)
                    {
                        this.DokumentenArt = enumDokumentenArt.ManuelleGutschrift.ToString();
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint);

                        if (bDirectPrint)
                        {
                            this.Rechnung.Druck = true;
                            this.Rechnung.Druckdatum = DateTime.Now;
                            this.Rechnung.UpdateRechnungPrint();
                            this.Rechnung.Fill();
                        }
                    }
                    else
                    {
                        this.DokumentenArt = enumDokumentenArt.manuelleRGGS.ToString();
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint);
                        if (bDirectPrint)
                        {
                            this.Rechnung.Druck = true;
                            this.Rechnung.Druckdatum = DateTime.Now;
                            this.Rechnung.UpdateRechnungPrint();
                            this.Rechnung.Fill();
                        }
                    }
                }
                else
                {
                    if (clsMessages.Fakturierung_RGGSPrintAgain())
                    {
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint);
                    }
                }
            }
            else
            {
                this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.Rechnung.Empfaenger, this._ctrMenu._frmMain.system.AbBereich.ID);
                if (!this.Rechnung.Druck)
                {
                    //REchnung
                    if (this.Rechnung.GS)
                    {
                        this.RepDocSettings = null;
                        this.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.ManuelleGutschrift.ToString());
                        if (this.RepDocSettings is clsReportDocSetting)
                        {
                            this.DokumentenArt = this.RepDocSettings.DocKey;
                            this._ctrMenu.OpenFrmReporView(this, bDirectPrint);
                            Thread.Sleep(100);

                            if (bDirectPrint)
                            {
                                this.Rechnung.Druck = true;
                                this.Rechnung.Druckdatum = DateTime.Now;
                                this.Rechnung.UpdateRechnungPrint();
                                this.Rechnung.Fill();
                            }
                        }
                    }
                    else
                    {
                        this.RepDocSettings = null;
                        this.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.Manuellerechnung.ToString());
                        if (this.RepDocSettings is clsReportDocSetting)
                        {
                            this.DokumentenArt = enumIniDocKey.Manuellerechnung.ToString();
                            this._ctrMenu.OpenFrmReporView(this, bDirectPrint);
                            if (bDirectPrint)
                            {
                                this.Rechnung.Druck = true;
                                this.Rechnung.Druckdatum = DateTime.Now;
                                this.Rechnung.UpdateRechnungPrint();
                                this.Rechnung.Fill();
                            }
                        }
                    }
                }
                else
                {
                    if (clsMessages.Fakturierung_RGGSPrintAgain())
                    {
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint);
                    }
                }
            }
        }
        ///<summary>ctrRGManuell / cbGutschrift_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbGutschrift_CheckedChanged(object sender, EventArgs e)
        {
            clsPrimeKeys pk = new clsPrimeKeys();
            pk.sys = this._ctrMenu._frmMain.system;
            pk._GL_User = this._ctrMenu._frmMain.GL_User;
            pk.Mandanten_ID = this._ctrMenu._frmMain.GL_System.sys_MandantenID;
            pk.AbBereichID = this._ctrMenu._frmMain.system.AbBereich.ID;

            if (cbGutschrift.Checked)
            {
                pk.GetNEWGSNrWOUpdate();
                tbRechnungID.Text = pk.GSNr.ToString();
                FillComboDocName();
                Functions.SetComboToSelecetedValue(ref cbDocName, clsRechnung.const_RGDocTitel_GS);
                lblRGGSText.Text = "Gutschrift-Nr:";
            }
            else
            {
                pk.GetNEWRGNrWOUpdate();
                tbRechnungID.Text = pk.RGNr.ToString();
                FillComboDocName();
                Functions.SetComboToSelecetedValue(ref cbDocName, clsRechnung.const_RGDocTitel_RG);
                lblRGGSText.Text = "Rechnungs-Nr:";
            }
        }
        ///<summary>ctrRGManuell / dtpRechnungsDate_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpRechnungsDate_ValueChanged(object sender, EventArgs e)
        {
            tbRechnungID.Text = Rechnung.RGNr.ToString();
        }
        ///<summary>ctrRGManuell / tsbDummy_Click</summary>
        ///<remarks></remarks>
        private void tsbDummy_Click(object sender, EventArgs e)
        {
            newRechnung();

            clsRGPositionen RGPos = new clsRGPositionen();
            //RGPos.RGText = "Rechnungsplatzhalter für FIBU.";
            //tbInfoTextFIBU.Text = "Rechnungsplatzhalter für FIBU.";
            RGPos.RGText = clsRechnung.const_RGText_Dummy;
            tbInfoTextFIBU.Text = clsRechnung.const_RGText_Dummy;

            RGPos.NettoPreis = 0;
            //RGPos.Position = RowNumber+1 // Später
            RGPos.FibuKto = 0;
            RGPos.AbrechnungsArt = clsRechnung.const_RechnungsArt_Manuell;
            DataRow row = dtRechnung.NewRow();
            row["ID"] = 0;
            row["Pos"] = dtRechnung.Rows.Count + 1;
            row["Text"] = RGPos.RGText;
            row["Netto €"] = 0;
            row["Abrechnungsart"] = clsRechnung.const_RechnungsArt_Manuell;
            row["Einheit"] = "";
            row["RGPosText"] = clsRechnung.const_RechnungsArt_Manuell;
            row["€/Einheit"] = 0;
            row["FibuKto"] = 0;
            row["Menge"] = 0;
            row["SumCalc"] = true;
            row["€/Einheit"] = 0;
            dtRechnung.Rows.Add(row);

            clsMandanten man = new clsMandanten();
            man.ID = this._ctrMenu._frmMain.GL_System.sys_MandantenID;
            man.GetMandantByID();
            Rechnung.ADR_RGEmpfaenger = new clsADR();
            Rechnung.ADR_RGEmpfaenger.ID = man.ADR_ID;
            Rechnung.ADR_RGEmpfaenger.Fill();
            tbRGMatchcode.Text = Rechnung.ADR_RGEmpfaenger.ViewID;
            tsbtnSave.Enabled = true;
        }
        ///<summary>ctrRGManuell / cbVorlage_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbVorlage_CheckedChanged(object sender, EventArgs e)
        {
            tbVorlage.Enabled = (cbVorlage.Checked);
            if (!cbVorlage.Checked)
            {
                tbVorlage.Text = string.Empty;
            }
        }
        ///<summary>ctrRGManuell / tsbtnRefreshVorlage_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefreshVorlage_Click(object sender, EventArgs e)
        {
            InitDGVVorlagen();
        }
        ///<summary>ctrRGManuell / tsbtnDeleteVorlage_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDeleteVorlage_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                RGVorlagenTxt.Delete();
                clsMessages.DeleteOKAllgemein();
                InitDGVVorlagen();
            }
        }
        ///<summary>ctrRGManuell / tsbtnPrintPrev_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrintPrev_Click(object sender, EventArgs e)
        {
            PrintOrPreview(false);
            tsbtnRGRevision.Enabled = (!Rechnung.Druck);
        }
        ///<summary>ctrRGManuell / tsbtnPrintPrev_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClearInfoText_Click(object sender, EventArgs e)
        {
            tbInfoText.Text = string.Empty;
        }
        ///<summary>ctrRGManuell / tsbtnPrintPrev_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClearInfoTextFIBU_Click(object sender, EventArgs e)
        {
            tbInfoTextFIBU.Text = string.Empty;
        }
        ///<summary>ctrRGManuell / tsbtnRGRevision_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRGRevision_Click(object sender, EventArgs e)
        {
            RGRevision();
            SetVorschauLabel(ctrRGManuell.const_LableInfo_Korrektur);
        }
        ///<summary>ctrRGManuell / FilldtRechnungForUpdate</summary>
        ///<remarks></remarks>
        private void FilldtRechnungForUpdate()
        {
            this.dtRechnung.Rows.Clear();
            for (Int32 i = 0; i <= this.Rechnung.dtRechnungsPositionen.Rows.Count - 1; i++)
            {
                DataRow row = dtRechnung.NewRow();
                decimal decTmp = 0;
                Decimal.TryParse(this.Rechnung.dtRechnungsPositionen.Rows[i]["ID"].ToString(), out decTmp);
                row["ID"] = decTmp;
                Int32 iTmp = 0;
                Int32.TryParse(this.Rechnung.dtRechnungsPositionen.Rows[i]["Position"].ToString(), out iTmp);
                row["Pos"] = iTmp;
                row["Text"] = this.Rechnung.dtRechnungsPositionen.Rows[i]["RGText"].ToString();
                decTmp = 0;
                Decimal.TryParse(this.Rechnung.dtRechnungsPositionen.Rows[i]["NettoPreis"].ToString(), out decTmp);
                row["Netto €"] = decTmp;
                row["Abrechnungsart"] = clsRechnung.const_RechnungsArt_Manuell;
                row["Einheit"] = this.Rechnung.dtRechnungsPositionen.Rows[i]["Abrechnungseinheit"].ToString(); ;
                row["RGPosText"] = clsRechnung.const_RechnungsArt_Manuell;
                decTmp = 0;
                Decimal.TryParse(this.Rechnung.dtRechnungsPositionen.Rows[i]["EinzelPreis"].ToString(), out decTmp);
                row["€/Einheit"] = decTmp;
                iTmp = 0;
                Int32.TryParse(this.Rechnung.dtRechnungsPositionen.Rows[i]["FibuKto"].ToString(), out iTmp);
                row["FibuKto"] = iTmp;
                decTmp = 0;
                Decimal.TryParse(this.Rechnung.dtRechnungsPositionen.Rows[i]["Menge"].ToString(), out decTmp);
                row["Menge"] = decTmp;
                row["SumCalc"] = true;
                decTmp = 0;
                Decimal.TryParse(this.Rechnung.dtRechnungsPositionen.Rows[i]["PricePerUnitFactor"].ToString(), out decTmp);
                row["PricePerUnitFactor"] = decTmp;
                decTmp = 0;
                Decimal.TryParse(this.Rechnung.dtRechnungsPositionen.Rows[i]["TarifPricePerUnit"].ToString(), out decTmp);
                row["TarifPricePerUnit"] = decTmp;

                dtRechnung.Rows.Add(row);
            }
        }
        ///<summary>ctrRGManuell / SetRGKopfToFrm</summary>
        ///<remarks></remarks>
        private void SetRGKopfToFrm()
        {
            if (this.Rechnung.ID > 0)
            {
                SetRechnungsEmpfaenger(Rechnung.Empfaenger);
                dtpAbrVon.Value = Rechnung.AbrZeitraumVon;
                dtpAbrBis.Value = Rechnung.AbrZeitraumBis;
                dtpRechnungsDate.Value = Rechnung.Datum;
                //Functions.SetComboToSelecetedText(ref cbDocName, Rechnung.DocName);
                Functions.SetComboToSelecetedValue(ref cbDocName, Rechnung.DocName);
                nudRGMwSt.Value = Rechnung.MwStSatz;

                decimal decNettoRG = 0;
                if (this.Rechnung.dtRechnungsPositionen.Rows.Count > 0)
                {
                    for (int i = 0; i < this.Rechnung.dtRechnungsPositionen.Rows.Count; i++)
                    {
                        decNettoRG += Decimal.Round((decimal)this.Rechnung.dtRechnungsPositionen.Rows[i]["NettoPreis"], 2);
                    }
                }

                Rechnung.NettoBetrag = decNettoRG;
                Rechnung.MwStBetrag = Decimal.Round((decNettoRG * (nudRGMwSt.Value / 100)), 2);
                Rechnung.BruttoBetrag = Decimal.Round((decNettoRG + Rechnung.MwStBetrag), 2);
                Rechnung.Update(true);
                Rechnung.Fill();

                //if (!Rechnung.NettoBetrag.Equals(decNettoRG))
                //{                   
                //    Rechnung.NettoBetrag = decNettoRG;
                //    Rechnung.MwStBetrag = Decimal.Round((decNettoRG * (nudRGMwSt.Value / 100)), 2);
                //    Rechnung.BruttoBetrag = Decimal.Round((decNettoRG + Rechnung.MwStBetrag), 2);
                //    Rechnung.Update(true);
                //    Rechnung.Fill();
                //}


                tbRGNetto.Text = Functions.FormatDecimal(Rechnung.NettoBetrag);
                tbRGBrutto.Text = Functions.FormatDecimal(Rechnung.BruttoBetrag);
                tbRGMwStBetrag.Text = Functions.FormatDecimal(Rechnung.MwStBetrag);

                tbRechnungID.Text = Rechnung.RGNr.ToString();
                cbGutschrift.Checked = Rechnung.GS;
                this.tbInfoText.Text = Rechnung.InfoText;
                this.tbInfoTextFIBU.Text = Rechnung.FibuInfo;
            }
        }
        ///<summary>ctrRGManuell / textBox1_Validated</summary>
        ///<remarks></remarks>
        private void textBox1_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            Decimal.TryParse(textBox1.Text, out decTmp);
            if (decTmp > 0)
            {
                this.Rechnung.ID = decTmp;
                RGRevision();
            }
        }
        ///<summary>ctrRGManuell / RGRevision</summary>
        ///<remarks></remarks>
        private void RGRevision()
        {
            if (this.Rechnung.ID > 0)
            {
                bUpdate = true;
                panRechnungsdaten.Enabled = true;
                scRechnungspositionen.Enabled = true;
                //Rechnungsdaten holen
                //this.Rechnung.Fill();
                SetRGKopfToFrm();
                FilldtRechnungForUpdate();
                InitDGV();
                this.tsbtnSave.Enabled = true;
                tsbtnRGRevision.Enabled = true;
                tsbtnPrint.Enabled = true;
                tsbtnPrintPrev.Enabled = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgvNotPrintedInvoices()
        {
            this.dgvNotPrintedInvoices.DataSource = clsRechnung.GetNotPrintedInvoices((int)this._ctrMenu._frmMain.system.AbBereich.ID);

            if (this.dgvNotPrintedInvoices.Rows.Count > 0)
            {

                for (Int32 i = 0; i <= this.dgvNotPrintedInvoices.Columns.Count - 1; i++)
                {
                    string ColName = dgvNotPrintedInvoices.Columns[i].Name.ToString();
                    this.dgvNotPrintedInvoices.Columns[i].ReadOnly = true;
                    switch (ColName)
                    {
                        case "ID":
                            this.dgvNotPrintedInvoices.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;
                        case "RGNr":
                            this.dgvNotPrintedInvoices.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;
                        case "Datum":
                            this.dgvNotPrintedInvoices.Columns[i].FormatString = "{0:d}";
                            this.dgvNotPrintedInvoices.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;
                        case "MwStSatz":
                            this.dgvNotPrintedInvoices.Columns[i].FormatString = "{0:N2}";
                            this.dgvNotPrintedInvoices.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                            break;
                        case "MwStBetrag":
                            this.dgvNotPrintedInvoices.Columns[i].FormatString = "{0:N2}";
                            this.dgvNotPrintedInvoices.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                            break;
                        case "NettoBetrag":
                            this.dgvNotPrintedInvoices.Columns[i].FormatString = "{0:N2}";
                            this.dgvNotPrintedInvoices.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                            break;
                        case "BruttoBetrag":
                            this.dgvNotPrintedInvoices.Columns[i].FormatString = "{0:N2}";
                            this.dgvNotPrintedInvoices.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                            break;
                        default:
                            this.dgvNotPrintedInvoices.Columns[i].IsVisible = false;
                            break;
                    }
                }
                this.dgvNotPrintedInvoices.BestFitColumns();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myIsUp"></param>
        private void Loop_dgvNotPrintedInvoieces(bool myIsUp)
        {
            if (this.dgvNotPrintedInvoices.Rows.Count > 0)
            {

                for (Int32 i = 0; i <= this.dgvNotPrintedInvoices.Rows.Count - 1; i++)
                {
                    if (dgvNotPrintedInvoices.Rows[i].IsSelected)
                    {
                        if (myIsUp)
                        {
                            int x = i;
                            if ((i - 1) >= 0)
                            {
                                x = (i - 1);
                            }
                            dgvNotPrintedInvoices.Rows[x].IsSelected = true;
                            dgvNotPrintedInvoices.Rows[x].IsCurrent = true;
                        }
                        else
                        {
                            int x = i;
                            if ((i + 1) <= this.dgvNotPrintedInvoices.RowCount - 1)
                            {
                                x = (i + 1);
                            }
                            dgvNotPrintedInvoices.Rows[x].IsSelected = true;
                            dgvNotPrintedInvoices.Rows[x].IsCurrent = true;
                        }
                        i = this.dgvNotPrintedInvoices.RowCount;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNotPrintedInvoices_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvNotPrintedInvoices.SelectedRows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvNotPrintedInvoices.SelectedRows.Count - 1; i++)
                {
                    int iTmp = 0;
                    if (int.TryParse(this.dgvNotPrintedInvoices.SelectedRows[i].Cells["ID"].Value.ToString(), out iTmp))
                    {
                        if (iTmp > 0)
                        {
                            this.Rechnung = new clsRechnung();
                            this.Rechnung.InitCls(this._ctrMenu._frmMain.system
                                                  , this._ctrMenu._frmMain.GL_System
                                                  , this._ctrMenu._frmMain.GL_User
                                                  , iTmp);
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
        private void tsbtnRefreshNotPrintedInvoices_Click(object sender, EventArgs e)
        {
            InitDgvNotPrintedInvoices();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNotPrintedInvoices_CellClick(object sender, GridViewCellEventArgs e)
        {
            this.dgvNotPrintedInvoices.Rows[e.RowIndex].IsSelected = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNotPrintedInvoices_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            InitForRevision();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnUpNotPrintedInvoice_Click(object sender, EventArgs e)
        {
            Loop_dgvNotPrintedInvoieces(true);
            InitForRevision();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDownNotPrintedInvoice_Click(object sender, EventArgs e)
        {
            Loop_dgvNotPrintedInvoieces(false);
            InitForRevision();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitForRevision()
        {
            RGRevision();
            panRechnungsdaten.Enabled = false;
            scRechnungspositionen.Enabled = false;
            SetVorschauLabel(string.Empty);
        }

    }
}
