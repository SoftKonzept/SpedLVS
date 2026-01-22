namespace LVS.InitValueCommunicator
{
    public class InitValueCom_TaskCustomizedProcessThreadDuration
    {
        public const string const_Section = "SYSTEM";

        public const string const_Key_Matchcode = "VE_TaskCustomizedProcessThreadDuration";
        public static int Value(string myClient)
        {
            int iDuration = 100000; // Default duration in milliseconds (5 minutes)
            string strReturn = string.Empty;
            clsINI.clsINI ini = GlobalINI.GetINI();
            string tmpSection = myClient + const_Section;

            if (ini.SectionNames().Contains(tmpSection))
            {
                if (ini.ReadString(tmpSection, const_Key_Matchcode) != null)
                {
                    strReturn = ini.ReadString(tmpSection, const_Key_Matchcode);
                }
            }
            int.TryParse(strReturn, out iDuration);
            if (iDuration < 100000)
            {
                iDuration = 100000; // Reset to default if invalid
            }
            return iDuration;
        }

    }
}
