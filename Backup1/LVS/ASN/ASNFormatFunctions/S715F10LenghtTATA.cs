namespace LVS.ASN.ASNFormatFunctions
{
    public class S715F10LenghtTATA
    {
        /// <summary>
        ///             Löschen das Zeichen "Bindestrich" aus dem String
        /// </summary>

        public const string const_S715F10LenghtTATA = "#715F10LenghtTATA#";

        ///<summary>clsASNFromatFunctions / FillTo9With0Left</summary>
        ///<remarks></remarks>>
        public static string Execute(string myValue)
        {
            string strReturn = string.Empty;
            int iTmp = 0;
            int.TryParse(myValue, out iTmp);
            decimal decTmp = 0;
            if (iTmp > 0)
            {
                decTmp = iTmp / 1000;
            }
            strReturn = decTmp.ToString();
            return strReturn;
        }
    }
}
