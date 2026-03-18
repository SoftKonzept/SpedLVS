namespace Communicator.Classes
{
    public class up1030
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1030 = "1030";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNAction','UseOldPropertyValue') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ASNAction] ADD [UseOldPropertyValue] [bit] default ((0)) NOT NULL; " +
                  "END ";
            return sql;
        }
    }
}
