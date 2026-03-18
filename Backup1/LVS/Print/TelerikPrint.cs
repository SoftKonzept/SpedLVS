using Common.Enumerations;
using Common.Helper;
using Common.Models;
using LVS.InitValueLvsPrinterService;
using LVS.Constants;
using LVS.Helper;
using LVS.ViewData;
using System;
using System.Linq;
using Telerik.Reporting;

namespace LVS.Print
{
    public class TelerikPrint
    {
        internal Globals._GL_USER GL_USER { get; set; }
        internal Globals._GL_SYSTEM GLSystem { get; set; }
        internal clsSystem Sys { get; set; }

        internal string PaperSource { get; set; }
        internal string PrinterName { get; set; }
        internal string FilePathTemp { get; set; }

        internal string PDFFilePathTemp { get; set; }
        internal string PDFFileNameTemp { get; set; }
        public int PrintCount { get; set; } = 1;
        public string Text { get; set; } = string.Empty;
        //public enumDokumentenart DokumentenArt { get; set; }
        public enumDokumentenArt PrintDocumentArt { get; set; }
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

        internal ErrorAnalyse log { get; set; }
        public TelerikPrint()
        {
            aViewData = new ArchiveViewData();
            aViewData.Archive = new Archives();
            Article = new Articles();
            Eingang = new Eingaenge();
            Ausgang = new Ausgaenge();

            reportViewer = new Telerik.ReportViewer.WinForms.ReportViewer();

            log = new ErrorAnalyse();
            log.AddLog(Environment.NewLine);
            log.AddLog(Environment.NewLine);
            log.AddLog("CL - TelerikPrint()" + Environment.NewLine);
        }

        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, clsSystem mySystem, PrintQueues myPrintQueue)
        {
            try
            {
                log.AddLog("InitClass()");
                GL_USER = myGLUser;
                GLSystem = myGLSystem;
                Sys = mySystem;

                if (myPrintQueue.Id > 0)
                {
                    PrintCount = myPrintQueue.PrintCount;
                    PrinterName = myPrintQueue.PrinterName;
                }

                clsReportDocSetting ReportDocSetting = new clsReportDocSetting();
                ReportDocSetting.FillByAssId(myPrintQueue.ReportDocSettingId, myPrintQueue.ReportDocSettingAssignmentId);
                enumDokumentenArt tmpDocumentArt = StringToEnumPrintDocumentConverter.ConvertToEnum(ReportDocSetting.DocKey);

                PrintDocumentArt = StringToEnumPrintDocumentConverter.ConvertToEnum(ReportDocSetting.DocKey);
                //PrintCount = myPrintQueue.PrintCount;

                if (
                        //(1==2) &&
                        (ReportDocSetting is clsReportDocSetting) &&
                        (ReportDocSetting.ID > 0) &&
                        (ReportDocSetting.RSAId > 0) &&
                        (ReportDocSetting.ReportDataFile != null) &&
                        (ReportDocSetting.ReportDataFile.Length > 0)
                   )
                {
                    int iTmpAuftraggeber = 0;
                    int iTmpArbeitsbereichId = 0;
                    if (
                            (myPrintQueue.TableName.Equals(enumDatabaseSped4_TableNames.LEingang.ToString())) &&
                            (myPrintQueue.TableId > 0)
                       )
                    {
                        eingangViewData = new EingangViewData(myPrintQueue.TableId, (int)GL_USER.User_ID, true);
                        Eingang = eingangViewData.Eingang.Copy();
                        iTmpArbeitsbereichId = Eingang.ArbeitsbereichId;
                        iTmpAuftraggeber = Eingang.Auftraggeber;

                        aViewData.Archive.TableId = Eingang.Id;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                        aViewData.Archive.WorkspaceId = Eingang.ArbeitsbereichId;
                    }
                    else if (
                                (myPrintQueue.TableName.Equals(enumDatabaseSped4_TableNames.LAusgang.ToString())) &&
                                (myPrintQueue.TableId > 0)
                            )
                    {
                        ausgangViewData = new AusgangViewData(myPrintQueue.TableId, (int)GL_USER.User_ID, true);
                        Ausgang = ausgangViewData.Ausgang.Copy();
                        iTmpArbeitsbereichId = Ausgang.ArbeitsbereichId;
                        iTmpAuftraggeber = Ausgang.Auftraggeber;

                        aViewData.Archive.TableId = Ausgang.Id;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
                        aViewData.Archive.WorkspaceId = Ausgang.ArbeitsbereichId;
                    }
                    else if (
                                (myPrintQueue.TableName.Equals(enumDatabaseSped4_TableNames.Artikel.ToString())) &&
                                (myPrintQueue.TableId > 0)
                            )
                    {
                        articleViewData = new ArticleViewData(myPrintQueue.TableId, (int)GL_USER.User_ID, false);
                        Article = articleViewData.Artikel.Copy();
                        eingangViewData = new EingangViewData(Article.LEingangTableID, (int)GL_USER.User_ID, true);
                        Eingang = eingangViewData.Eingang.Copy();
                        if (Article.LAusgangTableID > 0)
                        {
                            ausgangViewData = new AusgangViewData(Article.LAusgangTableID, (int)GL_USER.User_ID, true);
                            Ausgang = ausgangViewData.Ausgang.Copy();
                        }
                        iTmpArbeitsbereichId = Article.AbBereichID;
                        iTmpAuftraggeber = Eingang.Auftraggeber;

                        aViewData.Archive.TableId = Article.Id;
                        aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Artikel.ToString();
                        aViewData.Archive.WorkspaceId = Article.AbBereichID;
                    }

                    log.AddLog("Filled Classes");

                    if (Printing(ReportDocSetting))
                    {
                        //PrintQueueViewData pVD = new PrintQueueViewData(myPrintQueue.Id, (int)GL_USER.User_ID);
                        //pVD.Delete();
                    }
                }
                else
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_USER;
                    error.Code = "";
                    error.Aktion = "TelerikPrint - Check ReportDocSetting";
                    error.Details += "ReportDocSetting" + Environment.NewLine;
                    error.Details += "ReportDocSetting.Id: " + ReportDocSetting.ID.ToString() + Environment.NewLine;
                    error.Details += "ReportDocSetting.DocKey.: " + ReportDocSetting.DocKey + Environment.NewLine;
                    error.Details += "ReportDocSetting.RSAId:" + ReportDocSetting.RSAId.ToString() + Environment.NewLine;
                    if (ReportDocSetting.ReportDataFile == null)
                    {
                        error.Details += "ReportDocSetting.ReportDataFile:  null" + Environment.NewLine;
                    }
                    error.Details += "PrintQueue" + Environment.NewLine;
                    error.Details += "PrintQueue.Id :" + myPrintQueue.Id.ToString() + Environment.NewLine;
                    error.Details += "PrintQueue.RepoortDocSettingId :" + myPrintQueue.ReportDocSettingId.ToString() + Environment.NewLine;
                    error.Details += "PrintQueue.RepoortDocSettingAssignmentId :" + myPrintQueue.ReportDocSettingAssignmentId.ToString() + Environment.NewLine;
                    error.Details += "PrintQueue.TableName: " + myPrintQueue.TableName.ToString() + Environment.NewLine;
                    error.Details += "PrintQueue.TableId: " + myPrintQueue.TableId.ToString() + Environment.NewLine;


                    error.Details = error.Details + Environment.NewLine;
                    error.Details += "Log:" + Environment.NewLine;
                    foreach (string s in log.Log)
                    {
                        error.Details += "- " + s + Environment.NewLine;
                    }

                    error.WriteError();

                    if (InitValue_Settings.LogEnabled())
                    {
                        log.WriteToHdd_Log();
                    }
                }
            }
            catch (Exception ex)
            {
                clsError error = new clsError();
                //error._GL_User = this.;
                error.Code = "";
                error.Aktion = "TelerikPrint - InitClass";
                error.exceptText = ex.ToString();

                error.Details = error.Details + Environment.NewLine;
                error.Details += "Log:" + Environment.NewLine;
                foreach (string s in log.Log)
                {
                    error.Details += "- " + s + Environment.NewLine;
                }

                error.WriteError();
            }

            log.AddLog("ENDE TelerikPrint" + Environment.NewLine);
            log.AddLog(Environment.NewLine);

            bool bWriteLog = InitValue_Settings.LogEnabled();
            if (bWriteLog)
            {
                log.WriteToHdd_Log();
            }
        }

        private bool Printing(clsReportDocSetting tmpSetting)
        {
            log.AddLog("Printing");

            bool retValue = false;
            if ((tmpSetting is clsReportDocSetting) && (tmpSetting.ID > 0))
            {
                //Report ermitteltn bzw. erstellen
                if ((tmpSetting.ReportDataFileExist) && (tmpSetting.ReportDataFile.Length > 0))
                {

                    //-- save Report as file to use the report
                    FilePathTemp = TelerikPrint.SaveReportFileToHDForUse(tmpSetting);

                    log.AddLog("FilePath: " + FilePathTemp);

                    if (!FilePathTemp.Equals(string.Empty))
                    {
                        //uriReportSource = new TelerikUriReportSource(FilePathTemp, Eingang.Id, Ausgang.Id, Article.Id, PrintDocumentArt, (int)this.GL_USER.User_ID);
                        //reportViewer.ReportSource = uriReportSource;
                        //reportViewer.Name = uriReportSource.DokumentName;
                        //log.AddListToLog(uriReportSource.log.Log);
                        //log.AddLog("uriReportSource.Uri: " + uriReportSource.Uri);                      

                        for (Int32 i = 1; i <= PrintCount; i++)
                        {
                            uriReportSource = new TelerikUriReportSource(FilePathTemp, Eingang.Id, Ausgang.Id, Article.Id, PrintDocumentArt, (int)this.GL_USER.User_ID);
                            reportViewer.ReportSource = uriReportSource;
                            reportViewer.Name = uriReportSource.DokumentName;


                            log.AddListToLog(uriReportSource.log.Log);

                            if (this.reportViewer == null)
                            {
                                log.AddLog("this.reportViewer is null ");
                            }
                            log.AddLog("GL_USER UserID:" + GL_USER.User_ID.ToString());
                            log.AddLog("PrintDocumentArt: " + PrintDocumentArt.ToString());

                            log.AddLog("--> TO TelerikPrint_DirectPrint ");

                            try
                            {
                                //this.reportViewer = null;
                                if (
                                        (this.reportViewer != null) &&
                                        (GL_USER.User_ID > 0) &&
                                        (!PrintDocumentArt.Equals(enumDokumentenArt.NotSet))
                                  )
                                {
                                    TelerikPrint_DirectPrint directPrint = new TelerikPrint_DirectPrint(this.reportViewer,
                                                                    GL_USER,
                                                                    Sys,
                                                                    PrintDocumentArt.ToString(),
                                                                    this.PrinterName,
                                                                    this.PaperSource
                                                                    );

                                    //TelerikPrint_DirectPrint directPrint = new TelerikPrint_DirectPrint(this.reportViewer, GL_USER, Sys, PrintDocumentArt);
                                    retValue = directPrint.Success;
                                    log.AddListToLog(directPrint.log.Log);
                                }
                            }
                            catch (Exception ex)
                            {
                                string Error = "TelerikPrint - TelerikPrint_DirectPrint - Fehler im Druck [DirectPrint]";
                                clsError error = new clsError();
                                error.InitClass(GL_USER, this.Sys);
                                error.Code = clsError.code6_101;
                                error.Aktion = "DirektPrint - Zeile 249";
                                error.ErrorText = Error;
                                error.Details = ex.Message;

                                error.Details = error.Details + Environment.NewLine;
                                error.Details += "Log:" + Environment.NewLine;
                                foreach (string s in log.Log)
                                {
                                    error.Details += "- " + s + Environment.NewLine;
                                }

                                error.SQLString = string.Empty;
                                error.WriteError();

                                if (InitValue_Settings.LogEnabled())
                                {
                                    log.WriteToHdd_Log();
                                }
                            }

                            log.AddLog("zurück in TelerikPrint ");
                        }
                    }
                    if (retValue)
                    {
                        log.AddLog("retValue: " + retValue.ToString());

                        if (Sys.Client.Modul.Archiv)
                        {

                            // - benötigte Daten (TableName, TableId, FileArt)
                            // -  TableName und TableId in InitClass
                            aViewData.Archive.FileArt = enumFileArt.PDF;
                            aViewData.Archive.ReportDocSettingId = tmpSetting.ID;
                            aViewData.Archive.ReportDocSettingAssignmentId = tmpSetting.RSAId;
                            aViewData.Archive.DocKey = tmpSetting.DocKey;
                            aViewData.Archive.DocKeyID = tmpSetting.DocKeyID;
                            aViewData.Archive.UserId = (int)GL_USER.User_ID;

                            if (!aViewData.ExistArchiveData())
                            {
                                //--- Report Export nach PDF File 
                                //PDFFilePathTemp = System.IO.Path.Combine(clsReportDocSetting.const_localTempPDFReportPath, System.IO.Path.ChangeExtension(tmpSetting.TempReportFileName, "pdf"));
                                PDFFilePathTemp = System.IO.Path.Combine(constValue_Report.const_localTempPDFReportPath, System.IO.Path.ChangeExtension(tmpSetting.TempReportFileName, "pdf"));
                                helper_IOFile.CheckPath(PDFFilePathTemp);

                                log.AddLog("PDFFilePathTemp: " + PDFFilePathTemp.ToString());
                                log.AddLog("to TelerikPrint_DirectPrintToPDF: " + Environment.NewLine);

                                TelerikPrint_DirectPrintToPDF printPdf = new TelerikPrint_DirectPrintToPDF(uriReportSource, PDFFilePathTemp, true);
                                retValue = printPdf.Success;
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

            log.AddLog("ENDE TelerikPrint" + Environment.NewLine);
            log.AddLog(Environment.NewLine);

            return retValue;
        }

        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, clsSystem mySystem, int myEingangTableId, int myAusgangTableId, int myArticleId, enumDokumentenArt myDocArt)
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

                aViewData.Archive.TableId = Eingang.Id;
                aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LEingang.ToString();
                aViewData.Archive.WorkspaceId = Eingang.ArbeitsbereichId;
            }
            if (myAusgangTableId > 0)
            {
                ausgangViewData = new AusgangViewData(myAusgangTableId, (int)GL_USER.User_ID, true);
                Ausgang = ausgangViewData.Ausgang.Copy();
                iTmpArbeitsbereichId = Ausgang.ArbeitsbereichId;
                iTmpAuftraggeber = Ausgang.Auftraggeber;

                aViewData.Archive.TableId = Ausgang.Id;
                aViewData.Archive.TableName = enumDatabaseSped4_TableNames.LAusgang.ToString();
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
                aViewData.Archive.TableName = enumDatabaseSped4_TableNames.Artikel.ToString();
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
                if (tmpSetting is null)
                {
                    tmpSetting = ReportDocSetting.ListReportDocSettingAll.FirstOrDefault(x => x.DocKey == PrintDocumentArt.ToString());
                }

                if ((tmpSetting is clsReportDocSetting) && (tmpSetting.ID > 0))
                {
                    //Report ermitteltn bzw. erstellen
                    if ((tmpSetting.ReportDataFileExist) && (tmpSetting.ReportDataFile.Length > 0))
                    {
                        //-- save Report as file to use the report
                        //FilePathTemp = System.IO.Path.Combine(clsReportDocSetting.const_localTempReportPath, tmpSetting.TempReportFileName);
                        //FileAndImageHelper.SaveByteArrayToFileWithStaticMethod(tmpSetting.ReportDataFile, FilePathTemp);

                        FilePathTemp = TelerikPrint.SaveReportFileToHDForUse(tmpSetting);
                        if (!FilePathTemp.Equals(string.Empty))
                        {
                            uriReportSource = new TelerikUriReportSource(FilePathTemp, Eingang.Id, Ausgang.Id, Article.Id, PrintDocumentArt, (int)this.GL_USER.User_ID);
                            reportViewer.ReportSource = uriReportSource;
                            reportViewer.Name = uriReportSource.DokumentName;

                            for (Int32 i = 1; i <= PrintCount; i++)
                            {
                                //TelerikPrint_DirectPrint directPrint = new TelerikPrint_DirectPrint(this.reportViewer, GL_USER, Sys, PrintDocumentArt);

                                TelerikPrint_DirectPrint directPrint = new TelerikPrint_DirectPrint(this.reportViewer,
                                                                        GL_USER,
                                                                        Sys,
                                                                        PrintDocumentArt.ToString(),
                                                                        this.PrinterName,
                                                                        this.PaperSource
                                                                        );
                            }
                        }

                        if (Sys.Client.Modul.Archiv)
                        {
                            // - benötigte Daten (TableName, TableId, FileArt)
                            // -  TableName und TableId in InitClass
                            aViewData.Archive.FileArt = enumFileArt.PDF;
                            aViewData.Archive.ReportDocSettingId = tmpSetting.ID;
                            aViewData.Archive.ReportDocSettingAssignmentId = tmpSetting.RSAId;
                            aViewData.Archive.DocKey = tmpSetting.DocKey;

                            if (!aViewData.ExistArchiveData())
                            {
                                //--- Report Export nach PDF File 
                                //PDFFilePathTemp = System.IO.Path.Combine(clsReportDocSetting.const_localTempPDFReportPath, System.IO.Path.ChangeExtension(tmpSetting.TempReportFileName, "pdf"));
                                PDFFilePathTemp = System.IO.Path.Combine(constValue_Report.const_localTempPDFReportPath, System.IO.Path.ChangeExtension(tmpSetting.TempReportFileName, "pdf"));
                                helper_IOFile.CheckPath(PDFFilePathTemp);
                                TelerikPrint_DirectPrintToPDF printPdf = new TelerikPrint_DirectPrintToPDF(uriReportSource, PDFFilePathTemp, true);

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
        }

        public static string SaveReportFileToHDForUse(clsReportDocSetting myTmpSetting)
        {
            string strReturnFilePathTemp = string.Empty;
            if (
                    (myTmpSetting is clsReportDocSetting) &&
                    (myTmpSetting.ID > 0) &&
                    (myTmpSetting.RSAId > 0) &&
                    (myTmpSetting.ReportDataFile != null) &&
                    (myTmpSetting.ReportDataFile.Length > 0)
               )
            {
                strReturnFilePathTemp = System.IO.Path.Combine(constValue_Report.const_localTempReportPath, myTmpSetting.TempReportFileName);
                //Check ob der Report bereits vorhanden ist, dann löschen
                if (System.IO.File.Exists(strReturnFilePathTemp))
                {
                    System.IO.File.Delete(strReturnFilePathTemp);
                }
                //--- speichern der Reportdatei
                helper_Image.SaveByteArrayToFileWithStaticMethod(myTmpSetting.ReportDataFile, strReturnFilePathTemp);
                if (!helper_IOFile.CheckFile(strReturnFilePathTemp))
                {
                    strReturnFilePathTemp = string.Empty;
                }
            }
            return strReturnFilePathTemp;
        }

    }
}
