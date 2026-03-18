namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_714F08Netto
    {
        public const string const_BMW_714F08Netto = "#BMW_714F08Netto#";

        public static string Execute(clsArtikel myArtikel)
        {
            string strTmp = string.Empty;
            if ((myArtikel is clsArtikel) && (myArtikel.GArt is clsGut))
            {
                if (myArtikel.GArt.ArtikelArt.ToUpper().Contains("COIL"))
                {
                    strTmp = ((int)myArtikel.Netto).ToString() + "000";
                    int iLen714F08 = 13;
                    while (iLen714F08 - strTmp.Length > 0)
                    {
                        strTmp = "0" + strTmp;
                    }
                    //strTmp = ((int)myArtikel.Netto).ToString();
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
