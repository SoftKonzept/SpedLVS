namespace LVS.InitValueWDService
{
    public class InitValue_WD_OdetteProcessName
    {
        public const string const_Section = "OdetteProcessName";
        public const string const_Key = "WATCHDOG";
        public static string Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            string stTmp = string.Empty;

            if (ini.ReadString(myClient + InitValue_WD_OdetteProcessName.const_Key, InitValue_WD_OdetteProcessName.const_Section) != null)
            {
                stTmp = ini.ReadString(myClient + InitValue_WD_OdetteProcessName.const_Key, InitValue_WD_OdetteProcessName.const_Section);
                stTmp = ini.ReadString(myClient + InitValue_WD_OdetteProcessName.const_Key, InitValue_WD_OdetteProcessName.const_Section);
                //int.TryParse(tmp, out iTmp);
            }
            return stTmp;
        }
    }
}
