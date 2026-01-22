namespace LvsMobileAPI.SqlStatements
{
    public class sql_TestConnection
    {
        public static string sql_Top10Databases()
        {
            string sql = string.Empty;
            sql = @"SELECT Top (10) name FROM master.dbo.sysdatabases";
            return sql;
        }
    }
}
