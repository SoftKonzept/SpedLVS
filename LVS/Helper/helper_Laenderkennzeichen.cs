using System.Collections.Generic;


namespace LVS.Helper
{
    public class helper_Laenderkennzeichen
    {
        ///<summary>Globals / dicCounty</summary>
        ///<remarks>Liefer die Liste (Dictonary) der Länder und Länderkennzeichen</remarks>
        public static Dictionary<string, string> DicCountry()
        {
            Dictionary<string, string> dicCounty = new Dictionary<string, string>();
            dicCounty.Add("AT", "Österreich");
            dicCounty.Add("AL", "Albanien");
            dicCounty.Add("BE", "Belgien");
            dicCounty.Add("BG", "Bulgarien");
            dicCounty.Add("BA", "Bosnien-Herzegowina");
            dicCounty.Add("CH", "Schweiz");
            dicCounty.Add("CY", "Zypern");
            dicCounty.Add("CZ", "Tschechische Republik");
            dicCounty.Add("DE", "Deutschland");
            dicCounty.Add("DK", "Dänemark");
            dicCounty.Add("ES", "Spanien");
            dicCounty.Add("EE", "Estland");
            dicCounty.Add("FR", "Frankreich");
            dicCounty.Add("FI", "Finnland");
            dicCounty.Add("FL", "Lichtenstein");
            dicCounty.Add("GR", "Griechenland");
            dicCounty.Add("HU", "Ungarn");
            dicCounty.Add("HR", "Kroatien");
            dicCounty.Add("IT", "Italien");
            dicCounty.Add("IE", "Irland");
            dicCounty.Add("IS", "Island");
            dicCounty.Add("LU", "Luxemburg");
            dicCounty.Add("LT", "Litauen");
            dicCounty.Add("LV", "Lettland");
            dicCounty.Add("MT", "Malta");
            dicCounty.Add("MD", "Moldawien");
            dicCounty.Add("MK", "Mazedonien");
            dicCounty.Add("MNE", "Montenegro");
            dicCounty.Add("NO", "Norwegen");
            dicCounty.Add("NL", "Niederlande");
            dicCounty.Add("PT", "Protugal");
            dicCounty.Add("PL", "Polen");
            dicCounty.Add("XK", "Kosovo");
            dicCounty.Add("RO", "Rumänien");
            dicCounty.Add("SE", "Schweden");
            dicCounty.Add("SK", "Slowakei");
            dicCounty.Add("SI", "Slowenien");
            dicCounty.Add("CS", "Serbien");
            dicCounty.Add("TR", "Türkei");
            dicCounty.Add("UA", "Ukraine");

            return dicCounty;
        }

    }
}
