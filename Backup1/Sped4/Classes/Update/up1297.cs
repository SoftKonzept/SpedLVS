namespace Sped4.Classes.Update
{
    public class up1297
    {
        /// <summary>
        ///             Table ADR um das Datenfeld DUNSNR (int) erweitern
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1297 = "1297";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ADR','DUNSNr') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ADR] ADD [DUNSNr] [int] DEFAULT((0)); " +
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
            sql = " Update ADR SET DUNSNr = 0 ";
            return sql;
        }
    }
}
