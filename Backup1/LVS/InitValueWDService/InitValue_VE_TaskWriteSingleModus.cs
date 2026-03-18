namespace LVS.InitValueWDService
{
    public class InitValue_VE_TaskWriteSingleModus
    {
        public const string const_Section = "VE_TaskWriteSingleModus";
        public const string const_Key = "SYSTEM";
        public static bool Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            bool bReturn = true;

            if (ini.ReadString(myClient + InitValue_VE_TaskWriteSingleModus.const_Key, InitValue_VE_TaskWriteSingleModus.const_Section) != null)
            {
                string stTmp = ini.ReadString(myClient + InitValue_VE_TaskWriteSingleModus.const_Key, InitValue_VE_TaskWriteSingleModus.const_Section);
                bool.TryParse(stTmp, out bReturn);
            }
            return bReturn;
        }
    }
}
