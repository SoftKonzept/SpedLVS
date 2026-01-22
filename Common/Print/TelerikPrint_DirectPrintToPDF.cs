using System;
using System.IO;
using Telerik.Reporting;
using LVS;

namespace Common.Print
{
    public class TelerikPrint_DirectPrintToPDF
    {
        public string PdfFilePath { get; set; } = string.Empty;
        public string PdfFileName { get; set; } = string.Empty;

        internal UriReportSource UriReportSource { get; set; }
        public bool Success { get; set; } = false;  
        public TelerikPrint_DirectPrintToPDF(UriReportSource myUriReportSource, string myPdfFilePath) 
        {
            Success = false;
            PdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + System.IO.Path.GetFileName(myPdfFilePath);
            PdfFilePath = System.IO.Path.Combine(clsReportDocSetting.const_localTempPDFReportPath, PdfFileName);
            UriReportSource = myUriReportSource;
            
            if (
                    (UriReportSource != null) &&
                    (PdfFilePath.Length > 0)
               )
            {
                PrintDirectToPDF();
            }
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

            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(PdfFilePath, FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }
                Success = true;
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                Success = false;
            }
        }


    }
}
