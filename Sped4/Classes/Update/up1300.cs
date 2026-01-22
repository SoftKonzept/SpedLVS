namespace Sped4.Classes.Update
{
    public class up1300
    {
        /// <summary>
        ///             NEU Table InventoryArticle (Inventur)
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1300 = "1300";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InventoryArticle]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[InventoryArticle](" +
                            "[Id][int] IDENTITY(1,1) NOT NULL," +
                            "[InventoryId] [int] NOT NULL, " +
                            "[Description] [nvarchar](max)NULL," +
                            "[ArtikelId] [int] NULL," +
                            "[Status] [int] NULL," +
                            "[Text] [nvarchar](max)NULL," +
                            "[Created] [datetime2](7) NOT NULL," +
                            "[Scanned] [datetime2](7) NULL," +
                        "CONSTRAINT[PK_InventoryArtikel] PRIMARY KEY CLUSTERED(" +
                        "[Id] ASC" +
                        ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
                        ") ON[PRIMARY] TEXTIMAGE_ON[PRIMARY] " +

                        "ALTER TABLE[dbo].[InventoryArticle] ADD CONSTRAINT[DF_InventoryArtikel_InventoryId]  DEFAULT((0)) FOR[InventoryId] " +
                        "ALTER TABLE[dbo].[InventoryArticle] ADD CONSTRAINT[DF_InventoryArtikel_created]  DEFAULT(getdate()) FOR[created] " +
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
