using Common.Enumerations;
using Common.Helper;
using LVS;
using System;
using Telerik.Reporting;
using Telerik.ReportViewer.WinForms;

namespace Common.Print
{


    public class TelerikPrint_DirectPrint
    {
        internal ReportViewer reportViewer;
        internal TelerikUriReportSource uriReportSource;
        internal enumPrintDocumentArt DocumentPrintArt { get; set; } = enumPrintDocumentArt.NotSet;
        internal string DocumentPrintArtString { get; set; } = string.Empty;
        public Globals._GL_USER GLUser { get; set; }
        public clsSystem Sys { get; set; }
        internal string PaperSource { get; set; }
        public string Error { get; set; } = string.Empty;
        public string PrinterName { get; set; } = string.Empty;
        public bool Success { get; set; } = false;

        public TelerikPrint_DirectPrint(ReportViewer myReportViewer, Globals._GL_USER myGLUser, clsSystem mySystem, enumPrintDocumentArt myPrintDocArt)
        { 
            this.reportViewer = myReportViewer;
            this.GLUser = myGLUser;
            this.Sys = mySystem;
            this.DocumentPrintArt = myPrintDocArt;
            this.DocumentPrintArtString = StringToEnumPrintDocumentConverter.ConvertToString(myPrintDocArt);

            Success = false;
            DirektPrint();
        }
        public TelerikPrint_DirectPrint(ReportViewer myReportViewer, Globals._GL_USER myGLUser, clsSystem mySystem, string myPrintDocArtValue)
        {
            this.reportViewer = myReportViewer;
            this.GLUser = myGLUser;
            this.Sys = mySystem;
            this.DocumentPrintArtString = myPrintDocArtValue;
            this.DocumentPrintArt = StringToEnumPrintDocumentConverter.ConvertToEnum(DocumentPrintArtString);
            Success = false;
            DirektPrint();
        }

        private void DirektPrint()
        {
            //PrinterSettings printerSettings = new PrinterSettings();
            //// Obtain the settings of the default printer
            ////System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();
            //if (PrinterName != string.Empty)
            //    printerSettings.PrinterName = PrinterName;
            //PaperSource pkSource = null;
            //string strPaperSource = string.Empty;

            //for (int j = 0; j < printerSettings.PaperSources.Count; j++)
            //{

            //    if (printerSettings.PaperSources[j].SourceName == PaperSource)
            //        pkSource = printerSettings.PaperSources[j];

            //}
            //if (pkSource != null)
            //{
            //    printerSettings.DefaultPageSettings.PaperSource = pkSource;
            //}
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
                    reportProcessor.PrintReport(this.reportViewer.ReportSource, printerSettings);
                    Success = true;
                }
                catch (Exception ex)
                {
                    //Error = "Fehler im Druck [DirectPrint]";
                    //clsError error = new clsError();
                    //error.InitClass(GLUser, this.Sys);
                    //error.Code = clsError.code6_101;
                    //error.Aktion = "DirektPrint";

                    //string strTmp1 = string.Empty;
                    //string strTmp2 = string.Empty;
                    //string tabFormat = "{0}\t{1}";
                    //string tabFormat1 = "{0}\t\t{1}";
                    ////-- Details
                    //strTmp1 = "Printer:";
                    //strTmp2 = "NULL";
                    //if (printerSettings.PrinterName != null)
                    //{
                    //    strTmp2 = "[" + printerSettings.PrinterName + "]";
                    //}
                    //error.Details = error.Details + String.Format(tabFormat1, strTmp1, strTmp2) + Environment.NewLine;

                    //strTmp1 = "PaperSource:";
                    //strTmp2 = "NULL";
                    //if (printerSettings.DefaultPageSettings.PaperSource != null)
                    //{
                    //    strTmp2 = "[" + printerSettings.DefaultPageSettings.PaperSource.ToString() + "]";
                    //}
                    //error.Details = error.Details + String.Format(tabFormat1, strTmp1, strTmp2) + Environment.NewLine;

                    //strTmp1 = "Dokumentenart:";
                    //strTmp2 = "NULL";
                    //if (this.DocumentPrintArtString != null)
                    //{
                    //    strTmp2 = "[" + this.DocumentPrintArtString + "]";
                    //}
                    //strTmp2 = "[" + this.DocumentPrintArtString + "]";
                    //error.Details = error.Details + String.Format(tabFormat, strTmp1, strTmp2) + Environment.NewLine;

                    ////strTmp1 = "DocPath:";
                    ////strTmp2 = "NULL";
                    ////if (this. != null)
                    ////{
                    ////    strTmp2 = "[" + this.DocPath + "]";
                    ////}
                    ////error.Details = error.Details + String.Format(tabFormat1, strTmp1, strTmp2) + Environment.NewLine;

                    //strTmp1 = "Report Source:";
                    //strTmp2 = "NULL";
                    //if (this.reportViewer.ReportSource != null)
                    //{
                    //    strTmp2 = "[" + this.reportViewer.ReportSource.ToString() + "]";
                    //}
                    //error.Details = error.Details + String.Format(tabFormat1, strTmp1, strTmp2) + Environment.NewLine;

                    ////strTmp1 = "Uri:";
                    ////strTmp2 = "NULL";
                    ////if (this.reportViewer.ReportSource != null)
                    ////{
                    ////    strTmp2 = "[" + this.reportViewer.ReportSource. + "]";
                    ////}
                    //error.Details = error.Details + String.Format(tabFormat1, strTmp1, strTmp2) + Environment.NewLine;

                    //strTmp1 = "Parameter:";
                    //error.Details = error.Details + String.Format(tabFormat1, strTmp1, string.Empty) + Environment.NewLine;
                    //if (this.reportViewer.ReportSource != null)
                    //{
                    //    foreach (Parameter item in this.reportViewer.ReportSource.Parameters)
                    //    {
                    //        error.Details = error.Details + String.Format(tabFormat1, item.Name, item.Value) + Environment.NewLine;
                    //    }
                    //}
                    //error.exceptText = ex.ToString();
                    //error.SQLString = string.Empty;
                    //error.WriteError();

                    //Error = Error + Environment.NewLine + error.Details;
                    //clsMessages.Allgemein_InfoTextShow(Error + Environment.NewLine + ex.ToString());
                    //Success = false;
                }
            }
            else
            {
                Error = "Beim Drucken ist ein Fehler aufgetreten. Der Vorgang wird beendet! Setzen Sie sich mit dem Support in Verbindung." + Environment.NewLine;
            }

        }
    }
}
