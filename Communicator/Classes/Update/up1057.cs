
namespace Communicator.Classes
{
    public class up1057

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1057 = "1057";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('EdiZQMQalityXml','Description') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiZQMQalityXml] ADD [Description] [Text] NULL;" +
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
            sql = "Update EdiZQMQalityXml " +
                             "SET Description = '' ";

            return sql;
        }
    }
}
