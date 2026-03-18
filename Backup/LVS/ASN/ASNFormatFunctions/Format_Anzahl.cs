using System;

namespace LVS.ASN.ASNFormatFunctions
{
    public class Format_Anzahl
    {
        public const string const_Function_CheckGArtSetAnzahl = "#func_CheckGArtSetAnzahl#";
        /// <summary>
        ///             
        /// </summary>
        /// <param name="myValue"></param>
        /// <returns></returns>
        public static string Execute(ref clsArtikel myArt, string myValue)
        {
            string strReturn = string.Empty;
            myArt.GArt.ID = myArt.GArtID;
            myArt.GArt.Fill();

            if (myArt.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
            {
                myArt.Anzahl = 1;
            }
            else
            {
                Int32 iAnzahl = 0;
                Int32.TryParse(myValue, out iAnzahl);
                myArt.Anzahl = iAnzahl;
            }
            myArt.Einheit = myArt.GArt.Einheit;
            strReturn = myArt.Anzahl.ToString();
            return strReturn;
        }

    }
}
