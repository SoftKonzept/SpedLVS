namespace LVS.InitValue
{
    public class InitValue_Client
    {
        public const string const_Section = "Matchcode";
        public const string const_Key = "CLIENT";
        public static string Value()
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            string retDefaultPrinter = string.Empty;
            if (ini.ReadString(InitValue_Client.const_Key, InitValue_Client.const_Section) != null)
            {
                retDefaultPrinter = ini.ReadString(InitValue_Client.const_Key, InitValue_Client.const_Section);
            }
            return retDefaultPrinter;
        }
    }
}
