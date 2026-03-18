using Common.Enumerations;
using Common.Models;
using LVS;
using LVS.Constants;
using LVS.Dokumente;
using LVS.IniValuePrinter;
using LVS.Print;
using LVS.ViewData;
using System;
using System.Windows.Forms;
using Telerik.Reporting;

namespace Sped4
{
    public partial class frmPrintRepViewer : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public Globals._GL_SYSTEM GL_System;
        internal clsSystem sys;
        clsINI.clsINI INI_Rep;
        public ctrPrintLager _ctrPrintLager;
        public ctrPrintLagerOrtLabel _ctrPrintLagerOrtLabel;
        public ctrReihen _ctrReihen;
        public ctrBestand _ctrBestand;
        public ctrJournal _ctrJournal;
        public ctrFaktLager _ctrFaktLager;
        public ctrPostCenter _ctrPostCenter;
        public ctrLieferEinteilung _ctrLieferEinteilungen;
        public ctrADR_List _ctrADRList;
        public ctrSPLAdd _SPLAdd;
        internal clsLager LagerP;
        internal clsDocScan DocScan;
        internal decimal decAdrID = 0;

        //internal docBestandsliste _docBestandsliste;
        //internal docJournal _docJournal;
        internal docLagerRechnung _docLagerRG;
        internal ctrRGList _ctrRGList;
        internal ctrRGManuell _ctrRGManuell;

        internal string DokumentenArt;
        internal string DocPath;
        internal string PaperSource;
        internal string PrinterName;
        //Lagerdokumente
        internal Int32 iPrintCount = 0;
        internal InstanceReportSource repSource;
        internal int iRGTableId = 0;

        //--for new Print
        public UriReportSource uRepSource;
        public clsReportDocSetting ReportDocSettingCurrent;
        public ArchiveViewData aViewData = new ArchiveViewData();
        public enumPrintDocumentArt PrintDocumentArt { get; set; } = enumPrintDocumentArt.NotSet;

        internal bool bPrintFromForm;
        public string ErrorText { get; set; }
        public frmPrintRepViewer()
        {
            InitializeComponent();
        }
        ///<summary>frmPrintRepView / frmPrintRepView_Load</summary>
        ///<remarks></remarks>
        private void frmPrintRepView_Load(object sender, EventArgs e)
        {
        }
        ///<summary>frmPrintRepView / InitFrm</summary>
        ///<remarks></remarks>
        public void InitFrm(bool bFromForm = false)
        {
            iRGTableId = 0;
            aViewData = new ArchiveViewData();

            bPrintFromForm = bFromForm;
            //Check auf Lager oder Spedition ?
            if (this._ctrPrintLager != null)
            {
                //Druckanzahl festlegen
                iPrintCount = (Int32)this._ctrPrintLager.nudPrintCount.Value;
                //Class Lager erstellen
                LagerP = new clsLager();
                //LAGER
                if (this._ctrPrintLager._ctrEinlagerung != null)
                {
                    this.Text = "Lagerverwaltung - Dokumentenerstellung - Einlagerung ";
                    LagerP = this._ctrPrintLager._ctrEinlagerung.Lager;
                    decAdrID = LagerP.Eingang.Auftraggeber;

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();

                }
                if (this._ctrPrintLager._ctrAuslagerung != null)
                {
                    this.Text = "Lagerverwaltung - Dokumentenerstellung - Auslagerung ";
                    LagerP = this._ctrPrintLager._ctrAuslagerung.Lager;
                    decAdrID = LagerP.Ausgang.Auftraggeber;

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                }
                this.sys = this._ctrPrintLager._ctrMenu._frmMain.system;
                if (this.sys.Client.Modul.Print_OldVersion)
                {
                    DokumentenArt = this._ctrPrintLager._DokumentenArt;
                    DocPath = this._ctrPrintLager._DocPath;
                }
                else
                {
                    if (this._ctrPrintLager.RepDocSettings is clsReportDocSetting)
                    {
                        DokumentenArt = this._ctrPrintLager.RepDocSettings.DocKey;
                        //DocPath = this._ctrPrintLager.RepDocSettings.DocPath;
                        DocPath = this._ctrPrintLager.RepDocSettings.DocFileNameAndPath;
                        this.PrinterName = this._ctrPrintLager.RepDocSettings.PrinterName;
                        this.PaperSource = this._ctrPrintLager.RepDocSettings.PaperSource;
                        this.iPrintCount = this._ctrPrintLager.RepDocSettings.PrintCount;

                        ReportDocSettingCurrent = this._ctrPrintLager.RepDocSettings;
                    }
                }
                InitReportView();
            }
            if (this._ctrBestand != null)
            {
                sys = this._ctrBestand._ctrMenu._frmMain.system;
                this.Text = "Lagerverwaltung - Dokumentenerstellung - Bestände";
                //Druckanzahl festlegen
                iPrintCount = 1;
                if (!this.sys.Client.Modul.Print_OldVersion)
                {
                    if (this._ctrBestand.ReportDocSetting is clsReportDocSetting)
                    {
                        DokumentenArt = this._ctrBestand.ReportDocSetting.DocKey;
                        DocPath = this._ctrBestand.ReportDocSetting.DocFileNameAndPath;
                        this.PrinterName = this._ctrBestand.ReportDocSetting.PrinterName;
                        this.PaperSource = this._ctrBestand.ReportDocSetting.PaperSource;
                        this.iPrintCount = this._ctrBestand.ReportDocSetting.PrintCount;

                        ReportDocSettingCurrent = this._ctrBestand.ReportDocSetting;
                    }
                }
                else
                {
                    DokumentenArt = enumIniDocKey.Bestandsliste.ToString();
                }
                InitReportView();
            }
            if (this._ctrJournal != null)
            {
                sys = this._ctrJournal._ctrMenu._frmMain.system;
                this.Text = "Lagerverwaltung - Dokumentenerstellung - Journal";
                //Druckanzahl festlegen
                iPrintCount = 1;
                DokumentenArt = enumDokumentenArt.Journal.ToString();
                InitReportView();
            }
            if (this._ctrFaktLager != null)
            {
                sys = this._ctrFaktLager._ctrMenu._frmMain.system;
                this.Text = "Lagerverwaltung - Dokumentenerstellung - Fakturierung";
                this.sys = this._ctrFaktLager._ctrMenu._frmMain.system;
                decAdrID = this._ctrFaktLager.FaktLager.Rechnung.Auftraggeber;
                iRGTableId = (int)this._ctrFaktLager.FaktLager.Rechnung.ID;

                //Druckanzahl festlegen
                if (this.sys.Client.Modul.Print_OldVersion)
                {
                    this.sys.CustomizeDocPath(ref this.GL_System, decAdrID);
                    this.iPrintCount = 1;
                    DokumentenArt = this._ctrFaktLager.DokumentenArt;
                    //DokumentenArt = this._ctrFaktLager.DokumentenArt;
                    //DocPath = this._ctrFaktLager.;
                }
                else
                {
                    if (this._ctrFaktLager.FaktLager.RepDocSettings is clsReportDocSetting)
                    {
                        DokumentenArt = this._ctrFaktLager.FaktLager.RepDocSettings.DocKey;
                        //DocPath = this._ctrPrintLager.RepDocSettings.DocPath;
                        DocPath = this._ctrFaktLager.FaktLager.RepDocSettings.DocFileNameAndPath;
                        this.PrinterName = this._ctrFaktLager.FaktLager.RepDocSettings.PrinterName;
                        this.PaperSource = this._ctrFaktLager.FaktLager.RepDocSettings.PaperSource;
                        this.iPrintCount = this._ctrFaktLager.FaktLager.RepDocSettings.PrintCount;

                        ReportDocSettingCurrent = this._ctrFaktLager.FaktLager.RepDocSettings;
                        iRGTableId = (int)this._ctrFaktLager.FaktLager.Rechnung.ID;
                    }
                }
                InitReportView();
            }
            if (this._ctrRGList != null)
            {
                sys = this._ctrRGList._ctrMenu._frmMain.system;
                this.Text = "Rechnungsdruck";
                //Druckanzahl festlegen
                if (this.sys.Client.Modul.Print_OldVersion)
                {
                    iPrintCount = 1;
                    DokumentenArt = this._ctrRGList.DokumentenArt;
                    decAdrID = this._ctrRGList.FaktLager.Rechnung.Auftraggeber;
                    iRGTableId = (int)this._ctrFaktLager.FaktLager.Rechnung.ID;

                    this.sys.CustomizeDocPath(ref this.GL_System, decAdrID);
                    this.sys = this._ctrRGList._ctrMenu._frmMain.system;
                    //DokumentenArt = this._ctrFaktLager.DokumentenArt;
                    //DocPath = this._ctrPrintLager._DocPath;
                }
                else
                {
                    if (this._ctrRGList.FaktLager.RepDocSettings is clsReportDocSetting)
                    {
                        DokumentenArt = this._ctrRGList.FaktLager.RepDocSettings.DocKey;
                        //DocPath = this._ctrPrintLager.RepDocSettings.DocPath;
                        DocPath = this._ctrRGList.FaktLager.RepDocSettings.DocFileNameAndPath;
                        this.PrinterName = this._ctrRGList.FaktLager.RepDocSettings.PrinterName;
                        this.PaperSource = this._ctrRGList.FaktLager.RepDocSettings.PaperSource;
                        this.iPrintCount = this._ctrRGList.FaktLager.RepDocSettings.PrintCount;

                        ReportDocSettingCurrent = this._ctrRGList.FaktLager.RepDocSettings;

                        iRGTableId = (int)this._ctrRGList.FaktLager.Rechnung.ID;
                    }
                }
                InitReportView();
            }
            if (this._ctrRGManuell != null)
            {
                sys = this._ctrRGManuell._ctrMenu._frmMain.system;
                this.Text = "Manuelle Rechnungsdruck";
                //Druckanzahl festlegen
                iPrintCount = 1;
                if (this.sys.Client.Modul.Print_OldVersion)
                {
                    iPrintCount = 1;
                    DokumentenArt = this._ctrRGManuell.DokumentenArt;
                    this.sys = this._ctrRGManuell._ctrMenu._frmMain.system;
                    decAdrID = this._ctrRGManuell.Rechnung.Auftraggeber;
                    iRGTableId = (int)this._ctrFaktLager.FaktLager.Rechnung.ID;
                    this.sys.CustomizeDocPath(ref this.GL_System, decAdrID);
                }
                else
                {
                    if (this._ctrRGManuell.RepDocSettings is clsReportDocSetting)
                    {
                        DokumentenArt = this._ctrRGManuell.RepDocSettings.DocKey;
                        DocPath = this._ctrRGManuell.RepDocSettings.DocFileNameAndPath;
                        this.PrinterName = this._ctrRGManuell.RepDocSettings.PrinterName;
                        this.PaperSource = this._ctrRGManuell.RepDocSettings.PaperSource;
                        this.iPrintCount = this._ctrRGManuell.RepDocSettings.PrintCount;

                        ReportDocSettingCurrent = this._ctrRGManuell.RepDocSettings;
                        iRGTableId = (int)this._ctrRGManuell.Rechnung.ID; //-- FaktLager.Rechnung.ID;
                    }
                }
                InitReportView();
            }
            if (this._ctrADRList != null)
            {
                sys = this._ctrADRList._ctrMenu._frmMain.system;
                this.Text = "Adressliste";
                //Druckanzahl festlegen
                iPrintCount = 1;
                DokumentenArt = this.DokumentenArt;


                if (this._ctrRGManuell.RepDocSettings is clsReportDocSetting)
                {
                    DokumentenArt = this._ctrRGManuell.RepDocSettings.DocKey;
                    DocPath = this._ctrRGManuell.RepDocSettings.DocFileNameAndPath;
                    this.PrinterName = this._ctrRGManuell.RepDocSettings.PrinterName;
                    this.PaperSource = this._ctrRGManuell.RepDocSettings.PaperSource;
                    this.iPrintCount = this._ctrRGManuell.RepDocSettings.PrintCount;

                    ReportDocSettingCurrent = this._ctrRGManuell.RepDocSettings;
                }

                InitReportView();
            }
            if (this._ctrLieferEinteilungen != null)
            {
                sys = this._ctrLieferEinteilungen._ctrMenu._frmMain.system;
                this.Text = "Liefereinteilungen";
                iPrintCount = 1;
                InitReportView();
            }
            if (this._ctrReihen != null)
            {
                sys = this._ctrReihen._ctrMenu._frmMain.system;
                this.Text = "LagerOrtLabel Druck";
                if (this.sys.Client.Modul.Print_OldVersion)
                {
                    DocPath = this.GL_System.docPath_LOLabelLinks;
                }
                else
                {
                    //TODO: Pfad aus der DB holen
                }
                DokumentenArt = "LOLabel";
                iPrintCount = 1;
                InitReportView();
            }
            if (this._ctrPrintLagerOrtLabel != null)
            {
                sys = this._ctrPrintLagerOrtLabel._ctrMenu._frmMain.system;
                this.Text = "LagerOrtLabel Druck";

                if (this._ctrPrintLagerOrtLabel.RepDocSettings is clsReportDocSetting)
                {
                    DocPath = this._ctrPrintLagerOrtLabel.DocFileNameAndPath;

                    DokumentenArt = this._ctrPrintLagerOrtLabel.RepDocSettings.DocKey;

                    iPrintCount = 1;
                    InitReportView();
                }

                //if (this.sys.Client.Modul.Print_OldVersion)
                //{
                //    DocPath = this.GL_System.docPath_LOLabelLinks;
                //}
                //else
                //{
                //    DocPath = this._ctrPrintLagerOrtLabel.DocFileNameAndPath;
                //}
                //DokumentenArt = "LOLabel";
                //iPrintCount = 1;
                //InitReportView();
            }
            if (this._SPLAdd != null)
            {
                LagerP = new clsLager();
                this.LagerP = this._SPLAdd.Lager;
                sys = this._SPLAdd.ctrMenu._frmMain.system;

                this.Text = ctrSPLAdd.const_SPLAdd_DocNameSPL;
                if (this._SPLAdd.ctrMenu._frmMain.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SZG + "_"))
                {
                    this.Text = ctrSPLAdd.const_SPLAdd_DocNameSchaden;
                }

                iPrintCount = 1;
                if (this.sys.Client.Modul.Print_OldVersion)
                {
                    DokumentenArt = this._SPLAdd.DokumentenArt;
                    DocPath = this._SPLAdd.DocPath;
                }
                else
                {
                    if (this._SPLAdd.RepDocSettings is clsReportDocSetting)
                    {
                        DokumentenArt = this._SPLAdd.RepDocSettings.DocKey;
                        //DocPath = this._SPLAdd.RepDocSettings.DocPath;
                        DocPath = this._SPLAdd.RepDocSettings.DocFileNameAndPath;
                        this.PrinterName = this._SPLAdd.RepDocSettings.PrinterName;
                        this.PaperSource = this._SPLAdd.RepDocSettings.PaperSource;
                        this.iPrintCount = this._SPLAdd.RepDocSettings.PrintCount;

                        ReportDocSettingCurrent = this._SPLAdd.RepDocSettings;

                        aViewData.Archive.TableId = (int)LagerP.Artikel.ID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                    }
                }
                InitReportView();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void AddToArchiv()
        {
            string str = string.Empty;
            if (

                    (ReportDocSettingCurrent is clsReportDocSetting) &&
                    (ReportDocSettingCurrent.ID > 0) &&
                    (ReportDocSettingCurrent.RSAId > 0)
               )
            {
                string filePath = TelerikPrint.SaveReportFileToHDForUse(ReportDocSettingCurrent);

                if (
                     (aViewData is ArchiveViewData) &&
                     (aViewData.Archive is Archives) &&
                     (aViewData.Archive.TableId > 0)
                   )
                {
                    // - benötigte Daten (TableName, TableId, FileArt)
                    // -  TableName und TableId in InitClass
                    aViewData.Archive.FileArt = enumFileArt.PDF;
                    aViewData.Archive.ReportDocSettingId = ReportDocSettingCurrent.ID;
                    aViewData.Archive.ReportDocSettingAssignmentId = ReportDocSettingCurrent.RSAId;
                    aViewData.Archive.DocKey = ReportDocSettingCurrent.DocKey;
                    aViewData.Archive.DocKeyID = ReportDocSettingCurrent.DocKeyID;

                    if (!aViewData.ExistArchiveData())
                    {
                        //--- Report Export nach PDF File 
                        string PDFFilePathTemp = System.IO.Path.Combine(constValue_Report.const_localTempPDFReportPath, System.IO.Path.ChangeExtension(ReportDocSettingCurrent.TempReportFileName, "pdf"));
                        helper_IOFile.CheckPath(PDFFilePathTemp);

                        TelerikPrint_DirectPrintToPDF printPdf = null;
                        clsDocKey key = new clsDocKey();
                        if ((key.ListDocKeyRechnung.Contains(DokumentenArt)) && (iRGTableId > 0))
                        {
                            if (this.sys.Client.Modul.Fakt_eInvoiceIsAvailable)
                            {
                                printPdf = new TelerikPrint_DirectPrintToPDF(uRepSource, PDFFilePathTemp, true, sys.Client.Modul.Fakt_eInvoiceIsAvailable, iRGTableId);
                                printPdf.PdfFilePath = printPdf.AttachmentFilePath;
                            }
                            else
                            {
                                printPdf = new TelerikPrint_DirectPrintToPDF(uRepSource, PDFFilePathTemp, true);
                            }
                        }
                        else
                        {
                            printPdf = new TelerikPrint_DirectPrintToPDF(uRepSource, PDFFilePathTemp, true);
                        }
                        //TelerikPrint_DirectPrintToPDF printPdf = new TelerikPrint_DirectPrintToPDF(uRepSource, PDFFilePathTemp, true);

                        if ((printPdf.Success) && (helper_IOFile.CheckFile(printPdf.PdfFilePath)))
                        {
                            aViewData.Archive.Extension = enumFileArtExtension.pdf.ToString();
                            aViewData.Archive.Filename = printPdf.PdfFileName;
                            aViewData.Archive.FileData = helper_IOFile.FileToByteArray(printPdf.PdfFilePath);
                            aViewData.Archive.Created = DateTime.Now;
                            aViewData.Archive.UserId = (int)this.GL_User.User_ID;
                            aViewData.Archive.Description += "add" + Environment.NewLine + aViewData.Archive.Description;

                            // add pdf to db
                            aViewData.Add();
                        }
                    }
                }
            }
        }
        ///<summary>frmPrintRepView / PrintDirect</summary>
        ///<remarks></remarks>
        public void PrintDirect()
        {
            ErrorText = string.Empty;
            //bool bPrinted = false;
            //Druckanzahl - Schleifendurchläufe
            for (Int32 i = 1; i <= iPrintCount; i++)
            {
                TelerikPrint_DirectPrint directPrint = new TelerikPrint_DirectPrint(this.rViewer,
                                                                                    GL_User,
                                                                                    sys,
                                                                                    DokumentenArt,
                                                                                    this.PrinterName,
                                                                                    this.PaperSource
                                                                                    );

                if (directPrint.Success)
                {
                    if (this.DokumentenArt == "LagerAusgangAnzeigePerDay")
                    {
                        LagerP = new clsLager();
                        LagerP.InitClass(this.GL_User, this.GL_System, this.sys);
                        LagerP.Ausgang = new clsLAusgang();
                        LagerP.Ausgang.UpdatePrintLAusgang(DokumentenArt, this._ctrPostCenter.AdrPrintID, this._ctrPostCenter.LagerDate);
                    }
                    if (this.DokumentenArt == "LagerEingangAnzeigePerDay")
                    {
                        LagerP = new clsLager();
                        LagerP.InitClass(this.GL_User, this.GL_System, this.sys);
                        LagerP.Eingang = new clsLEingang();
                        LagerP.Eingang.UpdatePrintLEingang(DokumentenArt, this._ctrPostCenter.AdrPrintID, this._ctrPostCenter.LagerDate);
                    }
                    if (LagerP != null && LagerP.Eingang.LEingangID > 0)
                    {
                        LagerP.Eingang.UpdatePrintLEingang(DokumentenArt);

                    }
                    if (LagerP != null && LagerP.Ausgang.LAusgangID > 0)
                    {
                        LagerP.Ausgang.UpdatePrintLAusgang(DokumentenArt);
                    }
                }
            }
        }
        ///<summary>frmPrintRepView / PrintDirectToPDF</summary>
        ///         Doppelt siehe clsprint    
        ///<remarks></remarks>
        public void PrintDirectToPDF(string myPDFName, string myFilePath)
        {
            //Druckanzahl - Schleifendurchläufe
            for (Int32 i = 1; i <= iPrintCount; i++)
            {
                Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                // set any deviceInfo settings if necessary
                System.Collections.Hashtable deviceInfo = new System.Collections.Hashtable();
                Telerik.Reporting.TypeReportSource typeReportSource = new Telerik.Reporting.TypeReportSource();

                //reportName is the Assembly Qualified Name of the report
                typeReportSource.TypeName = myPDFName;
                Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", this.rViewer.ReportSource, deviceInfo);

                using (System.IO.FileStream fs = new System.IO.FileStream(myFilePath, System.IO.FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }
            }




        }



        ///<summary>frmPrintRepView / InitReportView</summary>
        ///<remarks>Hier werden die verschiedenen Report / Dokumentenarten erstell und initialisiert, 
        ///         der erstellte Report kann dann gedruckt werden.</remarks>
        public void InitReportView()
        {
            INI_Rep = new clsINI.clsINI(Application.StartupPath + "\\config.ini");
            string strDateTimeStamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");

            InitValuePrinter_Druckereinstellungen iniPrinterSetting = new InitValuePrinter_Druckereinstellungen(DokumentenArt, this.GL_System, (int)this.GL_User.User_ID);
            this.PrinterName = iniPrinterSetting.PrinterName;
            this.PaperSource = iniPrinterSetting.PaperSource;
            this.iPrintCount = iniPrinterSetting.PrintCount;

            switch (DokumentenArt)
            {
                // neue Druckversion
                case "LabelOne":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("ArtikelID", this.LagerP.Artikel.ID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_LabelOne_Printer;
                        this.PaperSource = this.GL_System.docPath_LabelOne_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_LabelOne_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Artikel.ID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Artikel.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "ArtikelLabel";
                    break;

                case "LOLabel":
                case "LOLabelRechts":
                case "LOLabelLinks":
                case "LOLabelBeide":
                case "LOLabelLinksOben":
                case "LOLabelRechtsUnten":
                case "LOLabelBeideLinksObenRechtsUnten":
                    uRepSource = new UriReportSource();
                    if (this._ctrReihen != null
                        && this._ctrReihen.LagerOrt.Werk != null && this._ctrReihen.LagerOrt.Werk.Halle != null &&
                        this._ctrReihen.LagerOrt.Werk.Halle.Reihe != null)
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("ReihenID", this._ctrReihen.LagerOrt.Werk.Halle.Reihe.ID));
                    }
                    if (this._ctrPrintLagerOrtLabel != null)
                    {
                        string barcode = "#";
                        barcode = barcode + this._ctrPrintLagerOrtLabel.tbWerk.Text + "#";
                        barcode = barcode + this._ctrPrintLagerOrtLabel.tbHalle.Text + "#";
                        barcode = barcode + this._ctrPrintLagerOrtLabel.tbReihe.Text + "#";
                        barcode = barcode + this._ctrPrintLagerOrtLabel.tbEbene.Text + "#";
                        barcode = barcode + this._ctrPrintLagerOrtLabel.tbPlatz.Text;

                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BarcodeLinks", barcode));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("WerkLinks", this._ctrPrintLagerOrtLabel.tbWerk.Text));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("HalleLinks", this._ctrPrintLagerOrtLabel.tbHalle.Text));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("ReiheLinks", this._ctrPrintLagerOrtLabel.tbReihe.Text));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("EbeneLinks", this._ctrPrintLagerOrtLabel.tbEbene.Text));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("PlatzLinks", this._ctrPrintLagerOrtLabel.tbPlatz.Text));

                        barcode = "#";
                        barcode = barcode + this._ctrPrintLagerOrtLabel.tbWerkRight.Text + "#";
                        barcode = barcode + this._ctrPrintLagerOrtLabel.tbHalleRight.Text + "#";
                        barcode = barcode + this._ctrPrintLagerOrtLabel.tbReiheRight.Text + "#";
                        barcode = barcode + this._ctrPrintLagerOrtLabel.tbEbeneRight.Text + "#";
                        barcode = barcode + this._ctrPrintLagerOrtLabel.tbPlatzRight.Text;

                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BarcodeRechts", barcode));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("WerkRechts", this._ctrPrintLagerOrtLabel.tbWerkRight.Text));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("HalleRechts", this._ctrPrintLagerOrtLabel.tbHalleRight.Text));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("ReiheRechts", this._ctrPrintLagerOrtLabel.tbReiheRight.Text));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("EbeneRechts", this._ctrPrintLagerOrtLabel.tbEbeneRight.Text));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("PlatzRechts", this._ctrPrintLagerOrtLabel.tbPlatzRight.Text));

                    }

                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_LabelOne_Printer;
                        this.PaperSource = this.GL_System.docPath_LabelOne_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_LabelOne_Count;
                    }

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "LagerortLabel";
                    break;
                case "SchadenLable":
                case "SchadenLabel":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("ArtikelID", this.LagerP.Artikel.ID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_SPLLabel_Printer;
                        this.PaperSource = this.GL_System.docPath_SPLLabel_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_SPLLabel_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Artikel.ID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Artikel.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Schadenslabel";
                    break;

                //case Globals.enumDokumentenart.SchadenDoc.ToString():
                case "SchadenDoc":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("ArtikelID", this.LagerP.Artikel.ID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("Benutzer", this.GL_User.Vorname + " " + this.GL_User.Name));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_SPLDoc_Printer;
                        this.PaperSource = this.GL_System.docPath_SPLDoc_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_SPLDoc_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Artikel.ID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Artikel.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Schadensmeldung";
                    break;

                case "SPLLabel":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("ArtikelID", this.LagerP.Artikel.ID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_SPLLabel_Printer;
                        this.PaperSource = this.GL_System.docPath_SPLLabel_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_SPLLabel_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Artikel.ID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Artikel.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Sperrlagerlabel";
                    break;

                case "SPLDoc":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("ArtikelID", this.LagerP.Artikel.ID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_SPLDoc_Printer;
                        this.PaperSource = this.GL_System.docPath_SPLDoc_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_SPLDoc_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Artikel.ID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Artikel.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Sperlagerdokument";
                    break;

                case "LabelOneNeutral":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("ArtikelID", this.LagerP.Artikel.ID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_LabelOne_Printer;
                        this.PaperSource = this.GL_System.docPath_LabelOne_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_LabelOne_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Artikel.ID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Artikel.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "LabelOneNeutral";
                    break;

                case "LabelAll":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_LabelAll_Printer;
                        this.PaperSource = this.GL_System.docPath_LabelAll_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_LabelAll_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "LabelAll";
                    break;

                case "ArtikelListe":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("Artikel", this._ctrBestand.lstIDs));
                    uRepSource.Uri = Application.StartupPath + GL_System.docPath_ArtikelListe;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_Bestand_Printer;
                        this.PaperSource = this.GL_System.docPath_EingangDoc_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_Bestand_Count;
                    }
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "ArtikelListe";
                    break;

                case "EingangDoc":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Eingang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_EingangDoc_Printer;
                        this.PaperSource = this.GL_System.docPath_EingangDoc_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_EingangDoc_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Dokument Lagereingang";

                    break;
                case "EingangAnzeige":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Eingang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_EingangAnzeige_Printer;
                        this.PaperSource = this.GL_System.docPath_EingangAnzeige_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_EingangAnzeige_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "EingangAnzeige";
                    break;

                case "EingangLfs":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Eingang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_EingangLfs_Printer;
                        this.PaperSource = this.GL_System.docPath_EingangLfs_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_EingangLfs_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Eingangslieferschein";
                    break;

                case "EingangDocMat":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Eingang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_EingangDoc_Printer;
                        this.PaperSource = this.GL_System.docPath_EingangDoc_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_EingangDoc_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Eingangslieferschein";
                    break;

                case "Eingangsliste":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    //uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Eingang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_EingangList_Printer;
                        this.PaperSource = this.GL_System.docPath_EingangList_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_EingangList_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lagereingangsliste";
                    break;

                case "AusgangDoc":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerId", this.GL_User.User_ID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_AusgangDoc_Printer;
                        this.PaperSource = this.GL_System.docPath_AusgangDoc_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_AusgangDoc_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Ausgang.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Ausgangsschein";
                    break;

                case "AusgangAnzeige":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_AusgangAnzeige_Printer;
                        this.PaperSource = this.GL_System.docPath_AusgangAnzeige_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_AusgangAnzeige_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Ausgang.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lagerausganganzeige";

                    break;
                case "AusgangLfs":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Ausgang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_AusgangLfs_Printer;
                        this.PaperSource = this.GL_System.docPath_AusgangLfs_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_AusgangLfs_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Ausgang.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lieferschein Lagerausgang";
                    break;

                case "Ausgangsliste":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_AusgangList_Printer;
                        this.PaperSource = this.GL_System.docPath_AusgangList_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_AusgangList_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Ausgang.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Ausgangsliste";
                    break;

                case "CMRFrachtbrief":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    //uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Ausgang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_AusgangLfs_Printer;
                        this.PaperSource = this.GL_System.docPath_AusgangLfs_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_AusgangLfs_Count;
                    }

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Ausgang.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "CMRFrachtbrief";
                    break;

                case "KVOFrachtbrief":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    //uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Ausgang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_AusgangLfs_Printer;
                        this.PaperSource = this.GL_System.docPath_AusgangLfs_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_AusgangLfs_Count;
                    }
                    //else
                    //{
                    //    InitValuePrinter_Druckereinstellungen iniPrinterSetting = new InitValuePrinter_Druckereinstellungen(enumDokumentenArt.KVOFrachtbrief, this.GL_System, (int)this.GL_User.User_ID);
                    //    this.PrinterName = iniPrinterSetting.PrinterName;
                    //    this.PaperSource = iniPrinterSetting.PaperSource;
                    //    this.iPrintCount = iniPrinterSetting.PrintCount;
                    //}

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Ausgang.AbBereichID;

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "KVOFrachtbrief";
                    break;

                case "DocAuftragScan":
                    docAuftragScan docScan = new docAuftragScan();
                    docScan.DocScan = this.DocScan;
                    repSource = new InstanceReportSource();
                    repSource.ReportDocument = docScan;
                    this.rViewer.ReportSource = repSource;
                    this.rViewer.Name = "DocScan";
                    break;

                //alte Druckversion
                case "ArtikelLabel":
                    uRepSource = new UriReportSource();
                    if (!this._ctrPrintLager._ctrEinlagerung._bArtPrint)
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                        uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_LabelAll;
                        this.PaperSource = this.GL_System.docPath_LabelAll_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_LabelAll_Count;

                        aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                        aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;
                    }
                    else
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("ArtikelID", this.LagerP.Artikel.ID));
                        uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_LabelOne;
                        this.PaperSource = this.GL_System.docPath_LabelOne_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_LabelOne_Count;

                        aViewData.Archive.TableId = (int)LagerP.Artikel.ID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                        aViewData.Archive.WorkspaceId = (int)LagerP.Artikel.AbBereichID;
                    }
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "ArtikelLabel";
                    break;

                case "LagerEingangDoc":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Eingang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_EingangDoc;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Dokument Lagereingang";
                    this.PaperSource = this.GL_System.docPath_EingangDoc_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_EingangDoc_Count;

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;
                    break;
                case "LagerEingangDocMat":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Eingang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_EingangDocMat;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Dokument Lagereingang";
                    this.PaperSource = this.GL_System.docPath_EingangDoc_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_EingangDoc_Count;

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;
                    break;

                case "LagerEingangAnzeige":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_EingangAnzeige;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lieferschein Lagereingang";
                    this.PaperSource = this.GL_System.docPath_EingangAnzeige_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_EingangAnzeige_Count;

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;
                    break;

                case "LagerEingangAnzeigeMail":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_EingangAnzeigeMail;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lieferschein Lagereingang";

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;
                    break;
                case "LagerEingangAnzeigePerDay":
                    uRepSource = new UriReportSource();
                    //uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    Parameter pAuftraggeber = new Parameter("Auftraggeber", this._ctrPostCenter.AdrPrintID);
                    Parameter pStichtag = new Parameter("Stichtag", this._ctrPostCenter.LagerDate);
                    uRepSource.Parameters.Add(pAuftraggeber);
                    uRepSource.Parameters.Add(pStichtag);
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_EingangAnzeigePerDay;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lageranzeige pro Tag ";
                    this.PaperSource = this.GL_System.docPath_EingangAnzeigePerDay_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_EingangAnzeige_Count;
                    break;
                case "LagerEingangAnzeigePerDayFull":
                    uRepSource = new UriReportSource();
                    //uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    Parameter pAuftraggeber1 = new Parameter("Auftraggeber", this._ctrPostCenter.AdrPrintID);
                    Parameter pStichtag1 = new Parameter("Stichtag", this._ctrPostCenter.LagerDate);
                    uRepSource.Parameters.Add(pAuftraggeber1);
                    uRepSource.Parameters.Add(pStichtag1);
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_EingangAnzeigePerDay.Replace(".trdx", "Full.trdx");
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lageranzeige pro Tag ";
                    this.PaperSource = this.GL_System.docPath_EingangAnzeigePerDay_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_EingangAnzeige_Count;
                    break;
                case "LagerEingangAnzeigePerDayMail":
                    uRepSource = new UriReportSource();
                    //uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    uRepSource.Parameters.Add(new Parameter("Auftraggeber", this._ctrPostCenter.AdrPrintID));
                    uRepSource.Parameters.Add(new Parameter("Stichtag", this._ctrPostCenter.LagerDate));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_EingangAnzeigePerDayMail;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lageranzeige pro Tag ";
                    break;
                case "LagerEingangLfs":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Eingang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_EingangLfs;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lieferschein Lagereingang";
                    this.PaperSource = this.GL_System.docPath_EingangLfs_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_EingangLfs_Count;

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;
                    break;
                case "LagerEingangLfsMail":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", this.LagerP.Eingang.LEingangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Eingang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_EingangLfsMail;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lieferschein Lagereingang";

                    aViewData.Archive.TableId = (int)LagerP.Eingang.LEingangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;
                    break;
                case "LagerAusgangDoc":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerId", this.GL_User.User_ID));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_AusgangDoc;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lagerausgangschein";
                    this.PaperSource = this.GL_System.docPath_AusgangDoc_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_AusgangDoc_Count;

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Eingang.AbBereichID;
                    break;

                case "LagerAusgangAnzeige":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_AusgangAnzeige;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lagerausganganzeige";
                    this.PaperSource = this.GL_System.docPath_AusgangAnzeige_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_AusgangAnzeige_Count;

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Ausgang.AbBereichID;
                    break;

                case "LagerAusgangAnzeigeMail":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_AusgangAnzeigeMail;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lagerausganganzeige";

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Ausgang.AbBereichID;
                    break;
                case "LagerAusgangAnzeigePerDay":
                    uRepSource = new UriReportSource();
                    Parameter pAuftraggeberAus = new Parameter("Auftraggeber", this._ctrPostCenter.AdrPrintID);
                    Parameter pStichtagAus = new Parameter("Stichtag", this._ctrPostCenter.LagerDate);
                    uRepSource.Parameters.Add(pAuftraggeberAus);
                    uRepSource.Parameters.Add(pStichtagAus);
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_AusgangAnzeigePerDay;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lagerausganganzeige pro Tag";
                    this.PaperSource = this.GL_System.docPath_AusgangAnzeigePerDay_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_AusgangAnzeigePerDay_Count;
                    break;
                case "LagerAusgangAnzeigePerDayFull":
                    uRepSource = new UriReportSource();
                    Parameter pAuftraggeberAus1 = new Parameter("Auftraggeber", this._ctrPostCenter.AdrPrintID);
                    Parameter pStichtagAus1 = new Parameter("Stichtag", this._ctrPostCenter.LagerDate);
                    uRepSource.Parameters.Add(pAuftraggeberAus1);
                    uRepSource.Parameters.Add(pStichtagAus1);
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_AusgangAnzeigePerDay.Replace(".trdx", "Full.trdx");
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lagerausganganzeige pro Tag";
                    this.PaperSource = this.GL_System.docPath_AusgangAnzeigePerDay_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_AusgangAnzeigePerDay_Count;

                    break;
                case "LagerAusgangAnzeigePerDayMail":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Parameter("Auftraggeber", this._ctrPostCenter.AdrPrintID));
                    uRepSource.Parameters.Add(new Parameter("Stichtag", this._ctrPostCenter.LagerDate));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_AusgangAnzeigePerDayMail;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lagerausganganzeige pro Tag";
                    break;

                case "LagerAusgangLfs":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Ausgang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_AusgangLfs;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lieferschein Lagerausgang";
                    this.PaperSource = this.GL_System.docPath_AusgangLfs_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_AusgangLfs_Count;

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Ausgang.AbBereichID;
                    break;

                case "LagerAusgangLfsMat":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Ausgang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_AusgangLfsMat;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lieferschein Lagerausgang";
                    this.PaperSource = this.GL_System.docPath_AusgangLfs_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_AusgangLfs_Count;

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Ausgang.AbBereichID;
                    break;
                case "LagerAusgangLfsMail":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", this.LagerP.Ausgang.BenutzerID));
                    uRepSource.Uri = Application.StartupPath + this.GL_System.docPath_AusgangLfsMail;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lieferschein Lagerausgang";

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Ausgang.AbBereichID;
                    break;
                case "LagerAusgangNeutralDoc":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", this.LagerP.Ausgang.LAusgangTableID));
                    uRepSource.Uri = Application.StartupPath + GL_System.docPath_AusgangNeutralDoc;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Lieferschein Lagerausgang";
                    this.PaperSource = this.GL_System.docPath_AusgangNeutralDoc_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_AusgangNeutralDoc_Count;

                    aViewData.Archive.TableId = (int)LagerP.Ausgang.LAusgangTableID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                    aViewData.Archive.WorkspaceId = (int)LagerP.Ausgang.AbBereichID;

                    break;

                case "Bestandsliste":
                    uRepSource = new UriReportSource();
                    //uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("Titel", this._ctrBestand.PrintTitel));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("AuftraggeberID", this._ctrBestand.ADR.ID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("OrderBy", this._ctrBestand.sortID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("Stichtag", this._ctrBestand.Stichtag));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("AbBereich", this._ctrBestand._ctrMenu._frmMain.system.AbBereich.ID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_Bestand_Printer;
                        this.PaperSource = string.Empty;// this.GL_System.docPath_Bestand ;
                        this.iPrintCount = this.GL_System.docPath_Bestand_Count;
                    }

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Bestandsliste";
                    this.iPrintCount = this.GL_System.docPath_Bestand_Count;
                    break;

                case "Inventur":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("AuftraggeberID", this._ctrBestand.ADR.ID));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("Stichtag", this._ctrBestand.Stichtag));
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("AbBereich", this._ctrBestand._ctrMenu._frmMain.system.AbBereich.ID));
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        this.PrinterName = this.GL_System.docPath_Inventur_Printer;
                        this.PaperSource = string.Empty; // this.GL_System.docPath_AusgangLfs_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_Inventur_Count;
                    }

                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Inventurliste";
                    this.iPrintCount = this.GL_System.docPath_Inventur_Count;

                    break;

                case "Journal":
                    break;

                case "Rechnung":
                case "Gutschrift":
                case "LagerRechnung":
                case "Lagerrechnung":
                    switch (GL_System.client_MatchCode)
                    {
                        case LVS.clsClient.const_ClientMatchcode_Heisiep + "_":
                            docBriefkopfHeisiepLager docHeisiep = new docBriefkopfHeisiepLager();
                            docHeisiep.GL_System = this.GL_System;
                            docHeisiep.GL_User = this.GL_User;
                            if (this._ctrFaktLager is ctrFaktLager)
                            {
                                docHeisiep.InitDoc(this._ctrFaktLager.FaktLager.Rechnung.ID);
                            }
                            if (this._ctrRGList is ctrRGList)
                            {
                                docHeisiep.InitDoc(this._ctrRGList.FaktLager.Rechnung.ID);
                            }

                            repSource = new InstanceReportSource();
                            repSource.ReportDocument = docHeisiep;
                            this.rViewer.ReportSource = repSource;
                            this.rViewer.Name = "Rechnung / Gutschrift Lager";
                            break;

                        default:
                            uRepSource = new UriReportSource();
                            if (this._ctrRGList != null)
                            {
                                uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrRGList.FaktLager.Rechnung.ID));
                                aViewData.Archive.TableId = (int)this._ctrRGList.FaktLager.Rechnung.ID;
                                aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                                aViewData.Archive.WorkspaceId = (int)this._ctrRGList.FaktLager.Rechnung.ArBereichID;
                            }
                            if (this._ctrFaktLager != null)
                            {
                                uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrFaktLager.FaktLager.Rechnung.ID));
                                aViewData.Archive.TableId = (int)this._ctrFaktLager.FaktLager.Rechnung.ID;
                                aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                                aViewData.Archive.WorkspaceId = (int)this._ctrFaktLager.FaktLager.Rechnung.ArBereichID;
                            }
                            uRepSource.Uri = Application.StartupPath + DocPath;

                            if (this.sys.Client.Modul.Print_OldVersion)
                            {
                                uRepSource.Uri = Application.StartupPath + GL_System.docPath_Lagerrechnung;
                                this.PrinterName = this.GL_System.docPath_Lagerrechnung_Printer;
                                this.PaperSource = this.GL_System.docPath_Lagerrechnung_PaperSource;
                                this.iPrintCount = this.GL_System.docPath_Lagerrechnung_Count;
                            }
                            this.rViewer.ReportSource = uRepSource;
                            this.rViewer.Name = "Rechnung";
                            break;
                    }
                    break;
                case "RechnungMail":
                case "LagerRechnungMail":
                    uRepSource = new UriReportSource();
                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrRGList.FaktLager.Rechnung.ID));
                    uRepSource.Uri = Application.StartupPath + GL_System.docPath_LagerrechnungMail;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Rechnung per Mail";

                    aViewData.Archive.TableId = (int)this._ctrRGList.FaktLager.Rechnung.ID;
                    aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                    aViewData.Archive.WorkspaceId = (int)this._ctrRGList.FaktLager.Rechnung.ArBereichID;
                    break;

                case "Manuellerechnung":
                case "manuelleRGGS":
                    uRepSource = new UriReportSource();
                    if (this._ctrRGManuell != null)
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrRGManuell.Rechnung.ID));
                        aViewData.Archive.TableId = (int)this._ctrRGManuell.Rechnung.ID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                        aViewData.Archive.WorkspaceId = (int)this._ctrRGManuell.Rechnung.ArBereichID;
                    }
                    if (this._ctrRGList != null)
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrRGList.FaktLager.Rechnung.ID));
                        aViewData.Archive.TableId = (int)this._ctrRGList.FaktLager.Rechnung.ID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                        aViewData.Archive.WorkspaceId = (int)this._ctrRGList.FaktLager.Rechnung.ArBereichID;
                    }
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        uRepSource.Uri = Application.StartupPath + GL_System.docPath_Manuellerechnung;
                        this.PrinterName = this.GL_System.docPath_Lagerrechnung_Printer;
                        this.PaperSource = this.GL_System.docPath_Manuellerechnung_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_Manuellerechnung_Count;
                    }
                    this.rViewer.Name = "Manuelle Rechnung";
                    this.rViewer.ReportSource = uRepSource;
                    break;

                case "ManuelleGutschrift":
                    uRepSource = new UriReportSource();

                    if (this._ctrRGManuell != null)
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrRGManuell.Rechnung.ID));
                        aViewData.Archive.TableId = (int)this._ctrRGManuell.Rechnung.ID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                        aViewData.Archive.WorkspaceId = (int)this._ctrRGManuell.Rechnung.ArBereichID;
                    }
                    if (this._ctrRGList != null)
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrRGList.FaktLager.Rechnung.ID));
                        aViewData.Archive.TableId = (int)this._ctrRGList.FaktLager.Rechnung.ID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                        aViewData.Archive.WorkspaceId = (int)this._ctrRGList.FaktLager.Rechnung.ArBereichID;
                    }
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        uRepSource.Uri = Application.StartupPath + GL_System.docPath_ManuelleGutschrift;
                        this.PrinterName = this.GL_System.docPath_Lagerrechnung_Printer;
                        this.PaperSource = this.GL_System.docPath_ManuelleGutschrift_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_ManuelleGutschrift_Count;
                    }
                    this.rViewer.Name = "Manuelle Gutschrift";
                    this.rViewer.ReportSource = uRepSource;
                    break;

                case "manuelleRGGSMail":
                    switch (GL_System.client_MatchCode)
                    {
                        case "Honselmann_":
                            uRepSource = new UriReportSource();
                            uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrRGManuell.Rechnung.ID));
                            uRepSource.Uri = Application.StartupPath + GL_System.docPath_ManuellerechnungMail;
                            this.rViewer.ReportSource = uRepSource;
                            this.rViewer.Name = "Manuelle Rechnung";

                            aViewData.Archive.TableId = (int)this._ctrRGManuell.Rechnung.ID;
                            aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                            aViewData.Archive.WorkspaceId = (int)this._ctrRGManuell.Rechnung.ArBereichID;
                            break;
                    }
                    break;
                case "RGBuch":
                    uRepSource = new UriReportSource();
                    if (this._ctrRGList != null)
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("AbBereich", this._ctrRGList._ctrMenu._frmMain.system.AbBereich.ID));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("ZVon", this._ctrRGList.FaktLager.Rechnung.RGDatumVon));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("ZBis", this._ctrRGList.FaktLager.Rechnung.RGDatumBis));
                    }
                    uRepSource.Uri = Application.StartupPath + DocPath;

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        uRepSource.Uri = Application.StartupPath + GL_System.docPath_RGBuch;
                        this.PrinterName = this.GL_System.docPath_RGBuch_Printer;
                        this.iPrintCount = this.GL_System.docPath_RGBuch_Count;
                        this.PaperSource = this.GL_System.docPath_RGBuch_PaperSource;
                    }
                    this.rViewer.Name = "Rechnungsbuch";
                    this.rViewer.ReportSource = uRepSource;
                    break;

                case "RGAnhang":
                    uRepSource = new UriReportSource();
                    if (this._ctrRGList != null)
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrRGList.FaktLager.Rechnung.ID));
                        aViewData.Archive.TableId = (int)this._ctrRGList.FaktLager.Rechnung.ID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                        aViewData.Archive.WorkspaceId = (int)this._ctrRGList.FaktLager.Rechnung.ArBereichID;
                    }
                    if (this._ctrRGManuell != null)
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrRGManuell.Rechnung.ID));
                        aViewData.Archive.TableId = (int)this._ctrRGManuell.Rechnung.ID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                        aViewData.Archive.WorkspaceId = (int)this._ctrRGManuell.Rechnung.ArBereichID;
                    }
                    if (this._ctrFaktLager != null)
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrFaktLager.FaktLager.Rechnung.ID));
                        aViewData.Archive.TableId = (int)this._ctrFaktLager.FaktLager.Rechnung.ID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                    }

                    uRepSource.Uri = Application.StartupPath + DocPath;
                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        uRepSource.Uri = Application.StartupPath + GL_System.docPath_RGAnhang;
                        this.PrinterName = this.GL_System.docPath_RGAnhang_Printer;
                        this.PaperSource = this.GL_System.docPath_RGAnhang_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_RGAnhang_Count;
                    }
                    this.rViewer.Name = "Rechnungsanhang";
                    this.rViewer.ReportSource = uRepSource;
                    break;

                case "RGAnhangMat":
                    uRepSource = new UriReportSource();
                    if (this._ctrRGList != null)
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrRGList.FaktLager.Rechnung.ID));
                        aViewData.Archive.TableId = (int)this._ctrRGList.FaktLager.Rechnung.ID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                        aViewData.Archive.WorkspaceId = (int)this._ctrRGList.FaktLager.Rechnung.ArBereichID;
                    }
                    else if (this._ctrFaktLager != null)
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrFaktLager.FaktLager.Rechnung.ID));
                        aViewData.Archive.TableId = (int)this._ctrFaktLager.FaktLager.Rechnung.ID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                        aViewData.Archive.WorkspaceId = (int)this._ctrFaktLager.FaktLager.Rechnung.ArBereichID;
                    }
                    else
                    {
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this._ctrRGManuell.Rechnung.ID));
                        aViewData.Archive.TableId = (int)this._ctrRGManuell.Rechnung.ID;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Rechnungen.ToString();
                        aViewData.Archive.WorkspaceId = (int)this._ctrRGManuell.Rechnung.ArBereichID;
                    }
                    uRepSource.Uri = Application.StartupPath + GL_System.docPath_RGAnhangMat;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "RG Anhang Material";

                    if (this.sys.Client.Modul.Print_OldVersion)
                    {
                        uRepSource.Uri = Application.StartupPath + GL_System.docPath_RGAnhangMat;
                        this.PrinterName = this.GL_System.docPath_RGAnhang_Printer;
                        this.PaperSource = this.GL_System.docPath_RGAnhang_PaperSource;
                        this.iPrintCount = this.GL_System.docPath_RGAnhang_Count;
                    }
                    break;
                case "Adressliste":
                    uRepSource = new UriReportSource();
                    uRepSource.Uri = Application.StartupPath + GL_System.docPath_Adressliste;
                    this.rViewer.ReportSource = uRepSource;
                    this.rViewer.Name = "Stückliste Lagerausgang";
                    this.PaperSource = this.GL_System.docPath_RGAnhang_PaperSource;
                    this.iPrintCount = this.GL_System.docPath_Adressliste_Count;
                    break;
            }

            if (bPrintFromForm == true)
                iPrintCount = (Int32)this._ctrPrintLager.nudPrintCount.Value;
            this.rViewer.RefreshReport();
        }
    }
}
