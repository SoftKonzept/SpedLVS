namespace Sped4.Classes.Update
{
    public class up1299
    {
        /// <summary>
        ///             NEU Table Inventory (Inventur)
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1299 = "1299";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inventories]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE[dbo].[Inventories](" +
                            "[Id][int] IDENTITY(1,1) NOT NULL," +
                            "[Name] [nvarchar](100) NOT NULL," +
                            "[Description] [text] NULL," +
                            "[Art] [int] NOT NULL," +
                            "[Created] [datetime2](7) NOT NULL," +
                            "[UserId] [int] NOT NULL, " +
                            "[ArbeitsbereichId] [int] NOT NULL, " +
                        "CONSTRAINT[PK_Inventories] PRIMARY KEY CLUSTERED" +
                        "(" +
                          "[Id] ASC" +
                          ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
                        ") ON[PRIMARY] " +

                        "ALTER TABLE[dbo].[Inventories] ADD CONSTRAINT[DF_Inventories_Art]  DEFAULT((0)) FOR[Art] " +
                        "ALTER TABLE[dbo].[Inventories] ADD CONSTRAINT[DF_Inventories_created]  DEFAULT(getdate()) FOR[created] " +
                        "ALTER TABLE[dbo].[Inventories] ADD CONSTRAINT[DF_Inventories_UserId]  DEFAULT((0)) FOR[UserId] " +
                        "ALTER TABLE[dbo].[Inventories] ADD CONSTRAINT[DF_Inventories_ArbeitsbereichID]  DEFAULT((0)) FOR[ArbeitsbereichID] " +
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
