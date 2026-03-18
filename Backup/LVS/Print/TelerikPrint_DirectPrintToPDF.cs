using LVS.Models;
using LVS.ViewData;
using LVS.ZUGFeRD;
using PdfSharp.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Telerik.Reporting;

namespace LVS.Print
{
    public class TelerikPrint_DirectPrintToPDF
    {
        public string AttachmentFilePath { get; set; } = string.Empty;
        public string PdfFilePath { get; set; } = string.Empty;
        public string PdfFileName { get; set; } = string.Empty;
        internal InvoiceViewData InvoiceViewData { get; set; }
        public List<string> LogMessages { get; set; } = new List<string>();
        internal UriReportSource UriReportSource { get; set; }
        public bool Success { get; set; } = false;

        public ErrorAnalyse log = new ErrorAnalyse();
        int iCol0Width = 40;
        int iCol1Width = 120;

        public TelerikPrint_DirectPrintToPDF(UriReportSource myUriReportSource, string myPdfFilePath, bool myEditPdfFilePath = true, bool myIsEInvoice = false, int myInvoiceId = 0)
        {
            LogMessages = new List<string>();
            LogMessages.Add("-" + Environment.NewLine);
            LogMessages.Add("---> TelerikPrint_DirectPrintToPDF");
            LogMessages.Add("     |- Z 30 -  TelerikPrint_DirectPrintToPDF(UriReportSource myUriReportSource, string myPdfFilePath, bool myEditPdfFilePath = true, bool myIsEInvoice = false, int myInvoiceId = 0)");
            //LogMessages.Add("Auftruf clsFaktLager.cs -> CreateRGAndRGAnhangToPDF(clsSystem mySys)");
            //LogMessages.Add("       -> Ziel  this.FaktLager.CreateRGAndRGAnhangToPDF(this._ctrMenu._frmMain.system)");
            LogMessages.Add("     |-> Paremter:");
            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "myUriReportSource", myUriReportSource.ToString()));
            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "myPdfFilePath", myPdfFilePath));
            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "myEditPdfFilePath", myEditPdfFilePath));
            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "myIsEInvoice", myIsEInvoice.ToString()));
            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "myInvoiceId", myInvoiceId));

            AttachmentFilePath = string.Empty;

            log.AddLog(Environment.NewLine);
            log.AddLog(Environment.NewLine);
            log.AddLog("TelerikPrint_DirectPrintToPDF" + Environment.NewLine);

            Success = false;

            if (myEditPdfFilePath)
            {
                PdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + System.IO.Path.GetFileName(myPdfFilePath);

                string pdfPath = System.IO.Path.GetDirectoryName(myPdfFilePath);
                if (System.IO.Directory.Exists(pdfPath))
                {
                    PdfFilePath = System.IO.Path.Combine(pdfPath, PdfFileName);
                }
            }
            else
            {
                PdfFileName = System.IO.Path.GetFileName(myPdfFilePath);
                PdfFilePath = myPdfFilePath;
            }

            //-------- Print PDF 
            UriReportSource = myUriReportSource;
            if (
                    (UriReportSource != null) &&
                    (PdfFilePath.Length > 0)
               )
            {
                log.AddLog("PrintDirectToPDF" + Environment.NewLine);
                PrintDirectToPDF();
            }

            if (myIsEInvoice)
            {
                /// --MR eRechnung Test
                myPdfFilePath = PdfFilePath; // @"D:\\PdfDateien\TextXml.xml";
                InvoiceViewData = new InvoiceViewData(myInvoiceId, 1);

                //-- E-Rechnung Check
                if (InvoiceViewData.ZugferdCheck.IsZUGFeRDAvailable)
                {
                    //ZUGFeRDInvoice zugf = new ZUGFeRDInvoice(myPdfFilePath, InvoiceViewData);
                    //AttachmentFilePath = zugf.AttachmentPath;
                    //log.AddLog("Created E-Rechnung ZUGFeRD" + Environment.NewLine);
                    try
                    {
                        ZUGFeRDInvoice zugf = new ZUGFeRDInvoice(myPdfFilePath, InvoiceViewData);
                        AttachmentFilePath = zugf.AttachmentPath;
                        LogMessages.AddRange(zugf.LogMessages);
                        log.AddLog("Created E-Rechnung ZUGFeRD" + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
                        clsMail ErrorMail = new clsMail();
                        ErrorMail.InitClass(new Globals._GL_USER(), null);
                        ErrorMail.Subject = "TelerikPrint_DirectPrintToPDF | ZUGFeRDInvoice - Error Mail E-Rechnung";

                        string strMes = "Exception bei Aufruf ZUGFeRDInvoice [TelerikPrint_DirectPrintToPDF > Zeile 84]" + Environment.NewLine;

                        strMes += Environment.NewLine + Environment.NewLine;
                        strMes += "-----------------------------------" + Environment.NewLine;
                        strMes += "ZUGFeRDInvoice";
                        strMes += "Paremter:";
                        strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "myUriReportSource:".PadRight(iCol0Width), myUriReportSource.ToString());
                        strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "myPdfFilePath:".PadRight(iCol0Width), myPdfFilePath);
                        strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "myEditPdfFilePath:".PadRight(iCol0Width), myEditPdfFilePath.ToString());
                        strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "myInvoiceId:".PadRight(iCol0Width), myInvoiceId.ToString());

                        strMes += "-----------------------------------" + Environment.NewLine;
                        strMes += "ZUGFeRDInvoice";
                        strMes += "Paremter:";
                        strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "myPdfFilePath:".PadRight(iCol0Width), myPdfFilePath);
                        if ((InvoiceViewData is InvoiceViewData) && (InvoiceViewData.Invoice is Invoices))
                        {
                            strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "InvoiceId:".PadRight(iCol0Width), InvoiceViewData.Invoice.Id);
                            strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "Invoice Nr:".PadRight(iCol0Width), InvoiceViewData.Invoice.InvoiceNo);
                            strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "Receiver:".PadRight(iCol0Width), InvoiceViewData.Invoice.AdrReceiver.AddressStringShort);
                        }
                        else
                        {
                            strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "InvoiceViewData".PadRight(iCol0Width), "Null");
                        }


                        strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "AttachmentFilePath:".PadRight(iCol0Width), AttachmentFilePath);

                        strMes += ">>>" + Environment.NewLine;
                        strMes += ">>> ex.Message:" + Environment.NewLine;
                        strMes += ex.Message;
                        strMes += ">>> ex.InnerException:" + Environment.NewLine;
                        strMes += ex.InnerException.ToString();

                        strMes += ">>> Log aus ZUGFeRDInvoice" + Environment.NewLine;
                        foreach(string logMsg in LogMessages)
                        {
                            strMes += logMsg + Environment.NewLine;
                        }

                        ErrorMail.Message = strMes;
                        ErrorMail.SendError();
                    }
                }
                else
                {
                    AttachmentFilePath = PdfFilePath;

                    //try
                    //{
                    //    clsMail ErrorMail = new clsMail();
                    //    ErrorMail.InitClass(new Globals._GL_USER(), null);
                    //    ErrorMail.Subject = "TelerikPrint_DirectPrintToPDF - Error Mail E-Rechnung";

                    //    string strMes = "Info zur Rechnung: " + Environment.NewLine;
                    //    strMes += "RG:           " + InvoiceViewData.Invoice.InvoiceNo + "[" + InvoiceViewData.Invoice.Id + "]" + Environment.NewLine;
                    //    strMes += "Datum:        " + InvoiceViewData.Invoice.Datum.ToString("dd.MM.yyyy") + Environment.NewLine;
                    //    strMes += "Empfänger Id: " + InvoiceViewData.Invoice.Receiver + Environment.NewLine;
                    //    strMes += Environment.NewLine;
                    //    strMes += "InvoiceViewData.ZugferdCheck: " + Environment.NewLine;

                    //    if (InvoiceViewData.ZugferdCheck.LogCheck.Length > 0)
                    //    {
                    //        strMes += "LogCheck: " + Environment.NewLine;
                    //        strMes += InvoiceViewData.ZugferdCheck.LogCheck + Environment.NewLine;
                    //    }
                    //    ErrorMail.Message = strMes;
                    //    ErrorMail.SendError();
                    //}
                    //catch (Exception ex)
                    //{ }
                }
            }

            log.AddLog("ENDE TelerikPrint_DirectPrintToPDF" + Environment.NewLine);
            log.AddLog(Environment.NewLine);
        }

        public TelerikPrint_DirectPrintToPDF(UriReportSource myUriReportSource, string myPdfFilePath, bool myEditPdfFilePath = true)
        {
            log.AddLog(Environment.NewLine);
            log.AddLog(Environment.NewLine);
            log.AddLog("TelerikPrint_DirectPrintToPDF" + Environment.NewLine);

            Success = false;

            if (myEditPdfFilePath)
            {
                PdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + System.IO.Path.GetFileName(myPdfFilePath);
                string pdfPath = System.IO.Path.GetDirectoryName(myPdfFilePath);
                if (System.IO.Directory.Exists(pdfPath))
                {
                    PdfFilePath = System.IO.Path.Combine(pdfPath, PdfFileName);
                }
            }
            else
            {
                PdfFileName = System.IO.Path.GetFileName(myPdfFilePath);
                PdfFilePath = myPdfFilePath;
            }

            UriReportSource = myUriReportSource;

            if (
                    (UriReportSource != null) &&
                    (PdfFilePath.Length > 0)
               )
            {
                log.AddLog("PrintDirectToPDF" + Environment.NewLine);
                PrintDirectToPDF();
                AttachmentFilePath = PdfFilePath;
            }

            log.AddLog("ENDE TelerikPrint_DirectPrintToPDF" + Environment.NewLine);
            log.AddLog(Environment.NewLine);
        }



        private void PrintDirectToPDF()
        {
            Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
            // set any deviceInfo settings if necessary
            System.Collections.Hashtable deviceInfo = new System.Collections.Hashtable();
            Telerik.Reporting.TypeReportSource typeReportSource = new Telerik.Reporting.TypeReportSource();

            // reportName is the Assembly Qualified Name of the report
            typeReportSource.TypeName = PdfFileName;
            Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", UriReportSource, deviceInfo);

            log.AddLog("Write PDF" + Environment.NewLine);

            try
            {
                //Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                //// set any deviceInfo settings if necessary
                //System.Collections.Hashtable deviceInfo = new System.Collections.Hashtable();
                //Telerik.Reporting.TypeReportSource typeReportSource = new Telerik.Reporting.TypeReportSource();

                //// reportName is the Assembly Qualified Name of the report
                //typeReportSource.TypeName = PdfFileName;
                //Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", UriReportSource, deviceInfo);

                //log.AddLog("Write PDF" + Environment.NewLine);

                using (System.IO.FileStream fs = new System.IO.FileStream(PdfFilePath, FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }
                Success = true;
                log.AddLog("Write PDF - Success" + Environment.NewLine);


                //-- 
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                log.AddLog("Error: " + ex.ToString() + Environment.NewLine);
                Success = false;
                log.AddLog("Write PDF - NOT Success" + Environment.NewLine);
            }


        }


    }
}
