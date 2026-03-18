using System;
using System.Data;
using System.Data.SqlClient;

namespace LVS
{
    public class clsSQLARCHIVE
    {
        private SqlConnection cSQLCon = new SqlConnection("");

        private static string _server;
        private static string _database;
        private static string _user;
        private static string _pw;

        public static string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }

        public static string Database
        {
            get
            {
                return _database;
            }
            set
            {
                _database = value;
            }
        }

        public static string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        public static string Password
        {
            get
            {
                return _pw;
            }
            set
            {
                _pw = value;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return cSQLCon;
            }
            set
            {
                cSQLCon = value;
            }
        }

        public void Open()
        {
            if (cSQLCon.State == ConnectionState.Closed)
            {
                try
                {
                    cSQLCon.Open();
                }
                catch { }
            }
        }

        public void Close()
        {
            cSQLCon.Close();
        }

        public bool init()
        {
            bool bConnected = true;
            string strConnectionString = ("Data Source = "
                            + (Server + ("; " + ("Initial Catalog = "
                            + (Database + ("; " + ("Persist Security Info = True; " + ("User ID = "
                            + (User + ("; " + ("Password = " + Password)))))))))));
            try
            {
                //cSQLCon.ConnectionString = ("Data Source = "
                //            + (Server + ("; " + ("Initial Catalog = "
                //            + (Database + ("; " + ("Persist Security Info = True; " + ("User ID = "
                //            + (User + ("; " + ("Password = " + Password)))))))))));
                cSQLCon.ConnectionString = strConnectionString;

                cSQLCon.Open();
                cSQLCon.Close();
            }
            catch (Exception ex)
            {
                clsError Error = new clsError();
                Error.Code = clsError.code1_101;
                Error.Aktion = "Initialisierung der DB Connection";
                Error.exceptText += "Connectionstring: " + strConnectionString + Environment.NewLine + Environment.NewLine;
                Error.exceptText += ex.ToString() + Environment.NewLine;
                Error.WriteError();

                //ex.ToString();
                //clsMessages.Allgemein_ERRORTextShow(ex.ToString());

                bConnected = false;
            }
            return bConnected;
        }
        //
        //----------- Insert / ADD oder Update -----------
        //
        public static bool ExecuteSQL(string strSQL, decimal decBenutzer)
        {
            bool retVal = true;
            //--- initialisierung des sqlcommand---
            SqlCommand Command_1 = new SqlCommand();
            clsSQLARCHIVE sql = new clsSQLARCHIVE();
            sql.init();
            Command_1.Connection = sql.cSQLCon;//sql.cSQLCon;
            try
            {
                //----- SQL Abfrage -----------------------
                Command_1.CommandText = strSQL;

                sql.Open();
                Command_1.ExecuteNonQuery();
                Command_1.Dispose();
                sql.Close();
                if ((Command_1.Connection != null) && (Command_1.Connection.State == ConnectionState.Open))
                {
                    Command_1.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                // System.Windows.Forms.MessageBox.Show(ex.ToString());
                string Beschreibung = strSQL + "#" + ex;
                Functions.AddLogbuch(decBenutzer, enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
            finally
            {
                //SQL Anweisung mit Fehlern - Eintrag in Logbuch zur Kontrolle
                if (!retVal)
                {
                    string Beschreibung = strSQL;
                    Functions.AddLogbuch(decBenutzer, enumLogbuchAktion.Exception.ToString(), Beschreibung);
                }
            }
            return retVal;
        }
        //
        //
        public static bool ExecuteSQLWithTRANSACTION(string strSQL, string strTActionName, decimal decBenutzer)
        {
            bool bTActionOK = true;
            SqlCommand CommandT = new SqlCommand();
            clsSQLARCHIVE sql = new clsSQLARCHIVE();
            sql.init();
            sql.Open();
            //sql.Open();

            //Start Transaction
            SqlTransaction tAction;
            //tAction = sql.cSQLCon.BeginTransaction(strTActionName);
            tAction = sql.cSQLCon.BeginTransaction(strTActionName);
            CommandT.Connection = sql.cSQLCon;
            CommandT.Transaction = tAction;
            try
            {
                CommandT.CommandText = strSQL;
                CommandT.ExecuteNonQuery();
                tAction.Commit();
                CommandT.Dispose();
                sql.Close();
            }
            catch (Exception ex)
            {
                tAction.Rollback();
                //Add Logbucheintrag Exception
                string Beschreibung = "Exception-ExecuteSQLWithTRANSACTION: " + strSQL + "#" + ex;
                Functions.AddLogbuch(decBenutzer, enumLogbuchAktion.Exception.ToString(), Beschreibung);
                bTActionOK = false;
            }
            tAction.Dispose();
            return bTActionOK;
        }
        ///<summary>ctrFaktLager / tscbMandanten_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        public static string ExecuteSQLWithTRANSACTIONGetValue(string strSQL, string strTActionName, decimal decBenutzer)
        {
            bool bTActionOK = true;
            string ReturnValue = string.Empty;
            SqlCommand CommandT = new SqlCommand();
            clsSQLARCHIVE sql = new clsSQLARCHIVE();
            sql.init();
            sql.Open();

            //Start Transaction
            SqlTransaction tAction;
            tAction = sql.cSQLCon.BeginTransaction(strTActionName);

            CommandT.Connection = sql.cSQLCon;
            CommandT.Transaction = tAction;
            try
            {
                CommandT.CommandText = strSQL;
                sql.Open();
                object obj = CommandT.ExecuteScalar();
                if ((obj == null) | (obj is DBNull))
                {
                    ReturnValue = string.Empty;
                }
                else
                {
                    ReturnValue = obj.ToString();
                }
                tAction.Commit();
                CommandT.Dispose();
                sql.Close();
            }
            catch (Exception ex)
            {
                tAction.Rollback();
                //Add Logbucheintrag Exception
                string Beschreibung = "Exception-ExecuteSQLWithTRANSACTION: " + strSQL + "#" + ex;
                Functions.AddLogbuch(decBenutzer, enumLogbuchAktion.Exception.ToString(), Beschreibung);
                bTActionOK = false;
            }
            tAction.Dispose();
            return ReturnValue;
        }

        public static object ExecuteSQLWithTRANSACTIONGetObject(string strSQL, string strTActionName, decimal decBenutzer)
        {
            bool bTActionOK = true;
            object ReturnValue = null;
            SqlCommand CommandT = new SqlCommand();
            clsSQLARCHIVE sql = new clsSQLARCHIVE();
            sql.init();
            sql.Open();

            //Start Transaction
            SqlTransaction tAction;
            tAction = sql.Connection.BeginTransaction(strTActionName);

            CommandT.Connection = sql.Connection;
            CommandT.Transaction = tAction;
            try
            {
                CommandT.CommandTimeout = 60;
                CommandT.CommandText = strSQL;
                sql.Open();
                ReturnValue = CommandT.ExecuteScalar();
                tAction.Commit();
                CommandT.Dispose();
                sql.Close();
            }
            catch (Exception ex)
            {
                tAction.Rollback();
            }
            tAction.Dispose();
            return ReturnValue;
        }
        //
        //
        //
        public static DataTable ExecuteSQL_GetDataTable(string strSQL, decimal decBenutzer, string strTableName)
        {
            DataTable dt = new DataTable(strTableName);
            dt.Clear();
            if (strSQL != string.Empty)
            {
                bool retVal = true;

                //--- initialisierung des sqlcommand---
                SqlCommand CommandDT = new SqlCommand();
                SqlDataAdapter adaDT = new SqlDataAdapter();
                clsSQLARCHIVE sql = new clsSQLARCHIVE();
                sql.init();

                CommandDT.Connection = sql.cSQLCon;
                try
                {
                    //----- SQL Abfrage -----------------------
                    adaDT.SelectCommand = CommandDT;
                    CommandDT.CommandText = strSQL;

                    sql.Open();
                    adaDT.Fill(dt);
                    CommandDT.Dispose();
                    sql.Close();
                    if ((CommandDT.Connection != null) && (CommandDT.Connection.State == ConnectionState.Open))
                    {
                        CommandDT.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    retVal = false;
                    //System.Windows.Forms.MessageBox.Show(ex.ToString());
                    string Beschreibung = strSQL + "#" + ex;
                    Functions.AddLogbuch(decBenutzer, enumLogbuchAktion.Exception.ToString(), Beschreibung);
                }
                finally
                {
                    //SQL Anweisung mit Fehlern - Eintrag in Logbuch zur Kontrolle
                    if (!retVal)
                    {
                        string Beschreibung = strSQL;
                        Functions.AddLogbuch(decBenutzer, enumLogbuchAktion.Exception.ToString(), Beschreibung);
                    }
                }
            }
            return dt;
        }
        //
        //
        //
        public static string ExecuteSQL_GetValue(string strSQL, decimal mdecBenutzer)
        {
            bool retVal = true;
            string strSql = string.Empty;
            string ReturnValue;
            ReturnValue = string.Empty;
            SqlDataAdapter adaV = new SqlDataAdapter();
            SqlCommand CommandV = new SqlCommand();
            clsSQLARCHIVE sql = new clsSQLARCHIVE();
            sql.init();
            CommandV.Connection = sql.cSQLCon;
            adaV.SelectCommand = CommandV;

            try
            {
                CommandV.CommandText = strSQL;
                sql.Open();
                object obj = CommandV.ExecuteScalar();
                if ((obj == null) | (obj is DBNull))
                {
                    ReturnValue = string.Empty;
                }
                else
                {
                    ReturnValue = obj.ToString();
                }
                CommandV.Dispose();
                sql.Close();
                if ((CommandV.Connection != null) && (CommandV.Connection.State == ConnectionState.Open))
                {
                    CommandV.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                // System.Windows.Forms.MessageBox.Show(ex.ToString());
                string Beschreibung = strSQL + "#" + ex;
                clsError error = new clsError();
                //error._GL_User = this.;
                error.Code = clsError.code1_101;
                error.Aktion = "DB Verbindung in ExecuteSQL_GetValue";
                error.exceptText = ex.ToString();
                error.WriteError();
            }
            finally
            {
                //SQL Anweisung mit Fehlern - Eintrag in Logbuch zur Kontrolle
                if (!retVal)
                {
                    //string Beschreibung = strSQL;
                    //Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
                }

            }
            return ReturnValue;
        }
        //
        public static bool ExecuteSQL_GetValueBool(string strSQL, decimal decBenutzer)
        {
            bool retVal = true;
            bool boExist = false;
            string strSql = string.Empty;

            SqlDataAdapter adaB = new SqlDataAdapter();
            SqlCommand CommandB = new SqlCommand();
            clsSQLARCHIVE sql = new clsSQLARCHIVE();
            sql.init();
            CommandB.Connection = sql.cSQLCon;
            adaB.SelectCommand = CommandB;
            CommandB.CommandText = strSQL;
            sql.Open();

            try
            {
                if ((CommandB.ExecuteScalar() == null) | (CommandB.ExecuteScalar() is DBNull))
                {
                    boExist = false;
                }
                else
                {
                    boExist = true;
                }
                CommandB.Dispose();
                sql.Close();
                if ((CommandB.Connection != null) && (CommandB.Connection.State == ConnectionState.Open))
                {
                    CommandB.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                string Beschreibung = strSQL + "#" + ex;
                Functions.AddLogbuch(decBenutzer, enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
            finally
            {
                //SQL Anweisung mit Fehlern - Eintrag in Logbuch zur Kontrolle
                if (!retVal)
                {
                    string Beschreibung = strSQL;
                    Functions.AddLogbuch(decBenutzer, enumLogbuchAktion.Exception.ToString(), Beschreibung);
                }

            }
            return boExist;
        }

        public static DataTable ExecuteSQLWithTRANSACTIONGetDataTable(string strSQL, string strTActionName, string strTableName, decimal decBenutzer)
        {
            DataTable dt = new DataTable(strTableName);
            bool bTActionOK = true;
            string ReturnValue = string.Empty;
            SqlCommand CommandT = new SqlCommand();
            SqlDataAdapter adaDT = new SqlDataAdapter();
            clsSQLARCHIVE sql = new clsSQLARCHIVE();
            //sql.init();
            //sql.Open();

            if (sql.init())
            {
                sql.Open();
                //Start Transaction
                SqlTransaction tAction;
                tAction = sql.Connection.BeginTransaction(strTActionName);

                CommandT.Connection = sql.Connection;
                CommandT.Transaction = tAction;
                try
                {
                    adaDT.SelectCommand = CommandT;
                    CommandT.CommandText = strSQL;
                    sql.Open();
                    tAction.Commit();
                    adaDT.Fill(dt);
                    CommandT.Dispose();
                    sql.Close();
                }
                catch (Exception ex)
                {
                    tAction.Rollback();
                    //Add Logbucheintrag Exception
                    string Beschreibung = "Exception-ExecuteSQLWithTRANSACTION: " + strSQL + "#" + ex;
                    Functions.AddLogbuch(decBenutzer, enumLogbuchAktion.Exception.ToString(), Beschreibung);
                    bTActionOK = false;
                }
                tAction.Dispose();
            }
            return dt;
        }
    }
}
