using System.Collections.Generic;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Artikel_Gueterart
    {
        public const string const_Artikel_Gut = clsArtikel.ArtikelField_Gut;


        public static string Execute(clsArtikel myArtikel)
        {
            string strGut = myArtikel.GArt.Bezeichnung.Trim();
            List<string> CharakterToChange = Artikel_Gueterart.ListCharakterToChange();
            foreach (string ch in CharakterToChange)
            {
                if (strGut.Contains(ch))
                {
                    switch (ch)
                    {
                        case "Ä":
                            strGut = strGut.Replace(ch, "Ae");
                            break;
                        case "ä":
                            strGut = strGut.Replace(ch, "ae");
                            break;
                        case "Ö":
                            strGut = strGut.Replace(ch, "Oe");
                            break;
                        case "ö":
                            strGut = strGut.Replace(ch, "oe");
                            break;
                        case "Ü":
                            strGut = strGut.Replace(ch, "Ue");
                            break;
                        case "ü":
                            strGut = strGut.Replace(ch, "ue");
                            break;
                        case "ß":
                            strGut = strGut.Replace(ch, "ss");
                            break;
                        case "-":
                            strGut = strGut.Replace(ch, " ");
                            break;
                    }
                }
            }
            return strGut;
        }

        public static List<string> ListCharakterToChange()
        {
            List<string> ReturnListCharakterToChange = new List<string>()
            {
                "Ä",
                "ä",
                "Ö",
                "ö",
                "Ü",
                "ü",
                "ß",
                "-"
            };
            return ReturnListCharakterToChange;
        }
    }
}
