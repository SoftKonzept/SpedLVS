using LVS;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmRGGSErstellen : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public Int32 SearchButton;
        public DataTable dt = new DataTable("RGGSDaten");
        public bool AdressatSuche = true;
        internal decimal KundeID = 0;
        internal decimal SU_ID = 0;
        internal decimal zwZM = 0;
        internal decimal zwAuflieger = 0;
        internal decimal zwSU = 0;
        internal decimal auftrag = 0;
        internal decimal auftragPos = 0;
        internal decimal AP_ID = 0;
        public decimal MandantenID = 0;
        internal bool isGS = false;
        internal bool getResource = false;
        internal bool ResourceZMLoad = false;
        internal bool ResourceAufliegerLoad = false;

        public frmRGGSErstellen()
        {
            InitializeComponent();
            LoadComboResource();
        }
        //
        private void frmRGGSErstellen_Load(object sender, EventArgs e)
        {
            SetFrmForRechnung();
            // Rescourcen zuweisen
            gbResourcen.Enabled = false;

            //Auftrag zuweisen
            gbAuftrag.Enabled = false;

            //DataTable Columns hinzufügen
            SetColForTable();
        }
        //
        //------------- init Columns ------------
        //
        private void SetColForTable()
        {
            dt.Columns.Add("Frachten_ID", typeof(decimal));
            dt.Columns.Add("AP_ID", typeof(decimal));
            dt.Columns.Add("Fracht", typeof(decimal));
            dt.Columns.Add("Fracht_ADR", typeof(decimal));
            dt.Columns.Add("Frachttext", typeof(string));
            dt.Columns.Add("MwStSatz", typeof(decimal));
            dt.Columns.Add("zw_ZM", typeof(decimal));
            dt.Columns.Add("zw_Auflieger", typeof(decimal));
            dt.Columns.Add("zw_SU", typeof(decimal));
            dt.Columns.Add("RG_Nr", typeof(decimal));
            dt.Columns.Add("GS", typeof(bool));
        }

        //
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            OpenADRPanel();
        }
        //
        //
        //
        private void OpenADRPanel()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmADRPanelFakturierung)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmADRPanelFakturierung));
            }
            //frmADRPanelFakturierung ADR = new frmADRPanelFakturierung(this);
            frmADRPanelFakturierung ADR = new frmADRPanelFakturierung();
            ADR.SetRGGSFrm(this);
            ADR.StartPosition = FormStartPosition.CenterScreen;
            ADR.Dock = DockStyle.Fill;
            ADR.Show();
            ADR.BringToFront();
        }
        //
        //--------- close Frm -------------------
        //
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        //------------------ ADR - ID für ------------------
        //
        public void SetADRRecAfterADRSearch(decimal ADR_ID)
        {
            string strE = string.Empty;
            DataSet dsADR = clsADR.ReadADRbyID(ADR_ID);
            for (Int32 i = 0; i < dsADR.Tables[0].Rows.Count; i++)
            {
                strE = dsADR.Tables[0].Rows[i]["ViewID"].ToString() + " - ";
                strE = strE + dsADR.Tables[0].Rows[i]["KD_ID"].ToString() + " - ";
                strE = strE + dsADR.Tables[0].Rows[i]["Name1"].ToString() + " - ";
                strE = strE + dsADR.Tables[0].Rows[i]["PLZ"].ToString() + " - ";
                strE = strE + dsADR.Tables[0].Rows[i]["Ort"].ToString();
            }

            if (AdressatSuche)
            {
                tbAuftraggeber.Text = strE;
                KundeID = ADR_ID;
                //Eintrag MwST Satz
                nudRGMwSt.Value = clsKunde.GetMwStSatz(ADR_ID);
                nudRGMwSt.Refresh();
            }
            else
            {
                tbSU.Text = strE;
                AdressatSuche = true;
                SU_ID = ADR_ID;
            }
        }
        //
        //--------- SU Resource zurücksetzen -----------------
        //
        private void SetSUBack()
        {
            SU_ID = 0;
            tbSU.Text = string.Empty;
        }
        //
        //------------ Checkbox / umschalten Rechnung Gutschrift --------------
        //
        private void cbDocArt_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDocArt.Checked == true)  // Gutschrift
            {
                SetFrmForGutschrift();
            }
            else
            {
                SetFrmForRechnung();
            }
        }
        //
        //------------ Frm umstellen auf GS / RG ------------------------
        //
        private void SetFrmForGutschrift()
        {
            label1.Text = "GUTSCHRIFT";
            gbDocAdressat.Text = "Gutschriftsempfänger";
            gbRGAnzeige.Text = "Anzeige Gutschriftsbetrag";
            cbDocArt.Text = "Rechnung erstellen";
            gbDocText.Text = "Gutschriftstext";
            lNetto.Text = "Gutschrift Netto €";
            lBrutto.Text = "Gutschrift Brutto €";
            base.Text = "manuelle Gutschriftserstellung";
            isGS = true;
        }
        private void SetFrmForRechnung()
        {
            label1.Text = "RECHNUNG";
            gbDocAdressat.Text = "Rechnungsempfänger";
            gbRGAnzeige.Text = "Anzeige Rechnungsbetrag";
            cbDocArt.Text = "Gutschrift erstellen";
            gbDocText.Text = "Rechnungstext";
            lNetto.Text = "Rechnung Netto €";
            lBrutto.Text = "Rechnung Brutto €";
            base.Text = "manuelle Rechnungserstellung";
            isGS = false;
        }
        //
        //----------- laden der ComboBoxen Ressourcen -----------------
        //
        private void LoadComboResource()
        {
            //ComboZM 
            comboZM.DataSource = clsFahrzeuge.GetFahrzeuge_ZMforCombo(this.GL_User);
            comboZM.DisplayMember = "KFZ";
            comboZM.ValueMember = "ID";
            //comboZM.SelectedIndex = -1;
            //comboZM.SelectionStart = 1;
            SetStartIndexCombo(ref comboZM);
            ResourceZMLoad = true;


            //ComboAuflieger
            comboAuflieger.DataSource = clsFahrzeuge.GetFahrzeuge_AufliegerforCombo(this.GL_User);
            comboAuflieger.DisplayMember = "KFZ";
            comboAuflieger.ValueMember = "ID";
            //comboAuflieger.SelectedIndex= -1;
            //comboAuflieger.SelectionStart = 1;
            SetStartIndexCombo(ref comboAuflieger);
            getResource = true;
            ResourceAufliegerLoad = true;
        }
        //
        private void SetStartIndexCombo(ref ComboBox combo)
        {
            combo.SelectedIndex = -1;
            combo.SelectionStart = 1;
        }
        //
        //------------ ADR Suche für Ressource SU ----------------
        //
        private void btnSU_Click(object sender, EventArgs e)
        {
            AdressatSuche = false;
            OpenADRPanel();
        }
        //
        //-------- Neue Zeile im DGV ---------------------
        //
        private void tsbNeu_Click(object sender, EventArgs e)
        {
            this.dgv.DataSource = null;
            this.dgv.AllowUserToAddRows = true;
            this.dgv.Rows.Add();
            this.dgv.AllowUserToAddRows = false;
            BerechneSummenwerte();
        }
        //
        //------------- löschen Zeile aus DGV ------------------
        //
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dgv.Rows.Remove(this.dgv.Rows[this.dgv.CurrentRow.Index]);
                BerechneSummenwerte();
            }
        }
        //
        //----------- CellFormating - Button wird hinzugefügt (image) ---------------
        //
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //DeleteButton
            if (e.ColumnIndex == 0)
            {
                e.Value = Sped4.Properties.Resources.delete_16;
            }
        }
        //
        //-------------- Eingabe Check DGV --------------------
        //
        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 2)
                {
                    decimal decTest = 0.0m;
                    bool isNumeric;
                    decimal decOut;
                    string strCell = this.dgv.CurrentRow.Cells[e.ColumnIndex].Value.ToString();
                    isNumeric = decimal.TryParse(strCell, out decOut);

                    if (!isNumeric)
                    {
                        MessageBox.Show("Der Betrag darf keien Buchstaben oder Sonderzeichen enthalten!");
                        decTest = Convert.ToDecimal(decTest);
                    }
                    else
                    {
                        decTest = Convert.ToDecimal(strCell);
                    }
                    this.dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = decTest;
                    //Update Summe
                    BerechneSummenwerte();
                }
            }
        }
        //
        //------------------ Berechnung der Summe -----------------------
        //
        private decimal GetSummeDGV()
        {
            decimal Summe = 0.0m;
            if (this.dgv.Rows.Count > -1)
            {
                for (Int32 i = 0; i <= dgv.Rows.Count - 1; i++)
                {
                    if (this.dgv.Rows[i].Cells["Betrag"].Value != null)
                    {
                        Summe = Summe + (decimal)this.dgv.Rows[i].Cells["Betrag"].Value;
                    }
                }
            }
            return Summe;
        }
        //
        //----------- Berechnung aller Werte (Netto, Brutto usw.) ----------
        //
        private void BerechneSummenwerte()
        {
            decimal netto = GetSummeDGV();
            decimal mwstBetrag = 0.0m;
            if (nudRGMwSt.Value > 0)
            {
                mwstBetrag = netto * (nudRGMwSt.Value / 100);
            }
            decimal brutto = netto + mwstBetrag;
            tbRGNetto.Text = Functions.FormatDecimal(netto);
            tbRGMwStBetrag.Text = Functions.FormatDecimal(mwstBetrag);
            tbRGBrutto.Text = Functions.FormatDecimal(brutto);
        }
        //
        //---------------- Setzen MwSt Satz -------------------------
        //
        private void nudRGMwSt_ValueChanged(object sender, EventArgs e)
        {
            BerechneSummenwerte();
        }
        //
        //------------- Checkbox Ressourcen zuweisen ein / aus ----------
        //
        private void cbResourcen_CheckedChanged(object sender, EventArgs e)
        {
            if (cbResourcen.Checked == true)
            {
                gbResourcen.Enabled = true;
                getResource = true;
                SetStartIndexCombo(ref comboZM);
                SetStartIndexCombo(ref comboAuflieger);
            }
            else
            {
                gbResourcen.Enabled = false;
                getResource = false;
                SetStartIndexCombo(ref comboZM);
                SetStartIndexCombo(ref comboAuflieger);
                SetSUBack();
            }
        }
        //
        //----------- Auftrag zuweisen ----------------
        //
        private void cbAuftrag_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAuftrag.Checked == true)
            {
                gbAuftrag.Enabled = true;
                tbAuftrag.Text = auftrag.ToString();
                tbAuftragsPos.Text = auftragPos.ToString();
                tbAuftrag.Focus();
            }
            else
            {
                gbAuftrag.Enabled = false;
                SetAuftragsnummerToZero();
            }
        }
        private void SetAuftragsnummerToZero()
        {
            auftrag = 0;
            auftragPos = 0;
            tbAuftrag.Text = auftrag.ToString();
            tbAuftragsPos.Text = auftragPos.ToString();
        }
        //
        //---------- Check Eingabe Auftragsnummer ------------------
        //
        private void tbAuftrag_Validated(object sender, EventArgs e)
        {
            if ((Functions.CheckForInt(tbAuftrag.Text)) & (Functions.CheckNum(tbAuftrag.Text)))
            {

            }
            else
            {
                clsMessages.Allgemein_EingabeIstKeineGanzzahl();
                SetAuftragsnummerToZero();
                tbAuftrag.Focus();
            }
        }
        //
        //---------- Check tbAuftragPos nach Eingabe ----------------
        //
        private void tbAuftragsPos_Validated(object sender, EventArgs e)
        {
            if ((Functions.CheckForInt(tbAuftragsPos.Text)) & (Functions.CheckNum(tbAuftragsPos.Text)))
            {

            }
            else
            {
                clsMessages.Allgemein_EingabeIstKeineGanzzahl();
                SetAuftragsnummerToZero();
                tbAuftrag.Focus();
            }
        }
        //
        //------------ Check auf Eingabe in GB ---------------
        //
        private void gbAuftrag_Leave(object sender, EventArgs e)
        {
            if ((Functions.CheckForInt(tbAuftrag.Text)) & (Functions.CheckNum(tbAuftrag.Text)))
            {
                //check Auftrag exist
                decimal iAuftrag = Convert.ToDecimal(tbAuftrag.Text);
                if (iAuftrag == 0)
                {
                    cbAuftrag.Checked = false;
                }
                else
                {

                }
            }
        }

        /*********************************************************************************
         *                      RG / GS erstellen und drucken 
         * 
         * *******************************************************************************/
        //
        //
        //
        private void tsbtnSavePrint_Click(object sender, EventArgs e)
        {
            if (tbAuftraggeber.Text != "")
            {
                //Check Angaben / Betrag vorhanden
                decimal j = Convert.ToDecimal(tbRGNetto.Text);
                if ((Convert.ToDecimal(tbRGNetto.Text) > 0) & (KundeID > 0))
                {
                    SetDGVtoDataTable();
                }
                //Eintrag in DB Frachten
                WriteToDB();
                //Form close
                this.Close();
            }
            else
            {
                clsMessages.Fakturierung_AuftraggeberFehlt();
            }
        }
        //
        private void SetDGVtoDataTable()
        {
            if (dgv.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dgv.Rows.Count - 1; i++)
                {
                    clsFakturierung fakt = new clsFakturierung();
                    fakt.Fracht = (decimal)dgv.Rows[i].Cells["Betrag"].Value;
                    fakt.KundeID = KundeID;
                    fakt.FrachtText = (string)dgv.Rows[i].Cells["Text"].Value;
                    fakt.MwStSatz = nudRGMwSt.Value;
                    fakt.zwZM = zwZM;
                    fakt.zwAufliefer = zwAuflieger;
                    fakt.zwSU = zwSU;
                    SetRowsForTable(ref fakt);
                }
            }
        }
        //
        private void SetRowsForTable(ref clsFakturierung fakt)
        {
            DataRow row1;
            row1 = dt.NewRow();
            row1["Frachten_ID"] = 0;
            row1["AP_ID"] = AP_ID;
            row1["Fracht"] = fakt.Fracht;
            row1["Fracht_ADR"] = fakt.KundeID;
            row1["Frachttext"] = fakt.FrachtText;
            row1["MwStSatz"] = fakt.MwStSatz;
            row1["zw_ZM"] = fakt.zwZM;
            row1["zw_Auflieger"] = fakt.zwAufliefer;
            row1["zw_SU"] = fakt.zwSU;
            row1["RG_Nr"] = 0;
            row1["GS"] = isGS;
            dt.Rows.Add(row1);
        }
        //
        //------------------ ComboBox Changed --------------------
        //
        private void comboZM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ResourceZMLoad)
            {
                if (comboZM.SelectedIndex > -1)
                {
                    zwZM = (decimal)comboZM.SelectedValue;
                }
            }
        }
        //
        private void comboAuflieger_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ResourceAufliegerLoad)
            {
                if (comboAuflieger.SelectedIndex > -1)
                {
                    zwAuflieger = (decimal)comboAuflieger.SelectedValue;
                }
            }
        }
        //
        //------ Eintrag in DB Frachten ---------
        //
        private void WriteToDB()
        {
            //Eintrag in DB Frachten
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            clsFakturierung fakt = new clsFakturierung();
            ds = fakt.AddToDB(ds);

            //DataSet erstellen für frmReportViewer "siehe Beschreibung clsRG_DataSet
            DataTable dtDaten = new DataTable();
            Int32 iAnzahl = ds.Tables["RGGSDaten"].Rows.Count;


            dtDaten = fakt.GetManRGGSDaten((decimal)ds.Tables["RGGSDaten"].Rows[0]["Fracht_ADR"]);


            ds.Tables.Add(dtDaten);

            //REchnungs-/Gutschriftsnummer
            ds = Functions.GetAndSetRGGSNr(ds, false);

            PrintRGGSDirect(ds);
        }
        //
        //
        private void PrintRGGSDirect(DataSet ds)
        {
            Int32 j = (Int32)nudDruckanzahl.Value;

            for (Int32 i = 1; i <= j; i++)
            {
                //RG / GS Drucken
                //Auftrag und AuftragPos liegen nicht vor , da manuelle RG / GS Eingabe

                /***
                frmReportViewer reportview = new frmReportViewer(ds, Globals.enumDokumentenart.manuelleRGGS.ToString());
                reportview.GL_User = GL_User;
                reportview._ArtikelTableID = this._ArtikelTableID;
                reportview._AuftragID = this._AuftragID;
                reportview._AuftragPosTableID = _AuftragPosTableID;
                reportview._MandantenID = _MandantenID;
                reportview.GL_User = GL_User;
                reportview.Hide();
                reportview.PrintDirect();
                reportview.Close();
                 * ***/
            }
        }
    }
}
