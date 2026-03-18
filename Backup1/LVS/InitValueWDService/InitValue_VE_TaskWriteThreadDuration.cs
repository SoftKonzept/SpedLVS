namespace LVS.InitValueWDService
{
    public class InitValue_VE_TaskWriteThreadDuration
    {
        //Int32.TryParse(INI.ReadString(strClient + "WATCHDOG", const_WD_SectionKey_Alarmfactor), out _WD_Alarmfactor);
        public const string const_Section = "VE_TaskWriteThreadDuration";
        public const string const_Key = "SYSTEM";
        public static int Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            int iTmp = clsSystem.const_Default_MainThreadWaitDuration;

            if (ini.ReadString(myClient + InitValue_VE_TaskWriteThreadDuration.const_Key, InitValue_VE_TaskWriteThreadDuration.const_Section) != null)
            {
                string tmp = ini.ReadString(myClient + InitValue_VE_TaskWriteThreadDuration.const_Key, InitValue_VE_TaskWriteThreadDuration.const_Section);
                int.TryParse(tmp, out iTmp);
                if (iTmp < clsSystem.const_Default_MainThreadWaitDuration)
                {
                    iTmp = clsSystem.const_Default_MainThreadWaitDuration;
                }
            }
            return iTmp;
        }
    }
}
