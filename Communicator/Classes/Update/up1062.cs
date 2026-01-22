namespace Communicator.Classes
{
    public class up1062
    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1062 = "1062";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('CronJobs','AdrId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [CronJobs] ADD [AdrId]  [int] default(0);" +
                  "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string SqlStringUpdate_UpdateExistingColumns()
        {
            string sql = string.Empty;
            sql = "Update CronJobs ";
            sql += "SET AdrId = 0 ";

            return sql;
        }
    }
}
