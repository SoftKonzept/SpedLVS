
namespace Communicator.Classes
{
    public class up1047

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1047 = "1047";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Jobs','DelforVerweis') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Jobs] ADD [DelforVerweis] [nvarchar] (30);" +
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
            sql = "Update Jobs " +
                             "SET DelforVerweis = ''; ";

            return sql;
        }
    }
}
