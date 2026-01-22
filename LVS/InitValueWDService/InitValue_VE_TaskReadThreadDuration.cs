namespace LVS.InitValueWDService
{
    public class InitValue_VE_TaskReadThreadDuration
    {
        public const string const_Section = "VE_TaskReadThreadDuration";
        public const string const_Key = "SYSTEM";
        public static int Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            int iReturn = clsSystem.const_Default_MainThreadWaitDuration;

            if (ini.ReadString(myClient + InitValue_VE_TaskReadThreadDuration.const_Key, InitValue_VE_TaskReadThreadDuration.const_Section) != null)
            {
                string stTmp = ini.ReadString(myClient + InitValue_VE_TaskReadThreadDuration.const_Key, InitValue_VE_TaskReadThreadDuration.const_Section);
                int.TryParse(stTmp, out iReturn);
            }
            return iReturn;
        }
    }
}
