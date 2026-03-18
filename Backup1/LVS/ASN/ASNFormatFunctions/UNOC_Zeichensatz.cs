namespace LVS
{
    public class UNOC_Zeichensatz
    {
        /// <summary>
        ///             1:1987 Information processing . 
        ///             8 bit single byte coded graphic character
        ///             sets.Part 1: Latin alphabet No. 1.
        ///             
        ///             Diese Norm unterstützt die folgenden Sprachen: 
        ///             
        ///             Dänisch, Niederländisch, Englisch, Färöisch, Finnisch, Französisch, 
        ///             Deutsch, Isländisch, Irisch, Italienisch, Norwegisch, Portugiesisch, 
        ///             Spanisch und Schwedisch
        /// </summary>


        public static string Execute(string myInputValue)
        {
            myInputValue = myInputValue.Trim();
            if (myInputValue.Contains("ä"))
            {
                myInputValue = myInputValue.Replace("ä", "ae");
            }
            if (myInputValue.Contains("ü"))
            {
                myInputValue = myInputValue.Replace("ü", "ue");
            }
            if (myInputValue.Contains("ö"))
            {
                myInputValue = myInputValue.Replace("ö", "oe");
            }
            if (myInputValue.Contains("Ä"))
            {
                myInputValue = myInputValue.Replace("Ä", "Ae");
            }
            if (myInputValue.Contains("Ü"))
            {
                myInputValue = myInputValue.Replace("Ü", "Ue");
            }
            if (myInputValue.Contains("Ö"))
            {
                myInputValue = myInputValue.Replace("Ö", "Oe");
            }
            if (myInputValue.Contains("ß"))
            {
                myInputValue = myInputValue.Replace("ß", "ss");
            }


            string strReturn = myInputValue;
            return strReturn;
        }
    }
}
