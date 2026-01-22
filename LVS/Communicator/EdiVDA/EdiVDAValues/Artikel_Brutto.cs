namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Artikel_Brutto
    {
        public const string const_Artikel_Brutto = clsArtikel.ArtikelField_Brutto;

        public static object Execute(clsArtikel myArtikel)
        {
            object retObj = myArtikel.Brutto;
            return retObj;
        }

        public static string ExecuteXFactor(clsArtikel myArtikel, int myFactor)
        {
            object retObj = myArtikel.Brutto;

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
