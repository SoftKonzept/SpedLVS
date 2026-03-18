namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class ProduktionsnummerFillTo9With0
    {
        public const string const_ProduktionsnummerFillTo9With0 = "#ProduktionsnummerFillTo9With0#";

        public static string Execute(clsArtikel myArtikel)
        {
            string strReturn = string.Empty;
            string myValue = myArtikel.Produktionsnummer;
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
