namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Artikel_Anzahl
    {
        public const string const_Artikel_Anzahl = clsArtikel.ArtikelField_Anzahl;

        public static object Execute(clsArtikel myArtikel)
        {
            object retObj = myArtikel.Anzahl;
            return retObj;
        }
    }
}
