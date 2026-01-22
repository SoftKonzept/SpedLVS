namespace LVS.InitValueWDService
{
    public class InitValue_WD_WDClientCount
    {
        public const string const_Section = "WDClientCount";
        public const string const_Key = "WATCHDOG";
        public static int Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            int iTmp = 1;

            if (ini.ReadString(myClient + InitValue_WD_WDClientCount.const_Key, InitValue_WD_WDClientCount.const_Section) != null)
            {
                string tmp = ini.ReadString(myClient + InitValue_WD_WDClientCount.const_Key, InitValue_WD_WDClientCount.const_Section);
                int.TryParse(tmp, out iTmp);
            }
            return iTmp;
        }
    }
}
