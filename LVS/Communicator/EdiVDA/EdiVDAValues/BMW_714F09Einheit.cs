namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_714F09Einheit
    {
        public const string const_BMW_714F09Einhei = "#BMW_714F09Einheit#";

        public static string Execute(clsArtikel myArtikel)
        {
            string strTmp = string.Empty;
            if ((myArtikel is clsArtikel) && (myArtikel.GArt is clsGut))
            {
                if (myArtikel.GArt.ArtikelArt.ToUpper().Contains("COIL"))
                {
                    strTmp = myArtikel.Einheit.ToUpper().ToString();
                }
                else if (myArtikel.GArt.ArtikelArt.ToUpper().Contains("PLATINE"))
                {
                    strTmp = string.Empty;
                }
            }
            return strTmp;
        }
    }
}
