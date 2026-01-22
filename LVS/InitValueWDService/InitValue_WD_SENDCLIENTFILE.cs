namespace LVS.InitValueWDService
{

    public class InitValue_WD_SENDCLIENTFILE
    {
        public const string const_Section = "SENDCLIENTFILE";
        public const string const_Key = "WATCHDOG";
        public static bool Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            bool bReturn = false;

            if (ini.ReadString(myClient + InitValue_WD_SENDCLIENTFILE.const_Key, InitValue_WD_SENDCLIENTFILE.const_Section) != null)
            {
                string stTmp = ini.ReadString(myClient + InitValue_WD_SENDCLIENTFILE.const_Key, InitValue_WD_SENDCLIENTFILE.const_Section);
                bool.TryParse(stTmp, out bReturn);
            }
            return bReturn;
        }
    }
}
