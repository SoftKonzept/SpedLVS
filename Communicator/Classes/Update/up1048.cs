namespace Communicator.Classes
{
    public class up1048

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1048 = "1048";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql =

            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EdiClientWorkspaceValue]') AND type in (N'U')) " +
                    "BEGIN " +
                      "CREATE TABLE [dbo].[EdiClientWorkspaceValue](" +
                      "[Id][int] IDENTITY(1,1) NOT NULL, " +
                      "[AdrId] [int] NULL, " +
                      "[WorkspaceId][int] NULL, " +
                      "[AsnArtId][int] NULL, " +
                      "[Property][nvarchar] (100) NULL, " +
                      "[Value][nvarchar] (100) NULL, " +
                      "[Created][datetime] NULL, " +
                      "[Direction][nvarchar] (100) NULL, " +
                      "CONSTRAINT[PK_EdiClientWorkspaceValue] PRIMARY KEY CLUSTERED([Id] ASC" +
                      ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY] " +
                      ") ON[PRIMARY] " +
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
