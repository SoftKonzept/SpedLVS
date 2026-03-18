namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class MENDRITZKI_Produktionsnummer
    {
        public const string const_MENDRITZKI_Produktionsnummer = "#MENDRITZKI_Produktionsnummer#";

        public static string Execute(clsArtikel myArtikel)
        {
            string strTmp = string.Empty;
            if (myArtikel is clsArtikel)
            {
                if ((!myArtikel.Charge.Equals(string.Empty) && (!myArtikel.Position.Equals(string.Empty))))
                {
                    strTmp = myArtikel.Charge + "-" + myArtikel.Position.ToString();
                }
            }
            return strTmp;
        }
    }
}
