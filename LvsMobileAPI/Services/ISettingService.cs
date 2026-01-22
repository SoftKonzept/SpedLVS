using LVS.InitValue;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface ISettingService
    {
        public List<string> GET_Printers();
    }


    public class SettingService : ISettingService
    {
        private SvrSettings srv;

        public SettingService()
        {
            srv = new SvrSettings();
        }

        public List<string> GET_Printers()
        {
            List<string> retList = new List<string>();
            retList = InitValue_Printer.Printer();
            return retList;
        }

    }


}
