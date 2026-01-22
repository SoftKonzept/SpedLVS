namespace Sped4.Classes.Update
{
    public class up1291
    {
        /// <summary>
        ///             damit eine Unterscheidung der Texte für die einzelnen Arbeitsbereich erreicht wird
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1291 = "1291";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ADRText','ArbeitsbereichID') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ADRText] ADD [ArbeitsbereichID] [int] DEFAULT((0)); " +
                  "END " +
                  "IF COL_LENGTH('ADRText','UseForAll') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ADRText] ADD [UseForAll] [bit] DEFAULT((1)); " +
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
            sql = "Update ADRText Set " +
                        "ArbeitsbereichID=0" +
                        ", UseForAll=1;";
            return sql;
        }
    }
}
