using System;
using System.Drawing.Printing;
using Telerik.Reporting;

namespace LVS
{
    class clsPrint
    {

        ///<summary>clsPrint / DoAutoJournal</summary>
        ///<remarks></remarks>
        public static void PrintDirectToPDF(string myPDFName, string myFilePath, UriReportSource myRepSource)
        {
            Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
            // set any deviceInfo settings if necessary
            System.Collections.Hashtable deviceInfo = new System.Collections.Hashtable();
            Telerik.Reporting.TypeReportSource typeReportSource = new Telerik.Reporting.TypeReportSource();

            // reportName is the Assembly Qualified Name of the report
            typeReportSource.TypeName = myPDFName;
            Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", myRepSource, deviceInfo);

            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(myFilePath, System.IO.FileMode.Create))
                {
                    fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                }
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }

        }
        ///<summary>clsPrint / PrintDirect</summary>
        ///<remarks></remarks>
        public static void PrintDirect(Int32 iPrintCount, UriReportSource myRepSource)
        {
            //Druckanzahl - Schleifendurchläufe
            for (Int32 i = 1; i <= iPrintCount; i++)
            {
                // Obtain the settings of the default printer
                System.Drawing.Printing.PrinterSettings printerSettings = new System.Drawing.Printing.PrinterSettings();

                PaperSource pkSource = null;
                string strPaperSource = string.Empty;

                //for (int j = 0; j < printerSettings.PaperSources.Count; j++)
                //{

                //    if(printerSettings.PaperSources[j].SourceName == PaperSource)
                //    pkSource = printerSettings.PaperSources[j];

                //} 
                //if (pkSource != null)
                //{
                //    printerSettings.DefaultPageSettings.PaperSource = pkSource;
                //}    // CF

                // The standard print controller comes with no UI
                System.Drawing.Printing.PrintController standardPrintController = new System.Drawing.Printing.StandardPrintController();
                // Print the report using the custom print controller
                Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                reportProcessor.PrintController = standardPrintController;

                //Druck des Reports
                reportProcessor.PrintReport(myRepSource, printerSettings);
            }

        }
    }

}
