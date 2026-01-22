using Common.Enumerations;

namespace Common.Helper
{
    public class StringToEnumPrintDocumentConverter
    {
        public static enumDokumentenArt ConvertToEnum(string myValue)
        {
            enumDokumentenArt ReturnPrintDocumentArt = enumDokumentenArt.NotSet;
            switch (myValue)
            {
                // neue Druckversion
                case "LabelOne":
                    ReturnPrintDocumentArt = enumDokumentenArt.LableOne;
                    break;
                case "LOLabel":
                case "SchadenLable":
                case "SchadenLabel":
                    ReturnPrintDocumentArt = enumDokumentenArt.SchadenLabel;
                    break;

                case "SchadenDoc":
                    ReturnPrintDocumentArt = enumDokumentenArt.SchadenDoc;
                    break;
                case "SPLLabel":
                    ReturnPrintDocumentArt = enumDokumentenArt.SPLLabel;
                    break;
                case "SPLDoc":
                    ReturnPrintDocumentArt = enumDokumentenArt.SPLDoc;
                    break;
                case "LabelOneNeutral":
                    ReturnPrintDocumentArt = enumDokumentenArt.NotSet;
                    break;
                case "LabelAll":
                    ReturnPrintDocumentArt = enumDokumentenArt.LabelAll;
                    break;
                case "ArtikelListe":
                case "DocAuftragScan":
                case "ArtikelLabel":
                case "LagerEingangAnzeigeMail":
                case "LagerEingangAnzeigePerDay":
                case "LagerEingangAnzeigePerDayFull":
                case "LagerEingangAnzeigePerDayMail":
                case "LagerEingangLfsMail":
                case "LagerAusgangAnzeigeMail":
                case "LagerAusgangAnzeigePerDay":
                case "LagerAusgangAnzeigePerDayFull":
                case "LagerAusgangAnzeigePerDayMail":
                case "LagerAusgangLfsMail":
                case "RechnungMail":
                case "LagerRechnungMail":
                case "manuelleRGGSMail":
                case "Adressliste":
                case "manuelleRGGS":
                    ReturnPrintDocumentArt = enumDokumentenArt.NotSet;
                    break;

                case "EingangDoc":
                case "LagerEingangDoc":
                case "LagerEingangDocMat":
                    ReturnPrintDocumentArt = enumDokumentenArt.Einlagerungsschein;
                    break;

                case "EingangAnzeige":
                case "LagerEingangAnzeige":
                case "LagerEingangLfs":
                    ReturnPrintDocumentArt = enumDokumentenArt.Einlagerungsanzeige;
                    break;

                case "EingangLfs":
                case "EingangDocMat":
                    break;

                case "Eingangsliste":
                    ReturnPrintDocumentArt = enumDokumentenArt.Eingangsliste;
                    break;

                case "AusgangDoc":
                case "LagerAusgangDoc":
                case "LagerAusgangNeutralDoc":
                    ReturnPrintDocumentArt = enumDokumentenArt.Auslagerungsschein;
                    break;

                case "AusgangAnzeige":
                case "LagerAusgangAnzeige":
                    ReturnPrintDocumentArt = enumDokumentenArt.Auslagerungsanzeige;
                    break;

                case "AusgangLfs":
                case "LagerAusgangLfs":
                case "LagerAusgangLfsMat":
                    ReturnPrintDocumentArt = enumDokumentenArt.AusgangLfs;
                    break;

                case "Ausgangsliste":
                    ReturnPrintDocumentArt = enumDokumentenArt.Ausgangsliste;
                    break;

                case "CMRFrachtbrief":
                    ReturnPrintDocumentArt = enumDokumentenArt.CMRFrachtbrief;
                    break;

                case "KVOFrachtbrief":
                    ReturnPrintDocumentArt = enumDokumentenArt.KVOFrachtbrief;
                    break;

                case "Bestandsliste":
                    ReturnPrintDocumentArt = enumDokumentenArt.Bestand;
                    break;

                case "Inventur":
                    ReturnPrintDocumentArt = enumDokumentenArt.Inventur;
                    break;

                case "Journal":
                    ReturnPrintDocumentArt = enumDokumentenArt.Journal;
                    break;

                case "Rechnung":
                case "Gutschrift":
                case "LagerRechnung":
                case "Lagerrechnung":
                    ReturnPrintDocumentArt = enumDokumentenArt.Lagerrechnung;
                    break;

                case "Manuellerechnung":
                    ReturnPrintDocumentArt = enumDokumentenArt.Manuellerechnung;
                    break;

                case "ManuelleGutschrift":
                    ReturnPrintDocumentArt = enumDokumentenArt.ManuelleGutschrift;
                    break;

                case "RGBuch":
                    ReturnPrintDocumentArt = enumDokumentenArt.RGBuch;
                    break;

                case "RGAnhang":
                case "RGAnhangMat":
                    ReturnPrintDocumentArt = enumDokumentenArt.RGAnhang;
                    break;
            }
            return ReturnPrintDocumentArt;
        }

        public static string ConvertToString(enumDokumentenArt myPrintDocumentArt)
        {
            string strReturn = string.Empty;
            switch (myPrintDocumentArt)
            {
                //---- Ausgang
                case enumDokumentenArt.Auslagerungsschein:
                    strReturn = "LagerAusgangDoc";
                    break;
                case enumDokumentenArt.Auslagerungsanzeige:
                    strReturn = "LagerAusgangAnzeige";
                    break;
                case enumDokumentenArt.AusgangLfs:
                    strReturn = "LagerAusgangLfs";
                    break;

                case enumDokumentenArt.KundenAusgangslieferschein: break;
                case enumDokumentenArt.Ausgangsliste:
                    strReturn = "Ausgangsliste";
                    break;
                case enumDokumentenArt.KVOFrachtbrief:
                    strReturn = "KVOFrachtbrief";
                    break;
                case enumDokumentenArt.CMRFrachtbrief:
                    strReturn = "CMRFrachtbrief";
                    break;

                //---- Eingang
                case enumDokumentenArt.Einlagerungsschein:
                    strReturn = "LagerEingangDoc";
                    break;
                case enumDokumentenArt.Einlagerungsanzeige:
                    strReturn = "LagerEingangAnzeige";
                    break;
                case enumDokumentenArt.Eingangsliste:
                    strReturn = "Eingangsliste";
                    break;

                //---- Rechnung
                case enumDokumentenArt.Lagerrechnung:
                    strReturn = "Lagerrechnung";
                    break;
                case enumDokumentenArt.Speditionsrechnung: break;
                case enumDokumentenArt.RGAnhang:
                    strReturn = "RGAnhang";
                    break;
                case enumDokumentenArt.RGBuch:
                    strReturn = "RGBuch";
                    break;

                case enumDokumentenArt.Manuellerechnung:
                    strReturn = "Manuellerechnung";
                    break;
                case enumDokumentenArt.ManuelleGutschrift:
                    strReturn = "ManuelleGutschrift";
                    break;
                case enumDokumentenArt.NotSet:
                    strReturn = string.Empty;
                    break;
                default:
                    strReturn = string.Empty;
                    break;
            }

            return strReturn;
        }
    }
}
