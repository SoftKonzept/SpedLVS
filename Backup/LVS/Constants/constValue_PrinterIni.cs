using System.IO;

namespace LVS.Constants
{
    public class constValue_PrinterIni
    {
        public const string const_localConfigPath = "C:\\LVS\\Config\\";
        public const string const_localConfigINIMainFileName = "printer.ini";
        public int UserId { get; set; } = 0;

        public string IniFilePaht
        {
            get
            {
                string strTmpIniFilePath = Path.Combine(const_localConfigPath, UserId.ToString() + "_" + const_localConfigINIMainFileName);
                return strTmpIniFilePath;
            }
        }

        public constValue_PrinterIni(int myUserId)
        {
            UserId = myUserId;
        }
    }
}
