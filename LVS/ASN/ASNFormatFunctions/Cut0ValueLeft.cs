namespace LVS.ASN.ASNFormatFunctions
{
    public class Cut0ValueLeft
    {
        /// <summary>
        ///             
        /// </summary>

        public const string const_Cut0ValueLeft = "#func_Cut0ValueLeft#";

        ///<summary>Cut0ValueLeft / Execute</summary>
        ///<remarks></remarks>>
        public static string Execute(string myValue)
        {
            string strReturn = string.Empty;
            strReturn = myValue.TrimStart('0');
            return strReturn;
        }
    }
}
