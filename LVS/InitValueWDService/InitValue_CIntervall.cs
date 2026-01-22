namespace LVS.InitValueWDService
{
    public class InitValue_CIntervall
    {
        //Int32.TryParse(INI.ReadString(strClient + "WATCHDOG", const_WD_SectionKey_Alarmfactor), out _WD_Alarmfactor);
        public const string const_Section = "CIntervall";
        public const string const_Key = "WATCHDOG";
        public static int Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            int iTmp = 1;

            if (ini.ReadString(myClient + InitValue_CIntervall.const_Key, InitValue_CIntervall.const_Section) != null)
            {
                string tmp = ini.ReadString(myClient + InitValue_CIntervall.const_Key, InitValue_CIntervall.const_Section);
                int.TryParse(tmp, out iTmp);
            }
            return iTmp;
        }
    }
}
