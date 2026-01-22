namespace LVS.InitValue
{
    public class InitValue_SystemInfo_IsTestsystem
    {
        public const string const_Section = "ID";
        public const string const_Key = "SYSTEMINFO";
        public static bool Value()
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            bool bReturn = true;
            if (ini.ReadString(InitValue_SystemInfo_IsTestsystem.const_Key, InitValue_SystemInfo_IsTestsystem.const_Section) != null)
            {
                string stTmp = ini.ReadString(InitValue_SystemInfo_IsTestsystem.const_Key, InitValue_SystemInfo_IsTestsystem.const_Section);
                switch (stTmp)
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
