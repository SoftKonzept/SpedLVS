using Common.Models;

namespace Common.Customize
{
    public class vw_ArticleIdReferenz
    {
        ///<remarks>ArtikelIDRef für VW 
        ///            Lieferantennummer (9stellig)+
        ///            LVSNR ( 8Stellig)+
        ///            Produktionsnummer (9stellig)
        ///</remarks>
        public static string CreateByModel(Articles myArtikel)
        {
            string strReturn = string.Empty;

            string strLieferantennummer = "000000000"; // 9 Stellen
            string strLVSNr = "00000000";              // 8 Stellen
            string strProduktionsnummer = "000000000"; // 9 Stellen

            //1. Teil Lieferantennummer 
            strLieferantennummer = strLieferantennummer + myArtikel.Eingang.Lieferant.Trim();
            strLieferantennummer = strLieferantennummer.Substring(strLieferantennummer.Length - 9);

            //2. Teil LVSNR
            strLVSNr = strLVSNr + myArtikel.LVS_ID.ToString("00000000");
            strLVSNr = strLVSNr.Substring(strLVSNr.Length - 8);

            //3. Teil Produktionsnummer
            strProduktionsnummer = strProduktionsnummer + myArtikel.Produktionsnummer.Trim();
            strProduktionsnummer = strProduktionsnummer.Substring(strProduktionsnummer.Length - 9);

            string strTmp = strLieferantennummer + strLVSNr + strProduktionsnummer;
            if (strTmp.Length == 26)
            {
                strReturn = strTmp;
            }
            else
            {
                if (strTmp.Length > 26)
                {
                    strTmp = strTmp.Substring(strTmp.Length - 26);
                }
                if (strTmp.Length < 26)
                {
                    strTmp = strTmp + "0000000000";
                    strTmp = strTmp.Substring(1, 26);
                }
                strReturn = strTmp;
            }
            strReturn = strTmp;
            return strReturn;
        }
    }
}
