using Common.Enumerations;
using Common.Models;
using LVS;
using LVS.Helper;
using LVS.ViewData;
using Telerik.Reporting;

namespace Common.Print
{
    public class TelerikPrint
    {
        internal Globals._GL_USER GL_USER { get; set; }
        internal Globals._GL_SYSTEM GLSystem { get; set; }
        internal clsSystem Sys { get; set; }

        internal string PaperSource { get; set; }
        internal string PrinterName { get; set; }
        internal string FilePathTemp{ get; set; }

        internal string PDFFilePathTemp { get; set; }
        internal string PDFFileNameTemp { get; set; }
        public int PrintCount { get; set; } = 1;
        public string Text { get; set; } = string.Empty;
        //public enumDokumentenart DokumentenArt { get; set; }
        public enumPrintDocumentArt PrintDocumentArt { get; set; }
        //public enumDokumentenArt DocumentArt { get; set; }
        public Articles Article { get; set; }
        public ArticleViewData articleViewData { get; set; }
        public Ausgaenge Ausgang { get; set; }
        public AusgangViewData ausgangViewData { get; set; }
        public Eingaenge Eingang { get; set; }
        public EingangViewData eingangViewData { get; set; }
        public clsReportDocSetting ReportDocSetting { get; set; }
        //internal UriReportSource uriReportSource { get; set; }
        internal TelerikUriReportSource uriReportSource { get; set; }
        
        internal InstanceReportSource repSource { get; set; }
        
        //internal Telerik.
        internal Telerik.ReportViewer.WinForms.ReportViewer reportViewer;
        internal ArchiveViewData aViewData { get; set; }

        public TelerikPrint() 
        {
            aViewData = new ArchiveViewData();
            aViewData.Archive = new Archives();
            Article = new Articles();
            Eingang = new Eingaenge();
            Ausgang = new Ausgaenge();

            reportViewer = new Telerik.ReportViewer.WinForms.ReportViewer();
        }
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, clsSystem mySystem,int myEingangTableId, int myAusgangTableId, int myArticleId, enumPrintDocumentArt myDocArt)
        {
            GL_USER = myGLUser;
            GLSystem = myGLSystem;
            Sys = mySystem;
            PrintDocumentArt = myDocArt;

            int iTmpAuftraggeber = 0;
            int iTmpArbeitsbereichId = 0;
            if (myEingangTableId > 0)
            {
                eingangViewData = new EingangViewData(myEingangTableId, (int)GL_USER.User_ID, true);
                Eingang = eingangViewData.Eingang.Copy();
                iTmpArbeitsbereichId = Eingang.ArbeitsbereichId;
                iTmpAuftraggeber = Eingang.Auftraggeber;

                aViewData.Archive.TableId= Eingang.Id;
                aViewData.Archive.TableName = enumDatabaseTableNames.LEingang.ToString();
                aViewData.Archive.WorkspaceId = Eingang.ArbeitsbereichId;
            }
            if(myAusgangTableId > 0)
            {
                ausgangViewData = new AusgangViewData(myAusgangTableId, (int)GL_USER.User_ID, true);
                Ausgang = ausgangViewData.Ausgang.Copy();
                iTmpArbeitsbereichId = Ausgang.ArbeitsbereichId;
                iTmpAuftraggeber = Ausgang.Auftraggeber;

                aViewData.Archive.TableId = Ausgang.Id;
                aViewData.Archive.TableName = enumDatabaseTableNames.LAusgang.ToString(); 
                aViewData.Archive.WorkspaceId = Ausgang.ArbeitsbereichId;

            }
            if (myArticleId > 0)
            {
                articleViewData = new ArticleViewData(myArticleId, (int)GL_USER.User_ID, false);
                Article = articleViewData.Artikel.Copy();
                eingangViewData = new EingangViewData(Article.LEingangTableID, (int)GL_USER.User_ID, true);
                Eingang = eingangViewData.Eingang.Copy();
                if (Article.LAusgangTableID > 0)
                {
                    ausgangViewData = new AusgangViewData(myAusgangTableId, (int)GL_USER.User_ID, true);
                    Ausgang = ausgangViewData.Ausgang.Copy();
                }
                iTmpArbeitsbereichId = Article.AbBereichID;
                iTmpAuftraggeber = Eingang.Auftraggeber;

                aViewData.Archive.TableId = Article.Id;
                aViewData.Archive.TableName = enumDatabaseTableNames.Artikel.ToString();
                aViewData.Archive.WorkspaceId = Article.AbBereichID;
            }
            //PrintDocumentArt = myPrintDocArt;

            ReportDocSetting = new clsReportDocSetting();
            ReportDocSetting.InitClass(GL_USER, GLSystem, Sys, iTmpAuftraggeber, iTmpArbeitsbereichId);

            //Report zum Druck ermittelt werden
            if (
                    (ReportDocSetting is clsReportDocSetting) && 
                    (ReportDocSetting.ListReportDocSettingAll.Count > 0)
               )
            {
                clsReportDocSetting tmpSetting = new clsReportDocSetting();
                if (iTmpAuftraggeber > 0)
                {
                    tmpSetting = ReportDocSetting.ListReportDocSettingAll.FirstOrDefault(x => x.AdrID == iTmpAuftraggeber);
                }
                if(tmpSetting is null)
                {
                    tmpSetting = ReportDocSetting.ListReportDocSettingAll.FirstOrDefault(x => x.DocKey == PrintDocumentArt.ToString());
                }

                if ((tmpSetting is clsReportDocSetting) && (tmpSetting.ID > 0))
                {
                    //Report ermitteltn bzw. erstellen
                    if ((tmpSetting.ReportDataFileExist) && (tmpSetting.ReportDataFile.Length>0))
                    {
                        //-- save Report as file to use the report
                        //FilePathTemp = System.IO.Path.Combine(clsReportDocSetting.const_localTempReportPath, tmpSetting.TempReportFileName);
                        //FileAndImageHelper.SaveByteArrayToFileWithStaticMethod(tmpSetting.ReportDataFile, FilePathTemp);

                        FilePathTemp = TelerikPrint.SaveReportFileToHDForUse(tmpSetting);
                        if(!FilePathTemp.Equals(string.Empty)) 
                        {
                            uriReportSource = new TelerikUriReportSource(FilePathTemp, Eingang.Id, Ausgang.Id, Article.Id, PrintDocumentArt, (int)this.GL_USER.User_ID);
                            reportViewer.ReportSource = uriReportSource;
                            //reportViewer.Name = uriReportSource.DokumentName;

                            for (Int32 i = 1; i <= PrintCount; i++)
                            {
                                TelerikPrint_DirectPrint directPrint = new TelerikPrint_DirectPrint(this.reportViewer, GL_USER, Sys, PrintDocumentArt);
                            }
                        }
                        // - benötigte Daten (TableName, TableId, FileArt)
                        // -  TableName und TableId in InitClass
                        aViewData.Archive.FileArt = enumFileArt.PDF;
                        aViewData.Archive.ReportDocSettingId = tmpSetting.ID;
                        aViewData.Archive.ReportDocSettingAssignmentId = tmpSetting.RSAId;
                        aViewData.Archive.DocKey = tmpSetting.DocKey;

                        if (!aViewData.ExistArchiveData())
                        {
                            //--- Report Export nach PDF File 
                            PDFFilePathTemp =  System.IO.Path.Combine(clsReportDocSetting.const_localTempPDFReportPath, System.IO.Path.ChangeExtension(tmpSetting.TempReportFileName, "pdf"));
                            helper_IOFile.CheckPath(PDFFilePathTemp);
                            TelerikPrint_DirectPrintToPDF printPdf = new TelerikPrint_DirectPrintToPDF(uriReportSource, PDFFilePathTemp);

                            if ((printPdf.Success) && (helper_IOFile.CheckFile(printPdf.PdfFilePath)))
                            {
                                aViewData.Archive.Extension = enumFileArtExtension.pdf.ToString();
                                aViewData.Archive.Filename = printPdf.PdfFileName;
                                aViewData.Archive.FileData = helper_IOFile.FileToByteArray(printPdf.PdfFilePath);
                                aViewData.Archive.Created = DateTime.Now;
                                aViewData.Archive.UserId = (int)GL_USER.User_ID;
                                aViewData.Archive.Description += "add" + Environment.NewLine + aViewData.Archive.Description;

                                // add pdf to db
                                aViewData.Add();
                            }
                        }
                    }
                }
            }
        }

        public static string SaveReportFileToHDForUse(clsReportDocSetting myTmpSetting)
        {
            string strReturnFilePathTemp = string.Empty;
            if (
                    (myTmpSetting is clsReportDocSetting) &&
                    (myTmpSetting.ID > 0) &&
                    (myTmpSetting.RSAId > 0)
               )
            {
                strReturnFilePathTemp = System.IO.Path.Combine(clsReportDocSetting.const_localTempReportPath, myTmpSetting.TempReportFileName);
                helper_Image.SaveByteArrayToFileWithStaticMethod(myTmpSetting.ReportDataFile, strReturnFilePathTemp);
                if (!helper_IOFile.CheckFile(strReturnFilePathTemp))
                {
                    strReturnFilePathTemp = string.Empty;
                }
            }
            return strReturnFilePathTemp;
        }
               
        //private void DirektPrint()
        //{

        //    // Obtain the settings of the default printer
        //    System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
        //    if (PrinterName != string.Empty)
        //        printerSettings.PrinterName = PrinterName;
        //    PaperSource pkSource = null;
        //    string strPaperSource = string.Empty;

        //    for (int j = 0; j < printerSettings.PaperSources.Count; j++)
        //    {

        //        if (printerSettings.PaperSources[j].SourceName == PaperSource)
        //            pkSource = printerSettings.PaperSources[j];

        //    }
        //    if (pkSource != null)
        //    {
        //        printerSettings.DefaultPageSettings.PaperSource = pkSource;
        //    }
        //    // The standard print controller comes with no UI
        //    System.Drawing.Printing.PrintController standardPrintController = new System.Drawing.Printing.StandardPrintController();
        //    // Print the report using the custom print controller
        //    Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
        //    reportProcessor.PrintController = standardPrintController;
        //    //Druck des Reports
        //    if (this.reportViewer.ReportSource != null)
        //    {
        //        try
        //        {
        //            reportProcessor.PrintReport(this.reportViewer.ReportSource, printerSettings);
        //            //bPrinted = true;
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //    }
        //    else
        //    {
        //        string str = "Beim Drucken ist ein Fehler aufgetreten. Der Vorgang wird beendet! Setzen Sie sich mit dem Support in Verbindung." + Environment.NewLine;
        //    }

        //}

        //private void PrintDirectToPDF()
        //{
        //    Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
        //    // set any deviceInfo settings if necessary
        //    System.Collections.Hashtable deviceInfo = new System.Collections.Hashtable();
        //    Telerik.Reporting.TypeReportSource typeReportSource = new Telerik.Reporting.TypeReportSource();

        //    // reportName is the Assembly Qualified Name of the report
        //    typeReportSource.TypeName = PDFFileNameTemp;
        //    Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", uriReportSource, deviceInfo);

        //    try
        //    {
        //        using (System.IO.FileStream fs = new System.IO.FileStream(PDFFilePathTemp, FileMode.Create))
        //        {
        //            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = ex.ToString();
        //    }

        //}
    }
}
