namespace Communicator.Classes
{
    public class up1035
    {
        /// <summary>
        ///             BUILT 1.3.2.0
        ///             Erweiterung VDAClientOut um Spalte ASNArtId
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1035 = "1035";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Queue','ASNArtId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Queue] ADD [ASNArtId] [int] Default ((0)); " +
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
            sql = "Update Queue SET ASNArtId=0;";
            return sql;
        }
    }
}
