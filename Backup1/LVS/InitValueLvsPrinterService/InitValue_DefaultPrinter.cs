using LVS;

namespace LVS.InitValueLvsPrinterService
{
    public class InitValue_PrintServicePrinter_Default
    {
        public const string const_Section = "PrintServicePrinter";
        public const string const_Key = "Default";
        public static string DefaultPrinter()
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            string retDefaultPrinter = string.Empty;
            if (ini.ReadString("PrintServicePrinter", "Default") != null)
            {
                retDefaultPrinter = ini.ReadString("PrintServicePrinter", "Default");
            }
            return retDefaultPrinter;
        }
    }
}
