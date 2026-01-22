using LVS;
using LVS.Dokumente;
using Sped4.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmFakturierung : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public decimal AuftragID;
        public decimal AuftragPos;
        DataSet ds;
        clsFakturierung fakt;
        bool FrmIsLoad = false;
        public Int32 SearchButton = 0;
        public bool GSEingabeFrachtVorlage = false;
        public string Abrechnungsart = string.Empty;

        internal bool SavenPrint = false;

        //DataTAble
        internal DataTable dtSumme = new DataTable("Summe");

        public delegate void ctrFakturierungRefreshEventHandler();
        public event ctrFakturierungRefreshEventHandler ctrFakturierungRefersh;

        public delegate void ctrFakturierungCreateRGEventHandler(decimal AuftragPosition_ID, decimal AuftragID, decimal AuftragPos, bool Einzelposition, bool RG);
        public event ctrFakturierungCreateRGEventHandler ctrFakturierungCreateRG;


        public frmFakturierung()
        {
            InitializeComponent();
        }
        //
        //----------- LOAD FRM ------------------------
        //
        private void frmFakturierung_Load(object sender, EventArgs e)
        {
            FrmIsLoad = true;
            fakt = new clsFakturierung();
            fakt.AuftragID = AuftragID;
            fakt.AuftragPos = AuftragPos;

            //Auftrag
            tbAuftrag.Text = AuftragID.ToString() + " / " + AuftragPos.ToString();

            GetDatenForFakurierung();
            SetDatenToForm();

            //Spalten für die Summen hinzufügen Table"Auftragsdaten"
            SetColSummeToTableAuftragsDaten();

            //Statusübersicht
            dgvAListe.DataSource = ds.Tables["Statusliste"];
            dgvAListe.Columns["Status"].Visible = false;
            dgvAListe.Columns["AuftragID"].Visible = false;
            dgvAListe.Columns["AuftragPos"].Visible = false;
            dgvAListe.Columns["PosGemGewicht"].Visible = false;
            dgvAListe.Columns["PosBrutto"].Visible = false;
            dgvAListe.Columns["ID"].Visible = false;
            dgvAListe.Columns["GesamtGemGewicht"].Visible = false;
            dgvAListe.Columns["GesamtBrutto"].Visible = false;


            //ArtikelGrid
            dgvArtikel.DataSource = ds.Tables["Artikel"];
            dgvArtikel.Columns["Gut"].Visible = false;
            dgvArtikel.Columns["Dicke"].Visible = false;
            dgvArtikel.Columns["Breite"].Visible = false;
            dgvArtikel.Columns["Länge"].Visible = false;
            dgvArtikel.Columns["Höhe"].Visible = false;
            dgvArtikel.Columns["ME"].Visible = false;
            //dgvArtikel.Columns["ArtikelGewicht"].Visible = false;
            dgvArtikel.Columns["gemGewicht"].Visible = false;
            dgvArtikel.Columns["Brutto"].Visible = false;
            dgvArtikel.Columns["ID"].Visible = false;
            dgvArtikel.Columns["Werksnummer"].Visible = false;
            dgvArtikel.Columns["Netto"].Visible = false;

            dgvArtikel.Columns["ME"].DisplayIndex = 1;
            dgvArtikel.Columns["Gut"].DisplayIndex = 2;
            dgvArtikel.Columns["Abmessung"].DisplayIndex = 3;
            dgvArtikel.Columns["gemGewicht"].DisplayIndex = 4;
            dgvArtikel.Columns["Netto"].DisplayIndex = 5;
            dgvArtikel.Columns["Brutto"].DisplayIndex = 6;
            dgvArtikel.Columns["Betrag"].DisplayIndex = 7;
            dgvArtikel.Columns["GSSU"].DisplayIndex = 8;



            //dgvSumme
            InitDGVSumme();


            //RG / GS Betragsübersicht
            decimal mwstBetrag = 0.00m;
            decimal brutto = 0.00m;
            decimal netto = 0.00m; ;
            tbRGMwStBetrag.Text = mwstBetrag.ToString();
            tbRGNetto.Text = netto.ToString();
            tbRGBrutto.Text = brutto.ToString();
            tbGSMwStBetrag.Text = mwstBetrag.ToString();
            tbGSNetto.Text = netto.ToString();
            tbGSBrutto.Text = brutto.ToString();

            CheckAndSetFVGSDaten();

            //MwStSatz Auftraggeber und SU
            nudRGMwSt.Value = SetMwStSatz((decimal)ds.Tables["Auftragsdaten"].Rows[0]["A_ID"]);

            //Frachtbetrag falls bei Auftragserfassung bereits eingetragen
            decimal vFracht = clsAuftrag.GetVFrachtByAuftragID(AuftragID);
            InsterFrachtToTableSumme(vFracht);
            tbRGNetto.Text = Functions.FormatDecimal(vFracht);
        }
        //
        //
        //
        private void InitDGVSumme()
        {
            SetColForTableSumme();
            SetRowsForTableSumme();
            dgvSumme.DataSource = dtSumme;
            dgvSumme.Columns["Beschreibung"].Visible = false;
            dgvSumme.Columns["Summe €"].Visible = false;
            dgvSumme.Columns["ID"].Visible = false;
            //Zeilen deaktivieren
            dgvSumme.Rows[0].ReadOnly = true;
            dgvSumme.Rows[1].ReadOnly = false;
            dgvSumme.Rows[2].ReadOnly = true;
        }
        //
        //-------- MwST - Kunde ---------------
        //
        private decimal SetMwStSatz(decimal ADR_ID)
        {
            clsKunde kd = new clsKunde();
            kd.BenutzerID = GL_User.User_ID;
            kd.ADR_ID = ADR_ID;
            return kd.GetMwStSatz();
        }
        //
        //------------------ Daten die in DB geschrieben werden -------------
        //
        private void SetColSummeToTableAuftragsDaten()
        {
            if (ds.Tables["Auftragsdaten"].Columns["SummeArtikel"] == null)
            {
                ds.Tables["Auftragsdaten"].Columns.Add("SummeArtikel", typeof(decimal));
            }
            if (ds.Tables["Auftragsdaten"].Columns["TextZusatzKosten"] == null)
            {
                ds.Tables["Auftragsdaten"].Columns.Add("TextZusatzKosten", typeof(string));
            }
            if (ds.Tables["Auftragsdaten"].Columns["ZusatzKosten"] == null)
            {
                ds.Tables["Auftragsdaten"].Columns.Add("ZusatzKosten", typeof(decimal));
            }
            if (ds.Tables["Auftragsdaten"].Columns["AP_Summe"] == null)
            {
                ds.Tables["Auftragsdaten"].Columns.Add("AP_Summe", typeof(decimal));
            }
            if (ds.Tables["Auftragsdaten"].Columns["MargeEuro"] == null)
            {
                ds.Tables["Auftragsdaten"].Columns.Add("MargeEuro", typeof(decimal));
            }
            if (ds.Tables["Auftragsdaten"].Columns["MargeProzent"] == null)
            {
                ds.Tables["Auftragsdaten"].Columns.Add("MargeProzent", typeof(decimal));
            }
            if (ds.Tables["Auftragsdaten"].Columns["GS"] == null)
            {
                ds.Tables["Auftragsdaten"].Columns.Add("GS", typeof(string));
            }
            if (ds.Tables["Auftragsdaten"].Columns["GS_Date"] == null)
            {
                ds.Tables["Auftragsdaten"].Columns.Add("GS_Date", typeof(DateTime));
            }
            if (ds.Tables["Auftragsdaten"].Columns["MwStSatz"] == null)
            {
                ds.Tables["Auftragsdaten"].Columns.Add("MwStSatz", typeof(decimal));
            }
            if (ds.Tables["Auftragsdaten"].Columns["FV_B_ID"] == null)
            {
                ds.Tables["Auftragsdaten"].Columns.Add("FV_B_ID", typeof(decimal));
            }
            if (ds.Tables["Auftragsdaten"].Columns["FV_E_ID"] == null)
            {
                ds.Tables["Auftragsdaten"].Columns.Add("FV_E_ID", typeof(decimal));
            }
        }
        //
        //
        //
        private void GetDatenForFakurierung()
        {
            clsFrachtvergabe vergabe = new clsFrachtvergabe();
            vergabe.AuftragID = AuftragID;
            vergabe.AuftragPos = AuftragPos;
            //vergabe.ID_AP=clsAuftragPos.GetIDbyAuftragAndAuftragPos(AuftragID, AuftragPos);

            if (clsFrachtvergabe.IsIDIn(vergabe.ID_AP)) //Auftrag an SU vergeben
            {
                SetFormForSU();
                ds = fakt.GetAuftragDatenForFakturierung(false);

                //MwSt-Satz für SU
                nudGSMwSt.Value = SetMwStSatz((decimal)ds.Tables["Auftragsdaten"].Rows[0]["SU_ID"]);
            }
            else  //Transport im Selbsteintritt
            {
                //Elemente für GS an SU werden augeblendet
                SetFormForSelbsteintritt();
                ds = fakt.GetAuftragDatenForFakturierung(true);
            }
        }
        //
        //
        //
        private void SetDatenToForm()
        {
            if (ds != null)
            {
                //Resourcen
                string recource = ds.Tables["Auftragsdaten"].Rows[0]["ZM"].ToString().Trim() + " - " +
                                  ds.Tables["Auftragsdaten"].Rows[0]["Auflieger"].ToString().Trim() + " - " +
                                  ds.Tables["Auftragsdaten"].Rows[0]["Fahrer"].ToString().Trim();
                tbRecourcen.Text = recource;

                //ADR
                string auftraggeber = ds.Tables["Auftragsdaten"].Rows[0]["Auftraggeber"].ToString().Trim() + ", " +
                                      ds.Tables["Auftragsdaten"].Rows[0]["A_Strasse"].ToString().Trim() + ", " +
                                      ds.Tables["Auftragsdaten"].Rows[0]["A_PLZ"].ToString().Trim() + ", " +
                                      ds.Tables["Auftragsdaten"].Rows[0]["A_Ort"].ToString().Trim();
                tbAuftraggeber.Text = auftraggeber;


                //Versender
                string versender = ds.Tables["Auftragsdaten"].Rows[0]["Versender"].ToString().Trim() + ", " +
                                      ds.Tables["Auftragsdaten"].Rows[0]["V_Strasse"].ToString().Trim() + ", " +
                                      ds.Tables["Auftragsdaten"].Rows[0]["V_PLZ"].ToString().Trim() + ", " +
                                      ds.Tables["Auftragsdaten"].Rows[0]["V_Ort"].ToString().Trim();
                tbVersender.Text = versender;
                //ADR setzen FVGS
                SetADR_FVGS();

                //Empfänger
                string empfaenger = ds.Tables["Auftragsdaten"].Rows[0]["Empfänger"].ToString().Trim() + ", " +
                                      ds.Tables["Auftragsdaten"].Rows[0]["E_Strasse"].ToString().Trim() + ", " +
                                      ds.Tables["Auftragsdaten"].Rows[0]["E_PLZ"].ToString().Trim() + ", " +
                                      ds.Tables["Auftragsdaten"].Rows[0]["E_Ort"].ToString().Trim();
                tbEmpfaeger.Text = empfaenger;


                //ADR setzen FVGS
                SetADR_FVGS();

                //Be- / Entladedatum
                if (ds.Tables["Auftragsdaten"].Rows[0]["B_Datum"] != DBNull.Value)
                {
                    DateTime belDate = Convert.ToDateTime(ds.Tables["Auftragsdaten"].Rows[0]["B_Datum"]);
                    tbB_Datum.Text = belDate.ToShortDateString();
                }
                else
                {
                    tbB_Datum.Text = string.Empty;
                }

                if (ds.Tables["Auftragsdaten"].Rows[0]["E_Datum"] != DBNull.Value)
                {
                    DateTime entDate = (DateTime)ds.Tables["Auftragsdaten"].Rows[0]["E_Datum"];
                    tbE_Datum.Text = entDate.ToShortDateString();
                }
                else
                {
                    tbB_Datum.Text = string.Empty;
                }

                if (btnSU.Enabled == true)
                {
                    string Subunternehmer = ds.Tables["Auftragsdaten"].Rows[0]["SU"].ToString() + ", " +
                              ds.Tables["Auftragsdaten"].Rows[0]["SU_Strasse"].ToString() + ", " +
                              ds.Tables["Auftragsdaten"].Rows[0]["SU_PLZ"].ToString() + ", " +
                              ds.Tables["Auftragsdaten"].Rows[0]["SU_Ort"].ToString();
                    tbSU.Text = Subunternehmer;
                }

                if (gbFVGS.Enabled == true)
                {
                    if (ds.Tables["Auftragsdaten"].Rows[0]["FV_B_ID"] != null)
                    {
                        decimal adr = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["FV_B_ID"];
                        SetADRString(tbFVGSVersender, adr);
                    }
                    else
                    {
                        decimal adr = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["V_ID"];
                        SetADRString(tbFVGSVersender, adr);
                    }

                    if (ds.Tables["Auftragsdaten"].Rows[0]["FV_E_ID"] != null)
                    {
                        decimal adr = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["FV_E_ID"];
                        SetADRString(tbFVGSEmpfaenger, adr);
                    }
                    else
                    {
                        decimal adr = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["E_ID"];
                        SetADRString(tbFVGSEmpfaenger, adr);
                    }
                    gbFVGS.Refresh();
                    this.Refresh();
                }
            }
        }
        //
        //
        //
        private void SetADR_FVGS()
        {
            if (gbFVGS.Enabled == true)
            {
                if (ds.Tables["Auftragsdaten"].Columns["FV_B_ID"] == null)
                {
                    ds.Tables["Auftragsdaten"].Columns.Add("FV_B_ID", typeof(decimal));
                }
                ds.Tables["Auftragsdaten"].Rows[0]["FV_B_ID"] = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["V_ID"];

                if (ds.Tables["Auftragsdaten"].Columns["FV_E_ID"] == null)
                {
                    ds.Tables["Auftragsdaten"].Columns.Add("FV_E_ID", typeof(decimal));
                }
                ds.Tables["Auftragsdaten"].Rows[0]["FV_E_ID"] = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["E_ID"];
            }
        }
        //
        //-----------------------------------------------
        //
        private void SetADRString(TextBox tb, decimal ADR_ID)
        {
            string adresse = clsADR.GetADRString(ADR_ID);
            tb.Text = adresse;
        }
        //
        //--------- SU vorhanden  ----------------
        //
        private void SetFormForSelbsteintritt()
        {
            btnSU.Enabled = false;
            tbSU.Enabled = false;
            btnGSSU.Enabled = false;
        }
        private void SetFormForSU()
        {
            tbRecourcen.Enabled = false;
        }
        //
        //
        //
        private void dgvAListe_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            clsAuftragsstatus stat = new clsAuftragsstatus();

            //Status
            if ((!object.ReferenceEquals(dgvAListe.Rows[e.RowIndex].Cells["Status"].Value, DBNull.Value)))
            {
                if (dgvAListe.Rows[e.RowIndex].Cells["Status"].Value != null)
                {
                    stat.Status = (Int32)dgvAListe.Rows[e.RowIndex].Cells["Status"].Value;
                }
            }
            //AuftragID
            if ((!object.ReferenceEquals(dgvAListe.Rows[e.RowIndex].Cells["AuftragID"].Value, DBNull.Value)))
            {
                if (dgvAListe.Rows[e.RowIndex].Cells["AuftragID"].Value != null)
                {
                    stat.Auftrag_ID = (decimal)dgvAListe.Rows[e.RowIndex].Cells["AuftragID"].Value;
                }
            }
            //AuftragPos
            if ((!object.ReferenceEquals(dgvAListe.Rows[e.RowIndex].Cells["AuftragPos"].Value, DBNull.Value)))
            {
                if (dgvAListe.Rows[e.RowIndex].Cells["AuftragPos"].Value != null)
                {
                    stat.AuftragPos = (decimal)dgvAListe.Rows[e.RowIndex].Cells["AuftragPos"].Value;
                }
            }
            /************************************************************************************
             * Übersicht Status und entsprechende Images
             * - 1 unvollständig  - delete_16.png   (rotes Kreuz)
             * - 2 vollständig    - add.png         (grünes Kreuz)
             * - 3 storniert      - form_green_delete.png (grünes Form mit rotem Kreuz)
             * - 4 disponiert     - disponiert.png  (rotes Fähnchen)
             * - 5 durchgeführt   - done.png        (blaues Fähnchen)
             * - 6 Freigabe Berechnung - Freigabe_Berechnung.png  (grünes Fähnchen)
             * - 7 berechnet      - check       (gründer Haken) 
             * 
             * ***********************************************************************************/
            //Column 1  
            if (e.ColumnIndex == 0)
            {
                //Functions.GetDataGridCellStatusImage(ref e, stat.Status);
                e.Value = Functions.GetDataGridCellStatusImage(stat.Status);
                /****
                 switch(stat.Status)
                {

                 /****
                     //----------- Stati -------------------
                     case 1:
                         e.Value=Sped4.Properties.Resources.delete;
                         break;
                     case 2:
                         e.Value=Sped4.Properties.Resources.add;
                         break;
                     case 3:
                         e.Value=Sped4.Properties.Resources.form_green_delete;
                         break;
                     case 4:
                         e.Value=Sped4.Properties.Resources.disponiert;
                         break;
                     case 5:
                         e.Value=Sped4.Properties.Resources.done;
                         break;
                     case 6:
                         e.Value = Sped4.Properties.Resources.Freigabe_Berechnung;
                         break;
                     case 7:
                         e.Value=Sped4.Properties.Resources.check;
                         break;
                 }
                 * ***/
            }
            if (e.ColumnIndex == 1)
            {
                e.Value = stat.Auftrag_ID.ToString() + " / " + stat.AuftragPos.ToString();
            }
        }
        /***********************************************************************************************
         *                                DGV Artikel
         * 
         * ********************************************************************************************/
        //
        private void dgvArtikel_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (FrmIsLoad)
            {
                clsFakturierung fak = new clsFakturierung();
                if (e.RowIndex <= ds.Tables["Artikel"].Rows.Count - 1)
                {
                    fak.ME = Convert.ToInt32(ds.Tables["Artikel"].Rows[e.RowIndex]["ME"].ToString());
                    fak.Gut = (string)ds.Tables["Artikel"].Rows[e.RowIndex]["Gut"];
                    fak.Dicke = (decimal)ds.Tables["Artikel"].Rows[e.RowIndex]["Dicke"];
                    fak.Breite = (decimal)ds.Tables["Artikel"].Rows[e.RowIndex]["Breite"];
                    fak.Laenge = (decimal)ds.Tables["Artikel"].Rows[e.RowIndex]["Länge"];
                    fak.Hoehe = (decimal)ds.Tables["Artikel"].Rows[e.RowIndex]["Höhe"];
                    //fak.ArtikelGewicht = (decimal)ds.Tables["Artikel"].Rows[e.RowIndex]["ArtikelGewicht"];
                    fak.gemGewicht = (decimal)ds.Tables["Artikel"].Rows[e.RowIndex]["gemGewicht"];
                    fak.Brutto = (decimal)ds.Tables["Artikel"].Rows[e.RowIndex]["Brutto"];
                    fak.Artikel_ID = (decimal)ds.Tables["Artikel"].Rows[e.RowIndex]["ID"];
                    fak.Netto = (decimal)ds.Tables["Artikel"].Rows[e.RowIndex]["Netto"];
                }
                if (ds.Tables["Fracht"] != null)
                {
                    for (Int32 i = 0; i <= ds.Tables["Fracht"].Rows.Count - 1; i++)
                    {
                        //if (e.RowIndex <= ds.Tables["Fracht"].Rows.Count - 1)
                        //{
                        if (fak.Artikel_ID == (decimal)ds.Tables["Fracht"].Rows[i]["Artikel_ID"])
                        {
                            fak.ArtikelFracht = (decimal)ds.Tables["Fracht"].Rows[i]["Fracht"];
                        }
                        //}
                    }
                }
                else
                {
                    fak.ArtikelFracht = 0.00m;
                }
                /**********************************************************************************************/
                //        if (e.ColumnIndex < 10)
                //        {
                //Button
                if (e.ColumnIndex == 0)
                {
                    if (GSEingabeFrachtVorlage)
                    {
                        e.CellStyle.BackColor = Color.Gray;
                    }
                }
                //ME
                if (dgvArtikel.Columns[e.ColumnIndex].Name == "colME")
                //if (e.ColumnIndex == 1)
                {
                    e.Value = fak.ME;
                }
                //Gut
                if (dgvArtikel.Columns[e.ColumnIndex].Name == "colGut")
                //if (e.ColumnIndex == 2)
                {
                    e.Value = fak.Gut;
                }
                //Abmessungen
                if (dgvArtikel.Columns[e.ColumnIndex].Name == "colAbmessung")
                //if (e.ColumnIndex == 3)
                {
                    e.Value = fak.Abmessungen;
                }

                //gem Gewicht
                if (dgvArtikel.Columns[e.ColumnIndex].Name == "colgemGewicht")
                //if (e.ColumnIndex == 4)
                {
                    e.Value = fak.gemGewicht;
                }
                //Netto
                if (dgvArtikel.Columns[e.ColumnIndex].Name == "colNetto")
                //if (e.ColumnIndex == 5)
                {
                    e.Value = fak.Netto;
                }
                //Brutto
                if (dgvArtikel.Columns[e.ColumnIndex].Name == "colBrutto")
                //if (e.ColumnIndex == 5)
                {
                    e.Value = fak.Brutto;
                }
                //Fracht Artikel
                if (dgvArtikel.Columns[e.ColumnIndex].Name == "colBetrag")
                //if (e.ColumnIndex == 6)
                {
                    e.Value = fak.ArtikelFracht;
                }
                //GS SU
                if (dgvArtikel.Columns[e.ColumnIndex].Name == "colGSSU")
                //if (e.ColumnIndex == 7)
                {
                    e.Value = fak.GS;
                }
                //        }
            }
        }
        //
        //---------------- Fracht einzelne Artikel berechnen ------------------
        //
        private void dgvArtikel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!GSEingabeFrachtVorlage)
            {
                if (e.ColumnIndex == 0)
                {
                    Abrechnungsart = enumAbrechnungsart.ArtikelPauschal.ToString();
                    OpenFrmFrachtBerechnung((decimal)this.dgvArtikel.CurrentRow.Cells["ID"].Value, Abrechnungsart, true);
                }
            }
        }
        //
        //------------------  Übernahme der DataSet mit Daten aus frmFakturierung ---------
        //
        public void GetDataTableFrachtToDataSet(DataSet dsFracht)
        {
            this.ds = dsFracht;
            SetFrachtDatenToTable();
            dgvSumme.Refresh();
            dgvArtikel.Refresh();
            this.Refresh();
        }
        //
        //
        //
        private void InsterFrachtToTableSumme(decimal dtSummeNetto)
        {
            Int32 iRows = dtSumme.Rows.Count;
            if (dtSumme.Rows.Count == 0)
            {
                dtSumme.Columns.Clear();
                dtSumme.Rows.Clear();
                InitDGVSumme();
            }
            iRows = dtSumme.Rows.Count;
            dtSumme.Rows[0]["Summe €"] = dtSummeNetto;
            dtSumme.Rows[2]["Summe €"] = (decimal)dtSumme.Rows[0]["Summe €"] + (decimal)dtSumme.Rows[1]["Summe €"];
        }
        //
        private void SetFrachtDatenToTable()
        {
            if (ds.Tables["Fracht"] != null)
            {
                //Summe ArtikelFracht
                object obj = ds.Tables["Fracht"].Compute("SUM(Fracht)", "AP_ID>0");
                InsterFrachtToTableSumme(Convert.ToDecimal(obj.ToString()));
                //dtSumme.Rows[0]["Summe €"] = Convert.ToDecimal(obj.ToString());
                //dtSumme.Rows[2]["Summe €"] = (decimal)dtSumme.Rows[0]["Summe €"] + (decimal)dtSumme.Rows[1]["Summe €"];
            }
            else
            {
                decimal decDefault = 0.0m;
                InsterFrachtToTableSumme(decDefault);
                //dtSumme.Rows[0]["Summe €"] =0.00m;
            }

            ds.Tables["Auftragsdaten"].Rows[0]["SummeArtikel"] = (decimal)dtSumme.Rows[0]["Summe €"];
            ds.Tables["Auftragsdaten"].Rows[0]["TextZusatzkosten"] = (string)dtSumme.Rows[1]["Beschreibung"];
            ds.Tables["Auftragsdaten"].Rows[0]["Zusatzkosten"] = (decimal)dtSumme.Rows[1]["Summe €"];
            ds.Tables["Auftragsdaten"].Rows[0]["AP_Summe"] = (decimal)dtSumme.Rows[2]["Summe €"];

            //Update Übersicht Rechnungsbetrag
            decimal netto = (decimal)dtSumme.Rows[2]["Summe €"];
            tbRGNetto.Text = netto.ToString();
            tbGSNetto.Text = netto.ToString();
            if (GSEingabeFrachtVorlage)
            {
                tbFVGSBetrag.Text = netto.ToString();
                //ADR gbFVGS
                if (ds.Tables["Auftragsdaten"].Rows[0]["FV_B_ID"] != DBNull.Value)
                {
                    SetADRString(tbFVGSVersender, (decimal)ds.Tables["Auftragsdaten"].Rows[0]["FV_B_ID"]);
                }
                if (ds.Tables["Auftragsdaten"].Rows[0]["FV_E_ID"] != DBNull.Value)
                {
                    SetADRString(tbFVGSEmpfaenger, (decimal)ds.Tables["Auftragsdaten"].Rows[0]["FV_E_ID"]);
                }
            }
            BerechnungRGBetrag();
            BerechnungGSBetrag();
        }

        /*************************************************************************************
         *                                  MENÜ
         *************************************************************************************/
        //
        //------------ schliessen ----------------------
        //
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        //
        /**************************************************************************************
         *                        dgv Summe
         **************************************************************************************/
        //
        //
        private void SetColForTableSumme()
        {
            dtSumme.Columns.Add("Beschreibung", typeof(string));
            dtSumme.Columns.Add("Summe €", typeof(decimal));
            dtSumme.Columns.Add("ID", typeof(decimal));
        }
        //
        private void SetRowsForTableSumme()
        {

            DataRow row1;
            row1 = dtSumme.NewRow();
            row1["Beschreibung"] = fakt.Beschreibung1;
            row1["Summe €"] = fakt.SummeArtikel;
            row1["ID"] = 1;
            dtSumme.Rows.Add(row1);

            DataRow row2;
            row2 = dtSumme.NewRow();
            row2["Beschreibung"] = fakt.Beschreibung2;
            row2["Summe €"] = fakt.ZusatzKosten;
            row2["ID"] = 2;
            dtSumme.Rows.Add(row2);

            DataRow row3;
            row3 = dtSumme.NewRow();
            row3["Beschreibung"] = fakt.Beschreibung3;
            row3["Summe €"] = fakt.SummeAP;
            row3["ID"] = 0;
            dtSumme.Rows.Add(row3);
        }
        //
        //----------------- DataGridView Summe -----------------------------------------
        //
        private void dgvSumme_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //clsFakturierung fakt = new clsFakturierung();
                fakt = new clsFakturierung();

                switch (e.RowIndex)
                {
                    case 0:
                        fakt.Beschreibung1 = (string)dtSumme.Rows[e.RowIndex]["Beschreibung"];
                        fakt.SummeArtikel = (decimal)dtSumme.Rows[e.RowIndex]["Summe €"];
                        break;
                    case 1:
                        fakt.Beschreibung2 = (string)dtSumme.Rows[e.RowIndex]["Beschreibung"];
                        fakt.ZusatzKosten = (decimal)dtSumme.Rows[e.RowIndex]["Summe €"];
                        break;
                    case 2:
                        fakt.Beschreibung3 = (string)dtSumme.Rows[e.RowIndex]["Beschreibung"];
                        fakt.SummeAP = (decimal)dtSumme.Rows[e.RowIndex]["Summe €"];
                        break;
                }


                //*******************++
                if (e.ColumnIndex == 0)
                {
                    switch (e.RowIndex)
                    {
                        case 0:
                            e.Value = fakt.Beschreibung1;
                            break;
                        case 1:
                            e.Value = fakt.Beschreibung2;
                            break;
                        case 2:
                            e.Value = fakt.Beschreibung3;
                            break;
                    }
                }

                if (e.ColumnIndex == 1)
                {
                    switch (e.RowIndex)
                    {
                        case 0:
                            e.Value = fakt.SummeArtikel;
                            break;
                        case 1:
                            e.Value = fakt.ZusatzKosten;
                            break;
                        case 2:
                            e.Value = fakt.SummeAP;
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //
        //------------------------ Übernahme Daten dgvSumme in Table ------------------
        //
        private void dgvSumme_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            bool OK = false;
            if (e.RowIndex == 1)
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        fakt.Beschreibung2 = this.dgvSumme.CurrentCell.Value.ToString();
                        dtSumme.Rows[1]["Beschreibung"] = fakt.Beschreibung2;
                        OK = true;
                        break;
                    case 1:
                        if (Functions.CheckNum(this.dgvSumme.CurrentCell.Value.ToString()))
                        {
                            fakt.ZusatzKosten = Convert.ToDecimal(this.dgvSumme.CurrentCell.Value.ToString());
                            dtSumme.Rows[e.RowIndex]["Summe €"] = this.dgvSumme.CurrentCell.Value;
                            OK = true;
                        }
                        break;
                }
                if (OK)
                {
                    fakt.SummeArtikel = (decimal)dtSumme.Rows[0]["Summe €"];
                    fakt.ZusatzKosten = (decimal)dtSumme.Rows[1]["Summe €"];
                    fakt.SummeAP = fakt.SummeArtikel + fakt.ZusatzKosten;
                    dtSumme.Rows[2]["Summe €"] = fakt.SummeAP;
                    SetFrachtDatenToTable();
                    dgvSumme.Refresh();
                }
            }
        }
        /****************************************************************************************************************************
         * 
         *                                                  Button zur Abrechnung
         * 
         * *************************************************************************************************************************/
        //
        //----------------- Eingangsgutschrift Frachtgutschrift ----------
        //
        private void btnEGS_Click(object sender, EventArgs e)
        {
            gbRGAnzeige.Enabled = true;
            gbGSAnzeige.Enabled = false;
            Abrechnungsart = enumAbrechnungsart.GutschriftAuftragPosPauschal.ToString();
            // ArtikelID =0 , da al 
            OpenFrmFrachtBerechnung(0, Abrechnungsart, true);
        }
        //
        //----------------- Komplett Pauschal ----------------------
        //
        private void BtnPauschalKomplett_Click(object sender, EventArgs e)
        {
            gbRGAnzeige.Enabled = true;
            gbGSAnzeige.Enabled = false;
            //AritkelID = 0 da Pauschal
            Abrechnungsart = enumAbrechnungsart.AuftragsPositionPauschal.ToString();
            OpenFrmFrachtBerechnung(0, Abrechnungsart, true);
        }
        //
        //------------------- GS and SU --------------------------------
        //
        private void btnGSSU_Click(object sender, EventArgs e)
        {
            gbGSAnzeige.Enabled = true;
            gbRGAnzeige.Enabled = false;
            Abrechnungsart = enumAbrechnungsart.GutschriftanSU.ToString();
            OpenFrmFrachtBerechnung(0, Abrechnungsart, false);
        }
        //
        //----------------- FrachtvorlageGutschrift----------------------
        //
        private void btnFVGS_Click(object sender, EventArgs e)
        {
            //Frm Änderung für Eingabe FVGS
            gbFVGS.Enabled = true;
            if (ds.Tables["FVGS"] == null)
            {
                DataTable dt = new DataTable("FVGS");
                ds.Tables.Add(dt);
            }
            //Abfrage, ob schon eine Gutschrift vorhanden ist 
            GSEingabeFrachtVorlage = true;
            CheckAndSetFVGSDaten();
            Abrechnungsart = enumAbrechnungsart.GutschriftFrachtvorlage.ToString();
            ChangeFrmForGSFrachtVorlage(GSEingabeFrachtVorlage);
            this.Refresh();
        }
        //
        //------- prüft, ob FVGS vorhanden und Set die Angaben -----------
        //
        private void CheckAndSetFVGSDaten()
        {
            if (clsFakturierung.IsAPisInFVGS((decimal)ds.Tables["Auftragsdaten"].Rows[0]["ID"]))
            {
                fakt.AP_ID = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["ID"];
                if (ds.Tables["FVGS_Daten"] != null)
                {
                    ds.Tables.Remove("FVGS_Daten");
                }
                ds.Tables.Add(fakt.GetFVGSDaten());

                //FVGS Versand - / Empfangsort
                if ((decimal)ds.Tables["FVGS_Daten"].Rows[0]["FV_E_ID"] == 0)
                {
                    ds.Tables["Auftragsdaten"].Rows[0]["FV_E_ID"] = ds.Tables["Auftragsdaten"].Rows[0]["E_ID"];
                    clsFakturierung.updateFVADR((decimal)ds.Tables["Auftragsdaten"].Rows[0]["ID"], "Empfänger", (decimal)ds.Tables["Auftragsdaten"].Rows[0]["FV_E_ID"]);
                }
                if ((decimal)ds.Tables["FVGS_Daten"].Rows[0]["FV_V_ID"] == 0)
                {
                    ds.Tables["Auftragsdaten"].Rows[0]["FV_B_ID"] = ds.Tables["Auftragsdaten"].Rows[0]["V_ID"];
                    clsFakturierung.updateFVADR((decimal)ds.Tables["Auftragsdaten"].Rows[0]["ID"], "Versender", (decimal)ds.Tables["Auftragsdaten"].Rows[0]["FV_B_ID"]);
                }

                if (ds.Tables["FVGS_Daten"] != null)
                {
                    tbFVGSBetrag.Text = Functions.FormatDecimal((decimal)ds.Tables["FVGS_Daten"].Rows[0]["Fracht"]);
                    tbFVGSVersender.Text = clsADR.GetADRString((decimal)ds.Tables["FVGS_Daten"].Rows[0]["FV_V_ID"]);
                    tbFVGSEmpfaenger.Text = clsADR.GetADRString((decimal)ds.Tables["FVGS_Daten"].Rows[0]["FV_E_ID"]);
                    gbFVGS.Enabled = true;
                    gbFVGS.Refresh();
                }
            }
            else
            {
                if (GSEingabeFrachtVorlage)
                {
                    tbFVGSVersender.Text = tbVersender.Text;
                    tbFVGSEmpfaenger.Text = tbEmpfaeger.Text;
                }
            }
        }
        //
        //
        //
        private void OpenFrmFrachtBerechnung(decimal ArtikelID, string Abrechnungsart, bool Rechnung)
        {
        }
        //
        //---------------  Aktualisierung Netto Betrag --------------
        //
        private void tbNetto_TextChanged(object sender, EventArgs e)
        {
            //aktualisierung 
            BerechnungRGBetrag();
            BerechnungGSBetrag();
            //aktualisieren RG Info
            SetgbRGInfo();
        }
        //
        //------ Berechnung/Aktuallisierung Rechnungsbetragsübersicht -----------
        //
        private void BerechnungRGBetrag()
        {
            if (gbRGAnzeige.Enabled == true)
            {
                decimal mwst = 0.0m;
                decimal mwstBetrag = 0.0m;
                decimal netto = 0.0m;
                decimal brutto = 0.0m;

                if (tbRGNetto.Text != "")
                {
                    netto = Convert.ToDecimal(tbRGNetto.Text);
                }
                if (tbRGBrutto.Text != "")
                {
                    brutto = Convert.ToDecimal(tbRGBrutto.Text);
                }
                if (nudRGMwSt.Value == 0)
                {
                    brutto = netto;
                }
                else
                {
                    //netto = Convert.ToDecimal(tbNetto.Text);
                    mwst = nudRGMwSt.Value / 100;
                    mwstBetrag = netto * mwst;
                    brutto = netto + mwstBetrag;
                }

                tbRGMwStBetrag.Text = Functions.FormatDecimal(mwstBetrag);
                tbRGNetto.Text = Functions.FormatDecimal(netto);
                tbRGBrutto.Text = Functions.FormatDecimal(brutto);
                gbRGAnzeige.Refresh();
            }
        }
        //
        //------ Berechnung/Aktuallisierung Gutschriftübersicht -----------
        //
        private void BerechnungGSBetrag()
        {
            if (gbGSAnzeige.Enabled == true)
            {
                decimal mwst = 0.0m;
                decimal mwstBetrag = 0.0m;
                decimal netto = 0.0m;
                decimal brutto = 0.0m;

                if (tbGSNetto.Text != "")
                {
                    netto = Convert.ToDecimal(tbGSNetto.Text);
                }
                if (tbGSBrutto.Text != "")
                {
                    brutto = Convert.ToDecimal(tbGSBrutto.Text);
                }
                if (nudGSMwSt.Value == 0)
                {
                    brutto = netto;
                }
                else
                {
                    //netto = Convert.ToDecimal(tbNetto.Text);
                    mwst = nudGSMwSt.Value / 100;
                    mwstBetrag = netto * mwst;
                    brutto = netto + mwstBetrag;
                }

                tbGSMwStBetrag.Text = Functions.FormatDecimal(mwstBetrag);
                tbGSNetto.Text = Functions.FormatDecimal(netto);
                tbGSBrutto.Text = Functions.FormatDecimal(brutto);
                gbGSAnzeige.Refresh();
            }
        }
        //
        //----------- MwST - Satz Änderung RG ----------------
        //
        private void nudMwSt_ValueChanged(object sender, EventArgs e)
        {
            BerechnungRGBetrag();
        }
        //
        //----------- MwST - Satz Änderung GS ----------------
        //
        private void nupGSMwSt_ValueChanged(object sender, EventArgs e)
        {
            BerechnungGSBetrag();
        }
        //
        //------------ MwST Satz des Kunden -------------------- 
        //
        private void SetMwStSatz()
        {
            clsKunde kunde = new clsKunde();
            kunde.BenutzerID = GL_User.User_ID;
            kunde.ADR_ID = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["A_ID"];
            decimal test = kunde.GetMwStSatz();
            nudRGMwSt.Value = test;
            nudGSMwSt.Value = test;
        }
        //
        //---------- setzt die RG Info mit daten aus ds.Table["Fracht"]
        //
        private void SetgbRGInfo()
        {
            if (ds.Tables["Fracht"] != null)
            {
                decimal prozent = (decimal)ds.Tables["Fracht"].Rows[0]["MargeProzent"] * 100;
                tbMargeEuro.Text = Functions.FormatDecimal((decimal)ds.Tables["Fracht"].Rows[0]["MargeEuro"]);
                tbMargePro.Text = Functions.FormatDecimal(prozent);
            }
            else
            {
                decimal defdec = 0.00m;
                tbMargeEuro.Text = Functions.FormatDecimal(defdec);
                tbMargePro.Text = Functions.FormatDecimal(defdec);
            }
        }
        //
        //
        //
        private void OpenAuftragserfassungADREingabe()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmADRPanelFakturierung)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmADRPanelFakturierung));
            }
            //frmADRPanelFakturierung ADR = new frmADRPanelFakturierung(this);
            frmADRPanelFakturierung ADR = new frmADRPanelFakturierung();
            ADR.SetFakturierungFrm(this);
            ADR.StartPosition = FormStartPosition.CenterScreen;
            ADR.Dock = DockStyle.Fill;
            ADR.Show();
            ADR.BringToFront();
        }
        //
        //--------------------- ADR SEARCH Kunde ------------------------------
        //
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            OpenAuftragserfassungADREingabe();
        }
        private void btnSearchV_Click_1(object sender, EventArgs e)
        {
            SearchButton = 2;
            OpenAuftragserfassungADREingabe();
            SetDatenToForm();
        }
        private void btnSearchE_Click_1(object sender, EventArgs e)
        {
            SearchButton = 3;
            OpenAuftragserfassungADREingabe();
            SetDatenToForm();
        }
        private void btnSU_Click(object sender, EventArgs e)
        {
            SearchButton = 4;
            OpenAuftragserfassungADREingabe();
        }
        //
        //------------------ ADR - ID für ------------------
        //
        public void SetADRRecAfterADRSearch(decimal ADR_ID)
        {
            string strE = string.Empty;
            string strMC = string.Empty;
            DataSet dsADR = clsADR.ReadADRbyID(ADR_ID);
            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strMC = dsADR.Tables[0].Rows[i]["ViewID"].ToString();
                strE = dsADR.Tables[0].Rows[i]["ViewID"].ToString() + " - ";
                strE = strE + dsADR.Tables[0].Rows[i]["KD_ID"].ToString() + " - ";
                strE = strE + dsADR.Tables[0].Rows[i]["Name1"].ToString() + " - ";
                strE = strE + dsADR.Tables[0].Rows[i]["PLZ"].ToString() + " - ";
                strE = strE + dsADR.Tables[0].Rows[i]["Ort"].ToString();

                //SearchButton
                // 1 = KD
                // 2 = Versender
                // 3 = Empfänger
                // 4 = Subunternehmer

                decimal auftrag = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["AuftragID"];

                switch (SearchButton)
                {
                    case 1:
                        //Auftraggeber
                        string ADRBezeichnungA = "Auftraggeber";
                        clsAuftrag.updateADR_ID(auftrag, ADRBezeichnungA, ADR_ID);
                        // ID muss im DataTable hinterlegt werden
                        ds.Tables["Auftragsdaten"].Rows[0]["A_ID"] = ADR_ID;
                        ds.Tables["Auftragsdaten"].Rows[0]["Auftraggeber"] = (string)dsADR.Tables[0].Rows[i]["Name1"];
                        ds.Tables["Auftragsdaten"].Rows[0]["A_Strasse"] = (string)dsADR.Tables[0].Rows[i]["Str"];
                        ds.Tables["Auftragsdaten"].Rows[0]["A_PLZ"] = (string)dsADR.Tables[0].Rows[i]["PLZ"];
                        ds.Tables["Auftragsdaten"].Rows[0]["A_Ort"] = (string)dsADR.Tables[0].Rows[i]["ORT"];
                        //SetDatenToForm();
                        this.Refresh();
                        break;

                    case 2:
                        //Versender
                        string ADRBezeichnungV = "Versender";
                        if (GSEingabeFrachtVorlage)
                        {
                            if (clsFakturierung.IsAPisInFVGS((decimal)ds.Tables["Auftragsdaten"].Rows[0]["ID"]))
                            {
                                clsFakturierung.updateFVADR((decimal)ds.Tables["Auftragsdaten"].Rows[0]["ID"], ADRBezeichnungV, ADR_ID);
                            }
                            else
                            {
                                if (ds.Tables["Auftragsdaten"].Columns["FV_B_ID"] == null)
                                {
                                    ds.Tables["Auftragsdaten"].Columns.Add("FV_B_ID", typeof(decimal));
                                }
                                ds.Tables["Auftragsdaten"].Rows[0]["FV_B_ID"] = ADR_ID;
                            }

                        }
                        else
                        {
                            clsAuftrag.updateADR_ID(auftrag, ADRBezeichnungV, ADR_ID);
                        }
                        // ID muss im DataTable hinterlegt werden
                        ds.Tables["Auftragsdaten"].Rows[0]["V_ID"] = ADR_ID;
                        ds.Tables["Auftragsdaten"].Rows[0]["Versender"] = (string)dsADR.Tables[0].Rows[i]["Name1"];
                        ds.Tables["Auftragsdaten"].Rows[0]["V_Strasse"] = (string)dsADR.Tables[0].Rows[i]["Str"];
                        ds.Tables["Auftragsdaten"].Rows[0]["V_PLZ"] = (string)dsADR.Tables[0].Rows[i]["PLZ"];
                        ds.Tables["Auftragsdaten"].Rows[0]["V_Ort"] = (string)dsADR.Tables[0].Rows[i]["ORT"];
                        //SetDatenToForm();
                        this.Refresh();
                        break;

                    case 3:
                        //Empfänger
                        string ADRBezeichnungE = "Empfänger";
                        if (GSEingabeFrachtVorlage)
                        {
                            if (clsFakturierung.IsAPisInFVGS((decimal)ds.Tables["Auftragsdaten"].Rows[0]["ID"]))
                            {
                                clsFakturierung.updateFVADR((decimal)ds.Tables["Auftragsdaten"].Rows[0]["ID"], ADRBezeichnungE, ADR_ID);
                            }
                            else
                            {
                                if (ds.Tables["Auftragsdaten"].Columns["FV_E_ID"] == null)
                                {
                                    ds.Tables["Auftragsdaten"].Columns.Add("FV_B_ID", typeof(decimal));
                                }
                                ds.Tables["Auftragsdaten"].Rows[0]["FV_E_ID"] = ADR_ID;
                            }
                        }
                        else
                        {
                            clsAuftrag.updateADR_ID(auftrag, ADRBezeichnungE, ADR_ID);
                        }
                        // ID muss im DataTable hinterlegt werden
                        ds.Tables["Auftragsdaten"].Rows[0]["E_ID"] = ADR_ID;
                        ds.Tables["Auftragsdaten"].Rows[0]["Empfänger"] = (string)dsADR.Tables[0].Rows[i]["Name1"];
                        ds.Tables["Auftragsdaten"].Rows[0]["E_Strasse"] = (string)dsADR.Tables[0].Rows[i]["Str"];
                        ds.Tables["Auftragsdaten"].Rows[0]["E_PLZ"] = (string)dsADR.Tables[0].Rows[i]["PLZ"];
                        ds.Tables["Auftragsdaten"].Rows[0]["E_Ort"] = (string)dsADR.Tables[0].Rows[i]["ORT"];
                        //SetDatenToForm();
                        this.Refresh();
                        break;
                    case 4:
                        //Subunternehmer  //Achtung andere SQL für DB update
                        //ID in DB Auftrag ändern
                        decimal AP_ID = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["ID"];
                        clsFrachtvergabe.updateSU_ID(AP_ID, ADR_ID);

                        // ID muss im DataTable hinterlegt werden
                        ds.Tables["Auftragsdaten"].Rows[0]["SU_ID"] = ADR_ID;
                        ds.Tables["Auftragsdaten"].Rows[0]["SU"] = (string)dsADR.Tables[0].Rows[i]["Name1"];
                        ds.Tables["Auftragsdaten"].Rows[0]["SU_Strasse"] = (string)dsADR.Tables[0].Rows[i]["Str"];
                        ds.Tables["Auftragsdaten"].Rows[0]["SU_PLZ"] = (string)dsADR.Tables[0].Rows[i]["PLZ"];
                        ds.Tables["Auftragsdaten"].Rows[0]["SU_Ort"] = (string)dsADR.Tables[0].Rows[i]["ORT"];
                        //SetDatenToForm();
                        this.Refresh();
                        break;
                }
                SetDatenToForm();
            }
        }
        //
        //--------- Frm-Änderung bei Eingabe der Frachtvorlagengutschrift ---------------
        //
        private void ChangeFrmForGSFrachtVorlage(bool EingabeGSFrachtVorlage)
        {
            //entsprechenden Frm - Elemente werden ausgeblendet
            if (EingabeGSFrachtVorlage)
            {
                this.Text = "Erfassung Gutschrift für Frachtvorlage";
                btnSearchA.Enabled = false;
                btnSU.Enabled = false;
                gbRGAnzeige.Enabled = false;
                gbGSAnzeige.Enabled = false;
                gbInfoRG.Enabled = false;
                btnFVGS.Enabled = false;
                BtnPauschalKomplett.Enabled = false;
                //gbFrachtvorlage.Enabled = true;
                lFVGS.Visible = true;
            }
            else
            {
                this.Text = "Fakturierung";
                btnSearchA.Enabled = true;
                btnSU.Enabled = true;
                gbRGAnzeige.Enabled = true;
                gbGSAnzeige.Enabled = true;
                gbInfoRG.Enabled = true;
                btnFVGS.Enabled = true;
                BtnPauschalKomplett.Enabled = true;
                // gbFrachtvorlage.Enabled = false;
                lFVGS.Visible = false;
            }
        }
        //
        //------------------ Rechnung / Gutschrift speichern -----------------
        //
        private void tsbtnSpeichern_Click(object sender, EventArgs e)
        {
            if (ds.Tables["Fracht"] != null)
            {
                // MwSt
                if ((bool)ds.Tables["Fracht"].Rows[0]["RG"])
                {
                    ds.Tables["Auftragsdaten"].Rows[0]["MwStSatz"] = nudRGMwSt.Value;
                }
                else
                {
                    ds.Tables["Auftragsdaten"].Rows[0]["MwStSatz"] = Convert.ToDecimal(nudGSMwSt.Value);
                }
                //Erstellt eine Tabelle für den entsprechenden Insert
                SetFrachtdaten();

                //FVGS
                if (GSEingabeFrachtVorlage)
                {
                    //Datensetzen für DB insert
                    clsFakturierung InsertFakt = new clsFakturierung();
                    InsertFakt.ds = ds;
                    InsertFakt.InsertToFrachten();

                    GSEingabeFrachtVorlage = false;
                    ChangeFrmForGSFrachtVorlage(GSEingabeFrachtVorlage);
                    this.Close();

                }
                else // alles andere
                {
                    //Datensetzen für DB insert
                    clsFakturierung InsertFakt = new clsFakturierung();
                    InsertFakt.ds = ds;
                    InsertFakt.InsertToFrachten();
                }

                //Check auf GS - Status kann dann direkt geändert werden
                decimal AP_ID = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["ID"];

                if (!clsFrachtvergabe.IsIDIn(AP_ID))
                {
                    //Check for "Eingangs-"Gutschrift, wenn vorhanden kann der Status erhöht werden!
                    //eine Rechnung muss erst noch gedruckt werden
                    if (clsRechnungen.CheckForGS(AP_ID))
                    {
                        clsAuftragsstatus status = new clsAuftragsstatus();
                        status.Auftrag_ID = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["AuftragID"];
                        status.AuftragPos = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["AuftragPos"];
                        status.SetStatusBerechnet();
                    }
                }
                if (!SavenPrint)
                {
                    this.Close();
                    ctrFakturierungRefersh();
                }
            }
        }
        //
        //------------------- Rechnung / Gutschrift speichern und drucken -----------------
        //
        private void tsbtnSavePrint_Click(object sender, EventArgs e)
        {
            SavenPrint = true;
            tsbtnSpeichern_Click(sender, e);

            //Check weiter nur bei Rechnung
            if (clsKommission.IsAuftragPositionIn(this.GL_User, (decimal)ds.Tables["Auftragsdaten"].Rows[0]["ID"]))
            {
                decimal iAuftrag = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["AuftragID"];
                decimal iAuftragPos = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["AuftragPos"];
                decimal iAP_ID = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["ID"];
                ds.Dispose();
                ctrFakturierungCreateRG(iAP_ID, iAuftrag, iAuftragPos, true, true);

                this.Close();
            }
            else
            {
                this.Close();
            }
            ctrFakturierungRefersh();
        }
        //
        //
        //
        private void SetFrachtdaten()
        {
            string Tabellenname = string.Empty;

            switch (Abrechnungsart)
            {
                case "AuftragsPositionPauschal":
                    if (!GSEingabeFrachtVorlage)
                    {
                        Tabellenname = "Frachtdaten";
                    }
                    else
                    {
                        Tabellenname = "FVGS";
                    }
                    SetTableForFrachten(Tabellenname);
                    break;

                case "AuftragsPositionTarif":
                    if (!GSEingabeFrachtVorlage)
                    {
                        Tabellenname = "Frachtdaten";
                    }
                    else
                    {
                        Tabellenname = "FVGS";
                    }
                    SetTableForFrachten(Tabellenname);
                    break;

                case "ArtikelPauschal":
                    if (!GSEingabeFrachtVorlage)
                    {
                        Tabellenname = "Frachtdaten";
                    }
                    else
                    {
                        Tabellenname = "FVGS";
                    }
                    SetTableForFrachten(Tabellenname);
                    break;

                case "ArtikelTarif":
                    if (!GSEingabeFrachtVorlage)
                    {
                        Tabellenname = "Frachtdaten";
                    }
                    else
                    {
                        Tabellenname = "FVGS";
                    }
                    SetTableForFrachten(Tabellenname);
                    break;

                case "GutschriftAuftragPosPauschal":
                    if (!GSEingabeFrachtVorlage)
                    {
                        Tabellenname = "Frachtdaten";
                    }
                    else
                    {
                        Tabellenname = "FVGS";
                    }
                    SetTableForFrachten(Tabellenname);
                    break;

                case "GutschriftanSU":
                    Tabellenname = "GS_SU";
                    SetTableForFrachten(Tabellenname);
                    break;

                case "GutschriftFrachtvorlage":
                    if (!GSEingabeFrachtVorlage)
                    {
                        Tabellenname = "Frachtdaten";
                    }
                    else
                    {
                        Tabellenname = "FVGS";
                    }
                    SetTableForFrachten(Tabellenname);
                    break;
            }
            if (Tabellenname != "")
            {
                SetValue(Tabellenname);
            }
        }
        //
        //------------ Füllen der Table / DS -----------------
        //
        private void SetTableForFrachten(string Tabellenname)
        {
            if (ds.Tables[Tabellenname] == null)
            {
                DataTable dt = new DataTable(Tabellenname);
                ds.Tables.Add(dt);
            }
            ds.Tables[Tabellenname].Columns.Add("AP_ID", typeof(decimal));
            ds.Tables[Tabellenname].Columns.Add("Artikel_ID", typeof(decimal));
            ds.Tables[Tabellenname].Columns.Add("Fracht", typeof(decimal));
            ds.Tables[Tabellenname].Columns.Add("KD_ID", typeof(decimal));
            ds.Tables[Tabellenname].Columns.Add("Frachttext", typeof(string));
            ds.Tables[Tabellenname].Columns.Add("km", typeof(Int32));
            ds.Tables[Tabellenname].Columns.Add("fpflGewicht", typeof(decimal));
            ds.Tables[Tabellenname].Columns.Add("Pauschal", typeof(bool));
            if (Tabellenname == "FVGS")
            {
                ds.Tables[Tabellenname].Columns.Add("FV_B_ID", typeof(decimal));
                ds.Tables[Tabellenname].Columns.Add("FV_E_ID", typeof(decimal));
            }
            ds.Tables[Tabellenname].Columns.Add("MargeEuro", typeof(decimal));
            ds.Tables[Tabellenname].Columns.Add("MwStSatz", typeof(decimal));
            ds.Tables[Tabellenname].Columns.Add("Frachtsatz", typeof(decimal));
            ds.Tables[Tabellenname].Columns.Add("TextZusatzkosten", typeof(string));
            ds.Tables[Tabellenname].Columns.Add("Zusatzkosten", typeof(decimal));
            ds.Tables[Tabellenname].Columns.Add("GS_ID", typeof(string));
            ds.Tables[Tabellenname].Columns.Add("GS_Datum", typeof(DateTime));
            if (Tabellenname == "GS_SU")
            {
                ds.Tables[Tabellenname].Columns.Add("GSanSU", typeof(bool));
            }
        }

        private void SetValue(string Tabelle)
        {
            for (Int32 i = 0; i <= ds.Tables["Fracht"].Rows.Count - 1; i++)
            {
                DataRow row;
                row = ds.Tables[Tabelle].NewRow();
                row["AP_ID"] = (decimal)ds.Tables["Fracht"].Rows[i]["AP_ID"];
                row["Artikel_ID"] = (decimal)ds.Tables["Fracht"].Rows[i]["Artikel_ID"];
                row["Fracht"] = (decimal)ds.Tables["Fracht"].Rows[i]["Fracht"];
                if (Tabelle == "GS_SU")
                {
                    row["KD_ID"] = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["SU_ID"];
                    row["GSanSU"] = true;
                }
                else
                {
                    row["KD_ID"] = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["A_ID"];
                }
                if (ds.Tables["Fracht"].Rows[i]["Frachttext"] is DBNull)
                {
                    row["Frachttext"] = string.Empty;
                }
                else
                {
                    row["Frachttext"] = (string)ds.Tables["Fracht"].Rows[i]["Frachttext"];
                }
                row["km"] = (Int32)ds.Tables["Fracht"].Rows[i]["km"];
                row["Pauschal"] = (bool)ds.Tables["Fracht"].Rows[i]["Pauschal"];
                row["fpflGewicht"] = (decimal)ds.Tables["Fracht"].Rows[i]["fpflGewicht"];
                if (Tabelle == "FVGS")
                {
                    if (ds.Tables["Auftragsdaten"].Rows[0]["FV_B_ID"] != DBNull.Value)
                    {
                        row["FV_B_ID"] = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["FV_B_ID"];
                    }
                    if (ds.Tables["Auftragsdaten"].Rows[0]["FV_E_ID"] != DBNull.Value)
                    {
                        row["FV_E_ID"] = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["FV_E_ID"];
                    }
                }
                row["MargeEuro"] = (decimal)ds.Tables["Fracht"].Rows[i]["MargeEuro"];
                row["MwStSatz"] = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["MwStSatz"];
                row["Frachtsatz"] = (decimal)ds.Tables["Fracht"].Rows[i]["Frachtsatz"];
                row["TextZusatzkosten"] = (string)ds.Tables["Auftragsdaten"].Rows[0]["TextZusatzKosten"];
                row["Zusatzkosten"] = (decimal)ds.Tables["Auftragsdaten"].Rows[0]["ZusatzKosten"];
                if (ds.Tables["Fracht"].Rows[0]["GS_ID"] != DBNull.Value)
                {
                    row["GS_ID"] = (string)ds.Tables["Fracht"].Rows[0]["GS_ID"];
                }
                if (ds.Tables["Fracht"].Rows[0]["GS_Date"] != DBNull.Value)
                {
                    row["GS_Datum"] = (DateTime)ds.Tables["Fracht"].Rows[0]["GS_Date"];
                }

                ds.Tables[Tabelle].Rows.Add(row);
            }
        }

    }
}
