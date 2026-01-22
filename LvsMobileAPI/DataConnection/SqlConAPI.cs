using LVS;
using System.Data;
using System.Data.SqlClient;

namespace LvsMobileAPI.DataConnection
{
    public class SqlConAPI
    {
        public static System.Data.DataTable sqlConnection_DataTable(string mySqlStatement)
        {
            DataTable table = new DataTable();
            if (mySqlStatement != null)
            {
                try
                {
                    SvrSettings srv = new SvrSettings();

                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(srv.sqlConString);

                    cmd.Connection = sqlConnection;
                    adapter.SelectCommand = cmd;
                    cmd.CommandText = mySqlStatement;


                    sqlConnection.Open();
                    adapter.Fill(table);

                    cmd.Dispose();
                    sqlConnection.Close();

                    if ((cmd.Connection != null) && (cmd.Connection.State == ConnectionState.Open))
                    {
                        cmd.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }

            }
            return table;
        }

        public static bool sqlConnection_Update(string mySqlStatement)
        {
            bool result = false;
            if (mySqlStatement != null)
            {
                try
                {
                    SvrSettings srv = new SvrSettings();

                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(srv.sqlConString);

                    cmd.Connection = sqlConnection;
                    adapter.SelectCommand = cmd;
                    cmd.CommandText = mySqlStatement;
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    sqlConnection.Close();

                    if ((cmd.Connection != null) && (cmd.Connection.State == ConnectionState.Open))
                    {
                        cmd.Connection.Close();
                    }

                    result = true;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }
            }
            return result;
        }

        public static string sqlConnection_GetValue(string mySqlStatement)
        {
            string ReturnValue = string.Empty;
            if (mySqlStatement != null)
            {
                try
                {
                    SvrSettings srv = new SvrSettings();

                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(srv.sqlConString);

                    cmd.Connection = sqlConnection;
                    adapter.SelectCommand = cmd;
                    cmd.CommandText = mySqlStatement;
                    sqlConnection.Open();


                    object obj = cmd.ExecuteScalar();
                    if ((obj == null) | (obj is DBNull))
                    {
                        ReturnValue = string.Empty;
                    }
                    else
                    {
                        ReturnValue = obj.ToString();
                    }

                    cmd.Dispose();
                    sqlConnection.Close();

                    if ((cmd.Connection != null) && (cmd.Connection.State == ConnectionState.Open))
                    {
                        cmd.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }

            }
            return ReturnValue;
        }

        ///<summary>ctrFaktLager / tscbMandanten_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        public static string ExecuteSQLWithTRANSACTIONGetValue(string strSQL)
        {
            string strTActionName = "ExecuteTransActionProzess";
            decimal decBenutzer = 1;
            SvrSettings srv = new SvrSettings();

            //bool bTActionOK = true;
            string ReturnValue = string.Empty;

            SqlCommand CommandT = new SqlCommand();
            //clsSQLcon sql = new clsSQLcon();
            //sql.init();
            //sql.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(srv.sqlConString);

            //Start Transaction
            SqlTransaction tAction;
            tAction = sqlConnection.BeginTransaction(strTActionName);
            try
            {
                //tAction = sql.Connection.BeginTransaction(strTActionName);
                CommandT.Connection = sqlConnection;
                CommandT.Transaction = tAction;
                CommandT.CommandTimeout = 60;
                CommandT.CommandText = strSQL;
                sqlConnection.Open();
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
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                tAction.Rollback();
                //Add Logbucheintrag Exception
                string Beschreibung = "Exception-ExecuteSQLWithTRANSACTION: " + strSQL + "#" + ex;
                Functions.AddLogbuch(decBenutzer, enumLogbuchAktion.Exception.ToString(), Beschreibung);
                //bTActionOK = false;
            }
            tAction.Dispose();
            return ReturnValue;
        }
    }
}
