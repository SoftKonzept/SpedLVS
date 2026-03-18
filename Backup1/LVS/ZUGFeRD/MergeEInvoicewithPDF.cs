using System;
using System.IO;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.Editing;


namespace LVS.ZUGFeRD
{
    public class MergeEInvoicewithPDF
    {
        internal string eInvoicePath { get; set; } = string.Empty;
        internal string eInvoiceFileName { get; set; } = string.Empty;
        internal string AttachmentPath { get; set; } = string.Empty;
        internal string AttachmentFileName { get; set; } = string.Empty;
        internal string OutputPath { get; set; } = string.Empty;

        public MergeEInvoicewithPDF(string eInvoiceFilePath, string attachmentFilePath, string outputPath)
        {
            OutputPath = outputPath;
            eInvoiceFileName = Path.GetFileName(eInvoiceFilePath);
            eInvoicePath = Path.GetDirectoryName(eInvoiceFilePath);
            AttachmentFileName = Path.GetFileName(attachmentFilePath);
            AttachmentPath = Path.GetDirectoryName(attachmentFilePath);

            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }
            if (
                (!eInvoiceFileName.Equals(string.Empty)) &&
                (!eInvoicePath.Equals(string.Empty)) &&
                (!AttachmentFileName.Equals(string.Empty)) &&
                (!AttachmentPath.Equals(string.Empty)) &&
                (!OutputPath.Equals(string.Empty))
              )
            {
                TimeSpan tsTimeOut = new TimeSpan(0, 0, 30);
                // Schritt 1: Beide PDFs laden
                PdfFormatProvider pdfProvider = new PdfFormatProvider();
                RadFixedDocument embeddedPdfDoc = new RadFixedDocument();
                using (Stream fsPdf = File.OpenRead(eInvoiceFilePath))
                {
                    embeddedPdfDoc = pdfProvider.Import(fsPdf, tsTimeOut);
                }

                pdfProvider = new PdfFormatProvider();
                RadFixedDocument attachmentPdfDoc = new RadFixedDocument();
                using (Stream fsPdf = File.OpenRead(attachmentFilePath))
                {
                    attachmentPdfDoc = pdfProvider.Import(fsPdf, tsTimeOut);
                }


                // Neues Dokument erstellen
                RadFixedDocument mergedDoc = new RadFixedDocument();
                RadFixedDocumentEditor editor = new RadFixedDocumentEditor(mergedDoc);


                // Neues Dokument erstellen und beide zusammenführen
                ;
                mergedDoc.Merge(embeddedPdfDoc);
                mergedDoc.Merge(attachmentPdfDoc);



                //if (embeddedPdfDoc.Pages.Count >0)
                //{
                //    foreach (RadFixedPage page in embeddedPdfDoc.Pages)
                //    {
                //        //RadFixedPage clonePage = page.Clone();
                //        //mergedDoc.Pages.Add(page);
                //        editor.InsertPage(page);
                //    }
                //}
                //if (attachmentPdfDoc.Pages.Count > 0)
                //{
                //    foreach (RadFixedPage page in attachmentPdfDoc.Pages)
                //        mergedDoc.Pages.Add(page);
                //}


                pdfProvider = new PdfFormatProvider();
                try
                {
                    File.WriteAllBytes(Path.Combine(OutputPath, "final_invoice.pdf"), pdfProvider.Export(mergedDoc, tsTimeOut));
                }
                catch (Exception ex)
                {
                    throw new IOException("Fehler beim Speichern der finalen PDF-Datei.", ex);
                }

                TelerikReporting_CreateEmbeddedPdfFile telerikReporting_CreateEmbeddedPdfFile = new TelerikReporting_CreateEmbeddedPdfFile(eInvoiceFilePath, attachmentFilePath, Path.Combine(OutputPath, "final_invoice.pdf"));
            }
        }

        public void MergeEInvoiceWithReport(string eInvoiceFilePath, string attachmentFilePath, string outputPath)
        {
            //eInvoiceFileName = Path.GetFileName(einvoiceFilePath);
            //eInvoicePath = Path.GetDirectoryName(einvoiceFilePath);

            //// Schritt 1: Beide PDFs laden
            //PdfFormatProvider provider = new PdfFormatProvider();
            //RadFixedDocument doc1 = provider.Import(File.ReadAllBytes("erechnung.pdf"));
            //RadFixedDocument doc2 = provider.Import(File.ReadAllBytes("anhang.pdf"));

            //// Schritt 2: Neue PDF erstellen und Seiten zusammenführen
            //RadFixedDocument mergedDoc = new RadFixedDocument();
            //foreach (RadFixedPage page in doc1.Pages)
            //    mergedDoc.Pages.Add(page);
            //foreach (RadFixedPage page in doc2.Pages)
            //    mergedDoc.Pages.Add(page);

            //// Schritt 3: XML-Datei einbetten
            //EmbeddedFile embeddedXml = new EmbeddedFile(File.ReadAllBytes("zugferd-invoice.xml"))
            //{
            //    MimeType = "text/xml"
            //    //MimeType = "application/xml",
            //    Description = "ZUGFeRD Invoice XML"Pro
            //};
            //mergedDoc.EmbeddedFiles.Add("zugferd-invoice.xml", embeddedXml);

            //// Schritt 4: PDF/A-3b exportieren
            //PdfExportSettings settings = new PdfExportSettings()
            //{
            //    ComplianceLevel = PdfComplianceLevel.PdfA3B
            //};

            //File.WriteAllBytes("final_invoice.pdf", provider.Export(mergedDoc, se

        }


    }


}
