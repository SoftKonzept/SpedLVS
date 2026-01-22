namespace LVS.InitValueWDService
{
    public class InitValue_WD_IsWatchDog
    {
        public const string const_Section = "VE_IsWatchDog";
        public const string const_Key = "SYSTEM";
        public static bool Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            bool bReturn = true;

            if (ini.ReadString(myClient + InitValue_WD_IsWatchDog.const_Key, InitValue_WD_IsWatchDog.const_Section) != string.Empty)
            {
                string stTmp = ini.ReadString(myClient + InitValue_WD_IsWatchDog.const_Key, InitValue_WD_IsWatchDog.const_Section);
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
            else
            {
                bReturn = false;
            }
            return bReturn;
        }
    }
}
