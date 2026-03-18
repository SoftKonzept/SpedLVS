namespace LVS.InitValue
{
    public class InitValue_System_VE_eInvoiceLogActivatet
    {
        public const string const_Section = "SYSTEM";
        public const string const_Key = "VE_eInvoiceLogActivatet";
        public static bool Value()
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            string strClient = InitValue.InitValue_Client.Value();
            string tmpSection = strClient + const_Section;
            bool bReturn = false;
            try
            {
                if (ini.ReadString(tmpSection, InitValue_System_VE_eInvoiceLogActivatet.const_Key) != string.Empty)
                {
                    string stTmp = ini.ReadString(tmpSection, InitValue_System_VE_eInvoiceLogActivatet.const_Key);
                    bool.TryParse(stTmp, out bReturn);
                }
            }
            catch
            {
                bReturn = false;
            }
            return bReturn;
        }
    }
}
