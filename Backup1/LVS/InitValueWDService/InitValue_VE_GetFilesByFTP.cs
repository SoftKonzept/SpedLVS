namespace LVS.InitValueWDService
{
    public class InitValue_VE_GetFilesByFTP
    {
        public const string const_Section = "VE_GetFilesByFTP";
        public const string const_Key = "SYSTEM";
        public static bool Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            bool bReturn = false;
            if (ini.ReadString(myClient + InitValue_VE_GetFilesByFTP.const_Key, InitValue_VE_GetFilesByFTP.const_Section) != null)
            {
                string strTmp = ini.ReadString(myClient + InitValue_VE_GetFilesByFTP.const_Key, InitValue_VE_GetFilesByFTP.const_Section);
                switch (strTmp)
                {
                    case "0":
                        bReturn = false;
                        break;
                    case "1":
                        bReturn = true;
                        break;
                    default:
                        bReturn = false;
                        break;
                }
            }
            return bReturn;
        }
    }
}
