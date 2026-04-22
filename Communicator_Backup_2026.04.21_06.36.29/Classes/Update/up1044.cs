namespace Communicator.Classes
{
    public class up1044

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1044 = "1044";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('VDAClientOUT','Description') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [VDAClientOUT] ADD [Description] [nvarchar] (254);" +
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
            sql = "Update VDAClientOUT " +
                             "SET Description = ''; ";

            return sql;
        }
    }
}
