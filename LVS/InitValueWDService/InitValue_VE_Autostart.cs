namespace LVS.InitValueWDService
{
    public class InitValue_VE_Autostart
    {
        public const string const_Section = "VE_Autostart";
        public const string const_Key = "SYSTEM";
        public static bool Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            bool bReturn = true;

            if (ini.ReadString(myClient + InitValue_VE_Autostart.const_Key, InitValue_VE_Autostart.const_Section) != null)
            {
                string stTmp = ini.ReadString(myClient + InitValue_VE_Autostart.const_Key, InitValue_VE_Autostart.const_Section);
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
