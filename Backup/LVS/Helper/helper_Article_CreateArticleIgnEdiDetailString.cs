using System;

namespace LVS.Helper
{
    public class helper_Article_CreateArticleIgnEdiDetailString
    {
        ///<summary>Globals / List_KontoText</summary>
        ///<remarks>Liefert die Liste (Dictonary) der Kontenarten</remarks>
        public static string CreateArticleIgnEdiDetailString(clsArtikel myArticle)
        {
            string strMes = string.Empty;
            strMes += Environment.NewLine;
            strMes += string.Format("[Artikel-Id] LVSNr: \t [{0}] - {1}", myArticle.ID, myArticle.LVS_ID) + Environment.NewLine;
            strMes += string.Format("[GArt-Id] Gut: \t [{0}] - {1}", myArticle.GArt.ID, myArticle.GArt.ViewID) + Environment.NewLine;
            strMes += string.Format("IgnEDI: \t [{0}] ", myArticle.GArt.IgnoreEdi.ToString()) + Environment.NewLine;
            return strMes;
        }
    }
}
