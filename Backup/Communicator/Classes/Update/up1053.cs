namespace Communicator.Classes
{
    public class up1053

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1053 = "1053";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASN','EdiMessageValue') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ASN] ADD [EdiMessageValue] [Text];" +
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
            sql = "Update ASN " +
                             "SET EdiMessageValue = ''; ";

            return sql;
        }
    }
}
