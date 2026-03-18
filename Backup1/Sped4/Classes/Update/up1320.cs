namespace Sped4.Classes.Update
{
    public class up1320
    {
        /// <summary>
        ///             NEU Table StoreInScanner
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1320 = "1320";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoreInScanner]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[StoreInScanner](" +
                            "[Id][int] IDENTITY(1,1)," +
                            "[ProcessId] [bigint] NOT NULL, " +
                            "[Datafield] [nvarchar](100) NULL," +
                            "[FieldValue] [nvarchar] (100) NULL," +
                            "[UserId] [int] NULL," +
                            "[Created] [datetime2](7) NOT NULL," +
                        " CONSTRAINT [PK_StoreInScanner] PRIMARY KEY CLUSTERED ([Id] ASC " +
                        ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY] " +
                        ") ON[PRIMARY] " +

                       "ALTER TABLE[dbo].[StoreInScanner] ADD CONSTRAINT[DF_StoreInScanner_created]  DEFAULT(getdate()) FOR[created] " +
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
            //sql = " Update Artikel SET Glowdate = 0 ";            
            return sql;
        }
    }
}
