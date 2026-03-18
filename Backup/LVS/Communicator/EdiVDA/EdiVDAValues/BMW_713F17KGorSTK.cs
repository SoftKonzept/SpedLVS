namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_713F17KGorSTK
    {
        public const string const_BMW_713F17_KGorSTK = "#BMW_713F17KGorSTK#";

        public static string Execute(clsArtikel myArtikel)
        {
            string strTmp = string.Empty;
            if ((myArtikel is clsArtikel) && (myArtikel.GArt is clsGut))
            {
                if (myArtikel.GArt.ArtikelArt.ToUpper().Contains("COIL"))
                {
                    //decimal decTmp = 0;
                    //decTmp = myArtikel.Brutto; // * 1000;
                    strTmp = Functions.FormatDecimalNoDiggits(myArtikel.Brutto);
                }
                else if (myArtikel.GArt.ArtikelArt.ToUpper().Contains("PLATINE"))
                {
                    //decimal decTmp = 0;
                    strTmp = myArtikel.Anzahl.ToString(); // * 1000;
                    //strReturn = Functions.FormatDecimalNoDiggits(decTmp);
                }
            }
            return strTmp;
        }
    }
}
