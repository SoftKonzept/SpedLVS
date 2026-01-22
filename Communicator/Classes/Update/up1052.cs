namespace Communicator.Classes
{
    public class up1052

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1052 = "1052";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EdiZQMQalityXml]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[EdiZQMQalityXml](" +
                        "[Id][int] IDENTITY(1, 1) NOT NULL," +
                        "[iDocNo] [nvarchar] (50) NULL," +
                        "[iDocDate][datetime] NULL," +
                        "[Path][nvarchar] (250) NULL," +
                        "[FileName][nvarchar] (100) NULL," +
                        "[WorkspaceId][int] NULL," +
                        "[ArticleId][int] NULL," +
                        "[IsActive][bit] NULL," +
                        "[iDocXml][xml] NULL," +
                        "[Created][datetime] NULL" +
                        ") ON[PRIMARY] TEXTIMAGE_ON[PRIMARY] " +

                        "ALTER TABLE [dbo].[EdiZQMQalityXml] ADD  CONSTRAINT [DF_EdiZQMQalityXml_Created]  DEFAULT (getdate()) FOR [Created]" +
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
