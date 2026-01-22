namespace LVS.InitValueWDService
{
    public class InitValue_VE_SignOfLifeIntervall
    {
        //Int32.TryParse(INI.ReadString(strClient + "WATCHDOG", const_WD_SectionKey_Alarmfactor), out _WD_Alarmfactor);
        public const string const_Section = "VE_SignOfLifeIntervall";
        public const string const_Key = "SYSTEM";
        public static int Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            int iTmp = 120000;

            if (ini.ReadString(myClient + InitValue_VE_MainThreadDuration.const_Key, InitValue_VE_MainThreadDuration.const_Section) != null)
            {
                string tmp = ini.ReadString(myClient + InitValue_VE_MainThreadDuration.const_Key, InitValue_VE_MainThreadDuration.const_Section);
                int.TryParse(tmp, out iTmp);
                if (iTmp < 120000)
                {
                    iTmp = 120000;
                }
            }
            return iTmp;
        }
    }
}
