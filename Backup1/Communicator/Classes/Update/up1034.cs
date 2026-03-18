namespace Communicator.Classes
{
    public class up1034
    {
        /// <summary>
        ///             BUILT 1.2.4.2
        ///             EA.Anzahl wurde angepasst und ist nun EA.ArtikelCount
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1034 = "1034";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = " Update VDAClientOUT " +
                            "SET ValueArt = 'EA.ArtikelCount' " +
                            "WHERE " +
                                "ID IN(" +
                                        "SELECT ID FROM VDAClientOUT where ValueArt = 'EA.Anzahl' " +
                                      ");";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string SqlStringUpdate_UpdateExistingColumns()
        {
            string sql = string.Empty;
            return sql;
        }
    }
}
