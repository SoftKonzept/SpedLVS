
namespace Communicator.Classes
{
    public class up1058

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1058 = "1058";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('EdifactValue','OrderId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdifactValue] ADD [OrderId] [int] default(0);" +
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
            sql = "Update EdifactValue SET OrderId = 0 ";

            return sql;
        }
    }
}
