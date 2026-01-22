
namespace Communicator.Classes
{
    public class up1055

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1055 = "1055";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('EdiDelforD97AValue','Description') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiDelforD97AValue] ADD [Description] [nvarchar](254) NULL;" +
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
            sql = "Update EdiDelforD97AValue " +
                             "SET Description = ''; ";

            return sql;
        }
    }
}
