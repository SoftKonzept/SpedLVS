namespace Communicator.Classes
{
    public class up1060
    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1060 = "1060";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('EdiZQMQalityXml','WorkspaceXmlRef') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiZQMQalityXml] ADD [WorkspaceXmlRef]  [nvarchar](50) null;" +
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
            sql = "Update EdiZQMQalityXml ";
            sql += "SET WorkspaceXmlRef = '' ";

            return sql;
        }
    }
}
