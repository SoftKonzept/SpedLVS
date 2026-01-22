namespace Sped4.Classes.Update
{
    public class up1296
    {
        /// <summary>
        ///             Table LEingang um das Datenfeld Umlagerung (bit) erweitern
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1296 = "1296";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('LEingang','Umbuchung') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [Umbuchung] [bit] DEFAULT((0)); " +
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
            sql = " Update LEingang SET Umbuchung = 0 ";
            sql += " Update LEingang SET Umbuchung = 1 " +
                        " where " +
                            " ID IN ( " +
                                      "SELECT DISTINCT LEingangTableID " +
                                                    "FROM Artikel " +
                                                    " where ArtIDAlt in ( " +
                                                                "(SELECT ID FROM Artikel where UB = 1 and ArtIDAlt = 0)" +
                                                                        ") " +
                                                            "and UB = 0 and LEingangTableID>0" +
                                    ")";
            return sql;
        }
    }
}
