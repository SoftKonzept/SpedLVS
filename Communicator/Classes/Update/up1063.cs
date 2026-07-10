namespace Communicator.Classes
{
    public class up1063
    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1063 = "1063";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('EdiClientWorkspaceValue','Client') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiClientWorkspaceValue] ADD [Client] [int] ; " +
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

            sql += " Update EdiClientWorkspaceValue SET ";
            sql += "Client = 0 ";
            sql += "; ";

            return sql;
        }
    }
}
