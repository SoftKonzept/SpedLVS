namespace LVS.ASN.ASNFormatFunctions
{
    public class DeleteHyphen
    {
        /// <summary>
        ///             Löschen das Zeichen "Bindestrich" aus dem String
        /// </summary>

        public const string const_DeleteHyphen = "#func_DeleteHyphen#";

        public static string Execute(string myInputValue)
        {
            myInputValue = myInputValue.Replace(" ", "").Trim();
            string strReturn = myInputValue.Replace("-", "");
            return strReturn;
        }
    }
}
