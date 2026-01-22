namespace Communicator.Classes
{
    public class up1033
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1033 = "1033";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Queue','AbBereichID') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Queue] ADD [AbBereichID] [int] DEFAULT((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Queue','IsCreateByASNMesTestCtr') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Queue] ADD [IsCreateByASNMesTestCtr] [bit] DEFAULT((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Queue','Description') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Queue] ADD [Description] [nvarchar] (MAX) NULL; " +
                  "END ";

            return sql;
        }
    }
}
