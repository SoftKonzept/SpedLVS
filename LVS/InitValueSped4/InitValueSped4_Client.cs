namespace LVS.InitValueSped4
{
    public class InitValueSped4_Client
    {
        public const string const_Section = "CLIENT";
        public const string const_Key = "Matchcode";
        public static string Matchcode()
        {
            string strReturn = string.Empty;
            clsINI.clsINI ini = GlobalINI.GetINI();

            if (ini.SectionNames().Contains(const_Section))
            {
                if (ini.ReadString(const_Section, const_Key) != null)
                {
                    strReturn = ini.ReadString(const_Section, const_Key);
                }
            }
            return strReturn;
        }
    }
}
