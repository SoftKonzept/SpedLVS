namespace LVS.InitValueCommunicator
{
    public class InitValueCom_Client
    {
        public const string const_Section = "CLIENT";

        public const string const_Key_Matchcode = "Matchcode";
        public static string Matchcode()
        {
            string strReturn = string.Empty;
            clsINI.clsINI ini = GlobalINI.GetINI();

            if (ini.SectionNames().Contains(const_Section))
            {
                if (ini.ReadString(const_Section, const_Key_Matchcode) != null)
                {
                    strReturn = ini.ReadString(const_Section, const_Key_Matchcode);
                }
            }
            return strReturn;
        }

    }
}
