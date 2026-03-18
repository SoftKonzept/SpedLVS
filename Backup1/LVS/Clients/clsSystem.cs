using Common.Enumerations;
using LVS.Constants;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace LVS
{
    public class clsSystem
    {
        public const string const_Prozess_InitClassSystem = "Init. clsSystem";
        public clsReportDocSetting ReportDocSetting = new clsReportDocSetting();

        public List<string> listLogToFileSystem = new List<string>();

        /**************************************************************************
         *              const
         * ************************************************************************/

        public static readonly Color const_DefaultColorLvsSped_BaseColor = Color.FromArgb(4, 72, 117);
        public static readonly Color const_DefaultColorLvsSped_BaseColor2 = Color.FromArgb(255, 128, 0);
        public static readonly Color const_DefaultColorLvsSped_EffecColor = Color.FromArgb(191, 219, 255);
        public static readonly Color const_DefaultColorLvsSped_EffectColor2 = Color.FromArgb(255, 177, 99);
        public static readonly Color const_DefaultColorLvsSped_BackColor = Color.FromArgb(255, 255, 255);

        public static readonly DateTime const_DefaultDateTimeValue_Min = new DateTime(1900, 1, 1);
        public static readonly DateTime const_DefaultDateTimeValue_Max = new DateTime(2100, 1, 1);
        public static readonly string const_Default_ArtikelIDRefSeparator = "#";

        public static readonly Int32 const_Default_MainThreadWaitDuration = 60000;
        public static readonly Int32 const_Default_TaksThreadWaitDuration = 5000;
        public static readonly string const_Default_TaskSeparator = "----------------------------------------------------";
        public static readonly decimal const_Default_MaxLenghtBleche = 12000;


        ///<summary>csl / Copy</summary>
        ///<remarks></remarks>
        public clsSystem Copy()
        {
            return (clsSystem)this.MemberwiseClone();
        }

        internal clsINI.clsINI INI = new clsINI.clsINI();
        public string StartupPath { get; set; } = string.Empty;

        public clsSystem()
        {
            this.StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            INI = GlobalINI.GetINI();
        }
        public clsSystem(string myStartupPath) : this()
        {
            this.StartupPath = myStartupPath;
        }
        ///<summary>clsSystem / InitSystem</summary>
        ///<remarks></remarks>
        ///

        public void InitSystem(ref Globals._GL_SYSTEM GLSystem, decimal program = 0)
        {

            this.listLogToFileSystem = new List<string>();
            this.listLogToFileSystem.Add(clsLogbuchCon.const_Logbuch_Trennzeichen);
            string strTmp = string.Empty;
            strTmp = Functions.GetLogString(strTmp, "[System-Start] - Class System wird initialisiert", false);
            this.listLogToFileSystem.Add(strTmp);


            if (program == 0)
            {
                string strClientMatchCode = INI.ReadString("CLIENT", "Matchcode");

                Client = new clsClient();
                Client.InitClass(strClientMatchCode);

                //enumAppType appType = InitValue.InitValue_GlobalSettings.AppType();


                if (!this.VE_IsWatchDog)
                {
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

                    Client.InitSubClass();
                }


                GLSystem.client_MatchCode = this.Client.MatchCode;

                if (!this.VE_IsWatchDog)
                {
                    GLSystem.client_CompanyName = this.Client.ADR.Name1 + this.Client.ADR.Name2;
                    GLSystem.client_Strasse = this.Client.ADR.Str + " " + this.Client.ADR.HausNr;
                    GLSystem.client_PLZOrt = this.Client.ADR.PLZ + " " + this.Client.ADR.Ort;
                    GLSystem.client_AdrID = this.Client.ADR.ID;
                    this.UserQuantity = this.Client.UserAnzahl; //wird im Bereich System gesetzt
                                                                //}

                    //Module
                    GLSystem.Modul_Stammdaten_Adressen = Client.Modul.Stammdaten_Adressen;
                    GLSystem.Modul_Stammdaten_Personal = Client.Modul.Stammdaten_Personal;
                    GLSystem.Modul_Stammdaten_Fahrzeuge = Client.Modul.Stammdaten_Fahrzeuge;
                    GLSystem.Modul_Stammdaten_Gut = Client.Modul.Stammdaten_Gut;
                    GLSystem.Modul_Stammdaten_Relation = Client.Modul.Stammdaten_Relation;
                    GLSystem.Modul_Stammdaten_Lagerortverwaltung = Client.Modul.Stammdaten_Lagerortverwaltung;
                    GLSystem.Modul_Stammdaten_Schaeden = Client.Modul.Stammdaten_Schaeden;
                    GLSystem.Modul_Stammdaten_Einheiten = Client.Modul.Stammdaten_Einheiten;

                    //Disposition
                    GLSystem.Modul_Spedition = Client.Modul.Spedition;
                    GLSystem.Modul_Spedition_Auftragserfassung = Client.Modul.Spedition_Auftragserfassung;
                    GLSystem.Modul_Spedition_Dispo = Client.Modul.Spedition_Dispo;

                    //Lagerverwaltung
                    GLSystem.Modul_Lager_Einlagerung = Client.Modul.Lager_Einlagerung;
                    GLSystem.Modul_Lager_Auslagerung = Client.Modul.Lager_Auslagerung;
                    GLSystem.Modul_Lager_Umbuchung = Client.Modul.Lager_Umbuchung;
                    GLSystem.Modul_Lager_Journal = Client.Modul.Lager_Journal;
                    GLSystem.Modul_Lager_Bestandsliste = Client.Modul.Lager_Bestandsliste;
                    GLSystem.Modul_Lager_Sperrlager = Client.Modul.Lager_Sperrlager;


                    //LvsSCan
                    GLSystem.Modul_LvsScan = Client.Modul.LvsScan;
                    GLSystem.Modul_LvsScan_Inventory_List = Client.Modul.LvsScan_Inventory_List;
                    GLSystem.Modul_LvsScan_Inventory_Scan = Client.Modul.LvsScan_Inventory_Scan;

                    //Fakturierungsys_MandantenID
                    GLSystem.Modul_Fakt_Lager = Client.Modul.Fakt_Lager;
                    GLSystem.Modul_Fakt_Spedition = Client.Modul.Fakt_Spedition;
                    GLSystem.Modul_Fakt_Manuell = Client.Modul.Fakt_Manuell;
                    GLSystem.Modul_Fakt_Sonderkosten = Client.Modul.Fakt_Sonderkosten;
                    GLSystem.Modul_Fakt_UB_DifferentCalcAssignment = Client.Modul.Fakt_UB_DifferentCalcAssignment;
                    GLSystem.Modul_Fakt_Rechnungsbuch = Client.Modul.Fakt_Rechnungsbuch;
                    GLSystem.Modul_Fakt_LagerManuellSelection = Client.Modul.Fakt_LagerManuellSelection;
                }
                // system
                if (this.VE_IsWatchDog)
                {
                    GLSystem.sys_VersionApp = string.Empty;
                }
                else
                {
                    GLSystem.sys_VersionApp = this.SystemVersionApp; //muss als erstes zugewiesen werden, dann wird auch automatisch SystemVersionAppDecimal zugewiesen

                    GLSystem.sys_VersionAppDecimal = this.SystemVersionAppDecimal;
                    GLSystem.sys_VersionSystemBuilt = this.SystemVersionBuilt;
                    GLSystem.sys_UserQuantity = this.UserQuantity;
                    GLSystem.sys_WorkingPathExport = this.WorkingPathExport;

                    GLSystem.sys_VersionARCHIV = this.SystemVersionAppArchive;
                    GLSystem.sys_VersionARCHIVDecimal = this.SystemVersionAppDecimalArchive;

                    GLSystem.DictPrintViews = this.Client.DictPrintViews;
                    GLSystem.DictViews = this.Client.DictViews;
                    GLSystem.DictOrders = this.Client.DictOrders;

                    //Module
                    GLSystem.Modul_Xml_Uniport_CreateDirect_LAusgang = this.Client.Modul.Xml_Uniport_CreateDirect_LAusgang;
                    GLSystem.Modul_Xml_Uniport_CreateDirect_LEingang = this.Client.Modul.Xml_Uniport_CreateDirect_LEingang;
                    GLSystem.Modul_VDA_Use_KFZ = this.Client.Modul.VDA_Use_KFZ;
                }

                //GLSystem.sys_VersionAppDecimal = this.SystemVersionAppDecimal;
                //GLSystem.sys_VersionSystemBuilt = this.SystemVersionBuilt;
                //GLSystem.sys_UserQuantity = this.UserQuantity;
                //GLSystem.sys_WorkingPathExport = this.WorkingPathExport;

                //GLSystem.sys_VersionARCHIV = this.SystemVersionAppArchive;
                //GLSystem.sys_VersionARCHIVDecimal = this.SystemVersionAppDecimalArchive;

                // Views
                //GLSystem.DictPrintViews = this.Client.DictPrintViews;
                //GLSystem.DictViews = this.Client.DictViews;
                //GLSystem.DictOrders = this.Client.DictOrders;


                //connection FTP
                GLSystem.con_FtpUser = this.con_FtpUser;
                GLSystem.con_FtpPass = this.con_FtpPass;
                GLSystem.con_FtpServer = this.con_FtpServer;

                //Voreinstellungen 
                GLSystem.VE_OdettePath = this.VE_OdettePath;
                GLSystem.VE_MainThreadDuration = this.VE_MainThreadDuration;
                //GLSystem.VE_DocScanPath = this.VE_DocScanPath;

                ////Module
                //GLSystem.Modul_Xml_Uniport_CreateDirect_LAusgang = this.Client.Modul.Xml_Uniport_CreateDirect_LAusgang;
                //GLSystem.Modul_Xml_Uniport_CreateDirect_LEingang = this.Client.Modul.Xml_Uniport_CreateDirect_LEingang;
                //GLSystem.Modul_VDA_Use_KFZ = this.Client.Modul.VDA_Use_KFZ;
            }
        }


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

        private string _Client;
        public string strClient
        {
            get
            {
                _Client = INI.ReadString("CLIENT", "Matchcode");
                return _Client;
            }
            set { _Client = value; }
        }

        private string _SystemInfo;
        public string SystemInfo
        {
            get
            {
                _SystemInfo = INI.ReadString("SYSTEMINFO", "ID");
                return _SystemInfo;
            }
            set { _SystemInfo = value; }
        }

        private bool _IsTestsystem;
        public bool IsTestsystem
        {
            get
            {
                int iTmp = 0;
                int.TryParse(INI.ReadString("SYSTEMINFO", "IsTestsystem"), out iTmp);
                _IsTestsystem = Convert.ToBoolean(iTmp);
                return _IsTestsystem;
            }
            set { _IsTestsystem = value; }
        }

        public clsClient Client;
        public clsArbeitsbereiche AbBereich;
        public clsMandanten Mandant;
        public List<string> ListDocArtInUse;

        public bool DebugModeCOM
        {
            get
            {
                bool bTmp = false;
                string tmp = INI.ReadString("DEBUG", "Communicator");
                if (tmp != string.Empty)
                {
                    Boolean.TryParse(tmp, out bTmp);
                }
                return bTmp;
            }
        }

        public bool DebugModeARCHIVE
        {
            get
            {
                bool bTmp = false;
                string tmp = INI.ReadString("DEBUG", "Communicator");
                if (tmp != string.Empty)
                {
                    Boolean.TryParse(tmp, out bTmp);
                }
                return bTmp;
            }
        }
        public bool DebugModeLVSSPED
        {
            get
            {
                bool bTmp = false;
                string tmp = INI.ReadString("DEBUG", "LVSSPED");
                if (tmp != string.Empty)
                {
                    Boolean.TryParse(tmp, out bTmp);
                }
                return bTmp;
            }
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

        /********************************************************************************************
           *                      System Mail Kontakt 
           * *****************************************************************************************/

        //public const string const_Mail_SMTPServer = "smtp.1und1.de";
        //public const Int32 const_Mail_SMTPPort = 587;
        //public const string const_Mail_SMTPUser = "lvsreport@comtec-noeker.de";
        //public const string const_Mail_SMTPPasswort = "CTlvs2014";
        //public const string const_MailAdress = "lvsreport@comtec-noeker.de";

        //public const string const_Mail_SMTPServer = "smtp.ionos.de";
        //public const Int32 const_Mail_SMTPPort = 587;
        //public const string const_Mail_SMTPUser = "lvsreport@comtec-noeker.de";
        //public const string const_Mail_SMTPPasswort = "CTlvs2014";
        //public const string const_MailAdress = "lvsreport@comtec-noeker.de";

        public const string const_Mail_SMTPServer = "smtp.ionos.de";
        public const Int32 const_Mail_SMTPPort = 587;
        public const string const_Mail_SMTPUser = "support@softkonzept.com";
        public const string const_Mail_SMTPPasswort = "!29suPP%1Ay33e&fcdW";
        public const string const_MailAdress = "support@softkonzept.com";


        /*******************************************************************************************
         *                          Working Space
         * *****************************************************************************************/
        //private string _WorkingPathLVS;
        //public string WorkingPathLVS
        //{
        //    get
        //    {
        //        if (!Directory.Exists(@"C:\LVS"))
        //        {
        //            try
        //            {
        //                Directory.CreateDirectory(@"C:\LVS");
        //            }
        //            catch (Exception ex)
        //            {
        //                clsError error = new clsError();
        //                //error._GL_User = this.;
        //                error.Code = clsError.code1_501;
        //                error.Aktion = "Directory.CreateDirectory(@'C:\\LVS)";
        //                error.exceptText = ex.ToString();
        //                error.WriteError();
        //            }
        //        }
        //        _WorkingPathLVS = @"C:\LVS";
        //        return _WorkingPathLVS;
        //    }
        //    set { _WorkingPathLVS = value; }
        //}


        private string _WorkingPathPostCenter;
        public string WorkingPathPostCenter
        {
            get
            {
                if (!Directory.Exists(@"C:\LVS"))
                {
                    try
                    {
                        Directory.CreateDirectory(@"C:\LVS");
                    }
                    catch (Exception ex)
                    {
                        clsError error = new clsError();
                        //error._GL_User = this.;
                        error.Code = clsError.code1_501;
                        error.Aktion = "Directory.CreateDirectory(@'C:\\LVS)";
                        error.exceptText = ex.ToString();
                        error.WriteError();
                    }
                }
                if (!Directory.Exists(@"C:\LVS\PostCenter"))
                {
                    try
                    {
                        Directory.CreateDirectory(@"C:\LVS\PostCenter");
                    }
                    catch (Exception ex)
                    {
                        clsError error = new clsError();
                        //error._GL_User = this.;
                        error.Code = clsError.code1_501;
                        error.Aktion = "Directory.CreateDirectory(@'C:\\LVS\\PostCenter)";
                        error.exceptText = ex.ToString();
                        error.WriteError();
                    }
                }
                _WorkingPathPostCenter = @"C:\LVS\PostCenter";
                return _WorkingPathPostCenter;
            }
            set { _WorkingPathPostCenter = value; }
        }



        private string _WorkingPathExport;
        public string WorkingPathExport
        {
            get
            {
                if (!Directory.Exists(@"C:\LVS"))
                {
                    try
                    {
                        Directory.CreateDirectory(@"C:\LVS");
                    }
                    catch (Exception ex)
                    {
                        clsError error = new clsError();
                        //error._GL_User = this.;
                        error.Code = clsError.code1_501;
                        error.Aktion = "Directory.CreateDirectory(@'C:\\LVS)";
                        error.exceptText = ex.ToString();
                        error.WriteError();
                    }
                }
                if (!Directory.Exists(@"C:\LVS\Export"))
                {
                    try
                    {
                        Directory.CreateDirectory(@"C:\LVS\Export");
                    }
                    catch (Exception ex)
                    {
                        clsError error = new clsError();
                        //error._GL_User = this.;
                        error.Code = clsError.code1_501;
                        error.Aktion = "Directory.CreateDirectory(@'C:\\LVS\\Export)";
                        error.exceptText = ex.ToString();
                        error.WriteError();
                    }
                }
                _WorkingPathExport = @"C:\LVS\Export";
                return _WorkingPathExport;
            }
            set { _WorkingPathExport = value; }
        }

        /*******************************************************************************************
         *                         DocCustomIniPath
         * *****************************************************************************************/
        private string _VE_DocCustomINIPath;
        public string VE_DocCustomINIPath
        {
            get
            {
                _VE_DocCustomINIPath = INI.ReadString(strClient + "SYSTEM", "VE_DocCustomINIPath");
                return _VE_DocCustomINIPath;
            }
        }

        /*********************************************************************************************
        *                      VE - Voreinstellungen - Standard
        * ******************************************************************************************/
        public bool VE_IsWatchDog
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                bool _VE_IsWatchDog = InitValueWDService.InitValue_WD_IsWatchDog.Value(strClient);
                return _VE_IsWatchDog;
            }
        }
        public string VE_OdettePath
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                string _VE_OdettePath = InitValueWDService.InitValue_VE_OdettePath.Value(strClient);
                return _VE_OdettePath;
            }
        }

        public string VE_FilePathCheckNotFTP
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                return InitValueWDService.InitValue_VE_FilePathCheckNotFTP.Value(strClient);
            }
        }

        public bool VE_GetFilesByFTP
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                return InitValueWDService.InitValue_VE_GetFilesByFTP.Value(strClient);
            }
        }

        public Int32 VE_MainThreadDuration
        {
            get
            {
                Int32 _VE_MainThreadDuration = clsSystem.const_Default_MainThreadWaitDuration;
                string strClient = InitValue.InitValue_Client.Value();
                int iTmp = InitValueWDService.InitValue_VE_MainThreadDuration.Value(strClient);
                if (iTmp > clsSystem.const_Default_MainThreadWaitDuration)
                {
                    _VE_MainThreadDuration = iTmp;
                }
                return _VE_MainThreadDuration;
            }
            //set { _VE_MainThreadDuration = value; }
        }
        //private Int32 _VE_TaskWriteThreadDuration;
        public Int32 VE_TaskWriteThreadDuration
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                Int32 _VE_TaskWriteThreadDuration = clsSystem.const_Default_MainThreadWaitDuration;
                int iTmp = InitValueWDService.InitValue_VE_TaskWriteThreadDuration.Value(strClient);
                if (iTmp > clsSystem.const_Default_MainThreadWaitDuration)
                {
                    _VE_TaskWriteThreadDuration = iTmp;
                }
                return _VE_TaskWriteThreadDuration;
            }
            //set { _VE_TaskWriteThreadDuration = value; }
        }


        //private Int32 _VE_TaskReadThreadDuration;
        public Int32 VE_TaskReadThreadDuration
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                Int32 _VE_TaskReadThreadDuration = clsSystem.const_Default_MainThreadWaitDuration;
                int iTmp = InitValueWDService.InitValue_VE_TaskReadThreadDuration.Value(strClient);
                if (iTmp > clsSystem.const_Default_MainThreadWaitDuration)
                {
                    _VE_TaskReadThreadDuration = iTmp;
                }
                return _VE_TaskReadThreadDuration;
            }
            //set { _VE_TaskReadThreadDuration = value; }
        }

        public Int32 VE_TaskCustomizedProcessThreadDuration
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                Int32 _VE_TaskCustomizedProcessThreadDuration = clsSystem.const_Default_MainThreadWaitDuration;
                int iTmp = InitValueCommunicator.InitValueCom_TaskCustomizedProcessThreadDuration.Value(strClient);
                if (iTmp > clsSystem.const_Default_MainThreadWaitDuration)
                {
                    _VE_TaskCustomizedProcessThreadDuration = iTmp;
                }
                return _VE_TaskCustomizedProcessThreadDuration;
            }
            //set { _VE_TaskReadThreadDuration = value; }
        }

        public bool VE_TaskWriteSingleModus
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                bool _VE_TaskWriteSingleModus = InitValueWDService.InitValue_VE_TaskWriteSingleModus.Value(strClient);
                return _VE_TaskWriteSingleModus;
            }
        }
        private Int32 _VE_MaxImageCountSPLMessage;
        public Int32 VE_MaxImageCountSPLMessage
        {
            get
            {
                Int32 iTmp = 0;
                string strTmp = INI.ReadString(strClient + "SYSTEM", "VE_MaxImageCountSPLMessage");
                Int32.TryParse(strTmp, out iTmp);
                if (iTmp < 1)
                {
                    iTmp = 1;
                }
                _VE_MaxImageCountSPLMessage = iTmp;
                return _VE_MaxImageCountSPLMessage;
            }
        }

        //private bool _VE_Autostart;
        public bool VE_Autostart
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                bool _VE_Autostart = InitValueWDService.InitValue_VE_Autostart.Value(strClient);
                return _VE_Autostart;
            }
        }
        public int VE_SignOfLifeIntervall
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                int _VE_SignOfLifeIntervall = InitValueWDService.InitValue_VE_SignOfLifeIntervall.Value(strClient);
                return _VE_SignOfLifeIntervall;
            }
        }

        public int VE_MailCountSignOfLife
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                int _VE_MailCountSignOfLife = InitValueWDService.InitValue_VE_MailCountSignOfLife.Value(strClient);
                return _VE_MailCountSignOfLife;
            }
        }

        public List<string> VE_List_WDInfoMail
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                List<string> VE_List_WDInfoMail = InitValueWDService.InitValue_WD_ListCMailSoL.Value(strClient);
                return VE_List_WDInfoMail;
            }
        }
        /********************************************************************************************
        *                      System FTP TRANSFER WD
        * *****************************************************************************************/
        private string _con_FtpUser;
        public string con_FtpUser
        {
            get
            {
                _con_FtpUser = string.Empty;
                if (INI.ReadString(strClient + "FTP", "FTPUser") != null)
                {
                    _con_FtpUser = INI.ReadString(strClient + "FTP", "FTPUser");
                }
                return _con_FtpUser;
            }
            set { _con_FtpUser = value; }
        }

        private string _con_FtpPass;
        public string con_FtpPass
        {
            get
            {
                _con_FtpPass = string.Empty;
                if (INI.ReadString(strClient + "FTP", "FTPPw") != null)
                {
                    _con_FtpPass = INI.ReadString(strClient + "FTP", "FTPPw");
                }
                return _con_FtpPass;
            }
            set { _con_FtpPass = value; }
        }

        private string _con_FtpServer;
        public string con_FtpServer
        {
            get
            {
                _con_FtpServer = INI.ReadString(strClient + "FTP", "FTPServer");
                return _con_FtpServer;
            }
            set { _con_FtpServer = value; }
        }

        /*********************************************************************************************
        *                       WD - WATCHDOG
        * ******************************************************************************************/
        //public const string const_WD_FTPPath = "WDFTPPath";
        public const string const_WD_SectionKey_CreatedTimeStamp = "CreatedTimeStamp";
        //public const string const_WD_SectionKey_Alarmfactor = "Alarmfactor";
        //public const string const_WD_SectionKey_CMailCount = "CMailCount";
        //public const string const_WD_SectionKey_CMail = "CMail#";
        //public const string const_WD_SectionKey_CIntervall = "CIntervall";
        //public const string const_WD_SectionKey_OddetteProzessName = "OdetteProcessName";
        //public const string const_WD_SectionKey_WDClientCount = "WDClientCount";
        //public const string const_WD_SectionKey_WDClient = "WDClient#";
        //public const string const_WD_SectionKey_SendClientFile = "SENDCLIENTFILE";

        //private string _WD_FTPPath;
        public string WD_FTPPath
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                string _WD_FTPPath = string.Empty;
                _WD_FTPPath = InitValueWDService.InitValue_WD_WDFTPPath.Value(strClient);
                //_WD_FTPPath = INI.ReadString(strClient + "WATCHDOG", const_WD_FTPPath);
                return _WD_FTPPath;
            }
            //set { _WD_FTPPath = value; }
        }

        //private Int32 _WD_CMailCount;
        public Int32 WD_CMailCount
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                int _WD_CMailCount = InitValueWDService.InitValue_CMailCount.Value(strClient);
                return _WD_CMailCount;
            }
        }

        public Int32 WD_Alarmfactor
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                int _WD_Alarmfactor = InitValueWDService.InitValue_VE_Alarmfactor.Value(strClient);
                return _WD_Alarmfactor;
            }
        }

        //private bool _SendClientFile;
        public bool SendClientFile
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                bool _SendClientFile = true;
                _SendClientFile = InitValueWDService.InitValue_WD_SENDCLIENTFILE.Value(strClient);
                return _SendClientFile;
            }
            //set { _SendClientFile = value; }
        }

        //private List<string> _WD_ListCMail;
        public List<string> WD_ListCMail
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                List<string> _WD_ListCMail = new List<string>();
                _WD_ListCMail = InitValueWDService.InitValue_WD_ListCMail.Value(strClient);
                return _WD_ListCMail;
            }
            //set { _WD_ListCMail = value; }
        }

        public string WD_OdetteProcessName
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                string _WD_OdetteProcessName = InitValueWDService.InitValue_WD_OdetteProcessName.Value(strClient);
                return _WD_OdetteProcessName;
            }
            //set { _WD_OdetteProcessName = value; }
        }
        public Int32 WD_WDClientCount
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                Int32 _WD_WDClientCount = InitValueWDService.InitValue_WD_WDClientCount.Value(strClient);
                return _WD_WDClientCount;
            }
        }
        public List<string> WD_List_WDClient
        {
            get
            {
                string strClient = InitValue.InitValue_Client.Value();
                List<string> _WD_List_WDClient = new List<string>();
                _WD_List_WDClient = InitValueWDService.InitValue_WD_ListWDClient.Value(strClient);
                return _WD_List_WDClient;
            }
        }


        /********************************************************************************
         *                             Report
         * ******************************************************************************/
        private string _DocJournalAutoMail;
        public string DocJournalAutoMail
        {
            get
            {
                _DocJournalAutoMail = INI.ReadString(strClient + "DOKUMENTE", "DocJournalAutoMail");
                return _DocJournalAutoMail;
            }
            set { _DocJournalAutoMail = value; }
        }
        private string _DocBestandAutoMail;
        public string DocBestandAutoMail
        {
            get
            {
                _DocBestandAutoMail = INI.ReadString(strClient + "DOKUMENTE", "DocBestandAutoMail");
                return _DocBestandAutoMail;
            }
            set { _DocBestandAutoMail = value; }
        }
        private string _EingangDoc;
        public string EingangDoc
        {
            get
            {
                _EingangDoc = INI.ReadString(strClient + "DOKUMENTE", "DocEingangDoc");
                return _EingangDoc;
            }
            set { _EingangDoc = value; }
        }
        private string _AusgangDoc;
        public string AusgangDoc
        {
            get
            {
                _AusgangDoc = INI.ReadString(strClient + "DOKUMENTE", "DocAusgangDoc");
                return _AusgangDoc;
            }
            set { _AusgangDoc = value; }
        }

        /********************************************************************************************************
         *                        Versions - Info
         * *****************************************************************************************************/
        private string _SystemVersionAppArchive;
        public string SystemVersionAppArchive
        {
            get
            {
                if (!VersionARCHIVViewModel.ExistTable())
                {
                    _SystemVersionAppArchive = "0"; //Start 
                }
                else
                {
                    GetArchiveVersion();
                }
                decimal decTmp = 0;
                Decimal.TryParse(_SystemVersionAppArchive, out decTmp);
                SystemVersionAppDecimalArchive = decTmp;
                return _SystemVersionAppArchive;
            }
            set { _SystemVersionAppArchive = value; }
        }


        private string _SystemVersionApp;
        public string SystemVersionApp
        {
            get
            {
                GetVersion();
                return _SystemVersionApp;
            }
            set { _SystemVersionApp = value; }
        }
        private decimal _SystemVersionAppDecimal;
        public decimal SystemVersionAppDecimal
        {
            get { return _SystemVersionAppDecimal; }
            set { _SystemVersionAppDecimal = value; }
        }
        private decimal _SystemVersionAppDecimalArchive;
        public decimal SystemVersionAppDecimalArchive
        {
            get { return _SystemVersionAppDecimalArchive; }
            set { _SystemVersionAppDecimalArchive = value; }
        }
        private string _SystemVersionBuilt;
        public string SystemVersionBuilt
        {
            get
            {
                //_SystemVersionBuilt = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                _SystemVersionBuilt = System.Windows.Forms.Application.ProductVersion;
                return _SystemVersionBuilt;
            }
            set { _SystemVersionBuilt = value; }
        }
        private string _SystemVersionAppCOM;
        public string SystemVersionAppCOM
        {
            get
            {
                GetVersionCOM();
                return _SystemVersionAppCOM;
            }
            set { _SystemVersionAppCOM = value; }
        }
        private decimal _SystemVersionAppDecimalCOM;
        public decimal SystemVersionAppDecimalCOM
        {
            get { return _SystemVersionAppDecimalCOM; }
            set { _SystemVersionAppDecimalCOM = value; }
        }
        private string _SystemVersionBuiltCOM;
        public string SystemVersionBuiltCOM
        {
            get
            {
                _SystemVersionBuiltCOM = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                return _SystemVersionBuiltCOM;
            }
            set { _SystemVersionBuiltCOM = value; }
        }
        private bool _SystemIsInitialized;
        public bool SystemIsInitialized
        {
            get { return _SystemIsInitialized; }
            set { _SystemIsInitialized = value; }
        }
        private Int32 _UserQuantity;
        public Int32 UserQuantity
        {
            get { return _UserQuantity; }
            set { _UserQuantity = value; }
        }
        /*********************************************************************************************
         *                      VE - Voreinstellungen - Standard
        // * ******************************************************************************************/
        //private decimal _VE_Fakturierung_MwStSatz;
        //public decimal VE_Fakturierung_MwStSatz
        //{
        //    get
        //    {
        //        decimal decTmp = 0;
        //        Decimal.TryParse(Globals.INI.ReadString(Client.MatchCode + "FAKTURIERUNG", "MwStSatz"), out decTmp);
        //        _VE_Fakturierung_MwStSatz = decTmp;
        //        return _VE_Fakturierung_MwStSatz;
        //    }
        //    set { _VE_Fakturierung_MwStSatz = value; }
        //}
        //private bool _VE_Fakturierung_InvoiceDateInPast;
        //public bool VE_Fakturierung_InvoiceDateInPast
        //{
        //    get
        //    {
        //        bool bTemp = false;
        //        string strTmp = Globals.INI.ReadString(Client.MatchCode + "SYSTEM", "VE_InvoiceDateInPast");
        //        Boolean.TryParse(strTmp, out bTemp);
        //        _VE_Fakturierung_InvoiceDateInPast = bTemp;
        //        return _VE_Fakturierung_InvoiceDateInPast;
        //    }
        //    set { _VE_Fakturierung_InvoiceDateInPast = value; }
        //}

        //private string _VE_LagerOrt_AuswahlLagerOrtPlatz;
        //public string VE_LagerOrt_AuswahlLagerOrtPlatz
        //{
        //    get
        //    {
        //        _VE_LagerOrt_AuswahlLagerOrtPlatz = Globals.INI.ReadString(Client.MatchCode + "SYSTEM", "VE_AuswahlLagerOrtPlatz");
        //        return _VE_LagerOrt_AuswahlLagerOrtPlatz;
        //    }
        //    set { _VE_LagerOrt_AuswahlLagerOrtPlatz = value; }
        //}

        //private bool _VE_Lager_ASNVerkehr;
        //public bool VE_Lager_ASNVerkehr
        //{
        //    get
        //    {
        //        bool bTemp = false;
        //        string strTmp = Globals.INI.ReadString(Client.MatchCode + "SYSTEM", "VE_ASNVerkehr");
        //        Boolean.TryParse(strTmp, out bTemp);
        //        _VE_Lager_ASNVerkehr = bTemp;
        //        return _VE_Lager_ASNVerkehr;
        //    }
        //    set { _VE_Lager_ASNVerkehr = value; }
        //}



        /****************************************************************************************
         *                           COM - Einstellugen
         * **************************************************************************************/


        private string _ComPath;
        public string ComPath
        {
            get
            {
                _ComPath = INI.ReadString(Client.MatchCode + "COMMUNICATOR", "ComPath");
                return _ComPath;
            }
            set { _ComPath = value; }
        }

        private string _RETENTIONDAYS;
        public string RETENTIONDAYS
        {
            get
            {
                //string strClient = InitValue.InitValue_Client.Value();
                int iTmp = InitValueCommunicator.InitValueCom_RetentionDays.Value();
                _RETENTIONDAYS = INI.ReadString(Client.MatchCode + "COMMUNICATOR", "RETENTIONDAYS");
                return _RETENTIONDAYS;
            }
            set { _RETENTIONDAYS = value; }
        }



        /*********************************************************************************************
         *                      doc - Pfade
         * ******************************************************************************************/
        public string doc_LabelAll { get; set; }
        public string doc_LabelOne { get; set; }
        public string doc_EingangsLfs { get; set; }
        public string doc_AusgangsLfs { get; set; }
        public string doc_AusgangsDocs { get; set; }
        public string doc_AusgangsNeutralDocs { get; set; }
        public string doc_Lagerrechnung { get; set; }


        /********************************************************************************************************
         *                        Views Print
         * *****************************************************************************************************/
        private string _ViewPrintBestand;
        public string ViewPrintBestand
        {
            get
            {
                _ViewPrintBestand = INI.ReadString(strClient + "DOKUMENTE", "ViewPrintBestand");
                return _ViewPrintBestand;
            }
            set { _ViewPrintBestand = value; }
        }
        private string _ViewPrintJournal;
        public string ViewPrintJournal
        {
            get
            {
                _ViewPrintJournal = INI.ReadString(strClient + "DOKUMENTE", "ViewPrintJournal");
                return _ViewPrintJournal;
            }
            set { _ViewPrintJournal = value; }
        }


        public bool SystemExit { get; set; }
        /*******************************************************************************************
         * 
         * ****************************************************************************************/
        public bool InitDBConnection(ref Globals._GL_SYSTEM GLSystem, ref List<string> myListLogSys)
        //public bool InitDBConnection(ref FrmMain myFrmMain)
        {
            this.SystemExit = false;
            bool bReturn = false;
            string strClientMatchCode = INI.ReadString("CLIENT", "Matchcode");
            if (strClientMatchCode.Equals(string.Empty))
            {
                string strTxt = "Inder INI-Datei wurde kein Client hinterlegt! Sped4 kann nicht gestartet werden.";
                clsMessages.Allgemein_ERRORTextShow(strTxt);
                SystemExit = true;
            }
            else
            {
                Client = new clsClient();
                Client.InitClass(strClientMatchCode);


                GLSystem.con_UserDB = this.con_UserDB;
                GLSystem.con_PassDB = this.con_PassDB;
                GLSystem.con_Server = this.con_Server;
                GLSystem.con_Database = this.con_Database;
                if (Functions.init_con(ref GLSystem))
                {
                    bReturn = true;
                    GLSystem.sys_VersionApp = this.SystemVersionApp; //muss als erstes zugewiesen werden, dann wird auch automatisch SystemVersionAppDecimal zugewiesen
                    GLSystem.sys_VersionAppDecimal = this.SystemVersionAppDecimal;
                    GLSystem.sys_VersionSystemBuilt = this.SystemVersionBuilt;
                }
                else
                { bReturn = false; }

                GLSystem.con_UserDB_COM = this.con_UserDB_Com;
                GLSystem.con_PassDB_COM = this.con_PassDB_Com;
                GLSystem.con_Server_COM = this.con_Server_Com;
                GLSystem.con_Database_COM = this.con_Database_Com;
                if (Functions.init_conCOM(ref GLSystem, ref myListLogSys))
                { bReturn = true; }
                else
                { bReturn = false; }

                try
                {
                    GLSystem.con_UserDB_CALL = this.con_UserDB_Call;
                    GLSystem.con_PassDB_CALL = this.con_PassDB_Call;
                    GLSystem.con_Server_CALL = this.con_Server_Call;
                    GLSystem.con_Database_CALL = this.con_Database_Call;

                    if (Functions.init_conCALL(ref GLSystem, ref myListLogSys))
                    { }
                    else
                    { }
                }
                catch (Exception ex)
                { }

                try
                {
                    GLSystem.con_UserDB_ARCHIVE = this.con_UserDB_Archive;
                    GLSystem.con_PassDB_ARCHIVE = this.con_PassDB_Archive;
                    GLSystem.con_Server_ARCHIVE = this.con_Server_Archive;
                    GLSystem.con_Database_ARCHIVE = this.con_Database_Archive;

                    if (Functions.init_conARCHIVE(ref GLSystem, ref myListLogSys))
                    {
                        bReturn = true;

                    }
                    else
                    { bReturn = false; }

                    GLSystem.sys_VersionARCHIV = this.SystemVersionAppArchive;
                    GLSystem.sys_VersionARCHIVDecimal = this.SystemVersionAppDecimalArchive;
                }
                catch (Exception ex)
                { }

            }
            return bReturn;
        }

        ///<summary>clsSystem / ChangeWorkspace</summary>
        ///<remarks></remarks>
        public void ChangeWorkspace(ref Globals._GL_SYSTEM myGLSystem, Globals._GL_USER myGLUser, decimal myID)
        {
            AbBereich = new clsArbeitsbereiche();
            AbBereich.BenutzerID = myGLUser.User_ID;
            AbBereich.ID = myID;
            AbBereich.Fill();

            Mandant = new clsMandanten();
            Mandant.ID = AbBereich.MandantenID;
            Mandant.GetMandantByID();

            //Zuweisung
            myGLSystem.sys_ArbeitsbereichID = AbBereich.ID;
            myGLSystem.sys_MandantenID = AbBereich.MandantenID;
            myGLSystem.sys_Arbeitsbereichsname = AbBereich.ABName;
            myGLSystem.sys_Arbeitsbereich_ASNTransfer = AbBereich.ASNTransfer;

            ///---Printeinstellungen pro mandandt aus INI lesen und setzen
            GetDocPathByMandant(ref myGLSystem, this.AbBereich.MandantenID);
            GetDocPaperSourceByMandant(ref myGLSystem, this.AbBereich.MandantenID);
            GetDocPrintCount(ref myGLSystem, this.AbBereich.MandantenID);
            //GetDocPrinterByMandantSaved(ref myGLSystem, this.AbBereich.MandantenID);
            GetDocPrinterByMandantSaved(ref myGLSystem, (int)this._GL_User.User_ID);
        }
        ///<summary>clsSystem / ChangeWorkspace</summary>
        ///<remarks></remarks>
        public void ChangeLanguage(ref Globals._GL_SYSTEM myGLSystem, string myLanguage)
        {
            myGLSystem.sys_Sprache = myLanguage;
        }
        /// <summary>
        /// clsSystem / GetAuftraggeberMat
        /// </summary>
        /// <param name="myGLSystem"></param>
        /// <param name="myMandant"></param>
        public void GetAuftraggeberMat(ref Globals._GL_SYSTEM myGLSystem, decimal myMandant)
        {
            myGLSystem.AuftraggeberMat = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "EingangDocMatKunden");
        }
        /// <summary>
        /// clsSystem / GetAuftraggeberAnhangMat
        /// </summary>
        /// <param name="myGLSystem"></param>
        /// <param name="myMandant"></param>
        public void GetAuftraggeberAnhangMat(ref Globals._GL_SYSTEM myGLSystem, decimal myMandant)
        {
            myGLSystem.AuftraggeberAnhangMat = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "RGAnhangMatKunden");
        }
        /// <summary>
        /// clsSystem / GetLieferscheinMat
        /// </summary>
        /// <param name="myGLSystem"></param>
        /// <param name="myMandant"></param>
        public void GetLieferscheinMat(ref Globals._GL_SYSTEM myGLSystem, decimal myMandant)
        {
            myGLSystem.LieferscheinMat = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "LieferscheinMatKunden");
        }
        /// <summary>
        /// clsSystem / GetAuftraggeberMat
        /// </summary>
        /// <param name="myGLSystem"></param>
        /// <param name="myMandant"></param>
        /// <returns></returns>
        public Int32 GetCountPrintReport(ref Globals._GL_SYSTEM myGLSystem, decimal myMandant, string Report)
        {
            Int32 decCount = 2; // default Value
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), Report + "PrintCount"), out decCount);
            return decCount;
        }
        /// <summary>
        /// clsSystem / GetAuftraggeberMat
        /// </summary>
        /// <param name="myGLSystem"></param>
        /// <param name="myMandant"></param>
        /// <returns></returns>
        public decimal GetCountPrintDoc(ref Globals._GL_SYSTEM myGLSystem, decimal myMandant)
        {
            decimal decCount = 2; // default Value
            Decimal.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "DocPrintCount"), out decCount);
            return decCount;
        }
        /// <summary>
        /// clsSystem / GetAuftraggeberMat
        /// </summary>
        /// <param name="myGLSystem"></param>
        /// <param name="myMandant"></param>
        /// <returns></returns>
        public decimal GetCountPrintSPLLabel(ref Globals._GL_SYSTEM myGLSystem, decimal myMandant)
        {
            decimal decCount = 1; // default Value
            Decimal.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "SPLLabelPrintCount"), out decCount);
            return decCount;
        }
        ///<summary>clsSystem / GetDocPathByMandant</summary>
        ///<remarks></remarks>
        public void GetDocPathByMandant(ref Globals._GL_SYSTEM myGLSystem, decimal myMandant)
        {
            CustomizeDocPath(ref myGLSystem, 0);
        }
        ///<summary>clsSystem / CustomizeDocPath </summary>
        ///<remarks>Liest und weist die kundenspezifischen Dokumente zu</remarks>
        public void CustomizeDocPath(ref Globals._GL_SYSTEM myGLSystem, decimal myAdrID)
        {
            myGLSystem.docPath = new List<string>();
            ListDocArtInUse = new List<string>();
            //clsINI.clsINI INI_Rep = new clsINI.clsINI(Application.StartupPath + this.VE_DocCustomINIPath);

            clsINI.clsINI INI_Rep = new clsINI.clsINI(System.Windows.Forms.Application.StartupPath + this.VE_DocCustomINIPath);
            //if (myAdrID > 0)
            //{
            string strCustomPath = string.Empty;
            string strCustomSearch = string.Empty;
            //strCustomSearch = myAdrID.ToString("##0") + "_Mandant_" + this.AbBereich.MandantenID.ToString("##0");
            //string DefaultSearch = myGLSystem.client_MatchCode + "Mandant_" + this.AbBereich.MandantenID.ToString("##0");

            strCustomSearch = myAdrID.ToString("##0") + "_Mandant_" + myGLSystem.sys_MandantenID.ToString("##0");
            string DefaultSearch = myGLSystem.client_MatchCode + "Mandant_" + myGLSystem.sys_MandantenID.ToString("##0");

            string strDocElementName = string.Empty;

            //*********************************************************************************************************Eingangsdocs
            strDocElementName = "EingangAnzeige";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomSearch))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_EingangAnzeige = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_EingangAnzeige = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                try
                {
                    myGLSystem.docPath_EingangAnzeige = string.Empty;
                    if (INI.ReadString(DefaultSearch, strDocElementName) != null)
                    {
                        myGLSystem.docPath_EingangAnzeige = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
            if (!myGLSystem.docPath_EingangAnzeige.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "EingangAnzeigePerDay";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomSearch))
                {

                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_EingangAnzeigePerDay = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_EingangAnzeigePerDay = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                try
                {
                    myGLSystem.docPath_EingangAnzeigePerDay = string.Empty;
                    if (INI.ReadString(DefaultSearch, strDocElementName) != null)
                    {
                        myGLSystem.docPath_EingangAnzeigePerDay = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
            if (!myGLSystem.docPath_EingangAnzeigePerDay.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "EingangDoc";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_EingangDoc = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_EingangDoc = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                try
                {
                    myGLSystem.docPath_EingangDoc = string.Empty;
                    if (INI.ReadString(DefaultSearch, strDocElementName) != null)
                    {
                        myGLSystem.docPath_EingangDoc = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
            if (!myGLSystem.docPath_EingangDoc.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //********************************************************************************************************* EIngangsliste
            //
            strDocElementName = "Eingangsliste";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strDocElementName))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_EingangList = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_EingangList = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
                else
                {
                    try
                    {
                        myGLSystem.docPath_EingangAnzeige = string.Empty;
                        if (INI.ReadString(DefaultSearch, strDocElementName) != null)
                        {
                            myGLSystem.docPath_EingangAnzeige = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }
                }
            }
            else
            {
                try
                {
                    myGLSystem.docPath_EingangList = string.Empty;
                    if (INI.ReadString(DefaultSearch, strDocElementName) != null)
                    {
                        myGLSystem.docPath_EingangList = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
            if (!myGLSystem.docPath_EingangList.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "EingangLfs";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_EingangLfs = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_EingangLfs = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                try
                {
                    myGLSystem.docPath_EingangLfs = string.Empty;
                    if (INI.ReadString(DefaultSearch, strDocElementName) != null)
                    {
                        myGLSystem.docPath_EingangLfs = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
            if (!myGLSystem.docPath_EingangLfs.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "EingangDocMat";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_EingangDocMat = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_EingangDocMat = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                try
                {
                    myGLSystem.docPath_EingangDocMat = string.Empty;
                    myGLSystem.docPath_EingangDocMat = INI.ReadString(DefaultSearch, strDocElementName);
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
            if (!myGLSystem.docPath_EingangDocMat.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "EingangAnzeigeMail";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_EingangAnzeige = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_EingangAnzeige = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                try
                {
                    myGLSystem.docPath_EingangAnzeige = string.Empty;
                    myGLSystem.docPath_EingangAnzeige = INI.ReadString(DefaultSearch, strDocElementName);
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }

            strDocElementName = "EingangAnzeigePerDayMail";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_EingangAnzeigeMail = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_EingangAnzeigeMail = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                try
                {
                    myGLSystem.docPath_EingangAnzeigeMail = string.Empty;
                    myGLSystem.docPath_EingangAnzeigeMail = INI.ReadString(DefaultSearch, strDocElementName);
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }

            strDocElementName = "EingangLfsMail";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_EingangAnzeigePerDayMail = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_EingangAnzeigePerDayMail = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_EingangAnzeigePerDayMail = INI.ReadString(DefaultSearch, strDocElementName);
            }

            //********************************************************************************************************* Ausgangsdocs
            //
            strDocElementName = "AusgangDoc";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_AusgangDoc = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_AusgangDoc = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_AusgangDoc = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_AusgangDoc.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //********************************************************************************************************* Ausgangliste
            //
            strDocElementName = "Ausgangsliste";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_AusgangList = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_AusgangList = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_AusgangList = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_AusgangList.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }

            //
            strDocElementName = "AusgangLfs";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_AusgangLfs = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_AusgangLfs = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_AusgangLfs = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_AusgangLfs.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "AusgangAnzeige";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_AusgangAnzeige = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_AusgangAnzeige = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_AusgangAnzeige = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_EingangAnzeige.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "AusgangAnzeigePerDay";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_AusgangAnzeigePerDay = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_AusgangAnzeigePerDay = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_AusgangAnzeigePerDay = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_AusgangAnzeigePerDay.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "AusgangNeutralDoc";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_AusgangNeutralDoc = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_AusgangNeutralDoc = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_AusgangNeutralDoc = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_AusgangNeutralDoc.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "AusgangLfsMat";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_AusgangLfsMat = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_AusgangLfsMat = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_AusgangLfsMat = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_AusgangLfsMat.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "AusgangStornoDoc";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_AusgangStornoDoc = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_AusgangStornoDoc = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_AusgangStornoDoc = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_AusgangStornoDoc.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "AusgangLfsMail";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_AusgangLfsMail = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_AusgangLfsMail = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_AusgangLfsMail = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "AusgangAnzeigeMail";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_AusgangAnzeigeMail = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_AusgangAnzeigeMail = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_AusgangAnzeigeMail = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "AusgangAnzeigePerDayMail";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_EingangAnzeige = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_EingangAnzeige = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_EingangAnzeige = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "AusgangAnzeigePerDayMail";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_AusgangAnzeigePerDayMail = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_AusgangAnzeigePerDayMail = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_AusgangAnzeigePerDayMail = INI.ReadString(DefaultSearch, strDocElementName);
            }

            //*************************************************************************************************Rechnungs Docs
            //
            strDocElementName = "RGAnhang";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_RGAnhang = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_RGAnhang = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_RGAnhang = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "Lagerrechnung";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_Lagerrechnung = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_Lagerrechnung = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_Lagerrechnung = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_Lagerrechnung.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "Manuellerechnung";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_Manuellerechnung = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_Manuellerechnung = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_Manuellerechnung = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_Manuellerechnung.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "RGBuch";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_RGBuch = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_RGBuch = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_RGBuch = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "RGAnhangMat";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_RGAnhangMat = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_RGAnhangMat = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_RGAnhangMat = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "ManuelleGutschrift";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_ManuelleGutschrift = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_ManuelleGutschrift = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_ManuelleGutschrift = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_ManuelleGutschrift.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //
            strDocElementName = "LagerrechnungMail";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_LagerrechnungMail = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_LagerrechnungMail = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_LagerrechnungMail = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "ManuellerechnungMail";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_ManuellerechnungMail = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_ManuellerechnungMail = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_ManuellerechnungMail = INI.ReadString(DefaultSearch, strDocElementName);
            }

            //******************************************************************************************************** Label
            //
            strDocElementName = "LabelAll";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_LabelAll = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_LabelAll = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_LabelAll = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_LabelAll.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
            }
            //
            strDocElementName = "LabelOne";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_LabelOne = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_LabelOne = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_LabelOne = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_LabelOne.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
            }
            strDocElementName = "LOLabelLinks";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_LOLabelLinks = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_LOLabelLinks = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_LOLabelLinks = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_LOLabelLinks.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
            }
            strDocElementName = "LOLabelRechts";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_LOLabelRechts = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_LOLabelRechts = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_LOLabelRechts = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_LOLabelRechts.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
            }
            strDocElementName = "LOLabelBeide";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_LOLabelBeide = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_LOLabelBeide = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_LOLabelBeide = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_LOLabelBeide.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
            }
            //
            strDocElementName = "LabelOneNeutral";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_LabelOneNeutral = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_LabelOneNeutral = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_LabelOneNeutral = INI.ReadString(DefaultSearch, strDocElementName);
            }

            //******************************************************************************************* SPL Label
            strDocElementName = "SPLLabel";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_SPLLabel = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_SPLLabel = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_SPLLabel = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_SPLLabel.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //******************************************************************************************* SPL Doc
            strDocElementName = "SPLDoc";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_SPLDoc = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_SPLDoc = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_SPLDoc = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_SPLDoc.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //******************************************************************************************* SchadenLable
            strDocElementName = enumDokumentenArt.SchadenLabel.ToString();
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_SchadenLabel = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_SchadenLabel = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_SchadenLabel = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_SchadenLabel.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //******************************************************************************************* SchadenDoc
            strDocElementName = enumDokumentenArt.SchadenDoc.ToString();
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_SchadenDoc = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_SchadenDoc = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_SchadenDoc = INI.ReadString(DefaultSearch, strDocElementName);
            }
            if (!myGLSystem.docPath_SchadenDoc.Equals(string.Empty))
            {
                myGLSystem.docPath.Add(strDocElementName);
                if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                {
                    ListDocArtInUse.Add(strDocElementName);
                }
            }
            //*******************************************************************************************Divserse Docs
            //
            strDocElementName = "Bestand";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_Bestand = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_Bestand = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_Bestand = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "Journal";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_Journal = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_Journal = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_Journal = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "inventur";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_Inventur = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_Inventur = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_Inventur = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "Adressliste";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_Adressliste = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_Adressliste = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_Adressliste = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "ArtikelListe";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_ArtikelListe = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_ArtikelListe = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_ArtikelListe = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "KundenInformationen";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_KundenInformationen = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_KundenInformationen = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_KundenInformationen = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "TarifeKunden";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_TarifeKunden = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_TarifeKunden = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_TarifeKunden = INI.ReadString(DefaultSearch, strDocElementName);
            }

            strDocElementName = "ViewPrintBestand";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_ViewPrintBestand = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_ViewPrintBestand = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_ViewPrintBestand = INI.ReadString(DefaultSearch, strDocElementName);
            }


            strDocElementName = "ViewPrintJournal";
            if (myAdrID > 0)
            {
                strCustomPath = string.Empty;
                if (INI_Rep.SectionNames().Contains(strCustomPath))
                {
                    strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                    if (strCustomPath != string.Empty)
                    {
                        myGLSystem.docPath_ViewPrintJournal = strCustomPath;
                    }
                    else
                    {
                        myGLSystem.docPath_ViewPrintJournal = INI.ReadString(DefaultSearch, strDocElementName);
                    }
                }
            }
            else
            {
                myGLSystem.docPath_ViewPrintJournal = INI.ReadString(DefaultSearch, strDocElementName);
            }

            //*********************************************************************************************************

            if (this.Client.Modul.Print_OldVersion)
            {
                //Printcenter
                //Einlagerungen

                clsReportDocSetting tmpRep;
                this.Client.ListPrintCenterReportAuslagerung = new System.Collections.Generic.List<clsReportDocSetting>();
                this.Client.ListPrintCenterReportEinlagerung = new System.Collections.Generic.List<clsReportDocSetting>();
                string strDocPath = string.Empty;

                //
                //...|AllLabel
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.LabelAll.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Alle Label";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportEinlagerung.RemoveAt(this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                }
                //
                //...|LabelOne                 
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.LabelOne.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Artikellabel";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportEinlagerung.RemoveAt(this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                }
                //
                //...|LabelOneNeutral
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.LabelOneNeutral.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Neutrales Artikellabel";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportEinlagerung.RemoveAt(this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                }
                //
                //...| SPL Label
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.SPLLabel.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = enumIniDocKey.SPLLabel.ToString();
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportEinlagerung.RemoveAt(this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                }
                //******************************************************************************************* SPL Doc
                //
                //...| SPLDoc
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.SPLDoc.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = enumIniDocKey.SPLDoc.ToString();
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportEinlagerung.RemoveAt(this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                }
                //
                //...| SchadenLable
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.SchadenLabel.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = enumIniDocKey.SchadenLabel.ToString();
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportEinlagerung.RemoveAt(this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                }
                //
                //...|EingangDoc
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.EingangDoc.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Eingangsdokument";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportEinlagerung.RemoveAt(this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
                //...|EingangAnzeige
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.EingangAnzeige.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Eingangsanzeige";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportEinlagerung.RemoveAt(this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
                //...|EingangsLfs
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.EingangLfs.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Eingangslieferschein";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportEinlagerung.RemoveAt(this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
                //...|Eingangsliste
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.EingangAnzeige.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Eingangsanzeige";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportEinlagerung.RemoveAt(this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
                //...|EingangsLfs
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.Eingangsliste.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Eingangsliste";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportEinlagerung.RemoveAt(this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportEinlagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
                //***************************************************************************************Ausgangsdokumente
                //...|Ausgangsdoc
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.AusgangDoc.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Ausgangsdokument";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportAuslagerung.RemoveAt(this.Client.ListPrintCenterReportAuslagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
                //...|Ausgangsanzeige
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.AusgangAnzeige.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Ausgangsanzeige";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportAuslagerung.RemoveAt(this.Client.ListPrintCenterReportAuslagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
                //...|Ausgangsliste
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.Ausgangsliste.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Ausgangsliste";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportAuslagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportAuslagerung.RemoveAt(this.Client.ListPrintCenterReportAuslagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
                //...|AusgangLfs
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.AusgangLfs.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Ausgangslieferschein";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportAuslagerung.RemoveAt(this.Client.ListPrintCenterReportAuslagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
                //...|Frachtbrief KVO
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.KVOFrachtbrief.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Frachtbrief nat.";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportAuslagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportAuslagerung.RemoveAt(this.Client.ListPrintCenterReportAuslagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
                //...|Frachtbrief CMR
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.CMRFrachtbrief.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Frachtbrief int.";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportAuslagerung.RemoveAt(this.Client.ListPrintCenterReportAuslagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
                //Kunden Daten
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.KundenInformationen.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Kunde";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportAuslagerung.RemoveAt(this.Client.ListPrintCenterReportAuslagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
                //Kunden Tarife
                strDocPath = string.Empty;
                strDocElementName = enumIniDocKey.TarifeKunden.ToString();
                tmpRep = new clsReportDocSetting();
                tmpRep.ctrDocView = "Kundentarife";
                if (myAdrID > 0)
                {
                    strCustomPath = string.Empty;
                    if (INI_Rep.SectionNames().Contains(strCustomPath))
                    {
                        strCustomPath = INI_Rep.ReadString(strCustomSearch, strDocElementName);
                        if (strCustomPath != string.Empty)
                        {
                            strDocPath = strCustomPath;
                        }
                        else
                        {
                            strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                        }
                    }
                }
                else
                {
                    strDocPath = INI.ReadString(DefaultSearch, strDocElementName);
                }
                if (!strDocPath.Equals(string.Empty))
                {
                    if (this.Client.ListPrintCenterReportEinlagerung.IndexOf(tmpRep) > -1)
                    {
                        this.Client.ListPrintCenterReportAuslagerung.RemoveAt(this.Client.ListPrintCenterReportAuslagerung.IndexOf(tmpRep));
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    else
                    {
                        tmpRep.IniDocKeyName = strDocElementName;
                        tmpRep.IniDocKeyValuePath = strDocPath;
                        this.Client.ListPrintCenterReportAuslagerung.Add(tmpRep);
                    }
                    myGLSystem.docPath.Add(strDocElementName);
                    if (ListDocArtInUse.IndexOf(strDocElementName) < 0)
                    {
                        ListDocArtInUse.Add(strDocElementName);
                    }
                }
                //
            }
            else
            {
                ReportDocSetting = new clsReportDocSetting();
                ReportDocSetting.GLUser = this._GL_User;
            }
        }
        ///<summary>clsSystem / GetVersion </summary>
        ///<remarks>Ermittelt die aktuelle Datenbankversion von Sped4</remarks>
        ///<returns>retVal [decimal]</returns>
        public void GetVersion()
        {
            decimal decBenutzerID = -1;
            decimal retVal = 0.0M;
            string strSql = string.Empty;
            strSql = "SELECT Versionsnummer FROM Version WHERE ID=1";
            string strVersion = clsSQLcon.ExecuteSQL_GetValue(strSql, decBenutzerID);

            if (strVersion != string.Empty)
            {
                if (Decimal.TryParse(strVersion, out retVal))
                {
                    SystemVersionAppDecimal = retVal;
                    SystemVersionApp = Functions.FormatDecimalVersion(SystemVersionAppDecimal);
                }
                else
                {
                    decimal decUser = -1.00M;
                    Functions.AddLogbuch(decUser, "GetVersion", "Fehler beim Lesen der Versionsnummer!");
                }
            }
        }


        public void GetArchiveVersion()
        {
            string strReturn = string.Empty;
            decimal decBenutzerID = -1;
            decimal retVal = 0.0M;
            string strSql = string.Empty;
            strSql = "SELECT Versionsnummer FROM Version WHERE ID=1";
            string strVersion = clsSQLARCHIVE.ExecuteSQL_GetValue(strSql, decBenutzerID);

            if (strVersion != string.Empty)
            {
                if (Decimal.TryParse(strVersion, out retVal))
                {
                    SystemVersionAppDecimalArchive = retVal;
                    SystemVersionAppArchive = Functions.FormatDecimalVersion(SystemVersionAppDecimalArchive);

                }
                else
                {
                    decimal decUser = -1.00M;
                    Functions.AddLogbuch(decUser, "GetArchiveVersion", "Fehler beim Lesen der Versionsnummer!");
                }
            }
            else
            {
                //if (Decimal.TryParse(strVersion, out retVal))
                //{
                //    SystemVersionAppDecimalArchive = retVal;
                //    SystemVersionAppArchive = Functions.FormatDecimalVersion(SystemVersionAppDecimalArchive);

                //}
                SystemVersionAppDecimalArchive = retVal;
                SystemVersionAppArchive = Functions.FormatDecimalVersion(SystemVersionAppDecimalArchive);
            }
        }
        ///<summary>clsSystem / GetVersionCOM </summary>
        ///<remarks>Ermittelt die aktuelle Datenbankversion von Sped4</remarks>
        ///<returns>retVal [decimal]</returns>
        public void GetVersionCOM()
        {
            decimal decBenutzerID = -1;
            decimal retVal = 0.0M;
            string strSql = string.Empty;
            strSql = "SELECT Versionsnummer FROM Version WHERE ID=1";
            string strVersion = clsSQLCOM.ExecuteSQL_GetValue(strSql, decBenutzerID);

            if (strVersion != string.Empty)
            {
                Decimal.TryParse(strVersion, out retVal);
            }
            SystemVersionAppDecimalCOM = retVal;
            SystemVersionAppCOM = Functions.FormatDecimalVersion(SystemVersionAppDecimalCOM);
        }
        //public void GetDocPrinterByMandantSaved(ref Globals._GL_SYSTEM myGLSystem, decimal myMandant)
        public void GetDocPrinterByMandantSaved(ref Globals._GL_SYSTEM myGLSystem, int myUserId)
        {
            constValue_PrinterIni pIni = new constValue_PrinterIni(myUserId);

            if (File.Exists(pIni.IniFilePaht))
            {
                clsINI.clsINI ini = new clsINI.clsINI(pIni.IniFilePaht);

                //myGLSystem.docPath_RGAnhang_Printer = ini.ReadString("Druckereinstellungen" + myMandant.ToString(), "RGAnhang_Drucker");

                myGLSystem.docPath_RGAnhang_Printer = ini.ReadString("Druckereinstellungen", "RGAnhang_Drucker");
                myGLSystem.docPath_LabelAll_Printer = ini.ReadString("Druckereinstellungen", "LabelAll_Drucker");
                myGLSystem.docPath_LabelOne_Printer = ini.ReadString("Druckereinstellungen", "LabelOne_Drucker");

                myGLSystem.docPath_SPLLabel_Printer = ini.ReadString("Druckereinstellungen", "SPLLabel_Drucker");
                myGLSystem.docPath_SPLDoc_Printer = ini.ReadString("Druckereinstellungen", "SPLDoc_Drucker");

                myGLSystem.docPath_EingangAnzeige_Printer = ini.ReadString("Druckereinstellungen", "EingangAnzeige_Drucker");
                myGLSystem.docPath_EingangAnzeigePerDay_Printer = ini.ReadString("Druckereinstellungen", "EingangAnzeigePerDay_Drucker");
                myGLSystem.docPath_EingangDoc_Printer = ini.ReadString("Druckereinstellungen", "EingangDoc_Drucker");
                myGLSystem.docPath_EingangList_Printer = ini.ReadString("Druckereinstellungen", "Eingangsliste_Drucker");
                myGLSystem.docPath_EingangLfs_Printer = ini.ReadString("Druckereinstellungen", "EingangLfs_Drucker");

                myGLSystem.docPath_AusgangDoc_Printer = ini.ReadString("Druckereinstellungen", "AusgangDoc_Drucker");
                myGLSystem.docPath_AusgangList_Printer = ini.ReadString("Druckereinstellungen", "Ausgangsliste_Drucker");
                myGLSystem.docPath_AusgangLfs_Printer = ini.ReadString("Druckereinstellungen", "AusgangLfs_Drucker");
                myGLSystem.docPath_AusgangAnzeige_Printer = ini.ReadString("Druckereinstellungen", "AusgangAnzeige_Drucker");
                myGLSystem.docPath_AusgangAnzeigePerDay_Printer = ini.ReadString("Druckereinstellungen", "AusgangAnzeigePerDay_Drucker");
                myGLSystem.docPath_AusgangNeutralDoc_Printer = ini.ReadString("Druckereinstellungen", "AusgangNeutralDoc_Drucker");

                myGLSystem.docPath_Lagerrechnung_Printer = ini.ReadString("Druckereinstellungen", "Lagerrechnung_Drucker");
                myGLSystem.docPath_Manuellerechnung_Printer = ini.ReadString("Druckereinstellungen", "Manuellerechnung_Drucker");
                myGLSystem.docPath_RGBuch_Printer = ini.ReadString("Druckereinstellungen", "RGBuch_Drucker");
                myGLSystem.docPath_ManuelleGutschrift_Printer = ini.ReadString("Druckereinstellungen", "ManuelleGutschrift_Drucker");




                myGLSystem.docPath_RGAnhang_PaperSource = ini.ReadString("Druckereinstellungen", "RGAnhang_Fach");
                myGLSystem.docPath_LabelAll_PaperSource = ini.ReadString("Druckereinstellungen", "LabelAll_Fach");
                myGLSystem.docPath_LabelOne_PaperSource = ini.ReadString("Druckereinstellungen", "LabelOne_Fach");

                myGLSystem.docPath_SPLLabel_PaperSource = ini.ReadString("Druckereinstellungen", "SPLLabel_Fach");
                myGLSystem.docPath_SPLDoc_PaperSource = ini.ReadString("Druckereinstellungen", "SPLLabel_Fach");

                myGLSystem.docPath_EingangAnzeige_PaperSource = ini.ReadString("Druckereinstellungen", "EingangAnzeige_Fach");
                myGLSystem.docPath_EingangAnzeigePerDay_PaperSource = ini.ReadString("Druckereinstellungen", "EingangAnzeigePerDay_Fach");
                myGLSystem.docPath_EingangDoc_PaperSource = ini.ReadString("Druckereinstellungen", "EingangDoc_Fach");
                myGLSystem.docPath_EingangList_PaperSource = ini.ReadString("Druckereinstellungen", "EingangList_Fach");
                myGLSystem.docPath_EingangLfs_PaperSource = ini.ReadString("Druckereinstellungen", "EingangLfs_Fach");
                myGLSystem.docPath_AusgangDoc_PaperSource = ini.ReadString("Druckereinstellungen", "AusgangDoc_Fach");
                myGLSystem.docPath_AusgangList_PaperSource = ini.ReadString("Druckereinstellungen", "AusgangList_Fach");
                myGLSystem.docPath_AusgangLfs_PaperSource = ini.ReadString("Druckereinstellungen", "AusgangLfs_Fach");
                myGLSystem.docPath_AusgangAnzeige_PaperSource = ini.ReadString("Druckereinstellungen", "AusgangAnzeige_Fach");
                myGLSystem.docPath_AusgangAnzeigePerDay_PaperSource = ini.ReadString("Druckereinstellungen", "AusgangAnzeigePerDay_Fach");
                myGLSystem.docPath_AusgangNeutralDoc_PaperSource = ini.ReadString("Druckereinstellungen", "AusgangNeutralDoc_Fach");
                myGLSystem.docPath_Lagerrechnung_PaperSource = ini.ReadString("Druckereinstellungen", "Lagerrechnung_Fach");
                myGLSystem.docPath_Manuellerechnung_PaperSource = ini.ReadString("Druckereinstellungen", "Manuellerechnung_Fach");
                myGLSystem.docPath_RGBuch_PaperSource = ini.ReadString("Druckereinstellungen", "RGBuch_Fach");
                myGLSystem.docPath_ManuelleGutschrift_PaperSource = ini.ReadString("Druckereinstellungen", "ManuelleGutschrift_Fach");
            }
        }
        /// <summary>
        /// clsSystem / GetDocPaperSourceByMandant
        /// </summary>
        /// <param name="_GL_SYSTEM"></param>
        /// <param name="p"></param>
        public void GetDocPaperSourceByMandant(ref Globals._GL_SYSTEM myGLSystem, decimal myMandant)
        {
            myGLSystem.docPath_RGAnhang_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "RGAnhangPS");
            myGLSystem.docPath_LabelAll_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "LabelAllPS");
            myGLSystem.docPath_LabelOne_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "LabelOnePS");
            myGLSystem.docPath_SPLLabel_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "SPLLabelPS");
            myGLSystem.docPath_SPLDoc_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "SPLDocPS");
            myGLSystem.docPath_EingangAnzeige_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "EingangAnzeigePS");
            //myGLSystem.docPath_EingangList_PaperSource = Globals.INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "Eingangsliste");
            myGLSystem.docPath_EingangAnzeigePerDay_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "EingangAnzeigePerDayPS");
            myGLSystem.docPath_EingangDoc_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "EingangDocPS");
            myGLSystem.docPath_EingangLfs_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "EingangLfsPS");
            myGLSystem.docPath_AusgangDoc_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "AusgangDocPS");
            myGLSystem.docPath_AusgangLfs_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "AusgangLfsPS");
            myGLSystem.docPath_AusgangAnzeige_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "AusgangAnzeigePS");
            myGLSystem.docPath_AusgangAnzeigePerDay_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "AusgangAnzeigePerDayPS");
            myGLSystem.docPath_AusgangNeutralDoc_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "AusgangNeutralDocPS");
            myGLSystem.docPath_Lagerrechnung_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "LagerrechnungPS");
            myGLSystem.docPath_Manuellerechnung_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "ManuellerechnungPS");
            myGLSystem.docPath_RGBuch_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "RGBuchPS");
            myGLSystem.docPath_ManuelleGutschrift_PaperSource = INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "ManuelleGutschriftPS");
        }
        ///<summary>clsSystem / GetDocPathByMandant</summary>
        ///<remarks></remarks>
        public void GetDocPrintCount(ref Globals._GL_SYSTEM myGLSystem, decimal myMandant)
        {
            Int32 intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "RGAnhangPrintCount"), out intCount);
            myGLSystem.docPath_RGAnhang_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "LabelAllPrintCount"), out intCount);
            myGLSystem.docPath_LabelAll_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "LabelOnePrintCount"), out intCount);
            myGLSystem.docPath_LabelOne_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "SPLLabelPrintCount"), out intCount);
            myGLSystem.docPath_SPLLabel_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "SPLDocPrintCount"), out intCount);
            myGLSystem.docPath_SPLDoc_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "EingangAnzeigePrintCount"), out intCount);
            myGLSystem.docPath_EingangAnzeige_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "EingangAnzeigePerDayPrintCount"), out intCount);
            myGLSystem.docPath_EingangAnzeigePerDay_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "EingangDocPrintCount"), out intCount);
            myGLSystem.docPath_EingangDoc_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "EingangLfsPrintCount"), out intCount);
            myGLSystem.docPath_EingangLfs_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "EingangListPrintCount"), out intCount);
            myGLSystem.docPath_EingangList_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "AusgangDocPrintCount"), out intCount);
            myGLSystem.docPath_AusgangDoc_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "AusgangListPrintCount"), out intCount);
            myGLSystem.docPath_AusgangList_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "AusgangLfsPrintCount"), out intCount);
            myGLSystem.docPath_AusgangLfs_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "AusgangAnzeigePrintCount"), out intCount);
            myGLSystem.docPath_AusgangAnzeige_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "AusgangAnzeigePerDayPrintCount"), out intCount);
            myGLSystem.docPath_AusgangAnzeigePerDay_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "AusgangNeutralDocPrintCount"), out intCount);
            myGLSystem.docPath_AusgangNeutralDoc_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "LagerrechnungPrintCount"), out intCount);
            myGLSystem.docPath_Lagerrechnung_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "ManuellerechnungPrintCount"), out intCount);
            myGLSystem.docPath_Manuellerechnung_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "RGBuchPrintCount"), out intCount);
            myGLSystem.docPath_RGBuch_Count = intCount;
            intCount = 0;

            //myGLSystem.docPath_EingangDocMat_Count = Int32.TryParse(Globals.INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "EingangDocMat"));

            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "BestandPrintCount"), out intCount);
            myGLSystem.docPath_Bestand_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "JournalPrintCount"), out intCount);
            myGLSystem.docPath_Journal_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "inventurPrintCount"), out intCount);
            myGLSystem.docPath_Inventur_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "AdresslistePrintCount"), out intCount);
            myGLSystem.docPath_Adressliste_Count = intCount;
            intCount = 0;
            Int32.TryParse(INI.ReadString(myGLSystem.client_MatchCode + "Mandant_" + myMandant.ToString(), "ManuelleRechnungPrintCount"), out intCount);
            myGLSystem.docPath_ManuelleGutschrift_Count = intCount;
            intCount = 0;
        }

    }
}
