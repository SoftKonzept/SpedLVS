using Common.Models;
using System;

namespace LVS
{
    public class ArticleClassConverter
    {
        public static clsArtikel GetClsArtikelByArticels(Articles myArt)
        {
            clsArtikel articles = new clsArtikel();

            try
            {
                articles.ID = (int)myArt.Id;

                articles.AbrufReferenz = myArt.AbrufReferenz;
                articles.ADRLagerNr = (int)myArt.ADRLagerNr;
                articles.Anzahl = (int)myArt.Anzahl;
                articles.AbBereichID = (int)myArt.AbBereichID;
                articles.ASNVerbraucher = myArt.ASNVerbraucher;
                articles.ASNProduktionsnummer = myArt.ASNProduktionsnummer;
                articles.ArtIDAfterUB = (int)myArt.ArtIDAfterUB;
                articles.AuftragID = (int)myArt.AuftragID;
                articles.AuftragPos = (int)myArt.AuftragPos;
                articles.AuftragPosTableID = (int)myArt.AuftragPosTableID;
                articles.AusgangChecked = myArt.AusgangChecked;

                articles.ArtIDRef = myArt.ArtIDRef;
                articles.ArtIDAlt = (int)myArt.ArtIDAlt;

                articles.Bestellnummer = myArt.Bestellnummer;
                articles.BKZ = myArt.BKZ;
                articles.Breite = (decimal)myArt.Breite;
                articles.Brutto = (decimal)myArt.Brutto;
                articles.bSPL = myArt.bSPL;
                articles.bSPLartCert = myArt.bSPLartCert;

                articles.Charge = myArt.Charge;
                //articles.CreatedByScanner = false;

                articles.Dicke = (decimal)myArt.Dicke;

                articles.EAEingangAltLVS = myArt.LagerOrtTable;
                articles.EAAusgangAltLVS = myArt.LagerOrtTable;
                articles.Ebene = myArt.Ebene;
                articles.Einheit = myArt.Einheit;
                articles.EingangChecked = myArt.EingangChecked;
                articles.exAuftrag = myArt.exAuftrag;
                articles.exAuftragPos = myArt.exAuftragPos;
                articles.exBezeichnung = myArt.exBezeichnung;
                articles.externeInfo = myArt.externeInfo;
                articles.exLagerOrt = myArt.LagerOrtTable;
                articles.exMaterialnummer = myArt.exMaterialnummer;

                articles.FreigabeAbruf = myArt.FreigabeAbruf;

                articles.gemGewicht = (decimal)myArt.gemGewicht;
                articles.GArtID = (int)myArt.GArtID;
                articles.GlowDate = myArt.GlowDate;
                articles.Guete = myArt.Guete;
                articles.GutZusatz = myArt.GutZusatz;

                articles.Hoehe = (decimal)myArt.Hoehe;

                //articles.IdentifiedByScan = new DateTime(1900, 1, 1);
                articles.Info = myArt.Info;
                articles.interneInfo = myArt.interneInfo;
                articles.IsEMECreate = false;
                articles.IsEMLCreate = false;
                articles.IsKorStVerUse = myArt.IsKorStVerUse;
                articles.IsLagerArtikel = myArt.IsLagerArtikel;
                articles.IsLabelPrint = myArt.IsLabelPrint;
                articles.IsMulde = myArt.IsMulde;
                articles.IsProblem = myArt.IsProblem;
                articles.IsRL = myArt.IsRL;
                articles.IsStackable = myArt.IsStackable;
                articles.IsVerpackt = myArt.IsVerpackt;

                articles.Laenge = (decimal)myArt.Laenge;
                articles.LagerOrt = (int)myArt.LagerOrt;
                articles.LagerOrtTable = myArt.LagerOrtTable;
                articles.LAusgangTableID = (int)myArt.LAusgangTableID;
                articles.LEingangTableID = (int)myArt.LEingangTableID;
                articles.LVS_ID = (int)myArt.LVS_ID;
                articles.LVSNrBeforeUB = (int)myArt.LVSNrBeforeUB;
                articles.LVSNrAfterUB = (int)myArt.LVSNrAfterUB;
                articles.LZZ = myArt.LZZ;

                articles.MandantenID = (int)myArt.MandantenID;

                articles.Netto = (decimal)myArt.Netto;

                articles.Platz = myArt.Platz;
                articles.Position = myArt.Position;
                articles.Produktionsnummer = myArt.Produktionsnummer;

                articles.Reihe = myArt.Reihe;

                articles.ScanIn = myArt.ScanIn;
                articles.ScanInUser = myArt.ScanInUser;
                articles.ScanOut = myArt.ScanOut;
                articles.ScanOutUser = myArt.ScanOutUser;

                articles.TARef = myArt.TARef;

                articles.UB_AltCalcEinlagerung = myArt.UB_AltCalcEinlagerung;
                articles.UB_AltCalcAuslagerung = myArt.UB_AltCalcAuslagerung;
                articles.UB_AltCalcLagergeld = myArt.UB_AltCalcLagergeld;
                articles.UB_NeuCalcEinlagerung = myArt.UB_NeuCalcEinlagerung;
                articles.UB_NeuCalcAuslagerung = myArt.UB_NeuCalcAuslagerung;
                articles.UB_NeuCalcLagergeld = myArt.UB_NeuCalcLagergeld;
                articles.Umbuchung = (bool)myArt.Umbuchung;

                articles.Werk = myArt.Werk;
                articles.Werksnummer = myArt.Werksnummer;
            }
            catch (Exception ex)
            {

            }
            return articles;
        }



        public static Articles GetArticlesByClsArtikel(clsArtikel myArt)
        {
            Articles articles = new Articles();

            try
            {
                articles.Id = (int)myArt.ID;

                articles.AbrufReferenz = myArt.AbrufReferenz;
                articles.ADRLagerNr = (int)myArt.ADRLagerNr;
                articles.Anzahl = (int)myArt.Anzahl;
                articles.AbBereichID = (int)myArt.AbBereichID;
                articles.ASNVerbraucher = myArt.ASNVerbraucher;
                articles.ASNProduktionsnummer = myArt.ASNProduktionsnummer;
                articles.ArtIDAfterUB = (int)myArt.ArtIDAfterUB;
                articles.AuftragID = (int)myArt.AuftragID;
                articles.AuftragPos = (int)myArt.AuftragPos;
                articles.AuftragPosTableID = (int)myArt.AuftragPosTableID;
                articles.AusgangChecked = myArt.AusgangChecked;

                articles.ArtIDRef = myArt.ArtIDRef;
                articles.ArtIDAlt = (int)myArt.ArtIDAlt;

                articles.Bestellnummer = myArt.Bestellnummer;
                articles.BKZ = myArt.BKZ;
                articles.Breite = (decimal)myArt.Breite;
                articles.Brutto = (decimal)myArt.Brutto;
                articles.bSPL = myArt.bSPL;
                articles.bSPLartCert = myArt.bSPLartCert;

                articles.Charge = myArt.Charge;
                articles.CreatedByScanner = false;

                articles.Dicke = (decimal)myArt.Dicke;

                articles.EAEingangAltLVS = myArt.LagerOrtTable;
                articles.EAAusgangAltLVS = myArt.LagerOrtTable;
                articles.Ebene = myArt.Ebene;
                articles.Einheit = myArt.Einheit;
                articles.EingangChecked = myArt.EingangChecked;
                articles.exAuftrag = myArt.exAuftrag;
                articles.exAuftragPos = myArt.exAuftragPos;
                articles.exBezeichnung = myArt.exBezeichnung;
                articles.externeInfo = myArt.externeInfo;
                articles.exLagerOrt = myArt.LagerOrtTable;
                articles.exMaterialnummer = myArt.exMaterialnummer;

                articles.FreigabeAbruf = myArt.FreigabeAbruf;

                articles.gemGewicht = (decimal)myArt.gemGewicht;
                articles.GArtID = (int)myArt.GArtID;
                articles.GlowDate = myArt.GlowDate;
                articles.Guete = myArt.Guete;
                articles.GutZusatz = myArt.GutZusatz;

                articles.Hoehe = (decimal)myArt.Hoehe;

                articles.IdentifiedByScan = new DateTime(1900, 1, 1);
                articles.Info = myArt.Info;
                articles.interneInfo = myArt.interneInfo;
                articles.IsEMECreate = false;
                articles.IsEMLCreate = false;
                articles.IsKorStVerUse = myArt.IsKorStVerUse;
                articles.IsLagerArtikel = myArt.IsLagerArtikel;
                articles.IsLabelPrint = myArt.IsLabelPrint;
                articles.IsMulde = myArt.IsMulde;
                articles.IsProblem = myArt.IsProblem;
                articles.IsRL = myArt.IsRL;
                articles.IsStackable = myArt.IsStackable;
                articles.IsVerpackt = myArt.IsVerpackt;

                articles.Laenge = (decimal)myArt.Laenge;
                articles.LagerOrt = (int)myArt.LagerOrt;
                articles.LagerOrtTable = myArt.LagerOrtTable;
                articles.LAusgangTableID = (int)myArt.LAusgangTableID;
                articles.LEingangTableID = (int)myArt.LEingangTableID;
                articles.LVS_ID = (int)myArt.LVS_ID;
                articles.LVSNrBeforeUB = (int)myArt.LVSNrBeforeUB;
                articles.LVSNrAfterUB = (int)myArt.LVSNrAfterUB;
                articles.LZZ = myArt.LZZ;

                articles.MandantenID = (int)myArt.MandantenID;

                articles.Netto = (decimal)myArt.Netto;

                articles.Platz = myArt.Platz;
                articles.Position = myArt.Position;
                articles.Produktionsnummer = myArt.Produktionsnummer;

                articles.Reihe = myArt.Reihe;

                articles.ScanIn = myArt.ScanIn;
                articles.ScanInUser = myArt.ScanInUser;
                articles.ScanOut = myArt.ScanOut;
                articles.ScanOutUser = myArt.ScanOutUser;

                articles.TARef = myArt.TARef;

                articles.UB_AltCalcEinlagerung = myArt.UB_AltCalcEinlagerung;
                articles.UB_AltCalcAuslagerung = myArt.UB_AltCalcAuslagerung;
                articles.UB_AltCalcLagergeld = myArt.UB_AltCalcLagergeld;
                articles.UB_NeuCalcEinlagerung = myArt.UB_NeuCalcEinlagerung;
                articles.UB_NeuCalcAuslagerung = myArt.UB_NeuCalcAuslagerung;
                articles.UB_NeuCalcLagergeld = myArt.UB_NeuCalcLagergeld;
                articles.Umbuchung = (bool)myArt.Umbuchung;

                articles.Werk = myArt.Werk;
                articles.Werksnummer = myArt.Werksnummer;
            }
            catch (Exception ex)
            {

            }
            return articles;
        }
    }
}
