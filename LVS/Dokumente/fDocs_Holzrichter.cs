namespace LVS.Dokumente
{
    using LVS;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting.Drawing;


    /// <summary>
    /// Summary description for fDocs_Holzrichter.
    /// </summary>
    public partial class fDocs_Holzrichter : Telerik.Reporting.Report
    {
        public Globals._GL_USER GL_User;
        //public DataTable dtAuftragsdaten=new DataTable("Auftragsdaten");
        //public DataTable dtDetails=new DataTable("Auftragsdetails");
        public DataTable dtArtikel = new DataTable("Artikel");
        //public DataTable dtFLfsDaten = new DataTable("FLfsDaten");
        internal clsLieferscheine Lfs;

        public DataTable dtLfsDaten = new DataTable("LfsDaten");
        public DataTable dtArtikelDetails = new DataTable("Artikeldetails");
        public DataTable dtAuftragsdaten = new DataTable("Auftragsdaten");
        public DataTable dtPrintdaten = new DataTable("Printdaten");

        public fDocs_Holzrichter()
        {
            InitializeComponent();
        }
        //
        //
        //
        public void InitDoc()
        {
            Lfs = new clsLieferscheine();
            Lfs._GL_User = this.GL_User;
            SetArtikelColumnHolzrichter(ref dtArtikelDetails);
            Lfs.dtArtikelDetails = dtArtikelDetails;
            Lfs.dtPrintdaten = dtPrintdaten;
            Lfs.SetPrintDaten();
            Lfs.GetLieferscheinDaten();

            //Briefkopf
            InitBriefkopf();
            InitRessourcen();

            //Fuss Briefbogen
            InitFussBK();

            dtArtikel = Lfs.dsLieferschein.Tables["Artikel"];
        }
        //
        private void InitBriefkopf()
        {
            //Logo Holzrichter
            string LogoPfad = Application.StartupPath + "\\Heisiep\\Logo_Holzrichter.bmp";
            pbLogo.Value = Image.FromFile(LogoPfad);
            pbLogo.Sizing = ImageSizeMode.ScaleProportional;

            //ADR
            Lfs.SetPrintDaten();
            Lfs.GetLieferscheinDaten();
            tbEFirma1.Value = Lfs.EName1;
            tbEFirma2.Value = Lfs.EName2;
            tbEStr.Value = Lfs.EStr;
            tbEPLZ.Value = Lfs.EPLZ;
            tbEOrt.Value = Lfs.EOrt;
            SetValuedtFLfsDaten();
        }
        //
        private void InitRessourcen()
        {
            tbZM.Value = "Zugmaschine: " + Lfs.ZM;
            tbAuflieger.Value = "Auflieger: " + Lfs.Auflieger;
            tbFahrer.Value = "Fahrer: " + Lfs.Fahrer;
        }
        //
        private void InitFussBK()
        {
            //FUss Holzrichter
            string FussPfad = Application.StartupPath + "\\Heisiep\\Fuss_Holzrichter.bmp";
            pbFussBK.Value = Image.FromFile(FussPfad);
            pbFussBK.Sizing = ImageSizeMode.Stretch;
        }
        //
        private void dtArtikelliste_NeedDataSource(object sender, EventArgs e)
        {
            if (dtArtikel.Rows.Count > 0)
            {
                dtArtikelliste.DataSource = dtArtikel;
            }
        }
        //
        private void SetValuedtFLfsDaten()
        {
            for (Int32 i = 0; i <= dtLfsDaten.Rows.Count - 1; i++)
            {
                tbAbruf.Value = dtLfsDaten.Rows[i]["Abruf"].ToString();
                tbDate.Value = dtLfsDaten.Rows[i]["Datum"].ToString();
                tbBestellNr.Value = dtLfsDaten.Rows[i]["BestellNr"].ToString();
                tbIhrZeichen.Value = dtLfsDaten.Rows[i]["IhrZeichen"].ToString();
                tbLfsNr.Value = dtLfsDaten.Rows[i]["LfsNr"].ToString();
                tbLfsDate.Value = dtLfsDaten.Rows[i]["Lieferscheindatum"].ToString();
            }
        }
        //
        public static void SetArtikelColumnHolzrichter(ref DataTable dtDetails)
        {
            //die Standardspalten sollen nicht 
            /********************************************
             * Standardausgabe für Holzrichter
             * - Artikel
             *          Werksnummer 
             * - Bezeichnung 
             *          GArt
             *          Abmessungen
             * - Anzahl (ME)
             * - Netto
             * - Brutto
             * ******************************************/

            for (Int32 i = 0; i <= dtDetails.Rows.Count - 1; i++)
            {
                String strColumn = string.Empty;
                strColumn = dtDetails.Rows[i]["Text"].ToString();
                if (
                    //raus Holzrichter


                    //Standart
                    (strColumn == "Werksnummer") |
                    (strColumn == "ME") |
                    (strColumn == "gemGewicht") |
                    (strColumn == "Netto") |
                    //(strColumn == "Breite") |
                    //(strColumn == "Länge") |
                    //(strColumn == "Höhe") |
                    (strColumn == "Brutto") |
                    //(strColumn == "gemGewicht") |
                    (strColumn == "AuftragID") |
                    (strColumn == "AuftragPos") |
                    (strColumn == "ID") |
                    (strColumn == "Einheit") |
                    (strColumn == "LVS_ID") |
                    (strColumn == "Schaden") |
                    (strColumn == "Schadensbeschreibung") |
                    (strColumn == "Werk") |
                    (strColumn == "Halle") |
                    (strColumn == "Reihe") |
                    (strColumn == "Platz")
                    )
                {
                    dtDetails.Rows[i]["Standard"] = true;
                }
                else
                {
                    dtDetails.Rows[i]["Standard"] = false;
                }
                //dtDetails.Rows[i]["Selected"] = true;
            }
        }
        //
        //
        //



    }
}