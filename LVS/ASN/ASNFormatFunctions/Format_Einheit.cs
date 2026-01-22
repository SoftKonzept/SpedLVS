using System;

namespace LVS.ASN.ASNFormatFunctions
{
    public class Format_Einheit
    {
        public const string const_Function_EinheitAssignment = "#func_EinheitAss#";
        /// <summary>
        ///             
        /// </summary>
        /// <param name="myValue"></param>
        /// <returns></returns>
        public static string Execute_EinheitAss(string myValue)
        {
            string strReturn = string.Empty;
            switch (myValue)
            {
                case "ST":
                    strReturn = "Stück";
                    break;

            }
            return strReturn;
        }

        //******************************************************************************************************************/

        public const string const_Function_AnzahlCheckforEinheit = "#func_CheckAnzahlEinheit#";
        ///<summary>clsASNFromatFunctions / func_EinheitAss</summary>
        ///<remarks></remarks>>
        public static string Execute_CheckAnzahlEinheit(ref clsArtikel myArt, string myValue)
        {
            string strReturn = string.Empty;
            Int32 iAnzahl = 0;
            Int32.TryParse(myValue, out iAnzahl);
            myArt.Anzahl = iAnzahl;
            if (myArt.Anzahl > 1)
            {
                myArt.Einheit = "Stück";
            }
            else
            {
                myArt.Einheit = "KG";
            }
            strReturn = iAnzahl.ToString();
            return strReturn; ;
        }

        //******************************************************************************************************************/

        public const string const_Function_CheckGArtSetEinheit = "#func_CheckGArtSetEinheit#";

        public static string Execute_CheckGArtSetEinheit(ref clsArtikel myArt, string myValue)
        {
            string strReturn = string.Empty;
            myArt.GArt.ID = myArt.GArtID;
            myArt.GArt.Fill();

            if (myArt.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
            {
                myArt.Anzahl = 1;
            }
            myArt.Einheit = myArt.GArt.Einheit;
            strReturn = myArt.Einheit.ToString();
            return strReturn;
        }

    }
}
