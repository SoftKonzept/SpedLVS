using System.Collections.Generic;

namespace LVS.InitValueWDService
{
    public class InitValue_WD_ListWDClient
    {
        public const string const_Section = "WDClient#";
        public const string const_Key = "WATCHDOG";
        public static List<string> Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            int iClientCount = InitValue_WD_WDClientCount.Value(myClient);
            List<string> listClients = new List<string>();

            for (System.Int32 i = 1; i <= iClientCount; i++)
            {
                string strRead = InitValue_WD_ListWDClient.const_Section + i.ToString();
                string strWDClient = string.Empty;
                strWDClient = ini.ReadString(myClient + InitValue_WD_ListWDClient.const_Key, strRead);
                if (
                     (!strWDClient.Equals(string.Empty))
                   )
                {
                    if (!listClients.Contains(strWDClient))
                    {
                        listClients.Add(strWDClient);
                    }
                }
            }
            return listClients;
        }
    }
}
