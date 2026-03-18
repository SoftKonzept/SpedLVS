using System.Collections.Generic;
using System.Drawing.Printing;

namespace LVS.Print
{
    public class PrinterSettings_Printer
    {
        public static List<string> GetPrinter()
        {
            List<string> printer = new List<string>();
            foreach (string Name in PrinterSettings.InstalledPrinters)
            {
                printer.Add(Name);
            }
            return printer;
        }
    }
}
