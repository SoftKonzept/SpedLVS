namespace Communicator.Classes
{
    public class up1031
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1031 = "1031";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Queue','ASNActionTableID') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Queue] ADD [ASNActionTableID] [int] DEFAULT((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Queue','UseOldPropertyValue') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Queue] ADD [UseOldPropertyValue] [bit] DEFAULT((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Jobs','ViewProzessName') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Jobs] ADD [ViewProzessName] [nvarchar] (254) DEFAULT(('')) NOT NULL; " +
                  "END ";
            return sql;
        }
    }
}
