namespace Sped4.Classes.Update
{
    public class up1292
    {
        /// <summary>
        ///             damit eine Unterscheidung der Texte für die einzelnen Arbeitsbereich erreicht wird
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1292 = "1292";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ADRText','IsReceiver') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ADRText] ADD [IsReceiver] [bit] DEFAULT((0)); " +
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
                        "IsReceiver=0";
            return sql;
        }
    }
}
