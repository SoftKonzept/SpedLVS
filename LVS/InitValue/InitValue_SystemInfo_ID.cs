namespace LVS.InitValue
{
    public class InitValue_SystemInfo_ID
    {
        public const string const_Section = "ID";
        public const string const_Key = "SYSTEMINFO";
        public static string Value()
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            string retString = string.Empty;
            if (ini.ReadString(InitValue_SystemInfo_ID.const_Key, InitValue_SystemInfo_ID.const_Section) != null)
            {
                retString = ini.ReadString(InitValue_SystemInfo_ID.const_Key, InitValue_SystemInfo_ID.const_Section);
            }
            return retString;
        }
    }
}
