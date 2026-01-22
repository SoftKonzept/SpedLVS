namespace LVS.Dokumente
{
    using LVS;
    using System;
    using System.Data;

    /// <summary>
    /// Summary description for docRechnung.
    /// </summary>
    public partial class docRechnung : docBriefkofpHeisiep
    {
        public DataSet dsRG = new DataSet();
        internal DataTable dtPos = new DataTable("Rechnungspositionen");
        internal DataTable dtSumme = new DataTable("Summe");
        public string drRGnr = string.Empty;
        public string drRGDatum = string.Empty;

        public docRechnung()
        {
            InitializeComponent();
        }
        //
        //----- Übergabe DataSet ------------
        //
        public void InitRechnung(DataSet ds)
        {
            SetKontaktDaten = false;
            dsRG = ds;
            SetRechnungspositionen();

            //Überprüfung, ob GS / RG noch einmal gedruck werden sollen
            if (dsRG.Tables["PrintAgain"] == null)
            {
                //Eintrag RG-Daten in DB Rechnung
                SetRGDaten();

                //Update Status Auftrag berechnet
                for (Int32 i = 0; i <= ds.Tables["Frachtdaten"].Rows.Count - 1; i++)
                {
                    decimal auftrag = (decimal)ds.Tables["Frachtdaten"].Rows[i]["AuftragID"];
                    decimal auftragPos = (decimal)ds.Tables["Frachtdaten"].Rows[i]["AuftragPos"];
                    /*************************************************
                     * Update Status erst, wenn
                     * a) bei Selbsteintritt die RG and Kunde erstellt/gedruckt wurde
                     * b) bei Frachtvergabe die RG an Kunden und GS an SU erstellt und gedruckt wurde
                     * 
                     * ***********************************************/
                    if (clsRechnungen.AbrechnungAbgeschlossen(ref dsRG))
                    {
                        UpdateStatus(auftrag, auftragPos);
                    }
                }
            }
        }
        //
        //------------- Auftrag wird als berechnet gesetzt ------------
        //
        private void UpdateStatus(decimal auftrag, decimal auftragPos)
        {
            clsAuftragsstatus status = new clsAuftragsstatus();
            status.Auftrag_ID = auftrag;
            status.AuftragPos = auftragPos;
            status.SetStatusBerechnet();
        }
        //
        //----------- Eintrag Daten in DB RG -------------------
        //
        public void SetRGDaten()
        {
            /****
            //clsRechnungen rg = new Sped4.clsRechnungen();
            for (Int32 i = 0; i <= dsRG.Tables["Rechnung"].Rows.Count - 1; i++)
            {
              clsRechnungen rg = new Sped4.clsRechnungen();
              //bool GS = (bool)dsRG.Tables["Frachtdaten"].Rows[i]["GS"];
              bool GS = (bool)dsRG.Tables["Frachtdaten"].Rows[i]["GS"];
              rg.RG_ID = (Int32)dsRG.Tables["Rechnung"].Rows[i]["RGNr"];
              rg.MwStSatz = (decimal)dsRG.Tables["Frachtdaten"].Rows[i]["MwStSatz"];
              rg.AddRGGS(GS);
              rg.SetRGNrToFrachten(dsRG);
            }
              ***/
            //clsRechnungen rg = new Sped4.clsRechnungen();
            for (Int32 i = 0; i <= dsRG.Tables["Rechnung"].Rows.Count - 1; i++)
            {
                bool GS = false;
                clsRechnungen rg = new clsRechnungen();
                //bool GS = (bool)dsRG.Tables["RGGSDaten"].Rows[i]["GS"];

                if (dsRG.Tables["RGGSDaten"] != null)
                {
                    GS = (bool)dsRG.Tables["RGGSDaten"].Rows[i]["GS"];
                    rg.MwStSatz = (decimal)dsRG.Tables["RGGSDaten"].Rows[i]["MwStSatz"];
                }
                if (dsRG.Tables["Frachtdaten"] != null)
                {
                    GS = (bool)dsRG.Tables["Frachtdaten"].Rows[i]["GS"];
                    rg.MwStSatz = (decimal)dsRG.Tables["Frachtdaten"].Rows[i]["MwStSatz"];
                }
                rg.RG_ID = (decimal)dsRG.Tables["Rechnung"].Rows[i]["RGNr"];
                //rg.MwStSatz = (decimal)dsRG.Tables["RGGSDaten"].Rows[i]["MwStSatz"];
                rg.AddRGGS(GS);
                rg.SetRGNrToFrachten(dsRG);
            }
        }
        //
        //
        //
        private void SetRechnungspositionen()
        {
            //**************************************Briefkopf
            SetRGEmpfaenger();

            // RG / GS and Nr.
            SetDocNameAndNr();

            //KDNummer
            tbKDNummer.Value = "Konto-Nr:";

            decimal KDNr = 0;
            if (dsRG.Tables["RGGSDaten"] != null)
            {
                KDNr = (decimal)dsRG.Tables["Auftragsdaten"].Rows[0]["Fracht_ADR"];
            }
            else
            {
                if (((bool)dsRG.Tables["Frachtdaten"].Rows[0]["GS"]) & ((bool)dsRG.Tables["Frachtdaten"].Rows[0]["GS_SU"]))
                {
                    KDNr = (decimal)dsRG.Tables["Auftragsdaten"].Rows[0]["SU_ID"];
                }
                else
                {
                    KDNr = (decimal)dsRG.Tables["Auftragsdaten"].Rows[0]["KD"];
                }
            }

            KDNr = clsADR.GetKD_IDByID(KDNr);
            tbKDNr.Value = KDNr.ToString();

            //************************************ RG / GS Positionen

            bk_Details.Dispose();

            SetColumnsTablePos();
            //Anzahl der Positionen ermitteln
            object obj = dsRG.Tables["Frachtdaten"].Compute("Count(AP_ID)", "ID>0");

            Int32 Anzahl = (Int32)obj;

            //DataTable RGPositionen füllen und Row hinzufügen
            for (Int32 i = 0; i <= Anzahl - 1; i++)
            {
                decimal AP_ID = (decimal)dsRG.Tables["Frachtdaten"].Rows[i]["AP_ID"];
                DataRow row;
                row = dtPos.NewRow();
                row["AP_ID"] = AP_ID;
                row["Transportdaten"] = OutputTransportdaten(AP_ID);
                row["Versender"] = OutputVersender(AP_ID);
                row["Empfaenger"] = OutputEmpfaenger(AP_ID);
                row["Artikeldaten"] = OutputArtikeldaten(AP_ID);
                row["Berechnungsdaten"] = OutputBerechnungsdaten(AP_ID);
                row["TextZusatzkosten"] = OutputZusatzKostenText(AP_ID);
                row["ZusatzKosten"] = OutputZusatzkosten(AP_ID);
                row["PosFracht"] = OutputFracht(AP_ID);
                row["strPosFracht"] = strOutputFracht(AP_ID);
                row["strZusatzKosten"] = strOutputZusatzkosten(AP_ID);

                if (dtPos.Rows.Count > 0)
                {
                    if (!Functions.IsRowInDataTable(dtPos, row))
                    {
                        dtPos.Rows.Add(row);
                    }
                }
                else
                {
                    dtPos.Rows.Add(row);
                }
            }

            // DataTable 
            SetColumnsTableSumme();

            object obj1 = dtPos.Compute("SUM(PosFracht)", "AP_ID>0");
            object obj2 = dtPos.Compute("SUM(ZusatzKosten)", "AP_ID>0");

            decimal netto = (decimal)obj1 + (decimal)obj2;
            decimal mwstS = (decimal)dsRG.Tables["Frachtdaten"].Rows[0]["MwStSatz"] / 100;
            decimal mwstEuro = netto * mwstS;
            decimal brutto = netto + mwstEuro;

            DataRow row1;
            row1 = dtSumme.NewRow();
            row1["Netto"] = netto;
            row1["MwStP"] = mwstS * 100;
            row1["MwStEuro"] = mwstEuro;
            row1["Brutto"] = brutto;
            dtSumme.Rows.Add(row1);
        }
        //
        //--------- Bestimmung des Dokumentennamens ------------------------------
        //
        public string GetDocName()
        {
            string DocName = string.Empty;

            if (dsRG.Tables["RGGSDaten"] != null)
            {
                //Manuelle RG oder GS
                if ((bool)dsRG.Tables["Auftragsdaten"].Rows[0]["GS"])
                {
                    DocName = "GUTSCHRIFT";
                }
                else
                {
                    DocName = "RECHNUNG";
                }
            }
            else
            {
                for (Int32 i = 0; i <= dsRG.Tables["Frachtdaten"].Rows.Count - 1; i++)
                {
                    if ((bool)dsRG.Tables["Frachtdaten"].Rows[i]["GS"] == true)
                    {
                        if ((bool)dsRG.Tables["Frachtdaten"].Rows[i]["FVGS"] == true)
                        {
                            DocName = "FV - GUTSCHRIFT";
                        }
                        else
                        {
                            DocName = "GUTSCHRIFT";
                        }
                    }
                    else
                    {
                        DocName = "RECHNUNG";
                    }
                }
            }

            return DocName;
        }
        /*****************************************************************
         *                 Table Rechnungspos
         ****************************************************************/
        //
        //
        private void SetColumnsTablePos()
        {
            dtPos.Columns.Add("AP_ID", typeof(decimal));
            dtPos.Columns.Add("Transportdaten", typeof(string));
            dtPos.Columns.Add("Versender", typeof(string));
            dtPos.Columns.Add("Empfaenger", typeof(string));
            dtPos.Columns.Add("Artikeldaten", typeof(string));
            dtPos.Columns.Add("Berechnungsdaten", typeof(string));
            dtPos.Columns.Add("TextZusatzkosten", typeof(string));
            dtPos.Columns.Add("Zusatzkosten", typeof(decimal));
            dtPos.Columns.Add("PosFracht", typeof(decimal));
            dtPos.Columns.Add("strPosFracht", typeof(string));
            dtPos.Columns.Add("strZusatzKosten", typeof(string));
        }
        /*******************************************************************
         *                Table Summe
         ******************************************************************/
        //
        //
        //
        public void SetColumnsTableSumme()
        {
            dtSumme.Columns.Add("Netto", typeof(decimal));
            dtSumme.Columns.Add("MwStP", typeof(decimal));
            dtSumme.Columns.Add("MwStEuro", typeof(decimal));
            dtSumme.Columns.Add("Brutto", typeof(decimal));
        }
        //
        //-------------- Rechnungsempfänger ------------------------
        //
        public void SetRGEmpfaenger()
        {
            //DataSet RG Empfänger
            DataSet dsRGE = new DataSet();

            if (dsRG.Tables["RGGSDaten"] != null)
            {
                dsRGE = clsADR.ReadADRbyID((decimal)dsRG.Tables["Auftragsdaten"].Rows[0]["Fracht_ADR"]);
            }
            else
            {
                //GSanSU
                if (((bool)dsRG.Tables["Frachtdaten"].Rows[0]["GS"]) & ((bool)dsRG.Tables["Frachtdaten"].Rows[0]["GS_SU"]))
                {
                    dsRGE = clsADR.ReadADRbyID((decimal)dsRG.Tables["Auftragsdaten"].Rows[0]["SU_ID"]);
                }
                //FVGS
                if (((bool)dsRG.Tables["Frachtdaten"].Rows[0]["GS"]) & ((bool)dsRG.Tables["Frachtdaten"].Rows[0]["FVGS"]))
                {
                    dsRGE = clsADR.ReadADRbyID((decimal)dsRG.Tables["Frachtdaten"].Rows[0]["Fracht_ADR"]);
                }
                //RG
                if (!(bool)dsRG.Tables["Frachtdaten"].Rows[0]["GS"])
                {
                    dsRGE = clsADR.ReadADRbyID((decimal)dsRG.Tables["Auftragsdaten"].Rows[0]["KD"]);
                }
            }
            /**************** **********************************************************
             *  alles um eine Zeile nach unten versetzt, da tbADRzHD hier nicht gebraucht wird
             * 
                tbADRAnrede.Value = dsRGE.Tables[0].Rows[0]["FBez"].ToString();
                tbADRName1.Value = dsRGE.Tables[0].Rows[0]["Name1"].ToString();
                tbADRName2.Value = dsRGE.Tables[0].Rows[0]["Name2"].ToString();
                tbADRStr.Value = dsRGE.Tables[0].Rows[0]["Str"].ToString();
                tbADRPLZ.Value = dsRGE.Tables[0].Rows[0]["PLZ"].ToString();
                tbADROrt.Value = dsRGE.Tables[0].Rows[0]["Ort"].ToString();
             * 
             * ***************************************************************************/

            tbADRName1.Value = dsRGE.Tables[0].Rows[0]["FBez"].ToString();
            tbADRName2.Value = dsRGE.Tables[0].Rows[0]["Name1"].ToString();
            tbADRzHd.Value = dsRGE.Tables[0].Rows[0]["Name2"].ToString();
            tbADRStr.Value = dsRGE.Tables[0].Rows[0]["Str"].ToString();
            tbADRPLZ.Value = dsRGE.Tables[0].Rows[0]["PLZ"].ToString();
            tbADROrt.Value = dsRGE.Tables[0].Rows[0]["Ort"].ToString();
        }
        //
        //--------------- Ausgabe Versender -----------------
        //
        private string OutputVersender(decimal AP_ID)
        {
            string ausgabe = string.Empty;
            for (Int32 i = 0; i <= dsRG.Tables["Auftragsdaten"].Rows.Count - 1; i++)
            {
                if ((decimal)dsRG.Tables["Auftragsdaten"].Rows[i]["ID"] == AP_ID)
                {
                    ausgabe = ausgabe + dsRG.Tables["Auftragsdaten"].Rows[i]["Beladestelle"].ToString().Trim() + ", ";
                    ausgabe = ausgabe + dsRG.Tables["Auftragsdaten"].Rows[i]["B_PLZ"].ToString().Trim() + " ";
                    ausgabe = ausgabe + dsRG.Tables["Auftragsdaten"].Rows[i]["B_Ort"].ToString().Trim();
                }
            }
            return ausgabe;
        }
        //
        //--------------- Ausgabe Empfänger -----------------
        //
        private string OutputEmpfaenger(decimal AP_ID)
        {
            string ausgabe = string.Empty;
            for (Int32 i = 0; i <= dsRG.Tables["Auftragsdaten"].Rows.Count - 1; i++)
            {
                if ((decimal)dsRG.Tables["Auftragsdaten"].Rows[i]["ID"] == AP_ID)
                {
                    ausgabe = ausgabe + dsRG.Tables["Auftragsdaten"].Rows[i]["Entladestelle"].ToString().Trim() + ", ";
                    ausgabe = ausgabe + dsRG.Tables["Auftragsdaten"].Rows[i]["E_PLZ"].ToString().Trim() + " ";
                    ausgabe = ausgabe + dsRG.Tables["Auftragsdaten"].Rows[i]["E_Ort"].ToString().Trim();
                }
            }
            return ausgabe;
        }
        //
        //--------------- Ausgabe Transportdaten -----------------
        //
        private string OutputTransportdaten(decimal AP_ID)
        {
            //nur die erste Zeile, da alle weiteren Daten in der Column identisch sind
            string ausgabe = string.Empty;
            for (Int32 i = 0; i <= dsRG.Tables["Frachtdaten"].Rows.Count - 1; i++)
            {
                decimal test = (decimal)dsRG.Tables["Frachtdaten"].Rows[0]["AP_ID"];
                if ((decimal)dsRG.Tables["Frachtdaten"].Rows[i]["AP_ID"] == AP_ID)
                {
                    ausgabe = "";
                    if (dsRG.Tables["Frachtdaten"].Rows[i]["B_Datum_SU"].ToString() == "")
                    {
                        ausgabe = ausgabe + ((DateTime)dsRG.Tables["Frachtdaten"].Rows[i]["B_Datum"]).ToShortDateString();
                    }
                    else
                    {
                        ausgabe = ausgabe + ((DateTime)dsRG.Tables["Frachtdaten"].Rows[i]["B_Datum_SU"]).ToShortDateString();
                    }
                    ausgabe = ausgabe + " / Auftrag-Pos.: " + dsRG.Tables["Frachtdaten"].Rows[i]["AuftragID"].ToString() +
                                                          " - " + dsRG.Tables["Frachtdaten"].Rows[i]["AuftragPos"].ToString();
                }
            }
            return ausgabe;
        }
        //
        // ------ Ausgabe der Artikeldaten im String ----------------
        //
        private string OutputArtikeldaten(decimal AP_ID)
        {
            DataSet dsArtID = new DataSet();
            string ausgabe = string.Empty;

            for (Int32 i = 0; i <= dsRG.Tables["Artikel"].Rows.Count - 1; i++)
            {
                //prüfen, ob der Artikel zur
                decimal test = (decimal)dsRG.Tables["Artikel"].Rows[i]["AP_ID"];
                if (AP_ID == (decimal)dsRG.Tables["Artikel"].Rows[i]["AP_ID"])
                {
                    ausgabe = ausgabe + dsRG.Tables["Artikel"].Rows[i]["ME"].ToString() + " ";
                    ausgabe = ausgabe + dsRG.Tables["Artikel"].Rows[i]["Gut"].ToString() + " ";
                    ausgabe = ausgabe + Functions.FormatDecimal((decimal)dsRG.Tables["Artikel"].Rows[i]["Brutto"]) + " kg";
                    ausgabe = ausgabe + "\n";
                }
            }
            return ausgabe;
        }
        //
        // ------ Ausgabe der Berechnungsdaten im String ----------------
        //
        private string OutputBerechnungsdaten(decimal AP_ID)
        {
            string ausgabe = string.Empty;

            DataRow[] rows = dsRG.Tables["Frachtdaten"].Select("AP_ID ='" + AP_ID + "'", "AP_ID");
            foreach (DataRow row in rows)
            {
                if (dsRG.Tables["FVGS_Einzeldruck"] != null)
                {
                    if ((bool)row["FVGS"] == true)
                    {
                        ausgabe = ausgabe + row["Frachttext"].ToString() + " ";
                        ausgabe = ausgabe + Functions.FormatDecimal((decimal)row["fpflGewicht"]) + " kg ";
                        ausgabe = ausgabe + row["km"].ToString() + " km";
                        if ((decimal)row["MargeEuro"] > 0)
                        {
                            ausgabe = ausgabe + " - Marge:" + Functions.FormatDecimal((decimal)row["MargeEuro"]) + " € \n";
                        }
                        else
                        {
                            ausgabe = ausgabe + "\n";
                        }
                    }
                }
                else
                {
                    if ((bool)row["FVGS"] == true)
                    {
                        string VOrt = clsADR.GetOrtByID(this.GL_User, (decimal)row["FV_V_ID"]);
                        string EOrt = clsADR.GetOrtByID(this.GL_User, (decimal)row["FV_E_ID"]);
                        ausgabe = "FV-G: " + VOrt + " - " + EOrt + "\n";
                    }
                    else
                    {
                        ausgabe = ausgabe + row["Frachttext"].ToString() + " ";
                        ausgabe = ausgabe + Functions.FormatDecimal((decimal)row["fpflGewicht"]) + " kg ";
                        ausgabe = ausgabe + row["km"].ToString() + " km";
                        if ((decimal)row["MargeEuro"] > 0)
                        {
                            ausgabe = ausgabe + " - Marge:" + Functions.FormatDecimal((decimal)row["MargeEuro"]) + " € \n";
                        }
                        else
                        {
                            ausgabe = ausgabe + "\n";
                        }
                    }
                }
            }
            return ausgabe;
        }
        //
        //------------ Ausgabe für Zusatzkosten --------------------------
        //
        private string OutputZusatzKostenText(decimal AP_ID)
        {
            string ausgabe = string.Empty;
            DataRow[] rows = dsRG.Tables["Frachtdaten"].Select("AP_ID ='" + AP_ID + "'", "AP_ID");
            foreach (DataRow row in rows)
            {
                if ((decimal)row["ZusatzKosten"] > 0)
                {
                    ausgabe = "Zusatzkosten: " + row["TextZusatzkosten"].ToString() + " ";
                }
            }
            return ausgabe;
        }
        //
        //----------- Ausgabe Zusatzkosten -----------------------
        //
        private decimal OutputZusatzkosten(decimal AP_ID)
        {
            decimal ausgabe = 0.00m;

            DataRow[] rows = dsRG.Tables["Frachtdaten"].Select("AP_ID ='" + AP_ID + "'", "AP_ID");
            foreach (DataRow row in rows)
            {
                if ((decimal)row["ZusatzKosten"] > 0)
                {
                    ausgabe = (decimal)row["ZusatzKosten"];
                }
            }
            return ausgabe;
        }
        //
        //----------- Ausgabe Zusatzkosten alss String-----------------------
        //
        private string strOutputZusatzkosten(decimal AP_ID)
        {
            string ausgabe = string.Empty;

            DataRow[] rows = dsRG.Tables["Frachtdaten"].Select("AP_ID ='" + AP_ID + "'", "AP_ID");
            foreach (DataRow row in rows)
            {
                if ((decimal)row["ZusatzKosten"] > 0)
                {
                    ausgabe = Functions.FormatDecimal((decimal)row["ZusatzKosten"]) + " € \n";
                }
            }
            return ausgabe;
        }
        //
        //----------- Ausgabe Fracht -----------------------
        //
        private decimal OutputFracht(decimal AP_ID)
        {
            decimal ausgabe = 0.00m;

            DataRow[] rows = dsRG.Tables["Frachtdaten"].Select("AP_ID ='" + AP_ID + "'", "AP_ID");
            foreach (DataRow row in rows)
            {
                if ((bool)row["FVGS"] == true)
                {
                    ausgabe = ausgabe + ((decimal)row["Fracht"]); // * (-1));
                }
                else
                {
                    ausgabe = ausgabe + (decimal)row["Fracht"];
                }
            }
            return ausgabe;
        }
        //
        //----------- Ausgabe Fracht als String -----------------------
        //
        private string strOutputFracht(decimal AP_ID)
        {
            string ausgabe = string.Empty;

            DataRow[] rows = dsRG.Tables["Frachtdaten"].Select("AP_ID ='" + AP_ID + "'", "AP_ID");
            foreach (DataRow row in rows)
            {
                //ändern?????
                if ((bool)row["FVGS"] == true)
                {
                    ausgabe = ausgabe + Functions.FormatDecimal((decimal)row["Fracht"]) + " € \n";
                }
                else
                {
                    ausgabe = ausgabe + Functions.FormatDecimal((decimal)row["Fracht"]) + " € \n";
                }
            }
            return ausgabe;
        }
        //
        //---------------- Binding Datasource Table -------------------
        //
        private void docRechnung_NeedDataSource(object sender, EventArgs e)
        {
            RGPositionen.DataSource = dtPos;
            ListSumme.DataSource = dtSumme;
        }
        //
        //--------- DocName und RG/GS- NR. wird gesetzt ---------
        //
        public void SetDocNameAndNr()
        {
            tbDocName.Value = GetDocName();
            tbDocNr.Value = dsRG.Tables["Rechnung"].Rows[0]["RGNr"].ToString();
            drRGnr = tbDocNr.Value;
            drRGDatum = DateTime.Today.Date.ToShortDateString();

            //Ort und Datum
            if (dsRG.Tables["PrintAgain"] != null)
            {
                tbDatum.Value = ((DateTime)dsRG.Tables["Frachtdaten"].Rows[0]["RG_Datum"]).ToShortDateString();
                drRGDatum = tbDatum.Value;
            }
        }
    }
}