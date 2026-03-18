namespace LVS.Dokumente
{
    using LVS;
    using System;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for docLieferschein.
    /// </summary>
    public partial class docLieferschein : Telerik.Reporting.Report
    {
        internal clsLieferscheine Lfs;
        decimal AuftragID;
        decimal AuftragPos;
        public decimal MandantenID;
        public decimal AuftragPosTableID;

        public bool IsLieferschein = true;
        public Globals._GL_USER GL_User_Lfs;
        public DataTable dtPrintdaten = new DataTable("Printdaten");
        public DataTable dtArtikelDetails = new DataTable("Artikeldetails");
        public bool neutrLieferschein = false;
        public bool boOwnDoc = false;

        public docLieferschein()
        {
            InitializeComponent();
        }
        //
        //
        //
        public void GetDaten(decimal myAuftragPosTableID)
        {
            AuftragPosTableID = myAuftragPosTableID;

            Lfs = new clsLieferscheine();
            Lfs.AuftragID = clsAuftragPos.GetAuftragIDByID(AuftragPosTableID);
            Lfs.AuftragPos = clsAuftragPos.GetAuftragPosByID(AuftragPosTableID);
            Lfs.AuftragPosTableID = AuftragPosTableID;
            Lfs.MandantenID = MandantenID;
            Lfs.neutrLieferschein = neutrLieferschein;
            Lfs.boOwnDoc = boOwnDoc;
            Lfs.dtArtikelDetails = dtArtikelDetails;
            Lfs.dtPrintdaten = dtPrintdaten;
            Lfs.SetPrintDaten();
            Lfs.GetLieferscheinDaten();


            //Wenn eigenes Dokument soll die Unterschrift ausgeblendet werden
            if (boOwnDoc)
            {
                dtUnterschrift.Visible = false;
            }
            else
            {
                dtUnterschrift.Visible = true;
            }

            //Briefkopf
            InitBriefkopf();
            //Versender
            InitVersender();
            //Empfänger
            InitEmpfaenger();
            //Kommission/Ressourcen
            InitRessourcen();

            //Lieferscheinnummer
            tbLieferscheinNr.Value = Lfs.LieferscheinNr;
            //Auftrag
            tbAuftrag.Value = AuftragID.ToString() + "/" + AuftragPos.ToString();
        }
        //**********************************************************************************************
        //
        //--------------- Initialisierung Briefkopf / Fussdaten --------------------
        //
        public void InitBriefkopf()
        {

            clsBriefkopfdaten bk = new clsBriefkopfdaten();
            bk.InitBriefkopfDaten();
            //Absender

            //Briefkopfadresse
            tbBKFirma1.Value = bk.Name1BK;
            tbBKFirma2.Value = bk.Name2BK;
            tbBKStr.Value = bk.StrBK;
            //tbADRzHd.Value über Transportdaten
            tbBKPLZOrt.Value = bk.PLZBK + " - " + bk.OrtBK;
            tbBKUST.Value = bk.USTBK;
            tbBKSteuer.Value = bk.SteuerBK;
            tbTelefon.Value = bk.Telefon;
            tbFax.Value = bk.Fax;

            //InitAnsprechpartner();

            SetPrintDatenValue();

            //Ort Datum
            if (IsLieferschein)
            {
                if (Lfs.LfsExist)
                {
                    DateTime date = Lfs.GetLieferscheinDatum();
                    tbOrtDatum.Value = bk.OrtBK + ", " + date.ToShortDateString();
                }
                else
                {
                    tbOrtDatum.Value = bk.OrtBK + ", " + DateTime.Today.Date.ToShortDateString();
                }
            }
            else
            {
                tbOrtDatum.Value = bk.OrtBK + ", " + DateTime.Today.Date.ToShortDateString();
            }

            //LOGO
            if (bk.LogoPfad != null)
            {
                if (File.Exists(@bk.LogoPfad))
                {
                    pbLogo.Value = Image.FromFile(bk.LogoPfad);
                    pbLogo.Sizing = ImageSizeMode.ScaleProportional;
                }
            }
            if (bk.ZertPfad != null)
            {
                if (File.Exists(@bk.ZertPfad))
                {
                    pbZert.Value = Image.FromFile(bk.ZertPfad);
                    pbZert.Sizing = ImageSizeMode.ScaleProportional;
                }
            }

            tbHR.Value = bk.HR;

            if ((IsLieferschein) | (tbDocName.Value == "ABHOLSCHEIN"))
            {
                tbText.Value = bk.TextLieferschein;
            }
            else
            {
                tbText.Value = bk.Text;
            }
        }

        //
        //
        //
        public void SetPrintDatenValue()
        {
            tbZM.Value = Lfs.ZM;
            tbAuflieger.Value = Lfs.Auflieger;
            tbFahrer.Value = Lfs.Fahrer;
            tbDocName.Value = Lfs.DocName;
            tbLadenummer.Value = Lfs.Ladenummer;
            tbZF.Value = Lfs.ZF;
            tbNotiz.Value = Lfs.Notiz;
        }
        //
        //------ Versender--------
        //

        public void InitVersender()
        {
            tbVName1.Value = Lfs.VName1;
            tbVStr.Value = Lfs.VName2;
            tbVName2.Value = Lfs.VName3;
            tbVName3.Value = Lfs.VStr;
            tbVPLZ.Value = Lfs.VPLZ;
            tbVOrt.Value = Lfs.VOrt;
        }
        //
        //--- Empfänger ---------------
        //
        public void InitEmpfaenger()
        {
            tbEName1.Value = Lfs.EName1;
            tbEName2.Value = Lfs.EName2;
            tbEName3.Value = Lfs.EName3;
            tbEStr.Value = Lfs.EStr;
            tbEPLZ.Value = Lfs.EPLZ;
            tbEOrt.Value = Lfs.EOrt;
        }
        //
        //
        //
        public void InitRessourcen()
        {
            tbZM.Value = Lfs.ZM;
            tbAuflieger.Value = Lfs.Auflieger;
            tbFahrer.Value = Lfs.Fahrer;
        }
        //
        //
        //
        public void docLieferschein_NeedDataSource(object sender, EventArgs e)
        {
            if (IsLieferschein)
            {
                DataTable dataTable = Lfs.dsLieferschein.Tables["Artikel"];
                if (dataTable.Columns["Gewicht"] == null)
                {
                    if (dataTable.Columns["Brutto"] != null)
                    {
                        dataTable.Columns["Brutto"].ColumnName = "Gewicht";   //Spalte gem Gewicht wird umbenannt
                    }
                }
                dtArtikel.DataSource = dataTable;
            }
        }
        //
        //
        //
        public void InitAnsprechpartner()
        {
            //tbAnsprechpartner.Value = GL_User_Lfs.Name + ", " + GL_User_Lfs.Vorname;
            //tbTel.Value = "Tel.: " + GL_User_Lfs.Telefon;
            //tbFax.Value = "Fax: " + GL_User_Lfs.Fax;
            //tbMail.Value = "E-Mail: " + GL_User_Lfs.Mail;
        }

    }
}