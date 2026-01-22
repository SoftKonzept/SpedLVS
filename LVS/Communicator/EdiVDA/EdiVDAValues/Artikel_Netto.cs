namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Artikel_Netto
    {
        public const string const_Artikel_Netto = clsArtikel.ArtikelField_Netto;

        public static object Execute(clsArtikel myArtikel)
        {
            object retObj = myArtikel.Netto;
            return retObj;
        }
        public static string ExecuteXFactor(clsArtikel myArtikel, int myFactor)
        {
            object retObj = myArtikel.Netto;

            decimal decTmp = 0;
            if (decimal.TryParse(retObj.ToString(), out decTmp))
            {
                decTmp = decTmp * 1000;
            }
            string strReturn = Functions.FormatDecimalNoDiggits(decTmp);
            return strReturn;
        }
    }
}
