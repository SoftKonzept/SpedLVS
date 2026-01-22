using LVS;
using System;
using System.Data.SqlClient;
using System.Threading;

namespace Communicator.Classes
{
    class clsUpdate
    {
        ///<summary>
        ///             In dieser Klasse werden die Updates für das Programm Sped4 verwaltet.Beim Programmstart überprüft Sped4 
        ///             die aktuelle Version mit der Datenbankversion. Sind die Versionen unterschiedlich werden die entsprechenden
        ///             Update durchgeführt
        ///</summary>
        ///<remarks>
        ///             Die Versionnummer von Sped4 setzt sich folgendermaßen zusammen:
        ///             Die Versionsnummer von Sped4 ist 4-stellig (Bsp: 1.234)
        ///                 1. Stelle       : besondere / grundlegenede Erweiterungen
        ///                 2. Stelle       : Datenbankänderungen 
        ///                 3. + 4. Stelle  : kleinere Änderungen im Programm
        ///               
        ///             Das Array "UpdateArray" beinhaltet alle Versionen. Diese Array wird beim Update-Vorgang durchlaufen und mit 
        ///             der Datenbankversion verglichen und entsprechend das Update durchzuführen.
        ///             
        ///             Built in Communicator und Sped4 sind gleich.
        ///</remarks>
        //************  User  ***************
        public clsUpdate(frmMainCom myFrmMain, decimal myBenutzerId)
        {
            this.frmMain = myFrmMain;
            this.BenutzerID = myBenutzerId;
        }

        private decimal _BenutzerID;

        public decimal BenutzerID
        {
            get
            {
                return _BenutzerID;
            }
            set
            {
                _BenutzerID = value;
            }
        }

        internal clsSystem system;
        internal clsLogbuchCon LogCom;
        internal frmMainCom frmMain;
        private decimal SoftwareVersion = 1001M;
        public int[] UpdateArray =
        {
            1000, 1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009,
            1010, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018, 1019,
            1020, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029,
            1030, 1031, 1032, 1033, 1034 ,1035, 1036, 1037, 1038, 1039,
            1040, 1041, 1042, 1043, 1044, 1045, 1046, 1047, 1048, //1049,
            1050, 1051, 1052, 1053, 1054, 1055, 1056, 1057, 1058, 1059,
            1060, 1061, 1062

        };

        /*********************************************************************************************************/
        public decimal dbVersion { get; set; }
        public bool UpdateOK { get; set; }

        ///<summary>DoUpdate(string tmpSQL) / clsUpdate</summary>
        ///<remarks>Führt die SQL - Update-Anweisung aus.</remarks>
        ///<param name="tmpSQL">SQL-Anweisung</param>
        ///<returns>Rückgabe von TRUE / FALSE nach Durchführung der SQL-Anweisung</returns>
        private bool DoUpdate(string tmpSQL)
        {
            bool updateOK = true;

            SqlCommand UpCommand = new SqlCommand();
            Globals.SQLconCom.Open();

            //Start Transaction
            SqlTransaction tAction;
            tAction = Globals.SQLconCom.Connection.BeginTransaction("Update");

            UpCommand.Connection = Globals.SQLconCom.Connection;
            UpCommand.Transaction = tAction;
            try
            {
                UpCommand.CommandText = tmpSQL;
                //Globals.SQLcon.Open();
                UpCommand.ExecuteNonQuery();

                tAction.Commit();

                UpCommand.Dispose();
                Globals.SQLconCom.Close();
            }
            catch (Exception ex)
            {
                tAction.Rollback();

                //Add Logbucheintrag Exception
                string beschreibung = "Exception: " + ex;
                LogCom = new clsLogbuchCon();          //Logbucheintrag Start Communicator
                LogCom.Typ = enumLogArtItem.Update.ToString();
                LogCom.LogText = beschreibung;
                LogCom.TableName = string.Empty;
                LogCom.TableID = 0;
                LogCom.Add(LogCom.GetAddLogbuchSQLString());
                updateOK = false;

                try
                {
                    Globals._GL_USER tmpGLUser = new Globals._GL_USER();

                    clsMail EMail = new clsMail();
                    EMail.InitClass(tmpGLUser, this.system);
                    EMail.Subject = this.system.strClient + DateTime.Now.ToShortDateString() + "- Error Communicator Update ";
                    string strTxt = string.Empty;

                    strTxt += "Client: " + this.system.strClient + Environment.NewLine;
                    strTxt += "sql: " + tmpSQL + Environment.NewLine;
                    strTxt += "Exception: " + Environment.NewLine + ex.ToString();
                    EMail.Message = strTxt;
                    EMail.SendError();
                }
                catch (Exception ex1)
                {
                    string strError = ex1.ToString();
                }
            }
            tAction.Dispose();
            return updateOK;
        }
        ///<summary>AddToLog(string strVersionsupdate) / clsUpdate</summary>
        ///<remarks>Die Update-Aktion wird im Logbuch dokumentiert.</remarks>
        ///<param name="strVersionsupdate">neue Versionsnummer nach dem Update</param>
        private void AddToLog(string strVersionsupdate)
        {
            string Beschreibung = "Software Update durchgeführt auf " + strVersionsupdate;
            LogCom = new clsLogbuchCon();          //Logbucheintrag Start Communicator
            LogCom.Typ = enumLogArtItem.Update.ToString();
            LogCom.LogText = Beschreibung;
            LogCom.TableName = string.Empty;
            LogCom.TableID = 0;
            LogCom.Add(LogCom.GetAddLogbuchSQLString());
        }
        ///<summaryGetSQLforUpdateVersion(string strVersion) / clsUpdate</summary>
        ///<remarks>Liefert die SQL-Anweisung zum Eintrag der neuen Versionsnummer in die Datenbank.</remarks>
        ///<param name="strVersion">neue Versionsnummer nach dem Update</param>
        private string GetSQLforUpdateVersion(string strVersion)
        {
            string sql = string.Empty;
            sql = "Update Version SET Versionsnummer='" + strVersion + "' " +
                  "WHERE ID='1'";
            return sql;
        }
        ///<summary>InitUpdate() / clsUpdate</summary>
        ///<remarks>Start der Updatefunktion.</remarks>
        public bool InitUpdate()
        {
            LogCom = new clsLogbuchCon();          //Logbucheintrag Start Communicator
            LogCom.Typ = enumLogArtItem.Update.ToString();
            LogCom.LogText = "Check Update...";
            LogCom.TableName = string.Empty;
            LogCom.TableID = 0;
            //LogCom.Add(LogCom.GetAddLogbuchSQLString());

            //BenutzerID = this.frmMain.GLUser.User_ID;
            UpdateOK = true;
            frmMain.SetInfoInInfoBox2(LogCom.LogText);

            //DB-Version und SoftwareVersion werden verglichen
            system = frmMain.systemLVS;
            system.BenutzerID = this.BenutzerID;
            SoftwareVersion = FunctionsCom.GetMaxArray(UpdateArray);
            string strDBVersion = system.SystemVersionAppCOM.ToString();

            if (
                    (system.SystemVersionAppDecimalCOM < SoftwareVersion) |
                    (system.SystemVersionAppDecimalCOM == 0)
                )
            {
                //Update Array wird durchlaufen und die DB-Version mit der 
                //entsprechenden UpdateArray-Version verglichen 
                for (Int32 i = 0; i <= UpdateArray.Length - 1; i++)
                {
                    bool boUpdateOK = false;
                    decimal decTmp = 0.0M;
                    decTmp = Convert.ToDecimal(UpdateArray[i].ToString());

                    //Vergleich
                    if (system.SystemVersionAppDecimalCOM < decTmp)
                    {
                        frmMain.SetInfoInInfoBox2("Installiere Update # " + Functions.FormatDecimalVersion(decTmp));
                        switch (decTmp.ToString())
                        {
                            //---- ab hier neue Updates einfügen -------------
                            case up1062.const_up1062:
                                boUpdateOK = DoUpdate(up1062.SqlString());
                                boUpdateOK = DoUpdate(up1062.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1061.const_up1061:
                                boUpdateOK = DoUpdate(up1061.SqlString());
                                //boUpdateOK = DoUpdate(up1061.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1060.const_up1060:
                                boUpdateOK = DoUpdate(up1060.SqlString());
                                boUpdateOK = DoUpdate(up1060.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1059.const_up1059:
                                boUpdateOK = DoUpdate(up1059.SqlString());
                                boUpdateOK = DoUpdate(up1059.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1058.const_up1058:
                                boUpdateOK = DoUpdate(up1058.SqlString());
                                boUpdateOK = DoUpdate(up1058.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1057.const_up1057:
                                boUpdateOK = DoUpdate(up1057.SqlString());
                                boUpdateOK = DoUpdate(up1057.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;

                            case up1056.const_up1056:
                                boUpdateOK = DoUpdate(up1056.SqlString());
                                boUpdateOK = DoUpdate(up1056.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1055.const_up1055:
                                boUpdateOK = DoUpdate(up1055.SqlString());
                                boUpdateOK = DoUpdate(up1055.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1054.const_up1054:
                                boUpdateOK = DoUpdate(up1054.SqlString());
                                boUpdateOK = DoUpdate(up1047.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1053.const_up1053:
                                boUpdateOK = DoUpdate(up1053.SqlString());
                                //boUpdateOK = DoUpdate(up1047.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1052.const_up1052:
                                boUpdateOK = DoUpdate(up1052.SqlString());
                                //boUpdateOK = DoUpdate(up1047.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1051.const_up1051:
                                boUpdateOK = DoUpdate(up1051.SqlString());
                                //boUpdateOK = DoUpdate(up1047.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1050.const_up1050:
                                boUpdateOK = DoUpdate(up1050.SqlString());
                                //boUpdateOK = DoUpdate(up1047.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            //case up1049.const_up1049:
                            //    boUpdateOK = DoUpdate(up1049.SqlString());
                            //    //boUpdateOK = DoUpdate(up1047.SqlStringUpdate_UpdateExistingColumns());
                            //    Thread.Sleep(200);
                            //    break;

                            case up1048.const_up1048:
                                boUpdateOK = DoUpdate(up1048.SqlString());
                                //boUpdateOK = DoUpdate(up1047.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1047.const_up1047:
                                boUpdateOK = DoUpdate(up1047.SqlString());
                                boUpdateOK = DoUpdate(up1047.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1046.const_up1046:
                                boUpdateOK = DoUpdate(up1046.SqlString());
                                //boUpdateOK = DoUpdate(up1045.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1045.const_up1045:
                                boUpdateOK = DoUpdate(up1045.SqlString());
                                boUpdateOK = DoUpdate(up1045.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1044.const_up1044:
                                boUpdateOK = DoUpdate(up1044.SqlString());
                                boUpdateOK = DoUpdate(up1044.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1043.const_up1043:
                                boUpdateOK = DoUpdate(up1043.SqlString());
                                boUpdateOK = DoUpdate(up1043.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1042.const_up1042:
                                boUpdateOK = DoUpdate(up1042.SqlString());
                                boUpdateOK = DoUpdate(up1042.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1041.const_up1041:
                                boUpdateOK = DoUpdate(up1041.SqlString());
                                boUpdateOK = DoUpdate(up1041.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1040.const_up1040:
                                boUpdateOK = DoUpdate(up1040.SqlString());
                                boUpdateOK = DoUpdate(up1040.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;

                            //------------------------------------------------------

                            case "1000":
                                boUpdateOK = DoUpdate(Update1000());
                                Thread.Sleep(500);
                                break;
                            case "1001":
                                boUpdateOK = DoUpdate(Update1001());
                                Thread.Sleep(500);
                                break;
                            case "1002":
                                boUpdateOK = DoUpdate(Update1002());
                                Thread.Sleep(500);
                                break;
                            case "1003":
                                boUpdateOK = DoUpdate(Update1003());
                                Thread.Sleep(500);
                                break;
                            case "1004":
                                boUpdateOK = DoUpdate(Update1004());
                                Thread.Sleep(500);
                                break;
                            case "1005":
                                boUpdateOK = DoUpdate(Update1005());
                                Thread.Sleep(500);
                                break;
                            case "1006":
                                boUpdateOK = DoUpdate(Update1006());
                                Thread.Sleep(500);
                                break;
                            case "1007":
                                boUpdateOK = DoUpdate(Update1007());
                                Thread.Sleep(500);
                                break;
                            case "1008":
                                boUpdateOK = DoUpdate(Update1008());
                                Thread.Sleep(500);
                                break;
                            case "1009":
                                boUpdateOK = DoUpdate(Update1009());
                                Thread.Sleep(500);
                                break;
                            case "1010":
                                boUpdateOK = DoUpdate(Update1010());
                                Thread.Sleep(500);
                                break;
                            case "1011":
                                boUpdateOK = DoUpdate(Update1011());
                                Thread.Sleep(500);
                                break;
                            case "1012":
                                boUpdateOK = DoUpdate(Update1012());
                                Thread.Sleep(500);
                                break;
                            case "1013":
                                boUpdateOK = DoUpdate(Update1013());
                                Thread.Sleep(500);
                                break;
                            case "1014":
                                boUpdateOK = DoUpdate(Update1014());
                                Thread.Sleep(500);
                                break;
                            case "1015":
                                boUpdateOK = DoUpdate(Update1015());
                                Thread.Sleep(500);
                                break;
                            case "1016":
                                boUpdateOK = DoUpdate(Update1016());
                                Thread.Sleep(500);
                                break;
                            case "1017":
                                boUpdateOK = DoUpdate(Update1017());
                                Thread.Sleep(500);
                                break;
                            case "1018":
                                boUpdateOK = DoUpdate(Update1018());
                                Thread.Sleep(500);
                                break;
                            case "1019":
                                boUpdateOK = DoUpdate(Update1019());
                                Thread.Sleep(500);
                                break;
                            case "1020":
                                boUpdateOK = DoUpdate(Update1020());
                                Thread.Sleep(500);
                                break;
                            case "1021":
                                boUpdateOK = DoUpdate(Update1021());
                                Thread.Sleep(500);
                                break;
                            case "1022":
                                boUpdateOK = DoUpdate(Update1022());
                                Thread.Sleep(500);
                                break;
                            case "1023":
                                boUpdateOK = DoUpdate(Update1023());
                                Thread.Sleep(500);
                                break;
                            case "1024":
                                boUpdateOK = DoUpdate(Update1024());
                                Thread.Sleep(500);
                                break;
                            case "1025":
                                boUpdateOK = DoUpdate(Update1025());
                                Thread.Sleep(500);
                                break;
                            case "1026":
                                boUpdateOK = DoUpdate(Update1026());
                                Thread.Sleep(500);
                                break;
                            case "1027":
                                boUpdateOK = DoUpdate(Update1027());
                                Thread.Sleep(500);
                                break;
                            case "1028":
                                boUpdateOK = DoUpdate(Update1028());
                                Thread.Sleep(500);
                                break;
                            case "1029":
                                boUpdateOK = DoUpdate(Update1029());
                                Thread.Sleep(200);
                                break;
                            case up1030.const_up1030:
                                boUpdateOK = DoUpdate(up1030.SqlString());
                                Thread.Sleep(200);
                                break;
                            case up1031.const_up1031:
                                boUpdateOK = DoUpdate(up1031.SqlString());
                                Thread.Sleep(200);
                                break;
                            case up1032.const_up1032:
                                boUpdateOK = DoUpdate(up1032.SqlString());
                                Thread.Sleep(200);
                                break;
                            case up1033.const_up1033:
                                boUpdateOK = DoUpdate(up1033.SqlString());
                                Thread.Sleep(200);
                                break;
                            case up1034.const_up1034:
                                boUpdateOK = DoUpdate(up1034.SqlString());
                                Thread.Sleep(200);
                                break;
                            case up1035.const_up1035:
                                boUpdateOK = DoUpdate(up1035.SqlString());
                                boUpdateOK = DoUpdate(up1035.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1036.const_up1036:
                                boUpdateOK = DoUpdate(up1036.SqlString());
                                boUpdateOK = DoUpdate(up1036.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1037.const_up1037:
                                boUpdateOK = DoUpdate(up1037.SqlString());
                                //boUpdateOK = DoUpdate(up1036.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1038.const_up1038:
                                boUpdateOK = DoUpdate(up1038.SqlString());
                                //boUpdateOK = DoUpdate(up1036.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;
                            case up1039.const_up1039:
                                boUpdateOK = DoUpdate(up1039.SqlString());
                                boUpdateOK = DoUpdate(up1039.SqlStringUpdate_UpdateExistingColumns());
                                Thread.Sleep(200);
                                break;

                        }

                        if (boUpdateOK)   //Update OK 
                        {
                            //UpdateFUnktion wird ausgeführt und das Update im Logbuch eingetragen
                            DoUpdate(GetSQLforUpdateVersion(decTmp.ToString()));
                            AddToLog(decTmp.ToString());
                            SetMessageUpdateOK(Functions.FormatDecimalVersion(decTmp));
                        }
                        else
                        {
                            SetMessageUpdateFailed(Functions.FormatDecimalVersion(decTmp));
                            i = UpdateArray.Length;
                            frmMain.SetInfoInInfoBox2("Beim Versuch das Update durchzuführen sind Probleme aufgetreten. Bitte setzen Sie sich mit comTEC Nöker GmbH in Verbindung.");
                        }
                        //neue Versionnummer aus DB auslesen
                        strDBVersion = system.SystemVersionApp.ToString();
                        //upMirr.VisibleStartUpdateButton(false);
                        UpdateOK = boUpdateOK;
                    }
                }
            }
            else
            {
                frmMain.SetInfoInInfoBox2("Es liegt kein Update vor...");
            }
            return UpdateOK;
        }
        ///<summary>SetMessageUpdateOK(string strVersion) / clsUpdate </summary>
        ///<remarks>Ausgabe der Info, dass das Update erfolgreich durchgeführt wurde.</remarks>
        private void SetMessageUpdateOK(string strVersion)
        {
            string strText = "Update auf  Version " + strVersion + " erfolgreich durchgeführt!";
            frmMain.SetInfoInInfoBox2(strText);
            LogCom = new clsLogbuchCon();          //Logbucheintrag Start Communicator
            LogCom.Typ = enumLogArtItem.Update.ToString();
            LogCom.LogText = strText;
            LogCom.TableName = string.Empty;
            LogCom.TableID = 0;
            LogCom.Add(LogCom.GetAddLogbuchSQLString());
        }
        ///<summary>SetMessageUpdateOK(string strVersion) / clsUpdate </summary>
        ///<remarks>Ausgabe der Info, dass es beim Update zu einem Fehler gekommen ist und das Update nicht erfolgreich durchgeführt werden konnte.</remarks>
        private void SetMessageUpdateFailed(string strVersion)
        {
            string strText = "Update auf  Version " + strVersion + " ist fehlgeschlagen. Bitte starten Sie den Communicator erneut!";
            frmMain.SetInfoInInfoBox2(strText);
            LogCom = new clsLogbuchCon();          //Logbucheintrag Start Communicator
            LogCom.Typ = enumLogArtItem.Update.ToString();
            LogCom.LogText = strText;
            LogCom.TableName = string.Empty;
            LogCom.TableID = 0;
            LogCom.Add(LogCom.GetAddLogbuchSQLString());
        }
        /*********************************************************************************************************************
         *                                    SQL - Update
         * ******************************************************************************************************************/
        ///<summary>clsUpdate / Update1000</summary>
        ///<remarks></remarks>
        private string Update1000()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Version]') AND type in (N'U')) " +
                 "BEGIN " +
                      "CREATE TABLE [dbo].[Version](" +
                      "[ID] [decimal](18, 0) IDENTITY(1,1) NOT NULL," +
                      "[Versionsnummer] [decimal](4, 0) NOT NULL CONSTRAINT [DF_Version_Versionsnummer]  DEFAULT ((0))," +
                      "CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED ([ID] ASC" +
                      ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] " +
                      ") ON [PRIMARY]; " +
                      "INSERT INTO Version (Versionsnummer) VALUES (100); " +
                  "END";
            return sql;
        }
        ///<summary>clsUpdate / Update1001</summary>
        ///<remarks>Table Job neues Feld VerweisVDA4905 </remarks>
        private string Update1001()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Jobs','VerweisVDA4905') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [Jobs] ADD [VerweisVDA4905] [nvarchar](3) NULL ;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1002</summary>
        ///<remarks>Table ASNArtSatzFeld neues Feld Kennung </remarks>
        private string Update1002()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNArtSatzFeld','Kennung') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [ASNArtSatzFeld] ADD [Kennung] [nvarchar](10) NULL ;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1003</summary>
        ///<remarks>Table ASNArtSatzFeld neues Feld FillValue</remarks>
        private string Update1003()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNArtSatzFeld','FillValue') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [ASNArtSatzFeld] ADD [FillValue] [nvarchar](10) NULL ;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1004</summary>
        ///<remarks>Table ASNArtSatzFeld neues Feld FillValue</remarks>
        private string Update1004()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNArtSatzFeld','FillLeft') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [ASNArtSatzFeld] ADD [FillLeft] [bit] DEFAULT (0) NOT NULL ;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1005</summary>
        ///<remarks>Table ASNArtSatzFeld neues Feld FillValue</remarks>
        private string Update1005()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('VDAClientOUT','aktiv') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [VDAClientOUT] ADD [aktiv] [bit] DEFAULT (0) NOT NULL;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1005</summary>
        ///<remarks></remarks>
        private string Update1006()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('VDAClientOUT','NextSatz') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [VDAClientOUT] ADD [NextSatz] [int] DEFAULT (0) NOT NULL;" +
                   "END " +
                    "IF COL_LENGTH('VDAClientOUT','ArtSatz') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [VDAClientOUT] ADD [ArtSatz] [bit] DEFAULT (0) NOT NULL;" +
                   "END " +
                   "IF COL_LENGTH('VDAClientOUT','FillValue') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [VDAClientOUT] ADD [FillValue]  [nvarchar] (10) NULL ;" +
                   "END " +
                   "IF COL_LENGTH('VDAClientOUT','FillLeft') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [VDAClientOUT] ADD  [FillLeft] [bit] DEFAULT (0) NOT NULL;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1005</summary>
        ///<remarks></remarks>
        private string Update1007()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Queue','ASNAction') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [Queue] ADD [ASNAction] [int] DEFAULT (0) NOT NULL;" +
                   "END " +
                    "IF COL_LENGTH('Jobs','UseCRLF') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [Jobs] ADD [UseCRLF] [bit] DEFAULT (0) NOT NULL;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1008</summary>
        ///<remarks>start.job Datei erzeugen</remarks>
        private string Update1008()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Jobs','CreateOdetteStart') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [Jobs] ADD [CreateOdetteStart] [bit] DEFAULT (0) NOT NULL;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1008</summary>
        ///<remarks>start.job Datei erzeugen</remarks>
        private string Update1009()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASNAction]') AND type in (N'U')) " +
                 "BEGIN " +
                    "CREATE TABLE [dbo].[ASNAction](" +
                        "[ID] [decimal](18, 0) IDENTITY(1,1) NOT NULL," +
                        "[ActionASN] [int] NULL," +
                        "[ActionName] [nvarchar](max) NULL," +
                        "[Auftraggeber] [decimal](18, 0) NOT NULL CONSTRAINT [DF_ASNAction_Auftraggeber]  DEFAULT ((0))," +
                        "[Empfaenger] [decimal](18, 0) NOT NULL CONSTRAINT [DF_ASNAction_Empfaenger]  DEFAULT ((0))," +
                        "[OrderID] [int] NOT NULL CONSTRAINT [DF_ASNAction_OrderID]  DEFAULT ((0))," +
                        "[MandantID] [decimal](18, 0) NULL CONSTRAINT [DF_ASNAction_MandantID]  DEFAULT ((0))," +
                        "[AbBereichID] [decimal](18, 0) NULL CONSTRAINT [DF_ASNAction_AbBereichID]  DEFAULT ((0))," +
                        "[ASNTypID] [decimal](18, 0) NULL," +
                        "[Bemerkung] [nvarchar](max) NULL," +
                     "CONSTRAINT [PK_ASNAction] PRIMARY KEY CLUSTERED ([ID] ASC " +
                     ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] " +
                    ") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] " +
                  "END";
            return sql;
        }
        ///<summary>clsUpdate / Update1010</summary>
        ///<remarks>start.job Datei erzeugen</remarks>
        private string Update1010()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNTyp','TypID') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [ASNTyp] ADD [TypID] [int] DEFAULT (0) NOT NULL;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1010</summary>
        ///<remarks>start.job Datei erzeugen</remarks>
        private string Update1011()
        {
            string sql = string.Empty;
            sql = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASNTyp]') AND type in (N'U')) " +
                 "BEGIN " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_LSL + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_LSL + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_EML + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_EML + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_EME + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_EME + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_AML + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_AML + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_AME + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_AME + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_AVE + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_AVE + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_AVL + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_AVL + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_STL + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_STL + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_STE + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_STE + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_AbE + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_AbE + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_TAA + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_TAA + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_AVA + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_AVA + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_AbA + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_AbA + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_LFE + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_LFE + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_RLL + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_RLL + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_RLE + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_RLE + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_TSL + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_TSL + "'; " +
                      "Update ASNTyp SET TypID=" + clsASNTyp.const_ASNTyp_TSE + " WHERE Typ='" + clsASNTyp.const_string_ASNTyp_TSE + "'; " +

                      "IF (SELECT ID FROM ASNTyp WHERE Typ='" + clsASNTyp.const_string_ASNTyp_BML + "') IS NULL " +
                      "BEGIN " +
                            "INSERT INTO ASNTyp (Typ , Beschreibung, TypID) VALUES ('" + clsASNTyp.const_string_ASNTyp_BML + "', 'BML -> Bestandsmeldung an Lieferant'," + clsASNTyp.const_ASNTyp_BML + ") ;" +
                      "END " +
                      "IF (SELECT ID FROM ASNTyp WHERE Typ='" + clsASNTyp.const_string_ASNTyp_BME + "') IS NULL " +
                      "BEGIN " +
                            "INSERT INTO ASNTyp (Typ , Beschreibung, TypID) VALUES ('" + clsASNTyp.const_string_ASNTyp_BME + "', 'BML -> Bestandsmeldung an Lieferant'," + clsASNTyp.const_ASNTyp_BME + ") ;" +
                      "END " +

                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1012</summary>
        ///<remarks>start.job Datei erzeugen</remarks>
        private string Update1012()
        {
            string sql = string.Empty;
            sql = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Abruf]') AND type in (N'U')) " +
                 "BEGIN " +
                      "DROP TABLE Abruf; " +
                 "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1012</summary>
        ///<remarks>start.job Datei erzeugen</remarks>
        private string Update1013()
        {
            string sql = string.Empty;
            sql = "Update ASNArtSatzFeld SET " +
                            "FillValue='0'" +
                            ", FillLeft=1 " +
                            "WHERE ID IN ( " +
                                            "Select c.ID FROM ASNArtSatzFeld c " +
                                                                "INNER JOIN ASNArtSatz b ON b.ID = c.ASNSatzID " +
                                                                "WHERE LTRIM(RTRIM(b.Kennung))='719'  AND LTRIM(RTRIM(c.Kennung))<>'SATZ719F12' " +
                                        "); " +
                   "Update ASNArtSatzFeld SET " +
                            "FillValue=''" +
                            ", FillLeft=0 " +
                            "WHERE ID IN ( " +
                                            "Select c.ID FROM ASNArtSatzFeld c " +
                                                                "INNER JOIN ASNArtSatz b ON b.ID = c.ASNSatzID " +
                                                                "WHERE LTRIM(RTRIM(b.Kennung))='719'  AND LTRIM(RTRIM(c.Kennung))='SATZ719F12' " +
                                        "); ";
            return sql;
        }
        ///<summary>clsUpdate / Update1012</summary>
        ///<remarks>start.job Datei erzeugen</remarks>
        private string Update1014()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNAction','activ') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [ASNAction] ADD [activ] [bit] DEFAULT (0) NOT NULL;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1012</summary>
        ///<remarks>start.job Datei erzeugen</remarks>
        private string Update1015()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNAction','activ')>0 " +
                   "BEGIN " +
                      "Update ASNAction SET activ=1; " +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1012</summary>
        ///<remarks>start.job Datei erzeugen</remarks>
        private string Update1016()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VDAClientWorkspaceValue]') AND type in (N'U')) " +
                 "BEGIN " +
                        "CREATE TABLE VDAClientWorkspaceValue(" +
                        "[ID] [decimal](28, 0) IDENTITY(1,1) NOT NULL," +
                        "[AdrID] [decimal](18, 0) NULL," +
                        "[Receiver] [decimal](18, 0) NULL," +
                        "[AbBereichID] [decimal](18, 0) NULL," +
                        "[ASNFieldID] [decimal](18, 0) NULL," +
                        "[Kennung] [nvarchar](50) NULL," +
                        "[Value] [nvarchar](100) NULL CONSTRAINT [DF_VDAClientConstValue_Value]  DEFAULT ('')," +
                        "[activ] [bit] NOT NULL CONSTRAINT [DF_VDAClientConstValue_activ]  DEFAULT ((1))," +
                        "[IsFunction] [bit] NOT NULL CONSTRAINT [DF_VDAClientConstValue_IsFunction]  DEFAULT ((0))," +
                        "CONSTRAINT [PK_VDAClientConstValue] PRIMARY KEY CLUSTERED ([ID] ASC" +
                        ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                        ") ON [PRIMARY]" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1017</summary>
        ///<remarks></remarks>
        private string Update1017()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Jobs','CheckTransferPath')IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [Jobs] ADD [CheckTransferPath] [nvarchar] (254) NULL;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1018</summary>
        ///<remarks></remarks>
        private string Update1018()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNAction','IsVirtFile')IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [ASNAction] ADD [IsVirtFile]  [bit] DEFAULT (0) NOT NULL;" +
                   "END " +
                   "IF COL_LENGTH('Queue','IsVirtFile')IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [Queue] ADD [IsVirtFile]  [bit] DEFAULT (0) NOT NULL;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1019</summary>
        ///<remarks></remarks>
        private string Update1019()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Jobs','ASNFileStorePath') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [Jobs] ADD [ASNFileStorePath] [nvarchar] (254) Default('') NOT NULL;" +
                   "END " +
                   "IF COL_LENGTH('Jobs','ErrorPath') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [Jobs] ADD [ErrorPath] [nvarchar] (254) Default('') NOT NULL;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1019</summary>
        ///<remarks></remarks>
        private string Update1020()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Jobs','ASNFileStorePath') IS NOT NULL " +
                   "BEGIN " +
                       " Update Jobs SET  ASNFileStorePath = Path+'\\Transfer' ; " +
                   "END " +
                    "IF COL_LENGTH('Jobs','ErrorPath') IS NOT NULL " +
                   "BEGIN " +
                       " Update Jobs SET  ErrorPath = Path+'\\ERROR' ; " +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1021</summary>
        ///<remarks></remarks>
        private string Update1021()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Jobs','CheckTransferPath') IS NOT NULL " +
                   "BEGIN " +
                       " Update Jobs SET  CheckTransferPath = Path+'\\CheckTransfer' ; " +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1022</summary>
        ///<remarks></remarks>
        private string Update1022()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Jobs','TransferFileName') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [Jobs] ADD [TransferFileName] [nvarchar] (100) Default('') NOT NULL;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1023</summary>
        ///<remarks></remarks>
        private string Update1023()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Jobs','ActionDate') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [Jobs] ADD [ActionDate] [datetime2];" +
                   "END " +
                   "IF COL_LENGTH('Jobs','Periode') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [Jobs] ADD [Periode] [nvarchar] (50) Default('') NOT NULL;" +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1024</summary>
        ///<remarks></remarks>
        private string Update1024()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EdiSegment]') AND type in (N'U')) " +
                 "BEGIN " +
                        "CREATE TABLE[dbo].[EdiSegment](" +
                        "[Id][int] IDENTITY(1, 1) NOT NULL," +
                        "[ASNArtId] [int] NULL," +
                        "[Name] [nvarchar] (50) NULL," +
                        "[Status] [nvarchar] (50) NULL," +
                        "[RepeatCount] [int] NULL," +
                        "[Ebene] [int] NULL," +
                        "[Description] [nvarchar] (254) NULL," +
                        "[Created] [datetime2] (7) NULL," +
                        "CONSTRAINT[PK_EdiSegment] PRIMARY KEY CLUSTERED([Id] ASC" +
                        ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                        ") ON[PRIMARY] " +

                        "ALTER TABLE[dbo].[EdiSegment] ADD CONSTRAINT[DF_EdiSegment_created]  DEFAULT(getdate()) FOR[Created] " +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1025</summary>
        ///<remarks></remarks>
        private string Update1025()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EdiSegmentElement]') AND type in (N'U')) " +
                 "BEGIN " +
                        "CREATE TABLE [dbo].[EdiSegmentElement](" +
                        "[Id][int] IDENTITY(1, 1) NOT NULL," +
                        "[EdiSegmentId] [int] NOT NULL," +
                        "[Name] [nvarchar] (50) NULL," +
                        "[Description] [nvarchar] (50) NULL," +
                        "[Position] [int] NULL," +
                        "[Created] [datetime2] (7) NULL CONSTRAINT[DF_EdiSegmentElement_Created]  DEFAULT(getdate())," +
                        "CONSTRAINT[PK_EdiSegmentElement] PRIMARY KEY CLUSTERED([Id] ASC" +
                        ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                        ") ON[PRIMARY] " +

                        "ALTER TABLE[dbo].[EdiSegmentElement] WITH CHECK ADD CONSTRAINT[FK_EdiSegmentElement_EdiSegment] FOREIGN KEY([EdiSegmentId]) " +
                        "REFERENCES[dbo].[EdiSegment] " +
                        "([Id]) " +
                        "ON DELETE CASCADE " +
                        "ALTER TABLE[dbo].[EdiSegmentElement] CHECK CONSTRAINT[FK_EdiSegmentElement_EdiSegment] " +
                   "END ";
            return sql;
        }
        ///<summary>clsUpdate / Update1026</summary>
        ///<remarks></remarks>
        private string Update1026()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EdiSegmentElementField]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[EdiSegmentElementField](" +
                        "[Id][int] IDENTITY(1, 1) NOT NULL," +
                        "[EdiSemgentElementId] [int] NULL," +
                        "[Shorcut] [nvarchar] (50) NULL, " +
                        "[Name] [nvarchar] (254) NULL," +
                        "[Status] [nvarchar] (50) NULL," +
                        "[Format] [nvarchar] (50) NULL," +
                        "[Description] [nvarchar] (254) NULL," +
                        "[constValue] [nvarchar] (50) NULL," +
                        "[Position] [int] NULL," +
                        "[Created] [datetime2] (7) NULL CONSTRAINT[DF_EdiSegementElementField_Created]  DEFAULT(getdate())," +
                        "CONSTRAINT[PK_EdiSegementElementField] PRIMARY KEY CLUSTERED([Id] ASC" +
                        ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                        ") ON[PRIMARY] " +

                        "ALTER TABLE[dbo].[EdiSegmentElementField] WITH CHECK ADD CONSTRAINT[FK_EdiSegmentElementField_EdiSegmentElement] FOREIGN KEY([EdiSemgentElementId]) " +
                        "REFERENCES[dbo].[EdiSegmentElement] " +
                        "([Id]) " +
                        "ON DELETE CASCADE " +
                        "ALTER TABLE[dbo].[EdiSegmentElementField] CHECK CONSTRAINT[FK_EdiSegmentElementField_EdiSegmentElement] " +
                   "END ";
            return sql;
        }
        /// <summary>
        ///                 clsUpdate / Update1026
        ///                 => Erweiterung für verschiedene ASNArten
        /// </summary>
        /// <returns></returns>
        private string Update1027()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('VDAClientOUT','ASNArtId') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [VDAClientOUT] ADD [ASNArtId] [decimal] null;" +
                   "END ";
            return sql;
        }
        /// <summary>
        ///                 clsUpdate / Update1028
        ///                 => Update der bestehenden VDAClientOut-Einträge auf VDA4913
        /// </summary>
        /// <returns></returns>
        private string Update1028()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('VDAClientOUT','ASNArtId') IS NOT NULL " +
                   "BEGIN " +
                       "Update VDAClientOUT SET ASNArtID = (SELECT ID FROM ASNArt WHERE Typ = 'VDA4913') " +
                                            " where " +
                                                    "ISNULL(ASNArtID,0)=0 ;" +
                   "END ";
            return sql;
        }
        /// <summary>
        ///                 clsUpdate / Update1029
        /// </summary>
        /// <returns></returns>
        private string Update1029()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('EdiSegmentElementField','FormatString') IS NULL " +
                   "BEGIN " +
                       "ALTER TABLE [EdiSegmentElementField] ADD [FormatString] [nvarchar] (30) Default('') NOT NULL;" +
                   "END ";
            return sql;
        }

    }
}
