namespace LVS.ASN.ASNFormatFunctions
{
    public class FillTo9With0Left
    {
        /// <summary>
        ///             Löschen das Zeichen "Bindestrich" aus dem String
        /// </summary>

        public const string const_FillTo9With0Left = "#FillTo9With0LEFT#";

        ///<summary>clsASNFromatFunctions / FillTo9With0Left</summary>
        ///<remarks></remarks>>
        public static string Execute(string myValue)
        {
            string strReturn = string.Empty;
            if (myValue.Length == 9)
            {
                strReturn = myValue;
            }
            else
            {
                if (myValue.Length > 9)
                {
                    strReturn = myValue.Substring(myValue.Length - 9);
                }
                else
                {
                    while (myValue.Length < 9)
                    {
                        //0 voranstelle
                        myValue = "0" + myValue;
                    }
                    strReturn = myValue;
                }
            }
            return strReturn;
        }
    }
}
