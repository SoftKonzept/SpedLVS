using System.Collections.Generic;

namespace LVS.Helper
{
    public class helper_Kontoarten
    {
        ///<summary>Globals / List_KontoText</summary>
        ///<remarks>Liefert die Liste (Dictonary) der Kontenarten</remarks>
        public static List<string> List_KontoText()
        {
            List<string> listTmp = new List<string>();
            listTmp.Add("Gleisstellgebühr");
            listTmp.Add("gewährte Skonti");
            listTmp.Add("keine Zuweisung");
            listTmp.Add("Lagergeld");
            listTmp.Add("sonstiges");
            listTmp.Add("Miete");
            listTmp.Add("Umschlag");

            listTmp.Sort();
            return listTmp;
        }
    }
}
