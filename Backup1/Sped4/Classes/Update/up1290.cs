namespace Sped4.Classes.Update
{
    public class up1290
    {
        /// <summary>
        ///             30.01.2019 - mr
        ///             - in der Tabelle sollen alle Änderungen in den Tabellen Artikel, LEingang, LAusgang dokumentiert werden
        ///             - wird benötigt, da bei Storno-Korrektur-Verfahren BMW ein Lagermeldung mit den alten, nicht korrigierte Daten erwartet
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1290 = "1290";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjPropertyChanges]') AND type in (N'U')) " +
                  "BEGIN " +
                        "CREATE TABLE [dbo].[ObjPropertyChanges]( " +
                        "[ID][int] IDENTITY(1, 1) NOT NULL, " +
                        "[TableId] [int] NULL, " +
                        "[TableName] [nvarchar] (100) NULL, " +
                        "[Property] [nvarchar] (254) NULL, " +
                        "[ValueOld] [text] NULL, " +
                        "[ValueNew] [text] NULL, " +
                        "[UserId] [int] NULL, " +
                        "[CreateDate] [datetime2] (7) NULL " +
                        ") ON[PRIMARY] TEXTIMAGE_ON[PRIMARY] " +

                        " ALTER TABLE[dbo].[ObjPropertyChanges] ADD CONSTRAINT[DF_ObjPropertyChanges_CreateDate]  DEFAULT(getdate()) FOR[CreateDate] " +

                   "END ";
            return sql;
        }
    }
}
