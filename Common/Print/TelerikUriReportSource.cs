using Common.Enumerations;
using System;
using Telerik.Reporting;

namespace Common.Print
{
    public class TelerikUriReportSource : UriReportSource
    {
        public string DokumentName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string PaperSource { get; set; } = string.Empty;
        public int iPrintCount { get; set; } = 1;

        internal int LEingangTableId { get; set; } = 0;
        internal int LAusgangTableId { get; set; } = 0;
        internal int ArticleId { get; set; } = 0;
        internal int RgTableId { get; set; } = 0;
        internal int UserId { get; set; } = 0;
        internal enumPrintDocumentArt PrintDocArt { get; set; } = enumPrintDocumentArt.NotSet;

        public TelerikUriReportSource(string myFilePath, 
                                      int myEingangTableId, 
                                      int myAusgangTableId, 
                                      int myArticleId,
                                      enumPrintDocumentArt myPrintDocArt, 
                                      int myUserId) 
        {
            FilePath = myFilePath;
            LEingangTableId= myEingangTableId;
            LAusgangTableId= myAusgangTableId;
            ArticleId= myArticleId;
            PrintDocArt = myPrintDocArt;
            UserId = myUserId;
            SetValue();
        }

        public TelerikUriReportSource(string myFilePath,
                                      int myRgTableId,
                                      enumPrintDocumentArt myPrintDocArt,
                                      int myUserId)
        {
            FilePath = myFilePath;
            RgTableId = myRgTableId;
            UserId= myUserId;
            PrintDocArt = myPrintDocArt;
            SetValue();
        }

        public TelerikUriReportSource(string myFilePath,
                                      int myWorkspaceId,
                                      DateTime myZVon,
                                      DateTime myZBis,
                                      enumPrintDocumentArt myPrintDocArt = enumPrintDocumentArt.RGBuch)
        {
            FilePath = myFilePath;
            PrintDocArt = myPrintDocArt;
            this.Uri = FilePath;
            this.Parameters.Add(new Telerik.Reporting.Parameter("AbBereich", myWorkspaceId));
            this.Parameters.Add(new Telerik.Reporting.Parameter("ZVon", myZVon));
            this.Parameters.Add(new Telerik.Reporting.Parameter("ZBis", myZBis));
            DokumentName = "Rechnungsbuch";
        }

        private void SetValue()
        {
            this.Uri = FilePath;
            switch (PrintDocArt)
            {
                //---- Ausgang
                case enumPrintDocumentArt.Auslagerungsschein:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", LAusgangTableId));
                    this.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", UserId));
                    DokumentName = "Ausgangsschein";
                    break;
                case enumPrintDocumentArt.Auslagerungsanzeige:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", LAusgangTableId));
                    DokumentName = "Lagerausganganzeige";
                    break;
                case enumPrintDocumentArt.AusgangLfs:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", LAusgangTableId));
                    this.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", UserId));
                    DokumentName = "Lieferschein Lagerausgang";
                    break;

                case enumPrintDocumentArt.KundenAusgangslieferschein: break;
                case enumPrintDocumentArt.Ausgangsliste:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", LAusgangTableId));
                    DokumentName = "Ausgangsliste";
                    break;
                case enumPrintDocumentArt.KVOFrachtbrief:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", LAusgangTableId));
                    DokumentName = "KVOFrachtbrief";
                    break;
                case enumPrintDocumentArt.CMRFrachtbrief:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", LAusgangTableId));
                    DokumentName = "CMRFrachtbrief";
                    break;




                //---- Eingang

                case enumPrintDocumentArt.Einlagerungsschein: break;
                case enumPrintDocumentArt.Einlagerungsanzeige: break;
                case enumPrintDocumentArt.Eingangsliste:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", LEingangTableId));
                    DokumentName = "Lagereingangsliste";
                    break;




                //---- Rechnung
                case enumPrintDocumentArt.Lagerrechnung:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", RgTableId));
                    DokumentName = "Rechnung";
                    break;
                case enumPrintDocumentArt.Speditionsrechnung: break;
                case enumPrintDocumentArt.RGAnhang: break;
                case enumPrintDocumentArt.Manuellerechnung:
                case enumPrintDocumentArt.ManuelleGutschrift:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", RgTableId));
                    DokumentName = "Manuelle Rechnung";
                    break;



            }
        }
    }
}
