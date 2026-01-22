using Common.Enumerations;
using System;
using Telerik.Reporting;

namespace LVS.Print
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
        internal enumDokumentenArt PrintDocArt { get; set; } = enumDokumentenArt.NotSet;

        public ErrorAnalyse log = new ErrorAnalyse();
        public TelerikUriReportSource(string myFilePath,
                                      int myEingangTableId,
                                      int myAusgangTableId,
                                      int myArticleId,
                                      enumDokumentenArt myPrintDocArt,
                                      int myUserId)
        {
            log = new ErrorAnalyse();
            log.AddLog(Environment.NewLine);
            log.AddLog(Environment.NewLine);
            log.AddLog("CL - TelerikUriReportSource()" + Environment.NewLine);

            FilePath = myFilePath;
            LEingangTableId = myEingangTableId;
            LAusgangTableId = myAusgangTableId;
            ArticleId = myArticleId;
            PrintDocArt = myPrintDocArt;
            UserId = myUserId;

            log.AddLog("- FilePath: " + FilePath);
            log.AddLog("- LEingangTableId: " + LEingangTableId);
            log.AddLog("- LAusgangTableId: " + LAusgangTableId);
            log.AddLog("- ArticleId: " + ArticleId);
            log.AddLog("- PrintDocArt: " + PrintDocArt);
            log.AddLog("- UserId: " + UserId);

            SetValue();
        }

        public TelerikUriReportSource(string myFilePath,
                                      int myRgTableId,
                                      enumDokumentenArt myPrintDocArt,
                                      int myUserId)
        {
            log = new ErrorAnalyse();
            log.AddLog(Environment.NewLine);
            log.AddLog(Environment.NewLine);
            log.AddLog("CL - TelerikUriReportSource()" + Environment.NewLine);

            FilePath = myFilePath;
            RgTableId = myRgTableId;
            UserId = myUserId;
            PrintDocArt = myPrintDocArt;

            log.AddLog("- FilePath: " + FilePath);
            log.AddLog("- LEingangTableId: " + LEingangTableId);
            log.AddLog("- LAusgangTableId: " + LAusgangTableId);
            log.AddLog("- ArticleId: " + ArticleId);
            log.AddLog("- PrintDocArt: " + PrintDocArt);
            log.AddLog("- UserId: " + UserId);

            SetValue();
        }

        public TelerikUriReportSource(string myFilePath,
                                      int myWorkspaceId,
                                      DateTime myZVon,
                                      DateTime myZBis,
                                      enumDokumentenArt myPrintDocArt = enumDokumentenArt.RGBuch)
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
            log.AddLog("SetValue()");
            this.Uri = FilePath;
            switch (PrintDocArt)
            {
                //---- Ausgang
                case enumDokumentenArt.Auslagerungsschein:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", LAusgangTableId));
                    this.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", UserId));
                    DokumentName = "Ausgangsschein";
                    break;
                case enumDokumentenArt.Auslagerungsanzeige:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", LAusgangTableId));
                    DokumentName = "Lagerausganganzeige";
                    break;
                case enumDokumentenArt.AusgangLfs:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", LAusgangTableId));
                    this.Parameters.Add(new Telerik.Reporting.Parameter("BenutzerID", UserId));
                    DokumentName = "Lieferschein Lagerausgang";
                    break;

                case enumDokumentenArt.KundenAusgangslieferschein: break;
                case enumDokumentenArt.Ausgangsliste:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", LAusgangTableId));
                    DokumentName = "Ausgangsliste";
                    break;
                case enumDokumentenArt.KVOFrachtbrief:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", LAusgangTableId));
                    DokumentName = "KVOFrachtbrief";
                    break;
                case enumDokumentenArt.CMRFrachtbrief:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LAusgangTableID", LAusgangTableId));
                    DokumentName = "CMRFrachtbrief";
                    break;




                //---- Eingang

                case enumDokumentenArt.Eingangsliste:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", LEingangTableId));
                    DokumentName = "Lagereingangsliste";
                    break;

                case enumDokumentenArt.Einlagerungsanzeige:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", LEingangTableId));
                    DokumentName = "Einlagerungsanzeige";
                    break;

                case enumDokumentenArt.Einlagerungsschein:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", LEingangTableId));
                    DokumentName = "Einlagerungsschein";
                    break;

                case enumDokumentenArt.LabelAll:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", LEingangTableId));
                    DokumentName = "LabelAll";
                    break;

                case enumDokumentenArt.LableOne:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("LEingangTableID", ArticleId));
                    DokumentName = "LableOne";
                    break;

                //---- Rechnung
                case enumDokumentenArt.Lagerrechnung:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", RgTableId));
                    DokumentName = "Rechnung";
                    break;
                case enumDokumentenArt.Speditionsrechnung: break;
                case enumDokumentenArt.RGAnhang: break;
                case enumDokumentenArt.Manuellerechnung:
                case enumDokumentenArt.ManuelleGutschrift:
                    this.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", RgTableId));
                    DokumentName = "Manuelle Rechnung";
                    break;
            }
            log.AddLog("UriReportRecource-Parameter:");
            foreach (var x in this.Parameters)
            {
                log.AddLog("  -" + x.Name + " - " + x.Value.ToString());
            }

        }
    }
}
