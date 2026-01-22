namespace Communicator.Classes
{
    public class up1051

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1051 = "1051";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomProcesses]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[CustomProcesses](" +
                        "[Id][int] IDENTITY(1,1) NOT NULL," +
                        "[AdrId] [int] NULL," +
                        "[ProcessName][nvarchar] (100) NULL," +
                        "[IsActive][bit] NULL," +
                        "[ProcessWorkspaces][nvarchar] (254) NULL," +
                        "[Created] [datetime] NULL," +
                        ") ON[PRIMARY] " +

                        "ALTER TABLE [dbo].[CustomProcesses] ADD  CONSTRAINT [DF_CustomProcesses_Created]  DEFAULT (getdate()) FOR [Created]" +
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
            //sql = "Update Jobs " +
            //                 "SET DelforVerweis = ''; ";

            return sql;
        }
    }
}
