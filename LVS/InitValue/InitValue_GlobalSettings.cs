using Common.Enumerations;
using System;

namespace LVS.InitValue
{
    public class InitValue_GlobalSettings
    {

        public const string const_Section = "GLOBALSETTINGS";

        public const string const_Key_AppType = "AppType";
        public static enumAppType AppType()
        {
            clsINI.clsINI ini = GlobalINI.GetINI();
            string appTypeString = string.Empty;
            enumAppType appTypeReturn = enumAppType.NotSet;

            if (ini.ReadString(InitValue_GlobalSettings.const_Section, InitValue_GlobalSettings.const_Key_AppType) != null)
            {
                appTypeString = ini.ReadString(InitValue_GlobalSettings.const_Section, InitValue_GlobalSettings.const_Key_AppType);
            }
            if (appTypeString.Equals(string.Empty))
            {
                appTypeReturn = enumAppType.NotSet;
            }
            else
            {
                Enum.TryParse(appTypeString, out appTypeReturn);
            }

            return appTypeReturn;
        }

    }
}
