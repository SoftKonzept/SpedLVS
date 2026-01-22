namespace Communicator.Classes
{
    public class up1043

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1043 = "1043";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('EdiSegmentElement','IsActive') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegmentElement] ADD [IsActive] [bit] DEFAULT(1);" +
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
            sql = "Update EdiSegmentElement " +
                             "SET IsActive = 1; ";

            return sql;
        }
    }
}
