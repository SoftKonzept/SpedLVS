namespace Communicator.Classes
{
    public class up1050

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1050 = "1050";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EdifactValue]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[EdifactValue](" +
                        "[Id][int] IDENTITY(1, 1) NOT NULL," +
                        "[AsnId] [int] NULL," +
                        "[EdiSegmentElement][nvarchar] (254) NULL," +
                        "[EdiSegmentElementValue][nvarchar] (254) NULL," +
                        "[Property][nvarchar] (100) NULL," +
                        "[Created][datetime] NULL" +
                        ") ON[PRIMARY]  " +

                        "ALTER TABLE [dbo].[EdifactValue] ADD  CONSTRAINT [DF_EdifactValue_Created]  DEFAULT (getdate()) FOR [Created]" +
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
