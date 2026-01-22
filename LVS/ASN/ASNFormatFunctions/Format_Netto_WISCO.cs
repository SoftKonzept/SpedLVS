using System;
using System.Linq;

namespace LVS.ASN.ASNFormatFunctions
{
    public class Format_Netto_WISCO
    {
        public const string const_Function_Format_Netto_WISCO = "#func_WISCO_Netto#";
        /// <summary>
        ///             
        /// </summary>
        /// <param name="myValue"></param>
        /// <returns></returns>
        public static string Execute(ref clsArtikel myArt, string myValue)
        {
            string strReturn = string.Empty;
            string charDelete = "KGkgSTst";
            strReturn = myValue.TrimEnd(charDelete.ToArray());
            strReturn = strReturn.TrimStart('0');

            decimal decTmp = 0;
            Decimal.TryParse(strReturn, out decTmp);
            myArt.Netto = decTmp / 1000;
            return myArt.Netto.ToString();
        }


    }
}
