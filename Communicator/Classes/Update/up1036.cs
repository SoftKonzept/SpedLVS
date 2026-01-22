namespace Communicator.Classes
{
    public class up1036
    {
        /// <summary>
        ///             BUILT 1.3.2.9
        ///             Problem BMW: 
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1036 = "1036";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Jobs','CheckCloneFilePath') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Jobs] ADD [CheckCloneFilePath] [nvarchar] (254) DEFAULT(('')) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Jobs','CheckCloneFileName') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Jobs] ADD [CheckCloneFileName] [nvarchar] (254) DEFAULT(('')) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Jobs','CheckCloneFile') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Jobs] ADD [CheckCloneFile] [bit] NOT NULL DEFAULT ((0)); " +
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
            sql = "Update Jobs SET " +
                        "CheckCloneFilePath ='' " +
                        ", CheckCloneFileName='' " +
                        ", CheckCloneFile = 0 ";
            return sql;
        }
    }
}
