using System.Collections.Generic;

namespace LVS.Constants
{
    public class constValue_CustomProcesses
    {
        public const string const_Process_Novelis_ArticleAccessByCertifacte = "Novelis_ArticleAccessByCertifacte";
        public const string const_Process_SLEArcelor_CreateTeslaPNNo = "SLEArcelor_CreateTeslaPNNo";

        public static List<string> GetCustomProcessList()
        {
            List<string> list = new List<string>();

            list.Add("test");
            list.Add(const_Process_Novelis_ArticleAccessByCertifacte);
            list.Add(const_Process_SLEArcelor_CreateTeslaPNNo);

            return list;
        }
    }
}
