using System.Collections.Generic;

namespace LVS.InitValue
{
    public class InitValue_Printer
    {
        public const string const_Section = "Printerlist";
        public const string const_Key1 = "Printer#1";
        public const string const_Key2 = "Printer#2";
        public const string const_Key3 = "Printer#3";
        public const string const_Key4 = "Printer#4";
        public const string const_Key5 = "Printer#5";


        public static List<string> Printer()
        {
            int iPrinterCount = 5;
            clsINI.clsINI ini = GlobalINI.GetINI();
            List<string> listReturn = new List<string>();

            for (int i = 1; i <= iPrinterCount; i++)
            {
                string strKey = "Printer#" + i;
                if (ini.ReadString(const_Section, strKey) != null)
                {
                    string strTmp = ini.ReadString(const_Section, strKey);
                    if ((!strTmp.Equals(string.Empty)) && (!listReturn.Contains(strTmp)))
                    {
                        listReturn.Add(strTmp);
                    }
                }
            }
            return listReturn;
        }
    }
}
