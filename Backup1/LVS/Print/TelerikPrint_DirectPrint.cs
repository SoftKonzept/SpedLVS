using Common.Enumerations;
using Common.Helper;
using LVS.InitValueLvsPrinterService;
using LVS.InitValue;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;

using Telerik.Reporting;
using Telerik.ReportViewer.WinForms;

namespace LVS.Print
{
    public class TelerikPrint_DirectPrint
    {
        internal ReportViewer reportViewer;

        //internal TelerikUriReportSource uriReportSource { get; set; } = new TelerikUriReportSource();
        internal enumDokumentenArt DocumentPrintArt { get; set; } = enumDokumentenArt.NotSet;
        internal string DocumentPrintArtString { get; set; } = string.Empty;
        public Globals._GL_USER GLUser { get; set; }
        public clsSystem Sys { get; set; }
        internal string PaperSource { get; set; }
        public string Error { get; set; } = string.Empty;
        public string PrinterName { get; set; } = string.Empty;
        public int iPrintCount { get; set; } = 1;
        public bool Success { get; set; } = false;

        internal ErrorAnalyse log { get; set; }

        public TelerikPrint_DirectPrint(ReportViewer myReportViewer, Globals._GL_USER myGLUser, clsSystem mySystem, enumDokumentenArt myPrintDocArt)
        {
            log = new ErrorAnalyse();
            log.AddLog(Environment.NewLine);
            log.AddLog("CL elerikPrint_DirectPrint" + Environment.NewLine);


            try
            {
                this.reportViewer = myReportViewer;
                this.PrinterName = string.Empty;
                this.PaperSource = string.Empty;
                this.GLUser = myGLUser;
                this.Sys = mySystem;
                this.DocumentPrintArt = myPrintDocArt;
                this.DocumentPrintArtString = StringToEnumPrintDocumentConverter.ConvertToString(myPrintDocArt);

                Success = false;
                DirektPrint();
            }
            catch (Exception ex)
            {
                string Error = "TelerikPrint_DirectPrint - Fehler im Druck [TelerikPrint_DirectPrint]";
                clsError error = new clsError();
                error.InitClass(GLUser, this.Sys);
                error.Code = clsError.code6_101;
                error.Aktion = "TelerikPrint_DirectPrint - Zeile 54";
                error.ErrorText = Error;
                error.Details = ex.Message;

                error.Details = error.Details + Environment.NewLine;
                error.Details += "Log:" + Environment.NewLine;
                foreach (string s in log.Log)
                {
                    if (!s.Equals(string.Empty))
                    {
                        error.Details += " -> " + s + Environment.NewLine;
                    }
                }

                error.SQLString = string.Empty;
                error.WriteError();

                if (InitValue_Settings.LogEnabled())
                {
                    log.WriteToHdd_Log();
                }
            }
        }
        public TelerikPrint_DirectPrint(ReportViewer myReportViewer,
                                        Globals._GL_USER myGLUser,
                                        clsSystem mySystem,
                                        string myPrintDocArtValue,
                                        string myPrinterName,
                                        string myPaperSource
                                        )
        {
            log = new ErrorAnalyse();
            log.AddLog(Environment.NewLine);
            log.AddLog(Environment.NewLine);
            log.AddLog("Funktion: TelerikPrint_DirectPrint" + Environment.NewLine);

            try
            {
                this.reportViewer = myReportViewer;
                this.PrinterName = myPrinterName;
                this.PaperSource = myPaperSource;
                this.GLUser = myGLUser;
                this.Sys = mySystem;
                this.DocumentPrintArtString = myPrintDocArtValue;
                this.DocumentPrintArt = StringToEnumPrintDocumentConverter.ConvertToEnum(DocumentPrintArtString);
                Success = false;

                DirektPrint();
            }
            catch (Exception ex)
            {
                string Error = "TelerikPrint - Fehler im Druck [TelerikPrint_DirectPrint]";
                clsError error = new clsError();
                error.InitClass(GLUser, this.Sys);
                error.Code = clsError.code6_101;
                error.Aktion = "TelerikPrint_DirectPrint - Zeile 85";
                error.ErrorText = Error;
                error.Details = ex.Message;

                error.Details = error.Details + Environment.NewLine;
                error.Details += "Log:" + Environment.NewLine;
                foreach (string s in log.Log)
                {
                    if (!s.Equals(string.Empty))
                    {
                        error.Details += "- " + s + Environment.NewLine;
                    }
                }

                error.SQLString = string.Empty;
                error.WriteError();

                if (InitValue_Settings.LogEnabled())
                {
                    log.WriteToHdd_Log();
                }
            }
        }

        private void DirektPrint()
        {
            try
            {
                log.AddLog("DirektPrint procedure");

                List<string> printerList = PrinterSettings_Printer.GetPrinter();
                enumAppType appType = InitValue.InitValue_GlobalSettings.AppType();

                switch (appType)
                {
                    case enumAppType.LvsPrintService:
                        log.AddLog(Environment.NewLine);
                        log.AddLog("Funktion: PrinterSettings_Printer.GetPrinter();");

                        foreach (string s in printerList)
                        {
                            log.AddLog(" |-> Drucker: " + s);
                        }
                        log.AddLog(Environment.NewLine);

                        if (PrinterName.Equals(string.Empty))
                        {
                            //PrinterName = InitValueLvsPrinterService.InitValue.InitValue_PrintServicePrinter_Default.DefaultPrinter();
                            PrinterName = LVS.InitValueLvsPrinterService.InitValue_PrintServicePrinter_Default.DefaultPrinter();
                            log.AddLog("DefaultPrinter INI: " + PrinterName);
                        }
                        else
                        {
                            log.AddLog("Printer aus PrintQueue: " + PrinterName);
                        }
                        break;
                    case enumAppType.Sped4:

                        //PrinterName = string.Empty;

                        break;
                }

                // Obtain the settings of the default printer
                System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
                if (PrinterName != string.Empty)
                    printerSettings.PrinterName = PrinterName;

                PaperSource pkSource = null;
                string strPaperSource = string.Empty;

                for (int j = 0; j < printerSettings.PaperSources.Count; j++)
                {

                    if (printerSettings.PaperSources[j].SourceName == PaperSource)
                        pkSource = printerSettings.PaperSources[j];

                }
                if (pkSource != null)
                {
                    log.AddLog("(pkSource != null)");
                    printerSettings.DefaultPageSettings.PaperSource = pkSource;

                    log.AddLog("pkSource" + pkSource.SourceName);
                }
                // The standard print controller comes with no UI
                System.Drawing.Printing.PrintController standardPrintController = new System.Drawing.Printing.StandardPrintController();
                // Print the report using the custom print controller
                Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                reportProcessor.PrintController = standardPrintController;

                //Druck des Reports
                if (this.reportViewer.ReportSource != null)
                {
                    try
                    {
                        log.AddLog("standardPrintController:  " + standardPrintController.ToString());
                        log.AddLog("Printer: " + printerSettings.PrinterName);
                        log.AddLog("PrintReport");

                        if (InitValue_Settings.CheckPrinterList())
                        {
                            List<string> printerForAccess = InitValue_Printer.Printer();
                            if (printerForAccess.Count > 0)
                            {
                                if (printerForAccess.Contains(printerSettings.PrinterName))
                                {
                                    reportProcessor.PrintReport(this.reportViewer.ReportSource, printerSettings);
                                    Success = true;
                                    log.AddLog("Success");
                                }
                                else
                                {
                                    Success = false;
                                    log.AddLog(PrinterName + " ist nicht freigeben!");
                                }
                            }
                            else
                            {
                                Success = false;
                                log.AddLog("keine Printer freigeben!");
                            }
                        }
                        else
                        {
                            reportProcessor.PrintReport(this.reportViewer.ReportSource, printerSettings);
                            Success = true;
                            log.AddLog("Success");
                        }

                    }
                    catch (Exception ex)
                    {
                        Error = "TelerikPrint_DirectPrint - Fehler im Druck [DirectPrint]";
                        clsError error = new clsError();
                        error.InitClass(GLUser, this.Sys);
                        error.Code = clsError.code6_101;
                        error.Aktion = "DirektPrint - Zeile 163";

                        string strTmp1 = string.Empty;
                        string strTmp2 = string.Empty;
                        string tabFormat = "{0}\t{1}";
                        string tabFormat1 = "{0}\t\t{1}";
                        //-- Details
                        strTmp1 = "Printer:";
                        strTmp2 = "NULL";
                        if (printerSettings.PrinterName != null)
                        {
                            strTmp2 = "[" + printerSettings.PrinterName + "]";
                        }
                        error.Details = error.Details + String.Format(tabFormat1, strTmp1, strTmp2) + Environment.NewLine;

                        strTmp1 = "PaperSource:";
                        strTmp2 = "NULL";
                        if (printerSettings.DefaultPageSettings.PaperSource != null)
                        {
                            strTmp2 = "[" + printerSettings.DefaultPageSettings.PaperSource.ToString() + "]";
                        }
                        error.Details = error.Details + String.Format(tabFormat1, strTmp1, strTmp2) + Environment.NewLine;

                        strTmp1 = "Dokumentenart:";
                        strTmp2 = "NULL";
                        if (this.DocumentPrintArtString != null)
                        {
                            strTmp2 = "[" + this.DocumentPrintArtString + "]";
                        }
                        strTmp2 = "[" + this.DocumentPrintArtString + "]";
                        error.Details = error.Details + String.Format(tabFormat, strTmp1, strTmp2) + Environment.NewLine;

                        strTmp1 = "Report Source:";
                        strTmp2 = "NULL";
                        if (this.reportViewer.ReportSource != null)
                        {
                            strTmp2 = "[" + this.reportViewer.ReportSource.ToString() + "]";
                        }
                        error.Details = error.Details + String.Format(tabFormat1, strTmp1, strTmp2) + Environment.NewLine;

                        strTmp1 = "Parameter:";
                        error.Details = error.Details + String.Format(tabFormat1, strTmp1, string.Empty) + Environment.NewLine;
                        if (this.reportViewer.ReportSource != null)
                        {
                            foreach (Parameter item in this.reportViewer.ReportSource.Parameters)
                            {
                                error.Details = error.Details + String.Format(tabFormat1, item.Name, item.Value) + Environment.NewLine;
                            }
                        }

                        strTmp1 = "Log:";
                        strTmp2 = string.Empty;
                        error.Details = error.Details + String.Format(tabFormat1, strTmp1, strTmp2) + Environment.NewLine;
                        foreach (string s in log.Log)
                        {
                            if (!s.Equals(string.Empty))
                            {
                                error.Details += "- " + s + Environment.NewLine;
                            }
                        }
                        error.SQLString = string.Empty;

                        error.WriteError();

                        Error = Error + Environment.NewLine + error.Details;
                        Success = false;

                        if (InitValue_Settings.LogEnabled())
                        {
                            log.WriteToHdd_Log();
                        }
                    }
                }
                else
                {
                    log.AddLog("Error" + Error + Environment.NewLine);
                    Error = "Beim Drucken ist ein Fehler aufgetreten. Der Vorgang wird beendet! Setzen Sie sich mit dem Support in Verbindung." + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                string Error = "TelerikPrint_DirectPrint - Fehler im Druck [DirektPrint]";
                clsError error = new clsError();
                error.InitClass(GLUser, this.Sys);
                error.Code = clsError.code6_101;
                error.Aktion = "DirektPrint";
                error.ErrorText = Error;
                error.Details = ex.Message;


                error.Details = error.Details + Environment.NewLine;
                error.Details += "Log:" + Environment.NewLine;
                foreach (string s in log.Log)
                {
                    if (!s.Equals(string.Empty))
                    {
                        error.Details += "- " + s + Environment.NewLine;
                    }
                }

                error.SQLString = string.Empty;
                error.WriteError();

                if (InitValue_Settings.LogEnabled())
                {
                    log.WriteToHdd_Log();
                }
            }

            log.AddLog("ENDE Prozess: TelerikPrint_DirektPrint");
            log.AddLog(Environment.NewLine);

        }
    }
}
