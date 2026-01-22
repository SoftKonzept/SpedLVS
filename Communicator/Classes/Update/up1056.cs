
namespace Communicator.Classes
{
    public class up1056

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1056 = "1056";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('EdiZQMQalityXml','LfsNr') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiZQMQalityXml] ADD [LfsNr] [nvarchar](30) NULL;" +
                  "END " +
                  "IF COL_LENGTH('EdiZQMQalityXml','Produktionsnummer') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiZQMQalityXml] ADD [Produktionsnummer] [nvarchar](30) NULL;" +
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
                             "SET LfsNr = '' " +
                             ", Produktionsnummer = ''; ";

            return sql;
        }
    }
}
