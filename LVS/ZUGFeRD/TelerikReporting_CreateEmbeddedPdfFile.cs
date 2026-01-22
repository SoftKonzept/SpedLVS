using DocumentFormat.OpenXml.Wordprocessing;
using LVS.Models;
using LVS.ViewData;
using PdfSharp.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Threading;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Editing;

namespace LVS.ZUGFeRD
{
    public class TelerikReporting_CreateEmbeddedPdfFile
    {
        public List<string> LogMessages { get; set; } = new List<string>();
        public bool IsEmbeddedFileCreated { get; set; } = false;
        int iCol0Width = 40;
        int iCol1Width = 120;
        public TelerikReporting_CreateEmbeddedPdfFile(string sourceFilePathPdf, string xmlFilePath, string embeddedFilePath)
        {
            try
            {
                LogMessages = new List<string>();
                LogMessages.Add("-" + Environment.NewLine);
                LogMessages.Add("---> Start TelerikReporting_CreateEmbeddedPdfFile     XXXXX");
                LogMessages.Add("     |- Z 24 - TelerikReporting_CreateEmbeddedPdfFile(string sourceFilePathPdf, string xmlFilePath, string embeddedFilePath)");
                LogMessages.Add("     |-> Paremter:");
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "sourceFilePathPdf", sourceFilePathPdf));
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "xmlFilePath", xmlFilePath));
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "embeddedFilePath", embeddedFilePath));

                //=======================================================================  Telerik
                ///// ---Create an instance of PdfFormatProvider
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "CultureInfo", Thread.CurrentThread.CurrentCulture.ToString()));

                TimeSpan tsTimeOut = new TimeSpan(0, 0, 30);

                //--- funktioniert
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Step: Import PDF started", string.Empty));
                RadFixedDocument pdfDoc = new RadFixedDocument();
                PdfFormatProvider pdfProvider = new PdfFormatProvider();
                using (Stream fsPdf = File.OpenRead(sourceFilePathPdf))
                {
                    pdfDoc = pdfProvider.Import(fsPdf, tsTimeOut);
                }
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Step: Import PDF finish", string.Empty));
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Step: Read XML started", string.Empty));
                byte[] bytes = File.ReadAllBytes(xmlFilePath);
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Step: XML size = "+  bytes.Length.ToString() +" bytes", string.Empty));

                //pdfDoc.EmbeddedFiles.AddZugferdInvoice(bytes);
                //pdfDoc.EmbeddedFiles.AddZugferdInvoice(bytes, Telerik.Windows.Documents.Fixed.Model.EmbeddedFiles.ZugferdConformanceLevel.Comfort); //  => Fehler beim Testen
                // NEU (richtig für XRechnung 3.0 / EN16931):
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Step: embed ZUGFeRD started", string.Empty));
                pdfDoc.EmbeddedFiles.AddZugferdInvoice(bytes, Telerik.Windows.Documents.Fixed.Model.EmbeddedFiles.ZugferdConformanceLevel.Extended);
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Step: embed ZUGFeRD finish", string.Empty));
                ////--- TEst neu
                //RadFixedDocument pdfDoc = new RadFixedDocument();
                //using (RadFixedDocumentEditor editor = new RadFixedDocumentEditor(pdfDoc))
                //{
                //    //editor.CharacterProperties.TrySetFont(new FontFamily("Calibri"));
                //    editor.InsertRun("PDF/A-3B Compliant Invoice");
                //}
                //byte[] bytes = File.ReadAllBytes(xmlFilePath);
                //pdfDoc.EmbeddedFiles.AddZugferdInvoice(bytes, Telerik.Windows.Documents.Fixed.Model.EmbeddedFiles.ZugferdConformanceLevel.Comfort);
                ////---- Ende Test neu

                // Export mit PDF/A-3B
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Step: Export PDF/A-3B started", string.Empty));
                PdfFormatProvider provider = new PdfFormatProvider();
                Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export.PdfExportSettings settings = new Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export.PdfExportSettings();
                settings.ComplianceLevel = PdfComplianceLevel.PdfA3B;  // PDF/A-3B lt Telerik
            
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "  ComplianceLevel", settings.ComplianceLevel));

                provider.ExportSettings = settings;
                using (Stream output = File.OpenWrite(embeddedFilePath))
                {
                    //provider.Export(epdfDoc, output, tsTimeOut);
                    provider.Export(pdfDoc, output, tsTimeOut);
                }
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Step: Export PDF/A-3B finish", string.Empty)); 

                IsEmbeddedFileCreated = File.Exists(embeddedFilePath);
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "IsEmbeddedFileCreated", IsEmbeddedFileCreated.ToString()));
            }
            catch (Exception ex)
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
                //LogFile.Error.Show(ex, "TelerikReporting_CreateEmbeddedPdfFile");
                //throw;
                IsEmbeddedFileCreated = false;

                LogMessages.Add("-"+Environment.NewLine);
                LogMessages.Add("===> Exception.Message");
                LogMessages.Add("-" + Environment.NewLine);
                LogMessages.Add(ex.Message);
                LogMessages.Add("-" + Environment.NewLine);
                LogMessages.Add("===> Exception.InnerException.Message");
                LogMessages.Add("-" + Environment.NewLine);
                LogMessages.Add(ex.InnerException.Message);

                clsMail ErrorMail = new clsMail();
                ErrorMail.InitClass(new Globals._GL_USER(), null);
                ErrorMail.Subject = "TelerikReporting_CreateEmbeddedPdfFile - Error Mail E-Rechnung";

                string strMes = "Exception bei Aufruf TelerikReporting_CreateEmbeddedPdfFile" + Environment.NewLine;

                strMes += Environment.NewLine + Environment.NewLine;
                strMes += "-----------------------------------" + Environment.NewLine;
                strMes += "TelerikReporting_CreateEmbeddedPdfFile";
                strMes += "Paremter:";
                strMes += string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "sourceFilePathPdf", sourceFilePathPdf);
                strMes += string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "sourceFilePathPdf", sourceFilePathPdf);
                strMes += string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "sourceFilePathPdf", sourceFilePathPdf);

                strMes += ">>>" + Environment.NewLine;
                strMes += ">>> ex.Message:" + Environment.NewLine;
                strMes += ex.Message;
                strMes += ">>> ex.InnerException:" + Environment.NewLine;
                strMes += ex.InnerException.ToString();


                strMes += ">>> Log aus TelerikReporting_CreateEmbeddedPdfFile" + Environment.NewLine;
                foreach (string logLine in LogMessages)
                {
                    strMes += logLine + Environment.NewLine;
                }
                ErrorMail.Message = strMes;
                ErrorMail.SendError();
            }
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
        }
    }
}
