using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LVS;

namespace Import
{
    public class clsSystemImport
    {
        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GLUser;
        public clsSystem System;

        public bool Import_GutOnlyIsUsed { set; get; }
        public bool Import_CreateNewDestDB { set; get; }
        public DateTime CalcDateToKeep { set; get; }

        public clsSystemImport(Globals._GL_SYSTEM myGLSystem)
        {
            GLSystem = myGLSystem;
            if (this.CheckConnectionSped)
            {
                clsUser ImpUser = new clsUser();
                ImpUser.ID = 1;
                this.GLUser = ImpUser.Fill();
            }
        }

        clsINI.clsINI INI = new clsINI.clsINI(Application.StartupPath + "\\Config.ini");
        public clsArbeitsbereiche AbBereich;
        public clsMandanten Mandant;

        public Globals._GL_USER _GL_User;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        /**********************************************************************************
         *          con_xxxx  -  connection Database SPED
         * ********************************************************************************/
        private string _con_UserDB;
        public string con_UserDB
        {
            get
            {
                _con_UserDB = INI.ReadString("Config", "User");
                return _con_UserDB;
            }
            set { _con_UserDB = value; }
        }

        private string _con_PassDB;
        public string con_PassDB
        {
            get
            {
                _con_PassDB = INI.ReadString("Config", "pw");
                return _con_PassDB;
            }
            set { _con_PassDB = value; }
        }

        private string _con_Server;
        public string con_Server
        {
            get
            {
                _con_Server = INI.ReadString("Config", "Server");
                return _con_Server;
            }
            set { _con_Server = value; }
        }

        private string _con_Database;
        public string con_Database
        {
            get
            {
                _con_Database = INI.ReadString("Config", "Database");
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
                _con_UserDB_Com = Globals.INI.ReadString("ConfigCOM", "User");
                return _con_UserDB_Com;
            }
            set { _con_UserDB_Com = value; }
        }

        private string _con_PassDB_Com;
        public string con_PassDB_Com
        {
            get
            {
                _con_PassDB_Com = Globals.INI.ReadString("ConfigCOM", "pw");
                return _con_PassDB_Com;
            }
            set { _con_PassDB_Com = value; }
        }

        private string _con_Server_Com;
        public string con_Server_Com
        {
            get
            {
                _con_Server_Com = Globals.INI.ReadString("ConfigCOM", "Server");
                return _con_Server_Com;
            }
            set { _con_Server = value; }
        }

        private string _con_Database_Com;
        public string con_Database_Com
        {
            get
            {
                _con_Database_Com = Globals.INI.ReadString("ConfigCOM", "Database");
                return _con_Database_Com;
            }
            set { _con_Database_Com = value; }
        }



        /**********************************************************************************
        *          con_xxxx  -  connection Database Import
        * ********************************************************************************/
        public string con_UserImp { get; set; }
        public string con_PassDBImp { get; set; }
        public string con_ServerImp { get; set; }
        public string con_DatabaseImp { get; set; }







        public bool CheckConnectionSped
        {
            get
            {
                GLSystem.con_UserDB = this.con_UserDB;
                GLSystem.con_PassDB = this.con_PassDB;
                GLSystem.con_Server = this.con_Server;
                GLSystem.con_Database = this.con_Database;
                bool retVal = clsInitDBConnection.init_conLVS(ref GLSystem);
                return retVal;
            }
        }
        public bool CheckConnectionLVSOld
        {
            get
            {
                GLSystem.con_UserDB_Imp = this.con_UserImp;
                GLSystem.con_PassDB_Imp = this.con_PassDBImp;
                GLSystem.con_Server_Imp = this.con_ServerImp;
                GLSystem.con_Database_Imp = this.con_DatabaseImp;
                bool retVal = clsInitDBConnection.init_conLVSOld(ref GLSystem);
                return retVal;
            }
        }
        public bool CheckConnectionCom
        {
            get
            {
                GLSystem.con_UserDB_COM = this.con_UserDB_Com;
                GLSystem.con_PassDB_COM = this.con_PassDB_Com;
                GLSystem.con_Server_COM = this.con_Server_Com;
                GLSystem.con_Database_COM = this.con_Database_Com;
                bool retVal = clsInitDBConnection.init_conCOM(ref GLSystem);
                return retVal;
            }
        }

    }
}
