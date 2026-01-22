namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_Einheit
    {
        public const string const_BMW_Einheit = "#BMW_Einheit#";

        public static string Execute(clsArtikel myArtikel)
        {
            string strTmp = string.Empty;
            if ((myArtikel is clsArtikel) && (myArtikel.GArt is clsGut))
            {
                if (myArtikel.GArt.ArtikelArt.ToUpper().Contains("COIL"))
                {
                    strTmp = "KG";
                }
                else if (myArtikel.GArt.ArtikelArt.ToUpper().Contains("PLATINE"))
                {
                    strTmp = "PCE";
                }
            }
            return strTmp;
        }
    }
}
