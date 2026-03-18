namespace LVS.Dokumente
{
    using LVS;
    using System;
    using System.Data;

    /// <summary>
    /// Summary description for docManRGGS.
    /// </summary>
    public partial class docManRGGS : docRechnung
    {
        internal new DataTable dtPos = new DataTable("RGPositionen");
        internal new DataTable dtSumme = new DataTable("Summe");

        public docManRGGS()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm);

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        //
        //----- Initialisierung Doc --------------
        //
        public void InitDocManRGGS(DataSet ds)
        {
            //DataSet aus docRechnung
            dsRG = ds.Copy();

            //fremde details aus docBriefkopf und docRechnung ausblenden
            detail_RG.Dispose();
            bk_Details.Dispose();

            //Rechnungsempfänger
            SetRGEmpfaenger();

            //Dokument und Nr
            SetDocNameAndNr();

            //Rechnungsdaten Positionen
            SetManRGGSPositionen();

            //Kundennummer
            tbKDNummer.Value = "Konto-Nr.: ";
            decimal KDNr = 0;
            KDNr = (decimal)dsRG.Tables["Auftragsdaten"].Rows[0]["Fracht_ADR"];
            KDNr = clsADR.GetKD_IDByID(KDNr);
            tbKDNr.Value = KDNr.ToString();

            //Liste Summe
            SetManRGGSSumme();

            //Update RGNr. in Frachten
            SetRGDaten();
        }
        /*****************************************************************
        *                 Table Rechnungspos
        ****************************************************************/
        //
        //
        private void SetColumnsTablePos()
        {
            dtPos.Columns.Add("Pos", typeof(decimal));
            dtPos.Columns.Add("Fracht", typeof(decimal));
            dtPos.Columns.Add("Frachttext", typeof(string));
        }
        /*******************************************************************
         *                Table Summe
         ******************************************************************/
        //
        //
        //
        private void SetColumnsTableSumme()
        {
            dtSumme.Columns.Add("Netto", typeof(decimal));
            dtSumme.Columns.Add("MwStP", typeof(decimal));
            dtSumme.Columns.Add("MwStEuro", typeof(decimal));
            dtSumme.Columns.Add("Brutto", typeof(decimal));
        }
        //
        //
        //
        private void SetManRGGSPositionen()
        {
            SetColumnsTablePos();

            for (Int32 i = 0; i <= dsRG.Tables["Auftragsdaten"].Rows.Count - 1; i++)
            {
                DataRow row;
                row = dtPos.NewRow();
                row["Pos"] = i + 1;
                row["Fracht"] = (decimal)dsRG.Tables["Auftragsdaten"].Rows[i]["Fracht"];
                row["Frachttext"] = (string)dsRG.Tables["Auftragsdaten"].Rows[i]["Frachttext"];
                dtPos.Rows.Add(row);
            }
            dsRG.Tables.Add(dtPos);

        }
        //
        //
        //
        private void SetManRGGSSumme()
        {
            SetColumnsTableSumme();

            object obj1 = dtPos.Compute("SUM(Fracht)", "Fracht>0");

            decimal netto = (decimal)obj1;
            decimal mwstS = (decimal)dsRG.Tables["Auftragsdaten"].Rows[0]["MwStSatz"] / 100;
            decimal mwstEuro = netto * mwstS;
            decimal brutto = netto + mwstEuro;

            DataRow row1;
            row1 = dtSumme.NewRow();
            row1["Netto"] = netto;
            row1["MwStP"] = mwstS * 100;
            row1["MwStEuro"] = mwstEuro;
            row1["Brutto"] = brutto;
            dtSumme.Rows.Add(row1);
            dsRG.Tables.Add(dtSumme);
        }
        //
        //--------------- Zuweisung DATASOURCE -------------------
        //
        private void docManRGGS_NeedDataSource(object sender, EventArgs e)
        {
            RGPositionen.DataSource = dsRG.Tables["RGPositionen"];
            ListSumme.DataSource = dsRG.Tables["Summe"];
        }
        //
        //
        //
    }
}