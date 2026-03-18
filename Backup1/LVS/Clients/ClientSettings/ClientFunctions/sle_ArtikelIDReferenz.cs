using Common.Models;

namespace LVS
{
    public class sle_ArtikelIDReferenz
    {
        ///<remarks>sle_ArtikelIDReferenz
        ///         Produktionsnummer+“X“+LVSNR
        ///         Produktionsnummer bei Arcelor 9-stellig
        ///         LVSNR keine feste länge
        ///</remarks>
        public static string Execute(clsArtikel myArtikel)
        {
            string strReturn = string.Empty;
            if (myArtikel.AbBereichID == 6)
            {
                strReturn = myArtikel.Produktionsnummer + "X" + int.Parse(myArtikel.LVS_ID.ToString());
            }
            return strReturn;
        }
        ///<remarks>sle_ArtikelIDReferenz 
        ///         Produktionsnummer+“X“+LVSNR
        ///</remarks>
        public static string Execute(Articles myArtikel)
        {
            string strReturn = string.Empty;
            if (myArtikel.AbBereichID == 6)
            {
                strReturn = myArtikel.Produktionsnummer + "X" + int.Parse(myArtikel.LVS_ID.ToString());
            }
            return strReturn;
        }

    }
}
