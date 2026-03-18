namespace Communicator.Classes
{
    public class up1037
    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1037 = "1037";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Jobs','FTPUsePassiveMode') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Jobs] ADD [FTPUsePassiveMode] [bit] NOT NULL DEFAULT ((0));" +
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
            //sql = "Update Jobs SET " +
            //            "CheckCloneFilePath ='' " +
            //            ", CheckCloneFileName='' " +
            //            ", CheckCloneFile = 0 ";
            return sql;
        }
    }
}
