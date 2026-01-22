using LVS;
using LvsMobileAPI.SqlStatements;
using System.Data;
using System.Data.SqlClient;


namespace LvsMobileAPI.DataConnection
{
    public class TestConnection
    {
        internal SvrSettings srv;
        public List<string> CheckResultList = new List<string>();
        public string Message = string.Empty;
        internal int iDruchlauf { get; set; } = 0;
        public TestConnection()
        {


            for (int i = 0; i <= 2; i++)
            {
                iDruchlauf = i + 1;
                CheckResultList.Add(iDruchlauf + ". Check:" + Environment.NewLine);
                srv = new SvrSettings();
                switch (i)
                {
                    case 0:
                        srv.AppType = Common.Enumerations.enumAppType.Sped4;
                        break;
                    case 1:
                        srv.AppType = Common.Enumerations.enumAppType.LvsMobileAPI;
                        break;
                    case 2:
                        srv.AppType = Common.Enumerations.enumAppType.LvsPrintService;
                        break;

                }
                CheckSqlConnectionString();
                CheckDBConnection();
                GetTop10Tables();
            }

            Message = string.Empty;
            foreach (string s in CheckResultList)
            {
                Message += s + Environment.NewLine;
            }
        }

        /// <summary>
        ///             1. Check: Connectionstring
        /// </summary>
        private void CheckSqlConnectionString()
        {

            string strMes = $" " + iDruchlauf + ".a " + srv.AppType.ToString() + ": sqlConnectionString: " + srv.sqlConString;
            CheckResultList.Add(strMes);
        }
        /// <summary>
        ///             2. Check DB Connection
        /// </summary>
        private void CheckDBConnection()
        {
            string msgError = string.Empty;
            string strMes = string.Empty;
            DataTable table = new DataTable();
            try
            {
                //SvrSettings srv = new SvrSettings();

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter();
                System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(srv.sqlConString);

                cmd.Connection = sqlConnection;
                adapter.SelectCommand = cmd;
                cmd.CommandText = sql_TestConnection.sql_Top10Databases();


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
                strMes = ex.Message;
                CheckResultList.Add(strMes);
            }
            finally
            {
                if (table.Rows.Count > 0)
                {
                    strMes = $" " + iDruchlauf + ".b CheckDBConnection: erfolgreich!";
                }
                else
                {
                    strMes = $" " + iDruchlauf + ".b CheckDBConnection: fehlgeschlagen!";
                }

                if (!msgError.Equals(string.Empty))
                {
                    strMes += Environment.NewLine;
                    strMes += "Exception:" + Environment.NewLine;
                    strMes += msgError;
                }
            }
            CheckResultList.Add(strMes);
        }

        /// <summary>
        ///           3. Check: Top 10 Tables der Database  
        /// </summary>
        private void GetTop10Tables()
        {
            try
            {
                string strTableNames = string.Empty;
                DataTable dt = new DataTable();
                //dt = SqlConnection.SqlCon.sqlConnection_DataTable(sql_TestConnection.sql_Top10Databases());
                dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(sql_TestConnection.sql_Top10Databases(), "InventoryList", "InventoryList", 0);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        strTableNames += " - " + r["name"].ToString() + Environment.NewLine;
                    }
                }

                string strMes = $" " + iDruchlauf + ".c Connection Test Top 10 Table" + Environment.NewLine;
                strMes += strTableNames;
                CheckResultList.Add(strMes);
            }
            catch (Exception ex)
            {
                string strMes = ex.Message;
                CheckResultList.Add(strMes);
            }
        }
    }
}
