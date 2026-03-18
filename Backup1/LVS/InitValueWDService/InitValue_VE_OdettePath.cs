namespace LVS.InitValueWDService
{
    public class InitValue_VE_OdettePath
    {
        public const string const_Section = "VE_OdettePath";
        public const string const_Key = "SYSTEM";
        public static string Value(string myClient)
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            string retValue = string.Empty;
            if (ini.ReadString(myClient + InitValue_VE_OdettePath.const_Key, InitValue_VE_OdettePath.const_Section) != null)
            {
                retValue = ini.ReadString(myClient + InitValue_VE_OdettePath.const_Key, InitValue_VE_OdettePath.const_Section);
                //bool.TryParse(strTmp, out retValue);
            }
            return retValue;
        }
    }
}
