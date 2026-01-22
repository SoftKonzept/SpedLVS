namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_Schaden
    {
        public const string const_BMW_Schaden = "#BMW_Schaden#";

        public static string Execute(clsArtikel myArtikel)
        {
            string strTmp = string.Empty;
            if ((myArtikel is clsArtikel) && (myArtikel.GArt is clsGut))
            {
                strTmp = myArtikel.SchadenTopOne.Trim();
                if (strTmp.Equals(string.Empty))
                {
                    strTmp = "Schadenstext";
                }
            }
            return strTmp;
        }
    }
}
