using LVS;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CustomizedUpdates.MainSystem
{
    public class SystemMain
    {
        public string StartupPath { get; set; } = string.Empty;
        internal clsINI.clsINI INI = new clsINI.clsINI();
        public clsClient Client;
        public List<string> listLogToFileSystem = new List<string>();
        public SystemMain()
        {
            this.StartupPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);      // System.IO.Path.GetDirectoryName(SystemMain.Reflection.Assembly.GetExecutingAssembly().Location);
            INI = GlobalINI.GetINI();
        }

        public void InitSystem(ref Globals._GL_SYSTEM GLSystem, decimal program = 0)
        {
            this.listLogToFileSystem = new List<string>();

            string strClientMatchCode = INI.ReadString("CLIENT", "Matchcode");
            Client = new clsClient();
            Client.InitClass(strClientMatchCode);

            //hier müssen erst die Connectiondaten gesetzt werden, da sonst keine DB-Verbindung möglich ist
            GLSystem.con_UserDB = this.con_UserDB;
            GLSystem.con_PassDB = this.con_PassDB;
            GLSystem.con_Server = this.con_Server;
            GLSystem.con_Database = this.con_Database;

            GLSystem.con_UserDB_COM = this.con_UserDB_Com;
            GLSystem.con_PassDB_COM = this.con_PassDB_Com;
            GLSystem.con_Server_COM = this.con_Server_Com;
            GLSystem.con_Database_COM = this.con_Database_Com;

            GLSystem.con_UserDB_CALL = this.con_UserDB_Call;
            GLSystem.con_PassDB_CALL = this.con_PassDB_Call;
            GLSystem.con_Server_CALL = this.con_Server_Call;
            GLSystem.con_Database_CALL = this.con_Database_Call;

            GLSystem.con_UserDB_ARCHIVE = this.con_UserDB_Archive;
            GLSystem.con_PassDB_ARCHIVE = this.con_PassDB_Archive;
            GLSystem.con_Server_ARCHIVE = this.con_Server_Archive;
            GLSystem.con_Database_ARCHIVE = this.con_Database_Archive;

            LVS.clsSQLcon.Server = this.con_Server;
            LVS.clsSQLcon.Database = this.con_Database;
            LVS.clsSQLcon.User = this.con_UserDB;
            LVS.clsSQLcon.Password = this.con_PassDB;

            LVS.clsSQLCOM.Server = this.con_Server_Com;
            LVS.clsSQLCOM.Database = this.con_Database_Com;
            LVS.clsSQLCOM.User = this.con_UserDB_Com;
            LVS.clsSQLCOM.Password = this.con_PassDB_Com;

            LVS.clsSQLCall.Server = this.con_Server_Call;
            LVS.clsSQLCall.Database = this.con_Database_Call;
            LVS.clsSQLCall.User = this.con_UserDB_Call;
            LVS.clsSQLCall.Password = this.con_PassDB_Call;

            LVS.clsSQLARCHIVE.Server = this.con_Server_Archive;
            LVS.clsSQLARCHIVE.Database = this.con_Database_Archive;
            LVS.clsSQLARCHIVE.User = this.con_UserDB_Archive;
            LVS.clsSQLARCHIVE.Password = this.con_PassDB_Archive;

            Functions.init_con(ref GLSystem);
            Functions.init_conCOM(ref GLSystem, ref listLogToFileSystem);
            Functions.init_conCALL(ref GLSystem, ref listLogToFileSystem);
            Functions.init_conARCHIV(ref GLSystem, ref listLogToFileSystem);
        }

        /**********************************************************************************
          *          con_xxxx  -  connection Database SPED
          * ********************************************************************************/
        private string _con_UserDB;
        public string con_UserDB
        {
            get
            {
                _con_UserDB = INI.ReadString(Client.MatchCode + "Config", "User");
                return _con_UserDB;
            }
            set { _con_UserDB = value; }
        }

        private string _con_PassDB;
        public string con_PassDB
        {
            get
            {
                _con_PassDB = INI.ReadString(Client.MatchCode + "Config", "pw");
                return _con_PassDB;
            }
            set { _con_PassDB = value; }
        }

        private string _con_Server;
        public string con_Server
        {
            get
            {
                _con_Server = INI.ReadString(Client.MatchCode + "Config", "Server");
                return _con_Server;
            }
            set { _con_Server = value; }
        }

        private string _con_Database;
        public string con_Database
        {
            get
            {
                _con_Database = INI.ReadString(Client.MatchCode + "Config", "Database");
                return _con_Database;
            }
            set { _con_Database = value; }
        }
        /**********************************************************************************
           *          con_xxxx  -  connection Database Communicator
           * ********************************************************************************/
        private string _con_UserDB_Com;
        public string con_UserDB_Com
        {
            get
            {
                _con_UserDB_Com = INI.ReadString(Client.MatchCode + "ConfigCOM", "User");
                return _con_UserDB_Com;
            }
            set { _con_UserDB_Com = value; }
        }

        private string _con_PassDB_Com;
        public string con_PassDB_Com
        {
            get
            {
                _con_PassDB_Com = INI.ReadString(Client.MatchCode + "ConfigCOM", "pw");
                return _con_PassDB_Com;
            }
            set { _con_PassDB_Com = value; }
        }

        private string _con_Server_Com;
        public string con_Server_Com
        {
            get
            {
                _con_Server_Com = INI.ReadString(Client.MatchCode + "ConfigCOM", "Server");
                return _con_Server_Com;
            }
            set { _con_Server = value; }
        }

        private string _con_Database_Com;
        public string con_Database_Com
        {
            get
            {
                _con_Database_Com = INI.ReadString(Client.MatchCode + "ConfigCOM", "Database");
                return _con_Database_Com;
            }
            set { _con_Database_Com = value; }
        }
        /**********************************************************************************
            *          con_xxxx  -  connection Database CALL
            * ********************************************************************************/
        private string _con_UserDB_Call;
        public string con_UserDB_Call
        {
            get
            {
                _con_UserDB_Com = INI.ReadString(Client.MatchCode + "ConfigCALL", "User");
                return _con_UserDB_Com;
            }
            set { _con_UserDB_Com = value; }
        }

        private string _con_PassDB_Call;
        public string con_PassDB_Call
        {
            get
            {
                _con_PassDB_Call = INI.ReadString(Client.MatchCode + "ConfigCALL", "pw");
                return _con_PassDB_Call;
            }
            set { _con_PassDB_Call = value; }
        }

        private string _con_Server_Call;
        public string con_Server_Call
        {
            get
            {
                _con_Server_Call = INI.ReadString(Client.MatchCode + "ConfigCALL", "Server");
                return _con_Server_Call;
            }
            set { _con_Server_Call = value; }
        }

        private string _con_Database_Call;
        public string con_Database_Call
        {
            get
            {
                _con_Database_Call = INI.ReadString(Client.MatchCode + "ConfigCALL", "Database");
                return _con_Database_Call;
            }
            set { _con_Database_Call = value; }
        }

        /**********************************************************************************
    *          con_xxxx  -  connection Database Archive
    * ********************************************************************************/
        private string _con_UserDB_Archive;
        public string con_UserDB_Archive
        {
            get
            {
                _con_UserDB_Archive = INI.ReadString(Client.MatchCode + "ConfigARCHIV", "User");
                return _con_UserDB_Archive;
            }
            set { _con_UserDB_Archive = value; }
        }

        private string _con_PassDB_Archive;
        public string con_PassDB_Archive
        {
            get
            {
                _con_PassDB_Archive = INI.ReadString(Client.MatchCode + "ConfigARCHIV", "pw");
                return _con_PassDB_Archive;
            }
            set { _con_PassDB_Archive = value; }
        }

        private string _con_Server_Archive;
        public string con_Server_Archive
        {
            get
            {
                _con_Server_Archive = INI.ReadString(Client.MatchCode + "ConfigARCHIV", "Server");
                return _con_Server_Archive;
            }
            set { _con_Server_Archive = value; }
        }

        private string _con_Database_Archive;
        public string con_Database_Archive
        {
            get
            {
                _con_Database_Archive = INI.ReadString(Client.MatchCode + "ConfigARCHIV", "Database");
                return _con_Database_Archive;
            }
            set { _con_Database_Archive = value; }
        }
    }
}
