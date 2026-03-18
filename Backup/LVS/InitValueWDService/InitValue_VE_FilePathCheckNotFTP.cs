namespace LVS.InitValueWDService
{
    public class InitValue_VE_FilePathCheckNotFTP
    {
        public const string const_Section = "VE_FilePathCheckNotFTP";
        public const string const_Key = "SYSTEM";
        public static string Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            string retDefaultPrinter = string.Empty;
            if (ini.ReadString(myClient + InitValue_VE_FilePathCheckNotFTP.const_Key, InitValue_VE_FilePathCheckNotFTP.const_Section) != null)
            {
                retDefaultPrinter = ini.ReadString(myClient + InitValue_VE_FilePathCheckNotFTP.const_Key, InitValue_VE_FilePathCheckNotFTP.const_Section);
            }
            return retDefaultPrinter;
        }
    }
}
