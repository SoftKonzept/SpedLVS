using Common.Enumerations;
using LVS;
using LVS.Dokumente;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using Telerik.WinControls.Export;


namespace Sped4
{
    public partial class frmReportViewer : Sped4.frmTEMPLATE
    {
        public Globals._GL_SYSTEM GL_System;
        public Globals._GL_USER GL_User;
        public decimal _DocScanTableID;
        public decimal _AuftragID = 0;
        public decimal _AuftragPos;
        public decimal _ArtikelTableID;
        public decimal _AuftragTableID;
        public decimal _AuftragPosTableID;
        public decimal _MandantenID;
        DataSet dsFrachtauftrag;
        DataSet ds;
        string Dokumentenart = string.Empty;
        public bool neutralerLfs = false;
        public bool boFDocs = false;
        public DataTable dtArtikelDetails = new DataTable("Artikeldetails");
        public DataTable dtLfsDaten = new DataTable("LfsDaten");
        public DataTable dtAuftragsdaten = new DataTable("Auftragsdaten");
        public DataTable dtPrintdaten = new DataTable("Printdaten");
        public DataSet dsFDocs = new DataSet();

        //Für AuftragScan
        public string ImageArt = string.Empty;
        public Int32 PicNum;

        public frmFDocs _frmFDocs;

        docFrachtauftragSU docFrachtauftragSU;
        docLieferschein docLfs;
        docAuftragScan auftragScan;
        docAbholschein docAbholschein;
        docRechnung docRG;
        docManRGGS docManRGGS;
        fDocs_Holzrichter fDocs;

        //public frmReportViewer(decimal myDecAuftragPosID,decimal myDecArtikelID, DataSet _ds, string docArt)
        //public frmReportViewer()
        public frmReportViewer(DataSet _ds, string docArt)
        {
            InitializeComponent();
            boFDocs = false;
            Dokumentenart = docArt;
            ds = _ds;
        }
        //
        //
        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            if (_AuftragPosTableID > 0)
            {
                //InitTableIDs();
                GetArtikelDaten();
                SetPrintDatenToFrm();
                if (_AuftragID > 0)
                {
                    if (boFDocs)
                    {
                        InitFDocsPrint();
                        this.repView.RefreshReport();
                    }
                    else
                    {
                        InitReportView();
                        SetPageSetting();
                    }
                    this.repView.RefreshReport();
                }
            }
            else if (_ArtikelTableID == -1)
            {
                //Drucken Scan Image
                InitTableIDs();
                InitReportView();
                SetPageSetting();
                this.repView.RefreshReport();
            }
            else
            {
                this.Close();
            }
        }
        //
        private void InitTableIDs()
        {
            if (_AuftragPosTableID > 0)
            {
                clsAuftragPos Apos = new clsAuftragPos();
                Apos._GL_User = this.GL_User;
                Apos.ID = _AuftragPosTableID;
                Apos.Fill();
                _AuftragID = Apos.Auftrag_ID;
                _AuftragPos = Apos.AuftragPos;
            }
        }
        //
        private void GetArtikelDaten()
        {
            //Auftragnummer und AuftragPosNummer ermitteln
            /***
            DataTable dt = clsArtikel.GetAllArtikeldateDispoByID(this.GL_User,_ArtikelTableID);
            if (dt.Rows.Count > 0)
            {
                for(Int32 i=0; i<=dt.Rows.Count-1; i++)
                {
                    //hier können die Artikeldaten übernommen werden
                    _AuftragID=(decimal)dt.Rows[i]["ArtikelID"];
                    _AuftragPos = (decimal)dt.Rows[i]["AuftragPos"];
                }
            }
             * ***/
        }
        //
        private void SetPrintDatenToFrm()
        {
            for (Int32 i = 0; i <= dtPrintdaten.Rows.Count - 1; i++)
            {
                _AuftragID = (decimal)dtPrintdaten.Rows[i]["AuftragID"];
                _AuftragPos = (decimal)dtPrintdaten.Rows[i]["AuftragPos"];
                Dokumentenart = dtPrintdaten.Rows[i]["DocArt"].ToString();
            }
        }
        //
        //----------- Frachtauftrag an SU ---------------------
        //
        public void InitReportView()
        {
            decimal BenutzerID = GL_User.User_ID;
            string Beschreibung = string.Empty;
            switch (Dokumentenart)
            {
                case "FrachtauftragAnSU":
                    docFrachtauftragSU = new docFrachtauftragSU();
                    docFrachtauftragSU.GL_User = GL_User;
                    docFrachtauftragSU.SetKontaktDaten = true;
                    dsFrachtauftrag = ds;
                    docFrachtauftragSU.IntiDataSet(dsFrachtauftrag);
                    docFrachtauftragSU.DocumentName = "Frachtauftrag_" + _AuftragID + "_" + _AuftragPos;
                    this.Text = "Vergabe Frachtauftrag_" + _AuftragID + "_" + _AuftragPos;
                    this.repView.Report = docFrachtauftragSU;
                    AddFrachtauftragToAuftragScan();
                    Beschreibung = "Druck - Frachtauftrag für Subunternehmer Auftrag/Pos: " + _AuftragID + "/" + _AuftragPos + " gedruckt";
                    break;

                case "Lieferschein":
                    docLfs = new docLieferschein();
                    docLfs.GL_User_Lfs = GL_User;
                    docLfs.neutrLieferschein = neutralerLfs;
                    docLfs.AuftragPosTableID = _AuftragPosTableID;
                    docLfs.MandantenID = _MandantenID;
                    docLfs.dtPrintdaten = dtPrintdaten;
                    docLfs.dtArtikelDetails = dtArtikelDetails;
                    docLfs.GetDaten(_AuftragPosTableID);
                    //docLfs.GetDaten(_AuftragID, _AuftragPos);
                    docLfs.DocumentName = "Lieferschein_" + _AuftragID + "_" + _AuftragPos;
                    this.Text = "Lfs:" + _AuftragID + "_" + _AuftragPos;
                    this.repView.Report = docLfs;
                    AddLieferscheinToDocScan();
                    Beschreibung = "Druck - Lieferschein Auftrag/Pos: " + _AuftragID + "/" + _AuftragPos + " gedruckt";
                    break;

                case "OwnDoc":
                    docLfs = new docLieferschein();
                    docLfs.GL_User_Lfs = GL_User;
                    docLfs.neutrLieferschein = neutralerLfs;
                    docLfs.boOwnDoc = true;
                    docLfs.dtPrintdaten = dtPrintdaten;
                    docLfs.dtArtikelDetails = dtArtikelDetails;
                    docLfs.GetDaten(_AuftragPosTableID);
                    //docLfs.GetDaten(_AuftragID, _AuftragPos);
                    docLfs.DocumentName = "EigenesDokument" + _AuftragID + "_" + _AuftragPos;
                    this.Text = "LEigenesDokument:" + _AuftragID + "_" + _AuftragPos;
                    this.repView.Report = docLfs;
                    AddOwnDocToDocScan();
                    Beschreibung = "Druck - EigenesDokument Auftrag/Pos: " + _AuftragID + "/" + _AuftragPos + " gedruckt";
                    break;

                case "AuftragScan":
                    auftragScan = new docAuftragScan();
                    auftragScan.DocScanTableID = _DocScanTableID;
                    auftragScan.AuftragPosTableID = _AuftragPosTableID;
                    auftragScan.AuftragID = _AuftragID;
                    auftragScan.AuftragPos = _AuftragPos;
                    auftragScan.ImageArt = ImageArt;
                    auftragScan.PicNum = PicNum;
                    this.repView.Report = auftragScan;
                    break;

                case "Abholschein":
                    docAbholschein = new docAbholschein();
                    docAbholschein.GL_User = GL_User;
                    docAbholschein.dtPrintdaten = dtPrintdaten;
                    docAbholschein.dtArtikelDetails = dtArtikelDetails;
                    docAbholschein.SetAbholschein(_AuftragPosTableID);
                    //docAbholschein.SetAbholschein(_AuftragID, _AuftragPos);
                    docAbholschein.DocumentName = "Abholschein_" + _AuftragID + "_" + _AuftragPos;
                    this.Text = "Abholschein:" + _AuftragID + "_" + _AuftragPos;
                    this.repView.Report = docAbholschein;
                    Beschreibung = "Druck - Abholschein Auftrag/Pos: " + _AuftragID + "/" + _AuftragPos + " gedruckt";
                    AddAbholscheinToAuftragScan();
                    break;

                case "Anmeldeschein":
                    docAbholschein = new docAbholschein();
                    docAbholschein.GL_User = GL_User;
                    docAbholschein.IsAnmeldeschein = true;
                    docAbholschein.dtPrintdaten = dtPrintdaten;
                    docAbholschein.dtArtikelDetails = dtArtikelDetails;
                    docAbholschein.SetAbholschein(_AuftragPosTableID);
                    //docAbholschein.SetAbholschein(_AuftragID, _AuftragPos);
                    docAbholschein.DocumentName = "Anmeldeschein" + _AuftragID + "_" + _AuftragPos;
                    this.Text = "Anmeldeschein:" + _AuftragID + "_" + _AuftragPos;
                    this.repView.Report = docAbholschein;
                    Beschreibung = "Druck - Anmeldeschein Auftrag/Pos: " + _AuftragID + "/" + _AuftragPos + " gedruckt";
                    dtPrintdaten.Clear();
                    AddAbholscheinToAuftragScan();
                    break;

                case "Rechnung":
                    docRG = new docRechnung();
                    docRG.InitRechnung(ds);
                    docRG.DocumentName = "Rechnung für Auftrag_" + _AuftragID + "_" + _AuftragPos;
                    this.Text = "Rechnung:" + _AuftragID + "_" + _AuftragPos;
                    this.repView.Report = docRG;
                    Beschreibung = "Druck RG/GS: " + docRG.drRGnr + " - Datum: " + docRG.drRGDatum;
                    break;

                case "manuelleRGGS":
                    docManRGGS = new docManRGGS();
                    docManRGGS.InitDocManRGGS(ds);
                    docManRGGS.DocumentName = "Rechnung";
                    this.Text = "manuelle Rechnung";
                    this.repView.Report = docManRGGS;
                    Beschreibung = "Druck - manuelle RG/GS: " + docManRGGS.drRGnr + " - Datum: " + docManRGGS.drRGDatum; ;
                    break;
            }
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Druck.ToString(), Beschreibung);
            this.repView.Refresh();
        }
        //
        //-------------------- Page Settings ----------------
        //
        private void SetPageSetting()
        {
            switch (Dokumentenart)
            {
                case "FrachtauftragAnSU":
                    docFrachtauftragSU.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.0);
                    docFrachtauftragSU.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0.0);
                    docFrachtauftragSU.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(1.0);
                    docFrachtauftragSU.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(2.0);
                    break;

                case "Lieferschein":
                    docLfs.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.0);
                    docLfs.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0.0);
                    docLfs.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(1.0);
                    docLfs.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(2.0);
                    break;

                case "AuftragScan":
                    auftragScan.PageSettings.Margins.Bottom = Telerik.Reporting.Drawing.Unit.Cm(0.0);
                    auftragScan.PageSettings.Margins.Top = Telerik.Reporting.Drawing.Unit.Cm(0.0);
                    auftragScan.PageSettings.Margins.Right = Telerik.Reporting.Drawing.Unit.Cm(1.0);
                    auftragScan.PageSettings.Margins.Left = Telerik.Reporting.Drawing.Unit.Cm(2.0);
                    break;
            }
        }
        //
        //----------------- Report Frachtauftrag wird in DB gespeichert --------
        //
        private void AddFrachtauftragToAuftragScan()
        {
            string mimType = string.Empty;
            string extension = string.Empty;
            Encoding encoding = null;

            Hashtable deviceInfo = new Hashtable();
            deviceInfo["OutputFormat"] = "BMP";

            byte[] buffer = Telerik.Reporting.Processing.ReportProcessor.Render(
                                                                                "IMAGE",
                                                                                docFrachtauftragSU,
                                                                                deviceInfo,
                                                                                out mimType,
                                                                                out extension,
                                                                                out encoding
                                                                                );

            using (MemoryStream ms = new MemoryStream(buffer))
            {
                Image img = Image.FromStream(ms);

                clsDocScan lfs = new clsDocScan();
                lfs._GL_User = this.GL_User;
                lfs.m_dec_AuftragID = clsAuftrag.GetAuftragTableIDByAuftragPosTableID(this.GL_User, _AuftragPosTableID);
                //lfs.m_dec_AuftragID = _AuftragID;
                lfs.m_dec_AuftragPosTableID = _AuftragPosTableID;
                lfs.m_dec_LAusgangID = 0;
                lfs.m_dec_LEingangID = 0;
                lfs.m_i_picnum = 1 + (lfs.GetMaxPicNumByAuftrag());
                lfs.m_str_ImageArt = enumDokumentenArt.AuftragScan.ToString();
                lfs.AuftragImageIn = img;  // der Ausdruck
                lfs.AddDocScan();
                lfs.SaveScanDocToHDD();
            }
        }
        //
        //-------------- Print akt. der DB ------------------------
        //
        public void PrintDirect()
        {
            this.repView.RefreshReport();

            EventArgs e = new EventArgs();
            frmReportViewer_Load(this, e);

            // Obtain the settings of the default printer
            System.Drawing.Printing.PrinterSettings printerSettings
                = new System.Drawing.Printing.PrinterSettings();

            // The standard print controller comes with no UI
            System.Drawing.Printing.PrintController standardPrintController =
                new System.Drawing.Printing.StandardPrintController();

            // Print the report using the custom print controller
            Telerik.Reporting.Processing.ReportProcessor reportProcessor
                = new Telerik.Reporting.Processing.ReportProcessor();

            reportProcessor.PrintController = standardPrintController;

            //Baustelle fehler
            //reportProcessor.PrintReport(this.repView.Report, printerSettings);

        }
        //
        //
        //
        private void repView_Print(object sender, CancelEventArgs e)
        {
            if (_AuftragID > 0)
            {
                //decimal iAP_ID = clsAuftragPos.GetIDbyAuftragAndAuftragPos(this.GL_User,_AuftragID, _AuftragPos, _MandantenID, this.GL_User.sys_ArbeitsbereichID);
                decimal iAP_ID = clsAuftragPos.GetIDbyAuftragAndAuftragPos(this.GL_User, _AuftragID, _AuftragPos, _MandantenID, this.GL_System.sys_ArbeitsbereichID);
                if (clsLieferscheine.LieferscheinExist(iAP_ID))
                {
                    clsLieferscheine.UpdatePrint(iAP_ID);
                }
            }
        }
        //
        //------------- Export = Print akt. der DB -----------------
        //
        private void repView_Export(object sender, ExportEventArgs args)
        {
            if (_AuftragID > 0)
            {
                //decimal iAP_ID = clsAuftragPos.GetIDbyAuftragAndAuftragPos(this.GL_User,_AuftragID, _AuftragPos, _MandantenID, this.GL_User.sys_ArbeitsbereichID);
                decimal iAP_ID = clsAuftragPos.GetIDbyAuftragAndAuftragPos(this.GL_User, _AuftragID, _AuftragPos, _MandantenID, this.GL_System.sys_ArbeitsbereichID);
                if (clsLieferscheine.LieferscheinExist(iAP_ID))
                {
                    clsLieferscheine.UpdatePrint(iAP_ID);
                }
            }
        }
        //
        //
        //
        /****************************************************************************************************
         *                  Fremde Docs / Lieferschein
         * **************************************************************************************************/
        //
        private void InitFDocsPrint()
        {

            fDocs = new fDocs_Holzrichter();
            fDocs.GL_User = GL_User;
            fDocs.dtArtikelDetails = dtArtikelDetails;
            fDocs.dtLfsDaten = dtLfsDaten;
            fDocs.dtPrintdaten = dtPrintdaten;
            fDocs.InitDoc();
            fDocs.DocumentName = "Lieferschein_" + _AuftragID + "_" + _AuftragPos;
            this.Text = "Lfs:" + _AuftragID + "_" + _AuftragPos;
            this.repView.Report = fDocs;
            AddFremdLieferscheinToDocScan();
        }
        //
        //neu
        private void AddFremdLieferscheinToDocScan()
        {
            string mimType = string.Empty;
            string extension = string.Empty;
            Encoding encoding = null;

            Hashtable deviceInfo = new Hashtable();
            deviceInfo["OutputFormat"] = "BMP";

            byte[] buffer = Telerik.Reporting.Processing.ReportProcessor.Render(
                                                                                "IMAGE",
                                                                                fDocs,
                                                                                deviceInfo,
                                                                                out mimType,
                                                                                out extension,
                                                                                out encoding
                                                                                );

            using (MemoryStream ms = new MemoryStream(buffer))
            {
                Image img = Image.FromStream(ms);
                clsDocScan lfs = new clsDocScan();
                lfs._GL_User = this.GL_User;
                //lfs.m_dec_AuftragID = clsAuftrag.GetAuftragTableIDByAuftragPosTableID(this.GL_User, _AuftragPosTableID);
                lfs.m_dec_AuftragPosTableID = _AuftragPosTableID;
                lfs.m_dec_AuftragID = _AuftragID;
                lfs.m_dec_LAusgangID = 0;
                lfs.m_dec_LEingangID = 0;
                lfs.m_i_picnum = 1 + (lfs.GetMaxPicNumByAuftrag());
                lfs.m_str_ImageArt = enumDokumentenArt.Fremdlieferschein.ToString();
                lfs.AuftragImageIn = img;  // der Ausdruck
                lfs.AddDocScan();
                lfs.SaveScanDocToHDD();
            }
        }
        //
        //----------------- Report Frachtauftrag wird in DB gespeichert --------
        //
        private void AddAbholscheinToAuftragScan()
        {
            string mimType = string.Empty;
            string extension = string.Empty;
            Encoding encoding = null;

            Hashtable deviceInfo = new Hashtable();
            deviceInfo["OutputFormat"] = "BMP";

            byte[] buffer = Telerik.Reporting.Processing.ReportProcessor.Render(
                                                                                "IMAGE",
                                                                                docAbholschein,
                                                                                deviceInfo,
                                                                                out mimType,
                                                                                out extension,
                                                                                out encoding
                                                                                );

            using (MemoryStream ms = new MemoryStream(buffer))
            {
                Image img = Image.FromStream(ms);

                clsDocScan lfs = new clsDocScan();
                lfs._GL_User = this.GL_User;
                lfs.m_dec_AuftragID = clsAuftrag.GetAuftragTableIDByAuftragPosTableID(this.GL_User, _AuftragPosTableID);
                lfs.m_dec_LAusgangID = 0;
                lfs.m_dec_LEingangID = 0;
                lfs.m_dec_AuftragPosTableID = _AuftragPosTableID;
                lfs.m_i_picnum = 1 + (lfs.GetMaxPicNumByAuftrag());
                lfs.m_str_ImageArt = enumDokumentenArt.Abholschein.ToString();
                lfs.AuftragImageIn = img;  // der Ausdruck
                lfs.AddDocScan();
                lfs.SaveScanDocToHDD();
            }
        }
        //
        //----------------- Report Frachtauftrag wird in DB gespeichert --------
        //
        private void AddLieferscheinToDocScan()
        {
            string mimType = string.Empty;
            string extension = string.Empty;
            Encoding encoding = null;

            Hashtable deviceInfo = new Hashtable();
            deviceInfo["OutputFormat"] = "BMP";

            byte[] buffer = Telerik.Reporting.Processing.ReportProcessor.Render(
                                                                                "IMAGE",
                                                                                docLfs,
                                                                                deviceInfo,
                                                                                out mimType,
                                                                                out extension,
                                                                                out encoding
                                                                                );

            using (MemoryStream ms = new MemoryStream(buffer))
            {
                Image img = Image.FromStream(ms);

                clsDocScan lfs = new clsDocScan();
                lfs._GL_User = this.GL_User;
                lfs.m_dec_AuftragID = clsAuftrag.GetAuftragTableIDByAuftragPosTableID(this.GL_User, _AuftragPosTableID);
                lfs.m_dec_LAusgangID = 0;
                lfs.m_dec_LEingangID = 0;
                lfs.m_dec_AuftragPosTableID = _AuftragPosTableID;
                lfs.m_i_picnum = 1 + (lfs.GetMaxPicNumByAuftrag());
                lfs.m_str_ImageArt = enumDokumentenArt.Lieferschein.ToString();
                lfs.AuftragImageIn = img;  // der Ausdruck
                lfs.AddDocScan();
                lfs.SaveScanDocToHDD();
            }
        }
        //
        //----------------- Report Frachtauftrag wird in DB gespeichert --------
        //
        private void AddOwnDocToDocScan()
        {
            string mimType = string.Empty;
            string extension = string.Empty;
            Encoding encoding = null;

            Hashtable deviceInfo = new Hashtable();
            deviceInfo["OutputFormat"] = "BMP";

            byte[] buffer = Telerik.Reporting.Processing.ReportProcessor.Render(
                                                                                "IMAGE",
                                                                                docLfs,
                                                                                deviceInfo,
                                                                                out mimType,
                                                                                out extension,
                                                                                out encoding
                                                                                );

            using (MemoryStream ms = new MemoryStream(buffer))
            {
                Image img = Image.FromStream(ms);

                clsDocScan lfs = new clsDocScan();
                lfs._GL_User = this.GL_User;
                lfs.m_dec_AuftragID = clsAuftrag.GetAuftragTableIDByAuftragPosTableID(this.GL_User, _AuftragPosTableID);
                lfs.m_dec_LAusgangID = 0;
                lfs.m_dec_LEingangID = 0;
                lfs.m_dec_AuftragPosTableID = _AuftragPosTableID;
                lfs.m_i_picnum = 1 + (lfs.GetMaxPicNumByAuftrag());
                lfs.m_str_ImageArt = enumDokumentenArt.OwnDoc.ToString();
                lfs.AuftragImageIn = img;  // der Ausdruck
                lfs.AddDocScan();
                lfs.SaveScanDocToHDD();
            }
        }
    }
}
