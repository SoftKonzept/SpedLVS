//using System;
//using System.Linq;
//using System.Data;
//using System.Data.SqlClient;

//namespace LVS
//{
//    public class clsSQLCOM
//    {
//        private SqlConnection cSQLCon = new SqlConnection("");
//        private string _server;
//        private string _database;
//        private string _user;
//        private string _pw;
//        //private static string _server;
//        //private static string _database;
//        //private static string _user;
//        //private static string _pw;

//        public string Server
//        {
//            get
//            {
//                return _server;
//            }
//            set
//            {
//                _server = value;
//            }
//        }

//        public string Database
//        {
//            get
//            {
//                return _database;
//            }
//            set
//            {
//                _database = value;
//            }
//        }

//        public string User
//        {
//            get
//            {
//                return _user;
//            }
//            set
//            {
//                _user = value;
//            }
//        }

//        public string Password
//        {
//            get
//            {
//                return _pw;
//            }
//            set
//            {
//                _pw = value;
//            }
//        }

//        public SqlConnection Connection
//        {
//            get
//            {
//                return cSQLCon;
//            }
//            set
//            {
//                cSQLCon = value;
//            }
//        }

//        public void Open()
//        {
//            if (cSQLCon.State == ConnectionState.Closed)
//            {
//                    cSQLCon.Open();
//            }
//        }

//        public void Close()
//        {
//            cSQLCon.Close();
//        }

//        public bool init_con(ref Globals._GL_SYSTEM GLSystem, bool bCheckForComDB)
//        {
//            //string strClient = Globals.INI.ReadString("CLIENT", "Matchcode");
//            if (bCheckForComDB)
//            {
//                this.Server = GLSystem.con_Server_COM;
//                this.Database = GLSystem.con_Database_COM;
//                this.User = GLSystem.con_UserDB_COM;
//                this.Password = GLSystem.con_PassDB_COM;
//            }
//            else
//            {
//                this.Server = GLSystem.con_Server;
//                this.Database = GLSystem.con_Database;
//                this.User = GLSystem.con_UserDB;
//                this.Password = GLSystem.con_PassDB;
//            }

//            if (this.init() == false)
//            {
//                return false;
//            }
//            try
//            {
//                this.cSQLCon.Open();
//                this.cSQLCon.Close();
//            }
//            catch (Exception)
//            {
//                //Baustelle Eintrag als Error
//                // decimal decUser = -1.0M;
//                // Functions.AddLogbuch(decUser, "init_con", ex.ToString());
//                this.cSQLCon.Close();
//                return false;
//            }
//            return true;
//        }

//        public bool init()
//        {
//            bool bConnected = true;
//            try
//            {
//                cSQLCon.ConnectionString = ("Data Source = "
//                            + (Server + ("; " + ("Initial Catalog = "
//                            + (Database + ("; " + ("Persist Security Info = True; " + ("User ID = "
//                            + (User + ("; " + ("Password = " + Password)))))))))));
//                cSQLCon.Open();
//                cSQLCon.Close();
//            }
//            catch (Exception ex)
//            {
//                //clsError Error = new clsError();
//                //Error._GL_User = ;
//                //Error.Code = clsError.code1_101;
//                //Error.Aktion = "Initialisierung der DB Connection";
//                //Error.exceptText = ex.ToString();
//                //Error.WriteError();

//                //ex.ToString();
//                //clsMessages.Allgemein_ERRORTextShow(strError);
//                bConnected = false;
//            }
//            return bConnected;
//        }
//        //
//        //----------- Insert / ADD oder Update -----------
//        //
//        public static bool ExecuteSQL(string strSQL, decimal decBenutzer)
//        {
//            bool retVal = true;
//            //--- initialisierung des sqlcommand---
//            SqlCommand Command_1 = new SqlCommand();
//            Command_1.Connection = Globals.SQLconCom.Connection;
//            try
//            {
//                //----- SQL Abfrage -----------------------
//                Command_1.CommandText = strSQL;

//                Globals.SQLconCom.Open();
//                Command_1.ExecuteNonQuery();
//                Command_1.Dispose();
//                Globals.SQLconCom.Close();
//                if ((Command_1.Connection != null) && (Command_1.Connection.State == ConnectionState.Open))
//                {
//                    Command_1.Connection.Close();
//                }
//            }
//            catch (Exception ex)
//            {
//                retVal = false;
//               // System.Windows.Forms.MessageBox.Show(ex.ToString());
//                string Beschreibung = strSQL + "#" + ex;
//                Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);              
//            }
//            finally
//            {
//                //SQL Anweisung mit Fehlern - Eintrag in Logbuch zur Kontrolle
//                if (!retVal)
//                {
//                    string Beschreibung = strSQL;
//                    Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
//                }
//            }
//            return retVal;
//        }
//        //
//        //
//        public static bool ExecuteSQLWithTRANSACTION(string strSQL, string strTActionName, decimal decBenutzer)
//        {
//            bool bTActionOK = true;
//            SqlCommand CommandT = new SqlCommand();
//            Globals.SQLconCom.Open();

//            //Start Transaction
//            SqlTransaction tAction;
//            tAction = Globals.SQLconCom.Connection.BeginTransaction(strTActionName);

//            CommandT.Connection = Globals.SQLconCom.Connection;
//            CommandT.Transaction = tAction;
//            try
//            {
//                CommandT.CommandText = strSQL;
//                CommandT.ExecuteNonQuery();
//                tAction.Commit();
//                CommandT.Dispose();
//                Globals.SQLconCom.Close();
//            }
//            catch (Exception ex)
//            {
//                tAction.Rollback();
//                //Add Logbucheintrag Exception
//                string Beschreibung = "Exception-ExecuteSQLWithTRANSACTION: " + strSQL +"#"+ex;
//                Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
//                bTActionOK = false;
//            }
//            tAction.Dispose();
//            return bTActionOK;
//        }
//        ///<summary>ctrFaktLager / tscbMandanten_SelectedIndexChanged</summary>
//        ///<remarks></remarks>
//        public static string ExecuteSQLWithTRANSACTIONGetValue(string strSQL, string strTActionName, decimal decBenutzer)
//        {
//            bool bTActionOK = true;
//            string ReturnValue = string.Empty;
//            SqlCommand CommandT = new SqlCommand();
//            Globals.SQLconCom.Open();

//            //Start Transaction
//            SqlTransaction tAction;
//            tAction = Globals.SQLconCom.Connection.BeginTransaction(strTActionName);

//            CommandT.Connection = Globals.SQLcon.Connection;
//            CommandT.Transaction = tAction;
//            try
//            {
//                CommandT.CommandText = strSQL;
//                Globals.SQLconCom.Open();
//                object obj = CommandT.ExecuteScalar();
//                if ((obj == null) | (obj is DBNull))
//                {
//                    ReturnValue = string.Empty;
//                }
//                else
//                {
//                    ReturnValue = obj.ToString();
//                }
//                tAction.Commit();
//                CommandT.Dispose();
//                Globals.SQLconCom.Close();
//            }
//            catch (Exception ex)
//            {
//                tAction.Rollback();
//                //Add Logbucheintrag Exception
//                string Beschreibung = "Exception-ExecuteSQLWithTRANSACTION: " + strSQL + "#" + ex;
//                Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
//                bTActionOK = false;
//            }
//            tAction.Dispose();
//            return ReturnValue;
//        }
//        //
//        //
//        //
//        public static DataTable ExecuteSQL_GetDataTable(string strSQL, decimal decBenutzer, string strTableName)
//        {
//            DataTable dt = new DataTable(strTableName);
//            dt.Clear();
//            if (strSQL != string.Empty) 
//            {
//                bool retVal = true;

//                //--- initialisierung des sqlcommand---
//                SqlCommand CommandDT = new SqlCommand();
//                SqlDataAdapter adaDT = new SqlDataAdapter();
//                //CommandDT.Connection = Globals.SQLcon.Connection;
//                CommandDT.Connection = Globals.SQLconCom.Connection;
//                try
//                {
//                    //----- SQL Abfrage -----------------------
//                    adaDT.SelectCommand = CommandDT;
//                    CommandDT.CommandText = strSQL;
//                    Globals.SQLconCom.Open();
//                    adaDT.Fill(dt);
//                    CommandDT.Dispose();
//                    Globals.SQLconCom.Close();
//                    if ((CommandDT.Connection != null) && (CommandDT.Connection.State == ConnectionState.Open))
//                    {
//                        CommandDT.Connection.Close();
//                    }
//                }
//                catch (Exception ex)
//                {
//                    retVal = false;
//                    //System.Windows.Forms.MessageBox.Show(ex.ToString());
//                    string Beschreibung = strSQL + "#" + ex;
//                    Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
//                }
//                finally
//                {
//                    //SQL Anweisung mit Fehlern - Eintrag in Logbuch zur Kontrolle
//                    if (!retVal)
//                    {
//                        string Beschreibung = strSQL;
//                        Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
//                    }
//                }
//            }
//            return dt;
//        }
//        //
//        //
//        //
//        public static string ExecuteSQL_GetValue(string strSQL, decimal decBenutzer)
//        {
//            bool retVal = true;
//            string strSql = string.Empty;
//            string ReturnValue;
//            ReturnValue = string.Empty;
//            SqlDataAdapter adaV = new SqlDataAdapter();
//            SqlCommand CommandV = new SqlCommand();

//            CommandV.Connection = Globals.SQLconCom.Connection;
//            adaV.SelectCommand = CommandV;

//            try
//            {
//                CommandV.CommandText = strSQL;
//                Globals.SQLconCom.Open();
//                object obj = CommandV.ExecuteScalar();
//                if ((obj == null) | (obj is DBNull))
//                {
//                    ReturnValue = string.Empty;
//                }
//                else
//                {
//                    ReturnValue = obj.ToString();
//                }
//                CommandV.Dispose();
//                Globals.SQLconCom.Close();
//                if ((CommandV.Connection != null) && (CommandV.Connection.State == ConnectionState.Open))
//                {
//                    CommandV.Connection.Close();
//                }
//            }
//            catch (Exception ex)
//            {
//                retVal = false;
//               // System.Windows.Forms.MessageBox.Show(ex.ToString());
//                string Beschreibung = strSQL + "#" + ex;
//                Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
//            }
//            finally
//            {
//                //SQL Anweisung mit Fehlern - Eintrag in Logbuch zur Kontrolle
//                if (!retVal)
//                {
//                    string Beschreibung = strSQL;
//                    Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
//                } 

//            }
//           return ReturnValue;    
//        }
//        //
//        public static bool ExecuteSQL_GetValueBool(string strSQL, decimal decBenutzer)
//        {
//            bool retVal = true;
//            bool boExist = false;
//            string strSql = string.Empty;

//            SqlDataAdapter adaB = new SqlDataAdapter();
//            SqlCommand CommandB = new SqlCommand();

//            CommandB.Connection = Globals.SQLconCom.Connection;
//            adaB.SelectCommand = CommandB;
//            CommandB.CommandText = strSQL;
//            Globals.SQLconCom.Open();

//            try
//            {
//                if ((CommandB.ExecuteScalar() == null) | (CommandB.ExecuteScalar() is DBNull))
//                {
//                    boExist = false;
//                }
//                else
//                {
//                    boExist = true;
//                }
//                CommandB.Dispose();
//                Globals.SQLconCom.Close();
//                if ((CommandB.Connection != null) && (CommandB.Connection.State == ConnectionState.Open))
//                {
//                    CommandB.Connection.Close();
//                }
//            }
//            catch (Exception ex)
//            {
//                retVal = false;
//                //System.Windows.Forms.MessageBox.Show(ex.ToString());
//                string Beschreibung = strSQL +"#"+ex;
//                Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
//            }
//            finally
//            {
//                //SQL Anweisung mit Fehlern - Eintrag in Logbuch zur Kontrolle
//                if (!retVal)
//                {
//                    string Beschreibung = strSQL;
//                    Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
//                }

//            }
//            return boExist;
//        }
//    }        
//}
