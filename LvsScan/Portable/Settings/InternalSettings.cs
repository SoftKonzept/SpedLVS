using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace LvsScan.Portable.Settings
{
    public static class InternalSettings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        public static string Username
        {
            get
            {
                return AppSettings.GetValueOrDefault("Username", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Username", value);
            }
        }
        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("Password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Password", value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }
        public static bool AccessGranted
        {
            get
            {
                bool flag = false;
                bool tmpBool = false;
                flag = AppSettings.GetValueOrDefault("AccessGranted", tmpBool);
                return flag;
            }
            set
            {
                string strBool = value.ToString();
                bool flag = false;
                bool.TryParse(strBool, out flag);
                AppSettings.AddOrUpdateValue("AccessGranted", flag);
            }

        }

        public static int UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault("UserId", 0);
            }
            set
            {
                AppSettings.AddOrUpdateValue("UserId", value);
            }
        }

    }
}
