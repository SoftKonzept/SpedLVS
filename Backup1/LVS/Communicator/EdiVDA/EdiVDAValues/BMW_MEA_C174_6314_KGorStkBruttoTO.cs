namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_MEA_C174_6314_KGorStkBruttoTO
    {
        public const string const_BMW_MEA_C174_6314_KGorStkBruttoTO = "#BMW_MEA_C174_6314_KGorStkBruttoTO#";

        public static string Execute(clsArtikel myArtikel)
        {
            string strTmp = string.Empty;
            if ((myArtikel is clsArtikel) && (myArtikel.GArt is clsGut))
            {
                if (myArtikel.GArt.ArtikelArt.ToUpper().Contains("COIL"))
                {
                    strTmp = Functions.FormatDecimalNoDiggits(myArtikel.Brutto);
                }
                else if (myArtikel.GArt.ArtikelArt.ToUpper().Contains("PLATINE"))
                {
                    strTmp = myArtikel.Anzahl.ToString();
                }
            }
            return strTmp;
        }
    }
}
