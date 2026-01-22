using Common.Enumerations;

namespace LvsMobileAPI.DataConnection
{
    public class SvrSettings
    {
        public clsINI.clsINI ini = new clsINI.clsINI();
        internal enumAppType AppType;
        internal AppCustomer AppCustomer;
        internal string AppStartupPath = String.Empty;
        internal string AppTypeString = string.Empty;
        internal Dictionary<string, string> DictAppSettings = new Dictionary<string, string>();

        public SvrSettings()
        {
            AppStartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (Directory.Exists(AppStartupPath))
            {
                ini = new clsINI.clsINI(AppStartupPath + "\\Config.ini");
                if (ini != null)
                {
                    Matchcode = string.Empty;
                    SrvName = string.Empty;
                    AppTypeString = string.Empty;

                    Matchcode = ini.ReadString("Client", "Matchcode");
                    SrvName = ini.ReadString("Server", "Server");
                    //AppTypeString = 

                    enumAppType AppType = LVS.InitValue.InitValue_GlobalSettings.AppType();
                    //Enum.TryParse(AppTypeString, out AppType);


                    LVS.clsSQLcon.Server = SrvName;
                    LVS.clsSQLcon.Database = SrvDBNameLvs;
                    LVS.clsSQLcon.User = SrvUserName;
                    LVS.clsSQLcon.Password = SrvPassword;

                    LVS.clsSQLCOM.Server = LVS.clsSQLcon.Server;
                    LVS.clsSQLCOM.Database = SrvDBNameCom;
                    LVS.clsSQLCOM.User = LVS.clsSQLcon.User;
                    LVS.clsSQLCOM.Password = LVS.clsSQLcon.Password;

                    LVS.clsSQLCall.Server = LVS.clsSQLcon.Server;
                    LVS.clsSQLCall.Database = SrvDBNameCall;
                    LVS.clsSQLCall.User = LVS.clsSQLcon.User;
                    LVS.clsSQLCall.Password = LVS.clsSQLcon.Password;

                    LVS.clsSQLARCHIVE.Server = LVS.clsSQLcon.Server;
                    LVS.clsSQLARCHIVE.Database = SrvDBNameArchiv;
                    LVS.clsSQLARCHIVE.User = LVS.clsSQLcon.User;
                    LVS.clsSQLARCHIVE.Password = LVS.clsSQLcon.Password;


                }
            }
        }

        private string _Matchcode = string.Empty;
        public string Matchcode
        {
            get
            {
                return _Matchcode;
            }
            set
            {
                _Matchcode = value;
            }
        }

        private string _srvName = string.Empty;
        public string SrvName
        {
            get { return _srvName; }
            set { _srvName = value; }
        }

        private string _srvDBNameLvs = string.Empty;
        public string SrvDBNameLvs
        {
            get
            {
                _srvDBNameLvs = Matchcode + "_LVS";
                return _srvDBNameLvs;
            }
        }

        private string _srvDBNameCom = string.Empty;
        public string SrvDBNameCom
        {
            get
            {
                _srvDBNameCom = Matchcode + "_COM";
                return _srvDBNameCom;
            }
        }

        private string _srvDBNameCall = string.Empty;
        public string SrvDBNameCall
        {
            get
            {
                _srvDBNameCall = Matchcode + "_Call";
                return _srvDBNameCall;
            }
        }

        private string _srvDBNameArchiv = string.Empty;
        public string SrvDBNameArchiv
        {
            get
            {
                _srvDBNameArchiv = Matchcode + "_ARCHIV";
                return _srvDBNameArchiv;
            }
        }

        private string _srvUserName = string.Empty;
        public string SrvUserName
        {
            get
            {
#if DEBUG
                _srvUserName = "sa";
#else
                _srvUserName = "LvsUser";
#endif
                return _srvUserName;
            }
        }

        private string _srvPassword = string.Empty;
        public string SrvPassword
        {
            get
            {
#if DEBUG
                _srvPassword = "masterkey";
#else
                _srvPassword = "Lvs@comtec.24";
#endif
                return _srvPassword;
            }
        }

        private string _sqlConString = string.Empty;
        public string sqlConString
        {
            get
            {
                //string str = app.
                string tmp = string.Empty;
                switch (AppType)
                {
                    case enumAppType.Communicator:
                        tmp = $"Server={SrvName};Database={SrvDBNameCom};User Id={SrvUserName}; Password={SrvPassword};";
                        break;
                    case enumAppType.LVSCall:
                        tmp = $"Server={SrvName};Database={SrvDBNameCall};User Id={SrvUserName}; Password={SrvPassword};";
                        break;
                    case enumAppType.Sped4:
                    case enumAppType.LVSScan:
                    case enumAppType.LvsMobileAPI:
                        tmp = $"Server={SrvName};Database={SrvDBNameLvs};User Id={SrvUserName}; Password={SrvPassword};";
                        break;
                    case enumAppType.LvsPrintService:
                        tmp = $"Server={SrvName};Database={SrvDBNameArchiv};User Id={SrvUserName}; Password={SrvPassword};";
                        break;
                }
                _sqlConString = tmp;
                return _sqlConString;
            }
            set
            {
                _sqlConString = value;
            }
        }
    }
}
