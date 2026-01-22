using System;

namespace LVS.ASN.ASNFormatFunctions
{
    public class Diff1000
    {
        /// <summary>
        ///             Löschen das Zeichen "Bindestrich" aus dem String
        /// </summary>

        public const string const_Diff1000 = "#func_Diff1000#";

        ///<summary>clsASNFromatFunctions / func_Diff1000</summary>
        ///<remarks></remarks>>
        public static string Execute(string myValue)
        {
            string strReturn = string.Empty;
            decimal decTmp = 0;
            Decimal.TryParse(myValue, out decTmp);
            decTmp = decTmp / 1000;
            strReturn = decTmp.ToString();
            return strReturn;
        }
    }
}
