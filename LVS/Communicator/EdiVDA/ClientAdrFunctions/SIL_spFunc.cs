using System;

namespace LVS
{
    class SIL_spFunc
    {
        internal clsStringCheck StrCheck = new clsStringCheck();
        ///<summary>SIL_spFunc / FillValueWithstringToLenth</summary>
        ///<remarks>            // NEU, SIL Emden: (NUR NOCH VW)
        ///         F03 (Text1)
        ///
        ///         Ab Stelle 01, 03 LEER-Stellen.
        ///         Ab Stelle 04, 06 Zeichen: Bei Coilmaterial: Coil-Länge in Metern
        ///                           Anderes Material: LEER
        ///         Ab Stelle 10, 30 Zeichen: Von SIL eingegebene, zusätzliche
        ///                           Information zum Artikel.
        ///         Stelle 40, 01 Zeichen: Kennzeichen "Problematischer Artikel".
        ///                           LEER: Kein Problem
        ///                           "X" : Problem existent
        ///
        ///         Alle Vorgangsschlüssel.</remarks>
        public string SIL_716F03(clsArtikel myArt)
        {
            // NEU, SIL Emden: (NUR NOCH VW)
            // F03 (Text1)
            //
            // Ab Stelle 01, 03 LEER-Stellen.
            // Ab Stelle 04, 06 Zeichen: Bei Coilmaterial: Coil-Länge in Metern
            //                           Anderes Material: LEER
            // Ab Stelle 10, 30 Zeichen: Von SIL eingegebene, zusätzliche
            //                           Information zum Artikel.
            //    Stelle 40, 01 Zeichen: Kennzeichen "Problematischer Artikel".
            //                           LEER: Kein Problem
            //                           "X" : Problem existent
            //
            //   Alle Vorgangsschlüssel.

            // Stelle 1-3 leer  = 3 Zeichen
            string strSt1 = "   ";
            // Stelle 4-10      = 6 Zeichen
            string strSt2 = "      ";
            myArt.GArt.ID = myArt.GArtID;
            myArt.GArt.Fill();
            if (myArt.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
            {
                strSt2 = strSt2 + myArt.Laenge.ToString("000000");
                strSt2 = strSt2.Substring(strSt2.Length - 6);
            }
            // Stelle 10 -40     = 30 Zeichen
            string strSt3 = string.Empty;
            strSt3 = myArt.externeInfo;
            StrCheck.CheckString(ref strSt3);
            //strSt3 = myArt.interneInfo;
            if (strSt3.Length > 30)
            {
                strSt3 = strSt3.Substring(strSt3.Length - 30);
            }
            else
            {
                while (strSt3.Length < 30)
                {
                    strSt3 = strSt3 + " ";
                }

            }
            // Stelle 40  
            string strSt4 = " ";
            if (myArt.IsProblem)
            {
                strSt4 = "X";
            }
            //string zusammensetzen
            string strReturn = string.Empty;
            strReturn = strSt1 + strSt2 + strSt3 + strSt4;
            //Check auf Stringlänge 40
            if (strReturn.Length > 40)
            {
                strReturn = strReturn.Substring(strReturn.Length - 40);
            }
            return strReturn;
        }
        ///<summary>SIL_spFunc / SIL_ProdNrCHeck</summary>
        ///<remarks>Fehlerkorrektur nach Umstellung auf neue LVS.n</remarks>
        public string SIL_ProdNrCHeck(ref clsLagerdaten myLager, string strASNTyp)
        {
            string strReturn = string.Empty;
            //check Artikel Einlagerung bis 03.08.2015
            DateTime CheckDate = Convert.ToDateTime("04.08.2015");
            //this.Lager.Eingang.AbBereichID = this.Lager.Artikel.LEingangTableID;
            //this.Lager.Eingang.FillEingang();
            if (myLager.Artikel.Eingang.LEingangDate < CheckDate)
            {
                //Produktionsnummer auf 9 stellen links auffüllen
                while (myLager.Artikel.Produktionsnummer.Length < 9)
                {
                    //0 voranstelle
                    myLager.Artikel.Produktionsnummer = "0" + myLager.Artikel.Produktionsnummer;
                }
            }
            //Check Eingangsdatum > 03.08.2015 und kleiner 27.08.2015
            if (
                (myLager.Artikel.Eingang.LEingangDate >= Convert.ToDateTime("03.08.2015")) &
                (myLager.Artikel.Eingang.LEingangDate <= Convert.ToDateTime("27.08.2015"))
              )
            {
                //Wenn Charge <> Produktionsnummer, dann Charge, da bis zum 27.08.2015 die Charge statt Produktionnummer gesendet wurde
                if (!myLager.Artikel.Produktionsnummer.Equals(myLager.Artikel.Charge))
                {
                    myLager.Artikel.Produktionsnummer = myLager.Artikel.Charge;
                }
            }
            switch (strASNTyp)
            {
                case "EML":
                case "EME":
                    //case "BML":
                    //case "BME":
                    strReturn = myLager.Artikel.Produktionsnummer;
                    myLager.Artikel.UpdateASNProduktionsnummer();
                    break;
                default:

                    strReturn = myLager.Artikel.ASNProduktionsnummer;
                    break;
            }
            return strReturn;
        }
        /////<summary>SIL_spFunc / GetJournalHeaderText</summary>
        /////<remarks>Headertext beim Excelexport oder Print</remarks>
        //public string GetJournalHeaderText(string myHeaderText)
        //{
        //    string strReturn = string.Empty;
        //    switch (myHeaderText)
        //    { 
        //        case "nur Eingänge":
        //            strReturn = "Lagereingänge gesamt";
        //            break;
        //        case "nur Ausgänge":
        //            strReturn = "Lagerausgänge";
        //            break;
        //        case "mit Schaden":
        //            strReturn = "Lagereingänge mit Schaden";
        //            break;
        //        case "ohne Schaden":
        //            strReturn = "Lagereingänge ohne Schaden";
        //            break;
        //        default:
        //            strReturn = myHeaderText;
        //            break;
        //    }
        //    return strReturn;
        //}


    }
}
