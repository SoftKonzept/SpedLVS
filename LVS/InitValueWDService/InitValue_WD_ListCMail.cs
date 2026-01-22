using System.Collections.Generic;

namespace LVS.InitValueWDService
{
    public class InitValue_WD_ListCMail
    {
        public const string const_Section = "CMail#";
        public const string const_Key = "WATCHDOG";
        public static List<string> Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            int iMailCount = InitValue_CMailCount.Value(myClient);
            List<string> listMails = new List<string>();
            listMails.Add(clsSystem.const_MailAdress);

            for (System.Int32 i = 1; i <= iMailCount; i++)
            {
                string strRead = InitValue_WD_ListCMail.const_Section + i.ToString();
                string strWDClient = string.Empty;
                strWDClient = ini.ReadString(myClient + InitValue_WD_ListCMail.const_Key, strRead);
                if (
                     (!strWDClient.Equals(string.Empty)) &&
                     (strWDClient.Contains("@"))
                   )
                {
                    listMails.Add(strWDClient);
                }
            }
            return listMails;
        }
    }
}
