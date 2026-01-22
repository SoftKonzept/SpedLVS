namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_PACNumberBolzen
    {
        public const string const_BMW_PACNumberBolzen = "#BMW_PACNumberBolzen#";

        public static string Execute(clsArtikel myArtikel)
        {
            string strTmp = string.Empty;
            if ((myArtikel is clsArtikel) && (myArtikel.GArt is clsGut))
            {
                strTmp = myArtikel.GArt.AbsteckBolzenNr.Trim();
                if (strTmp.Equals(string.Empty))
                {
                    strTmp = "LEER";
                }
            }
            return strTmp;
        }
    }
}
