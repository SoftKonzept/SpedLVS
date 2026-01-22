using LVS;

namespace LVS.InitValueLvsPrinterService
{
    public class InitValue_Settings
    {
        /// <summary>
        ///             SETTINGS
        ///             -Properties
        ///                 > TimerElapsedDuration 
        ///                 > LogEnabled
        /// </summary>

        public const string const_Section = "SETTINGS";

        public const string const_Key_TimerElapsedDuration = "TimerElapsedDuration";
        public static int TimerElapsedDuration()
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            int retDuration = 10000; // 10 Sec
            if (ini.ReadString(InitValue_Settings.const_Section, InitValue_Settings.const_Key_TimerElapsedDuration) != null)
            {
                string tmpDuration = ini.ReadString(InitValue_Settings.const_Section, InitValue_Settings.const_Key_TimerElapsedDuration);
                int.TryParse(tmpDuration, out retDuration);
                if (retDuration < 10000)
                {
                    retDuration = 10000;
                }
            }
            return retDuration;
        }

        public const string const_Key_LogEnabled = "LogEnabled";
        public static bool LogEnabled()
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            bool retEnabled = false; // 10 Sec

            if (ini.ReadString(InitValue_Settings.const_Section, InitValue_Settings.const_Key_LogEnabled) != null)
            {
                string tmp = ini.ReadString(InitValue_Settings.const_Section, InitValue_Settings.const_Key_LogEnabled);

                switch (tmp.ToUpper())
                {
                    case "TRUE":
                        retEnabled = true;
                        break;
                    case "FALSE":
                        retEnabled = false;
                        break;
                }
            }
            return retEnabled;
        }

        public const string const_Key_CheckPrinterList = "CheckPrinterList";
        public static bool CheckPrinterList()
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            bool retEnabled = false; // 10 Sec
            bool bSectionExist = ini.SectionNames().Contains(InitValue_Settings.const_Section);

            if (ini.SectionNames().Contains(InitValue_Settings.const_Section))
            {
                if (ini.ReadString(InitValue_Settings.const_Section, InitValue_Settings.const_Key_CheckPrinterList) != null)
                {
                    string tmp = ini.ReadString(InitValue_Settings.const_Section, InitValue_Settings.const_Key_CheckPrinterList);

                    switch (tmp.ToUpper())
                    {
                        case "TRUE":
                            retEnabled = true;
                            break;
                        case "FALSE":
                            retEnabled = false;
                            break;
                    }
                }
            }
            return retEnabled;
        }
    }
}
