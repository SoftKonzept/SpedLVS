using System;
using System.Data;
using System.Data.SqlClient;

public class clsSQLcon_LVS
{
    internal SqlConnection cSQLCon = new SqlConnection("");

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
    ///<summary>clsSQLcon_LVS / Open</summary>
    ///<remarks></remarks>
    public void Open()
    {
        if (cSQLCon.State == ConnectionState.Closed)
        {
            cSQLCon.Open();

        }
    }
    ///<summary>clsSQLcon_LVS / Close</summary>
    ///<remarks></remarks>
    public void Close()
    {
        cSQLCon.Close();
    }
    ///<summary>clsSQLcon_LVS / InitClass</summary>
    ///<remarks></remarks>
    public bool init()
    {
        bool bConnected = true;
        try
        {
            //cSQLCon.ConnectionString = ConfigurationSettings.AppSettings["DatabaseLVS"];
            cSQLCon.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["DatabaseLVS"];
            cSQLCon.Open();
            cSQLCon.Close();
        }
        catch (Exception ex)
        {
            //clsError Error = new clsError();
            //Error._GL_User = ;
            //Error.Code = clsError.code1_101;
            //Error.Aktion = "Initialisierung der DB Connection";
            //Error.exceptText = ex.ToString();
            //Error.WriteError();

            //ex.ToString();
            //clsMessages.Allgemein_ERRORTextShow(strError);
            bConnected = false;
        }
        return bConnected;
    }
    ///<summary>clsSQLcon_LVS / ExecuteSQL</summary>
    ///<remarks></remarks>
    public bool ExecuteSQL(string strSQL)
    {
        bool retVal = true;
        //--- initialisierung des sqlcommand---
        SqlCommand Command_1 = new SqlCommand();
        clsSQLcon_LVS sql = new clsSQLcon_LVS();
        sql.init();
        Command_1.Connection = sql.Connection;
        //sql.cSQLCon;//sql.cSQLCon;
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
            //Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
        }
        finally
        {
            //SQL Anweisung mit Fehlern - Eintrag in Logbuch zur Kontrolle
            if (!retVal)
            {
                string Beschreibung = strSQL;
                //Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
        }
        return retVal;
    }
    ///<summary>clsSQLcon_LVS / ExecuteSQLWithTRANSACTION</summary>
    ///<remarks></remarks>
    public bool ExecuteSQLWithTRANSACTION(string strSQL, string strTActionName)
    {
        bool bTActionOK = true;
        SqlCommand CommandT = new SqlCommand();
        clsSQLcon_LVS sql = new clsSQLcon_LVS();
        sql.init();
        sql.Open();
        //sql.Open();

        //Start Transaction
        SqlTransaction tAction;
        //tAction = sql.cSQLCon.BeginTransaction(strTActionName);
        tAction = sql.Connection.BeginTransaction(strTActionName);
        CommandT.Connection = sql.Connection;
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
            //Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
            bTActionOK = false;
        }
        tAction.Dispose();
        return bTActionOK;
    }
    ///<summary>clsSQLcon_LVS / tscbMandanten_SelectedIndexChanged</summary>
    ///<remarks></remarks>
    public string ExecuteSQLWithTRANSACTIONGetValue(string strSQL, string strTActionName)
    {
        string ReturnValue = string.Empty;
        SqlCommand CommandT = new SqlCommand();
        clsSQLcon_LVS sql = new clsSQLcon_LVS();
        sql.init();
        sql.Open();

        //Start Transaction
        SqlTransaction tAction;
        tAction = sql.Connection.BeginTransaction(strTActionName);

        CommandT.Connection = sql.Connection;
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
            //Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);

        }
        tAction.Dispose();
        return ReturnValue;
    }
    ///<summary>clsSQLcon_LVS / tscbMandanten_SelectedIndexChanged</summary>
    ///<remarks></remarks>
    public DataTable ExecuteSQL_GetDataTable(string strSQL, string strTableName)
    {
        DataTable dt = new DataTable(strTableName);
        dt.Clear();
        if (strSQL != string.Empty)
        {
            bool retVal = true;
            //--- initialisierung des sqlcommand---
            SqlCommand CommandDT = new SqlCommand();
            SqlDataAdapter adaDT = new SqlDataAdapter();
            clsSQLcon_LVS sql = new clsSQLcon_LVS();
            sql.init();

            CommandDT.Connection = sql.Connection;
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
                //Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
            finally
            {
                //SQL Anweisung mit Fehlern - Eintrag in Logbuch zur Kontrolle
                if (!retVal)
                {
                    string Beschreibung = strSQL;
                    //Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
                }
            }
        }
        return dt;
    }
    ///<summary>clsSQLcon_LVS / tscbMandanten_SelectedIndexChanged</summary>
    ///<remarks></remarks>
    public string ExecuteSQL_GetValue(string strSQL)
    {
        bool retVal = true;
        string strSql = string.Empty;
        string ReturnValue;
        ReturnValue = string.Empty;
        SqlDataAdapter adaV = new SqlDataAdapter();
        SqlCommand CommandV = new SqlCommand();
        clsSQLcon_LVS sql = new clsSQLcon_LVS();
        sql.init();
        CommandV.Connection = sql.Connection;
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
            //string Beschreibung = strSQL + "#" + ex;
            //clsError error = new clsError();
            ////error._GL_User = this.;
            //error.Code = clsError.code1_101;
            //error.Aktion = "DB Verbindung in ExecuteSQL_GetValue";
            //error.exceptText = ex.ToString();
            //error.SQLString = strSQL;
            //error.WriteError();
            //clsMail mail = new clsMail();

        }
        finally
        {
            //SQL Anweisung mit Fehlern - Eintrag in Logbuch zur Kontrolle
            if (!retVal)
            {
                string Beschreibung = strSQL;
                //Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }

        }
        return ReturnValue;
    }
    ///<summary>clsSQLcon_LVS / tscbMandanten_SelectedIndexChanged</summary>
    ///<remarks></remarks>
    public bool ExecuteSQL_GetValueBool(string strSQL)
    {
        bool retVal = true;
        bool boExist = false;
        string strSql = string.Empty;

        SqlDataAdapter adaB = new SqlDataAdapter();
        SqlCommand CommandB = new SqlCommand();
        clsSQLcon_LVS sql = new clsSQLcon_LVS();
        sql.init();
        CommandB.Connection = sql.Connection;
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
            //Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
        }
        finally
        {
            //SQL Anweisung mit Fehlern - Eintrag in Logbuch zur Kontrolle
            if (!retVal)
            {
                string Beschreibung = strSQL;
                //Functions.AddLogbuch(decBenutzer, Globals.enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }

        }
        return boExist;
    }

}
